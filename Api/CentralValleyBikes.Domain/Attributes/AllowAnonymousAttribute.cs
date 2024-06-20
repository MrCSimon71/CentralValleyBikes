using Microsoft.AspNetCore.Mvc.Authorization;

namespace CentralValleyBikes.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowAnonymous : Attribute, IAllowAnonymousFilter
    {
    }
}
