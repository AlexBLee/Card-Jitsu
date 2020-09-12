using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardImage : MonoBehaviour
{
    public Vector2 formerPosition { get; set; }
    public Vector2 formerScale { get; set; }

    private void Awake() {
        formerPosition = GetComponent<RectTransform>().position;
        formerScale = transform.localScale;
    }
}
