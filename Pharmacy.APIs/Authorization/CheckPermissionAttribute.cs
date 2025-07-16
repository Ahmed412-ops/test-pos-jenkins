namespace Pharmacy.APIs.Authorization;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class CheckPermissionAttribute(string permission) : Attribute
{
    public string Permission { get; } = permission;
}
