using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : View {

    public PlayerControll playerControll;
    public DeskControl deskControl;
    public ComputerControl computerLeftControl;
    public ComputerControl computerRightControl;

    public void Init()
    {
        playerControll.Identity = Identity.Farmer;
        computerLeftControl.Identity = Identity.Farmer;
        computerRightControl.Identity = Identity.Farmer;
    }

    public void AddCard(CharacterType type,Card card,bool isSelect,ShowPoint pos)
    {
        switch (type)
        {          
            case CharacterType.Player:
                playerControll.AddCard(card, isSelect);
                break;
            case CharacterType.ComputerLeft:
                computerLeftControl.AddCard(card, isSelect);
                break;
            case CharacterType.ComputerRight:
                computerRightControl.AddCard(card, isSelect);
                break;
            case CharacterType.Desk:
                deskControl.AddCard(card, isSelect, pos);
                break;
            default:
                break;
        }
    }

    public void DealLordCard(CharacterType characterType)
    {
        Card card = null;
        switch (characterType)
        {            
            case CharacterType.Player:                
                for (int i = 0; i < 3; i++)
                {
                    card = deskControl.DealCard();                    
                    playerControll.AddCard(card, true);
                    deskControl.SetShowCard(card, i);
                }
                playerControll.Identity = Identity.Landlord;
                playerControll.Sort(false);
                break;
            case CharacterType.ComputerLeft:
                for (int i = 0; i < 3; i++)
                {
                    card = deskControl.DealCard();
                    computerLeftControl.AddCard(card, false);
                    deskControl.SetShowCard(card, i);
                }
                computerLeftControl.Identity = Identity.Landlord;
                computerLeftControl.Sort(true);
                break;
            case CharacterType.ComputerRight:
                for (int i = 0; i < 3; i++)
                {
                    card = deskControl.DealCard();
                    computerRightControl.AddCard(card, false);
                    deskControl.SetShowCard(card, i);
                }
                computerRightControl.Identity = Identity.Landlord;
                computerRightControl.Sort(true);
                break;
            default:
                break;
        }
        deskControl.Clear(ShowPoint.Desk);

    }
}
