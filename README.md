# Missing Event Flags Checker Plugin
This is a [PKHeX](https://github.com/kwsch/PKHeX) plugin.

The main purpose is to check your save data and report back important Event Flags that you may have missed during your gameplay.

It is really easy to miss a hidden item, or a partially covered field item, or even that isolated trainer that needs lots of entering/exiting cave doors and get you lost in the path.

This plugin will report all of the above, so you can properly look for it.  
For completionists, this may give you that satisfaction that you have explored and gathered all 100% of what the game provides you to acquire.

If you need to edit the event flags or just dump them, you can use the companion plugin [FlagsEditorEX](https://github.com/fattard/FlagsEditorEXPlugin).

## Setup Instructions
- Download the plugin from the latest release [here](https://github.com/fattard/MissingEventFlagsCheckerPlugin/releases/latest).
- Extract and unblock them in Windows' Properties Menu.
- Put them in the *plugins* folder that is in the same directory as the PKHeX program path.
- Check for more instructions examples if you have trouble: [Manually Installing PKHeX Plugins](https://github.com/architdate/PKHeX-Plugins/wiki/Installing-PKHeX-Plugins#manual-installation-or-installing-older-releases).

## Actions

### Export full Checklist

This action will export the internal tracked database of event flags in a checklist format

    [ ] not completed
    [x] completed

Each entry will have a category, a location name, and a description.  
This checklist will have all entries current in the internal database, and may be expanded later as research is done.  
You can find samples of the full checklists at the [Wiki](https://github.com/fattard/MissingEventFlagsCheckerPlugin/wiki) section.  
The exported file will contains the name *checklist_VERSION.txt* that will be created alongside the PKHeX program path.

**Note: The checklist may contain unused data, which will be filtered out later, as well as being sorted in some confusing order that will also be fixed in later versions, as documentation on the flags progresses**

### Export only Missing Flags

This action will export only the tracked events that are not marked yet, so you can refer only for the stuff you missed.  
If the result file is empty, you are missing nothing.  
The exported file will contains the name *missing_events_VERSION.txt* that will be created alongside the PKHeX program path.

**Note: The list may contain unused data, which will be filtered out later, as well as being sorted in some confusing order that will also be fixed in later versions, as documentation on the flags progresses**

## Supported Games
All mainline games are supported (limited descriptions for many of them)

- Red / Blue / Yellow (International versions)
- Gold / Silver / Crystal (International versions)
- Ruby / Sapphire / Emerald / FireRed / LeafGreen
- Diamond / Pearl / Platinum / HeartGold / Soul Silver
- Black / White / Black 2 / White 2
- X / Y / Omega Ruby / Alpha Sapphire
- Sun / Moon / Ultra Sun / Ultra Moon / Let's Go Pikachu / Let's Go Eevee
- Sword / Shield / Brilliant Diamond / Shiny Pearl / Legends: Arceus
- Scarlet / Violet

## Contributing

The event flags are being researched little by little.  
As the flags gets documented and descriptions are created, they will be embedded into the next version of the plugin.

All research work can be checked here

[Event Flags - Research spreadsheet](https://docs.google.com/spreadsheets/d/1PkY3AVafdOEqKiD_TzD4hTDRvf39ad-eI7e4JylyVII/edit?usp=sharing)

To contribute, create a copy of the above, fill the info you researched, and contact back with the information of what needs to be merged.

Priority for community contribution would be the 3DS games.  
B2W2 had some progress right now.
