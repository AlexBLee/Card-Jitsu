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

    public void ResetCards()
    {
        centerCard.ResetCard();
        centerCard2.ResetCard();
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

    public IEnumerator PlayCardWinningAnimation(bool player1Win)
    {
        StartCoroutine(centerCard2.FlipImage());

        yield return new WaitForSeconds(1.0f);

        GameObject winningCard = (player1Win == true) ? centerCard.gameObject : centerCard2.gameObject;
        GameObject losingCard = (player1Win == true) ? centerCard2.gameObject : centerCard.gameObject;

        iTween.ScaleTo(losingCard.gameObject, new Vector3(0,0,0), 0.5f);

        yield return new WaitForSeconds(0.5f);

        ResetCards();
        UpdateCards();
    }

}
