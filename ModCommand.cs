using System;
using HarmonyLib;
using UnityEngine;

namespace UndeadExperiment;

public static class ModCommand
{
    private static string _lastErr = "";

    public static void Initialize()
    {
        new Harmony($"{Plugin.HarmonyPath}.customcommand").PatchAll(typeof(ModCommand));
    }

    [HarmonyPatch(typeof(ConsoleScript), "RegisterAllCommands")]
    public static class ConsoleScriptPatcher
    {
        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Global
        public static void Postfix_ConsoleScript_RegisterAllCommands()
        {
            ConsoleScript.Commands.Add(new Command("undeadmode", "Toggles undead mode on/off.",
                (Command.Action) (args =>
                {
                    CheckForWorld();
                    bool newState;
                    if (args.Length > 1)
                    {
                        newState = ParseBool(args[1]);
                    }
                    else
                    {
                        newState = !Plugin.ConfigUndeadMode.Value;
                    }
                    
                    Plugin.ConfigUndeadMode.Value = newState;
                    
                    var message = newState ? "Undead Mode Enabled!" : "Undead Mode Disabled!";
                    var statusMessage = $"Undead Mode: {newState}";
                    
                    PlayerCamera.main.DoAlert(message);
                    LogToConsole(statusMessage);
                    Plugin.Logger.LogInfo(statusMessage);
                }), null,
                ("bool", "Enable undead mode? (optional)")));
        }
    }

    private static void LogToConsole(string text)
    {
        if (ConsoleScript.instance != null)
        {
            ConsoleScript.instance.ExecuteCommand($"log {text.Replace(" ", " ")}");
        }
    }

    private static void ApplicationLogCallback(string condition, string stackTrace, LogType type)
    {
        if (_lastErr == condition)
            return;
            
        _lastErr = condition;

        switch (type)
        {
            case LogType.Error or LogType.Assert:
                LogToConsole($"<color=red>{condition}; {stackTrace}</color>");
                break;
            case LogType.Warning:
                LogToConsole($"<color=yellow>{condition}; {stackTrace}</color>");
                break;
            case LogType.Log:
                LogToConsole(condition);
                break;
            case LogType.Exception:
                LogToConsole($"<color=red>{condition}; {stackTrace}</color>");
                break;
        }
    }

    [HarmonyPatch(typeof(ConsoleScript), "Awake")]
    public static class ConsoleScriptAwakePatcher
    {
        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Global
        public static void Postfix_ConsoleScript_Awake()
        {
            Application.logMessageReceived += ApplicationLogCallback;
        }
    }

    private static bool ParseBool(string s)
    {
        return !bool.TryParse(s, out var result) ? throw new Exception($"\"{s}\" is not a valid boolean value! (true/false)") : result;
    }
    
    private static void CheckForWorld()
    {
        if (!(bool) (UnityEngine.Object) PlayerCamera.main)
            throw new Exception("No world is loaded. Try starting a game?");
    }
}