using System.Linq.Expressions;
using System.Reflection;
using CQRSLinbis.Domain.Attributes;

namespace CQRSLinbis.Infrastructure.Extensions;
public static class SercheableExtensions
{
    public static Expression<Func<TEntity, bool>>? BuildSearchPredicate<TEntity>(string searchString)
    {
        List<PropertyInfo> properties = typeof(TEntity).GetTypeInfo().DeclaredProperties
            .Where(p => p.GetCustomAttribute(typeof(SearchableAttribute)) != null)
            .ToList();

        if (properties.Count == 0)
            return null;

        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "p");
        List<Expression> searchExpressions = new List<Expression>();

        foreach (var property in properties)
        {
            MemberExpression propertyExpression = Expression.Property(parameter, property);
            ConstantExpression searchValue = Expression.Constant(searchString);
            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            MethodCallExpression containsExpression = Expression.Call(propertyExpression, containsMethod, searchValue);
            searchExpressions.Add(containsExpression);
        }

        if (searchExpressions.Any())
        {
            var body = searchExpressions.Aggregate(Expression.OrElse);
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }

        return null;
    }

}
