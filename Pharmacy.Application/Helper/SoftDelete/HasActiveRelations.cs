using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Application.Helper.SoftDelete;

public class HasActiveRelations
{
    public static async Task<bool> HasActiveRelationsAsync<T>(T entity, DbContext context) where T : class
    {
        var entry = context.Entry(entity);
        foreach (var navigation in entry.Navigations)
        {
            // Ensure the navigation is loaded
            if (!navigation.IsLoaded)
                await navigation.LoadAsync();

            // If it's a collection, check if it contains any items.
            if (navigation.Metadata.IsCollection)
            {
                if (navigation.CurrentValue is IEnumerable collection && collection.Cast<object>().Any())
                    return true;
            }
            else // For reference navigation properties
            {
                if (navigation.CurrentValue != null)
                    return true;
            }
        }
        return false;
    }

}
