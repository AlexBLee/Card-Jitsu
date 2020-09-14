using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardImage : CardDisplay
{
    public Vector2 formerScale { get; set; }
    public GameObject stats;

    private void Awake() 
    {
        formerPosition = GetComponent<RectTransform>().position;
        formerScale = transform.localScale;
    }

    public IEnumerator FlipImage()
    {
        iTween.RotateTo(gameObject, new Vector3(0,90,0), 0.5f);

        yield return new WaitForSeconds(0.1f);
        UpdateCard();
        stats.SetActive(true);

        iTween.RotateTo(gameObject, new Vector3(0,0,0), 0.5f);
    }

    public override void ResetCard()
    {
        stats.SetActive(false);

        gameObject.SetActive(false);
        transform.position = formerPosition - new Vector2(0, 300);

        // For some reason, changing transform.localScales works very inconsistently, however using
        // iTween's ScaleTo makes the scaling work 100% of the time.
        iTween.ScaleTo(gameObject, formerScale, 0f);
        
        gameObject.SetActive(true);
        iTween.MoveTo(gameObject, formerPosition, 0.7f);

        GetComponent<Image>().color = Color.white;
    }
}
