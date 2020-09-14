using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardButton : CardDisplay
{
    public override void ResetCard()
    {
        gameObject.SetActive(false);
        transform.position = formerPosition - new Vector2(0, 300);
        gameObject.SetActive(true);
        iTween.ScaleTo(gameObject, new Vector3(1,1,1), 0f);
        iTween.MoveTo(gameObject, formerPosition, 0.7f);
    }
}
