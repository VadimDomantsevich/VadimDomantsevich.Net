using ConsoleTables;
using System.Collections.Generic;

namespace UI.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteCollectionAsTable<T>(this IEnumerable<T> items)
        {
            ConsoleTable.From(items).Write();
        }
    }
}
