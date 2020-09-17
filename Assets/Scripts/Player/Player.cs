using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public int playerID = 0;

    public Deck deck;
    public Card cardPlayed;
    bool played;

    List<Card> fireCardList = new List<Card>();
    List<Card> waterCardList = new List<Card>();
    List<Card> snowCardList = new List<Card>();

    [SerializeField]
    List<Button> cardButtonList = new List<Button>();

    void Start()
    {
        deck = new Deck();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Card xx = (Card)PhotonNetwork.CurrentRoom.CustomProperties["p1Card"];
            Card xx2 = (Card)PhotonNetwork.CurrentRoom.CustomProperties["p2Card"];

            Debug.LogError("Player 1: " + xx.GetCardStats());
            Debug.LogError("Player 2: " + xx2.GetCardStats());

        }
    }

    public void PlayCard(int index)
    {
        cardPlayed = deck.cardList[index];

        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties["p1Card"] != null)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["p1Card"] = cardPlayed;
                PhotonNetwork.CurrentRoom.SetCustomProperties(Launcher.customProperties);
            }
            else
            {
                Launcher.customProperties.Add("p1Card", cardPlayed);
                PhotonNetwork.CurrentRoom.SetCustomProperties(Launcher.customProperties);
            }
        }
        else
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties["p2Card"] != null)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["p2Card"] = cardPlayed;
                PhotonNetwork.CurrentRoom.SetCustomProperties(Launcher.customProperties);
            }
            else
            {
                Launcher.customProperties.Add("p2Card", cardPlayed);
                PhotonNetwork.CurrentRoom.SetCustomProperties(Launcher.customProperties);
            }
        }




        GameManager.instance.uiManager.MoveCardToCenter(index, playerID);
        // GameManager.instance.uiManager.SetButtonsActive(false);

        deck.RemoveCard(index);
        GameManager.instance.uiManager.UpdateCards();
    }

    public void AddWinningCard(Card card)
    {
        switch (card.ElementType)
        {
            case Card.Element.Fire:
                fireCardList.Add(card);
                GameManager.instance.uiManager.DisplayWinningCard(card, playerID, fireCardList.Count);
                break;

            case Card.Element.Snow:
                snowCardList.Add(card);
                GameManager.instance.uiManager.DisplayWinningCard(card, playerID, snowCardList.Count);
                break;

            case Card.Element.Water:
                waterCardList.Add(card);
                GameManager.instance.uiManager.DisplayWinningCard(card, playerID, waterCardList.Count);
                break;
        }
    }

    public bool GetWinningConditions()
    {
        int fireColourCount = fireCardList.GroupBy(x => x.ColourType).Select(g => g.First()).ToList().Count;
        int snowColourCount = snowCardList.GroupBy(x => x.ColourType).Select(g => g.First()).ToList().Count;
        int waterColourCount = waterCardList.GroupBy(x => x.ColourType).Select(g => g.First()).ToList().Count;

        if (fireCardList.Count >= 1 && 
            snowCardList.Count >= 1 && 
            waterCardList.Count >= 1)
        {
            return true;
        }
        else if (fireColourCount == 3 ||
                 snowColourCount == 3 ||
                 waterColourCount == 3)
        {
            return true;
        }

        return false;
    }
}
