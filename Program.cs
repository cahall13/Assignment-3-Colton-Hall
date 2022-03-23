using System;

namespace Assignment_3_Colton_Hall
{
    class Program
    {
        static void Main(string[] args)
        {
            int currCredits = 50;//setting all ints that will be referenced and updated
            int theForceWins = 0;
            int theForceLosses = 0;
            int blastersWins = 0;
            int blastersLosses = 0;
            int bet = 0;

            string userInput = MenuChoice(); //accepts user input from MenuChoice() method
            while (userInput != "4" && currCredits>0 && currCredits<300) //sets stop parameters 
            {
                Route(userInput, ref currCredits, ref bet, ref theForceWins, ref theForceLosses, ref blastersWins, ref blastersLosses);
                userInput = MenuChoice();
            }

            GameOver(ref currCredits,ref theForceWins,ref theForceLosses,ref blastersWins,ref blastersLosses);
        }
        static string MenuChoice() //displays menu and calls method to validate before passing userInput to route
        {
            DisplayMenu();
            string userInput = Console.ReadLine();
            if (!ValidMenuChoice(userInput))
            {
                System.Console.WriteLine("Improper menu choice");
                System.Console.WriteLine("Press any key to continue");
                System.Console.ReadKey();
                Console.Clear();

                DisplayMenu();
                userInput = Console.ReadLine();
            }
            return userInput;
        }
        static void DisplayMenu() //defines menu options
        {
            Console.Clear();
            System.Console.WriteLine("Welcome Luke Skywalker");
            System.Console.WriteLine("1.    Play The Force");
            System.Console.WriteLine("2.    Play Blasters");
            System.Console.WriteLine("3.    Scoreboard");
            System.Console.WriteLine("4.    Exit Game");
        }
        static bool ValidMenuChoice(string userInput) //validates user menu choice 
        {
            if (userInput == "1" || userInput == "2" || userInput == "3" || userInput == "4")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void Route(string userInput, ref int currCredits, ref int bet, ref int theForceWins, ref int theForceLosses, ref int blastersWins, ref int blastersLosses)
        //sends user to whichever menu option they chose
        {
            if (userInput == "1")
            {
                ForceInstructions (ref currCredits, ref bet, ref theForceWins, ref theForceLosses);

            }
            if (userInput == "2")
            {
                BlastersInstructions (ref currCredits, ref bet, ref blastersWins, ref blastersLosses);
            }
            if (userInput == "3")
            {
                Scoreboard (currCredits, ref theForceWins, ref theForceLosses, ref blastersWins, ref blastersLosses);
            }
        }
        static void ForceInstructions (ref int currCredits, ref int bet,  ref int theForceWins, ref int theForceLosses)
        //shows all instructions for the Force before starting the game and asking for bet
        {
            Console.Clear();
            System.Console.WriteLine("The game will begin by Yoda laying out 10 cards");
            System.Console.WriteLine("After your 10 cards are chosen you will be asked for a guess");
            System.Console.WriteLine("You must guess if your next card is higher or lower than the previous card");
            System.Console.WriteLine("Aces are the lowest card and Kings are the highest");
            System.Console.WriteLine("If you guess correctly you will move on to your next card");
            System.Console.WriteLine("If you guess incorrectly the game is over");
            System.Console.WriteLine("If you get less than 5 correct guesses you lose your bet");
            System.Console.WriteLine("If you get 5 correct guesses you break even");
            System.Console.WriteLine("If you get 7 correct guesses you win double your bet");
            System.Console.WriteLine("If you get all 10 correct guesses you win three times your bet!");
            System.Console.WriteLine("If you would like to play press 1 to continue. If not press 2.");

            string decision = Console.ReadLine();
            if(decision == "1") //sends user to game to ask for bet
            {
                TheForce(ref currCredits, ref bet, ref theForceWins, ref theForceLosses);
            }
            else if (decision == "2") //sends user back to menu before bet screen
            {
                MenuChoice();
            }
            else //checks for valid input
            {
                System.Console.WriteLine("invalid choice please try again");
                System.Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                ForceInstructions(ref currCredits,ref bet, ref theForceWins, ref theForceLosses);
            }     
        }
        static void TheForce (ref int currCredits, ref int bet, ref int theForceWins, ref int theForceLosses)
        //asks for bet before game begins then runs game program
        {
            System.Console.WriteLine($"You currently have {currCredits} credits, How many credits would you like to bet?");
            bet = int.Parse(Console.ReadLine());
            if (bet > currCredits || bet < 1) //checks if bet is valid before beginning the game
            {
                System.Console.WriteLine($"Invalid bet, bet must be {currCredits} credits or less");
                System.Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                
                TheForce (ref currCredits, ref bet, ref theForceWins, ref theForceLosses);
            }
            Console.Clear();
            const int FULL_DECK = 52;
            string[] deck = new string [FULL_DECK];
            Cards(deck);
            GetRandomCard(deck, FULL_DECK);
            int missed = 0;
            int attempts = 0;
            int i = 0;

            while (missed == 0 && attempts < 10) //checks if game is over 
            {
                System.Console.WriteLine($"Your card is: {deck[i]}, Do you think the next card will be higher or lower? ");
                System.Console.WriteLine("Enter 1 for higher \n Enter 2 for lower");
                string UserInput = Console.ReadLine();
                CheckGuess(UserInput, i, deck, ref missed, ref attempts);
                i++;
            }
            
            if (attempts == 10) //triple bet condition
            {
                System.Console.WriteLine("You've won it all! 3 times your bet is your reward!!");
                currCredits = currCredits + bet + bet;
                theForceWins++;
                theForceEnd(ref currCredits, ref bet, ref theForceWins, ref theForceLosses);
                MenuChoice();
            }
            if (missed != 0 && attempts < 5) //losing condition
            { 
                System.Console.WriteLine("You lost your bet, better luck next time.");
                currCredits = currCredits - bet;
                theForceLosses++;
                theForceEnd(ref currCredits, ref bet, ref theForceWins, ref theForceLosses);
                MenuChoice();
            }
            if (missed !=0 && attempts > 4 && attempts <7) //break even condition
            {
                System.Console.WriteLine("You broke even!");
                theForceEnd(ref currCredits, ref bet, ref theForceWins, ref theForceLosses);
                MenuChoice();
            }
            if (missed !=0 && attempts > 6 && attempts <10) //twice bet condition
            {
                System.Console.WriteLine("You doubled your bet!");
                currCredits = currCredits + bet;
                theForceWins++;
                theForceEnd(ref currCredits, ref bet, ref theForceWins, ref theForceLosses);
                MenuChoice();
            }
               
        }
        static void Cards (string[] deck) //assigns value to each card in deck array
        {
            string[] value = new string[13] {"Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};
            string[] suits = new string[4] {"Spades", "Clubs", "Hearts", "Diamonds"};

            int count = 0;
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < value.Length; j++)
                {
                    deck[count] = value[j]+ " of "+suits[i];
                    count++;
                }
            }
        }
        static void GetRandomCard(string[] deck, int FULL_DECK) //mixes up cards in deck array
        {
            for (int i = 0; i<deck.Length; i++)
            {
                Random rnd = new Random();
                int j = rnd.Next(FULL_DECK);
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
        static int CurrentCard(string [] deck,int i) //placeholder for whatever card is currently up
        {
            if (deck[i].Contains("Ace"))
            {
                return 1;
            }
            if (deck[i].Contains("Two"))
            {
                return 2;
            }
            if (deck[i].Contains("Three"))
            {
                return 3;
            }
            if (deck[i].Contains("Four"))
            {
                return 4;
            }
            if (deck[i].Contains("Five"))
            {
                return 5;
            }
            if (deck[i].Contains("Six"))
            {
                return 6;
            }
            if (deck[i].Contains("Seven"))
            {
                return 7;
            }
            if (deck[i].Contains("Eight"))
            {
                return 8;
            }
            if (deck[i].Contains("Nine"))
            {
                return 9;
            }
            if (deck[i].Contains("Ten"))
            {
                return 10;
            }
            if (deck[i].Contains("Jack"))
            {
                return 11;
            }
            if (deck[i].Contains("Queen"))
            {
                return 12;
            }
            if (deck[i].Contains("King"))
            {
                return 13;
            }
            else 
            {
                return 0;
            }
        }
        static int NextCard(string [] deck, int i) //placeholder for the card after current one to compare the two
        {
            if (deck[i+1].Contains("Ace"))
            {
                return 1;
            }
            if (deck[i+1].Contains("Two"))
            {
                return 2;
            }
            if (deck[i+1].Contains("Three"))
            {
                return 3;
            }
            if (deck[i+1].Contains("Four"))
            {
                return 4;
            }
            if (deck[i+1].Contains("Five"))
            {
                return 5;
            }
            if (deck[i+1].Contains("Six"))
            {
                return 6;
            }
            if (deck[i+1].Contains("Seven"))
            {
                return 7;
            }
            if (deck[i+1].Contains("Eight"))
            {
                return 8;
            }
            if (deck[i+1].Contains("Nine"))
            {
                return 9;
            }
            if (deck[i+1].Contains("Ten"))
            {
                return 10;
            }
            if (deck[i+1].Contains("Jack"))
            {
                return 11;
            }
            if (deck[i+1].Contains("Queen"))
            {
                return 12;
            }
            if (deck[i+1].Contains("King"))
            {
                return 13;
            }
            else
            {
                return 0;
            }
        }
        static void CheckGuess(string userInput, int i, string [] deck, ref int missed, ref int attempts) 
        //checks if guess is right or wrong
        {
            int current = CurrentCard(deck, i);
            int next = NextCard(deck, i);
            
            if (userInput == "1")//user guessed higher
            { 
                if (current < next)
                {
                    System.Console.WriteLine("You got it  right!");
                    attempts++;
                    i++;
                }
                if (current >= next)
                {
                    System.Console.WriteLine("That's wrong");
                    System.Console.WriteLine($"The next card was: {deck[i+1]}");
                    missed++;
                }
            }
            if (userInput == "2")//user guessed lower
            {
                if (current > next)
                {
                    System.Console.WriteLine("You got it right!");
                    attempts++; 
                    i++;
                }
                if (current <= next)
                {
                    System.Console.WriteLine("That's wrong");
                    System.Console.WriteLine($"The next card was: {deck[i+1]}");
                    missed++;
                }
            }
        }
        static void theForceEnd (ref int currCredits, ref int bet, ref int theForceWins, ref int theForceLosses) 
        //makes path for playing again without having to review instructions 
        {
            System.Console.WriteLine("Game over. Would you like to play again or return to menu?");
            System.Console.WriteLine("Chose 1 to play again or 2 to go to menu");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                TheForce(ref currCredits, ref bet, ref theForceWins, ref theForceLosses);
            }
            if (userInput == "2") //sends user back to menu
            {

            }
            else //accepts an invalid input
            {
                System.Console.WriteLine("Invalid input. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                theForceEnd(ref currCredits, ref bet, ref theForceWins, ref theForceLosses);
            }
        }
        static void BlastersInstructions (ref int currCredits, ref int bet, ref int blastersWins, ref int blastersLosses)
        //shows all instructions for Blasters before starting the game and asking for a bet
        {
            System.Console.WriteLine("In this game Yoda will shoot lasers at you with a blaster");
            System.Console.WriteLine("You will begin the game with 15 points");
            System.Console.WriteLine("Each time you're hit you will lose 5 points");
            System.Console.WriteLine("Each time you deflect you will gain 10 points");
            System.Console.WriteLine("Each time you dodge you will gain 5 points");
            System.Console.WriteLine("Deflecting has a 30% chance of success");
            System.Console.WriteLine("Dodging has a 50% chance of success");
            System.Console.WriteLine("You win the game when you reach 40 points or lose when you reach 0");
            System.Console.WriteLine("If you would like to play press 1 to continue, if not press 2");
            
            string decision = Console.ReadLine();
            if(decision == "1") //sends user to game and asks for bet
            {
                Blasters(ref currCredits, ref bet, ref blastersWins, ref blastersLosses);
            }
            else if (decision == "2") //gives user option to go back to menu before bet screen
            {
                MenuChoice();
            }
            else //path to allow invalid input
            {
                System.Console.WriteLine("invalid choice please try again");
                BlastersInstructions(ref currCredits,ref bet, ref blastersWins, ref blastersLosses);
            }     
        }
        static void Blasters (ref int currCredits, ref int bet, ref int blastersWins, ref int blastersLosses)
        //prompts user for bet and runs game 2 after
        {
            System.Console.WriteLine($"You currently have {currCredits} credits, How many credits would you like to bet?");
            bet = int.Parse(Console.ReadLine());

            if (bet > currCredits || bet < 20) //validates bet and makes path for invalid input
            {
                System.Console.WriteLine($"Invalid bet, bet must be {currCredits} credits or less and must also be minimum of 20 credits");
                System.Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                Blasters (ref currCredits, ref bet, ref blastersWins, ref blastersLosses);
            }

            Console.Clear();
            int points = 15;

            while (points > 0 && points < 40) //checks if game is over
            {
                System.Console.WriteLine("Yoda is shooting! What would you like to do?");
                System.Console.WriteLine($"You currently have {points} points");
                System.Console.WriteLine("To dodge press 1");
                System.Console.WriteLine("To deflect press 2");
                System.Console.WriteLine("To quit press 3");
                string blastersChoice = Console.ReadLine();
                DodgeOrDeflect(blastersChoice, ref points);
            }
            if (points == 0) //losing condition
            {
                System.Console.WriteLine("You lost");
                blastersLosses++;
                currCredits-= bet;
                blastersEnd(ref currCredits, ref bet, ref blastersWins, ref blastersLosses);
                MenuChoice();
            }
            if (points >= 40) //winning condition
            {
                System.Console.WriteLine("You won");
                blastersWins++;
                currCredits+= bet;
                blastersEnd(ref currCredits, ref bet, ref blastersWins, ref blastersLosses);
                MenuChoice();
            }
        }
        static void DodgeOrDeflect (string blastersChoice, ref int points) //takes dodge and deflect and sets chance of success for each
        {
            Random rnd = new Random();

            if (blastersChoice == "1")
            {
                int dodge = rnd.Next(0,2); //50% success chance
                if (dodge == 0)
                {
                    points += 5;
                    System.Console.WriteLine("You successfully dodged");
                }
                if (dodge == 1)
                {
                    points -= 5;
                    System.Console.WriteLine("Your dodge failed");
                }
            }
            if (blastersChoice == "2")
            {
                int deflect = rnd.Next(0,10); //30% success chance
                if (deflect <= 2)
                {
                    points += 10;
                    System.Console.WriteLine("You deflected successfully");
                }
                if (deflect > 2)
                {
                    points -= 5;
                    System.Console.WriteLine("Your deflect failed");
                }
            }
            if (blastersChoice == "3") //exit option
            {
                MenuChoice();
            }
        }
        static void GameOver(ref int currCredits, ref int theForceWins, ref int theForceLosses, ref int blastersWins, ref int blastersLosses)
        //shows statistics at the stop of entire program 
        {
            Console.Clear();
            System.Console.WriteLine($"You won {currCredits-50} credits");
            System.Console.WriteLine($"You won {theForceWins} games of The Force");
            System.Console.WriteLine($"You lost {theForceLosses} games of The Force");
            System.Console.WriteLine($"You won {blastersWins} games of Blasters");
            System.Console.WriteLine($"You lost {blastersLosses} games of Blasters");
            System.Console.WriteLine("May the force be with you always");
        }
        static void blastersEnd (ref int currCredits, ref int bet, ref int blastersWins, ref int blastersLosses)
        //gives option to play blasters again without seeing the instructions after a game
        {
            System.Console.WriteLine("Game over. Would you like to play again or return to menu?");
            System.Console.WriteLine("Chose 1 to play again or 2 to go to menu");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                Blasters(ref currCredits, ref bet, ref blastersWins, ref blastersLosses);
            }
            if (userInput == "2") //sends user back to menu
            {
                
            }
            else //accepts bad input
            {
                System.Console.WriteLine("Invalid input. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                blastersEnd(ref currCredits, ref bet, ref blastersWins, ref blastersLosses);
            } 
        }
        static void Scoreboard (int currCredits, ref int theForceWins, ref int theForceLosses, ref int blastersWins, ref int blastersLosses)
        //menu option to see scores
        {
            Console.Clear();
            System.Console.WriteLine($"You have {currCredits} credits");
            System.Console.WriteLine($"You've won {currCredits-50} credits");
            System.Console.WriteLine($"You've won {theForceWins} games of The Force");
            System.Console.WriteLine($"You've lost {theForceLosses} games of The Force");
            System.Console.WriteLine($"You've won {blastersWins} games of Blasters");
            System.Console.WriteLine($"You've lost {blastersLosses} games of Blasters");
            System.Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    }
}
