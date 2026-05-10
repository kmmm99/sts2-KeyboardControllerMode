using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.ControllerInput;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

[ModInitializer("Initialize")]
public static class ModEntry
{
    public static void Initialize()
    {
        KeyboardControllerMode.Config.ModConfig.Load();
        KeyboardControllerMode.Config.ModConfigBridge.DeferredRegister();
        new Harmony("kmmm99.keyboardcontrollermode").PatchAll();
        GD.Print("[KeyboardControllerMode] initialized");
    }
}

namespace KeyboardControllerMode.Config
{
    public sealed class ModConfigData
    {
        public string TopPanelKey { get; set; } = "T";
    }

    public static class ModConfig
    {
        private const string ConfigFileName = "KeyboardControllerMode.config.json";
        internal const string ModId = "KeyboardControllerMode";
        internal const string TopPanelKeyConfigKey = "topPanelKey";
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public static long TopPanelKeyCode { get; private set; } = (long)Key.T;

        public static void Load()
        {
            try
            {
                string path = GetConfigPath();
                ModConfigData config;

                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    config = JsonSerializer.Deserialize<ModConfigData>(json) ?? new ModConfigData();
                }
                else
                {
                    config = new ModConfigData();
                    File.WriteAllText(path, JsonSerializer.Serialize(config, JsonOptions));
                }

                if (!Enum.TryParse(config.TopPanelKey, ignoreCase: true, out Key topPanelKey))
                {
                    GD.PrintErr($"[KeyboardControllerMode] Invalid TopPanelKey '{config.TopPanelKey}', falling back to T");
                    topPanelKey = Key.T;
                }

                SetTopPanelKeyCode((long)topPanelKey, persist: false);
                GD.Print($"[KeyboardControllerMode] TopPanelKey = {topPanelKey}");
            }
            catch (Exception ex)
            {
                TopPanelKeyCode = (long)Key.T;
                GD.PrintErr($"[KeyboardControllerMode] failed to load config, falling back to T: {ex}");
            }
        }

        public static void SetTopPanelKeyCode(long keyCode, bool persist)
        {
            if (keyCode == 0)
            {
                keyCode = (long)Key.T;
            }

            TopPanelKeyCode = keyCode;

            if (persist)
            {
                SaveTopPanelKeyCode(keyCode);
            }
        }

        private static void SaveTopPanelKeyCode(long keyCode)
        {
            try
            {
                string display = OS.GetKeycodeString((Key)keyCode);
                if (string.IsNullOrWhiteSpace(display))
                {
                    display = ((Key)keyCode).ToString();
                }

                ModConfigData config = new ModConfigData { TopPanelKey = display };
                File.WriteAllText(GetConfigPath(), JsonSerializer.Serialize(config, JsonOptions));
            }
            catch (Exception ex)
            {
                GD.PrintErr($"[KeyboardControllerMode] failed to save config: {ex}");
            }
        }

        private static string GetConfigPath()
        {
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            string? directory = Path.GetDirectoryName(assemblyPath);
            return Path.Combine(directory ?? ".", ConfigFileName);
        }
    }

    internal static class ModConfigBridge
    {
        private static bool _available;
        private static bool _registered;
        private static Type? _apiType;
        private static Type? _entryType;
        private static Type? _configTypeEnum;

        internal static void DeferredRegister()
        {
            SceneTree tree = (SceneTree)Engine.GetMainLoop();
            tree.ProcessFrame += OnNextFrame;
        }

        private static void OnNextFrame()
        {
            SceneTree tree = (SceneTree)Engine.GetMainLoop();
            tree.ProcessFrame -= OnNextFrame;
            Detect();
            if (_available)
            {
                Register();
                ModConfig.SetTopPanelKeyCode(GetValue(ModConfig.TopPanelKeyConfigKey, ModConfig.TopPanelKeyCode), persist: true);
            }
        }

        private static void Detect()
        {
            try
            {
                Type[] allTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly =>
                    {
                        try { return assembly.GetTypes(); }
                        catch { return Type.EmptyTypes; }
                    })
                    .ToArray();

                _apiType = allTypes.FirstOrDefault(type => type.FullName == "ModConfig.ModConfigApi");
                _entryType = allTypes.FirstOrDefault(type => type.FullName == "ModConfig.ConfigEntry");
                _configTypeEnum = allTypes.FirstOrDefault(type => type.FullName == "ModConfig.ConfigType");
                _available = _apiType != null && _entryType != null && _configTypeEnum != null;
            }
            catch
            {
                _available = false;
            }
        }

        private static void Register()
        {
            if (_registered)
            {
                return;
            }

            _registered = true;

            try
            {
                Array entries = BuildEntries();
                Dictionary<string, string> displayNames = new Dictionary<string, string>
                {
                    ["en"] = "KeyboardControllerMode",
                    ["ja"] = "KeyboardControllerMode"
                };

                MethodInfo registerMethod = _apiType!.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(method => method.Name == "Register")
                    .OrderByDescending(method => method.GetParameters().Length)
                    .First();

                if (registerMethod.GetParameters().Length == 4)
                {
                    registerMethod.Invoke(null, new object[] { ModConfig.ModId, displayNames["en"], displayNames, entries });
                }
                else
                {
                    registerMethod.Invoke(null, new object[] { ModConfig.ModId, displayNames["en"], entries });
                }
            }
            catch (Exception ex)
            {
                GD.PrintErr($"[KeyboardControllerMode] ModConfig registration failed: {ex}");
            }
        }

        private static Array BuildEntries()
        {
            List<object> entries = new List<object>();

            entries.Add(Entry(config =>
            {
                Set(config, "Key", ModConfig.TopPanelKeyConfigKey);
                Set(config, "Label", "Top Panel Key");
                Set(config, "Labels", new Dictionary<string, string>
                {
                    ["en"] = "Top Panel Key",
                    ["ja"] = "Top Panel Key"
                });
                Set(config, "Type", EnumVal("KeyBind"));
                Set(config, "DefaultValue", (object)ModConfig.TopPanelKeyCode);
                Set(config, "Description", "Keyboard key used for the Top Panel controller action.");
                Set(config, "Descriptions", new Dictionary<string, string>
                {
                    ["en"] = "Keyboard key used for the Top Panel controller action.",
                    ["ja"] = "Top Panel controller action に使うキーボードキーです。"
                });
                Set(config, "OnChanged", new Action<object>(value =>
                {
                    long keyCode = Convert.ToInt64(value);
                    ModConfig.SetTopPanelKeyCode(keyCode, persist: true);
                }));
            }));

            Array result = Array.CreateInstance(_entryType!, entries.Count);
            for (int i = 0; i < entries.Count; i++)
            {
                result.SetValue(entries[i], i);
            }

            return result;
        }

        private static long GetValue(string key, long fallback)
        {
            if (!_available)
            {
                return fallback;
            }

            try
            {
                object? result = _apiType!.GetMethod("GetValue", BindingFlags.Public | BindingFlags.Static)
                    ?.MakeGenericMethod(typeof(long))
                    ?.Invoke(null, new object[] { ModConfig.ModId, key });
                return result != null ? Convert.ToInt64(result) : fallback;
            }
            catch
            {
                return fallback;
            }
        }

        private static object Entry(Action<object> configure)
        {
            object instance = Activator.CreateInstance(_entryType!)!;
            configure(instance);
            return instance;
        }

        private static void Set(object obj, string name, object value)
        {
            obj.GetType().GetProperty(name)?.SetValue(obj, value);
        }

        private static object EnumVal(string name)
        {
            return Enum.Parse(_configTypeEnum!, name);
        }
    }
}

namespace KeyboardControllerMode.Patches
{
    [HarmonyPatch(typeof(NControllerManager), "_Input")]
    public static class NControllerManagerInputPatch
    {
        private static bool _synthesizing;
        private static readonly Dictionary<long, StringName[]> _keyToControllerActions = new Dictionary<long, StringName[]>();
        private static FieldInfo? _keyboardMapField;
        private static FieldInfo? _controllerMapField;

        [HarmonyPrefix]
        private static bool Prefix(NControllerManager __instance, InputEvent inputEvent)
        {
            if (_synthesizing || inputEvent is not InputEventKey keyEvent)
            {
                return true;
            }

            StringName[] actions = MapKey(keyEvent);
            if (actions.Length == 0)
            {
                return true;
            }

            __instance.GetViewport()?.SetInputAsHandled();

            _synthesizing = true;
            try
            {
                if (keyEvent.Pressed && !keyEvent.Echo)
                {
                    foreach (StringName action in actions)
                    {
                        Input.ActionPress(action);
                        using InputEventAction synthetic = new InputEventAction
                        {
                            Action = action,
                            Pressed = true,
                            Strength = 1.0f
                        };
                        Input.ParseInputEvent(synthetic);
                    }
                }
                else if (!keyEvent.Pressed)
                {
                    foreach (StringName action in actions)
                    {
                        Input.ActionRelease(action);
                        using InputEventAction synthetic = new InputEventAction
                        {
                            Action = action,
                            Pressed = false,
                            Strength = 0.0f
                        };
                        Input.ParseInputEvent(synthetic);
                    }
                }
            }
            finally
            {
                _synthesizing = false;
            }

            return false;
        }

        private static StringName[] MapKey(InputEventKey keyEvent)
        {
            long key = (long)keyEvent.GetKeycodeWithModifiers();
            if (key == 0)
            {
                key = (long)keyEvent.Keycode;
            }

            ReloadBindings();
            return _keyToControllerActions.TryGetValue(key, out StringName[]? actions)
                ? actions
                : Array.Empty<StringName>();
        }

        private static void ReloadBindings()
        {
            NInputManager? inputManager = NInputManager.Instance;
            if (inputManager == null)
            {
                return;
            }

            try
            {
                _keyboardMapField ??= typeof(NInputManager).GetField("_keyboardInputMap", BindingFlags.Instance | BindingFlags.NonPublic);
                _controllerMapField ??= typeof(NInputManager).GetField("_controllerInputMap", BindingFlags.Instance | BindingFlags.NonPublic);

                Dictionary<StringName, Key>? keyboardMap =
                    _keyboardMapField?.GetValue(inputManager) as Dictionary<StringName, Key>;
                Dictionary<StringName, StringName>? controllerMap =
                    _controllerMapField?.GetValue(inputManager) as Dictionary<StringName, StringName>;

                if (keyboardMap == null || controllerMap == null || keyboardMap.Count == 0 || controllerMap.Count == 0)
                {
                    return;
                }

                _keyToControllerActions.Clear();

                foreach (KeyValuePair<StringName, Key> keyboardBinding in keyboardMap)
                {
                    if (!controllerMap.TryGetValue(keyboardBinding.Key, out StringName? controllerAction) || controllerAction == null)
                    {
                        continue;
                    }

                    AddBinding((long)keyboardBinding.Value, controllerAction);
                }

                if (controllerMap.TryGetValue(MegaInput.topPanel, out StringName? topPanelAction) && topPanelAction != null)
                {
                    AddBinding(KeyboardControllerMode.Config.ModConfig.TopPanelKeyCode, topPanelAction);
                }
            }
            catch (Exception ex)
            {
                GD.PrintErr($"[KeyboardControllerMode] failed to reload key bindings: {ex}");
            }
        }

        private static void AddBinding(long key, StringName controllerAction)
        {
            if (_keyToControllerActions.TryGetValue(key, out StringName[]? existing))
            {
                _keyToControllerActions[key] = existing.Append(controllerAction).Distinct().ToArray();
            }
            else
            {
                _keyToControllerActions[key] = new[] { controllerAction };
            }
        }
    }
}
