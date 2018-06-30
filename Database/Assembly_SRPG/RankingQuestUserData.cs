namespace SRPG
{
    using GR;
    using System;

    public class RankingQuestUserData
    {
        public string m_PlayerName;
        public int m_PlayerLevel;
        public int m_Rank;
        public UnitData m_UnitData;
        public int m_MainScore;
        public int m_SubScore;
        public RankingQuestType m_RankingType;
        public string m_UID;

        public RankingQuestUserData()
        {
            base..ctor();
            return;
        }

        public static RankingQuestUserData CreateRankingUserDataFromJson(FlowNode_ReqQuestRanking.Json_OwnRankingData json, RankingQuestType type)
        {
            RankingQuestUserData data;
            data = null;
            if (json == null)
            {
                goto Label_001C;
            }
            data = new RankingQuestUserData();
            data.Deserialize(json);
            data.m_RankingType = type;
        Label_001C:
            return data;
        }

        public static RankingQuestUserData[] CreateRankingUserDataFromJson(FlowNode_ReqQuestRanking.Json_OthersRankingData[] json, RankingQuestType type)
        {
            RankingQuestUserData[] dataArray;
            int num;
            dataArray = new RankingQuestUserData[0];
            if (json == null)
            {
                goto Label_0046;
            }
            dataArray = new RankingQuestUserData[(int) json.Length];
            num = 0;
            goto Label_003D;
        Label_001D:
            dataArray[num] = new RankingQuestUserData();
            dataArray[num].Deserialize(json[num]);
            dataArray[num].m_RankingType = type;
            num += 1;
        Label_003D:
            if (num < ((int) dataArray.Length))
            {
                goto Label_001D;
            }
        Label_0046:
            return dataArray;
        }

        public void Deserialize(FlowNode_ReqQuestRanking.Json_OthersRankingData json)
        {
            UnitBuilder builder;
            this.m_PlayerName = json.name;
            this.m_Rank = json.rank;
            this.m_MainScore = json.main_score;
            this.m_SubScore = json.sub_score;
            this.m_UID = json.uid;
            builder = new UnitBuilder(json.unit_iname);
            this.m_UnitData = builder.SetExpByLevel(json.unit_lv).SetJob(string.Empty, json.job_lv).SetAwake(0x19).SetRarity(5).SetUnlockTobiraNum(7).Build();
            return;
        }

        public void Deserialize(FlowNode_ReqQuestRanking.Json_OwnRankingData json)
        {
            GameManager manager;
            UnitBuilder builder;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            this.m_PlayerName = manager.Player.Name;
            this.m_Rank = json.rank;
            this.m_MainScore = json.main_score;
            this.m_SubScore = json.sub_score;
            this.m_UID = manager.DeviceId;
            builder = new UnitBuilder(json.unit.unit_iname);
            this.m_UnitData = builder.SetExpByLevel(json.unit.unit_lv).SetJob(string.Empty, json.unit.job_lv).SetAwake(0x19).SetRarity(5).SetUnlockTobiraNum(7).Build();
            return;
        }

        public bool IsActionCountRanking
        {
            get
            {
                return (this.m_RankingType == 1);
            }
        }
    }
}

