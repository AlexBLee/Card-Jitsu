using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<CardButton> cardButtonList = new List<CardButton>();

    private void Start() 
    {
        UpdateCards();
    }

    public void UpdateCards()
    {
        Deck deck = GameManager.instance.player1.deck;

        int counter = 0;
        foreach (CardButton cardButton in cardButtonList)
        {
            Card card = deck.cardList[counter];
            Color color = new Color(0,0,0);

            if (card == null)
            {
                cardButton.gameObject.SetActive(false);
                return;
            }
            else
            {
                cardButton.gameObject.SetActive(true);
            }
            
            switch (card.ColourType)
            {
                case Card.Colour.Blue:
                    color = Color.blue;
                    break;

                case Card.Colour.Green:
                    color = Color.green;
                    break;

                case Card.Colour.Orange:
                    color = new Color(255,165,0);
                    break;

                case Card.Colour.Purple:
                    color = new Color(75,0,130);
                    break;

                case Card.Colour.Yellow:
                    color = Color.yellow;
                    break;

                case Card.Colour.Red:
                    color = Color.red;
                    break;
            }

            cardButton.UpdateCard(card.Value, card.ElementType, color);
            counter++;
        }
    }
}
