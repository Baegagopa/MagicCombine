using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// enum 과 int 형을 같이 저장하기 위한 자료형
public struct Bytecode
{
    [StructLayout(LayoutKind.Explicit)]
    struct Value
    {
        [FieldOffset(0)]
        public Command _commend;
        [FieldOffset(1)]
        public ActorType _actor;
        [FieldOffset(2)]
        public float _float;
    }

    Value val;

    public Bytecode(Command d) : this() {val._commend = d; }
    public static implicit operator Command(Bytecode d)
    {
        return d.val._commend;
    }
    public static implicit operator Bytecode(Command d)
    {
        return new Bytecode(d);
    }

    public Bytecode(ActorType d) : this() { val._actor = d; }
    public static implicit operator ActorType(Bytecode d)
    {
        return d.val._actor;
    }
    public static implicit operator Bytecode(ActorType d)
    {
        return new Bytecode(d);
    }

    public Bytecode(float d) : this() { val._float = d; }
    public static implicit operator float(Bytecode d)
    {
        return d.val._float;
    }
    public static implicit operator Bytecode(float d)
    {
        return new Bytecode(d);
    }
}


public class BytecodeVM : MonoBehaviour
{

    const int MAX_STACK = 128;
    int stackSize;
    Bytecode[] cmdStack;

    void Init()
    {
        stackSize = 0;
        cmdStack = new Bytecode[MAX_STACK];
    }

    void Push(Bytecode val)
    {
        if ( stackSize > MAX_STACK )
            Debug.Log("스택 오버플로 _ 오버");
        else
            cmdStack[stackSize++] = val;
    }

    Bytecode Pop()
    {
        if ( stackSize < 1 )
        {
            Debug.Log("스택 오버플로 _ 언더");
            return -1;
        }
        else
            return cmdStack[--stackSize];
    }

    void Interpret(Bytecode[] bytecode, int size)
    {
        int i;
        float a, b, val;
        ActorType targetA;
        for(i = 0; i < size; i++)
        {
            switch ((Command)bytecode[i])
            {

                case Command.CMD_LITERAL:
                    val = bytecode[++i];
                    Debug.Log("값 " + val + " 입력");
                    Push(val);
                    break;

                case Command.CMD_CALC_PLUS:
                    b = Pop();
                    a = Pop();

                    Debug.Log("값 " + a + " + " + b + " = " + InterpretMagic.Plus(a, b));
                    Push(InterpretMagic.Plus(a, b));
                    break;

                case Command.CMD_CALC_MINUS:
                    b = Pop();
                    a = Pop();
                    Push(InterpretMagic.Minus(a, b));
                    break;

                case Command.CMD_CALC_DIVISION:
                    b = Pop();
                    a = Pop();

                    Push(InterpretMagic.Division(a, b));
                    break;

                case Command.CMD_CALC_MULTIPLY:
                    b = Pop();
                    a = Pop();
                    Push(InterpretMagic.Multiply(a, b));
                    break;

                case Command.CMD_CALC_INVOLUTION:
                    b = Pop();
                    a = Pop();

                    Debug.Log("값 " + a + " 의 " + b + "제곱 = " + InterpretMagic.Involution(a, b));
                    Push(InterpretMagic.Involution(a, b));
                    break;

                case Command.CMD_CALC_SQRT:
                    val = Pop();
                    Push(InterpretMagic.Sqrt(val));
                    break;

                case Command.CMD_GET_HEALTH:
                    targetA = Pop();
                    val = InterpretMagic.GetStatus(targetA, StatusType.Health);
                    Debug.Log("체력 " + val + "불러옴");
                    Push(val);
                    break;

                case Command.CMD_GET_MANA:
                    targetA = Pop();
                    val = InterpretMagic.GetStatus(targetA, StatusType.Mana);
                    Debug.Log("마력 " + val + "불러옴");
                    Push(val);
                    break;

                case Command.CMD_LOG:
                    Debug.Log("로그 : " + (float)Pop());
                    break;
            }


        }
    }


    void Start() {
        Init();
        
        Bytecode[] bytecode____ = {
            Command.CMD_LITERAL,
            ActorType.Player,
            Command.CMD_GET_HEALTH,
            Command.CMD_LITERAL,
            ActorType.Player,
            Command.CMD_GET_MANA,
            Command.CMD_CALC_PLUS,
            Command.CMD_LITERAL,
            2,
            Command.CMD_CALC_INVOLUTION,
            Command.CMD_LOG
        };

        Interpret(bytecode____, bytecode____.Length);

    }

}
