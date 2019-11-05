using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactaterUI : MonoBehaviour {

    public Image head;
    public Text score;
    public Text remain;


    public void SetIdentity(Identity identity)
    {
        switch (identity)
        {
            case Identity.Farmer:
                head.sprite = Resources.Load<Sprite>("Pokers/Role_Farmer");
                break;
            case Identity.Landlord:
                head.sprite = Resources.Load<Sprite>("Pokers/Role_Landlord");
                break;
            default:
                break;
        }
    }

    public void SetIntergration(int score)
    {
        this.score.text = score.ToString();
    }

    public void SetRemain(int remain)
    {
        this.remain.text=remain.ToString();
    }
}
