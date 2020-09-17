using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
            GetComponent<PhotonView>().RPC("CheckCardsPlayed", RpcTarget.All);
        }
    }

    [PunRPC]
    public void CheckCardsPlayed()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player2.cardPlayed = (Card)PhotonNetwork.CurrentRoom.CustomProperties["p2Card"];
        }
        else
        {
            player2.cardPlayed = (Card)PhotonNetwork.CurrentRoom.CustomProperties["p1Card"];

        }
        
        // Used for the card winning animation to determine which cards on screen will do the winning animation.
        bool playerOneWin = false;

        int playerOneElement = (int)player1.cardPlayed.ElementType;
        int playerTwoElement = (int)player2.cardPlayed.ElementType;

        if ((playerOneElement + 1) % 3 == playerTwoElement)
        {
            player1.AddWinningCard(player1.cardPlayed);
            playerOneWin = true;
        }
        else if ((playerTwoElement + 1) % 3 == playerOneElement)
        {
            player2.AddWinningCard(player2.cardPlayed);
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
                player2.AddWinningCard(player2.cardPlayed);
            }
        }
        StartCoroutine(uiManager.PlayCardWinningAnimation(playerOneWin, player2.cardPlayed));
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

            uiManager.SetButtonsActive(true);
            
            Debug.Log("Turn ended");
        }
    }
    

}
