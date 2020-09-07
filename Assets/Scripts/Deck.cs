using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private const int DECK_LIMIT = 5;
    public List<Card> deck = new List<Card>();

    public Deck()
    {
        for (int i = 0; i < DECK_LIMIT; i++)
        {
            deck.Add(new Card());
        }
    }

    public void RemoveCard(int index)
    {
        deck[index] = null;
    }

    public void AddCard()
    {
        for (int i = 0; i < DECK_LIMIT; i++)
        {
            if (deck[i] == null)
            {
                deck[i] = new Card();
            }
        }
    }
}
