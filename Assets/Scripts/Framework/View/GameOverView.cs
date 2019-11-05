using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : View {

    public Image showImage;
    public List<Sprite> showList;
    public Button button;
    public Button Quit;

    public void Init(bool isLord,bool isWin)
    {
        if (isLord)
        {
            if (isWin)
            {
                Sound.Instance.PlayEffect(Contsts.Win);
                showImage.sprite = showList[0];
            }
            else
            {
                Sound.Instance.PlayEffect(Contsts.Lose);
                showImage.sprite = showList[1];
            }
                
        }
        else
        {
            if (isWin)
            {
                Sound.Instance.PlayEffect(Contsts.Win);
                showImage.sprite = showList[2];
            }
            else
            {
                Sound.Instance.PlayEffect(Contsts.Lose);
                showImage.sprite = showList[3];
            }
                
        }
    }
}
