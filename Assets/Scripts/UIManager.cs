using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<CardButton> cardButtonList = new List<CardButton>();

    [SerializeField]
    List<CardImage> imageList = new List<CardImage>();

    [SerializeField]
    RectTransform cardCenterPosition1;

    [SerializeField]
    RectTransform cardCenterPosition2;

    CardButton centerCard;
    CardImage centerCard2;


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
                // cardButton.gameObject.SetActive(false);
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

    public void MoveCardToCenter(int index, int id)
    {
        if (id == 0)
        {
            centerCard = cardButtonList[index];
            iTween.MoveTo(cardButtonList[index].gameObject, cardCenterPosition1.position, 0.5f);
        }
        else
        {
            centerCard2 = imageList[index];
            iTween.MoveTo(imageList[index].gameObject, cardCenterPosition2.position, 0.5f);
            iTween.ScaleTo(imageList[index].gameObject, new Vector3(1,1,1), 0.5f);
        }
    }

    public void MoveCardToInitPosition()
    {
        centerCard.gameObject.SetActive(false);
        centerCard.transform.position = centerCard.formerPosition;
        centerCard.gameObject.SetActive(true);

        centerCard2.gameObject.SetActive(false);
        centerCard2.transform.position = centerCard2.formerPosition;
        centerCard2.gameObject.SetActive(true);
    }

}
