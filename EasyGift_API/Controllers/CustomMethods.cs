
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
namespace EasyGift_API.Controllers
{
    public class CustomMethods<T>
    {
        internal static Dictionary<string, object> fetchPerticularColumns(string[] columns, object model)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            foreach (var column in columns)
            {
                var property = model.GetType().GetProperty(column);

                if (property != null)
                {
                    var convertedValue = model.GetType().GetProperties().Single(u => u.Name == column).GetValue(model);
                    keyValuePairs.Add(column, convertedValue);
                }
                else
                {
                    keyValuePairs.Add("Error", $"Invalid property name: {column} ");
                    return keyValuePairs;
                }
            }
            return keyValuePairs;
        }

        internal static Expression<Func<T, bool>> ConvertToExpression<T>(string expression) 
        {
            // Define the input parameter for the expression
            var parameter = Expression.Parameter(typeof(T), "item");

            try
            {
                // Use the DynamicExpression.ParseLambda method to parse the string expression
                var lambdaExpression = DynamicExpressionParser.ParseLambda(new[] { parameter }, typeof(bool), expression);

                // Convert the parsed expression to a strongly-typed Expression<Func<Product, bool>> using Expression<Func<,>> constructor
                Expression<Func<T, bool>> typedExpression = (Expression<Func<T, bool>>)(lambdaExpression);
                //var typedLambdaExpression = Expression.Lambda<Func<T, bool>>(typedExpression, parameter);
                
                return typedExpression;
            }
            catch (Exception ex)
            {
                // Handle any exception that may occur during parsing, e.g. for invalid expressions
                throw new ArgumentException("Invalid expression", ex);
            }
        }

    }
}
