using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PassCardCommand:EventCommand
{
    [Inject]
    public RoundModel roundModel { get; set; }

    public override void Execute()
    {
        roundModel.Turn();
    }
}
