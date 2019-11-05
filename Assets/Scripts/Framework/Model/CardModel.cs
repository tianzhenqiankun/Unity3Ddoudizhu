using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel {

    CharacterType characterType = CharacterType.Library;
    Queue<Card> cardLibrary = new Queue<Card>();

    public int CardCount
    {
        get
        {
            return cardLibrary.Count;
        }
     }
    public void InitCardLibrary()
    {
        for (int color = 1; color < 5; color++)
        {
            for (int big = 0; big < 13; big++)
            {
                Colors c = (Colors)color;
                Big b = (Big)big;
                string name = c.ToString() + b.ToString();
                Card card = new Card(name, b, c, characterType);
                cardLibrary.Enqueue(card);
            }
        }

        Card sJoker = new Card("SJoker",  Big.SJoker, Colors.Joker, characterType);
        Card lJoker = new Card("LJoker", Big.LJoker, Colors.Joker, characterType);
        cardLibrary.Enqueue(sJoker);
        cardLibrary.Enqueue(lJoker);
    }

    public void Shuffle()
    {
        List<Card> cardList = new List<Card>();
        foreach (var card in cardLibrary)
        {
            int index = Random.Range(0, cardList.Count + 1);
            cardList.Insert(index, card);
        }
        cardLibrary.Clear();
        foreach (var card in cardList)
        {
            cardLibrary.Enqueue(card);
        }

        cardList.Clear();
    }

    public Card DealCard(CharacterType characterType)
    {
        Card card = cardLibrary.Dequeue();
        card.Character = characterType;
        return card;
    }
}
