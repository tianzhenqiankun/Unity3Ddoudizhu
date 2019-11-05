using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskControl : CharacterBase {

    List<Card> playerCards = new List<Card>();
    public List<Card> PlayerCards { get { return playerCards; } }

    List<Card> computerLeftCards = new List<Card>();
    public List<Card> ComputerLeftCards { get { return computerLeftCards; } }

    List<Card> computerRightCards = new List<Card>();
    public List<Card> ComputerRightCards { get { return computerRightCards; } }

    Transform playerPoint;
    public DeskUI deskUI;
    public Transform PlayerPoint
    {
        get
        {
            if (playerPoint == null)
                playerPoint = transform.Find("PlayerPoint");
            return playerPoint;
        }        
    }

    Transform computerLeftPoint;
    public Transform ComputerLeftPoint
    {
        get
        {
            if (computerLeftPoint == null)
                computerLeftPoint = transform.Find("ComputerLeftPoint");
            return computerLeftPoint;
        }
    }

    Transform computerRightPoint;
    public Transform ComputerRightPoint
    {
        get
        {
            if (computerRightPoint == null)
                computerRightPoint = transform.Find("ComputerRightPoint");
            return computerRightPoint;
        }
    }
    public void SetShowCard(Card card,int index)
    {
        deskUI.SetShowCard(card,index);
    }

    public void CreateCardUI(Card card,int index,bool isSelect,ShowPoint pos)
    {
        GameObject go = Lean.Pool.LeanPool.Spawn(prefab);
        go.name = card.Character.ToString() + index.ToString();

        CardUI cardUI = go.GetComponent<CardUI>();
        cardUI.Card = card;
        cardUI.IsSelect = isSelect;
        switch (pos)
        {
            case ShowPoint.Desk:
                cardUI.SetPostion(CreatPoint, index);
                break;
            case ShowPoint.Player:
                cardUI.SetPostion(PlayerPoint,index);
                break;
            case ShowPoint.ComputerLeft:
                cardUI.SetPostion(ComputerLeftPoint, index);
                break;
            case ShowPoint.ComputerRight:
                cardUI.SetPostion(ComputerRightPoint, index);
                break;
            default:
                break;
        }
    }
    public  void AddCard(Card card, bool select,ShowPoint pos)
    {
        card.Character = CharacterType.Desk;
        switch (pos)
        {
            case ShowPoint.Desk:
                Cards.Add(card);
                //card.Character = CharacterType.Desk;
                CreateCardUI(card, Cards.Count - 1, select,pos);
                break;
            case ShowPoint.Player:
                PlayerCards.Add(card);
                //card.Character = CharacterType.Player;
                CreateCardUI(card, PlayerCards.Count - 1, select, pos);
                break;
            case ShowPoint.ComputerLeft:
                ComputerLeftCards.Add(card);
                
                //card.Character = CharacterType.ComputerLeft;
                CreateCardUI(card, ComputerLeftCards.Count - 1, select, pos);
                break;
            case ShowPoint.ComputerRight:
                ComputerRightCards.Add(card);
                //card.Character = CharacterType.ComputerRight;
                CreateCardUI(card, ComputerRightCards.Count - 1, select, pos);
                break;
            default:
                break;
        }        
    }

    public void Clear(ShowPoint pos)
    {
        switch (pos)
        {
            case ShowPoint.Desk:
                Cards.Clear();
                CardUI[] cardUIs = CreatPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < cardUIs.Length; i++)
                    cardUIs[i].Destroy();
                break;
            case ShowPoint.Player:
                PlayerCards.Clear();
                CardUI[] playercardUIs = PlayerPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < playercardUIs.Length; i++)
                    playercardUIs[i].Destroy();
                break;
            case ShowPoint.ComputerLeft:
                ComputerLeftCards.Clear();
                CardUI[] computerLeftcardUIs = ComputerLeftPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < computerLeftcardUIs.Length; i++)
                    computerLeftcardUIs[i].Destroy();
                break;
            case ShowPoint.ComputerRight:
                ComputerRightCards.Clear();
                CardUI[] computerRightcardUIs = ComputerRightPoint.GetComponentsInChildren<CardUI>();
                for (int i = 0; i < computerRightcardUIs.Length; i++)
                    computerRightcardUIs[i].Destroy();
                break;
            default:
                break;
        }
    }
}
