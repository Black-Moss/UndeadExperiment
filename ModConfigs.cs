namespace UndeadExperiment;

public static class ModConfigs
{
    // General
    public static readonly bool  UndeadMode    = Plugin.ConfigUndeadMode.Value;
    public static readonly float HealCountdown = Plugin.ConfigHealCountdown.Value;
    
    // Limb
    public static readonly float MuscleHealth     = Plugin.ConfigMuscleHeadth.Value;
    public static readonly float SkinHealth       = Plugin.ConfigSkinHealth.Value;
    public static readonly float BoneHealTImer    = Plugin.ConfigBoneHealTImer.Value;
    public static readonly float DislocationTimer = Plugin.ConfigDislocationTimer.Value;
    public static readonly float InfectionAmount  = Plugin.ConfigInfectionAmount.Value;
    public static readonly float BleedAmount      = Plugin.ConfigBleedAmount.Value;
    public static readonly float Painkillers      = Plugin.ConfigPainkillers.Value;
    public static readonly int   Shrapnel         = Plugin.ConfigShrapnel.Value;
    public static readonly bool  Infected         = Plugin.ConfigInfected.Value;
    public static readonly bool  Dismembered      = Plugin.ConfigDismembered.Value;
    public static readonly bool  BlockedBleeding  = Plugin.ConfigBlockedBleeding.Value;
    
    // Body
    public static readonly float BrainHealth                = Plugin.ConfigBrainHealth.Value;
    public static readonly float BloodAmount                = Plugin.ConfigBloodAmount.Value;
    public static readonly int   BloodViscous               = Plugin.ConfigBloodViscous.Value;
    public static readonly float OxygenAmount               = Plugin.ConfigOxygenAmount.Value;
    public static readonly float Hunger                     = Plugin.ConfigHunger.Value;
    public static readonly float Thirst                     = Plugin.ConfigThirst.Value;
    public static readonly float Temperature                = Plugin.ConfigTemperature.Value;
    public static readonly float Consciousness              = Plugin.ConfigConsciousness.Value;
    public static readonly float Stamina                    = Plugin.ConfigStamina.Value;
    public static readonly float Energy                     = Plugin.ConfigEnergy.Value;
    public static readonly float SicknessAmount             = Plugin.ConfigSicknessAmount.Value;
    public static readonly float SepticShock                = Plugin.ConfigSepticShock.Value;
    public static readonly float RadiationSickness          = Plugin.ConfigRadiationSickness.Value;
    public static readonly float TraumaAmount               = Plugin.ConfigTraumaAmount.Value;
    public static readonly float InternalBleeding           = Plugin.ConfigInternalBleeding.Value;
    public static readonly float Hemothorax                 = Plugin.ConfigHemothorax.Value;
    public static readonly float Dirtyness                  = Plugin.ConfigDirtyness.Value;
    public static readonly float Wetness                    = Plugin.ConfigWetness.Value;
    public static readonly float Happiness                  = Plugin.ConfigHappiness.Value;
    public static readonly float AntidepressantHappiness    = Plugin.ConfigAntidepressantHappiness.Value;
    public static readonly float OpiateHappiness            = Plugin.ConfigOpiateHappiness.Value;
    public static readonly float HearingLoss                = Plugin.ConfigHearingLoss.Value;
    public static readonly float WeightOffset               = Plugin.ConfigWeightOffset.Value;
    public static readonly float BadSleepAmount             = Plugin.ConfigBadSleepAmount.Value;
    public static readonly float Adrenaline                 = Plugin.ConfigAdrenaline.Value;
    public static readonly float CurAdrenaline              = Plugin.ConfigCurAdrenaline.Value;
    public static readonly float AntibioticImmunityTime     = Plugin.ConfigAntibioticImmunityTime.Value;
    public static readonly float BrainGrowSickness          = Plugin.ConfigBrainGrowSickness.Value;
    public static readonly bool  Disfigured                 = Plugin.ConfigDisfigured.Value;
    public static readonly bool  EyeGone                    = Plugin.ConfigEyeGone.Value;
    public static readonly bool  TriedRollingLastStand      = Plugin.ConfigTriedRollingLastStand.Value;
    public static readonly bool  SuccesfullyRolledLastStand = Plugin.ConfigSuccesfullyRolledLastStand.Value;
    public static readonly float LastStandTime              = Plugin.ConfigLastStandTime.Value;
}