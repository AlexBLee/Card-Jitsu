using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    Deck deck;
    public Card cardPlayed;
    bool played;

    List<Card> fireCardList = new List<Card>();
    List<Card> waterCardList = new List<Card>();
    List<Card> snowCardList = new List<Card>();

    void Start()
    {
        deck = new Deck();
    }

    void PlayCard(Card card)
    {
        cardPlayed = card;
        deck.RemoveCard(0);
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
