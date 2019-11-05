using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    string cardName;
    Big cardBig;
    Colors cardColor;
    CharacterType character;

    public string CardName
    {
        get
        {
            return cardName;
        }
    }

    public Big CardBig
    {
        get
        {
            return cardBig;
        }
    }

    public Colors CardColor
    {
        get
        {
            return cardColor;
        }
    }

    public CharacterType Character
    {
        get
        {
            return character;
        }

        set
        {
            character = value;
        }
    }
    public Card(string cardName,Big cardBig,Colors cardColor,CharacterType character)
    {
        this.cardName = cardName;
        this.cardBig = cardBig;
        this.cardColor = cardColor;
        this.character = character;
    }
}
