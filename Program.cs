using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TB_Map_Resizer {
    class Program {
        static void Main()
        {

            string filename = "Map1.json";

            if (!File.Exists(filename))
            {
                Console.WriteLine("File Map1.json doesn't exist in folder." +
                    "\nPress \"Enter\" to exit.");

                Console.ReadKey();
                return;
            }

            Root tm = JsonSerializer.Deserialize<Root>(File.ReadAllText(filename));

            Methods Repo = new Methods();

            // getting map size
            int map_size_x = tm.Singletons.MapSize.Size.X; //(MIN: 4x4, MAX: 256x256)
            int map_size_y = tm.Singletons.MapSize.Size.Y;

            // setting default resize, shift values and desired height
            int resize_x = 0;
            int resize_y = 0;

            int shift_x = 0;
            int shift_y = 0;

            int desired_height = 0;

            // console stuff
            bool start = false;
            bool remove_entities = false;
            bool testing = false;
            bool water_maps = false;

            // read console input if testing == false;
            if (!testing)
                while (!start) // reading console commands (-r x y, -s x y, -h, -re, -wm) until method returns true (-resize)
                    start = Repo.Accept_Command(ref resize_x, ref resize_y, ref shift_x, ref shift_y, ref desired_height, ref remove_entities, ref water_maps);

            Console.WriteLine("Start Timer");
            var timer = Microsoft.VisualBasic.DateAndTime.Timer; //timer
            Console.WriteLine("Working...");

            // setting resized map size //(MIN: 4x4, MAX: 256x256)
            int map2_size_x = map_size_x + resize_x;
            int map2_size_y = map_size_y + resize_y;

            map2_size_x = Math.Clamp(map2_size_x, 4, 256);
            map2_size_y = Math.Clamp(map2_size_y, 4, 256);

            tm.Singletons.MapSize.Size.X = map2_size_x;
            tm.Singletons.MapSize.Size.Y = map2_size_y;

            if ((shift_x == 0 && shift_y == 0) || !testing)
            {
                // center map if shift values are default
                shift_x = Math.Clamp((map2_size_x - map_size_x) / 2, 0, map2_size_x);
                shift_y = Math.Clamp((map2_size_y - map_size_y) / 2, 0, map2_size_y);

                Console.WriteLine("Centering Map...");
            }

            string desired_string = desired_height.ToString(); // preping string

            //resize terrain map
            string[] TerrainMap_Heights = tm.Singletons.TerrainMap.Heights.Array.Split(' '); // take TerrainMap heights as substrings array
            string[] Resized_TerrainMap = Repo.Resize_Map(TerrainMap_Heights, map_size_x, map_size_y, map2_size_x, map2_size_y, shift_x, shift_y, desired_string); // hellhole #1

            tm.Singletons.TerrainMap.Heights.Array = string.Join(' ', Resized_TerrainMap); // array -> string

            if (water_maps == true) // resize water maps
            {
                desired_string = "0";

                string[] WaterMap = tm.Singletons.WaterMap.WaterDepths.Array.Split(' ');
                string[] Resized_WaterMap = Repo.Resize_Map(WaterMap, map_size_x, map_size_y, map2_size_x, map2_size_y, shift_x, shift_y, desired_string);

                string[] MoistureMap = tm.Singletons.SoilMoistureSimulator.MoistureLevels.Array.Split(' ');
                string[] Resized_MoistureMap = Repo.Resize_Map(MoistureMap, map_size_x, map_size_y, map2_size_x, map2_size_y, shift_x, shift_y, desired_string);

                desired_string = "0:0:0:0";

                string[] OutflowsMap = tm.Singletons.WaterMap.Outflows.Array.Split(' ');
                string[] Resized_OutflowsMap = Repo.Resize_Map(OutflowsMap, map_size_x, map_size_y, map2_size_x, map2_size_y, shift_x, shift_y, desired_string);

                tm.Singletons.WaterMap.WaterDepths.Array = string.Join(' ', Resized_WaterMap);
                tm.Singletons.WaterMap.Outflows.Array = string.Join(' ', Resized_OutflowsMap);
                tm.Singletons.SoilMoistureSimulator.MoistureLevels.Array = string.Join(' ', Resized_MoistureMap);
            }
            else // delete water maps
            {
                tm.Singletons.WaterMap = null;
                tm.Singletons.SoilMoistureSimulator = null;
            }

            /* CONSOLE TESTING
            Console.WriteLine("\nResized\n");
            for (int x = 0; x < map2_size_y; x++) 
            {
                for (int y = 0; y < map2_size_x; y++) 
                {
                    Console.Write($"{Resized_TerrainMap[x * map2_size_x + y]} ");
                }
                Console.WriteLine();
            }
            */

            if (!remove_entities) // shifts X & Y of Entities
            {
                for (int i = tm.Entities.Count - 1; i >= 0; i--)
                {
                    int coord_x = tm.Entities[i].Components.BlockObject.Coordinates.X;
                    int coord_y = tm.Entities[i].Components.BlockObject.Coordinates.Y;

                    if (coord_x + shift_x > map2_size_x || coord_x + shift_x < 0)
                        tm.Entities.RemoveAt(i);
                    else
                        tm.Entities[i].Components.BlockObject.Coordinates.X = coord_x + shift_x;
                    if (coord_y + shift_y > map2_size_y || coord_y + shift_y < 0)
                        tm.Entities.RemoveAt(i);
                    else
                        tm.Entities[i].Components.BlockObject.Coordinates.Y = coord_y + shift_y;
                }
            }
            else
            {
                tm.Entities = null; // deletes all entities
            }
            
            JsonSerializerOptions options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            File.WriteAllText("Resized Map.json", JsonSerializer.Serialize(tm, options));

            string timer_end = String.Format("{0:0.000}", Microsoft.VisualBasic.DateAndTime.Timer - timer);

            Console.WriteLine($"\nDone, press \"Enter\" to Exit. (Time Elapsed: {timer_end}s)");
            Console.ReadKey();
        }
    }
}
