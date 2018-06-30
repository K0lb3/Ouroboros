namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class RaidResult
    {
        public QuestParam quest;
        public int pexp;
        public int uexp;
        public int gold;
        public List<UnitData> members;
        public List<RaidQuestResult> results;
        public QuestParam[] chquest;
        public string[] campaignIds;

        public RaidResult(PlayerPartyTypes type)
        {
            PartyData data;
            this.results = new List<RaidQuestResult>(10);
            this.chquest = new QuestParam[0];
            base..ctor();
            data = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(type);
            this.members = new List<UnitData>(data.MAX_UNIT);
            return;
        }
    }
}

