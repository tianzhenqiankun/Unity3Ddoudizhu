using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intergration {
    /// <summary>
    /// 底分
    /// </summary>
    public int BasePoint;
    /// <summary>
    /// 倍数
    /// </summary>
    public int Mulitple;
    public int Result
    {
        get
        {
            return BasePoint * Mulitple;
        }
    }

    public int PlayerIntergration
    {
        get
        {
            return playerIntergration;
        }

        set
        {
            if (playerIntergration<0)
            {
                playerIntergration = 0;
            }
            else playerIntergration = value;
        }
    }

    public int ComputerLeftIntergration
    {
        get
        {
            return computerLeftIntergration;
        }

        set
        {
            if (computerLeftIntergration < 0)
                computerLeftIntergration = 0;
            else
            computerLeftIntergration = value;
        }
    }

    public int ComputerRightIntergration
    {
        get
        {
            return computerRightIntergration;
        }

        set
        {
            if (computerRightIntergration < 0)
                computerRightIntergration = 0;
            else
            computerRightIntergration = value;
        }
    }

    private int playerIntergration;
    private int computerLeftIntergration;
    private int computerRightIntergration;

    public void InitIntergration()
    {
        Mulitple = 1;
        BasePoint = 100;
        PlayerIntergration = 1500;
        ComputerLeftIntergration = 1500;
        ComputerRightIntergration = 1500;
    }
}
