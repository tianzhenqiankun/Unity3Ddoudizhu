using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : CharacterBase {

    public CharactaterUI charactaterUI;

    Identity identity;

    public Identity Identity
    {
        get
        {
            return identity;
        }

        set
        {
            identity = value;
            charactaterUI.SetIdentity(value);
        }
    }

    public override void AddCard(Card card, bool select)
    {
        characterType = CharacterType.Player;
        base.AddCard(card, select);
        charactaterUI.SetRemain(CardCount);
    }
    public override Card DealCard()
    {
        Card card= base.DealCard();
        charactaterUI.SetRemain(CardCount);
        return card;
    }

    List<Card> tempCard = null;
    List<CardUI> tempUI = null;

    public List<Card> FindSelectCard()
    {
        CardUI[] cardUIs = CreatPoint.GetComponentsInChildren<CardUI>();
        tempCard = new List<Card>();
        tempUI = new List<CardUI>();
        for (int i = 0; i < cardUIs.Length; i++)
        {
            if (cardUIs[i].IsSelect)
            {
                tempUI.Add(cardUIs[i]);
                tempCard.Add(cardUIs[i].Card);
            }
        }
        Tool.Sort(tempCard, true);
        return tempCard;
    }

    public void DestroySelectCard()
    {
        if (tempCard == null || tempUI == null)
            return;
        else
        {
            for (int i = 0; i < tempCard.Count; i++)
            {
                Cards.Remove(tempCard[i]);
                tempUI[i].Destroy();
            }
            Sort(false);
            charactaterUI.SetRemain(CardCount);
        }
    }

}
