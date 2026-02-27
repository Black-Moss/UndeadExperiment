using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using MossLib.Base;
using UnityEngine;

namespace UndeadExperiment;

public class ModCommand : ModCommandBase
{
    private static ModCommand Instance { get; set; } = new();
    
    private static ModCommand _instance;
    
    public static void Initialize(ManualLogSource logger)
    { 
        if (_instance != null) return;
        _instance = new ModCommand();
        Instance = _instance;
        _instance.Initialize(logger, Plugin.Guid, Plugin.Name, Assembly.GetExecutingAssembly());
    }

    [HarmonyPatch(typeof(ConsoleScript), "RegisterAllCommands")]
    public static class ConsoleScriptRegisterAllCommandsPatcher
    {
        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Global
        public static void RegisterCustomCommands()
        {
            ConsoleScript.Commands.Add(new Command(
                "undeadmode", 
                ModLocale.GetFormat("command.undeadmode"),
                args =>
                {
                    Instance.CheckForWorld();
                    bool newState;
                    if (args.Length > 1)
                    {
                        newState = Instance.ParseBool(args[1]);
                    }
                    else
                    {
                        newState = !Plugin.ConfigUndeadMode.Value;
                    }
                    
                    Plugin.ConfigUndeadMode.Value = newState;

                    var message = newState ? 
                        ModLocale.GetFormat("undeadmode.enable") : 
                        ModLocale.GetFormat("undeadmode.disable");
                    var statusMessage = ModLocale.GetFormat("undeadmode.state", newState);
                    
                    PlayerCamera.main.DoAlert(message);
                    Instance.LogToConsole(statusMessage);
                    Plugin.Logger.LogInfo(statusMessage);
                }, null,
                ("bool", ModLocale.GetFormat("undeadmode.input"))));
        }
    }

    [HarmonyPatch(typeof(ConsoleScript), "Awake")]
    public new class ConsoleScriptAwakePatcher
    {
        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Global
        public static void AddCustomLogCallback()
        {
            Application.logMessageReceived += Instance.ApplicationLogCallback;
        }
    }
}