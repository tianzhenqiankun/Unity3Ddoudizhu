using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayCardCommand:EventCommand
{
    [Inject]
    public RoundModel roundModel { get; set; }
    [Inject]
    public Intergration intergration { get; set; }

    public override void Execute()
    {
        PlayCardArgs e = (PlayCardArgs)evt.data;
        if (e.characterType==CharacterType.Player)
        {
            if (e.cardMode == roundModel.CurrentMode &&e.length==roundModel.CurrentLength&&
                e.big > roundModel.CurrentBig)
                dispatcher.Dispatch(ViewEvent.SuccessPlay);
            else if (e.cardMode == CardMode.Boom && roundModel.CurrentMode != CardMode.Boom)
                dispatcher.Dispatch(ViewEvent.SuccessPlay);
            else if (e.cardMode == CardMode.JokerBoom)
                dispatcher.Dispatch(ViewEvent.SuccessPlay);
            else if (e.characterType == roundModel.BiggestCharacter)
                dispatcher.Dispatch(ViewEvent.SuccessPlay);
            else
            {
                Tips tips = new Tips()
                {
                    tips="你选择的牌不符合规则"
                };
                dispatcher.Dispatch(ViewEvent.ShowTips,tips);
                return;
            }
        }





        //播放音效
        if (e.characterType!=roundModel.BiggestCharacter&&e.cardMode!=CardMode.Single&&
            e.cardMode!=CardMode.Double&&e.cardMode!=CardMode.Boom&&e.cardMode!=CardMode.JokerBoom)
        {
            Sound.Instance.PlayEffect(Contsts.PlayCard[UnityEngine.Random.Range(0, 3)]);
        }
        else
        {
            switch (e.cardMode)
            {
                case CardMode.Single:
                    Sound.Instance.PlayEffect(Contsts.Single[e.big]);
                    break;
                case CardMode.Double:
                    Sound.Instance.PlayEffect(Contsts.Double[e.big / 2]);
                    break;
                case CardMode.Straight:
                    Sound.Instance.PlayEffect(Contsts.Straight);
                    break;
                case CardMode.DoubleStraight:
                    Sound.Instance.PlayEffect(Contsts.DoubleStraight);
                    break;
                case CardMode.TripleStraight:
                    Sound.Instance.PlayEffect(Contsts.TripleStraight);
                    break;
                case CardMode.Three:
                    Sound.Instance.PlayEffect(Contsts.Three);
                    break;
                case CardMode.ThreeAndOne:
                    Sound.Instance.PlayEffect(Contsts.ThreeAndOne);
                    break;
                case CardMode.ThreeAndTwo:
                    Sound.Instance.PlayEffect(Contsts.ThreeAndTwo);
                    break;
                case CardMode.Boom:
                    Sound.Instance.PlayEffect(Contsts.Boom);
                    break;
                case CardMode.JokerBoom:
                    Sound.Instance.PlayEffect(Contsts.JokerBoom);
                    break;
                default:
                    break;
            }
        }

        roundModel.BiggestCharacter = e.characterType;
        roundModel.CurrentBig = e.big;
        roundModel.CurrentLength = e.length;
        roundModel.CurrentMode = e.cardMode;

        if (e.cardMode == CardMode.Boom)
            dispatcher.Dispatch(CommandEvent.Changemulitipe,2);
        else if (e.cardMode == CardMode.JokerBoom)
            dispatcher.Dispatch(CommandEvent.Changemulitipe, 4);

        roundModel.Turn();
    }
}

