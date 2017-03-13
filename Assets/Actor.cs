using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    public float Health { get; set; }
    public float mana { get; set; }
    public Vector3 position { get { return transform.position; } set { transform.position = value; } }
    public ActorEnum actorType { get; set; }
}
