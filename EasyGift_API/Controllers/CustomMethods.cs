namespace EasyGift_API.Controllers
{
    public class CustomMethods
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
    }
}
