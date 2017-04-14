using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InterpretMagic
{
    public static float Plus(float a, float b)
    {
        return a + b;
    }

    public static float Minus(float a, float b)
    {
        return a - b;
    }

    public static float Division(float a, float b)
    {
        return a / b;
    }

    public static float Multiply(float a, float b)
    {
        return a * b;
    }

    public static float Involution(float a, float b)
    {
        return Mathf.Pow(a, b);
    }

    public static float Sqrt(float a)
    {
        return Mathf.Sqrt(a);
    }

    public static float GetStatus(ActorType actor, StatusType status)
    {
        switch (status)
        {
            case StatusType.Health:
                return ActorManager.instance.GetActor(actor).Health;
            case StatusType.Mana:
                return ActorManager.instance.GetActor(actor).Mana;
            default:
                return 0f;
        }
    }
}
