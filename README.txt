USAGE: 
Run the program normally. It will ask for a rotation angle (which must be a multiple of 90), then it will ask for a tile size (which must be greater than 0), and then it will ask for the number of times to tesselate the tile (which must be greater than 0). Entering an invalid value for any of these will result in the program rejecting the value and asking for it to be reentered. Finally, it will ask for the tile itself, at which point the user must enter n lines of n characters (n being the tile size that was previously entered). If at any point the user enters a line of invalid length, the program will prompt the user to reenter that line.

Once all necessary data has been entered, the program will spit out the resultant tesselated tiles, at which point the user can exit the program by pressing Enter.

To quit the program at any other time, close the console or quit the program through Visual Studio. At any point prior to submitting the tile contents, you can also enter \q to quit out. The reason why you cannot do this while submitting tile contents is because a 2x2 tile could legitimately contain \q as its contents.

---

REDDIT SOURCE:
https://www.reddit.com/r/dailyprogrammer/comments/8ylltu/20180713_challenge_365_hard_tessellations_and/


PROBLEM DESCRIPTION:
[2018-07-13] Challenge #365 [Hard] Tessellations and Tilings


Description

A Tessellation (or Tiling) is the act of covering a surface with a pattern of flat shapes so that there are no overlaps or gaps. Tessellations express fascinating geometric and symmetric properties as art, and famously appear in Islamic art with four, five, and six-fold regular tessellations.

Today we'll your challenge is to write a program that can do basic regular tessellations in ASCII art.
Input Description

You'll be given an integer on the first line, which can be positive or negative. It tells you the rotation (relative to clockwise, so 180, 90, 0, or -90) to spin the tile as you tessellate it. The next line contains a single integer that tells your program how many columns and rows to read (assume it's a square). Then the next N rows contain the pattern of the tile in ASCII art.

Example:

90
4
####
#--#
#++#
####

Output Description

Your program should emit a tessellation of the tile, with the rotation rules applied, repeated at least two times in both the horizontal and vertical directions, you can do more if you wish. For the above:

########
#--##+|#
#++##+|#
########
########
#+|##++#
#+|##--#
########

Challenge Input

90
6
/\-/|-
/\/-\/
||\\-\
|\|-|/
|-\|/|
|\-/-\

180
6
&`{!#;
#*#@+#
~/}}?|
'|(==]
\^)~=*
|?|*<%

Bonus

Feel free to come up with some fun designs you can feed your program.

Feel free, also, to do this not with ASCII art but ANSI or even graphics.
