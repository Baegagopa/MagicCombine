using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private void Awake()
    {
        ActorManager.instance.AddActor(ActorType.Player, this);
    }
    void Start () {
        Health = 10f;
        Mana = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
