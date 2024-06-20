using PluralizeService.Core;

namespace CentralValleyBikes.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string Pluralize(this string @this)
        {
            return PluralizationProvider.Pluralize(@this);
        }

        public static string Singularize(this string @this)
        {
            return PluralizationProvider.Singularize(@this);
        }
    }
}
