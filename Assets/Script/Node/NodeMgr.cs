using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMgr : MonoBehaviour {

    public CObjectPool<CPoolableObject> interpretNodePool;
    public GameObject f;       
    void Awake()
    {
        interpretNodePool = new CObjectPool<CPoolableObject>();
    }

    void Start ()
    {
        interpretNodePool = new CObjectPool<CPoolableObject>(5, () =>
        {
            //GameObject prefab = Instantiate(f) as GameObject;
            GameObject prefab = Instantiate(Resources.Load("Prefab/InterpretNode")) as GameObject;
            InterpretNode node = prefab.GetComponent<InterpretNode>();
            node.Create(interpretNodePool);
            return node;
        });
        interpretNodePool.Allocate();
    }
	
	void Update ()
    {
		
	}

}
