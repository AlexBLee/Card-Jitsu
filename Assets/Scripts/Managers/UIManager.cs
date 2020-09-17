using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] List<CardButton> cardButtonList = new List<CardButton>();
    [SerializeField] List<CardImage> imageList = new List<CardImage>();

    [SerializeField] RectTransform cardCenterPosition1;
    [SerializeField] RectTransform cardCenterPosition2;

    [SerializeField] List<RectTransform> winningCardXCoorList = new List<RectTransform>();

    CardButton centerCard;
    CardImage centerCard2;

    [SerializeField] CardImage winningCardDisplay;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI winText;
    public PhotonView photonView;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        photonView = GetComponent<PhotonView>();

    }

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

            if (card == null)
            {
                return;
            }
            else
            {
                cardButton.card = card;
                cardButton.gameObject.SetActive(true);
            }

            cardButton.UpdateCard();
            counter++;
        }
    }

    public void SetButtonsActive(bool state)
    {
        foreach (CardButton cardButton in cardButtonList)
        {
            cardButton.button.interactable = state;
        }
    }

    [PunRPC]
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

    public IEnumerator PlayCardWinningAnimation(bool player1Win, Card player2Card)
    {
        centerCard2.card = player2Card;
        
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(centerCard2.FlipImage());

        yield return new WaitForSeconds(1.0f);

        GameObject winningCard = (player1Win == true) ? centerCard.gameObject : centerCard2.gameObject;
        GameObject losingCard = (player1Win == true) ? centerCard2.gameObject : centerCard.gameObject;

        iTween.ScaleTo(losingCard.gameObject, new Vector3(0,0,0), 0.5f);

        yield return new WaitForSeconds(0.5f);

        ResetCards();
        UpdateCards();
    }

    public void DisplayWinningCard(Card card, int playerID, int length)
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        int xOffset = playerID == 0 ? 0 : 3; // Determine which side the cards should show up on.
        float yOffset = -20.0f;
        int pos = (int)card.ElementType;

        winningCardDisplay.card = card;
        winningCardDisplay.UpdateCard();
        winningCardDisplay.stats.SetActive(true);

        Instantiate(winningCardDisplay, 
                    new Vector3(winningCardXCoorList[pos + xOffset].position.x, 
                                winningCardXCoorList[pos + xOffset].position.y + (yOffset * length)),
                    Quaternion.identity, canvas);

    }

    public void DisplayGameOverMessage(string winResult)
    {
        gameOverPanel.SetActive(true);
        winText.text = winResult;
    }
}
