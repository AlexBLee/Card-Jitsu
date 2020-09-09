using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public System.Random rnd = new System.Random();
    public Player player1;
    public Player player2;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CheckCardsPlayed()
    {
        int playerOneElement = (int)player1.cardPlayed.ElementType;
        int playerTwoElement = (int)player2.cardPlayed.ElementType;

        if ((playerOneElement + 1) % 3 == playerTwoElement)
        {
            player1.AddWinningCard(player1.cardPlayed);
        }
        else if ((playerTwoElement + 1) % 3 == playerOneElement)
        {
            player2.AddWinningCard(player1.cardPlayed);
        }
        // If both cards are the same element.
        else if (playerOneElement == playerTwoElement)
        {
            if (player1.cardPlayed.Value > player2.cardPlayed.Value)
            {
                player1.AddWinningCard(player1.cardPlayed);
            }
            else if (player2.cardPlayed.Value > player1.cardPlayed.Value)
            {
                player2.AddWinningCard(player1.cardPlayed);
            }
        }
    }

}
