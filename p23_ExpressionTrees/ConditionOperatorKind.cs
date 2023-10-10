namespace p23_ExpressionTrees
{
    public enum ConditionOperatorKind : short
    {
        Equal = 0,
        NotEqual = 1,
        GreaterThan = 2,
        LessThan = 3,
        GreaterThanOrEqual = 4,
        LessThanOrEqual = 5,
        Contains = 6,
        Filled = 7,
        NotFilled = 8,
        Include = 9
        //interval, containsall ...
    }
}