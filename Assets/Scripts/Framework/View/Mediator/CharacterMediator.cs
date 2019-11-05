using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMediator : EventMediator {

    [Inject]
    public CharacterView characterView { get; set; }
    [Inject]
    public Intergration intergration { get; set; }
    public override void OnRegister()
    {
        characterView.Init();
        
        dispatcher.AddListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.AddListener(ViewEvent.OnCompulteDeal, OnComplete);
        dispatcher.AddListener(ViewEvent.DealLordCard, OnDeaLordCard);
        dispatcher.AddListener(ViewEvent.RequestPlay, OnPlayerPlay);
        dispatcher.AddListener(ViewEvent.SuccessPlay, OnSuccessPlay);
        dispatcher.AddListener(ViewEvent.UpdateIntergration, OnUpateIntergration);
        dispatcher.AddListener(ViewEvent.RestartGame, OnRestartGame);

        dispatcher.Dispatch(CommandEvent.RequsetUpdate);

        RoundModel.ComputerHandler += RoundModel_ComputerHandler;
        RoundModel.PlayerHandler += RoundModel_PlayerHandler;
    }



    public override void OnRemove()
    {
        dispatcher.RemoveListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.RemoveListener(ViewEvent.OnCompulteDeal, OnComplete);
        dispatcher.RemoveListener(ViewEvent.DealLordCard, OnDeaLordCard);
        dispatcher.RemoveListener(ViewEvent.RequestPlay, OnPlayerPlay);
        dispatcher.RemoveListener(ViewEvent.SuccessPlay, OnSuccessPlay);
        dispatcher.RemoveListener(ViewEvent.UpdateIntergration, OnUpateIntergration);
        dispatcher.RemoveListener(ViewEvent.RestartGame, OnRestartGame);

        RoundModel.ComputerHandler -= RoundModel_ComputerHandler;
        RoundModel.PlayerHandler -= RoundModel_PlayerHandler;
    }



    private void OnDealCard(IEvent evt)
    {
        DealCardArgs e = (DealCardArgs)evt.data;
        characterView.AddCard(e.characterType, e.card, e.isSelect, ShowPoint.Desk);
    }
    /// <summary>
    /// 发牌结束
    /// </summary>
    private void OnComplete()
    {
        characterView.playerControll.Sort(false);
        characterView.computerLeftControl.Sort(true);
        characterView.computerRightControl.Sort(true);
        characterView.deskControl.Sort(true);
    }

    /// <summary>
    /// 发地主牌
    /// </summary>
    private void OnDeaLordCard(IEvent evt)
    {
        GrabLordCardArgs e = (GrabLordCardArgs)evt.data;
        characterView.DealLordCard(e.cType);
    }
    private void OnPlayerPlay()
    {
        List<Card> cards = characterView.playerControll.FindSelectCard();
        CardMode cardMode;
        if (Ruler.CanPop(cards,out cardMode))
        {
            PlayCardArgs e = new PlayCardArgs()
            {
                cardMode = cardMode,
                characterType = CharacterType.Player,
                length = cards.Count,
                big = Tool.GetBig(cards, cardMode)
            };
            dispatcher.Dispatch(CommandEvent.PlayCard, e);
        }
        else
        {
            Tips tips = new Tips()
            {
                tips = "你选择的牌不符合规则"
            };
            dispatcher.Dispatch(ViewEvent.ShowTips, tips);
            return;
        }        
    }

    private void OnSuccessPlay()
    {
        List<Card> cards = characterView.playerControll.FindSelectCard();
        characterView.deskControl.Clear(ShowPoint.Player);
        foreach (var card in cards)
            characterView.deskControl.AddCard(card, false,ShowPoint.Player);
        characterView.playerControll.DestroySelectCard();
        if (characterView.playerControll.CardCount==2)
        {
            Sound.Instance.PlayEffect(Contsts.Baojing2);
        }
        if (characterView.playerControll.CardCount == 1)
        {
            Sound.Instance.PlayEffect(Contsts.Baojing1);
        }
        if (characterView.playerControll.CardCount == 0)
        {
            //游戏结束
            Identity left = characterView.computerLeftControl.Identity;
            Identity right = characterView.computerRightControl.Identity;
            Identity player = characterView.playerControll.Identity;
            GameOverArgs overArgs = new GameOverArgs()
            {
                PlayerWin = true,
                ComputerLeftWin = left == player ? true : false,
                ComputerRightWin = right == player ? true : false,
                isLord = player == Identity.Landlord
            };
            dispatcher.Dispatch(CommandEvent.GameOver, overArgs);
        }
    }

    private void RoundModel_ComputerHandler(ComputerSmartArgs e)
    {
        StartCoroutine(Delay(e));
    }

    private IEnumerator Delay(ComputerSmartArgs e)
    {
        yield return new WaitForSeconds(1f);
        bool can = false;
        switch (e.cType)
        {
            case CharacterType.ComputerLeft:

                characterView.deskControl.Clear(ShowPoint.ComputerLeft);


                can= characterView.computerLeftControl.SmartSelectCards(e.cardMode,e.big,e.length,
                    e.isbiggest==CharacterType.ComputerLeft);
                if (can)
                {
                    List<Card> cards = characterView.computerLeftControl.selectCards;
                    CardMode currMode = characterView.computerLeftControl.currentMode;
                    foreach (var card in cards)
                        characterView.deskControl.AddCard(card, false, ShowPoint.ComputerLeft);
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                        cardMode = currMode,
                        length = cards.Count,
                        characterType = CharacterType.ComputerLeft,
                        big = Tool.GetBig(cards, currMode),
                        
                    };
                    if (characterView.computerLeftControl.CardCount == 2)
                    {
                        Sound.Instance.PlayEffect(Contsts.Baojing2);
                    }
                    if (characterView.computerLeftControl.CardCount == 1)
                    {
                        Sound.Instance.PlayEffect(Contsts.Baojing1);
                    }
                    if (characterView.computerLeftControl.CardCount==0)
                    {
                        Identity left = characterView.computerLeftControl.Identity;
                        Identity right = characterView.computerRightControl.Identity;
                        Identity player = characterView.playerControll.Identity;
                        GameOverArgs overArgs = new GameOverArgs()
                        {
                            ComputerLeftWin = true,
                            ComputerRightWin = right == left ? true : false,
                            PlayerWin = player == left ? true : false
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver, overArgs);
                    }
                    else
                    {
                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);
                    }
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            case CharacterType.ComputerRight:
                characterView.deskControl.Clear(ShowPoint.ComputerRight);


                can = characterView.computerRightControl.SmartSelectCards(e.cardMode, e.big, e.length,
                    e.isbiggest == CharacterType.ComputerRight);
                if (can)
                {
                    List<Card> cards = characterView.computerRightControl.selectCards;
                    CardMode currMode = characterView.computerRightControl.currentMode;
                    foreach (var card in cards)
                        characterView.deskControl.AddCard(card, false, ShowPoint.ComputerRight);
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                        cardMode = currMode,
                        length = cards.Count,
                        characterType = CharacterType.ComputerRight,
                        big = Tool.GetBig(cards, currMode),
                    };
                    if (characterView.computerRightControl.CardCount == 2)
                    {
                        Sound.Instance.PlayEffect(Contsts.Baojing2);
                    }
                    if (characterView.computerRightControl.CardCount == 1)
                    {
                        Sound.Instance.PlayEffect(Contsts.Baojing1);
                    }
                    if (characterView.computerRightControl.CardCount == 0)
                    {
                        Identity left = characterView.computerLeftControl.Identity;
                        Identity right = characterView.computerRightControl.Identity;
                        Identity player = characterView.playerControll.Identity;
                        GameOverArgs overArgs = new GameOverArgs()
                        {
                            ComputerRightWin = true,
                            ComputerLeftWin=left==right?true:false,
                            PlayerWin=player==right?true:false
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver, overArgs);
                    }
                    else
                    {
                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);
                    }
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            default:
                break;
        }
    }
    private void RoundModel_PlayerHandler(bool obj)
    {
        characterView.deskControl.Clear(ShowPoint.Player);
    }

    private void OnUpateIntergration(IEvent data)
    {
        GameData gameData = (GameData)data.data;
        characterView.playerControll.charactaterUI.SetIntergration(gameData.playerIntergration);
        characterView.computerLeftControl.charactaterUI.SetIntergration(gameData.computerLeftIntergration);
        characterView.computerRightControl.charactaterUI.SetIntergration(gameData.computerRightIntergration);
    }

    private void OnRestartGame()
    {
        Lean.Pool.LeanPool.DespawnAll();

        characterView.playerControll.Cards.Clear();
        characterView.computerLeftControl.Cards.Clear();
        characterView.computerRightControl.Cards.Clear();

        characterView.Init();
        intergration.Mulitple = 1;
        dispatcher.Dispatch(CommandEvent.Changemulitipe, 1);
        characterView.playerControll.charactaterUI.SetRemain(0);
        characterView.computerLeftControl.charactaterUI.SetRemain(0);
        characterView.computerRightControl.charactaterUI.SetRemain(0);
        characterView.deskControl.deskUI.SetAlpha(0);
    }
}
