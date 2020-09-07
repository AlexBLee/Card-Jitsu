using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private const int DECK_LIMIT = 5;
    List<Card> deck = new List<Card>();

    void Initalize()
    {
        for (int i = 0; i < DECK_LIMIT; i++)
        {
            deck.Add(new Card());
        }
    }

    void RemoveCard(int index)
    {
        deck[index] = null;
    }

    void AddCard()
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
