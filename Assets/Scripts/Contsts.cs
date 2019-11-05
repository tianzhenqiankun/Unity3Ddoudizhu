using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contsts {

    public static string DataPath = Application.persistentDataPath + @"\savedata.xml";
    public static string BG = "normal";
    public static string DealCard = "givecard";
    public static string DisGrab = "buqiang";
    public static string Grab = "qiangdizhu1";
    public static string Select = "select";
    public static List<string> PassCard = new List<string> { "buyao1", "buyao2", "buyao3" };
    public static List<string> PlayCard = new List<string> { "dani1", "dani2", "dani3" };
    public static List<string> Single = new List<string> { "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "1", "2", "14", "15" };
    public static List<string> Double= new List<string> { "dui3", "dui4", "dui5", "dui6", "dui7", "dui8", "dui9", "dui10", "dui11",
        "dui12", "dui13", "dui1", "dui2"};
    public static string Straight = "shunzi";
    public static string DoubleStraight = "liandui";
    public static string TripleStraight = "feiji";
    public static string Three = "sange";
    public static string ThreeAndOne = "sandaiyi";
    public static string ThreeAndTwo = "sandaiyidui";
    public static string Boom = "zhadan";
    public static string JokerBoom = "wangzha";
    public static string Win = "win";
    public static string Lose = "lose";
    public static string Baojing1 = "baojing1";
    public static string Baojing2 = "baojing2";
}

public enum PanelType
{
    StartPanel,
    CharacterPanel,
    IntergrationPanel,
    GameOverPanel
}
public enum CommandEvent
{
    Changemulitipe,//是否加倍
    RequsetDeal ,//请求发牌
    GrabLord,//抢地主
    PlayCard,//出牌
    PassCard,//不出牌
    GameOver,//游戏结束
    RequsetUpdate,//数据更新
    UpdateGameOver//结算界面更新
}
public enum ViewEvent
{
    DealCard,
    OnCompulteDeal,
    DealLordCard,
    RequestPlay,
    SuccessPlay,
    UpdateIntergration,
    UpdateGameOver,
    RestartGame,
    ShowTips,
    ChangeMulitple
}
/// <summary>
/// 持有牌的角色
/// </summary>
public enum CharacterType
{
    Library,
    Player,
    ComputerRight,
    ComputerLeft,   
    Desk

}
public enum Colors
{
    Joker,//王
    Club,//梅花
    Heart,//红桃
    Spade,//黑桃
    Square//方片
}
public enum Big
{
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    LJoker
}
public enum CardMode
{
    None,
    Single,//单
    Double,//双
    Straight,//顺子
    DoubleStraight,//双顺
    TripleStraight,//飞机
    Three,//三不带
    ThreeAndOne,//三带一
    ThreeAndTwo,//三带二
    Boom,//炸弹
    JokerBoom//王炸
}
public enum Identity
{
    Farmer,
    Landlord
}
public enum ShowPoint
{
    Desk,
    Player,
    ComputerLeft,
    ComputerRight   
}

