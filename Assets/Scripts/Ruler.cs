using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ruler {

	public static bool CanPop(List<Card> cards,out CardMode type)
    {
        type = CardMode.None;
        bool can = false;
        switch (cards.Count)
        {
            case 1:
                if (IsSingle(cards))
                {
                    can = true;
                    type = CardMode.Single;
                }
                break;
            case 2:
                if (IsDouble(cards))
                {
                    can = true;
                    type = CardMode.Double;
                }else if (IsJokerBoom(cards))
                {
                    can = true;
                    type = CardMode.JokerBoom;
                }
                break;
            case 3:
                if (IsThree(cards))
                {
                    can = true;
                    type = CardMode.Three;
                }
                break;
            case 4:
                if (IsBoom(cards))
                {
                    can = true;
                    type = CardMode.Boom;
                }else if (IsThreeAndOne(cards))
                {
                    can = true;
                    type = CardMode.ThreeAndOne;
                }
                break;
            case 5:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardMode.Straight;
                }else if (IsThreeAndTwo(cards))
                {
                    can = true;
                    type = CardMode.ThreeAndTwo;
                }
                break;
            case 6:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardMode.Straight;
                }else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardMode.DoubleStraight;
                }else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardMode.TripleStraight;
                }
                break;
            case 7:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardMode.Straight;
                }
                break;
            case 8:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardMode.Straight;
                }else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardMode.DoubleStraight;
                }
                break;
            case 9:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardMode.Straight;
                }else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardMode.TripleStraight;
                }
                break;
            case 10:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardMode.Straight;
                }else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardMode.DoubleStraight;
                }
                break;
            case 11:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardMode.Straight;
                }
                break;
            case 12:
                if (IsStraight(cards))
                {
                    can = true;
                    type = CardMode.Straight;
                }
                else if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardMode.DoubleStraight;
                }
                else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardMode.TripleStraight;
                }
                break;
            case 13:

                break;
            case 14:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardMode.DoubleStraight;
                }
                break;
            case 15:
                if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardMode.TripleStraight;
                }
                break;
            case 16:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardMode.DoubleStraight;
                }
                break;
            case 17:
                break;
            case 18:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardMode.DoubleStraight;
                }else if (IsTripleStraight(cards))
                {
                    can = true;
                    type = CardMode.TripleStraight;
                }
                break;
            case 19:
                break;
            case 20:
                if (IsDoubleStraight(cards))
                {
                    can = true;
                    type = CardMode.DoubleStraight;
                }
                break;
            default:
                break;
        }
        return can;
    }

    public static bool IsSingle(List<Card> cards)
    {
        return cards.Count == 1;
    }
    public static bool IsDouble(List<Card> cards)
    {
        if (cards.Count==2)
        {
            if (cards[0].CardBig==cards[1].CardBig)
            {
                return true;
            }
        }
        return false;
    }
    public static bool IsStraight(List<Card> cards)
    {
        if (cards.Count<5||cards.Count>12)
        {
            return false;
        }
        for (int i = 0; i < cards.Count-1; i++)
        {
            if (cards[i+1].CardBig-cards[i].CardBig!=1)
            {
                return false;
            }
            if (cards[i].CardBig>Big.One||cards[i+1].CardBig>Big.One)
            {
                return false;
            }
        }
        return true;
    }
    public static bool IsDoubleStraight(List<Card> cards)
    {
        if (cards.Count<6||cards.Count%2!=0)
        {
            return false;
        }
        for (int i = 0; i < cards.Count-2; i+=2)
        {
            if (cards[i + 1].CardBig != cards[i].CardBig)
                return false;
            if (cards[i + 2].CardBig - cards[i].CardBig != 1)
                return false;
            if (cards[i].CardBig>Big.One||cards[i+2].CardBig>Big.One)
                return false;
        }
        return true;
    }
    public static bool IsTripleStraight(List<Card> cards)
    {
        if (cards.Count < 6 || cards.Count % 3 != 0)
        {
            return false;
        }
        for (int i = 0; i < cards.Count - 3; i += 3)
        {
            if (cards[i + 1].CardBig != cards[i].CardBig)
                return false;
            if (cards[i + 2].CardBig !=cards[i].CardBig)
                return false;
            if(cards[i + 1].CardBig != cards[i+2].CardBig)
                return false;
            if (cards[i + 3].CardBig - cards[i].CardBig != 1)
                return false;
            if (cards[i].CardBig > Big.One || cards[i + 3].CardBig > Big.One)
                return false;
        }
        return true;
    }
    public static bool IsThree(List<Card> cards)
    {
        if (cards.Count != 3)
            return false;
        if (cards[1].CardBig != cards[0].CardBig)
            return false;
        if (cards[2].CardBig != cards[0].CardBig)
            return false;
        if (cards[1].CardBig != cards[2].CardBig)
            return false;
        return true;

    }
    public static bool IsThreeAndOne(List<Card> cards)
    {
        if (cards.Count != 4)
            return false;
        if (cards[1].CardBig == cards[0].CardBig&& cards[1].CardBig == cards[2].CardBig)
            return true;
        else if(cards[1].CardBig==cards[2].CardBig&&cards[1].CardBig==cards[3].CardBig)
            return true;
        return false;

    }
    public static bool IsThreeAndTwo(List<Card> cards)
    {
        if (cards.Count != 5)
            return false;
        if (cards[1].CardBig == cards[0].CardBig && cards[1].CardBig == cards[2].CardBig)
        {
            if (cards[3].CardBig==cards[4].CardBig)           
            return true;
        }
        else if (cards[3].CardBig == cards[2].CardBig && cards[4].CardBig == cards[3].CardBig)
        {
            if (cards[0].CardBig==cards[1].CardBig)
            return true;
        }           
        return false;
    }
    public static bool IsBoom(List<Card> cards)
    {
        if (cards.Count != 4)
            return false;
        if (cards[1].CardBig != cards[0].CardBig)
            return false;
        if (cards[2].CardBig != cards[1].CardBig)
            return false;
        if (cards[3].CardBig != cards[2].CardBig)
            return false;
        return true;
    }
    public static bool IsJokerBoom(List<Card> cards)
    {
        if (cards.Count != 2)
            return false;
        if (cards[0].CardBig==Big.SJoker&&cards[1].CardBig==Big.LJoker)
            return true;
        return false;

    }
 
}
