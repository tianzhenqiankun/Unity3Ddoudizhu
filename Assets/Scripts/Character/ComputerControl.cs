using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerControl : CharacterBase {

    public CharactaterUI charactaterUI;

    public CanvasGroup canvasGroup;

    public ComputerAI computerAI;

    public List<Card> selectCards
    {
        get { return computerAI.selectCards; }
    }

    public CardMode currentMode
    {
        get { return computerAI.currentMode; }
    }

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
        if (gameObject.name== "ComputerLeft")
        {
            characterType = CharacterType.ComputerLeft;
        }
        else
        {
            characterType = CharacterType.ComputerRight;
        }               
        base.AddCard(card, select);
        charactaterUI.SetRemain(CardCount);
    }

    public override Card DealCard()
    {
        Card card= base.DealCard();
        charactaterUI.SetRemain(CardCount);
        return card;
    }

    public void ComputerPass()
    {
        canvasGroup.alpha = 1;
        Sound.Instance.PlayEffect(Contsts.PassCard[UnityEngine.Random.Range(0, 3)]);
        StartCoroutine(Pass());
    }

    IEnumerator Pass()
    {
        yield return new WaitForSeconds(1.5f);
        canvasGroup.alpha = 0;
    }
    /// <summary>
    /// 电脑出牌
    /// </summary>
    /// <param name="cards">手牌</param>
    /// <param name="cardMode">类型</param>
    /// <param name="big">大小</param>
    /// <param name="length">长度</param>
    /// <param name="isBiggest">是否最大</param>
    public bool SmartSelectCards(CardMode cardMode, int big, int length, bool isBiggest)
    {
        computerAI.SmartSelectCards(Cards, cardMode, big, length, isBiggest);
        if (selectCards.Count!=0)
        {
            DestroyCards();
            return true;
        }
        else
        {
            ComputerPass();
            return false;
        }
    }

    private void DestroyCards()
    {
        //删手牌
        //删UI
        CardUI[] cardUIs = CreatPoint.GetComponentsInChildren<CardUI>();
        for (int i = 0; i < cardUIs.Length; i++)
        {
            for (int j = 0; j < selectCards.Count; j++)
            {
                if (selectCards[j]==cardUIs[i].Card)
                {
                    cardUIs[i].Destroy();
                    Cards.Remove(selectCards[j]);
                    
                }
            }
        }
        Sort(true);
        charactaterUI.SetRemain(CardCount);

    }
}
