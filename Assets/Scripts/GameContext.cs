using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.context.api;

public class GameContext : MVCSContext {

    public GameContext(MonoBehaviour view,bool autoMapping):base(view,autoMapping)
    {

    }

    protected override void mapBindings()
    {
        //model
        injectionBinder.Bind<Intergration>().To<Intergration>().ToSingleton();
        injectionBinder.Bind<CardModel>().To<CardModel>().ToSingleton();
        injectionBinder.Bind<RoundModel>().To<RoundModel>().ToSingleton();
        //command
       

        commandBinder.Bind(CommandEvent.Changemulitipe).To<ChangemulitipeCommand>();
        commandBinder.Bind(CommandEvent.RequsetDeal).To<RequsetDealCommand>();
        commandBinder.Bind(CommandEvent.GrabLord).To<GrabLordCommand>();
        commandBinder.Bind(CommandEvent.PlayCard).To<PlayCardCommand>();
        commandBinder.Bind(CommandEvent.PassCard).To<PassCardCommand>();
        commandBinder.Bind(CommandEvent.GameOver).To<GameOverCommand>();
        commandBinder.Bind(CommandEvent.RequsetUpdate).To<RequsetUpdateCommand>();
        commandBinder.Bind(CommandEvent.UpdateGameOver).To<UpdateGameOverCommand>();
        //view
        mediationBinder.Bind<StartView>().To<StartMediator>();
        mediationBinder.Bind<IntergrationView>().To<IntergrationMediator>();
        mediationBinder.Bind<CharacterView>().To<CharacterMediator>();
        mediationBinder.Bind<GameOverView>().To<GameOverMediator>();

        commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
    }
}
