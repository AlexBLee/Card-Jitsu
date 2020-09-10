﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButton : MonoBehaviour
{
    public TextMeshProUGUI number;
    public TextMeshProUGUI elemType;
    
    public void UpdateCard(int numb, Card.Element element, Color color)
    {
        number.text = numb.ToString();
        elemType.text = element.ToString();
        GetComponent<Image>().color = color;
    }
}
