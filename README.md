# Timberborn Map Resizer
A map resizer for timberborn, very inefficient, command lines only.   

**How to use:**  
-Copy-paste your map into the same folder as executable re-named as "Map1.json"  
-It will spit out a "Resized Map.json"  

**Command structure:**  

Command | What it does
------------- | -------------
-r  | takes [x] and [y] (ex. -r 5 6)
-s  | takes [x] and [y] (ex. -s -4 5

Comand List: -r, -s, -resize;

"-r _x y_"  
ex: "-r 2 -4"  
resize the map by inputted amounts (can be negative), if the new map size is too big or too small it adjusts to limits  

"-s _x y_"  
ex: "-s 6 10"  
shift the map by inputted amount (can be negative)  

"-resize"
Starts the operation after reading entire inputted command line

commands can look like this:
"-resize -r 2 -4 -s 6 10"
or like this:
"-r 2 -4 -resize -s 6 10"

**Can:**  
-Copy terrain, water and moisture maps.  
-Copy entities.  
-Resize (-+) the map.  
-Shift the map.  

<p align="center">
  <img src="https://i.redd.it/nghr0wzeq9p71.png">
  <img src="https://i.redd.it/pte3n77s7gp71.png">
  <img src="https://i.redd.it/r2zoqx6tppp71.png">
</p>
