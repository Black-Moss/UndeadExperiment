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
    public static ConfigEntry<float> HealCountdown;
    public static ConfigEntry<bool>  UndeadMode;
    public static ConfigEntry<bool>  SwitchModeTip;
    
    // Limb
    public static ConfigEntry<float> MuscleHeadth;
    public static ConfigEntry<float> SkinHealth;
    public static ConfigEntry<float> BoneHealTImer;
    public static ConfigEntry<float> DislocationTimer;
    public static ConfigEntry<float> InfectionAmount;
    public static ConfigEntry<float> BleedAmount;
    public static ConfigEntry<float> Painkillers;
    public static ConfigEntry<int>   Shrapnel;
    public static ConfigEntry<bool>  Infected;
    public static ConfigEntry<bool>  Dismembered;
    public static ConfigEntry<bool>  BlockedBleeding;
    
    // Body
    public static ConfigEntry<float> BrainHealth;
    public static ConfigEntry<float> BloodAmount;
    public static ConfigEntry<int>   BloodViscous;
    public static ConfigEntry<float> OxygenAmount;
    public static ConfigEntry<float> Hunger;
    public static ConfigEntry<float> Thirst;
    public static ConfigEntry<float> Temperature;
    public static ConfigEntry<float> Consciousness;
    public static ConfigEntry<float> Stamina;
    public static ConfigEntry<float> Energy;
    public static ConfigEntry<float> SicknessAmount;
    public static ConfigEntry<float> SepticShock;
    public static ConfigEntry<float> RadiationSickness;
    public static ConfigEntry<float> TraumaAmount;
    public static ConfigEntry<float> InternalBleeding;
    public static ConfigEntry<float> Hemothorax;
    public static ConfigEntry<float> Dirtyness;
    public static ConfigEntry<float> Wetness;
    public static ConfigEntry<float> Happiness;
    public static ConfigEntry<float> AntidepressantHappiness;
    public static ConfigEntry<float> OpiateHappiness;
    public static ConfigEntry<float> HearingLoss;
    public static ConfigEntry<float> WeightOffset;
    public static ConfigEntry<float> BadSleepAmount;
    public static ConfigEntry<float> Adrenaline;
    public static ConfigEntry<float> CurAdrenaline;
    public static ConfigEntry<float> AntibioticImmunityTime;
    public static ConfigEntry<float> BrainGrowSickness;
    public static ConfigEntry<bool>  Disfigured;
    public static ConfigEntry<bool>  EyeGone;
    public static ConfigEntry<bool>  TriedRollingLastStand;
    public static ConfigEntry<bool>  SuccesfullyRolledLastStand;
    public static ConfigEntry<float> LastStandTime;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        ModLocale.Initialize(Logger);
        ModCommand.Initialize(Logger);
        _harmony.PatchAll();
        
        //  General
        HealCountdown = Config.Bind(
            "General",
            "Heal Countdown",
            1f
        );
        UndeadMode = Config.Bind(
            "General",
            "Undead Mode",
            true
        );
        SwitchModeTip = Config.Bind(
            "General",
            "Switch Mode Tip",
            true
        );

        // Limb
        MuscleHeadth = Config.Bind(
            "Limb",
            "Muscle Health",
            100f
        );
        SkinHealth = Config.Bind(
            "Limb",
            "Skin Health",
            100f
        );
        BoneHealTImer = Config.Bind(
            "Limb",
            "Bone Heal TImer",
            0.0f
        );
        DislocationTimer = Config.Bind(
            "Limb",
            "Dislocation Timer",
            0.0f
        );
        InfectionAmount = Config.Bind(
            "Limb",
            "Infection Amount",
            0.0f
        );
        BleedAmount = Config.Bind(
            "Limb",
            "Bleed Amount",
            0.0f
        );
        Painkillers = Config.Bind(
            "Limb",
            "Painkillers",
            0.0f
        );
        Shrapnel = Config.Bind(
            "Limb",
            "Shrapnel",
            0
        );
        Infected = Config.Bind(
            "Limb",
            "Infected",
            false
        );
        Dismembered = Config.Bind(
            "Limb",
            "Dismembered",
            false
        );
        BlockedBleeding = Config.Bind(
            "Limb",
            "Blocked Bleeding",
            false
        );
        
        // Body
        BrainHealth = Config.Bind(
            "Body",
            "Brain Health",
            100f
        );
        BloodAmount = Config.Bind(
            "Body",
            "Blood Amount",
            100f
        );
        BloodViscous = Config.Bind(
            "Body",
            "Blood Viscous",
            0
        );
        OxygenAmount = Config.Bind(
            "Body",
            "Oxygen Amount",
            100f
        );
        Hunger = Config.Bind(
            "Body",
            "Hunger",
            100f
        );
        Thirst = Config.Bind(
            "Body",
            "Thirst",
            100f
        );
        Temperature = Config.Bind(
            "Body",
            "Temperature",
            37f
        );
        Consciousness = Config.Bind(
            "Body",
            "Consciousness",
            100f
        );
        Stamina = Config.Bind(
            "Body",
            "Stamina",
            100f
        );
        Energy = Config.Bind(
            "Body",
            "Energy",
            100f
        );
        SicknessAmount = Config.Bind(
            "Body",
            "Sickness Amount",
            0.0f
        );
        SepticShock = Config.Bind(
            "Body",
            "Septic Shock",
            0.0f
        );
        RadiationSickness = Config.Bind(
            "Body",
            "Radiation Sickness",
            0.0f
        );
        TraumaAmount = Config.Bind(
            "Body",
            "Trauma Amount",
            0.0f
        );
        InternalBleeding = Config.Bind(
            "Body",
            "Internal Bleeding",
            0.0f
        );
        Hemothorax = Config.Bind(
            "Body",
            "Hemothorax",
            0.0f
        );
        Dirtyness = Config.Bind(
            "Body",
            "Dirtyness",
            0.0f
        );
        Wetness = Config.Bind(
            "Body",
            "Wetness",
            0.0f
        );
        Happiness = Config.Bind(
            "Body",
            "Happiness",
            0.0f
        );
        AntidepressantHappiness = Config.Bind(
            "Body",
            "Antidepressant Happiness",
            0.0f
        );
        OpiateHappiness = Config.Bind(
            "Body",
            "Opiate Happiness",
            0.0f
        );
        HearingLoss = Config.Bind(
            "Body",
            "Hearing Loss",
            0.0f
        );
        WeightOffset = Config.Bind(
            "Body",
            "Weight Offset",
            0.0f
        );
        BadSleepAmount = Config.Bind(
            "Body",
            "BadSleep Amount",
            0.0f
        );
        Adrenaline = Config.Bind(
            "Body",
            "Adrenaline",
            0.0f
        );
        CurAdrenaline = Config.Bind(
            "Body",
            "Cur Adrenaline",
            0.0f
        );
        AntibioticImmunityTime = Config.Bind(
            "Body",
            "Antibiotic Immunity Time",
            0.0f
        );
        BrainGrowSickness = Config.Bind(
            "Body",
            "Brain Grow Sickness",
            0.0f
        );
        Disfigured = Config.Bind(
            "Body",
            "Disfigured",
            false
        );
        EyeGone = Config.Bind(
            "Body",
            "EyeGone",
            false
        );
        TriedRollingLastStand = Config.Bind(
            "Body",
            "Tried Rolling Last Stand",
            false
        );
        SuccesfullyRolledLastStand = Config.Bind(
            "Body",
            "Succesfully Rolled LastStand",
            false
        );
        LastStandTime = Config.Bind(
            "Body",
            "Last Stand Time",
            -1000f
        );
        
        ModConfigs.Update();
    }

    [HarmonyPatch(typeof(Body), "Update")]
    public class BodyPatch
    {
        private static float _healTimer;

        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Global
        public static void Undead_Experiment()
        {
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