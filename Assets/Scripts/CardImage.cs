using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardImage : MonoBehaviour
{
    public Vector2 formerPosition { get; set; }

    private void Awake() {
        formerPosition = GetComponent<RectTransform>().position;
    }
}
