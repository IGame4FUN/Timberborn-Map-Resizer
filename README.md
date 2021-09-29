# Timberborn Map Resizer
A map resizer for timberborn, fairly efficient, command lines only.  (.NET 5.0)
As an added benefit, it compresses the Map.json file size by about 4 times by removing water maps that can be simulated in-game.  

**How to use:**  
-Copy-paste your map into the same folder as executable re-named as "Map1.json"  
-It will spit out a "Resized Map.json"  

Command | What it does
:---    | :---
-r      | resize values, takes 2 signed integers (ex. -r -5 6)
-s      | shift values, takes 2 signed integers  (ex. -s 5 -6)
-h      | height for added map
-re     | removes entities
-wm     | adds water and moisture maps
-resize | starts the operation

_console commands can look like this:_
"-resize -r 2 -4 -s 6 10"
_like this:_
"-r 5 3 -h -re"
_or like this:_
"-r 3 2"

**Can:**  
-Copy terrain map and entities. (and crop when necesarry)  
-Resize (-/+) the map.  
-Shift the map.  

<p align="center">
  <img src="https://i.redd.it/nghr0wzeq9p71.png">
  <img src="https://i.redd.it/pte3n77s7gp71.png">
  <img src="https://i.redd.it/r2zoqx6tppp71.png">
</p>
