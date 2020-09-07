using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    int value = 0;

    enum Colour { Red, Orange, Yellow, Green, Blue, Purple };
    enum Element { Fire, Water, Snow }

    Colour colourType;
    Element elementType;

    public Card()
    {
        value = GameManager.instance.rnd.Next(1, 12);

        Array values = Enum.GetValues(typeof(Colour));
        colourType = (Colour)values.GetValue(GameManager.instance.rnd.Next(values.Length));

        Array values2 = Enum.GetValues(typeof(Element));
        elementType = (Element)values2.GetValue(GameManager.instance.rnd.Next(values2.Length));
    }

    Card(int num, Colour colour, Element elem)
    {
        value = num;
        colourType = colour;
        elementType = elem;
    }

    public string GetCardStats()
    {
        string s = "";
        s += "Value: " + value + " ";
        s += "Elem: " + elementType + " ";
        s += "Colour: " + colourType + " ";

        return s;
    }
}
