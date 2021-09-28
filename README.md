# Timberborn Map Resizer
A map resizer for timberborn, very inefficient, command lines only.   

**How to use:**  
-Copy-paste your map into the same folder as executable re-named as "Map1.json"  
-It will spit out a "Resized Map.json"  

**Command structure:**  
Command | What it does
:---    | :---
-r      | resizes map from original size, takes 2 signed integers (ex. -r -5 6)
-s      | shifts map from bottom left corner, takes 2 signed integers  (ex. -s 5 -6)
-h      | height for the extended land
-re     | removes entities
-wm     | adds watter and moisture maps (NOT IMPLEMENTED)
-resize | Starts the operation.

_commands can look like this:_
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
