using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour {

    public List<Card> selectCards = new List<Card>();

    public CardMode currentMode = CardMode.None;

    public void SmartSelectCards(List<Card> cards, CardMode cardMode,int big, int length,bool isBiggest)
    {
        cardMode = isBiggest ? CardMode.None : cardMode;
        currentMode = cardMode;
        selectCards.Clear();
        switch (cardMode)
        {
            case CardMode.None:
                selectCards = FindSmallestCards(cards);
                break;
            case CardMode.Single:
                selectCards = FindSingle(cards,big);
                break;
            case CardMode.Double:
                selectCards = FindDouble(cards, big);
                break;
            case CardMode.Straight:
                selectCards = FindStraight(cards, big,length);
                if (selectCards.Count==0)
                {
                    selectCards = FindBoom(cards, -1);
                    currentMode = CardMode.Boom;
                    if (selectCards.Count==0)
                    {
                        selectCards = FindJokerBoom(cards);
                    }
                }
                break;
            case CardMode.DoubleStraight:
                selectCards = FindDoubleStraight(cards, big,length);
                if (selectCards.Count == 0)
                {
                    selectCards = FindBoom(cards, -1);
                    currentMode = CardMode.Boom;
                    if (selectCards.Count == 0)
                    {
                        selectCards = FindJokerBoom(cards);
                    }
                }
                break;
            case CardMode.TripleStraight:
                selectCards = FindBoom(cards, -1);
                currentMode = CardMode.Boom;
                if (selectCards.Count == 0)
                {
                    selectCards = FindJokerBoom(cards);
                }
                break;
            case CardMode.Three:
                selectCards = FindThree(cards, big);
                break;
            case CardMode.ThreeAndOne:
                selectCards = FindThreeAndOne(cards, big);
                break;
            case CardMode.ThreeAndTwo:
                selectCards = FindThreeAndTwo(cards, big);
                break;
            case CardMode.Boom:
                selectCards = FindBoom(cards, -1);
                currentMode = CardMode.Boom;
                if (selectCards.Count == 0)
                {
                    selectCards = FindJokerBoom(cards);
                }
                break;
            case CardMode.JokerBoom:
                break;
            default:
                break;
        }
    }

    private List<Card> FindSmallestCards(List<Card> cards)
    {
        List<Card> select = new List<Card>();


        if (select.Count==0)
        {
            for (int i = 0; i < 36; i+=3)
            {
                select = FindThreeAndTwo(cards, i - 1);
                if (select.Count!=0)
                {
                    currentMode = CardMode.ThreeAndTwo;
                    break;
                }
            }
        }
        if (select.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThreeAndOne(cards, i - 1);
                if (select.Count != 0)
                {
                    currentMode = CardMode.ThreeAndOne;
                    break;
                }
            }
        }
        if (select.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThree(cards, i - 1);
                if (select.Count != 0)
                {
                    currentMode = CardMode.Three;
                    break;
                }
            }
        }
        if (select.Count == 0)
        {
            for (int i = 20; i >=6; i -= 2)
            {
                select = FindDoubleStraight(cards, -1,i);
                if (select.Count != 0)
                {
                    currentMode = CardMode.DoubleStraight;
                    break;
                }
            }
        }
        if (select.Count == 0)
        {
            for (int i = 0; i < 24; i += 2)
            {
                select = FindDouble(cards, i - 1);
                if (select.Count != 0)
                {
                    currentMode = CardMode.Double;
                    break;
                }
            }
        }
        if (select.Count==0)
        {
            for (int i = 12; i >= 5; i--)
            {
                select = FindStraight(cards, -1, i);
                if (select.Count != 0)
                {
                    currentMode = CardMode.Straight;
                    break;
                }
            }
        }
        if (select.Count == 0)
        {
            select = FindSingle(cards, -1);
            currentMode = CardMode.Single;
        }
        return select;
    }

    public List<Card> FindSingle(List<Card> cards,int big)
    {
        List<Card> select = new List<Card>();

        for (int i = 0; i < cards.Count; i++)
        {
            if ((int)cards[i].CardBig>big)
            {
                select.Add(cards[i]);
                break;
            }
        }
        return select;
    }

    public List<Card> FindDouble(List<Card> cards, int big)
    {
        List<Card> select = new List<Card>();

        for (int i = 0; i < cards.Count-1; i++)
        {
            if ((int)cards[i].CardBig ==(int)cards[i+1].CardBig)
            {
                int totalbig = (int)cards[i].CardBig + (int)cards[i + 1].CardBig;
                if (totalbig>big)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    break;
                }               
            }
        }
        return select;
    }

    public List<Card> FindStraight(List<Card> cards, int minbig,int length)
    {
        List<Card> select = new List<Card>();
        int counter = 1;//card的索引
        List<int> indexList = new List<int>();
        for (int i = 0; i < cards.Count-4; i++)
        {
            int weight = (int)cards[i].CardBig;

            if (weight>minbig)
            {
                counter = 1;
                indexList.Clear();
                for (int j = i+1; j < cards.Count; j++)
                {
                    if (cards[j].CardBig > Big.One)
                        break;
                    if ((int)cards[j].CardBig-weight==counter)
                    {
                        counter++;
                        indexList.Add(j);
                    }
                    if (counter == length)
                        break;
                }
            }
            if (counter==length)
            {
                indexList.Insert(0, i);
                break;
            }
        }
        if (counter==length)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                select.Add(cards[indexList[i]]);
            }
        }
        return select;
    }

    public List<Card> FindDoubleStraight(List<Card> cards, int minbig, int length)
    {
        List<Card> select = new List<Card>();
        int counter = 0;//card的索引
        List<int> indexList = new List<int>();
        for (int i = 0; i < cards.Count - 4; i++)
        {
            int weight = (int)cards[i].CardBig;

            if (weight > minbig)
            {
                counter = 0;
                indexList.Clear();
                int temp = 0;//游标 控制counter++
                for (int j = i + 1; j < cards.Count; j++)
                {
                    if (cards[j].CardBig > Big.One)
                        break;
                    if ((int)cards[j].CardBig - weight == counter)
                    {
                        if (temp%2==1)
                        {
                            counter++;
                        }                        
                        indexList.Add(j);
                    }
                    if (counter == length/2)
                        break;
                }
            }
            if (counter == length/2)
            {
                indexList.Insert(0, i);
                break;
            }
        }
        if (counter == length/2)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                select.Add(cards[indexList[i]]);
            }
        }
        return select;
    }

    public List<Card> FindThree(List<Card> cards,int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count-3; i++)
        {
            if ((int)cards[i].CardBig==(int)cards[i+1].CardBig&&(int)cards[i].CardBig==(int)cards[i+2].CardBig)
            {
                int totalBig = (int)cards[i].CardBig + (int)cards[i + 1].CardBig+ (int)cards[i + 2].CardBig;
                if (totalBig>weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    select.Add(cards[i + 2]);
                    break;
                }
            }
        }
        return select;
    }

    public List<Card> FindThreeAndTwo(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        List<Card> three = FindThree(cards, weight);

        if (three.Count>0)
        {

            foreach (var card in three)
                cards.Remove(card);
            List<Card> two = FindDouble(cards, -1);
            foreach (var card in three)
                cards.Add(card);
            Tool.Sort(cards, true);
            if (two.Count!=0)
            {
                select.AddRange(three);
                select.AddRange(two);
            }
        }
        return select;
    }

    public List<Card> FindThreeAndOne(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        List<Card> three = FindThree(cards, weight);

        if (three.Count > 0)
        {

            foreach (var card in three)
                cards.Remove(card);
            List<Card> one = FindSingle(cards, -1);
            foreach (var card in three)
                cards.Add(card);
            Tool.Sort(cards, true);
            if (one.Count != 0)
            {

                select.AddRange(three);
                select.AddRange(one);
            }
        }
        return select;
    }

    public List<Card> FindBoom(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count - 4; i++)
        {
            if ((int)cards[i].CardBig == (int)cards[i + 1].CardBig &&
                (int)cards[i].CardBig == (int)cards[i + 2].CardBig&&
                (int)cards[i].CardBig == (int)cards[i + 3].CardBig)
            {
                int totalBig = (int)cards[i].CardBig + (int)cards[i + 1].CardBig 
                    + (int)cards[i + 2].CardBig + (int)cards[i + 3].CardBig;
                if (totalBig > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    select.Add(cards[i + 2]);
                    select.Add(cards[i + 3]);
                    break;
                }
            }
        }
        return select;
    }
    public List<Card> FindJokerBoom(List<Card> cards)
    {
        List<Card> select = new List<Card>();

        for (int i = 0; i < cards.Count-1; i++)
        {
            if (cards[i].CardBig ==Big.SJoker&&
                cards[i + 1].CardBig==Big.LJoker)
            {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    break;
            }
        }
        return select;
    }
}
