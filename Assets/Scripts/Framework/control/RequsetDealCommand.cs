using strange.extensions.command.impl;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;


public class RequsetDealCommand : EventCommand
{
    [Inject]
    public CardModel cardModel { get; set; }

    public DeskControl deskControl { get { return GameObject.FindObjectOfType<DeskControl>(); } }
    public override void Execute()
    {
        //发牌操作
        cardModel.Shuffle();
        DealCard();
        deskControl.StartCoroutine(DealCard());
    }

    IEnumerator DealCard()
    {
        CharacterType curr = CharacterType.Player;
        for (int i = 0; i < 51; i++)
        {
            if (curr==CharacterType.Library||curr==CharacterType.Desk)
            {
                curr = CharacterType.Player;
            }
            Deal(curr);
            Sound.Instance.PlayEffect(Contsts.DealCard);
            curr++;
            yield return new WaitForSeconds(0.05f);
            
        }
        for (int i = 0; i < 3; i++)
        {
            Deal(CharacterType.Desk);
        }
        CardUI[] cardUIs = deskControl.CreatPoint.GetComponentsInChildren<CardUI>();
        foreach (var ui in cardUIs)
            ui.SetImageAgain();
        dispatcher.Dispatch(ViewEvent.OnCompulteDeal);
    }

    private void Deal(CharacterType characterType)
    {
        Card card = cardModel.DealCard(characterType);
        DealCardArgs e = new DealCardArgs() { characterType = characterType, card = card, isSelect = false };

        dispatcher.Dispatch(ViewEvent.DealCard,e);

    }
}

