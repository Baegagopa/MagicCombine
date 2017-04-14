using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpretNode : CPoolableObject
{

    public InterpretNodeType nodeType;

    public override void Create(CObjectPool<CPoolableObject> pool)
    {
        base.Create(pool);
        
    }

    public void Update()
    {

    }


    

    public override void _OnEnableContents() { }
    public override void _OnDisableContents() { }
}
