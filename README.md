# Missing Event Flags Checker Plugin
This is a [PKHeX](https://github.com/kwsch/PKHeX) plugin to check your save data and report back important Event Flags that you may have missed during your gameplay.

It is really easy to miss a hidden item, or a partially covered field item, or even that isolated trainer that needs lots of entering/exiting cave doors and get you lost in the path.

This plugin will report all of the above, so you can properly look for it.  
For completionists, this may give you that satisfaction that you have explored and gathered all 100% of what the game provides you to acquire.

If you need to edit the event flags or just dump them, you can use the companion plugin [FlagsEditorEX](https://github.com/fattard/FlagsEditorEXPlugin).  

*Note: This is a WIP plugin, it already covers **the most important parts of each game**, but it is far from ideal.*  
*Gen1 and Gen2 games are now fully supported, while remaining ones are partially supported.*  
*Research on Gen3 games are slowly coming up.*

## Setup Instructions
- Download the plugin from the latest release [here](https://github.com/fattard/MissingEventFlagsCheckerPlugin/releases/latest).
- Extract and unblock them in Windows' Properties Menu.
- Put them in the *plugins* folder that is in the same directory as the PKHeX program path.
- If the *plugins* folder does not exist, create one, all lowercase letters.
- Check for more instructions examples if you have trouble: [Manually Installing PKHeX Plugins](https://github.com/architdate/PKHeX-Plugins/wiki/Installing-PKHeX-Plugins#manual-installation-or-installing-older-releases).

## Actions

![image](https://github.com/fattard/MissingEventFlagsCheckerPlugin/assets/1159052/9bf173c4-6781-4596-a577-ca93ff4857e2)

### Export Full Checklist

This action will export the internal tracked database of event flags in a checklist format

    [ ] not completed
    [x] completed

Each entry will have a category, a location name, and a description.  
This checklist will have all entries current in the internal database, and may be expanded later as research is done.  
You can find samples of the full checklists at the [Wiki](https://github.com/fattard/MissingEventFlagsCheckerPlugin/wiki) section.  

**Note: The checklist may contain unused data, which will be filtered out later, as well as being sorted in some confusing order that will also be fixed in later versions, as documentation on the flags progresses**

### Export only missing events

This action will export only the tracked events that are not marked yet, so you can refer only for the stuff you missed.  
If the result file is empty, you are missing nothing.  

**Note: The list may contain unused data, which will be filtered out later, as well as being sorted in some confusing order that will also be fixed in later versions, as documentation on the flags progresses**

### Export current view

This action will export the current table in the viewer in a checklist format
Use the checkboxes and filters to customize a view before exporting.  

**Note: The list may contain unused data, which will be filtered out later, as well as being sorted in some confusing order that will also be fixed in later versions, as documentation on the flags progresses**

## Supported Games
All mainline games are supported (limited descriptions for many of them)

- Red / Blue / Yellow (International and Japanese versions)
- Gold / Silver / Crystal (International, Japanese and Korean versions)
- Ruby / Sapphire / Emerald / FireRed / LeafGreen
- Diamond / Pearl / Platinum / HeartGold / Soul Silver
- Black / White / Black 2 / White 2
- X / Y / Omega Ruby / Alpha Sapphire
- Sun / Moon / Ultra Sun / Ultra Moon / Let's Go Pikachu / Let's Go Eevee
- Sword / Shield / Brilliant Diamond / Shiny Pearl / Legends: Arceus
- Scarlet / Violet

## Contributing

### Localized content

The UI localization files follows the same format as the PKHeX localization resources, with a key=value pair by the '=' character.  
The files are located at the [_localization_](/localization) folder.

It detects the same language as the main PKHeX application is currently using.

I've included an additional language file for pt-BR language, altough not supported, that I use to make room for labels in the UI, as this language is as bad in UI space constraints as Spanish or German.  
It could also be used as a given example on how the localization for the UI works.

The following table has the languages that are open to contribution
| Key | Language            | Contributors   |
|-----|---------------------|----------------|
| de  | German              |                |
| en  | English             | Me             |
| es  | Spanish             |                |
| fr  | French              |                |
| it  | Italian             |                |
| ja  | Japanese            |                |
| ko  | Korean              |                |
| zh  | Simplified Chinese  |                |
| zh2 | Traditional Chinese |                |

The checklist database resources can also be localized, but it is not recommended at this moment due to constantly changes to those resources.

Those files are simple _tsv_ text files located at  [_checklist_](/checklist) folder.

The header of the files, with some examples:
|Evt Source | Evt Idx | Event Type | Location     | Complement | Text Description                             |
|-----------|---------|------------|--------------|------------|----------------------------------------------|
| EvtFlags  | 0x0008  | ITEM GIFT  | Violet City  | Gym        | Received TM31 (Mud Slap) from Leader Falkner |


The following columns should **NOT** be modified, as they are part of internal logic
- Evt Source
- Evt Idx
- Event Type

Also, don't touch the _SEPARATOR rows, they are used to indicate section changes internally.

The localizable columns are:
- Location (the major location for this event flag, like a town name, city, dungeon)
- Complement (some useful complement like floor number, or name of a place like a house of someone)
- Text description (the description of the purpose of the event)

### New discovered flags

The event flags are being researched little by little.  
As the flags gets documented and descriptions are created, they will be embedded into the next version of the plugin.

All research work can be checked here

[Event Flags - Research spreadsheet](https://docs.google.com/spreadsheets/d/1PkY3AVafdOEqKiD_TzD4hTDRvf39ad-eI7e4JylyVII/copy)

To contribute, create a copy of the above, fill the info you researched, and contact back with the information of what needs to be merged.

Priority for community contribution would be the 3DS games.  
B2W2 had some progress right now.

## Credits

[Kurt](https://github.com/kwsch) for [PKHeX](https://github.com/kwsch/PKHeX) and [pkNX](https://github.com/kwsch/pkNX)  
[Matt](https://github.com/sora10pls) for a lot of research over event flags and datamining  
[Pret](https://github.com/Pret) and all the disassemblies  
All the people in [PPOrg](https://projectpokemon.org) that have contributed to event flags research
