


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

// 마법 대상 목록
public enum ActorType
{
    Player,
    Ai_1,
    Ai_2,
    Ai_3,
    Ai_4,
    Ai_5,
}

// 메인 노드 타입
public enum InterpretNodeType
{
    MAGIC,              // 마법
    FORM,               // 형태
    TRIGGERCONDITION,   // 발동 조건
    ATTRIBUTE,          // 기타 상태
}

// 구성품 노드 타입
public enum ComponentNodeType
{
    StatusType,
    OperatorType,
}

// 스탯
public enum StatusType
{
    Health,
    Mana,
}

// 마법 종류
public enum MagicType
{
    ATTACK,
    DEFENCE,
    MOVEMENT,
    CURE,
    BUFF,
    DEBUFF,
}

// 형태
public enum FormType
{
    TARGET_ONLY,    // 타겟만
    FANWISE,        // 부채꼴
    

}

// 발동 조건
public enum TriggerConditionType
{
    TRAP,       // 함정

}

//기타 상태
public enum AttributeType
{
    PENETRATE,   // 관통
}
