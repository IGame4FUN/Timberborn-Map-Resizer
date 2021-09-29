using System;

namespace TB_Map_Resizer {
    class Methods
    {
        public string[] Resize_TerrainMap(string[] terrain_map, int map_size_x, int map_size_y, int map2_size_x, int map2_size_y, int shift_x, int shift_y, string desired_height_string)
		{   //this thing is somewhat complicated but fairly efficient, petname: Hellhole #1
			string[] Resized_TerrainMap = new string[map2_size_x * map2_size_y];
			Array.Fill(Resized_TerrainMap, desired_height_string);

			int x_start_val = 0;
			int y_start_val = 0;

			int x_end_val = map_size_x;
			int y_end_val = map_size_y;
			
			if (shift_x < 0)
				y_start_val -= shift_x; // cuts top
			if (shift_y < 0)
				x_start_val -= shift_y; // cuts left

			int space_right = map2_size_x - (map_size_x + shift_x);
			int space_bottom = map2_size_y - (map_size_y + shift_y);
			
			if (space_right < 0)
				y_end_val += space_right; // cuts right
			if (space_bottom < 0)
				x_end_val += space_bottom; // cuts bottom

			for (int x = x_start_val; x < x_end_val; x++) // row
            {
				for (int y = y_start_val; y < y_end_val; y++) //column
                {
					Resized_TerrainMap[(x + shift_y) * map2_size_x + (y + shift_x)] = terrain_map[x * map_size_x + y];
					//Console.Write($"{terrain_map[x * map_size_x + y]} ");
				}
				//Console.WriteLine();
			}

			/*
			if (shift_y < 0) // detects crop on the left
				x_start_val += Math.Abs(shift_y);
			if (shift_x < 0) // detects crop on the top (fliped in memory)
				y_start_val += Math.Abs(shift_x);

			if (shift_y > (map2_size_y - map_size_y)) // detects crop on the right
				x_end_val -= shift_y - (map2_size_y - map_size_y);
			if (shift_x > (map2_size_x - map_size_x)) // detects crop on the bottom (fliped in memory)
				y_end_val -= shift_x - (map2_size_x - map_size_x);

			int clamped_shift_x = Math.Clamp(shift_y, 0, map2_size_y); //clamping for crop stuff
			int clamped_shift_y = Math.Clamp(shift_x, 0, map2_size_x);

			// quick loop?
			for (int x = x_start_val; x < x_end_val; x++) // rows
            {
				for (int y = y_start_val; y < y_end_val; y++) // columns
                {
					Resized_TerrainMap[(x - x_start_val + clamped_shift_x) * map2_size_x + (y - y_start_val + clamped_shift_y)] = terrain_map[x * map_size_x + y];
                }
			}
			*/
			return Resized_TerrainMap; //returns refit, cropped & resized string[] map
        }

		public bool Accept_Command(ref int rx, ref int ry, ref int sx, ref int sy, ref int h, ref bool remove_entities, ref bool wm) //for console use change to "testing = false" in Main()
		{
			string input_line = Console.ReadLine();
			string[] command = input_line.Split(' ');
			bool start = false;

			for (int i = 0; i < command.Length; i++)
			{
				//Console.WriteLine(command[i][0]);
				//Console.WriteLine($"command[i] : \"{command[i]}\"");
				if (command[i][0] == '-')
				{
					switch (command[i])
					{
						case "-r":
							Console.WriteLine("Resize Detected");
							if (!int.TryParse(command[i + 1], out rx))
								Console.WriteLine("Invalid x resize value");
							if (!int.TryParse(command[i + 2], out ry))
								Console.WriteLine("Invalid y resize value");
							break;

						case "-s":
							Console.WriteLine("Shift Detected");
							if (!int.TryParse(command[i + 1], out sx))
								Console.WriteLine("Invalid x shift value");
							if (!int.TryParse(command[i + 2], out sy))
								Console.WriteLine("Invalid y shift value");
							break;

						case "-h":
							if (!int.TryParse(command[i + 1], out h))
								Console.WriteLine($"Invalid height value");
							break;

						case "-re":
							Console.WriteLine("Entities Removed");
							remove_entities = true;
							break;

						case "-resize":
							Console.WriteLine("Start Detected");
							start = true;
							break;

						case "-wm":
							Console.WriteLine("Water Maps Added");
							wm = true;
							break;

						default:
							Console.WriteLine("Invalid Command");
							break;
					}
				}
			}
			return start;
		}
	}
}
