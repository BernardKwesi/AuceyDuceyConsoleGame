using System;


namespace AuceyDucey;

//The Gamestate class keeps track of all variables while game is implemented 
internal class GameState{

    //How much money the player has at the moment
    internal int Money { get; set; }

    // The highest money a player had throughout the game
    internal int MaxMoney { get; set; }

    //The number of turns for the player
    internal int CountTurns { get; set; }

    internal GameState(){ 
        //Player starts with an initial amount of 100 and 0 turns 
        Money = 100;
        MaxMoney = Money ;
        CountTurns = 0;

    }
}