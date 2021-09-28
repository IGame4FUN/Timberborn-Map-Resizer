using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TB_Map_Resizer {
    class Program
    {
        static void Main()
        {
            Root tm = JsonSerializer.Deserialize<Root>(File.ReadAllText("Map1.json"));

			Methods Repo = new Methods();

			// getting map size
			int map_size_x = tm.Singletons.MapSize.Size.X; //(MIN: 4x4, MAX: 256x256)
			int map_size_y = tm.Singletons.MapSize.Size.Y; //(MIN: 4x4, MAX: 256x256)

			// setting default resize and shift values
			int resize_x = 0;
			int resize_y = 0;

			int shift_x = 0;
			int shift_y = 0;

			// console stuff
			bool start = false;
			bool remove_entities = false;
			bool testing = true;
			bool water_maps = false;

			// read console input if testing == false;
			if (!testing)
				while (!start) // reading console commands (-r x y, -s x y, -re, -wm) until method returns true (-resize)
					start = Repo.Accept_Command(ref resize_x, ref resize_y, ref shift_x, ref shift_y, ref remove_entities, ref water_maps);

			Console.WriteLine("Working...");

			// setting resized map size
			int map2_size_x = map_size_x + resize_x; //(MIN: 4x4, MAX: 256x256)
			int map2_size_y = map_size_y + resize_y; //(MIN: 4x4, MAX: 256x256)


			if ((shift_x == 0 && shift_y == 0) || !testing)
            {
				// center map if shift values are default
				shift_x = (map2_size_x - map_size_x) / 2;
				shift_y = (map2_size_y - map_size_y) / 2;

				Console.WriteLine("Centering Map...");
            }
			

			string[] TerrainMap_Substrings = tm.Singletons.TerrainMap.Heights.Array.Split(' '); // take TerrainMap heights as substrings

			// converts the array of substrings into a IEnumerable<byte> (".Select(x => Convert.ToByte(x))") and then to a byte array (".ToArray()")
			// (why do docs have 2 args for .Select() lol)
			byte[] TerrainMap_Heights = TerrainMap_Substrings.Select(xy => Convert.ToByte(xy)).ToArray();

			byte[,] Resized_TerrainMap = new byte[map2_size_x, map2_size_y]; // preping resized map

			//Console.WriteLine("\ncropped terrain_map -->\n");

			byte[] Resized_TerrainMap_Test = new byte[map2_size_x * map2_size_y]; //test

			Resized_TerrainMap_Test = Repo.Resize_TerrainMap(TerrainMap_Heights, map_size_x, map_size_y, map2_size_x, map2_size_y, shift_x, shift_y); // hellhole #1
			
			tm.Singletons.TerrainMap.Heights.Array = string.Join(' ', Resized_TerrainMap_Test); // array -> string

			//Console.WriteLine($"{tm.Singletons.TerrainMap.Heights.Array}L");

			/*
			for (int x = 0; x < map2_size_x; x++)
            {
				for (int y = 0; y < map2_size_y; y++)
                {
					Console.Write($"{Resized_TerrainMap_Test[x * map2_size_x + y]} ");
                }
				Console.WriteLine();
            }
			*/

			// water_map, outflows, moisture
			if (!water_maps)
			{
				tm.Singletons.WaterMap.WaterDepths.Array = null;
				tm.Singletons.WaterMap.Outflows.Array = null;
				tm.Singletons.SoilMoistureSimulator.MoistureLevels.Array = null;
			}
            else // implement? maybe xd
            {
				throw new InvalidOperationException("Water Maps processing not implemented yet");
			}


			// entities
			if (remove_entities)
			{
				tm.Entities.Clear(); // deletes all entities
			}
			else
			{
				foreach (var obj in tm.Entities) //shift & crop entities according to new map
				{
					int coord_x = obj.Components.BlockObject.Coordinates.X;
					int coord_y = obj.Components.BlockObject.Coordinates.Y;

					if (!(coord_x + shift_x > map2_size_x || coord_x + shift_x < 0))
						obj.Components.BlockObject.Coordinates.X = coord_x + shift_x;
					if (!(coord_y + shift_y > map2_size_y || coord_y + shift_y < 0))
						obj.Components.BlockObject.Coordinates.Y = coord_y + shift_y;

				}
			}

			File.WriteAllText("Resized Map3.json", JsonSerializer.Serialize(tm));
		}
    }
}
