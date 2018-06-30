namespace SRPG
{
    using System;

    public enum EMissionType
    {
        [TowerQuestMissionType(0), QuestMissionType(0)]
        KillAllEnemy = 0,
        [QuestMissionType(0), TowerQuestMissionType(6)]
        NoDeath = 1,
        [TowerQuestMissionType(3), QuestMissionType(1)]
        LimitedTurn = 2,
        [QuestMissionType(1)]
        ComboCount = 3,
        [TowerQuestMissionType(3), QuestMissionType(1)]
        MaxSkillCount = 4,
        [QuestMissionType(1)]
        MaxItemCount = 5,
        [QuestMissionType(1), TowerQuestMissionType(6)]
        MaxPartySize = 6,
        [QuestMissionType(3), TowerQuestMissionType(6)]
        LimitedUnitElement = 7,
        [TowerQuestMissionType(6), QuestMissionType(2)]
        LimitedUnitID = 8,
        [QuestMissionType(0)]
        NoMercenary = 9,
        [QuestMissionType(1), TowerQuestMissionType(0)]
        Killstreak = 10,
        [QuestMissionType(1), TowerQuestMissionType(3)]
        TotalHealHPMax = 11,
        [QuestMissionType(1), TowerQuestMissionType(1)]
        TotalHealHPMin = 12,
        [QuestMissionType(1), TowerQuestMissionType(3)]
        TotalDamagesTakenMax = 13,
        [QuestMissionType(1), TowerQuestMissionType(1)]
        TotalDamagesTakenMin = 14,
        [QuestMissionType(1), TowerQuestMissionType(3)]
        TotalDamagesMax = 15,
        [QuestMissionType(1), TowerQuestMissionType(1)]
        TotalDamagesMin = 0x10,
        [QuestMissionType(1), TowerQuestMissionType(0)]
        LimitedCT = 0x11,
        [QuestMissionType(1)]
        LimitedContinue = 0x12,
        [QuestMissionType(0)]
        NoNpcDeath = 0x13,
        [QuestMissionType(5), TowerQuestMissionType(0)]
        TargetKillstreak = 20,
        [QuestMissionType(2)]
        NoTargetDeath = 0x15,
        [TowerQuestMissionType(3), QuestMissionType(5)]
        BreakObjClashMax = 0x16,
        [QuestMissionType(5), TowerQuestMissionType(1)]
        BreakObjClashMin = 0x17,
        [QuestMissionType(2)]
        WithdrawUnit = 0x18,
        [QuestMissionType(0)]
        UseMercenary = 0x19,
        [QuestMissionType(2), TowerQuestMissionType(6)]
        LimitedUnitID_MainOnly = 0x1a,
        [QuestMissionType(0), TowerQuestMissionType(0)]
        MissionAllCompleteAtOnce = 0x1b,
        [QuestMissionType(4), TowerQuestMissionType(6)]
        OnlyTargetArtifactType = 0x1c,
        [QuestMissionType(4), TowerQuestMissionType(6)]
        OnlyTargetArtifactType_MainOnly = 0x1d,
        [QuestMissionType(4), TowerQuestMissionType(6)]
        OnlyTargetJobs = 30,
        [QuestMissionType(4), TowerQuestMissionType(6)]
        OnlyTargetJobs_MainOnly = 0x1f,
        [TowerQuestMissionType(6), QuestMissionType(2)]
        OnlyTargetUnitBirthplace = 0x20,
        [QuestMissionType(2), TowerQuestMissionType(6)]
        OnlyTargetUnitBirthplace_MainOnly = 0x21,
        [QuestMissionType(1), TowerQuestMissionType(6)]
        OnlyTargetSex = 0x22,
        [TowerQuestMissionType(6), QuestMissionType(1)]
        OnlyTargetSex_MainOnly = 0x23,
        [QuestMissionType(0), TowerQuestMissionType(6)]
        OnlyHeroUnit = 0x24,
        [TowerQuestMissionType(6), QuestMissionType(0)]
        OnlyHeroUnit_MainOnly = 0x25,
        [TowerQuestMissionType(6), QuestMissionType(2)]
        Finisher = 0x26,
        [QuestMissionType(1)]
        TotalGetTreasureCount = 0x27,
        [QuestMissionType(2)]
        KillstreakByUsingTargetItem = 40,
        [QuestMissionType(2)]
        KillstreakByUsingTargetSkill = 0x29,
        [TowerQuestMissionType(6), QuestMissionType(1)]
        MaxPartySize_IgnoreFriend = 0x2a,
        [QuestMissionType(0), TowerQuestMissionType(6)]
        NoAutoMode = 0x2b,
        [QuestMissionType(0)]
        NoDeath_NoContinue = 0x2c,
        [QuestMissionType(4)]
        OnlyTargetUnits = 0x2d,
        [QuestMissionType(4)]
        OnlyTargetUnits_MainOnly = 0x2e,
        [TowerQuestMissionType(3), QuestMissionType(1)]
        LimitedTurn_Leader = 0x2f,
        [QuestMissionType(4)]
        NoDeathTargetNpcUnits = 0x30,
        [QuestMissionType(1)]
        UseTargetSkill = 0x31,
        [QuestMissionType(1)]
        TotalKillstreakCount = 50,
        [TowerQuestMissionType(1), QuestMissionType(1)]
        TotalGetGemCount_Over = 0x33,
        [QuestMissionType(1), TowerQuestMissionType(3)]
        TotalGetGemCount_Less = 0x34,
        [QuestMissionType(1)]
        TeamPartySizeMax_IncMercenary = 0x35,
        [QuestMissionType(1)]
        TeamPartySizeMax_NoMercenary = 0x36,
        [TowerQuestMissionType(3), QuestMissionType(1)]
        ChallengeCountMax = 0x37,
        [TowerQuestMissionType(3), QuestMissionType(1)]
        DeathCountMax = 0x38
    }
}

