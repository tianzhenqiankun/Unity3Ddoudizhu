using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntergrationMediator : EventMediator {

    [Inject]
    public IntergrationView intergrationView { get; set; }

    [Inject]
    public RoundModel roundModel { get; set; }

    public override void OnRegister()
    {
        intergrationView.Mulitple.onClick.AddListener(OnMulitpleClick);
        intergrationView.DisMulitple.onClick.AddListener(OnDisMulitpleClick);
        intergrationView.Grab.onClick.AddListener(OnGrabClick);
        intergrationView.DisGrab.onClick.AddListener(OnDisGrabClick);
        intergrationView.Play.onClick.AddListener(OnPlayClick);
        intergrationView.Pass.onClick.AddListener(OnPassClick);
        dispatcher.AddListener(ViewEvent.OnCompulteDeal,OnCompulteDeal);
        dispatcher.AddListener(ViewEvent.SuccessPlay, OnSuccessPlay);
        dispatcher.AddListener(ViewEvent.RestartGame, OnRestartGame);
        dispatcher.AddListener(ViewEvent.ShowTips, OnShowTips);
        dispatcher.AddListener(ViewEvent.ChangeMulitple, OnChangeMulitple);

        RoundModel.PlayerHandler += RoundModel_PlayerHandler;
    }


    public override void OnRemove()
    {
        intergrationView.Mulitple.onClick.RemoveListener(OnMulitpleClick);
        intergrationView.DisMulitple.onClick.RemoveListener(OnDisMulitpleClick);
        intergrationView.Grab.onClick.RemoveListener(OnGrabClick);
        intergrationView.DisGrab.onClick.RemoveListener(OnDisGrabClick);
        intergrationView.Play.onClick.RemoveListener(OnPlayClick);
        intergrationView.Pass.onClick.RemoveListener(OnPassClick);
        dispatcher.RemoveListener(ViewEvent.OnCompulteDeal, OnCompulteDeal);
        dispatcher.RemoveListener(ViewEvent.SuccessPlay, OnSuccessPlay);
        dispatcher.RemoveListener(ViewEvent.RestartGame, OnRestartGame);
        dispatcher.RemoveListener(ViewEvent.ShowTips, OnShowTips);
        dispatcher.RemoveListener(ViewEvent.ChangeMulitple, OnChangeMulitple);

        RoundModel.PlayerHandler -= RoundModel_PlayerHandler;
    }
    private void OnGrabClick()
    {
        intergrationView.DeactiveAll();
        GrabLordCardArgs e = new GrabLordCardArgs() { cType = CharacterType.Player };
        Sound.Instance.PlayEffect(Contsts.Grab);
        dispatcher.Dispatch(CommandEvent.GrabLord,e);
    }
    private void OnDisGrabClick()
    {
        
        intergrationView.DeactiveAll();
        Sound.Instance.PlayEffect(Contsts.DisGrab);
        int x= UnityEngine.Random.Range(0, 2);
        CharacterType characterType;
        if (x==0)
        {
            characterType = CharacterType.ComputerLeft;
        }
        else
        {
            characterType = CharacterType.ComputerRight;
        }       
        GrabLordCardArgs e = new GrabLordCardArgs() { cType = characterType };       
        dispatcher.Dispatch(CommandEvent.GrabLord, e);
    }
    private void OnCompulteDeal()
    {
        intergrationView.ActiveGrab();
    }


    private void OnMulitpleClick()
    {
        dispatcher.Dispatch(CommandEvent.Changemulitipe, 2);
        dispatcher.Dispatch(CommandEvent.RequsetDeal);
        intergrationView.DeactiveAll();
    }

    private void OnDisMulitpleClick()
    {
        dispatcher.Dispatch(CommandEvent.Changemulitipe, 1);
        dispatcher.Dispatch(CommandEvent.RequsetDeal);
        intergrationView.DeactiveAll();
    }

    private void RoundModel_PlayerHandler(bool CanPass)
    {
        intergrationView.ActiveDeal(CanPass);
    }
    private void OnPlayClick()
    {
        dispatcher.Dispatch(ViewEvent.RequestPlay);
    }
    private void OnSuccessPlay()
    {
        intergrationView.DeactiveAll();
    }
    private void OnPassClick()
    {
        dispatcher.Dispatch(CommandEvent.PassCard);
        intergrationView.DeactiveAll();
        Sound.Instance.PlayEffect(Contsts.PassCard[UnityEngine.Random.Range(0, 3)]);
    }

    private void OnRestartGame()
    {
        intergrationView.DeactiveAll();
        intergrationView.ActivePlayer();
    }

    private void OnShowTips(IEvent evt)
    {
        Tips tips = (Tips)evt.data;
        intergrationView.ShowTips(tips.tips);
        StartCoroutine(ClearTip());
    }
    IEnumerator ClearTip()
    {
        yield return new WaitForSeconds(1);
        intergrationView.ClearTips();
    }

    private void OnChangeMulitple(IEvent evt)
    {
        intergrationView.ChangeMulitple((int)evt.data);
    }

}
