namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    public class RankingQuestRewardParam
    {
        public int id;
        public RankingQuestRewardType type;
        public string iname;
        public int num;

        public RankingQuestRewardParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_RankingQuestRewardParam json)
        {
            this.id = json.id;
        Label_000C:
            try
            {
                this.type = (int) Enum.Parse(typeof(RankingQuestRewardType), json.type);
                goto Label_0041;
            }
            catch
            {
            Label_0031:
                DebugUtility.LogError("定義されていない列挙値が指定されようとしました");
                goto Label_0041;
            }
        Label_0041:
            this.iname = json.iname;
            this.num = json.num;
            return 1;
        }

        public static RankingQuestRewardParam FindByID(int id)
        {
            GameManager manager;
            <FindByID>c__AnonStorey2E7 storeye;
            storeye = new <FindByID>c__AnonStorey2E7();
            storeye.id = id;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0021;
            }
            return null;
        Label_0021:
            if (manager.RankingQuestRewardParams != null)
            {
                goto Label_002E;
            }
            return null;
        Label_002E:
            return manager.RankingQuestRewardParams.Find(new Predicate<RankingQuestRewardParam>(storeye.<>m__268));
        }

        [CompilerGenerated]
        private sealed class <FindByID>c__AnonStorey2E7
        {
            internal int id;

            public <FindByID>c__AnonStorey2E7()
            {
                base..ctor();
                return;
            }

            internal bool <>m__268(RankingQuestRewardParam param)
            {
                return (param.id == this.id);
            }
        }
    }
}

