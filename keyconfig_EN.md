# Keyconfig and Top Panel Key

## Top Panel Key Setting

`Top Panel` does not have a keyboard binding in Slay the Spire 2 itself, so KeyboardControllerMode provides its own setting for it. The default key is `T`.

If [ModConfig](https://github.com/xhyrzldf/ModConfig-STS2) is installed (the Nexus Mods version is available [here](https://www.nexusmods.com/slaythespire2/mods/27)), you can change this key from the in-game Mods tab. Click the already configured `T`, then press the key you want to assign.

Note: Downloading from Nexus Mods may require account registration.

```text
Settings
   └─ Mods(Modconfig)
      └─ KeyboardControllerMode
         └─ Top Panel Key: T
```

If ModConfig is not installed, you can configure the key by editing the JSON file.

```json
{
  "TopPanelKey": "T"
}
```

Config file:

```text
KeyboardControllerMode.config.json
```

## When Changing Keyboard Config

`KeyboardControllerMode` reads the current Slay the Spire 2 keyboard mappings and controller mappings, then links inputs with the same action name.

When the mod is enabled, keyboard input is treated as controller input. To reconfigure keyboard bindings, use this procedure:

1. Open `Mod Settings`
2. Uncheck `KeyBoard Controller Mode`
3. Restart Slay the Spire 2
4. Open `Settings`
5. Open `Input` and reconfigure keyboard bindings
6. Restart Slay the Spire 2 so `KeyboardControllerMode` can load the current settings

## Reference: Keyconfig Quick Reference

Names are listed in this order: `PC keyboard / Xbox controller layout / PlayStation controller layout`. `--` means no key is assigned.

| Action | PC Keyboard | Xbox | PS |
|---|---|---|---|
| Confirm Card | `Space` | `A` | `×` |
| Cancel / Exit | `Escape` | `B` | `〇` |
| View Map | `M` | `--` | Touchpad Button |
| Top Panel | `--` | `X` | `□` |
| View Deck | `D` | `LB` | `L1` |
| View Draw | `A` | `LT` | `L2` |
| View Discard | `S` | `RT` | `R2` |
| View Exhaust | `X` | `RB` | `R1` |
| End Turn | `E` | `Y` | `△` |
| Peek | `Space` | `LS` | `LS` |
| Up | `Up` | `D-pad Up` | `D-pad Up` |
| Down | `Down` | `D-pad Down` | `D-pad Down` |
| Left | `Left` | `D-pad Left` | `D-pad Left` |
| Right | `Right` | `D-pad Right` | `D-pad Right` |
| Select Card #1 | `Key1` | `--` | `--` |
| Select Card #2 | `Key2` | `--` | `--` |
| Select Card #3 | `Key3` | `--` | `--` |
| Select Card #4 | `Key4` | `--` | `--` |
| Select Card #5 | `Key5` | `--` | `--` |
| Select Card #6 | `Key6` | `--` | `--` |
| Select Card #7 | `Key7` | `--` | `--` |
| Select Card #8 | `Key8` | `--` | `--` |
| Select Card #9 | `Key9` | `--` | `--` |
| Select Card #10 | `Key10` | `--` | `--` |
| Release | `Down` | `--` | `--` |
