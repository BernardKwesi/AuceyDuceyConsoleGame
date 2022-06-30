using System;
using System.Text;


namespace AuceyDucey;

internal class Game{

    public void startGame(){
        displayIntro();

        do{
            PlayGame();
        }while(TryAgain());


    }
    //display intro information on the game 
    private void displayIntro(){
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("ACEY DUCEY CARD GAME");
        Console.WriteLine("CREATIVE COMPUTING MORRISTOWN, NEW  JERSEY");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Originally published in 1978 in the book 'Basic Computer Games' by David Ahl.");
        Console.WriteLine("Modernised and converted to C# in 2021 by Adam Dawes (@AdamDawes575).");
        Console.WriteLine("");

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("ACEY-DUCEY IS PLAYED IN THE FOLLOWING MANNER");
        Console.Write("THE DEALER (COMPUTER) DEALS TWO CARDS FACE UP");
        Console.WriteLine("TOU HAVE AN OPTION TO BET OR NOT BET DEPENDING");
        Console.WriteLine("ON WHETHER OR NOT YOU FEEL THE CARD WILL HAVE");
        Console.WriteLine("A VALUE BETWEEN THE FIRST TWO.");
        Console.WriteLine("IF YOU DO NOT WAT TO BET , INPUT A $0");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Press any key to start the game");
        Console.ReadKey(true);
    }

    private int getCard(){

        Random random  = new Random();
        return random.Next(2,15);
    }


    private void displayCard(int cardNumber){

        string cardText;

        switch(cardNumber){
            case 11:
                cardText = "Jack";
                break;
            case 12:
                cardText = "Queen";
                break;
            case 13:
                cardText = "King";
                break;
            case 14:
                cardText = "Ace";
                break;
            default:
                cardText = cardNumber.ToString();
                break;                
        }

        Console.Write("   ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write($"    {cardText}     ");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("");

    }

    private bool TryAgain(){

        //Ask if player would want to play again
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Would you like to play again? (Press 'Y' for Yes and 'N' for no)");
        Console.Write(">>> ");


        
        char keyPressed;
        do{
            //Accept input from user {y , n} 
            ConsoleKeyInfo answer = Console.ReadKey(true);
            keyPressed = Char.ToUpper(answer.KeyChar);

        }while(keyPressed != 'Y' && keyPressed != 'N');

        Console.WriteLine(keyPressed);

        //if Y return true ,continue to play game else return false ,end game
        return (keyPressed == 'Y');
        
        
        

        
   
    }

    private int GetTurnBetAmount(int currentAmount){
        int betAmount ;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("> $");
        takeInput:
        string? input = Console.ReadLine();

        if(!int.TryParse(input , out betAmount)){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The value inputted is not valid.  ");
            Console.WriteLine("Re-Enter Bet Amount ");
             goto takeInput;
        }
        if(betAmount < 0 || betAmount > currentAmount){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The value inputted is not valid.  ");
            Console.WriteLine("Re-Enter Bet Amount ");
             
             goto takeInput;
        }
        //
        return betAmount;
    }


    private void PlayRound(GameState state){
        Console.WriteLine("");
        Console.WriteLine("Here are your next two cards");

        //get random numbers 
        int firstCard = getCard();
        int secondCard = getCard();

        if(firstCard > secondCard){
            (firstCard  ,  secondCard) = (secondCard , firstCard);

        }
        //display the card names 
        displayCard(firstCard);
        displayCard(secondCard);

        //Ask  player if they  would like to place a bet
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("");
        Console.Write("Your current balance is :");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"GHC{state.Money}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("How much would you like to bet?");

        //read and set the amount pass the current amount to validate the bet amount
        int betAmount = GetTurnBetAmount(state.Money);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("");
        Console.WriteLine($"You chose to {(betAmount == 0 ? "pass": $"GHC bet {betAmount}")}");

        //Generate and display the final card
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("");
        Console.WriteLine("The next card is:");

        int thirdCard = getCard();
        displayCard(thirdCard);

        if(thirdCard > firstCard && thirdCard < secondCard){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You  win !!!.");
            if(betAmount == 0){
                Console.WriteLine($"Its quite unfortunate you didn't bet");
            }
            else{
                //increase the player's money if they placed a bet  
                state.Money += betAmount;
                //update MaxAmount if current Amount is > maxamount
                state.MaxMoney = Math.Max(state.Money, state.MaxMoney);        
            }
        }
        else{
             Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You Lose !!!.");
            if(betAmount == 0 ){
               
                Console.WriteLine("It's quite fortunate you didnt place a bet");
            }
            else{
                Console.ForegroundColor  = ConsoleColor.Red;
                state.Money -= betAmount;
                 state.MaxMoney = Math.Max(state.Money, state.MaxMoney);                
               


            }

        }
        //increase the number of turns 
         state.CountTurns +=1;
        Console.ForegroundColor = ConsoleColor.White;
         Console.WriteLine($"Current Balance :GHC{state.Money}");

         Console.ForegroundColor = ConsoleColor.DarkGreen;
         Console.WriteLine("");
         Console.WriteLine("Press any key to continue ....");
         Console.ReadKey(true);




    }

    private void PlayGame()
    {
        GameState state = new GameState();

        //Clear the console
        Console.Clear();

        //Keep playing until player  has no money
        do{

            PlayRound(state);
        }while(state.Money > 0);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("");
        Console.WriteLine($"Sorry Gee, you blew all your money.");
        Console.WriteLine($"Your Game is over after {state.CountTurns} {(state.CountTurns ==1 ? "turn": "turns")}");
        Console.WriteLine($"Your Highest Balance was {state.MaxMoney}");

    }

}