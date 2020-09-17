using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int Value = 0;
    public Colour ColourType;
    public Element ElementType;

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

    public static byte[] Serialize(object obj)
    {
        Card card = (Card)obj;
        byte[] bytes = new byte[3 * 5];
        int index = 0;
        ExitGames.Client.Photon.Protocol.Serialize(card.Value, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize((int)card.ElementType, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Serialize((int)card.ColourType, bytes, ref index);
        return bytes;
    }

    public static object Deserialize(byte[] bytes)
    {
        Card card = new Card();
        int index = 0;
        int element = (int)card.ElementType;
        int colour = (int)card.ColourType;

        ExitGames.Client.Photon.Protocol.Deserialize(out card.Value, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out element, bytes, ref index);
        ExitGames.Client.Photon.Protocol.Deserialize(out colour, bytes, ref index);

        card.ElementType = (Card.Element)element;
        card.ColourType = (Card.Colour)colour;

        return card;
    }



}
