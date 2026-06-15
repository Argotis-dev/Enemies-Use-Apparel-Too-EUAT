# Enemies Use Apparel Too - EUAT

The purppose of this mod is to allow enemy humanoids to use apparel like the grenadier armor or turret pack. In order to do this the mod does two things. It converts apparel xml verb defs into ability defs so the ai can spot and use them and it proved a way for ability use to charge apparel that has charges (i.e. grenades from the grenadier armor).  I haven't added harmory patches or Mod checkliss yet (if you don't have an expansion you might have issues).

## Note!!!!!!!

This upload doesn't add the items to enemy spawn pools (yet). It only allows Enemies to use it if they have it equipped.

Items made usable for enemies:

Jump Packs
Locust Armor
Pheonix Armor
Grenadier Armor
Hunter Packs
Turret Packs

## Additional Notes
This Mod is based off of the the template found here: https://github.com/Rimworld-Mods/Template. I would have struggled way longer to get started without them so big thanks to the creators!!!! I'm leaving the notes from the template mod here while I finish developing this MOD

## Setup
### Windows
1. Download and install [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core) and [.Net Framework 4.8 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/net48). This step can be skipped if you already have required C# packages from Visual Studio IDE.
2. Install [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).
3. Clone, pull or download this template into your Rimworld `Mods` folder.

### Linux
1. Linux `dotnet` setup may vary depending on how you install Rimworld and what distro is being used. Follow [Microsoft's instructions](https://learn.microsoft.com/en-us/dotnet/core/install/linux) to install `dotnet`.
2. Install [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp).
3. Clone, pull or download this template into your Rimworld `Mods` folder.

## Additional Notes
* By pressing `F5` key VS Code will perform 2 operations: build assembly file and launch Rimworld executable. 
* All intermediate files are kept inside `.vscode` folder.
* For XML only modders remove preLaunchTask line from `.vscode/launch.json` file.
* Modify `.vscode/mod.csproj` and `About/About.xml` according to your needs.

## Debugger
### Setup
1. Follow the instructions of [pardeike / Rimworld-Doorstop](https://github.com/pardeike/Rimworld-Doorstop) to create a debug server.
2. Install [Mono Debug extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.mono-debug).
3. In the Debug Panel (`Ctrl+Shift+D`), switch the configuration from `Build & Run` to `Build & Debug`.
4. Linux users additionally need to install the `mono` package.

### Potential Issues
* __Launch process hanging__  
If the Doorstop `debug_suspend` option is enabled, the `Build & Run` action will hang because the process is waiting for a debugger handshake. To resolve this, either attach the debugger manually or use the `Build & Debug` action to automate the connection.

## Companion Tools
* __Mod Generator Utility__ | [Jellypowered / Prepare New Mod](https://github.com/Jellypowered/PrepareNewMod)