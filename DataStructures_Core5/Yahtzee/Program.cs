﻿using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Yahtzee
{
    class Program
    {
        const int NONE = -1;
        const int ONES = 0;
        const int TWOS = 1;
        const int THREES = 2;
        const int FOURS = 3;
        const int FIVES = 4;
        const int SIXES = 5;
        const int THREE_OF_A_KIND = 6;
        const int FOUR_OF_A_KIND = 7;
        const int FULL_HOUSE = 8;
        const int SMALL_STRAIGHT = 9;
        const int LARGE_STRAIGHT = 10;
        const int CHANCE = 11;
        const int YAHTZEE = 12;
        const int SUBTOTAL = 13;
        const int BONUS = 14;
        const int TOTAL = 15;

        static void Main(string[] args)
        {
            /*
             * declare variables for the user's scorecard and the computer's scorecard
             * declare a variable for the number of turns the user has taken and another for the number of moves the computer has taken
             * declare a boolean that knows if it is the user's turn and set it to false
             * 
             * call ResetScorecard for the user
             * call ResetScorecard for the computer
             * 
             * set the window size of the console window because displaying the scorecard requires about 50 lines
             * 
             * do
             *      set the userTurn variable to the opposite value
             *      call UpdateScorecard for the user
             *      call UpdateScorecard for the computer
             *      call DisplayScorecards
             *      if it's the user's turn
             *          display a message
             *          you might also want to pause
             *          call UserPlay
             *      else
             *          display a message
             *          call ComputerPlay
             *      end if
             *  while both the user's count and the computer's count are <= yahtzee
             *  
             *  call UpdateScorecard for the user
             *  call UpdateScorecard for the computer
             *  call DisplayScorecards
             *  
             *  display a message about who won
             */

            int[] userScoreCard = new int[TOTAL];
            int[] compScoreCard = new int[TOTAL];
            int userMoves = 0;
            int compMoves = 0;
            bool userTurn = false;

            //ResetScorecard(userScoreCard);
            //ResetScorecard(compScoreCard);

            Console.WindowHeight = 30;
            Console.WriteLine("It is your turn");
            UserPlay(userScoreCard, userMoves);
            Console.ReadLine();
            /*do
            {
                userTurn = !(userTurn);
                UpdateScorecard(userScoreCard);
                UpdateScorecard(compScoreCard);
                
                if (userTurn)
                {
                    Console.WriteLine("It is your turn");
                    UserPlay(userScoreCard, userMoves);
                    Console.ReadLine();
                }
                /*else
                {
                    Console.WriteLine("It is the computers turn");
                    ComputerPlay(compScoreCard, compMoves);
                    Console.ReadLine();
                }
            } while (userMoves <= YAHTZEE && compMoves <= YAHTZEE); */

            /*do
            *set the userTurn variable to the opposite value
         *call UpdateScorecard for the user
         *call UpdateScorecard for the computer
         *call DisplayScorecards
         *      if it's the user's turn
         *display a message
         *you might also want to pause
         * call UserPlay
         *      else
            *display a message
         *call ComputerPlay
         * end if
         *  while both the user's count and the computer's count are <= yahtzee */

            Console.ReadLine();
        }

        #region Scorecard Methods

        // sets all of the items in a scorecard to -1 to start the game
        // takes a data structure for a scorecard and the corresponding score card count as parameters.  Both are altered by the method.
        static void ResetScorecard(int[] scoreCard)
        {
            for (int i = 0; i < scoreCard.Length; i++)
            {
                scoreCard[i] = -1;
            }
        }

        // calculates the subtotal, bonus and the total for the scorecard
        // takes a data structure for a scorecard as it's parameter
        static void UpdateScorecard(int[] scorecard)
        {
            
            scorecard[SUBTOTAL] = 0;
            scorecard[BONUS] = 0;
            for (int i = ONES; i <= SIXES; i++)
                if (scorecard[i] != -1)
                    scorecard[SUBTOTAL] += scorecard[i];

            if (scorecard[SUBTOTAL] >= 63)
                scorecard[BONUS] = 35;

            scorecard[TOTAL] = scorecard[SUBTOTAL] + scorecard[BONUS];
            for (int i = THREE_OF_A_KIND; i <= YAHTZEE; i++)
                if (scorecard[i] != -1)
                    scorecard[TOTAL] += scorecard[i];
            
        }

        static string FormatCell(int value)
        {
            return (value < 0) ? "" : value.ToString();
        }

        // takes the data structure for the user's scorecard and the data structure for the computer's scorecard as parameters
        static void DisplayScoreCards(int[] uScorecard, int[]cScorecard)
        {
            
            string[] labels = {"Ones", "Twos", "Threes", "Fours", "Fives", "Sixes",
                                "3 of a Kind", "4 of a Kind", "Full House", "Small Straight", "Large Straight",
                                "Chance", "Yahtzee", "Sub Total", "Bonus", "Total Score"};
            string lineFormat = "| {3,2} {0,-15}|{1,8}|{2,8}|";
            string border = new string('-', 39);

            Console.Clear();
            Console.WriteLine(border);
            Console.WriteLine(String.Format(lineFormat, "", "  You   ", "Computer", ""));
            Console.WriteLine(border);
            for (int i = ONES; i <= SIXES; i++)
            {
                Console.WriteLine(String.Format(lineFormat, labels[i], FormatCell(uScorecard[i]), FormatCell(cScorecard[i]), i));
            }
            Console.WriteLine(border);
            Console.WriteLine(String.Format(lineFormat, labels[SUBTOTAL], FormatCell(uScorecard[SUBTOTAL]), FormatCell(cScorecard[SUBTOTAL]), ""));
            Console.WriteLine(border);
            Console.WriteLine(String.Format(lineFormat, labels[BONUS], FormatCell(uScorecard[BONUS]), FormatCell(cScorecard[BONUS]), ""));
            Console.WriteLine(border);
            for (int i = THREE_OF_A_KIND; i <= YAHTZEE; i++)
            {
                Console.WriteLine(String.Format(lineFormat, labels[i], FormatCell(uScorecard[i]), FormatCell(cScorecard[i]), i));
            }
            Console.WriteLine(border);
            Console.WriteLine(String.Format(lineFormat, labels[TOTAL], FormatCell(uScorecard[TOTAL]), FormatCell(cScorecard[TOTAL]), ""));
            Console.WriteLine(border);
            
        }
        #endregion

        #region Rolling Methods
        // rolls the specified number of dice and adds them to the data structure for the dice
        // takes an integer that represents the number of dice to roll and a data structure to hold the dice as it's parameters
        static void Roll(int numberOfDice, List<int> dice)
        {
            Random rand = new Random();
            for (int i = 0; i < numberOfDice; i++)
            {
                int roll = rand.Next(1,7);
                dice.Add(roll);
            }
        }

        // takes a data structure that is a set of dice as it's parameter.  Call it dice.
        static void DisplayDice(List<int> dice)
        {
            
            string lineFormat = "|   {0}  |";
            string border = "*------*";
            string second = "|      |";

            foreach (int d in dice)
                Console.Write(border);
            Console.WriteLine();
            foreach (int d in dice)
                Console.Write(second);
            Console.WriteLine("");
            foreach (int d in dice)
                Console.Write(String.Format(lineFormat, d));
            Console.WriteLine();
            foreach (int d in dice)
                Console.Write(second);
            Console.WriteLine("");
            foreach (int d in dice)
                Console.Write(border);
            Console.WriteLine();
            
        }
        #endregion

        #region Computer Play Methods

        // figures out the highest possible score for the set of dice for the computer
        // takes the scorecard datastructure and the set of dice that the computer is keeping as it's parameters
        static int GetComputerScorecardItem(/* TODO */)
        {
            int indexOfMax = 0;
            int max = 0;

            /* you can uncomment this code once you've identified the parameters for this method
            for (int i = ONES; i <= YAHTZEE; i++)
            {
                if (scorecard[i] == -1)
                {
                    int score = Score(i, keeping);
                    if (score >= max)
                    {
                        max = score;
                        indexOfMax = i;
                    }
                }
            }
            */

            return indexOfMax;
        }

        // implements the computer's turn.  The computer only rolls once.
        // You can earn extra credit for making the computer play smarter
        // takes the computer's scorecard data structure and scorecard count as parameters.  Both are altered by the method.
        static void ComputerPlay(/* TODO */)
        {
            /* you can uncomment this code once you declare the parameters
            declare a data structure for the dice that the computer is keeping.  Call it keeping.
            int itemIndex = -1;

            Roll(5, keeping);
            Console.WriteLine("The dice the computer rolled: ");
            DisplayDice(keeping);
            Pause();
            Pause();

            itemIndex = GetComputerScorecardItem(cScorecard, keeping);
            cScorecard[itemIndex] = Score(itemIndex, keeping);
            cScorecardCount++;
            */
        }
        #endregion

        #region User Play Methods

        // moves the dice that the user want to keep from the rolling data structure to the keeping data structure
        // takes the rolling data structure and the keeping data structure as parameters
        static void GetKeeping(List<int> rollingDice, List<int> keepingDice)
        {

        }

        // on the last roll moves the dice that the user just rolled into the data structure for the dice that the user is keeping
        static void MoveRollToKeep(/* TODO */)
        {
            // iterate through the rolling data structure and copy each item into the keeping data structure
            // clear the rollling data structure
        }

        // asks the user which item on the scorecard they want to score 
        // must make sure that the scorecard doesn't already have a value for that item
        // remember that the scorecard is initialized with -1 in each item
        // takes a scorecard data structure as it's parameter 
        static int GetScorecardItem(/* TODO */)
        {
            return -1;
        }

        // implments the user's turn
        // takes the user's scorecard data structure and the user's move count as parameters.  Both will be altered by the method.
        static void UserPlay(int[] userScoreCard, int userMoves)
        {

            //declare a data structure for the dice that the user is rolling
            List<int> rollingDice = new List<int>();
       
            //declare a data structure for the dice that the user is keeping
            List<int> keepingDice = new List<int>();
            // declare a variable for the number of rolls
            int rolls = 0;
            //declare a variable for the scorecard item that the user wants to score on this turn
            int scorecardItem;
            do
            {
                Roll(rolls, rollingDice);
                rolls++;
                DisplayDice(rollingDice);
                if (rolls < 3)
                    GetKeeping(rollingDice,keepingDice);
                else
                    MoveRollToKeep();
                DisplayDice(keepingDice);
            } while (rolls < 3 && keepingDice.Count < 5);
            /* do
            *      Call Roll
            *      increment the number of rolls
            *      display a message
            *      Call DisplayDice
            *      if the number of rolls is < 3
            *          Call GetKeeping
            *      else
            *          Call MoveRollToKeeping
            *      end if
            *      Call DisplayDice
            * while the number of rolls < 3 and the number of dice the user is keeping is < 5 */

            // Call GetScorecardItem
            GetScorecardItem();
            //all Score
         
             // Increment the scorecard count
             
        }

        #endregion

        #region Scoring Methods

        // counts how many of a specified value are in the set of dice
        // takes the value that you're counting and the data structure containing the set of dice as it's parameter
        // returns the count
        static int Count(int value,List<int> dice)
        {
            int count = 0;
            foreach (int die in dice)
            {
                if (die == value)
                    count++;
            }
            return count;
        }

        // counts the number of ones, twos, ... sixes in the set of dice
        // takes a data structure for a set of dice as it's parameter
        // returns a data structure that contains the count for each dice value
        static int[] GetCounts(List<int> dice)
        {
            int[] counter = new int[6];
            for (int i = 1; i < 6; i++)
            {
                counter[i] = Count(i, dice);
         
            }
            return counter;
        }

        // adds the value of all of the dice based on the counts
        // takes a data structure that represents all of the counts as a parameter
        static int Sum(int[] counts)
        {
            int sum = 0;
             
            for (int i = ONES; i <= SIXES; i++)
            {
                int value = i + 1;
                sum += (value * counts[i]);
            }
            
            return sum;
        }

        // determines if you have a specified count based on the counts
        // takes a data structure that represents all of the counts as a parameter
        static bool HasCount(int howMany, int[] counts)
        {
            foreach (int count in counts)
                if (howMany == count)
                    return true;
            
            return false;
        }

        // chance is the sum of the dice
        // takes a data structure that represents all of the counts as a parameter
        static int ScoreChance(/* TODO */)
        {
            return 0;
        }

        // calculates the score for ONES given the set of counts (from GetCounts)
        // takes a data structure that represents all of the counts as a parameter
        static int ScoreOnes(/* TODO */)
        {
            // you can comment out this line when you have declared the parameters
            // return counts[ONES] * 1;
            return 0;
        }

        // WRITE ALL OF THESE: ScoreTwos, ScoreThrees, ScoreFours, ScoreFives, ScoreSies

        // scores 3 of a kind.  4 of a kind or 5 of a kind also can be used for 3 of a kind
        // the sum of the dice are used for the score
        // takes a data structure that represents all of the counts as a parameter
        static int ScoreThreeOfAKind(int[] counts)
        {
            return 0;
        }

        // WRITE ALL OF THESE: ScoreFourOfAKind, ScoreYahtzee - a yahtzee is worth 50 points

        // takes a data structure that represents all of the counts as a parameter
        static int ScoreFullHouse(int[] counts)
        {
            
            if (HasCount(2, counts) && HasCount(3, counts))
                return 25;
            else
                return 0;

        }

        // takes a data structure that represents all of the counts as a parameter
        static int ScoreSmallStraight(int[] counts)
        {
            
            for (int i = THREES; i <= FOURS; i++)
            {
                if (counts[i] == 0)
                    return 0;
            }
            if ((counts[ONES] >= 1 && counts[TWOS] >= 1) ||
                (counts[TWOS] >= 1 && counts[FIVES] >= 1) ||
                (counts[FIVES] >= 1 && counts[SIXES] >= 1))
                return 30;
            else
            
            return 0;
        }

        // takes a data structure that represents all of the counts as a parameter
        static int ScoreLargeStraight(int[] counts)
        {
            
            for (int i = TWOS; i <= FIVES; i++)
            {
                if (counts[i] == 0)
                    return 0;
            }
            if (counts[ONES] == 1 || counts[SIXES] == 1)
                return 40;
            else

            return 0;
        }

        // scores a score card item based on the set of dice
        // takes an integer which represent the scorecard item as well as a data structure representing a set of dice as parameters
        static int Score(int[] counts)
        {
            /* you can uncomment this code once you declare the parameter
            int[] counts = GetCounts(dice);
            switch (whichElement)
            {
                case ONES:
                    return ScoreOnes(counts);
                case TWOS:
                    return ScoreTwos(counts);
                case THREES:
                    return ScoreThrees(counts);
                case FOURS:
                    return ScoreFours(counts);
                case FIVES:
                    return ScoreFives(counts);
                case SIXES:
                    return ScoreSixes(counts);
                case THREE_OF_A_KIND:
                    return ScoreThreeOfAKind(counts);
                case FOUR_OF_A_KIND:
                    return ScoreFourOfAKind(counts);
                case FULL_HOUSE:
                    return ScoreFullHouse(counts);
                case SMALL_STRAIGHT:
                    return ScoreSmallStraight(counts);
                case LARGE_STRAIGHT:
                    return ScoreLargeStraight(counts);
                case CHANCE:
                    return ScoreChance(counts);
                case YAHTZEE:
                    return ScoreYahtzee(counts);
                default:
                    return 0;
            }
            */
            return 0;
        }

        #endregion

        static void Pause()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }
}
