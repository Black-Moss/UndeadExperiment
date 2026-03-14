using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using MossLib;
using MossLib.Base;
using UnityEngine;

namespace UndeadExperiment;

public class ModCommand : ModCommandBase
{
    private static ModCommand _instance;
    private static ModCommand Instance { get; set; } = new();
    private const string LocalePre = "command.undeadmode.";
    private static ManualLogSource _logger;
    
    public static void Initialize(ManualLogSource logger)
    { 
        if (_instance != null) return;
        _instance = new ModCommand();
        Instance = _instance;
        _logger = logger;
        _instance.Initialize(logger, Plugin.Guid, Plugin.Name, Assembly.GetExecutingAssembly());
    }

    [HarmonyPatch(typeof(ConsoleScript), "RegisterAllCommands")]
    public static class ConsoleScriptRegisterAllCommandsPatcher
    {
        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InconsistentNaming
        public static void RegisterCustomCommands(ConsoleScript __instance)
        {
            _logger.LogInfo(LocalePre);
            ConsoleScript.Commands.Add(new Command(
                "undeadmode", 
                ModLocale.GetFormat($"{LocalePre}description"),
                _ =>
                {
                    Tools.CheckForWorld();
                    Tools.SwitchType(Plugin.Guid, Plugin.UndeadMode, ModLocale.GetFormat($"{LocalePre}name"), _logger, __instance);
                    ModConfigs.Update();
                }, null
                )
            );
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