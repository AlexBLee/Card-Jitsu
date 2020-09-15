using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int Value { get; } = 0;
    public Colour ColourType { get; }
    public Element ElementType { get; }

    public enum Colour { Red, Orange, Yellow, Green, Blue, Purple };
    public enum Element { Water, Fire, Snow }

    public Card()
    {
        Value = GameManager.instance.rnd.Next(1, 12);

        Array values = Enum.GetValues(typeof(Colour));
        ColourType = (Colour)values.GetValue(GameManager.instance.rnd.Next(values.Length));

        Array values2 = Enum.GetValues(typeof(Element));
        ElementType = (Element)values2.GetValue(GameManager.instance.rnd.Next(values2.Length));
    }

    Card(int num, Colour colour, Element elem)
    {
        Value = num;
        ColourType = colour;
        ElementType = elem;
    }

    public string GetCardStats()
    {
        string s = "";
        s += "Value: " + Value + " ";
        s += "Elem: " + ElementType + " ";
        s += "Colour: " + ColourType + " ";

        return s;
    }

}
