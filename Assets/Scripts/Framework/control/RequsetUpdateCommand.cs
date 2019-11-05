using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RequsetUpdateCommand:EventCommand
{
    [Inject]
    public Intergration intergration { get; set; }

    public override void Execute()
    {
        GameData gameData = new GameData()
        {
            playerIntergration = intergration.PlayerIntergration,
            computerLeftIntergration = intergration.ComputerLeftIntergration,
            computerRightIntergration = intergration.ComputerRightIntergration
        };
        dispatcher.Dispatch(ViewEvent.UpdateIntergration, gameData);
    }
}

