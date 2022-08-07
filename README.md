![Logo](https://raw.githubusercontent.com/BakonBot/legacy-place-converter/master/lpcLogo.png)
# Roblox Legacy Place Converter
## [Download (Version 1.3a)](https://github.com/BakonBot/legacy-place-converter/releases/download/1.3a/Roblox_Legacy_Place_Converter_1.3a.exe)
## What does it do?
Converts recent Roblox places to a format that old Roblox versions can read.
## How do I use it?
Save a Roblox place in the .rbxlx format, open up the converter, select the Roblox place you want to convert, click Convert and boom!
No longer do you need an old Roblox Studio version to convert your places!
## Speaking of old Roblox Studio versions, they can already convert places. Why did you make this then?
The whole manual conversion process with old Studio versions is slow and clunky, not to mention possibly unsafe too (Just saying, there's always a possibility)
## How does this work?
What prevents new Roblox places opening on old Roblox versions is that for some reason it just doesn't like the Terrain object (Given that you saved the place as rbxlx and renamed it to rbxl, since saving the place as rbxl gives you a completely different file structure). 
Removing it from the file makes it able to open the place file on old Roblox versions, but everything is grey.

Why? Roblox uses Color3 (R, G, B) for part colors, while back in the day it used BrickColor (Predefined colors, when Color3 either wasn't a thing or was hard to implement for part colors)

Color3 (Color3uint8 in the place files) is represented similarly to hex color codes, difference being Color3 uses decimal numbers instead of hexadecimal ones and adds FF000000 (4278190080) to the hex color code for some reason. (I assume it is for the alpha channel that has no reason to exist because of the Transparency property)

BrickColor is represented as a color code from a list of predefined colors (which you can find here https://developer.roblox.com/en-us/articles/BrickColor-Codes)

Converting from Color3 to BrickColor is fairly easily done. For me it was time consuming as I converted each Color3 to their BrickColor counterparts manually.

Of course, this is just for converting basic places and there's a lot more that this converter does. I've had an idea to document it all in a PDF/Word file, but gave up as that would mean commenting other people's contributions as well which is a bit too time consuming considering the fact that I'm working on other projects as well.
