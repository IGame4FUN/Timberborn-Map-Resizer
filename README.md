# Timberborn Map Resizer
A map resizer for timberborn, very inefficient, command lines only.   

**How to use:**  
-Copy-paste your map into the same folder as executable re-named as "Map1.json"  
-It will spit out a "Resized Map.json"  

**Command structure:**  
Command | What it does
:---    | :---
-r      | resizes map, takes 2 signed integers (ex. -r -5 6)
-s      | shifts map, takes 2 signed integers  (ex. -s 5 -6)
-resize | Starts the operation.

_commands can look like this:_
"-resize -r 2 -4 -s 6 10"
or like this:
"-r 5 3 -resize -s 10 2"

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
