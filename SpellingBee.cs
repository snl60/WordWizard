using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Speech.Synthesis;
using System.Windows.Threading;

namespace SpellingBeeGame
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, List<string>> wordLists = new Dictionary<string, List<string>>
        {
            { "Easy", new List<string> { "cat", "dog", "sun", "bat", "fish", "apple", "book" } },
            { "Medium", new List<string> { "giraffe", "dolphin", "penguin", "banana", "elephant", "castle" } },
            { "Hard", new List<string> { "synchronize", "knowledge", "philosophy", "consciousness", "aesthetic", "quarantine" } }
        };

        private string currentWord = "";
        private int score = 0;
        private int lives = 3;
        private int timeLeft = 10;  
        private DispatcherTimer timer;
        private SpeechSynthesizer speechSynthesizer;
        private string difficultyLevel = "Easy";
        private string leaderboardFile = "leaderboard.txt";

        public MainWindow()
        {
            InitializeComponent();
            speechSynthesizer = new SpeechSynthesizer();
            LoadLeaderboard();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (DifficultyComboBox.SelectedItem is ComboBoxItem selected)
            {
                difficultyLevel = selected.Content.ToString();
            }

            score = 0;
            lives = 3;
            ScoreLabel.Text = "Score: 0";
            LivesLabel.Text = "Lives: 3";
            StartNewRound();
        }

        private void StartNewRound()
        {
            if (lives == 0)
            {
                ResultLabel.Text = "Game Over! Restart to play again.";
                ResultLabel.Foreground = Brushes.Red;
                SaveHighScore();
                LoadLeaderboard();
                return;
            }

            Random random = new Random();
            List<string> wordList = wordLists[difficultyLevel];
            currentWord = wordList[random.Next(wordList.Count)];

            UserInput.Text = "";
            ResultLabel.Text = "Spell the word!";
            ResultLabel.Foreground = Brushes.Black;

            timeLeft = 10;
            timer.Start();
        }

        private void CheckSpelling_Click(object sender, RoutedEventArgs e)
        {
            if (lives == 0) return;

            string userSpelling = UserInput.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(userSpelling))
            {
                ResultLabel.Text = "Please enter a word!";
                ResultLabel.Foreground = Brushes.Red;
                return;
            }

            if (userSpelling == currentWord.ToLower())
            {
                ResultLabel.Text = "Correct!";
                ResultLabel.Foreground = Brushes.Green;
                score += difficultyLevel == "Easy" ? 5 : difficultyLevel == "Medium" ? 10 : 15;
                ScoreLabel.Text = $"Score: {score}";
            }
            else
            {
                ResultLabel.Text = $"Incorrect! The correct word was: {currentWord}";
                ResultLabel.Foreground = Brushes.Red;
                lives--;
                LivesLabel.Text = $"Lives: {lives}";
            }

            timer.Stop();
            StartNewRound();
        }

        private void SpeakWord_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentWord))
            {
                speechSynthesizer.SpeakAsync(currentWord);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
            }
            else
            {
                timer.Stop();
                ResultLabel.Text = "Time's up! You lost a life!";
                ResultLabel.Foreground = Brushes.Red;
                lives--;
                LivesLabel.Text = $"Lives: {lives}";
                StartNewRound();
            }
        }

        private void SaveHighScore()
        {
            List<int> scores = File.Exists(leaderboardFile) 
                ? File.ReadAllLines(leaderboardFile).Select(int.Parse).ToList() 
                : new List<int>();

            scores.Add(score);
            scores = scores.OrderByDescending(s => s).Take(5).ToList();
            File.WriteAllLines(leaderboardFile, scores.Select(s => s.ToString()));
        }

        private void LoadLeaderboard()
        {
            if (File.Exists(leaderboardFile))
            {
                LeaderboardList.ItemsSource = File.ReadAllLines(leaderboardFile);
            }
        }
    }
}