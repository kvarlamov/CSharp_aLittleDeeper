using System;
using System.Linq.Expressions;

namespace p23_ExpressionTrees;

/// <summary>
/// Обеспечивает вспомагательными методами спецификации.
/// </summary>
public static class SpecificationExtension
{
    public static Expression<Func<T, bool>> Create<T>(bool value = true) =>
        value
            ? parameter => true
            : parameter => false;

    /// <summary>
    /// Используется для комбинации двух выражений посредством AND
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <param name="expr1">Первое объединямое выражение</param>
    /// <param name="expr2">Второе объединяемое выражение</param>
    /// <returns>The <see cref="Expression{Func{T, bool}}"/></returns>
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        if (IsExpressionBodyConstant(expr1))
            return expr2;

        ParameterExpression? parameter = Expression.Parameter(typeof(T));

        ReplaceExpressionVisitor? leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        Expression? left = leftVisitor.Visit(expr1.Body);

        ReplaceExpressionVisitor? rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        Expression? right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
    }

    public static Expression<Func<T, bool>> OrElse<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        if (IsExpressionBodyConstant(expr1))
            return expr2;

        ParameterExpression? parameter = Expression.Parameter(typeof(T));

        ReplaceExpressionVisitor? leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        Expression? left = leftVisitor.Visit(expr1.Body);

        ReplaceExpressionVisitor? rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        Expression? right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
    }

    private static bool IsExpressionBodyConstant<T>(Expression<Func<T, bool>> left)
    {
        return left.Body.NodeType == ExpressionType.Constant;
    }

    /// <summary>
    /// Определяет визитёр для выражений
    /// </summary>
    private class ReplaceExpressionVisitor
        : ExpressionVisitor
    {
        /// <summary>
        /// Определяет основное выражение
        /// </summary>
        private readonly Expression _oldValue;

        /// <summary>
        /// Определяет новое добавляемое выражение
        /// </summary>
        private readonly Expression _newValue;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ReplaceExpressionVisitor"/>.
        /// </summary>
        /// <param name="oldValue">Старое выражение.</param>
        /// <param name="newValue">Новое выражение.</param>
        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        /// <summary>
        /// Метод посетителя.
        /// </summary>
        /// <param name="node">Целевое выражение.</param>
        /// <returns>Выражение-результат</returns>
        public override Expression Visit(Expression node)
        {
            return node == _oldValue ? _newValue : base.Visit(node) ?? node;
        }
    }
}