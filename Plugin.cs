using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace UndeadExperiment;

[BepInPlugin(Guid, Name, "1.1.1")]
[BepInDependency("blackmoss.mosslib")]
public class Plugin : BaseUnityPlugin
{
    public new static ManualLogSource Logger;
    internal const string Guid = "blackmoss.undeadexperiment";
    internal const string Name = "Undead Experiment";
    private readonly Harmony _harmony = new(Guid);
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    private static Plugin Instance { get; set; } = null!;
    
    // General
    public static ConfigEntry<float> ConfigHealCountdown;
    public static ConfigEntry<bool>  ConfigUndeadMode;
    
    // Limb
    public static ConfigEntry<float> ConfigMuscleHeadth;
    public static ConfigEntry<float> ConfigSkinHealth;
    public static ConfigEntry<float> ConfigBoneHealTImer;
    public static ConfigEntry<float> ConfigDislocationTimer;
    public static ConfigEntry<float> ConfigInfectionAmount;
    public static ConfigEntry<float> ConfigBleedAmount;
    public static ConfigEntry<float> ConfigPainkillers;
    public static ConfigEntry<int>   ConfigShrapnel;
    public static ConfigEntry<bool>  ConfigInfected;
    public static ConfigEntry<bool>  ConfigDismembered;
    public static ConfigEntry<bool>  ConfigBlockedBleeding;
    
    // Body
    public static ConfigEntry<float> ConfigBrainHealth;
    public static ConfigEntry<float> ConfigBloodAmount;
    public static ConfigEntry<int>   ConfigBloodViscous;
    public static ConfigEntry<float> ConfigOxygenAmount;
    public static ConfigEntry<float> ConfigHunger;
    public static ConfigEntry<float> ConfigThirst;
    public static ConfigEntry<float> ConfigTemperature;
    public static ConfigEntry<float> ConfigConsciousness;
    public static ConfigEntry<float> ConfigStamina;
    public static ConfigEntry<float> ConfigEnergy;
    public static ConfigEntry<float> ConfigSicknessAmount;
    public static ConfigEntry<float> ConfigSepticShock;
    public static ConfigEntry<float> ConfigRadiationSickness;
    public static ConfigEntry<float> ConfigTraumaAmount;
    public static ConfigEntry<float> ConfigInternalBleeding;
    public static ConfigEntry<float> ConfigHemothorax;
    public static ConfigEntry<float> ConfigDirtyness;
    public static ConfigEntry<float> ConfigWetness;
    public static ConfigEntry<float> ConfigHappiness;
    public static ConfigEntry<float> ConfigAntidepressantHappiness;
    public static ConfigEntry<float> ConfigOpiateHappiness;
    public static ConfigEntry<float> ConfigHearingLoss;
    public static ConfigEntry<float> ConfigWeightOffset;
    public static ConfigEntry<float> ConfigBadSleepAmount;
    public static ConfigEntry<float> ConfigAdrenaline;
    public static ConfigEntry<float> ConfigCurAdrenaline;
    public static ConfigEntry<float> ConfigAntibioticImmunityTime;
    public static ConfigEntry<float> ConfigBrainGrowSickness;
    public static ConfigEntry<bool>  ConfigDisfigured;
    public static ConfigEntry<bool>  ConfigEyeGone;
    public static ConfigEntry<bool>  ConfigTriedRollingLastStand;
    public static ConfigEntry<bool>  ConfigSuccesfullyRolledLastStand;
    public static ConfigEntry<float> ConfigLastStandTime;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        ModLocale.Initialize(Logger);
        ModCommand.Initialize(Logger);
        _harmony.PatchAll();
        
        //  General
        ConfigHealCountdown = Config.Bind(
            "General",
            "HealCountdown",
            1f
        );
        ConfigUndeadMode = Config.Bind(
            "General",
            "UndeadMode",
            true
        );

        // Limb
        ConfigMuscleHeadth = Config.Bind(
            "Limb",
            "MuscleHealth",
            100f
        );
        ConfigSkinHealth = Config.Bind(
            "Limb",
            "SkinHealth",
            100f
        );
        ConfigBoneHealTImer = Config.Bind(
            "Limb",
            "BoneHealTImer",
            0.0f
        );
        ConfigDislocationTimer = Config.Bind(
            "Limb",
            "DislocationTimer",
            0.0f
        );
        ConfigInfectionAmount = Config.Bind(
            "Limb",
            "InfectionAmount",
            0.0f
        );
        ConfigBleedAmount = Config.Bind(
            "Limb",
            "BleedAmount",
            0.0f
        );
        ConfigPainkillers = Config.Bind(
            "Limb",
            "Painkillers",
            0.0f
        );
        ConfigShrapnel = Config.Bind(
            "Limb",
            "Shrapnel",
            0
        );
        ConfigInfected = Config.Bind(
            "Limb",
            "Infected",
            false
        );
        ConfigDismembered = Config.Bind(
            "Limb",
            "Dismembered",
            false
        );
        ConfigBlockedBleeding = Config.Bind(
            "Limb",
            "BlockedBleeding",
            false
        );
        
        // Body
        ConfigBrainHealth = Config.Bind(
            "Body",
            "BrainHealth",
            100f
        );
        ConfigBloodAmount = Config.Bind(
            "Body",
            "BloodAmount",
            100f
        );
        ConfigBloodViscous = Config.Bind(
            "Body",
            "BloodViscous",
            0
        );
        ConfigOxygenAmount = Config.Bind(
            "Body",
            "OxygenAmount",
            100f
        );
        ConfigHunger = Config.Bind(
            "Body",
            "Hunger",
            100f
        );
        ConfigThirst = Config.Bind(
            "Body",
            "Thirst",
            100f
        );
        ConfigTemperature = Config.Bind(
            "Body",
            "Temperature",
            37f
        );
        ConfigConsciousness = Config.Bind(
            "Body",
            "Consciousness",
            100f
        );
        ConfigStamina = Config.Bind(
            "Body",
            "Stamina",
            100f
        );
        ConfigEnergy = Config.Bind(
            "Body",
            "Energy",
            100f
        );
        ConfigSicknessAmount = Config.Bind(
            "Body",
            "SicknessAmount",
            0.0f
        );
        ConfigSepticShock = Config.Bind(
            "Body",
            "SepticShock",
            0.0f
        );
        ConfigRadiationSickness = Config.Bind(
            "Body",
            "RadiationSickness",
            0.0f
        );
        ConfigTraumaAmount = Config.Bind(
            "Body",
            "TraumaAmount",
            0.0f
        );
        ConfigInternalBleeding = Config.Bind(
            "Body",
            "InternalBleeding",
            0.0f
        );
        ConfigHemothorax = Config.Bind(
            "Body",
            "Hemothorax",
            0.0f
        );
        ConfigDirtyness = Config.Bind(
            "Body",
            "Dirtyness",
            0.0f
        );
        ConfigWetness = Config.Bind(
            "Body",
            "Wetness",
            0.0f
        );
        ConfigHappiness = Config.Bind(
            "Body",
            "Happiness",
            0.0f
        );
        ConfigAntidepressantHappiness = Config.Bind(
            "Body",
            "AntidepressantHappiness",
            0.0f
        );
        ConfigOpiateHappiness = Config.Bind(
            "Body",
            "OpiateHappiness",
            0.0f
        );
        ConfigHearingLoss = Config.Bind(
            "Body",
            "HearingLoss",
            0.0f
        );
        ConfigWeightOffset = Config.Bind(
            "Body",
            "WeightOffset",
            0.0f
        );
        ConfigBadSleepAmount = Config.Bind(
            "Body",
            "BadSleepAmount",
            0.0f
        );
        ConfigAdrenaline = Config.Bind(
            "Body",
            "Adrenaline",
            0.0f
        );
        ConfigCurAdrenaline = Config.Bind(
            "Body",
            "CurAdrenaline",
            0.0f
        );
        ConfigAntibioticImmunityTime = Config.Bind(
            "Body",
            "AntibioticImmunityTime",
            0.0f
        );
        ConfigBrainGrowSickness = Config.Bind(
            "Body",
            "BrainGrowSickness",
            0.0f
        );
        ConfigDisfigured = Config.Bind(
            "Body",
            "Disfigured",
            false
        );
        ConfigEyeGone = Config.Bind(
            "Body",
            "EyeGone",
            false
        );
        ConfigTriedRollingLastStand = Config.Bind(
            "Body",
            "TriedRollingLastStand",
            false
        );
        ConfigSuccesfullyRolledLastStand = Config.Bind(
            "Body",
            "SuccesfullyRolledLastStand",
            false
        );
        ConfigLastStandTime = Config.Bind(
            "Body",
            "LastStandTime",
            -1000f
        );
    }

    [HarmonyPatch(typeof(Body), "Update")]
    public class BodyPatch
    {
        private static float _healTimer;

        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Global
        public static void Undead_Experiment()
        {
            Logger.LogInfo("111");
            _healTimer += Time.deltaTime;
            while (_healTimer >= ModConfigs.HealCountdown)
            {
                Heal();
                _healTimer -= ModConfigs.HealCountdown;
            }
        }
    }

    private static void Heal()
    {
        foreach (var limb in PlayerCamera.main.body.limbs)
        {
            limb.muscleHealth     = ModConfigs.MuscleHealth;
            limb.skinHealth       = ModConfigs.SkinHealth;
            limb.boneHealTimer    = ModConfigs.BoneHealTImer;
            limb.dislocationTimer = ModConfigs.DislocationTimer;
            limb.infectionAmount  = ModConfigs.InfectionAmount;
            limb.bleedAmount      = ModConfigs.BleedAmount;
            limb.pain             = ModConfigs.Painkillers;
            limb.shrapnel         = ModConfigs.Shrapnel;
            limb.infected         = ModConfigs.Infected;
            limb.dismembered      = ModConfigs.Dismembered;
            limb.blockedBleeding  = ModConfigs.BlockedBleeding;
        }

        var body = PlayerCamera.main.body;
        body.brainHealth                = ModConfigs.BrainHealth;
        body.bloodAmount                = ModConfigs.BloodAmount;
        body.bloodViscous               = ModConfigs.BloodViscous;
        body.oxygenAmount               = ModConfigs.OxygenAmount;
        body.hunger                     = ModConfigs.Hunger;
        body.thirst                     = ModConfigs.Thirst;
        body.temperature                = ModConfigs.Temperature;
        body.consciousness              = ModConfigs.Consciousness;
        body.stamina                    = ModConfigs.Stamina;
        body.energy                     = ModConfigs.Energy;
        body.sicknessAmount             = ModConfigs.SicknessAmount;
        body.septicShock                = ModConfigs.SepticShock;
        body.radiationSickness          = ModConfigs.RadiationSickness;
        body.traumaAmount               = ModConfigs.TraumaAmount;
        body.internalBleeding           = ModConfigs.InternalBleeding;
        body.hemothorax                 = ModConfigs.Hemothorax;
        body.dirtyness                  = ModConfigs.Dirtyness;
        body.wetness                    = ModConfigs.Wetness;
        body.happiness                  = ModConfigs.Happiness;
        body.antidepressantHappiness    = ModConfigs.AntidepressantHappiness;
        body.opiateHappiness            = ModConfigs.OpiateHappiness;
        body.hearingLoss                = ModConfigs.HearingLoss;
        body.weightOffset               = ModConfigs.WeightOffset;
        body.badSleepAmount             = ModConfigs.BadSleepAmount;
        body.adrenaline                 = ModConfigs.Adrenaline;
        body.curAdrenaline              = ModConfigs.CurAdrenaline;
        body.antibioticImmunityTime     = ModConfigs.AntibioticImmunityTime;
        body.brainGrowSickness          = ModConfigs.BrainGrowSickness;
        body.disfigured                 = ModConfigs.Disfigured;
        body.eyeGone                    = ModConfigs.EyeGone;
        body.triedRollingLastStand      = ModConfigs.TriedRollingLastStand;
        body.succesfullyRolledLastStand = ModConfigs.SuccesfullyRolledLastStand;
        body.lastStandTime              = ModConfigs.LastStandTime;

        if (PlayerCamera.main.body.TryGetComponent(out Painkillers component1))
            Destroy(component1);
        if (PlayerCamera.main.body.TryGetComponent(out SleepingPills component2))
            Destroy(component2);
        if (PlayerCamera.main.body.TryGetComponent(out Antidepressants component3))
            Destroy(component3);
        CoUtils.instance.Stop("bleach");
        CoUtils.instance.Stop("mercury");
    }
}