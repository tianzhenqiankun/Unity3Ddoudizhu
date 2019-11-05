using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GameOverCommand:EventCommand
{
    [Inject ]
    public RoundModel roundModel { get; set; }

    [Inject]
    public Intergration intergration { get; set; }

    [Inject]
    public CardModel cardModel { get; set; }

    public override void Execute()
    {
        int temp = intergration.Result;

        GameOverArgs e = (GameOverArgs)evt.data;
        //更新数据
        if (e.PlayerWin)
            intergration.PlayerIntergration += temp;
        else
            intergration.PlayerIntergration -= temp;
        if (e.ComputerLeftWin)
            intergration.ComputerLeftIntergration += temp;
        else
            intergration.ComputerLeftIntergration -= temp;
        if (e.ComputerRightWin)
            intergration.ComputerRightIntergration += temp;
        else
            intergration.ComputerRightIntergration -= temp;


        roundModel.isLord = e.isLord;
        roundModel.isWin = e.PlayerWin;
        //存储数据
        GameData data = new GameData()
        {
            computerLeftIntergration = intergration.ComputerLeftIntergration,
            computerRightIntergration = intergration.ComputerRightIntergration,
            playerIntergration = intergration.PlayerIntergration
        };
        Tool.SavaData(data);

        //更新数据
        GameData gameData = new GameData()
        {
            playerIntergration = intergration.PlayerIntergration,
            computerLeftIntergration = intergration.ComputerLeftIntergration,
            computerRightIntergration = intergration.ComputerRightIntergration
        };
        dispatcher.Dispatch(ViewEvent.UpdateIntergration, gameData);
        //添加面板
        Tool.CreateUIPanel(PanelType.GameOverPanel);
        //清除游戏数据
        roundModel.InitRound();
        cardModel.InitCardLibrary();
    }

}

