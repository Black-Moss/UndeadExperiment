using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace UndeadExperiment;

[BepInPlugin("blackmoss.undeadexperiment", "Undead Experiment", "1.0.2")]
public class Plugin : BaseUnityPlugin
{
    internal new static ManualLogSource Logger;
    private readonly Harmony _harmony = new("blackmoss.undeadexperiment");
    private static Plugin Instance { get; set; } = null!;
    
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once MemberCanBePrivate.Global
    public static ConfigEntry<float> ConfigHealCountdown;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        _harmony.PatchAll();
        
        ConfigHealCountdown = Config.Bind(
            "General",
            "HealCountdown",
            1f
        );
    }

    [HarmonyPatch(typeof(global::Body), "Update")]
    public class BodyPatch
    {
        private static float _healTimer = 0f;

        [HarmonyPostfix]
        public static void Postfix_Body_Update()
        {
            _healTimer += Time.deltaTime;
            while (_healTimer >= ConfigHealCountdown.Value)
            {
                Heal();
                _healTimer -= ConfigHealCountdown.Value;
            }
        }
    }

    private static void Heal()
    {
        foreach (Limb limb in PlayerCamera.main.body.limbs)
        {
            limb.muscleHealth = 100f;
            limb.skinHealth = 100f;
            limb.boneHealTimer = 0.0f;
            limb.dislocationTimer = 0.0f;
            limb.infectionAmount = 0.0f;
            limb.bleedAmount = 0.0f;
            limb.pain = 0.0f;
            limb.shrapnel = 0;
            limb.infected = false;
        }

        PlayerCamera.main.body.brainHealth = 100f;
        PlayerCamera.main.body.bloodAmount = 100f;
        PlayerCamera.main.body.bloodViscous = 0;
        PlayerCamera.main.body.oxygenAmount = 100f;
        PlayerCamera.main.body.hunger = 100f;
        PlayerCamera.main.body.thirst = 100f;
        PlayerCamera.main.body.temperature = 37f;
        PlayerCamera.main.body.consciousness = 100f;
        PlayerCamera.main.body.stamina = 100f;
        PlayerCamera.main.body.energy = 100f;
        PlayerCamera.main.body.sicknessAmount = 0.0f;
        PlayerCamera.main.body.septicShock = 0.0f;
        PlayerCamera.main.body.radiationSickness = 0.0f;
        PlayerCamera.main.body.traumaAmount = 0.0f;
        PlayerCamera.main.body.internalBleeding = 0.0f;
        PlayerCamera.main.body.hemothorax = 0.0f;
        PlayerCamera.main.body.dirtyness = 0.0f;
        PlayerCamera.main.body.wetness = 0.0f;
        PlayerCamera.main.body.happiness = 0.0f;
        PlayerCamera.main.body.antidepressantHappiness = 0.0f;
        PlayerCamera.main.body.opiateHappiness = 0.0f;
        PlayerCamera.main.body.hearingLoss = 0.0f;
        PlayerCamera.main.body.weightOffset = 0.0f;
        PlayerCamera.main.body.badSleepAmount = 0.0f;
        PlayerCamera.main.body.adrenaline = 0.0f;
        PlayerCamera.main.body.curAdrenaline = 0.0f;
        PlayerCamera.main.body.antibioticImmunityTime = 0.0f;
        PlayerCamera.main.body.brainGrowSickness = 0.0f;
        PlayerCamera.main.body.disfigured = false;
        PlayerCamera.main.body.eyeGone = false;
        PlayerCamera.main.body.triedRollingLastStand = false;
        PlayerCamera.main.body.succesfullyRolledLastStand = false;
        PlayerCamera.main.body.lastStandTime = -1000f;
        Painkillers component1;
        if (PlayerCamera.main.body.TryGetComponent<Painkillers>(out component1))
            UnityEngine.Object.Destroy((UnityEngine.Object)component1);
        SleepingPills component2;
        if (PlayerCamera.main.body.TryGetComponent<SleepingPills>(out component2))
            UnityEngine.Object.Destroy((UnityEngine.Object)component2);
        Antidepressants component3;
        if (PlayerCamera.main.body.TryGetComponent<Antidepressants>(out component3))
            UnityEngine.Object.Destroy((UnityEngine.Object)component3);
    }
}