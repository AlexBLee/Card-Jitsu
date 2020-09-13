using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIManager uiManager;

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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F))
        {
            player2.PlayCard(rnd.Next(5));
        }    

        if (Input.GetKeyDown(KeyCode.G))
        {
            CheckCardsPlayed();
        }   

    }

    public void CheckCardsPlayed()
    {
        if (player1.cardPlayed == null || player2.cardPlayed == null)
        {
            Debug.Log("waiting for other player..");
            return;
        }
        
        // Used for the card winning animation to determine which cards on screen will do the winning animation.
        bool playerOneWin = false;

        Debug.Log(player1.cardPlayed.GetCardStats());
        Debug.Log(player2.cardPlayed.GetCardStats());

        int playerOneElement = (int)player1.cardPlayed.ElementType;
        int playerTwoElement = (int)player2.cardPlayed.ElementType;

        if ((playerOneElement + 1) % 3 == playerTwoElement)
        {
            player1.AddWinningCard(player1.cardPlayed);
            playerOneWin = true;
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
                playerOneWin = true;
            }
            else if (player2.cardPlayed.Value > player1.cardPlayed.Value)
            {
                player2.AddWinningCard(player1.cardPlayed);
            }
        }
        StartCoroutine(uiManager.PlayCardWinningAnimation(playerOneWin));
        NextTurn();
    }

    public void NextTurn()
    {
        if (player1.GetWinningConditions())
        {
            Debug.Log("Game Over");
        }
        else if (player2.GetWinningConditions())
        {
            Debug.Log("Game Over");
        }
        else
        {
            player1.deck.AddCard();
            player2.deck.AddCard();

            player1.cardPlayed = null;
            player2.cardPlayed = null;

            Debug.Log("Turn ended");
        }
    }

}
