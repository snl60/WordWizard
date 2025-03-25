using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace SpellingBeeGame
{
    public partial class MainWindow : Window
    {
        private List<string> words = new List<string> { "butterfly", "elephant", "giraffe", "kangaroo", "dolphin" };
        private string correctWord;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            SelectRandomWord();
        }

        private void SelectRandomWord()
        {
            correctWord = words[random.Next(words.Count)];
            WordLabel.Content = $"Spell this word: {new string('_', correctWord.Length)}";
        }

        private void CheckSpellingButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string userWord = UserInputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(userWord))
            {
                ResultLabel.Content = "Please enter a word.";
                return;
            }

            if (string.Equals(userWord, correctWord, StringComparison.OrdinalIgnoreCase))
            {
                ResultLabel.Content = "Correct! Well done!";
                SelectRandomWord(); // Select a new word after a correct answer
            }
            else
            {
                ResultLabel.Content = $"Incorrect. The correct spelling is: {correctWord}";
                SelectRandomWord();
            }
        }
    }
}
