namespace MaxTools
{
    public enum PointerType
    {
        None,
        Down,
        Up,
        Enter,
        Exit,
        Click,
        DragBegin,
        Drag,
        DragEnd,
        Drop
    }

    public enum TimeMode
    {
        ScaledTime, UnscaledTime
    }

    public enum AxisType
    {
        Horizontal, Vertical
    }

    public enum OperatorType
    {
        op_UnaryPlus,          // +
        op_UnaryNegation,      // -
        op_LogicalNot,         // !
        op_OnesComplement,     // ~
        op_Increment,          // ++
        op_Decrement,          // --
        op_True,               // true
        op_False,              // false
        op_Addition,           // +
        op_Subtraction,        // -
        op_Multiply,           // *
        op_Division,           // /
        op_Modulus,            // %
        op_BitwiseAnd,         // &
        op_BitwiseOr,          // |
        op_ExclusiveOr,        // ^
        op_LeftShift,          // <<
        op_RightShift,         // >>
        op_Equality,           // ==
        op_Inequality,         // !=
        op_GreaterThan,        // >
        op_LessThan,           // <
        op_GreaterThanOrEqual, // >=
        op_LessThanOrEqual,    // <=
        op_Implicit,           //
        op_Explicit            //
    }

    public enum OperatorParameterPosition
    {
        Undefined, First, Second
    }

    public enum IndexerType
    {
        get_Item,
        set_Item
    }
}
