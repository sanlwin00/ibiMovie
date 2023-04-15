using System.Text.Json;
using System.Text.Json.Nodes;

namespace IbiMovie.WebUI.Shared
{
    public class Utility
    {
        public static string GetPropertyCaseInsensitive(JsonElement element, string propertyName)
        {
            foreach (JsonProperty property in element.EnumerateObject())
            {
                if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    if (property.Value.ValueKind == JsonValueKind.Number)
                        return property.Value.GetUInt64().ToString();
                    else
                        return property.Value.ToString();
                }
            }

            return null; // Return null if the property is not found
        }
    }
}
