using System.Linq.Expressions;
using App.service.cache;

namespace App.service.interpolator;

public class InterpolatorImpl : IInterpolator
{
    private readonly StringFormatterCache _cache = new();

    public string Interpolate(string value, object obj)
    {
        var field = obj.GetType() + "." + value;
        return _cache.IsExistElement(field)
            ? InterpolateExistFunc(field, obj)
            : InterpolateNotExistFunc(value, field, obj);
    }

    private string InterpolateExistFunc(string field, object obj)
    {
        return _cache.GetElement(field)(obj);
    }

    private string InterpolateNotExistFunc(string value, string key, object obj)
    {
        var param = Expression.Parameter(typeof(object));
        var field = Expression.PropertyOrField(Expression.TypeAs(param, obj.GetType()), value);
        var callExpression = Expression.Call(field, "ToString", null, null);
        var expression = Expression.Lambda<Func<object, string>>(callExpression, param);
        var result = expression.Compile();
        _cache.AddElement(key, result);
        return result(obj);
    }
}