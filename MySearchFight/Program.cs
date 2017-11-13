using MySearchFight.SearchEngines;
using MySearchFight.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MySearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.ToString());
            }
        }

        private static void Run(string[] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new ArgumentException("Please enter at least one paremeter");

                var runners = ReadConfiguration().SearchRunners;
                var results = CollectResults(args, runners).Result;
                

                ConsoleHelpers.PrintAsTable(results.Languages, results.Runners, results.Counts);
                

                foreach (var winner in results.Winners)
                    Console.WriteLine("{0} winner: {1}", winner.Key, winner.Value);

                
                Console.WriteLine("Total winner: {0}", results.Winner);

                if (results.Winner != results.NormalizedWinner)
                Console.WriteLine("Normalized winner: {0}", results.NormalizedWinner);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }

        }

        private static async Task<Results> CollectResults(IReadOnlyList<string> languages, IReadOnlyList<ISearchRunner> runners)
        {
            return await Results.Collect(languages, runners);
        }


        private static Configuration ReadConfiguration()
        {
            using (var stream = File.OpenRead("Configuration.xml"))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(Configuration));
                    return (Configuration) serializer.Deserialize(stream);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Problem with configuration file. " + ex.Message);
                }
            }
            
        }

    }
}
