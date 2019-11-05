using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

public  static class Tool {

    static Transform transform;

    public static Transform Transform
    {
        get
        {
            if (transform==null)
            {
                transform = GameObject.Find("Canvas").transform;
            }
            return transform;
        }

        set
        {
            transform = value;
        }
    }

    public static GameObject CreateUIPanel(PanelType type)
    {

        GameObject gameObject = Resources.Load<GameObject>(type.ToString());
        if (gameObject==null)
        {
            Debug.Log(type.ToString()+"不存在");
            return null;
        }
        else
        {
            GameObject panel = GameObject.Instantiate(gameObject,Transform);
            panel.name = type.ToString();
            return gameObject;
        }
    }

    public static void Sort(List<Card> cards,bool asc)
    {
        cards.Sort(
            (Card a, Card b) =>
            {
                if (asc)
                    return a.CardBig.CompareTo(b.CardBig);
                else
                    return -a.CardBig.CompareTo(b.CardBig);
            }
            );
    }
    public static int GetBig(List<Card> cards,CardMode cardMode)
    {
        int totalbig = 0;
        if (cardMode==CardMode.ThreeAndOne||cardMode==CardMode.ThreeAndTwo)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].CardBig==cards[i+1].CardBig&&cards[i].CardBig==cards[i+2].CardBig)
                {
                    totalbig += (int)cards[i].CardBig;
                    totalbig *= 3;
                    break;
                }
            }
        }
        else if(cardMode==CardMode.Straight||cardMode==CardMode.DoubleStraight)
        {
            totalbig = (int)cards[0].CardBig;
        }
        else
        {
            for (int i = 0; i < cards.Count; i++)
            {
                totalbig += (int)cards[i].CardBig;
            }
            
        }
        return totalbig;
    }
    
    public static void SavaData(GameData data)
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement root = xmldoc.CreateElement("save");
        root.SetAttribute("name", "savefile");

        XmlElement playerscore = xmldoc.CreateElement("playerscore");
        playerscore.InnerText = data.playerIntergration.ToString();
        root.AppendChild(playerscore);
        XmlElement computerleftscore = xmldoc.CreateElement("computerleftscore");
        computerleftscore.InnerText = data.computerLeftIntergration.ToString();
        root.AppendChild(computerleftscore);
        XmlElement computerrightscore = xmldoc.CreateElement("computerrightscore");
        computerrightscore.InnerText = data.computerRightIntergration.ToString();
        root.AppendChild(computerrightscore);

        xmldoc.AppendChild(root);
        xmldoc.Save(Contsts.DataPath);
    }

    public static GameData GetData()
    {
        GameData data = new GameData();
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Contsts.DataPath);
        XmlNodeList playerscore = xmldoc.GetElementsByTagName("playerscore");
        int player = int.Parse(playerscore[0].InnerText);
        XmlNodeList computerleftscore = xmldoc.GetElementsByTagName("computerleftscore");
        int computerleft = int.Parse(computerleftscore[0].InnerText);
        XmlNodeList computerrightscore = xmldoc.GetElementsByTagName("computerrightscore");
        int computerright = int.Parse(computerrightscore[0].InnerText);

        data.playerIntergration = player;
        data.computerLeftIntergration = computerleft;
        data.computerRightIntergration = computerright;

        return data;
    }
}
