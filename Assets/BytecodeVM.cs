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
        public ActorEnum _actor;
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

    public Bytecode(ActorEnum d) : this() { val._actor = d; }
    public static implicit operator ActorEnum(Bytecode d)
    {
        return d.val._actor;
    }
    public static implicit operator Bytecode(ActorEnum d)
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

// 명령어 목록
public enum Command 
{
    CMD_LITERAL = 1,

    CMD_CALC_PLUS,
    CMD_CALC_MINUS,
    CMD_CALC_DIVISION,
    CMD_CALC_MULTIPLY,
    CMD_CALC_INVOLUTION,
    CMD_CALC_SQRT,

    CMD_GET_HEALTH,
    CMD_GET_MANA,

    CMD_LOG,
}

public enum ActorEnum
{
    Player,
    Ai_1,
    Ai_2,
    Ai_3,
    Ai_4,
    Ai_5,
}

public enum StatusEnum
{
    Health,
    Mana,
}

public class BytecodeVM : MonoBehaviour
{

    const int MAX_STACK = 128;
    int stackSize;
    Bytecode[] cmdStack;
    InterpretMagic inteMagic;

    void Init()
    {
        stackSize = 0;
        cmdStack = new Bytecode[MAX_STACK];
        inteMagic = new InterpretMagic();
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
        ActorEnum targetA;
        for(i = 0; i < size; i++)
        {
            switch ((Command)bytecode[i])
            {

                case Command.CMD_LITERAL:
                    val = bytecode[++i];
                    Push(val);
                    break;

                case Command.CMD_CALC_PLUS:
                    b = Pop();
                    a = Pop();
                    
                    Push(inteMagic.Plus(a, b));
                    break;

                case Command.CMD_CALC_MINUS:
                    b = Pop();
                    a = Pop();
                    Push(inteMagic.Minus(a, b));
                    break;

                case Command.CMD_CALC_DIVISION:
                    b = Pop();
                    a = Pop();
                    Push(inteMagic.Division(a, b));
                    break;

                case Command.CMD_CALC_MULTIPLY:
                    b = Pop();
                    a = Pop();
                    Push(inteMagic.Multiply(a, b));
                    break;

                case Command.CMD_CALC_INVOLUTION:
                    b = Pop();
                    a = Pop();
                    Push(inteMagic.Involution(a, b));
                    break;

                case Command.CMD_CALC_SQRT:
                    val = Pop();
                    Push(inteMagic.Sqrt(val));
                    break;

                case Command.CMD_GET_HEALTH:
                    targetA = Pop();
                    val = inteMagic.GetStatus(targetA, StatusEnum.Health);
                    Push(val);
                    break;

                case Command.CMD_GET_MANA:
                    targetA = Pop();
                    val = inteMagic.GetStatus(targetA, StatusEnum.Mana);
                    Push(val);
                    break;

                case Command.CMD_LOG:
                    Debug.Log((float)Pop());
                    break;
            }
        }
    }


    void Start() {
        Init();
        Actor a;
        
        Bytecode[] bytecode____ = { Command.CMD_LITERAL, ActorEnum.Player, Command.CMD_GET_HEALTH, Command.CMD_LITERAL, ActorEnum.Player, Command.CMD_GET_MANA, Command.CMD_CALC_PLUS, Command.CMD_LITERAL, 2, Command.CMD_CALC_INVOLUTION, Command.CMD_LOG };

        Interpret(bytecode____, bytecode____.Length);

    }

}
