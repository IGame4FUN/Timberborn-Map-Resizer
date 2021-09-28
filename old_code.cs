using System;

namespace Timberborn_Map_Resizer
{
	class Program
	{
		//Public variables
		public static int map_size_x = 0;
		public static int map_size_y = 0;
		public static int map2_size_x = 0;
		public static int map2_size_y = 0;

		// ---> User Inputs
		public static int shift_x = 0; //*can be negative, inf range
		public static int shift_y = 0; //*can be negative, inf range
		public static int desired_height = 0; // between 0 and 16
		public static int resize_x = 128; //*can be negative, cannot be resized outside: MIN: 4x4, MAX: 256x256
		public static int resize_y = 128; //*can be negative, cannot be resized outside: MIN: 4x4, MAX: 256x256
										  // <--- User Inputs

		// ---> text references
		public const string terrain_map_text = "\"TerrainMap\":{\"Heights\":{\"Array\":\"";
		public const string camera_restorer_text = "\"CameraStateRestorer\":{\"SavedCameraState\":{\"Target\":{";
		public const string water_map_text = "\"WaterMap\":{\"WaterDepths\":{\"Array\":\"";
		public const string outflows_text = "\"Outflows\":{\"Array\":\"";
		public const string soil_moisture_simulator_text = "\"SoilMoistureSimulator\":{\"MoistureLevels\":{\"Array\":\"";
		public const string entities_text = "\"Entities\":";
		public const string map_size_text_x = "\"X\":";
		public const string map_size_text_y = "\"Y\":";
		// <--- text references

		static int Get_XY(ref string map_data, string map_size_text, int start_index = 0) //sketchy way to get map sizes
		{
			int map_size_text_start = map_data.IndexOf(map_size_text, start_index) + map_size_text.Length;

			int map_size_xy = 0;
			int na = 1;
			int a;

			for (int i = 2; i >= 0; i--)
			{
				a = map_data[map_size_text_start + i];

				if (a >= '0' && a <= '9')
				{
					map_size_xy += (a - '0') * na;
					na *= 10;
				}
			}
			return map_size_xy;
		}

		static int Start_Pos(ref string map_data, string map_var_text) //gets usable start_pos
		{
			int map_var_text_start = map_data.IndexOf(map_var_text) + map_var_text.Length;

			return map_var_text_start;
		}

		static int End_Pos(ref string map_data, string map_var_text) //gets usable end_pos
		{
			int map_var_text_end = map_data.IndexOf(map_var_text);

			for (int i = map_var_text_end; i > 0; i--)
			{
				char a = map_data[i];
				if (a >= '0' && a <= '9')
				{
					map_var_text_end = i;
					i = 0;
				}
			}

			map_var_text_end += 1;

			return map_var_text_end;
		}

		static double[,] To_Array2d(ref string[] terrain_map_substrings, int local_map_size_x, int local_map_size_y) //converts substring list to double[,] in specified sizes
		{
			//write int[,] terrain_map_heights[,]
			double[,] terrain_map_heights = new double[local_map_size_x, local_map_size_y];
			int pos1 = 0;

			for (int y = 0; y < local_map_size_y; y++)
			{
				for (int x = 0; x < local_map_size_x; x++)
				{
					var sub = terrain_map_substrings[pos1++];
					if (double.TryParse(sub, out double terrain_map_height_int))
					{
						terrain_map_heights[x, y] = terrain_map_height_int;
					}
					else
						throw new Exception("Failed to parse (double) from substring");
					//Console.Write($"{terrain_map_heights[x, y]} ");
				}
				//Console.WriteLine("");
			}

			return terrain_map_heights;
		}

		static string Crop_Terrain(ref double[,] terrain_map_heights, int desired_height2) //transfers old map data to new resized map
		{
			//read int[,] terrain_map2_heights[,]
			double[,] terrain_map2_heights = new double[map2_size_x, map2_size_y];
			string terrain_map2 = "";

			//converts array back into string using new sizes and position
			for (int y = 0; y < map2_size_y; y++)
			{
				for (int x = 0; x < map2_size_x; x++)
				{
					if (x >= shift_x && x < map_size_x + shift_x && y >= shift_y && y < map_size_y + shift_y)
					{
						terrain_map2_heights[x, y] = terrain_map_heights[x - shift_x, y - shift_y];
					}
					else
					{
						terrain_map2_heights[x, y] = desired_height2;
					}
					terrain_map2 += $"{terrain_map2_heights[x, y]} ";
					//Console.Write($"{terrain_map2_heights[x, y]} ");
				}
				//Console.WriteLine("");
			}

			return terrain_map2; //returns string in usable format
		}

		static double[,] Rewrite_Map(ref string map_data, string map_text_index, string map_text2_index, ref int start_pos, ref int end_pos) // re-writing map data
		{

			start_pos = Start_Pos(ref map_data, map_text_index);
			end_pos = End_Pos(ref map_data, map_text2_index);

			string map = map_data[start_pos..end_pos];
			map_data = map_data.Remove(start_pos, end_pos - start_pos);

			string[] map_substrings = map.Split(' ');

			double[,] map_heights = To_Array2d(ref map_substrings, map_size_x, map_size_y);

			return map_heights;
		}
		static bool Accept_Command(ref int rx, ref int ry, ref int sx, ref int sy, ref bool re)
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
							Console.WriteLine("Resize detected");
							if (!int.TryParse(command[i + 1], out rx))
								Console.WriteLine("Invalid x resize value");
							if (!int.TryParse(command[i + 2], out ry))
								Console.WriteLine("Invalid y resize value");
							break;

						case "-s":
							Console.WriteLine("Shift detected");
							if (!int.TryParse(command[i + 1], out sx))
								Console.WriteLine("Invalid x shift value");
							if (!int.TryParse(command[i + 2], out sy))
								Console.WriteLine("Invalid y shift value");
							break;

						case "-re":
							Console.WriteLine("Entities Removed");
							re = true;
							break;

						case "-resize":
							Console.WriteLine("Start detected");
							start = true;
							break;

						case "t":
							break;
						case "w":
							break;
						case "m":
							break;

						default:
							Console.WriteLine("Invalid command");
							break;
					}
				}
			}
			return start;
		}

		static string Write_Empty(ref string map_data, int size_x, int size_y, string empty_val = "0 ")
        {
			string empty_row = "";
			string empty_map = "";

			for (int x = 0; x < size_x; x++)
            {
				empty_row += empty_val;
			}
			for (int y = 0; y < size_y; y++)
			{
				empty_map += empty_row;
			}

			empty_map = empty_map.Remove(empty_map.Length - 1, empty_map.Length);

			return empty_map; //returns string filled with empty values
		}

		static void Serializer_Kindof(System.IO.StreamReader sr)
        {
			char c = '0';
			string word = "";

			while (c != '{' || c != '}')
            {
				c = (char)sr.Read();
				if (c == '\"')
                {
					while (c != '\"')
                    {
						word += (char)sr.Read();
					}
                }
			}
        }

		//rudimentary_json_deserializer ()
		static void Main()
		{
			// read file in Memory and get text length ->
			string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			string file = dir + @"\Map1.json";
			string map_data = System.IO.File.ReadAllText(file);

			// getting map size ->
			map_size_x = Get_XY(ref map_data, map_size_text_x); //(MIN: 4x4, MAX: 256x256)
			map_size_y = Get_XY(ref map_data, map_size_text_y); //(MIN: 4x4, MAX: 256x256)

			switch (resize_x)
			{
				case < 4:
					break;
				case > 256:
					break;
			}

			//center map ->
			shift_x = (map2_size_x - map_size_x) / 2;
			shift_y = (map2_size_y - map_size_y) / 2;

			bool re = false;
			bool start = false;

			while (!start)
				start = Accept_Command(ref resize_x, ref resize_y, ref shift_x, ref shift_y, ref re);

			// resizing ->
			map2_size_x = map_size_x + resize_x; //(MIN: 4x4, MAX: 256x256)
			map2_size_y = map_size_y + resize_y; //(MIN: 4x4, MAX: 256x256)

			Console.WriteLine("Working...");

			int index_temp = Start_Pos(ref map_data, map_size_text_x);

			map_data = map_data.Remove(index_temp, Convert.ToString(map_size_x).Length);
			map_data = map_data.Insert(index_temp, Convert.ToString(map2_size_x));

			index_temp = Start_Pos(ref map_data, map_size_text_y);

			map_data = map_data.Remove(index_temp, Convert.ToString(map_size_y).Length);
			map_data = map_data.Insert(index_temp, Convert.ToString(map2_size_y));

			int start_pos = 0;
			int end_pos = 0;
			string map;
			double[,] map2;

			Console.WriteLine("terrain_map");
			map2 = Rewrite_Map(ref map_data, terrain_map_text, camera_restorer_text, ref start_pos, ref end_pos);

			map = Crop_Terrain(ref map2, desired_height);
			map = map.Remove(map.Length - 1, 1);
			map_data = map_data.Insert(start_pos, map);

			Console.WriteLine("water_map");
			map2 = Rewrite_Map(ref map_data, water_map_text, outflows_text, ref start_pos, ref end_pos);

			map = Crop_Terrain(ref map2, 0);
			map = map.Remove(map.Length - 1, 1);
			map_data = map_data.Insert(start_pos, map);

			Console.WriteLine("moisture_map");
			map2 = Rewrite_Map(ref map_data, soil_moisture_simulator_text, entities_text, ref start_pos, ref end_pos);

			map = Crop_Terrain(ref map2, 0);
			map = map.Remove(map.Length - 1, 1);
			map_data = map_data.Insert(start_pos, map);

			//temporarily clearing the overflow
			string temp_overflow_text = "0:0:0:0 ";
			string temp_overflow_text2 = "";
			string temp_overflow_text3 = "";

			for (int i = 0; i < map2_size_x; i++)
			{
				temp_overflow_text2 += temp_overflow_text;
			}
			for (int y = 0; y < map2_size_y; y++)
			{
				temp_overflow_text3 += temp_overflow_text2;
			}

			temp_overflow_text3 = temp_overflow_text3.Remove(temp_overflow_text3.Length - 1, 1);

			start_pos = Start_Pos(ref map_data, outflows_text);
			end_pos = End_Pos(ref map_data, soil_moisture_simulator_text);

			map_data = map_data.Remove(start_pos, end_pos - start_pos);
			map_data = map_data.Insert(start_pos, temp_overflow_text3); //adds enough empty overflows


			start_pos = Start_Pos(ref map_data, entities_text);
			end_pos = map_data.Length;

			if (re)
			{
				map_data = map_data.Remove(start_pos, end_pos - start_pos);
				map_data = map_data.Insert(start_pos, "[]}"); //just clears entities
			}
			else
			{

				//suuuper sketchy shifting of entities ->
				bool go = true;
				int safety_net = 0;
				int coord_text_length = map_size_text_x.Length;
				int coord_xy;
				string temp_text = "\"Coordinates\":";

				Console.WriteLine("Entities");

				while (go == true) //extremely sketchy
				{
					start_pos = map_data.IndexOf(temp_text, start_pos);
					if (start_pos == -1)
					{
						go = false;
					}
					else
					{
						//modify X in string ->
						start_pos = map_data.IndexOf(map_size_text_x, start_pos);
						coord_xy = Get_XY(ref map_data, map_size_text_x, start_pos);

						map_data = map_data.Remove(start_pos + coord_text_length, Convert.ToString(coord_xy).Length);
						coord_xy += shift_x;

						map_data = map_data.Insert(start_pos + coord_text_length, Convert.ToString(coord_xy));
						start_pos += 4;

						//modify Y in string ->
						start_pos = map_data.IndexOf(map_size_text_y, start_pos);
						coord_xy = Get_XY(ref map_data, map_size_text_y, start_pos);

						map_data = map_data.Remove(start_pos + coord_text_length, Convert.ToString(coord_xy).Length);
						coord_xy += shift_y;

						map_data = map_data.Insert(start_pos + coord_text_length, Convert.ToString(coord_xy));
						start_pos += 4;
					}

					if (safety_net == 1000000)
						go = false;
					safety_net++;
				}
			}

			System.IO.File.WriteAllText("Resized Map.json", map_data);
			Console.WriteLine();
			Console.WriteLine("Done (Press \"Enter\" to exit)");
			Console.ReadKey();
		}
	}
}
