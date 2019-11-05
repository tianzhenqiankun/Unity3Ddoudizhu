using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabLordCommand : EventCommand {

    [Inject]
    public  RoundModel roundModel{ get; set; }
    public override void Execute()
    {
        GrabLordCardArgs e = (GrabLordCardArgs)evt.data;
        dispatcher.Dispatch(ViewEvent.DealLordCard, e);
        roundModel.Start(e.cType);
    }
}
