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
            limb.dismembered = false;
            limb.blockedBleeding = false;
        }

        var body = PlayerCamera.main.body;
        body.brainHealth = 100f;
        body.bloodAmount = 100f;
        body.bloodViscous = 0;
        body.oxygenAmount = 100f;
        body.hunger = 100f;
        body.thirst = 100f;
        body.temperature = 37f;
        body.consciousness = 100f;
        body.stamina = 100f;
        body.energy = 100f;
        body.sicknessAmount = 0.0f;
        body.septicShock = 0.0f;
        body.radiationSickness = 0.0f;
        body.traumaAmount = 0.0f;
        body.internalBleeding = 0.0f;
        body.hemothorax = 0.0f;
        body.dirtyness = 0.0f;
        body.wetness = 0.0f;
        body.happiness = 0.0f;
        body.antidepressantHappiness = 0.0f;
        body.opiateHappiness = 0.0f;
        body.hearingLoss = 0.0f;
        body.weightOffset = 0.0f;
        body.badSleepAmount = 0.0f;
        body.adrenaline = 0.0f;
        body.curAdrenaline = 0.0f;
        body.antibioticImmunityTime = 0.0f;
        body.brainGrowSickness = 0.0f;
        body.disfigured = false;
        body.eyeGone = false;
        body.triedRollingLastStand = false;
        body.succesfullyRolledLastStand = false;
        body.lastStandTime = -1000f;
        
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