using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

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

			Console.WriteLine("Working...");

			// setting resized map size //(MIN: 4x4, MAX: 256x256)
			int map2_size_x = map_size_x + resize_x;
			int map2_size_y = map_size_y + resize_y;

			tm.Singletons.MapSize.Size.X = map2_size_x;
			tm.Singletons.MapSize.Size.Y = map2_size_y;

			string desired_height_string = desired_height.ToString(); // preping height string

			if ((shift_x == 0 && shift_y == 0) || !testing)
            {
				// center map if shift values are default
				shift_x = (map2_size_x - map_size_x) / 2;
				shift_y = (map2_size_y - map_size_y) / 2;

				Console.WriteLine("Centering Map...");
            }
			

			string[] TerrainMap_Heights = tm.Singletons.TerrainMap.Heights.Array.Split(' '); // take TerrainMap heights as substrings

			string[] Resized_TerrainMap = Repo.Resize_TerrainMap(TerrainMap_Heights, map_size_x, map_size_y, map2_size_x, map2_size_y, shift_x, shift_y, desired_height_string); // hellhole #1

			tm.Singletons.TerrainMap.Heights.Array = string.Join(' ', Resized_TerrainMap); // array -> string

			//Console.WriteLine($"{tm.Singletons.TerrainMap.Heights.Array}L");

			/*
			for (int x = 0; x < map2_size_x; x++)
            {
				for (int y = 0; y < map2_size_y; y++)
                {
					Console.Write($"{Resized_TerrainMap[x * map2_size_x + y]} ");
                }
				Console.WriteLine();
            }
			*/

			// water_map, moisture
			if (!water_maps)
			{
				tm.Singletons.WaterMap = null;
				tm.Singletons.SoilMoistureSimulator = null;
			}
            else // implement? maybe xd
            {
				throw new InvalidOperationException("Water Maps processing not implemented yet");
			}


			// entities
			if (remove_entities)
			{
				tm.Entities = null; // deletes all entities
			}
			else
			{
				int end_pos = tm.Entities.Count;
				for (int i = 0; i < end_pos; i++) //shift & crop entities according to new map
				{
					int coord_x = tm.Entities[i].Components.BlockObject.Coordinates.X;
					int coord_y = tm.Entities[i].Components.BlockObject.Coordinates.Y;

					if (!(coord_x + shift_x > map2_size_x && coord_x + shift_x < 0))
						tm.Entities[i].Components.BlockObject.Coordinates.X = coord_x + shift_x;
					else
						tm.Entities[i] = null;
					if (!(coord_y + shift_y > map2_size_y && coord_y + shift_y < 0))
						tm.Entities[i].Components.BlockObject.Coordinates.Y = coord_y + shift_y;
					else
						tm.Entities[i] = null;
				}
			}

			JsonSerializerOptions options = new()
			{
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				
			};

			File.WriteAllText("Resized Map.json", JsonSerializer.Serialize(tm, options));
		}
    }
}
