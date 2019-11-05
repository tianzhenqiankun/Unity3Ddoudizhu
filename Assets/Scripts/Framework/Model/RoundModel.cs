using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoundModel {

    public bool isLord = false;
    public bool isWin = false;

    public static event Action<bool> PlayerHandler;
    public static event Action<ComputerSmartArgs> ComputerHandler;

    int currentBig;
    int currentLength;
    CharacterType biggestCharacter;
    CharacterType currentCharacter;
    CardMode currentMode;

    public int CurrentBig
    {
        get
        {
            return currentBig;
        }

        set
        {
            currentBig = value;
        }
    }

    public int CurrentLength
    {
        get
        {
            return currentLength;
        }

        set
        {
            currentLength = value;
        }
    }

    public CharacterType BiggestCharacter
    {
        get
        {
            return biggestCharacter;
        }

        set
        {
            biggestCharacter = value;
        }
    }

    public CharacterType CurrentCharacter
    {
        get
        {
            return currentCharacter;
        }

        set
        {
            currentCharacter = value;
        }
    }

    public CardMode CurrentMode
    {
        get
        {
            return currentMode;
        }

        set
        {
            currentMode = value;
        }
    }

    public void InitRound()
    {
        this.CurrentMode = CardMode.None;
        this.currentBig = -1;
        this.currentLength = -1;
        this.biggestCharacter = CharacterType.Desk;
        this.currentCharacter = CharacterType.Desk;
    }

    public void Start(CharacterType character)
    {
        this.currentCharacter = character;
        this.biggestCharacter = character;
        BeginWith(character);
    }

    public void BeginWith(CharacterType character)
    {
        if (character==CharacterType.Player)
        {
            if (PlayerHandler!=null)
            {
                PlayerHandler(biggestCharacter!=CharacterType.Player);
            }
        }
        else
        {
            if (ComputerHandler!=null)
            {
                ComputerSmartArgs e = new ComputerSmartArgs()
                {
                    cardMode = currentMode,
                    big = currentBig,
                    length = currentLength,
                    isbiggest = biggestCharacter,
                    cType = currentCharacter
                };
                ComputerHandler(e);
            }
        }
    }
    
    public void Turn()
    {
        currentCharacter++;
        if (currentCharacter==CharacterType.Desk||currentCharacter==CharacterType.Library)
        {
            currentCharacter = CharacterType.Player;
        }
        BeginWith(currentCharacter);
    }
}
