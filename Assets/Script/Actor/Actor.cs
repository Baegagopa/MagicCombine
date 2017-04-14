using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    private float health;
    private float mana;

    public float Health { get { return health; } set { health = value; } }
    public float Mana { get { return mana; } set { mana = value; } }
    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public ActorType ActorType { get; set; }
}

