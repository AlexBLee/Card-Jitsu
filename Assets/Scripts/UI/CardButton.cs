using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CardButton : CardDisplay
{
    public Button button;

    private void Start() {
        button = GetComponent<Button>();
    }

    public override void ResetCard()
    {
        gameObject.SetActive(false);
        transform.position = formerPosition - new Vector2(0, 300);
        gameObject.SetActive(true);
        iTween.ScaleTo(gameObject, new Vector3(1,1,1), 0f);
        iTween.MoveTo(gameObject, formerPosition, 0.7f);
    }
}
