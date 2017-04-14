using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager
{

    public readonly static ActorManager instance = new ActorManager();
    private Dictionary<ActorType,Actor> actors;
    
    ActorManager()
    {
        actors = new Dictionary<ActorType, Actor>();
    }

    public void AddActor(ActorType actorEnum,Actor actor)
    {
        actors.Add(actorEnum, actor);
    }

    public Actor GetActor(ActorType actorEnum)
    {
        return actors[actorEnum];
    }
    
}
