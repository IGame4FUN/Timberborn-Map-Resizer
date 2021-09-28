﻿using System;

namespace TB_Map_Resizer {
    class Methods
    {
        public byte[] Resize_TerrainMap(byte[] terrain_map, int map_size_x, int map_size_y, int map2_size_x, int map2_size_y, int shift_x, int shift_y)
		{   //this entire thing is overly complicated as hell, but kinda efficient for once xd
			byte[,] resized_terrain_map = new byte[map2_size_x , map2_size_y];
			byte[] resized_terrain_map_test = new byte[map2_size_x * map2_size_y];
			int x_start_val = 0;
			int y_start_val = 0;
			int x_end_val = map_size_x;
			int y_end_val = map_size_y;


			if (shift_y < 0) // detects crop on the left
				x_start_val += Math.Abs(shift_y); // crops columns for negative shift value on the left
			if (shift_x < 0) // detects crop on the top (fliped in memory)
				y_start_val += Math.Abs(shift_x); // crops rows for negative shift value on the top (fliped in memory)

			if (shift_y > (map2_size_y - map_size_y)) // detects crop on the right
				x_end_val -= shift_y - (map2_size_y - map_size_y); // crops columns for positive shift value on the right
			if (shift_x > (map2_size_x - map_size_x)) // detects crop on the bottom (fliped in memory)
				y_end_val -= shift_x - (map2_size_x - map_size_x); // crops rows for positive shift value on the bottom (fliped in memory)

			// the loops only go through the cropped version of the original map, so minimal iterations (minimal for going through every element of the array)
			for (int x = x_start_val; x < x_end_val; x++) // rows
            {
				for (int y = y_start_val; y < y_end_val; y++) // columns
                {
					//resized_terrain_map[x - x_start_val + Math.Clamp(shift_y, 0, map2_size_x), y - y_start_val + Math.Clamp(shift_x, 0, map2_size_y)] = terrain_map[x * map_size_x + y]; // fit map into resized map at proper coord
					resized_terrain_map_test[(x - x_start_val + Math.Clamp(shift_y, 0, map2_size_x)) * map2_size_x + (y - y_start_val + Math.Clamp(shift_x, 0, map2_size_y))] = terrain_map[x * map_size_x + y];
					//Console.Write($"{terrain_map[x * map_size_x + y]} "); // display cropped terrain_map
                }
				//Console.WriteLine(); //
			}
			return resized_terrain_map_test; //returns refit, cropped & resized byte[,] map
        }

		public bool Accept_Command(ref int rx, ref int ry, ref int sx, ref int sy, ref bool remove_entities, ref bool wm) //for console use change to "testing = false" in Main()
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