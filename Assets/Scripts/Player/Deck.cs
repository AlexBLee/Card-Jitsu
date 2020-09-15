using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private const int DECK_LIMIT = 5;
    public List<Card> cardList = new List<Card>();

    public Deck()
    {
        for (int i = 0; i < DECK_LIMIT; i++)
        {
            cardList.Add(new Card());
        }
    }

    public void RemoveCard(int index)
    {
        cardList[index] = null;
    }

    public void AddCard()
    {
        for (int i = 0; i < DECK_LIMIT; i++)
        {
            if (cardList[i] == null)
            {
                cardList[i] = new Card();
            }
        }
    }
}
