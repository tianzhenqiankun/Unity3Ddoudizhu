using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMediator :EventMediator {

    [Inject]
    public StartView startView { get; set; }

    public override void OnRegister()
    {
        startView.button1.onClick.AddListener(OnButton1Click);
        startView.button2.onClick.AddListener(OnButton2Click);
    }
    public override void OnRemove()
    {
        startView.button1.onClick.RemoveListener(OnButton1Click);
        startView.button2.onClick.RemoveListener(OnButton2Click);
    }

    private void OnButton1Click()
    {
        Tool.CreateUIPanel(PanelType.CharacterPanel);
        Tool.CreateUIPanel(PanelType.IntergrationPanel);
        
        Destroy(startView.gameObject);
    }
    private void OnButton2Click()
    {
        Application.Quit();
    }
}
