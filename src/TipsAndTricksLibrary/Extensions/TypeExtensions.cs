namespace TipsAndTricksLibrary.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class TypeExtensions
    {
        public static Dictionary<Type, Func<object, TTo>> GetAllConversionsIn<TTo>()
        {
            var destinationType = typeof(TTo);
            var availableConversions = new Dictionary<Type, Func<object, TTo>>();

            var conversionsDefinitions = destinationType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.IsSpecialName && x.ReturnType == destinationType && x.GetParameters().Length == 1);

            var objectType = typeof(object);
            foreach (var definition in conversionsDefinitions)
            {
                var convertibleType = definition.GetParameters().Single().ParameterType;

                var parameter = Expression.Parameter(objectType, "x");
                var converter = Expression.Lambda(
                        Expression.Convert(
                            Expression.Convert(parameter, convertibleType),
                            destinationType,
                            definition),
                        parameter)
                    .Compile();

                availableConversions.Add(convertibleType, (Func<object, TTo>)converter);
            }

            return availableConversions;
        }
    }
}