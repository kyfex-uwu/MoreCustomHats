# About
This mod adds new hats to PEAK!<br>
It is based on Radsi2's CustomHats mod: https://thunderstore.io/c/peak/p/Radsi2/CustomHats/

# Contents
- Chibidoki hair & horns. ( https://linktr.ee/chibidoki )
- Criken Timmy controller ( https://www.twitch.tv/criken )
- Cigarette
- Buzzball
- More to come!

# Instructions
Download the mod with the Thunderstore app and new hats will automatically be added to your passport.<br>
The Passport pagination mod is required to display all the hats in an organized manner.

If a player does not have the mod, they'll see you bald but you can still play with them.

Mods I recommend using with this one:
- More skin colors: https://thunderstore.io/c/peak/p/figgies/SmoreSkinColors/
- Skin color sliders: https://thunderstore.io/c/peak/p/Snosz/SkinColorSliders/
- Third person toggle: https://thunderstore.io/c/peak/p/Evaisa/ThirdPersonToggle/

# Screenshots & videos
[![Chibidoki, Timmy, cigarette and buzzball picture](https://monamiral.games/wp-content/uploads/MoreHatsPack1.png)](https://monamiral.games/wp-content/uploads/MoreHatsPack1.png)]
[![Chibidoki video showcase](https://img.youtube.com/vi/mSxd_n8iQtA/0.jpg)](https://youtu.be/mSxd_n8iQtA)

# How to make your own mod
Feel free to clone the repository from GitHub if you want to make your own mod that adds hats (here: https://github.com/MonAmiral/VTuberHats )<br>
You'll need an IDE like Visual Studio or equivalent to modify the code and compile it, and Unity 6 to compile the assets into AssetBundles.

## First, code: 
Find the line in Plugin.cs where it says hats.Add(LoadHat("chibidoki")); and duplicate it as many times as you want, changing the name for each hat.<br>
Once you've made these changes compile the project and put its DLL into the mod folder.<br>
I'm not going to go into details about coding, compiling, and preparing your mod. There's plenty of resources out there.

[![Code tutorial image](https://monamiral.games/wp-content/uploads/MoreHatsTutorial1.png)](https://monamiral.games/wp-content/uploads/MoreHatsTutorial1.png)]

## Next, assets:
In Unity, create one prefab and one icon for each hat, and put them directly in the Assets folder (their FBX/textures/etc can be anywhere, but prefab and icon must be at the root).<br>
Associate both prefab and icon with an AssetBundle named "morecustomhats".<br>
Build the asset bundles using a simple button ( straight up copy code from here: https://docs.unity3d.com/540/Documentation/Manual/BuildingAssetBundles.html )<br>
Copy the asset bundle into the mod folder!

[![Assets tutorial image](https://monamiral.games/wp-content/uploads/MoreHatsTutorial2.png)](https://monamiral.games/wp-content/uploads/MoreHatsTutorial2.png)]

## Building the mod:
You can make a zip file with the DLL and the asset bundle and all the files which are needed to make a mod (manifest, icon, etc., check the Thunderstore documentation).

[![Final tutorial image](https://monamiral.games/wp-content/uploads/MoreHatsTutorial3.png)](https://monamiral.games/wp-content/uploads/MoreHatsTutorial3.png)]

Then you can test the mod by "Loading a Local Mod" in the Thunderstore Mod Manager, and admire the result!
