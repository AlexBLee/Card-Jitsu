using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Player : MonoBehaviour
{
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
        deck.RemoveCard(index);

        GameManager.instance.uiManager.UpdateCards();
        GameManager.instance.CheckCardsPlayed();
    }

    public void AddWinningCard(Card card)
    {
        switch (card.ElementType)
        {
            case Card.Element.Fire:
                fireCardList.Add(card);
                break;

            case Card.Element.Snow:
                snowCardList.Add(card);
                break;

            case Card.Element.Water:
                waterCardList.Add(card);
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
