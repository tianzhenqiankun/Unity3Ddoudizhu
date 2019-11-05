using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntergrationView : View
{

    public Button Mulitple;
    public Button DisMulitple;
    public Button Grab;
    public Button DisGrab;
    public Button Play;
    public Button Pass;
    public Text Tips;
    public Text Mulitpletext;

    public void DeactiveAll()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Mulitple.gameObject.SetActive(false);
        DisMulitple.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
    }
    public void ActivePlayer()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Mulitple.gameObject.SetActive(true);
        DisMulitple.gameObject.SetActive(true);
        Pass.gameObject.SetActive(false);
    }
    public void ActiveGrab()
    {
        Play.gameObject.SetActive(false);
        Grab.gameObject.SetActive(true);
        DisGrab.gameObject.SetActive(true);
        Mulitple.gameObject.SetActive(false);
        DisMulitple.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
    }
    public void ActiveDeal(bool isActive = true)
    {
        Play.gameObject.SetActive(true);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Mulitple.gameObject.SetActive(false);
        DisMulitple.gameObject.SetActive(false);
        Pass.gameObject.SetActive(true);
        Pass.interactable = isActive;
    }
    public void ShowTips(string t)
    {
        Tips.text = t;
    }
    public void ClearTips()
    {
        Tips.text = "";
    }
    
    public void ChangeMulitple(int m)
    {
        Mulitpletext.text = "X" + m;
    }
}
