using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Andre--
//Focus on lines marked with @@ at the end for skimming to main points.-Andre
//UMPIRE-Understand, -Match, -Plan, -Implementation, -Review, -Evaluate

/*
 Understand:
-For scoring logic, we need to define our source of points@@
-Points from Correct Words*@@
(Let's say a correct word is worth 30 points)@@

-Points from lives maintained(3 lives, 10 points each)**@@
(Let's say the number of lives they have is multiplied by 10 at the end
for the bonus points)@@

Let's say the total word limit is 5, to win, they must guess 5/5,
as well as maintain at least 1 life.@@

Parameters:
5/5 Correct: 5*30 = 150 Points max@@
3/3 Lives Maintained: 3*10 = 30 Bonus Points Max@@

Now, How will we tally this? Where does the 'End Screen' come into
play?

Match:
We can define some variables to start;
-int wordLimit(The total words the Player must spell correct to win)@@

-int correctWords(A variable which increments by 1 for each correct@@
word the user spells in the game.)

- double wordPoints = correctWords * 30;(Defines the points based@@
off of the games multiplier, and how many times the player was
correct in spelling.)//Implicit Cast int -> double?

-int lives = 3;(the hearts our player has, if it reaches 0,@@
the player is sent to the end screen of loss, and their correct
words are tallied, but they get NO bonus points,)

-double bonusPoints = lives * 10;(Bonus points possible)@@

-double totalPoints = bonusPoints + wordPoints;(Total point tally,@@
allocated at the end)

Plan:
We've defined our variables and out scenario, now for the conditions.
-The EndScreen, is a UI event function, reached based off of 2 main
conditions.
1. Player reaches 0 lives -> function call for loss menu@@
2. Player reaches goal of 5/5 word -> function call for win menu@@

For 1. requires the game to be within a loop of some sort,
our starting variables must be outside this loop.


Implementation:

-Scoring Class
-Attributes: 

int correctWords; 

double wordPoints = correctWords * pointMultiplier;  

int lives = 3;  

int pointMulitplier = 30; 

int bonusPointMulitplier = 10;

double bonusPoints = lives * bonusPointsMultiplier; 

double totalPoints = wordPoints + bonusPoints;

(Scope for all of these?)

-Functions: 
EndScreenLoss(), 
EndScreenWin()
(Scope and type?)

Questions: How would I connect the class to the object event?
Solution: We can try to connect UI objects to the class,
we achieve this through namespace referencing the class file,
and the UI elements file.(Not sure, if it will work as I expect it,
but got to try something).

//I will hard-code Player lives, pointMuliplier, and 
bonusPointMultiplier for now.


-End Screen UI Elements:@@

Loss Screen:
-End Screen Background Canvas: Bigger encompassing Popup, which 
contains all the UI elements I mention below.

-ReadOnly Textbox: Total Points(post-game calculation)

-ReadOnly TextBox: You're out of lives!

-Retry Button: Retry(Restarts round with same round parameters)

-Menu Button: Main Menu(Restarts game code basically, taking them
back to the main menu)

-Exit Button: Exit?(Closes the program, or we could also
have the button activate another popup with another button,
with the popup this time say, "Are you sure you want to quit?'
(nice to have type feature))

Win Screen:
Basically the same except the textbox saying, 
"You're out of lives!" instead says, "You won!"

 */
namespace ScoringData
{

    internal class Scoring
    {
        //Fields
        private int? lives = null;
         
        private int? correctWords = null;

        private int? pointMultiplier = null;

        private int? bonusPointMultiplier = null;

        private double totalPoints;

        private double bonusPoints;

        private double wordPoints;

        //static private double bonusPoints = lives * bonusPointMultiplier;

        // static private double wordPoints = correctWords * pointMultiplier;

        //static private double totalPoints = bonusPoints + wordPoints;

        /*Made static for convenince, if we don't go with staic, I'll probably make additional functions and remove the following attributes:
          'private int double wordPoints;
          'private int double totalPoints;
          'private int double bonusPoints;

        Additionally, if we're working with object instances free parameter
        specification, I could set all attributes to null values and make
        accessor methods(setters/getters), and constructors for object instances.
        (if that's the route we go).-Andre
        */
        //Fields






        //Cosntructors

        public Scoring() 
        {
            this.lives = 3;
            this.correctWords = 0;
            this.pointMultiplier = 30;
            this.bonusPointMultiplier = 10;
            this.totalPoints = 0;
            this.bonusPoints = 0;
            this.totalPoints = 0;
        }

        //Constructors


        //Functions


        public int Lives 
        {
            get 
            { 
                return (int)lives; 
            }
            set 
            { 
                lives = value; 
            }
        }

        public int CorrectWords //How would I connect this variable to the main runtime of the game as a counter?
        {//This should increment by +1 in main program if spelling validation logic is completely correct for a word. Otherwise, if it's not correct, it will not increment.
            get 
            {
                return (int)correctWords;
            }
            set 
            {
                correctWords = value;
            }
        }

        public int PointMultiplier 
        {
            get 
            {
                return (int)pointMultiplier;
            }
            set 
            {
                pointMultiplier = value;
            }
        }

        public int BonusPointMultiplier 
        {
            get 
            {
                return (int)bonusPointMultiplier;
            }

            set 
            {
                bonusPointMultiplier = value;
            }
        }

        public double TotalPoints 
        {
            get 
            {
                calculateTotalPoints();
                return totalPoints;
            }
        }

        public double BonusPoints 
        {
            get 
            {
                calculateBonusPoints();
                return bonusPoints;
            }
        }

        public double WordPoints 
        {
            get 
            {
                calculateWordPoints();
                return wordPoints;
            }
        }




        public double calculateWordPoints() 
        {
            wordPoints = (double)this.correctWords * (double)this.pointMultiplier;
            return wordPoints;
        }

        public double calculateBonusPoints() 
        {
            bonusPoints = (double)this.lives * (double)this.bonusPointMultiplier;
            return bonusPoints;
        }

        public double calculateTotalPoints() 
        {
            totalPoints = wordPoints + bonusPoints;
            return totalPoints;
        }

        public void EndScreenWin() 
        {
            /*double wordPoints = (double)this.correctWords * (double)this.pointMultiplier;
            double bonusPoints = (double)this.lives * (double)this.bonusPointMultiplier;
            double totalPoints = wordPoints + bonusPoints;*/

        }

        public void EndScreenLose() 
        {
            /*double wordPoints = (double)this.correctWords * (double)this.pointMultiplier;
            double bonusPoints = (double)this.lives * (double)this.bonusPointMultiplier;
            double totalPoints = wordPoints + bonusPoints;*/
        }

        /*
        So, on the context of class type, I'm just using internal
        as a placeholder, I'm not sure if we're aiming for abstract
        or virtual functions for overriding, but I'm fine with
        changing it in the future if this does becomes the case-Andre.

         */

        //Functions
    }
}

