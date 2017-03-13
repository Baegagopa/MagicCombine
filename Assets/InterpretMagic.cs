using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpretMagic
{
    public float Plus(float a, float b)
    {
        return a + b;
    }

    public float Minus(float a, float b)
    {
        return a - b;
    }

    public float Division(float a, float b)
    {
        return a / b;
    }

    public float Multiply(float a, float b)
    {
        return a * b;
    }

    public float Involution(float a, float b)
    {
        return Mathf.Pow(a, b);
    }

    public float Sqrt(float a)
    {
        return Mathf.Sqrt(a);
    }

    public float GetStatus(ActorEnum actor, StatusEnum status)
    {
        switch (status)
        {
            case StatusEnum.Health:
                return 10f;
            case StatusEnum.Mana:
                return 20f;
            default:
                return 0f;
        }
    }
}
