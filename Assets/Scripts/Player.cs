using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
