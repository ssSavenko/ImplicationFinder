using System;
using System.IO;
using System.Net;
using System.Threading;
 

namespace Books.ListMyLibrary
{
    internal class Program
    {
        static  IDictionary<string, IList<string>> listOfColumns = new Dictionary<string, IList<string>>()
        {
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

            GetImplicatesFromRelations(listOfColumns);

        }


        static int GetLongestImplication(IDictionary<string, IList<string>> table)
        {

            return 0;
        }

        static IDictionary<string, IList<string>> GetImplicatesFromRelations(IDictionary<string, IList<string>> table)
        {
            IDictionary<string, IList<string>> result = new Dictionary<string, IList<string>>(); 
            foreach (var implicatableKey in table.Keys)
            {
                IList<string> implicationsList = new List<string>();
                foreach (var initialKey in table.Keys)
                {
                    if (implicatableKey != initialKey &&  IsRowImplicatesTo(table[initialKey], table[implicatableKey]))
                    {
                        implicationsList.Add(initialKey);
                    } 
                } 
                result[implicatableKey]= implicationsList;
            }
            return result;  
        }

        static bool IsRowImplicatesTo(IList<string> initialRow, IList<string> implicatableRow)
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
            return result && wasAnyRepeat;
        }

    }
}