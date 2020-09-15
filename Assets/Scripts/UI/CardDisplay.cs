using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CardDisplay : MonoBehaviour
{
    public Card card { get; set; }

    [SerializeField]
    protected TextMeshProUGUI number;

    [SerializeField]
    protected Image iconDisplay;
    
    [SerializeField]
    private List<Sprite> icons = new List<Sprite>();

    protected Vector2 formerPosition;

    private void Awake() 
    {
        formerPosition = GetComponent<RectTransform>().position;
    }

    public void UpdateCard()
    {
        number.text = card.Value.ToString();

        Color colour = new Color(0,0,0);

        switch (card.ColourType)
        {
            case Card.Colour.Blue:
                colour = Color.blue;
                break;

            case Card.Colour.Green:
                colour = Color.green;
                break;

            case Card.Colour.Orange:
                colour = new Color(255,165,0);
                break;

            case Card.Colour.Purple:
                colour = new Color(75,0,130);
                break;

            case Card.Colour.Yellow:
                colour = Color.yellow;
                break;

            case Card.Colour.Red:
                colour = Color.red;
                break;
        }

        switch (card.ElementType)
        {
            case Card.Element.Water:
                iconDisplay.sprite = icons[0];
                break;

            case Card.Element.Fire:
                iconDisplay.sprite = icons[1];
                break;

            case Card.Element.Snow:
                iconDisplay.sprite = icons[2];
                break;
        }

        GetComponent<Image>().color = colour;
    }

    public virtual void ResetCard()
    {

    }

}
