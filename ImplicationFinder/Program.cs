using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;


namespace Books.ListMyLibrary
{
    internal class Program
    {
        static IDictionary<string, IList<string>> listOfColumns = new Dictionary<string, IList<string>>()
        {
            { "planet", new List<string>() {
                "Earth",
                "Earth",
                "Earth",
                "Mars",
                "Mars",
                "Mars",
                "Mars",
                "Mars",
                "Mars"
            } },
            { "class", new List<string>() {
                "high",
                "medium",
                "low",
                "high",
                "medium",
                "low",
                "high",
                "medium",
                "low"
            } },
            {"count", new List<string>() {
                "100000",
                "200000",
                "300000",
                "400000",
                "500000",
                "600000",
                "700000",
                "800000",
                "900000"
            } },
            { "continent", new List<string>() {
                "Europe",
                "Europe",
                "Europe",
                "America",
                "America",
                "America",
                "Asia",
                "Asia",
                "Asia"
            } },
            { "country", new List<string>() {
                "German",
                "German",
                "German",
                "USA",
                "USA",
                "Mexico",
                "China",
                "China",
                "Japan"
            } },
            {"city", new List<string>() {
                "Berlin",
                "Berlin",
                "Stuttgart",
                "Washington",
                "San Francisco",
                "Mexico City",
                "Hong Kong",
                "Hong Kong",
                "Tokyo"
            } }
        };

        static void Main(string[] args)
        {

            Console.WriteLine();
            Console.WriteLine();

            var implicationResult = GetLongestImplication(listOfColumns, true);
            Console.WriteLine("Lonely Implication true");
            Console.WriteLine("Longest implication row of column consists of " + implicationResult.Count + " elements");
            Console.WriteLine("There are next order of implication:");


            Console.WriteLine();

            foreach (var implication in implicationResult)
            {
                if (implication != implicationResult.First())
                    Console.Write(" -> ");
                Console.Write(implication);
            }

            Console.WriteLine();
            Console.WriteLine();
             

            implicationResult = GetLongestImplication(listOfColumns);
            Console.WriteLine("Lonely Implication false");
            Console.WriteLine("Longest implication row of column consists of " + implicationResult.Count + " elements");
            Console.WriteLine("There are next order of implication:");


            Console.WriteLine();

            foreach (var implication in implicationResult)
            {
                if (implication != implicationResult.First())
                    Console.Write(" -> ");
                Console.Write(implication);
            }

            Console.WriteLine();
            Console.WriteLine();
        }



        static IList<string> GetLongestImplication(IDictionary<string, IList<string>> table, bool includeLonelyImplications = false)
        {
            var implicationsFrom = GetImplicatesFromRelations(table, includeLonelyImplications);

            var result =  GetImplicationsRow(implicationsFrom).Reverse().ToList();

            return result;
        }

        static IList<string> GetImplicationsRow(IDictionary<string, IList<string>> implicationsFrom, IList<string> currentImplicationRow = null)
        {
            if (currentImplicationRow == null)
            {
                currentImplicationRow = new List<string>();
            }
            var lastColumn = currentImplicationRow.LastOrDefault() ?? "";
            var lastImplicationRow = currentImplicationRow;
            var longestImplicationRow = currentImplicationRow;

            foreach (var implicationsFromElement in implicationsFrom)
            {
                if (currentImplicationRow.Contains(implicationsFromElement.Key))
                    continue;

                if (currentImplicationRow.Count == 0)
                {
                    lastImplicationRow = GetImplicationsRow(implicationsFrom, new List<string>{implicationsFromElement.Key});
                }
                else if(implicationsFrom[lastColumn].Contains(implicationsFromElement.Key))
                {
                    var newList = new List<string>(currentImplicationRow)
                    {
                        implicationsFromElement.Key
                    };
                    lastImplicationRow = GetImplicationsRow(implicationsFrom, newList);
                }

                if(lastImplicationRow.Count > longestImplicationRow.Count)
                {
                    longestImplicationRow = lastImplicationRow;
                }
            }
            return longestImplicationRow;
        }

        static IDictionary<string, IList<string>> GetImplicatesFromRelations(IDictionary<string, IList<string>> table, bool includeLonelyImplications = false)
        {
            IDictionary<string, IList<string>> result = new Dictionary<string, IList<string>>();
            foreach (var implicatableKey in table.Keys)
            {
                IList<string> implicationsList = new List<string>();
                foreach (var initialKey in table.Keys)
                {
                    if (implicatableKey != initialKey && IsRowImplicatesTo(table[initialKey], table[implicatableKey], includeLonelyImplications))
                    {
                        implicationsList.Add(initialKey);
                    }
                }
                result[implicatableKey] = implicationsList;
            }
            return result;
        }

        static bool IsRowImplicatesTo(IList<string> initialRow, IList<string> implicatableRow, bool includeLonelyImplications = false)
        {
            Dictionary<string, string> keyDict = new Dictionary<string, string>();
            bool result = true;
            bool wasAnyRepeat = false;
            for (int i = 0; i < initialRow.Count; i++)
            {
                if (keyDict.ContainsKey(implicatableRow[i]))
                {
                    if (keyDict[implicatableRow[i]] != initialRow[i])
                    {
                        result = false;
                        break;
                    }
                    wasAnyRepeat = true;
                }
                else
                {
                    keyDict[implicatableRow[i]] = initialRow[i];
                }
            }
            return result && (wasAnyRepeat|| includeLonelyImplications);
        }

    }
}