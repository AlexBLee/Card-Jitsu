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

    System.Random rnd = new System.Random();

    public Card()
    {
        value = rnd.Next(1, 12);

        Array values = Enum.GetValues(typeof(Colour));
        colourType = (Colour)values.GetValue(rnd.Next(values.Length));

        Array values2 = Enum.GetValues(typeof(Element));
        elementType = (Element)values2.GetValue(rnd.Next(values2.Length));
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
