using System.Linq;

namespace IntegrationTest.Extensions
{
    public static class IQueryableExtension
    {
        public static int Count(this IQueryable query)
        {
            int count = 0;
            foreach (var o in query)
                count++;
            return count;
        }
    }
}
