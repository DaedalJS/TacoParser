using System;
using System.Windows;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines.Length}");
            if (lines.Length == 0) { logger.LogError("no lines read"); }
            if (lines.Length == 1) { logger.LogWarning("only one line read"); }
            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS
            logger.LogInfo("Locations Logged");
            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable first = null;
            ITrackable second = null;
            double distance = 0;
            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            // Create a new corA Coordinate with your locA's lat and long
            
            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
            // Create a new Coordinate with your locB's lat and long
            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
            logger.LogInfo("measuring distances from locations");
            foreach (TacoBell x in locations)
            {
                GeoCoordinate taco1 = new GeoCoordinate(x.Location.Latitude, x.Location.Longitude);
                foreach (TacoBell y in locations)
                {
                    GeoCoordinate taco2 = new GeoCoordinate(y.Location.Latitude, y.Location.Longitude);

                    var testDist = taco1.GetDistanceTo(taco2);
                    if (testDist > distance) 
                    {
                        distance = testDist;
                        first = x;
                        second = y;
                        logger.LogInfo("Found new farthest distance, setting new farthest TacoBells.");
                    }
                }
            }

            Console.WriteLine(first.Name + " " + second.Name + " Distance: " + distance);


            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.



        }
    }
}
