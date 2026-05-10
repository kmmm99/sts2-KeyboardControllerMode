# Keyboard Controller Mode Mod
[JP](https://github.com/kmmm99/sts2-KeyboardControllerMode/blob/main/README.md) | EN | [Keyconfig_JP](https://github.com/kmmm99/sts2-KeyboardControllerMode/blob/main/keyconfig.md) | [Keyconfig_EN](https://github.com/kmmm99/sts2-KeyboardControllerMode/blob/main/keyconfig_EN.md)

## Disclaimer

- The author is not responsible for corrupted save data, lost progress, malfunction, or any other damage caused by installing this mod, changing settings, migrating save data, or using this mod together with the base game or other mods. Install and use this mod at your own risk.
- Before installing the mod, always back up your save data. If necessary, temporarily disable Steam Cloud auto-sync.
- Slay the Spire 2 is currently in Early Access, so future updates may make this mod unusable.

## What Is This?

`KeyboardControllerMode` maps keyboard input to Slay the Spire 2 controller actions. It lets you operate the controller-style UI using only the keyboard, without requiring mouse input.

After launch, the mod reads the game's current keyboard mappings and controller mappings, then links inputs that share the same action name.

Example:

- Keyboard binding for `View Exhaust` is `E`
- Controller binding for `View Exhaust` is `RB`
- In this case, pressing `E` is treated as an `RB` controller input

Default mapping example:

| Key | Action |
|---|---|
| `↑` | Up |
| `↓` | Down |
| `←` | Left |
| `→` | Right |
| `Space` | Confirm / Peek |
| `Escape` | Cancel |
| `M` | View Map |
| `D` | View Deck |
| `A` | View Draw Pile |
| `S` | View Discard Pile |
| `X` | View Exhaust Pile |
| `E` | End Turn |

`Top Panel` does not exist in Slay the Spire 2's built-in keyboard settings, so KeyboardControllerMode provides a dedicated setting for it. By default, this is the `T` key. For details, see [Keyconfig and Top Panel Key](https://github.com/kmmm99/sts2-KeyboardControllerMode/blob/main/keyconfig_EN.md).

## 0. Requirements

- Slay the Spire 2 installed. This guide assumes a Windows environment.
- `KeyboardControllerMode.dll` and `KeyboardControllerMode.json`

## 1. Preparation

Before installing the mod, back up your save data and temporarily disable Steam Cloud.

### Back Up Your Save Data

Close Slay the Spire 2, then copy this entire folder somewhere safe:

```text
C:\Users\<UserName>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>
```

### Temporarily Disable Steam Cloud Auto-Sync

To avoid accidental save overwrites during migration, turn off Steam Cloud while working with save data.

1. Right-click `Slay the Spire 2` in your Steam Library
2. Open `Properties`
3. Open `General`
4. Turn off `Keep games saves in the Steam Cloud for Slay the Spire 2`

After save migration and testing are complete, turn it back on if needed.

## 2. Open the Game Install Folder

From Steam:

1. Right-click `Slay the Spire 2` in your Steam Library
2. Select `Manage`
3. Select `Browse local files`

Default path:

```text
~\Steam\steamapps\common\Slay the Spire 2
```

## 3. Create the mods Folder

Create a `mods` folder directly under the game folder.

```text
~\Steam\steamapps\common\Slay the Spire 2\mods
```

If it already exists, leave it as-is.

## 4. Install KeyboardControllerMode

Download and extract [KeyboardControllerMode.zip](https://github.com/kmmm99/sts2-KeyboardControllerMode/blob/main/KeyboardControllerMode.zip), then place it in the `mods` folder. Confirm that the `KeyboardControllerMode` folder contains the following three files:

```text
KeyboardControllerMode.dll
KeyboardControllerMode.json
KeyboardControllerMode.config.json
```

Expected layout:

```text
Slay the Spire 2
└─ mods
   └─ KeyboardControllerMode
      ├─ KeyboardControllerMode.dll
      ├─ KeyboardControllerMode.json
      └─ KeyboardControllerMode.config.json
```

## 5. Launch the Game and Enable the Mod

1. Launch Slay the Spire 2
2. Confirm that `Keyboard Controller Mode` appears in the mod loading screen
3. Start the game in the modded environment

## 6. After Installation: Use Vanilla Saves in Modded Mode

Slay the Spire 2 stores vanilla saves and modded saves separately.

Vanilla save path:

```text
C:\Users\<UserName>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>\profile1
```

Modded save path:

```text
C:\Users\<UserName>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>\modded\profile1
```

On first modded launch, your profile may look reset. In many cases, your vanilla save is not gone; the game is simply looking at the separate modded profile.

### Normal Copy Procedure

With Slay the Spire 2 closed, copy:

```text
C:\Users\<UserName>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>\profile1
```

to:

```text
C:\Users\<UserName>\AppData\Roaming\SlayTheSpire2\steam\<SteamID>\modded\profile1
```

### If a Simple Copy Does Not Work

If the copied profile is overwritten with an empty modded profile, use this procedure:

1. Launch the game with mods enabled
2. In-game, select a different profile such as `profile2`
3. While the game is still running, copy vanilla `profile1` into `modded\profile1`
4. Return to the game and switch to `profile1`

This can allow the modded environment to load your vanilla progress.

Notes:

- Confirm that your backup exists before copying
- Prefer one-way copying from `Vanilla -> Modded`
- Avoid copying `Modded -> Vanilla`
