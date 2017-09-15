using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMgr : MonoBehaviour {

    public static NodeMgr instance;

    public CObjectPool<CPoolableObject> interpretNodePool;
    public GameObject f;

    public SpriteRenderer stick;
    public SpriteRenderer triangle;


    ImgTest selectedNode_1;
    ImgTest selectedNode_2;

    public void InsertSelectNode(ImgTest node)
    {
        if (selectedNode_1 == null)
            selectedNode_1 = node;
        else
        {
            selectedNode_2 = node;
            Debug.Log("ㅇㅇ");
        }

        
            
    }

    public void RemoveSelectedNodes()
    {
        selectedNode_1.OutlineOFF();
        selectedNode_2.OutlineOFF();
        selectedNode_1 = null;
        selectedNode_2 = null;
        
    }

    

    void Awake()
    {
        instance = this;
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
