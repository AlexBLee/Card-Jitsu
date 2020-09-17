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

    public void PlayCard(int index)
    {
        cardPlayed = deck.cardList[index];

        if (PhotonNetwork.IsMasterClient)
        {
            AddCardToRoomProperties("p1Card");
        }
        else
        {
            AddCardToRoomProperties("p2Card");
        }

        GameManager.instance.uiManager.MoveCardToCenter(index, playerID);
        GameManager.instance.uiManager.GetComponent<PhotonView>().RPC("MoveCardToCenter", RpcTarget.Others, index, 1);

        GameManager.instance.uiManager.SetButtonsActive(false);

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

    public void AddCardToRoomProperties(string key)
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties[key] != null)
        {
            Launcher.customProperties.Remove(key);
            Launcher.customProperties.Add(key, cardPlayed);
            PhotonNetwork.CurrentRoom.SetCustomProperties(Launcher.customProperties);
        }
        else
        {
            Launcher.customProperties.Add(key, cardPlayed);
            PhotonNetwork.CurrentRoom.SetCustomProperties(Launcher.customProperties);
        }
    }
}
