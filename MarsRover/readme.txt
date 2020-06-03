**********Mars Rover coding challenge************
Console application using .Net Core 2.2
Testing project using NUnit

Assumptions
- Contolling one Rover on Mars within this application 

Design Patterns
- Singleton
- Factory

Problem Description

A rover’s position and location is represented by a combination of x and y co-ordinates and a letter representing one of the four cardinal compass points. The plateau is divided up into a grid to simplify navigation - for example “5 5” would initialise a plateau that is a 5x5 unit square. An example position might be “0, 0, N”, which means the rover is in the bottom left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are ‘L’, ‘R’ and ‘M’. ‘L’ and ‘R’ makes the rover spin 90 degrees left or right respectively, without moving from its current spot. ‘M’ means move forward one grid point, and maintain the same heading.

Test Input:
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM

Expected Output:
1 3 N
5 1 E
