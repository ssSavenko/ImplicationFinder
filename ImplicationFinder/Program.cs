using System;
using System.IO;
using System.Net;
using System.Threading;
 

namespace Books.ListMyLibrary
{
    internal class Program
    {
        IList<IList<string>> listOfColumns = new List<IList<string>>()
        {
            new List<string>() {"class",
                "high",
                "medium",
                "low",
                "high",
                "medium",
                "low",
                "high",
                "medium",
                "low"
            },
            new List<string>() {"count",
                "100000",
                "200000",
                "300000",
                "400000",
                "500000",
                "600000",
                "700000",
                "800000",
                "900000"
            },
            new List<string>() {"continent",
                "Europe",
                "Europe",
                "Europe",
                "America",
                "America",
                "America",
                "Asia",
                "Asia",
                "Asia"
            },
            new List<string>() {"country",
                "German",
                "German",
                "German",
                "USA",
                "USA",
                "Mexico",
                "China",
                "China",
                "Japan"
            },
            new List<string>() {"city",
                "Berlin",
                "Berlin",
                "Stuttgart",
                "Washington",
                "San Francisco",
                "Mexico City",
                "Hong Kong",
                "Hong Kong",
                "Tokyo"
            }
        };  

        static void Main(string[] args)
        {



        }

    }
}