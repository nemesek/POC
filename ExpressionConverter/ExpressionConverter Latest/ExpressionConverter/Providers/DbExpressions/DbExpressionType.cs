namespace ExpressionConverter.Providers.DbExpressions
{
    internal enum DbExpressionType
    {
        Table = 1000, // make sure these don't overlap with ExpressionType
        Column,
        Select,
        Projection
    }
}