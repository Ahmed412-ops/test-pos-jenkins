using System.ComponentModel;
using Pharmacy.Application.Helper;

namespace Pharmacy.Application.Constants.Permissions.PermissionsLocalization;

   public class LocalizedDescriptionAttribute(string category, string resourceKey) : DescriptionAttribute
    {
        private readonly string _resourceKey = resourceKey;
        private readonly string _category = category;

    public override string Description => CultureHelper.GetResource(_category, _resourceKey);
    }
