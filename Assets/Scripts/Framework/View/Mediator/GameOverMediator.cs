using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMediator :EventMediator{

    [Inject]
    public GameOverView gameOverView { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(ViewEvent.UpdateGameOver, OnUpdateGameOver);
        gameOverView.button.onClick.AddListener(OnReStartClick);
        gameOverView.Quit.onClick.AddListener(OnQuitClick);

        dispatcher.Dispatch(CommandEvent.UpdateGameOver);
    }


    public override void OnRemove()
    {
        dispatcher.RemoveListener(ViewEvent.UpdateGameOver, OnUpdateGameOver);
        gameOverView.button.onClick.RemoveListener(OnReStartClick);
        gameOverView.Quit.onClick.RemoveListener(OnQuitClick);
    }

    private void OnUpdateGameOver(IEvent evt)
    {
        UpdateGameOverArgs e = (UpdateGameOverArgs)evt.data;

        gameOverView.Init(e.isLord, e.isWin);
    }
    private void OnReStartClick()
    {
        dispatcher.Dispatch(ViewEvent.RestartGame);
        Destroy(gameObject);
    }

    private void OnQuitClick()
    {
        Application.Quit();
    }
}
