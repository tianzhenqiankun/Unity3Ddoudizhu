using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;

public class GameRoot : ContextView {

    void Start()
    {
        context = new GameContext(this, true);
        context.Start();
    }
}
