using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {

    public CharacterType characterType;

    List<Card> cards = new List<Card>();

    public List<Card> Cards
    {
        get
        {
            return cards;
        }
    }
    public GameObject prefab;

    Transform creatPoint;

    public bool HasCard
    {
        get
        {
            return cards.Count != 0;
        }
    }
    public int CardCount { get { return cards.Count; } }

    public Transform CreatPoint
    {
        get
        {
            if (creatPoint == null)
                creatPoint = transform.Find("CreatPoint");
            return creatPoint;
        }
        
    }

    public virtual void AddCard(Card card,bool select)
    {
        cards.Add(card);
        card.Character = characterType;
        CreateCardUI(card, cards.Count - 1, select);
    }

    public void Sort(bool asc)
    {
        Tool.Sort(cards, asc);
        SortCardUI(cards);
    }

    public void SortCardUI(List<Card> cards)
    {
        CardUI[] cardUIs = CreatPoint.GetComponentsInChildren<CardUI>();
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cardUIs.Length; j++)
            {
                if (cardUIs[j].Card==cards[i])
                {
                    cardUIs[j].SetPostion(CreatPoint, i);
                   
                }
            }
        }
    }
    public void CreateCardUI(Card card,int index,bool isSelect)
    {
        GameObject go = LeanPool.Spawn(prefab);
        go.name = characterType.ToString() + index.ToString();
        CardUI cardUI = go.GetComponent<CardUI>();
        cardUI.Card = card;
        cardUI.IsSelect = isSelect;
        cardUI.SetPostion(CreatPoint, index);
    }

    public virtual Card DealCard()
    {
        Card card = cards[CardCount - 1];
        cards.Remove(card);
        return card;
    }
}
