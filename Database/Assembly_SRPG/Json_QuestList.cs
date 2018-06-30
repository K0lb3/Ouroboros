namespace SRPG
{
    using System;

    public class Json_QuestList
    {
        public JSON_SectionParam[] worlds;
        public JSON_ChapterParam[] areas;
        public JSON_QuestParam[] quests;
        public JSON_ObjectiveParam[] objectives;
        public JSON_ObjectiveParam[] towerObjectives;
        public JSON_MagnificationParam[] magnifications;
        public JSON_QuestCondParam[] conditions;
        public JSON_QuestPartyParam[] parties;
        public JSON_QuestCampaignParentParam[] CampaignParents;
        public JSON_QuestCampaignChildParam[] CampaignChildren;
        public JSON_QuestCampaignTrust[] CampaignTrust;
        public JSON_TowerFloorParam[] towerFloors;
        public JSON_TowerRewardParam[] towerRewards;
        public JSON_TowerRoundRewardParam[] towerRoundRewards;
        public JSON_TowerParam[] towers;
        public JSON_VersusTowerParam[] versusTowerFloor;
        public JSON_VersusSchedule[] versusschedule;
        public JSON_VersusCoin[] versuscoin;
        public JSON_MultiTowerFloorParam[] multitowerFloor;
        public JSON_MultiTowerRewardParam[] multitowerRewards;
        public JSON_MapEffectParam[] MapEffect;
        public JSON_WeatherSetParam[] WeatherSet;
        public JSON_RankingQuestParam[] rankingQuests;
        public JSON_RankingQuestScheduleParam[] rankingQuestSchedule;
        public JSON_RankingQuestRewardParam[] rankingQuestRewards;
        public JSON_VersusFirstWinBonus[] versusfirstwinbonus;
        public JSON_VersusStreakWinSchedule[] versusstreakwinschedule;
        public JSON_VersusStreakWinBonus[] versusstreakwinbonus;
        public JSON_VersusRule[] versusrule;
        public JSON_VersusCoinCampParam[] versuscoincamp;
        public JSON_VersusEnableTimeParam[] versusenabletime;
        public JSON_VersusRankParam[] VersusRank;
        public JSON_VersusRankClassParam[] VersusRankClass;
        public JSON_VersusRankRankingRewardParam[] VersusRankRankingReward;
        public JSON_VersusRankRewardParam[] VersusRankReward;
        public JSON_VersusRankMissionScheduleParam[] VersusRankMissionSchedule;
        public JSON_VersusRankMissionParam[] VersusRankMission;
        public JSON_GuerrillaShopAdventQuestParam[] GuerrillaShopAdventQuest;
        public JSON_GuerrillaShopScheduleParam[] GuerrillaShopSchedule;
        public JSON_VersusDraftUnitParam[] VersusDraftUnit;

        public Json_QuestList()
        {
            base..ctor();
            return;
        }
    }
}

