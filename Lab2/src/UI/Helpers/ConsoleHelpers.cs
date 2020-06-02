using ConsoleTables;
using System.Collections.Generic;

namespace UI.View
{
    public static class ConsolePrinter
    {
        public static void WriteCollectionAsTable<T>(this IEnumerable<T> items)
        {
            ConsoleTable.From(items).Write();
        }
    }
}
