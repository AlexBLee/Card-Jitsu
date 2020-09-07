using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Deck deck;
    Card cardPlayed;
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
}
