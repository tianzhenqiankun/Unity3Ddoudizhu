using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UpdateGameOverCommand:EventCommand
{
    [Inject]
    public RoundModel roundModel { get; set; }
    public override void Execute()
    {
        UpdateGameOverArgs e = new UpdateGameOverArgs()
        {
            isLord = roundModel.isLord,
            isWin = roundModel.isWin
        };
        dispatcher.Dispatch(ViewEvent.UpdateGameOver, e);
    }
}

