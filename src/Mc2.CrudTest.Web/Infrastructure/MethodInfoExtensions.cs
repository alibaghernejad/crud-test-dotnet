using System.Reflection;
using Ardalis.GuardClauses;

namespace Mc2.CrudTest.Web.Infrastructure;

public static class MethodInfoExtensions
{
    public static bool IsAnonymous(this MethodInfo method)
    {
        var invalidChars = new[] { '<', '>' };
        return method.Name.Any(invalidChars.Contains);
    }

    public static void AnonymousMethod(this IGuardClause guardClause, Delegate input)
    {
        if (IsAnonymous(input.Method))
            throw new ArgumentException("The endpoint name must be specified when using anonymous handlers.");
    }
}