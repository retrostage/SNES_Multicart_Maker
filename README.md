# SNES Multi-Cart Maker

A simple tool to make multi-cart games for the SNES Blaster cartridge.

This tool is super early alpha stages, so there may (will) be bugs.

This tool is specific to the SNES Blaster product, it will not work with other multi-cart capable SNES cartridges (likely).

**DISCLAIMER**
Please use this tool responsibly. We recommend using only clean dumps of games you own.


**IMPORTANT NOTE**
When using the SNES Blaster in multi-cart mode, the cartridge dedicates 8KB of SRAM to each game for saves, unless none of the games in the multi-cart use SRAM in which case it turns SRAM off to avoid copy-protection warnings and other issues. 
If this program notices any of the games in the multi-cart use SRAM, it will enable it for ALL games. So you will need to use a program like UCON64 to remove the copy protection from the affected games if they use SRAM detection as a copy-protection feature. Otherwise most games should work normally, whether the SRAM is enabled or not.


**HOW TO USE:**
Run the program and select your SNES Blaster type (8MB or 16MB sizes). Then select your games, and once you've done that click "Build ROM". This will prompt you to name your new multi-cart file, then save it. Use this file with the RetroBlaster programmer (requires software version 2.1.0 or higher) to write this multi-cart to the SNES Blaster. Enjoy!

If using a 8MByte SNES Blaster, you have two 4MByte slots or up to four 2MByte slots for games. You cannot have a 4MByte game with two 2MByte games - it has to be 2x 4MB or 2x-4x 2MByte games. When using a game that is less than 4MByte or 2MByte, the software will automatically pad that file up to the 2MByte or 4MByte size. For example, you want to put 2x 1MByte games onto one cartridge. The software will pad both games to 2MByte, and then spit out a 4MByte multi-cart file.

16MByte SNES Blasters have 4x 4MByte slots, regardless of game sizes. So if the game you're writing is less than 4MByte, it will be automatically padded to fit.


If you run into any specific bugs, please open a new issue here. Alternatively, you can email us - support@retrostage.net
