using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangemulitipeCommand : EventCommand {

    [Inject]
    public Intergration intergration { get; set; }   
    public override void Execute()
    {
        //改model
        intergration.Mulitple *= (int)evt.data;
        dispatcher.Dispatch(ViewEvent.ChangeMulitple, intergration.Mulitple);
        //添加面板

    }
}
