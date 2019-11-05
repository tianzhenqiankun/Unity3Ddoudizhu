using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.command.impl;
using System;
using System.IO;

public class StartCommand : Command {

    [Inject]
    public Intergration intergration { get; set; }

    [Inject]
    public RoundModel roundModel { get; set; }

    [Inject]
    public CardModel cardModel { get; set; }

    /// <summary>
    /// 执行事件
    /// </summary>
    public override void Execute()
    {
        Tool.CreateUIPanel(PanelType.StartPanel);
        //初始化
        intergration.InitIntergration();
        roundModel.InitRound();
        cardModel.InitCardLibrary();

        //读取数据
        GetData();

        //播放背景音乐
        Sound.Instance.PlayBG(Contsts.BG);
    }

    private void GetData()
    {
        FileInfo info = new FileInfo(Contsts.DataPath);
        if (info.Exists)
        {
            GameData data = Tool.GetData();
            intergration.PlayerIntergration = data.playerIntergration;
            intergration.ComputerLeftIntergration = data.computerLeftIntergration;
            intergration.ComputerRightIntergration = data.computerRightIntergration;
        }
    }
}
