using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
        GameManager.instance.uiManager.MoveCardToCenter(index, playerID);
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
        if (fireCardList.Count >= 1 && 
            snowCardList.Count >= 1 && 
            waterCardList.Count >= 1)
        {
            return true;
        }

        // TODO: Fully implement
        // Win through matching 3 of a colour..
        // List<Card> dis = fireCardList.GroupBy(x => x.ElementType).Select(g => g.First()).ToList();
        // if dis.Count == 3??


        return false;
    }
}
