namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class MasterParam
    {
        private SRPG.FixParam mFixParam;
        private List<UnitParam> mUnitParam;
        private List<UnitJobOverwriteParam> mUnitJobOverwriteParam;
        private List<SkillParam> mSkillParam;
        private List<BuffEffectParam> mBuffEffectParam;
        private List<CondEffectParam> mCondEffectParam;
        private List<AbilityParam> mAbilityParam;
        private List<ItemParam> mItemParam;
        private List<ArtifactParam> mArtifactParam;
        private List<WeaponParam> mWeaponParam;
        private List<RecipeParam> mRecipeParam;
        private List<JobParam> mJobParam;
        private Dictionary<string, JobParam> mJobParamDict;
        private List<QuestClearUnlockUnitDataParam> mUnlockUnitDataParam;
        private List<CollaboSkillParam> mCollaboSkillParam;
        private List<TrickParam> mTrickParam;
        private List<BreakObjParam> mBreakObjParam;
        private List<WeatherParam> mWeatherParam;
        private Dictionary<string, UnitUnlockTimeParam> mUnitUnlockTimeParam;
        private List<TobiraParam> mTobiraParam;
        private Dictionary<TobiraParam.Category, TobiraCategoriesParam> mTobiraCategoriesParam;
        private List<TobiraCondsParam> mTobiraCondParam;
        private Dictionary<string, TobiraCondsUnitParam> mTobiraCondUnitParam;
        private List<TobiraRecipeParam> mTobiraRecipeParam;
        private Dictionary<string, ConceptCardParam> mConceptCard;
        private OInt[,] mConceptCardLvTbl;
        private Dictionary<string, ConceptCardConditionsParam> mConceptCardConditions;
        private Dictionary<string, ConceptCardTrustRewardParam> mConceptCardTrustReward;
        private Dictionary<string, UnitGroupParam> mUnitGroup;
        private Dictionary<string, JobGroupParam> mJobGroup;
        private List<JobSetParam> mJobSetParam;
        private List<GrowParam> mGrowParam;
        private List<AIParam> mAIParam;
        private List<GeoParam> mGeoParam;
        private List<RarityParam> mRarityParam;
        private List<ShopParam> mShopParam;
        private PlayerParam[] mPlayerParamTbl;
        private OInt[] mPlayerExpTbl;
        private OInt[] mUnitExpTbl;
        private OInt[] mArtifactExpTbl;
        private OInt[] mAbilityExpTbl;
        private OInt[] mAwakePieceTbl;
        private SRPG.LocalNotificationParam mLocalNotificationParam;
        private TrophyCategoryParam[] mTrophyCategory;
        private ChallengeCategoryParam[] mChallengeCategory;
        private TrophyParam[] mTrophy;
        public Dictionary<string, TrophyParam> mTrophyInameDict;
        private TrophyObjective[][] mTrophyDict;
        private TrophyParam[] mChallenge;
        private UnlockParam[] mUnlock;
        private VipParam[] mVip;
        private JSON_StreamingMovie[] mStreamingMovies;
        private BannerParam[] mBanner;
        private List<VersusMatchingParam> mVersusMatching;
        private List<VersusMatchCondParam> mVersusMatchCond;
        private Dictionary<string, TowerScoreParam[]> mTowerScores;
        private OInt[] mTowerRankTbl;
        public OInt[] mMultiLimitUnitLv;
        private Dictionary<string, UnitParam> mUnitDictionary;
        private Dictionary<string, Dictionary<string, UnitJobOverwriteParam>> mUnitJobOverwriteDictionary;
        private Dictionary<string, SkillParam> mSkillDictionary;
        private Dictionary<string, AbilityParam> mAbilityDictionary;
        private Dictionary<string, ItemParam> mItemDictionary;
        private Dictionary<string, ArtifactParam> mArtifactDictionary;
        private List<AwardParam> mAwardParam;
        private Dictionary<string, AwardParam> mAwardDictionary;
        private Dictionary<string, List<JobSetParam>> mJobsetDictionary;
        private LoginInfoParam[] mLoginInfoParam;
        private Dictionary<string, FriendPresentItemParam> mFriendPresentItemParam;
        public StatusCoefficientParam mStatusCoefficient;
        private Dictionary<string, CustomTargetParam> mCustomTarget;
        public SkillAbilityDeriveParam[] mSkillAbilityDeriveParam;
        private List<SkillAbilityDeriveData> mSkillAbilityDerives;
        private TipsParam[] mTipsParam;
        [CompilerGenerated]
        private bool <Loaded>k__BackingField;
        [CompilerGenerated]
        private static Func<ArtifactData, bool> <>f__am$cache4B;
        [CompilerGenerated]
        private static Func<ArtifactData, string> <>f__am$cache4C;
        [CompilerGenerated]
        private static Func<string, SkillAbilityDeriveTriggerParam> <>f__am$cache4D;
        [CompilerGenerated]
        private static Comparison<SkillAbilityDeriveTriggerParam[]> <>f__am$cache4E;

        public MasterParam()
        {
            this.mFixParam = new SRPG.FixParam();
            this.mJobParamDict = new Dictionary<string, JobParam>();
            this.mTobiraParam = new List<TobiraParam>();
            this.mTobiraCategoriesParam = new Dictionary<TobiraParam.Category, TobiraCategoriesParam>();
            this.mTobiraCondParam = new List<TobiraCondsParam>();
            this.mTobiraCondUnitParam = new Dictionary<string, TobiraCondsUnitParam>();
            this.mTobiraRecipeParam = new List<TobiraRecipeParam>();
            this.mSkillAbilityDerives = new List<SkillAbilityDeriveData>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <CreateSkillAbilityDeriveDataWithArtifacts>m__259(ArtifactData artifact)
        {
            return ((artifact == null) == 0);
        }

        [CompilerGenerated]
        private static string <CreateSkillAbilityDeriveDataWithArtifacts>m__25A(ArtifactData artifact)
        {
            return artifact.ArtifactParam.iname;
        }

        [CompilerGenerated]
        private static SkillAbilityDeriveTriggerParam <CreateSkillAbilityDeriveDataWithArtifacts>m__25B(string iname)
        {
            return new SkillAbilityDeriveTriggerParam(1, iname);
        }

        [CompilerGenerated]
        private static unsafe int <CreateSkillAbilityDeriveDataWithArtifacts>m__25C(SkillAbilityDeriveTriggerParam[] triggers1, SkillAbilityDeriveTriggerParam[] triggers2)
        {
            int num;
            num = (int) triggers2.Length;
            return &num.CompareTo((int) triggers1.Length);
        }

        private unsafe void AddUnitToItem()
        {
            KeyValuePair<string, UnitParam> pair;
            Dictionary<string, UnitParam>.Enumerator enumerator;
            ItemParam param;
            ItemParam param2;
            if (this.mUnitDictionary.Count <= 0)
            {
                goto Label_0108;
            }
            enumerator = this.mUnitDictionary.GetEnumerator();
        Label_001D:
            try
            {
                goto Label_00EB;
            Label_0022:
                pair = &enumerator.Current;
                if (&pair.Value == null)
                {
                    goto Label_00EB;
                }
                if (string.IsNullOrEmpty(&pair.Value.piece) == null)
                {
                    goto Label_0051;
                }
                goto Label_00EB;
            Label_0051:
                param = new ItemParam();
                param.iname = &pair.Value.iname;
                if (this.mItemDictionary.ContainsKey(&pair.Value.piece) != null)
                {
                    goto Label_008A;
                }
                goto Label_00EB;
            Label_008A:
                param2 = this.mItemDictionary[&pair.Value.piece];
                if (param2 == null)
                {
                    goto Label_00EB;
                }
                param.name = &pair.Value.name;
                param.icon = param2.icon;
                param.type = 0x10;
                param.cap = 0x3e7;
                this.mItemDictionary.Add(param.iname, param);
            Label_00EB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0022;
                }
                goto Label_0108;
            }
            finally
            {
            Label_00FC:
                ((Dictionary<string, UnitParam>.Enumerator) enumerator).Dispose();
            }
        Label_0108:
            return;
        }

        public void CacheReferences()
        {
            int num;
            num = 0;
            goto Label_002E;
        Label_0007:
            if (this.mUnitParam[num] == null)
            {
                goto Label_002A;
            }
            this.mUnitParam[num].CacheReferences(this);
        Label_002A:
            num += 1;
        Label_002E:
            if (num < this.mUnitParam.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public int CalcConceptCardLevel(int rarity, int totalExp, int levelCap)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = levelCap;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            goto Label_0033;
        Label_000D:
            num2 += this.GetConceptCardNextExp(rarity, num4 + 1);
            if (num2 > totalExp)
            {
                goto Label_003A;
            }
            num3 += 1;
            goto Label_002F;
            goto Label_003A;
        Label_002F:
            num4 += 1;
        Label_0033:
            if (num4 < num)
            {
                goto Label_000D;
            }
        Label_003A:
            return Math.Min(Math.Max(num3, 1), num);
        }

        public int CalcUnitLevel(int totalExp, int levelCap)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = levelCap;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            goto Label_0032;
        Label_000D:
            num2 += this.GetUnitNextExp(num4 + 1);
            if (num2 > totalExp)
            {
                goto Label_0039;
            }
            num3 += 1;
            goto Label_002E;
            goto Label_0039;
        Label_002E:
            num4 += 1;
        Label_0032:
            if (num4 < num)
            {
                goto Label_000D;
            }
        Label_0039:
            return Math.Min(Math.Max(num3, 1), num);
        }

        public bool CanUnlockTobira(string unit_iname)
        {
            TobiraCondsParam param;
            <CanUnlockTobira>c__AnonStorey2D8 storeyd;
            storeyd = new <CanUnlockTobira>c__AnonStorey2D8();
            storeyd.unit_iname = unit_iname;
            if (string.IsNullOrEmpty(storeyd.unit_iname) == null)
            {
                goto Label_001F;
            }
            return 0;
        Label_001F:
            if (this.mTobiraCondParam != null)
            {
                goto Label_0041;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraConditionsForUnit no data!</color>", new object[0]));
            return 0;
        Label_0041:
            if (this.mTobiraCondParam.Find(new Predicate<TobiraCondsParam>(storeyd.<>m__24E)) != null)
            {
                goto Label_0061;
            }
            return 0;
        Label_0061:
            return 1;
        }

        public bool ContainsUnitID(string key)
        {
            return this.mUnitDictionary.ContainsKey(key);
        }

        public Dictionary<string, SkillAbilityDeriveData> CreateSkillAbilityDeriveDataWithArtifacts(JobData[] jobData)
        {
            Dictionary<string, SkillAbilityDeriveData> dictionary;
            int num;
            string[] strArray;
            SkillAbilityDeriveData data;
            if (jobData == null)
            {
                goto Label_000F;
            }
            if (((int) jobData.Length) >= 1)
            {
                goto Label_0011;
            }
        Label_000F:
            return null;
        Label_0011:
            dictionary = null;
            num = 0;
            goto Label_00AB;
        Label_001A:
            if (jobData[num] != null)
            {
                goto Label_0027;
            }
            goto Label_00A7;
        Label_0027:
            if (<>f__am$cache4B != null)
            {
                goto Label_0047;
            }
            <>f__am$cache4B = new Func<ArtifactData, bool>(MasterParam.<CreateSkillAbilityDeriveDataWithArtifacts>m__259);
        Label_0047:
            if (<>f__am$cache4C != null)
            {
                goto Label_0069;
            }
            <>f__am$cache4C = new Func<ArtifactData, string>(MasterParam.<CreateSkillAbilityDeriveDataWithArtifacts>m__25A);
        Label_0069:
            strArray = Enumerable.ToArray<string>(Enumerable.Select<ArtifactData, string>(Enumerable.Where<ArtifactData>(jobData[num].ArtifactDatas, <>f__am$cache4B), <>f__am$cache4C));
            data = this.CreateSkillAbilityDeriveDataWithArtifacts(strArray);
            if (data == null)
            {
                goto Label_00A7;
            }
            if (dictionary != null)
            {
                goto Label_0093;
            }
            dictionary = new Dictionary<string, SkillAbilityDeriveData>();
        Label_0093:
            dictionary.Add(jobData[num].Param.iname, data);
        Label_00A7:
            num += 1;
        Label_00AB:
            if (num < ((int) jobData.Length))
            {
                goto Label_001A;
            }
            return dictionary;
        }

        public unsafe SkillAbilityDeriveData CreateSkillAbilityDeriveDataWithArtifacts(string[] artifactInames)
        {
            List<SkillAbilityDeriveData> list;
            SkillAbilityDeriveTriggerParam[] paramArray;
            List<SkillAbilityDeriveTriggerParam[]> list2;
            SkillAbilityDeriveTriggerParam[] paramArray2;
            List<SkillAbilityDeriveTriggerParam[]>.Enumerator enumerator;
            SkillAbilityDeriveData data;
            List<SkillAbilityDeriveData>.Enumerator enumerator2;
            SkillAbilityDeriveData data2;
            SkillAbilityDeriveData data3;
            List<SkillAbilityDeriveParam> list3;
            SkillAbilityDeriveParam param;
            HashSet<SkillAbilityDeriveParam>.Enumerator enumerator3;
            int num;
            SkillAbilityDeriveParam param2;
            HashSet<SkillAbilityDeriveParam>.Enumerator enumerator4;
            if (((int) artifactInames.Length) >= 1)
            {
                goto Label_000B;
            }
            return null;
        Label_000B:
            list = new List<SkillAbilityDeriveData>();
            if (<>f__am$cache4D != null)
            {
                goto Label_002A;
            }
            <>f__am$cache4D = new Func<string, SkillAbilityDeriveTriggerParam>(MasterParam.<CreateSkillAbilityDeriveDataWithArtifacts>m__25B);
        Label_002A:
            list2 = SkillAbilityDeriveTriggerParam.CreateCombination(Enumerable.ToArray<SkillAbilityDeriveTriggerParam>(Enumerable.Select<string, SkillAbilityDeriveTriggerParam>(artifactInames, <>f__am$cache4D)));
            if (<>f__am$cache4E != null)
            {
                goto Label_005A;
            }
            <>f__am$cache4E = new Comparison<SkillAbilityDeriveTriggerParam[]>(MasterParam.<CreateSkillAbilityDeriveDataWithArtifacts>m__25C);
        Label_005A:
            list2.Sort(<>f__am$cache4E);
            enumerator = list2.GetEnumerator();
        Label_006C:
            try
            {
                goto Label_00E3;
            Label_0071:
                paramArray2 = &enumerator.Current;
                enumerator2 = this.mSkillAbilityDerives.GetEnumerator();
            Label_0086:
                try
                {
                    goto Label_00A9;
                Label_008B:
                    data = &enumerator2.Current;
                    if (data.CheckContainsTriggerInames(paramArray2) == null)
                    {
                        goto Label_00A9;
                    }
                    list.Add(data);
                Label_00A9:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_008B;
                    }
                    goto Label_00C7;
                }
                finally
                {
                Label_00BA:
                    ((List<SkillAbilityDeriveData>.Enumerator) enumerator2).Dispose();
                }
            Label_00C7:
                if (list.Count <= 0)
                {
                    goto Label_00E3;
                }
                if (((int) paramArray2.Length) != ((int) artifactInames.Length))
                {
                    goto Label_00E3;
                }
                goto Label_00EF;
            Label_00E3:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0071;
                }
            Label_00EF:
                goto Label_0101;
            }
            finally
            {
            Label_00F4:
                ((List<SkillAbilityDeriveTriggerParam[]>.Enumerator) enumerator).Dispose();
            }
        Label_0101:
            data2 = null;
            if (list.Count <= 0)
            {
                goto Label_0227;
            }
            data2 = new SkillAbilityDeriveData();
            data3 = list[0];
            list3 = new List<SkillAbilityDeriveParam>();
            enumerator3 = data3.m_AdditionalSkillAbilityDeriveParam.GetEnumerator();
        Label_0135:
            try
            {
                goto Label_015A;
            Label_013A:
                param = &enumerator3.Current;
                if (list3.Contains(param) != null)
                {
                    goto Label_015A;
                }
                list3.Add(param);
            Label_015A:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_013A;
                }
                goto Label_0178;
            }
            finally
            {
            Label_016B:
                ((HashSet<SkillAbilityDeriveParam>.Enumerator) enumerator3).Dispose();
            }
        Label_0178:
            num = 1;
            goto Label_020A;
        Label_0180:
            if (list3.Contains(list[num].m_SkillAbilityDeriveParam) != null)
            {
                goto Label_01AD;
            }
            list3.Add(list[num].m_SkillAbilityDeriveParam);
        Label_01AD:
            enumerator4 = list[num].m_AdditionalSkillAbilityDeriveParam.GetEnumerator();
        Label_01C1:
            try
            {
                goto Label_01E6;
            Label_01C6:
                param2 = &enumerator4.Current;
                if (list3.Contains(param2) != null)
                {
                    goto Label_01E6;
                }
                list3.Add(param2);
            Label_01E6:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_01C6;
                }
                goto Label_0204;
            }
            finally
            {
            Label_01F7:
                ((HashSet<SkillAbilityDeriveParam>.Enumerator) enumerator4).Dispose();
            }
        Label_0204:
            num += 1;
        Label_020A:
            if (num < list.Count)
            {
                goto Label_0180;
            }
            data2.Setup(data3.m_SkillAbilityDeriveParam, list3);
        Label_0227:
            return data2;
        }

        private void CreateTrophyDict()
        {
            List<TrophyObjective> list;
            Array array;
            int num;
            TrophyConditionTypes types;
            int num2;
            TrophyParam param;
            int num3;
            list = new List<TrophyObjective>((int) this.mTrophy.Length);
            array = Enum.GetValues(typeof(TrophyConditionTypes));
            this.mTrophyDict = new TrophyObjective[array.Length][];
            num = 0;
            goto Label_00C6;
        Label_0036:
            types = (int) array.GetValue(num);
            list.Clear();
            num2 = 0;
            goto Label_00A5;
        Label_0051:
            param = this.mTrophy[num2];
            num3 = 0;
            goto Label_008F;
        Label_0064:
            if (param.Objectives[num3].type != types)
            {
                goto Label_0089;
            }
            list.Add(param.Objectives[num3]);
        Label_0089:
            num3 += 1;
        Label_008F:
            if (num3 < ((int) param.Objectives.Length))
            {
                goto Label_0064;
            }
            num2 += 1;
        Label_00A5:
            if (num2 < ((int) this.mTrophy.Length))
            {
                goto Label_0051;
            }
            this.mTrophyDict[num] = list.ToArray();
            num += 1;
        Label_00C6:
            if (num < array.Length)
            {
                goto Label_0036;
            }
            return;
        }

        public unsafe bool Deserialize(JSON_MasterParam json)
        {
            int[][] numArrayArray1;
            int num;
            UnitParam param;
            JSON_UnitJobOverwriteParam param2;
            JSON_UnitJobOverwriteParam[] paramArray;
            int num2;
            UnitJobOverwriteParam param3;
            Dictionary<string, UnitJobOverwriteParam> dictionary;
            int num3;
            SkillParam param4;
            int num4;
            BuffEffectParam param5;
            int num5;
            CondEffectParam param6;
            int num6;
            AbilityParam param7;
            int num7;
            ItemParam param8;
            int num8;
            ArtifactParam param9;
            int num9;
            WeaponParam param10;
            int num10;
            JSON_RecipeParam param11;
            RecipeParam param12;
            int num11;
            JobParam param13;
            int num12;
            JobSetParam param14;
            List<JobSetParam> list;
            int num13;
            GrowParam param15;
            int num14;
            AIParam param16;
            int num15;
            GeoParam param17;
            int num16;
            RarityParam param18;
            int num17;
            ShopParam param19;
            int num18;
            JSON_PlayerParam param20;
            int num19;
            int num20;
            int num21;
            int num22;
            int num23;
            Dictionary<int, TrophyCategoryParam> dictionary2;
            List<TrophyCategoryParam> list2;
            int num24;
            TrophyCategoryParam param21;
            List<TrophyParam> list3;
            int num25;
            TrophyParam param22;
            TrophyParam param23;
            TrophyParam[] paramArray2;
            int num26;
            Dictionary<string, ChallengeCategoryParam> dictionary3;
            List<ChallengeCategoryParam> list4;
            int num27;
            ChallengeCategoryParam param24;
            List<TrophyParam> list5;
            int num28;
            TrophyParam param25;
            int num29;
            TrophyParam param26;
            TrophyParam[] paramArray3;
            int num30;
            List<UnlockParam> list6;
            int num31;
            UnlockParam param27;
            List<VipParam> list7;
            int num32;
            VipParam param28;
            int num33;
            List<BannerParam> list8;
            int num34;
            BannerParam param29;
            List<QuestClearUnlockUnitDataParam> list9;
            int num35;
            QuestClearUnlockUnitDataParam param30;
            int num36;
            AwardParam param31;
            List<LoginInfoParam> list10;
            int num37;
            LoginInfoParam param32;
            List<CollaboSkillParam> list11;
            int num38;
            CollaboSkillParam param33;
            List<TrickParam> list12;
            int num39;
            TrickParam param34;
            List<BreakObjParam> list13;
            int num40;
            BreakObjParam param35;
            int num41;
            VersusMatchingParam param36;
            int num42;
            VersusMatchCondParam param37;
            int num43;
            JSON_TowerScore score;
            int num44;
            TowerScoreParam[] paramArray4;
            int num45;
            JSON_TowerScoreThreshold threshold;
            int num46;
            int num47;
            int num48;
            FriendPresentItemParam param38;
            List<WeatherParam> list14;
            int num49;
            WeatherParam param39;
            int num50;
            UnitUnlockTimeParam param40;
            int num51;
            TobiraParam param41;
            int num52;
            TobiraCategoriesParam param42;
            int num53;
            TobiraCondsParam param43;
            int num54;
            TobiraCondsUnitParam param44;
            int num55;
            TobiraRecipeParam param45;
            int num56;
            ConceptCardParam param46;
            int[][] numArray;
            int num57;
            int num58;
            int num59;
            ConceptCardConditionsParam param47;
            int num60;
            ConceptCardTrustRewardParam param48;
            int num61;
            UnitGroupParam param49;
            int num62;
            JobGroupParam param50;
            int num63;
            CustomTargetParam param51;
            int num64;
            int num65;
            SkillAbilityDeriveData data;
            List<SkillAbilityDeriveParam> list15;
            int num66;
            <Deserialize>c__AnonStorey2C1 storeyc;
            <Deserialize>c__AnonStorey2C2 storeyc2;
            <Deserialize>c__AnonStorey2C3 storeyc3;
            <Deserialize>c__AnonStorey2C4 storeyc4;
            <Deserialize>c__AnonStorey2C5 storeyc5;
            <Deserialize>c__AnonStorey2C6 storeyc6;
            <Deserialize>c__AnonStorey2C7 storeyc7;
            <Deserialize>c__AnonStorey2C8 storeyc8;
            <Deserialize>c__AnonStorey2C9 storeyc9;
            <Deserialize>c__AnonStorey2CA storeyca;
            <Deserialize>c__AnonStorey2CB storeycb;
            <Deserialize>c__AnonStorey2CC storeycc;
            <Deserialize>c__AnonStorey2CD storeycd;
            <Deserialize>c__AnonStorey2CE storeyce;
            if (this.Loaded == null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            DebugUtility.Verify(json, typeof(JSON_MasterParam));
            this.mLocalNotificationParam = null;
            this.mFixParam.Deserialize(json.Fix[0]);
            if (json.Unit == null)
            {
                goto Label_0147;
            }
            if (this.mUnitParam != null)
            {
                goto Label_0061;
            }
            this.mUnitParam = new List<UnitParam>((int) json.Unit.Length);
        Label_0061:
            if (this.mUnitDictionary != null)
            {
                goto Label_007F;
            }
            this.mUnitDictionary = new Dictionary<string, UnitParam>((int) json.Unit.Length);
        Label_007F:
            num = 0;
            goto Label_0139;
        Label_0086:
            storeyc = new <Deserialize>c__AnonStorey2C1();
            storeyc.data = json.Unit[num];
            param = this.mUnitParam.Find(new Predicate<UnitParam>(storeyc.<>m__237));
            if (param != null)
            {
                goto Label_00CD;
            }
            param = new UnitParam();
            this.mUnitParam.Add(param);
        Label_00CD:
            param.Deserialize(storeyc.data);
            if (this.mUnitDictionary.ContainsKey(storeyc.data.iname) != null)
            {
                goto Label_0114;
            }
            this.mUnitDictionary.Add(storeyc.data.iname, param);
            goto Label_0135;
        Label_0114:
            throw new Exception("重複エラー：Unit[" + storeyc.data.iname + "]");
        Label_0135:
            num += 1;
        Label_0139:
            if (num < ((int) json.Unit.Length))
            {
                goto Label_0086;
            }
        Label_0147:
            if (json.UnitJobOverwrite == null)
            {
                goto Label_0215;
            }
            if (this.mUnitJobOverwriteParam != null)
            {
                goto Label_0168;
            }
            this.mUnitJobOverwriteParam = new List<UnitJobOverwriteParam>();
        Label_0168:
            if (this.mUnitJobOverwriteDictionary != null)
            {
                goto Label_017E;
            }
            this.mUnitJobOverwriteDictionary = new Dictionary<string, Dictionary<string, UnitJobOverwriteParam>>();
        Label_017E:
            paramArray = json.UnitJobOverwrite;
            num2 = 0;
            goto Label_020B;
        Label_018D:
            param2 = paramArray[num2];
            param3 = new UnitJobOverwriteParam();
            this.mUnitJobOverwriteParam.Add(param3);
            param3.Deserialize(param2);
            this.mUnitJobOverwriteDictionary.TryGetValue(param2.unit_iname, &dictionary);
            if (dictionary != null)
            {
                goto Label_01E4;
            }
            dictionary = new Dictionary<string, UnitJobOverwriteParam>();
            this.mUnitJobOverwriteDictionary.Add(param2.unit_iname, dictionary);
        Label_01E4:
            if (dictionary.ContainsKey(param2.job_iname) != null)
            {
                goto Label_0205;
            }
            dictionary.Add(param2.job_iname, param3);
        Label_0205:
            num2 += 1;
        Label_020B:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_018D;
            }
        Label_0215:
            if (json.Skill == null)
            {
                goto Label_033A;
            }
            if (this.mSkillParam != null)
            {
                goto Label_023E;
            }
            this.mSkillParam = new List<SkillParam>((int) json.Skill.Length);
        Label_023E:
            if (this.mSkillDictionary != null)
            {
                goto Label_025C;
            }
            this.mSkillDictionary = new Dictionary<string, SkillParam>((int) json.Skill.Length);
        Label_025C:
            num3 = 0;
            goto Label_0320;
        Label_0264:
            storeyc2 = new <Deserialize>c__AnonStorey2C2();
            storeyc2.data = json.Skill[num3];
            param4 = this.mSkillParam.Find(new Predicate<SkillParam>(storeyc2.<>m__238));
            if (param4 != null)
            {
                goto Label_02B0;
            }
            param4 = new SkillParam();
            this.mSkillParam.Add(param4);
        Label_02B0:
            param4.Deserialize(storeyc2.data);
            if (this.mSkillDictionary.ContainsKey(storeyc2.data.iname) != null)
            {
                goto Label_02F9;
            }
            this.mSkillDictionary.Add(storeyc2.data.iname, param4);
            goto Label_031A;
        Label_02F9:
            throw new Exception("重複エラー：Skill[" + storeyc2.data.iname + "]");
        Label_031A:
            num3 += 1;
        Label_0320:
            if (num3 < ((int) json.Skill.Length))
            {
                goto Label_0264;
            }
            SkillParam.UpdateReplaceSkill(this.mSkillParam);
        Label_033A:
            if (json.Buff == null)
            {
                goto Label_03DB;
            }
            if (this.mBuffEffectParam != null)
            {
                goto Label_0363;
            }
            this.mBuffEffectParam = new List<BuffEffectParam>((int) json.Buff.Length);
        Label_0363:
            num4 = 0;
            goto Label_03CC;
        Label_036B:
            storeyc3 = new <Deserialize>c__AnonStorey2C3();
            storeyc3.data = json.Buff[num4];
            param5 = this.mBuffEffectParam.Find(new Predicate<BuffEffectParam>(storeyc3.<>m__239));
            if (param5 != null)
            {
                goto Label_03B7;
            }
            param5 = new BuffEffectParam();
            this.mBuffEffectParam.Add(param5);
        Label_03B7:
            param5.Deserialize(storeyc3.data);
            num4 += 1;
        Label_03CC:
            if (num4 < ((int) json.Buff.Length))
            {
                goto Label_036B;
            }
        Label_03DB:
            if (json.Cond == null)
            {
                goto Label_047C;
            }
            if (this.mCondEffectParam != null)
            {
                goto Label_0404;
            }
            this.mCondEffectParam = new List<CondEffectParam>((int) json.Cond.Length);
        Label_0404:
            num5 = 0;
            goto Label_046D;
        Label_040C:
            storeyc4 = new <Deserialize>c__AnonStorey2C4();
            storeyc4.data = json.Cond[num5];
            param6 = this.mCondEffectParam.Find(new Predicate<CondEffectParam>(storeyc4.<>m__23A));
            if (param6 != null)
            {
                goto Label_0458;
            }
            param6 = new CondEffectParam();
            this.mCondEffectParam.Add(param6);
        Label_0458:
            param6.Deserialize(storeyc4.data);
            num5 += 1;
        Label_046D:
            if (num5 < ((int) json.Cond.Length))
            {
                goto Label_040C;
            }
        Label_047C:
            if (json.Ability == null)
            {
                goto Label_0596;
            }
            if (this.mAbilityParam != null)
            {
                goto Label_04A5;
            }
            this.mAbilityParam = new List<AbilityParam>((int) json.Ability.Length);
        Label_04A5:
            if (this.mAbilityDictionary != null)
            {
                goto Label_04C3;
            }
            this.mAbilityDictionary = new Dictionary<string, AbilityParam>((int) json.Ability.Length);
        Label_04C3:
            num6 = 0;
            goto Label_0587;
        Label_04CB:
            storeyc5 = new <Deserialize>c__AnonStorey2C5();
            storeyc5.data = json.Ability[num6];
            param7 = this.mAbilityParam.Find(new Predicate<AbilityParam>(storeyc5.<>m__23B));
            if (param7 != null)
            {
                goto Label_0517;
            }
            param7 = new AbilityParam();
            this.mAbilityParam.Add(param7);
        Label_0517:
            param7.Deserialize(storeyc5.data);
            if (this.mAbilityDictionary.ContainsKey(storeyc5.data.iname) != null)
            {
                goto Label_0560;
            }
            this.mAbilityDictionary.Add(storeyc5.data.iname, param7);
            goto Label_0581;
        Label_0560:
            throw new Exception("重複エラー：Ability[" + storeyc5.data.iname + "]");
        Label_0581:
            num6 += 1;
        Label_0587:
            if (num6 < ((int) json.Ability.Length))
            {
                goto Label_04CB;
            }
        Label_0596:
            if (json.Item == null)
            {
                goto Label_06C1;
            }
            if (this.mItemParam != null)
            {
                goto Label_05BF;
            }
            this.mItemParam = new List<ItemParam>((int) json.Item.Length);
        Label_05BF:
            if (this.mItemDictionary != null)
            {
                goto Label_05DD;
            }
            this.mItemDictionary = new Dictionary<string, ItemParam>((int) json.Item.Length);
        Label_05DD:
            num7 = 0;
            goto Label_06AC;
        Label_05E5:
            storeyc6 = new <Deserialize>c__AnonStorey2C6();
            storeyc6.data = json.Item[num7];
            param8 = this.mItemParam.Find(new Predicate<ItemParam>(storeyc6.<>m__23C));
            if (param8 != null)
            {
                goto Label_0631;
            }
            param8 = new ItemParam();
            this.mItemParam.Add(param8);
        Label_0631:
            param8.Deserialize(storeyc6.data);
            param8.no = num7 + 1;
            if (this.mItemDictionary.ContainsKey(storeyc6.data.iname) != null)
            {
                goto Label_0685;
            }
            this.mItemDictionary.Add(storeyc6.data.iname, param8);
            goto Label_06A6;
        Label_0685:
            throw new Exception("重複エラー：Item[" + storeyc6.data.iname + "]");
        Label_06A6:
            num7 += 1;
        Label_06AC:
            if (num7 < ((int) json.Item.Length))
            {
                goto Label_05E5;
            }
            this.AddUnitToItem();
        Label_06C1:
            if (json.Artifact == null)
            {
                goto Label_07F1;
            }
            if (this.mArtifactParam != null)
            {
                goto Label_06EA;
            }
            this.mArtifactParam = new List<ArtifactParam>((int) json.Artifact.Length);
        Label_06EA:
            if (this.mArtifactDictionary != null)
            {
                goto Label_0708;
            }
            this.mArtifactDictionary = new Dictionary<string, ArtifactParam>((int) json.Artifact.Length);
        Label_0708:
            num8 = 0;
            goto Label_07E2;
        Label_0710:
            storeyc7 = new <Deserialize>c__AnonStorey2C7();
            storeyc7.data = json.Artifact[num8];
            if (storeyc7.data.iname != null)
            {
                goto Label_073D;
            }
            goto Label_07DC;
        Label_073D:
            param9 = this.mArtifactParam.Find(new Predicate<ArtifactParam>(storeyc7.<>m__23D));
            if (param9 != null)
            {
                goto Label_0772;
            }
            param9 = new ArtifactParam();
            this.mArtifactParam.Add(param9);
        Label_0772:
            param9.Deserialize(storeyc7.data);
            if (this.mArtifactDictionary.ContainsKey(storeyc7.data.iname) != null)
            {
                goto Label_07BB;
            }
            this.mArtifactDictionary.Add(storeyc7.data.iname, param9);
            goto Label_07DC;
        Label_07BB:
            throw new Exception("重複エラー：Artifact[" + storeyc7.data.iname + "]");
        Label_07DC:
            num8 += 1;
        Label_07E2:
            if (num8 < ((int) json.Artifact.Length))
            {
                goto Label_0710;
            }
        Label_07F1:
            if (json.Weapon == null)
            {
                goto Label_0892;
            }
            if (this.mWeaponParam != null)
            {
                goto Label_081A;
            }
            this.mWeaponParam = new List<WeaponParam>((int) json.Weapon.Length);
        Label_081A:
            num9 = 0;
            goto Label_0883;
        Label_0822:
            storeyc8 = new <Deserialize>c__AnonStorey2C8();
            storeyc8.data = json.Weapon[num9];
            param10 = this.mWeaponParam.Find(new Predicate<WeaponParam>(storeyc8.<>m__23E));
            if (param10 != null)
            {
                goto Label_086E;
            }
            param10 = new WeaponParam();
            this.mWeaponParam.Add(param10);
        Label_086E:
            param10.Deserialize(storeyc8.data);
            num9 += 1;
        Label_0883:
            if (num9 < ((int) json.Weapon.Length))
            {
                goto Label_0822;
            }
        Label_0892:
            if (json.Recipe == null)
            {
                goto Label_0901;
            }
            if (this.mRecipeParam != null)
            {
                goto Label_08BB;
            }
            this.mRecipeParam = new List<RecipeParam>((int) json.Recipe.Length);
        Label_08BB:
            num10 = 0;
            goto Label_08F2;
        Label_08C3:
            param11 = json.Recipe[num10];
            param12 = new RecipeParam();
            this.mRecipeParam.Add(param12);
            param12.Deserialize(param11);
            num10 += 1;
        Label_08F2:
            if (num10 < ((int) json.Recipe.Length))
            {
                goto Label_08C3;
            }
        Label_0901:
            if (json.Job == null)
            {
                goto Label_09BC;
            }
            if (this.mJobParam != null)
            {
                goto Label_092A;
            }
            this.mJobParam = new List<JobParam>((int) json.Job.Length);
        Label_092A:
            num11 = 0;
            goto Label_09AD;
        Label_0932:
            storeyc9 = new <Deserialize>c__AnonStorey2C9();
            storeyc9.data = json.Job[num11];
            param13 = this.mJobParam.Find(new Predicate<JobParam>(storeyc9.<>m__23F));
            if (param13 != null)
            {
                goto Label_0997;
            }
            param13 = new JobParam();
            this.mJobParam.Add(param13);
            this.mJobParamDict[storeyc9.data.iname] = param13;
        Label_0997:
            param13.Deserialize(storeyc9.data, this);
            num11 += 1;
        Label_09AD:
            if (num11 < ((int) json.Job.Length))
            {
                goto Label_0932;
            }
        Label_09BC:
            if (json.JobSet == null)
            {
                goto Label_0AE4;
            }
            if (this.mJobSetParam != null)
            {
                goto Label_09E5;
            }
            this.mJobSetParam = new List<JobSetParam>((int) json.JobSet.Length);
        Label_09E5:
            if (this.mJobsetDictionary != null)
            {
                goto Label_0A03;
            }
            this.mJobsetDictionary = new Dictionary<string, List<JobSetParam>>((int) json.Unit.Length);
        Label_0A03:
            num12 = 0;
            goto Label_0AD5;
        Label_0A0B:
            storeyca = new <Deserialize>c__AnonStorey2CA();
            storeyca.data = json.JobSet[num12];
            param14 = this.mJobSetParam.Find(new Predicate<JobSetParam>(storeyca.<>m__240));
            if (param14 != null)
            {
                goto Label_0A57;
            }
            param14 = new JobSetParam();
            this.mJobSetParam.Add(param14);
        Label_0A57:
            param14.Deserialize(storeyca.data);
            if (string.IsNullOrEmpty(param14.target_unit) != null)
            {
                goto Label_0ACF;
            }
            list = null;
            if (this.mJobsetDictionary.ContainsKey(param14.target_unit) == null)
            {
                goto Label_0AAA;
            }
            list = this.mJobsetDictionary[param14.target_unit];
            goto Label_0AC6;
        Label_0AAA:
            list = new List<JobSetParam>(3);
            this.mJobsetDictionary.Add(param14.target_unit, list);
        Label_0AC6:
            list.Add(param14);
        Label_0ACF:
            num12 += 1;
        Label_0AD5:
            if (num12 < ((int) json.JobSet.Length))
            {
                goto Label_0A0B;
            }
        Label_0AE4:
            if (json.Grow == null)
            {
                goto Label_0B85;
            }
            if (this.mGrowParam != null)
            {
                goto Label_0B0D;
            }
            this.mGrowParam = new List<GrowParam>((int) json.Grow.Length);
        Label_0B0D:
            num13 = 0;
            goto Label_0B76;
        Label_0B15:
            storeycb = new <Deserialize>c__AnonStorey2CB();
            storeycb.data = json.Grow[num13];
            param15 = this.mGrowParam.Find(new Predicate<GrowParam>(storeycb.<>m__241));
            if (param15 != null)
            {
                goto Label_0B61;
            }
            param15 = new GrowParam();
            this.mGrowParam.Add(param15);
        Label_0B61:
            param15.Deserialize(storeycb.data);
            num13 += 1;
        Label_0B76:
            if (num13 < ((int) json.Grow.Length))
            {
                goto Label_0B15;
            }
        Label_0B85:
            if (json.AI == null)
            {
                goto Label_0C26;
            }
            if (this.mAIParam != null)
            {
                goto Label_0BAE;
            }
            this.mAIParam = new List<AIParam>((int) json.AI.Length);
        Label_0BAE:
            num14 = 0;
            goto Label_0C17;
        Label_0BB6:
            storeycc = new <Deserialize>c__AnonStorey2CC();
            storeycc.data = json.AI[num14];
            param16 = this.mAIParam.Find(new Predicate<AIParam>(storeycc.<>m__242));
            if (param16 != null)
            {
                goto Label_0C02;
            }
            param16 = new AIParam();
            this.mAIParam.Add(param16);
        Label_0C02:
            param16.Deserialize(storeycc.data);
            num14 += 1;
        Label_0C17:
            if (num14 < ((int) json.AI.Length))
            {
                goto Label_0BB6;
            }
        Label_0C26:
            if (json.Geo == null)
            {
                goto Label_0CC7;
            }
            if (this.mGeoParam != null)
            {
                goto Label_0C4F;
            }
            this.mGeoParam = new List<GeoParam>((int) json.Geo.Length);
        Label_0C4F:
            num15 = 0;
            goto Label_0CB8;
        Label_0C57:
            storeycd = new <Deserialize>c__AnonStorey2CD();
            storeycd.data = json.Geo[num15];
            param17 = this.mGeoParam.Find(new Predicate<GeoParam>(storeycd.<>m__243));
            if (param17 != null)
            {
                goto Label_0CA3;
            }
            param17 = new GeoParam();
            this.mGeoParam.Add(param17);
        Label_0CA3:
            param17.Deserialize(storeycd.data);
            num15 += 1;
        Label_0CB8:
            if (num15 < ((int) json.Geo.Length))
            {
                goto Label_0C57;
            }
        Label_0CC7:
            if (json.Rarity == null)
            {
                goto Label_0D5B;
            }
            if (this.mRarityParam != null)
            {
                goto Label_0CF0;
            }
            this.mRarityParam = new List<RarityParam>((int) json.Rarity.Length);
        Label_0CF0:
            num16 = 0;
            goto Label_0D4C;
        Label_0CF8:
            param18 = null;
            if (this.mRarityParam.Count <= num16)
            {
                goto Label_0D21;
            }
            param18 = this.mRarityParam[num16];
            goto Label_0D35;
        Label_0D21:
            param18 = new RarityParam();
            this.mRarityParam.Add(param18);
        Label_0D35:
            param18.Deserialize(json.Rarity[num16]);
            num16 += 1;
        Label_0D4C:
            if (num16 < ((int) json.Rarity.Length))
            {
                goto Label_0CF8;
            }
        Label_0D5B:
            if (json.Shop == null)
            {
                goto Label_0DEF;
            }
            if (this.mShopParam != null)
            {
                goto Label_0D84;
            }
            this.mShopParam = new List<ShopParam>((int) json.Shop.Length);
        Label_0D84:
            num17 = 0;
            goto Label_0DE0;
        Label_0D8C:
            param19 = null;
            if (this.mShopParam.Count <= num17)
            {
                goto Label_0DB5;
            }
            param19 = this.mShopParam[num17];
            goto Label_0DC9;
        Label_0DB5:
            param19 = new ShopParam();
            this.mShopParam.Add(param19);
        Label_0DC9:
            param19.Deserialize(json.Shop[num17]);
            num17 += 1;
        Label_0DE0:
            if (num17 < ((int) json.Shop.Length))
            {
                goto Label_0D8C;
            }
        Label_0DEF:
            if (json.Player == null)
            {
                goto Label_0E54;
            }
            this.mPlayerParamTbl = new PlayerParam[(int) json.Player.Length];
            num18 = 0;
            goto Label_0E45;
        Label_0E15:
            param20 = json.Player[num18];
            this.mPlayerParamTbl[num18] = new PlayerParam();
            this.mPlayerParamTbl[num18].Deserialize(param20);
            num18 += 1;
        Label_0E45:
            if (num18 < ((int) json.Player.Length))
            {
                goto Label_0E15;
            }
        Label_0E54:
            if (json.PlayerLvTbl == null)
            {
                goto Label_0EAF;
            }
            this.mPlayerExpTbl = new OInt[(int) json.PlayerLvTbl.Length];
            num19 = 0;
            goto Label_0EA0;
        Label_0E7A:
            *(&(this.mPlayerExpTbl[num19])) = json.PlayerLvTbl[num19];
            num19 += 1;
        Label_0EA0:
            if (num19 < ((int) json.PlayerLvTbl.Length))
            {
                goto Label_0E7A;
            }
        Label_0EAF:
            if (json.UnitLvTbl == null)
            {
                goto Label_0F0A;
            }
            this.mUnitExpTbl = new OInt[(int) json.UnitLvTbl.Length];
            num20 = 0;
            goto Label_0EFB;
        Label_0ED5:
            *(&(this.mUnitExpTbl[num20])) = json.UnitLvTbl[num20];
            num20 += 1;
        Label_0EFB:
            if (num20 < ((int) json.UnitLvTbl.Length))
            {
                goto Label_0ED5;
            }
        Label_0F0A:
            if (json.ArtifactLvTbl == null)
            {
                goto Label_0F65;
            }
            this.mArtifactExpTbl = new OInt[(int) json.ArtifactLvTbl.Length];
            num21 = 0;
            goto Label_0F56;
        Label_0F30:
            *(&(this.mArtifactExpTbl[num21])) = json.ArtifactLvTbl[num21];
            num21 += 1;
        Label_0F56:
            if (num21 < ((int) json.ArtifactLvTbl.Length))
            {
                goto Label_0F30;
            }
        Label_0F65:
            if (json.AbilityRank == null)
            {
                goto Label_0FC0;
            }
            this.mAbilityExpTbl = new OInt[(int) json.AbilityRank.Length];
            num22 = 0;
            goto Label_0FB1;
        Label_0F8B:
            *(&(this.mAbilityExpTbl[num22])) = json.AbilityRank[num22];
            num22 += 1;
        Label_0FB1:
            if (num22 < ((int) json.AbilityRank.Length))
            {
                goto Label_0F8B;
            }
        Label_0FC0:
            if (json.AwakePieceTbl == null)
            {
                goto Label_101B;
            }
            this.mAwakePieceTbl = new OInt[(int) json.AwakePieceTbl.Length];
            num23 = 0;
            goto Label_100C;
        Label_0FE6:
            *(&(this.mAwakePieceTbl[num23])) = json.AwakePieceTbl[num23];
            num23 += 1;
        Label_100C:
            if (num23 < ((int) json.AwakePieceTbl.Length))
            {
                goto Label_0FE6;
            }
        Label_101B:
            this.mLocalNotificationParam = new SRPG.LocalNotificationParam();
            if (json.LocalNotification == null)
            {
                goto Label_1079;
            }
            this.mLocalNotificationParam.msg_stamina = json.LocalNotification[0].msg_stamina;
            this.mLocalNotificationParam.iOSAct_stamina = json.LocalNotification[0].iOSAct_stamina;
            this.mLocalNotificationParam.limitSec_stamina = json.LocalNotification[0].limitSec_stamina;
        Label_1079:
            dictionary2 = new Dictionary<int, TrophyCategoryParam>();
            if (json.TrophyCategory == null)
            {
                goto Label_1111;
            }
            list2 = new List<TrophyCategoryParam>((int) json.TrophyCategory.Length);
            num24 = 0;
            goto Label_10F5;
        Label_10A2:
            param21 = new TrophyCategoryParam();
            if (param21.Deserialize(json.TrophyCategory[num24]) == null)
            {
                goto Label_10EF;
            }
            list2.Add(param21);
            if (dictionary2.ContainsKey(param21.hash_code) != null)
            {
                goto Label_10EF;
            }
            dictionary2.Add(param21.hash_code, param21);
        Label_10EF:
            num24 += 1;
        Label_10F5:
            if (num24 < ((int) json.TrophyCategory.Length))
            {
                goto Label_10A2;
            }
            this.mTrophyCategory = list2.ToArray();
        Label_1111:
            if (json.Trophy == null)
            {
                goto Label_1215;
            }
            list3 = new List<TrophyParam>((int) json.Trophy.Length);
            num25 = 0;
            goto Label_11B2;
        Label_1133:
            param22 = new TrophyParam();
            if (param22.Deserialize(json.Trophy[num25]) == null)
            {
                goto Label_11AC;
            }
            if (dictionary2.ContainsKey(param22.category_hash_code) == null)
            {
                goto Label_117C;
            }
            param22.CategoryParam = dictionary2[param22.category_hash_code];
            goto Label_1192;
        Label_117C:
            DebugUtility.LogError(param22.iname + " => 親カテゴリが未設定 or 入力ミス");
        Label_1192:
            if (param22.IsPlanningToUse() != null)
            {
                goto Label_11A3;
            }
            goto Label_11AC;
        Label_11A3:
            list3.Add(param22);
        Label_11AC:
            num25 += 1;
        Label_11B2:
            if (num25 < ((int) json.Trophy.Length))
            {
                goto Label_1133;
            }
            this.mTrophy = list3.ToArray();
            this.mTrophyInameDict = new Dictionary<string, TrophyParam>();
            paramArray2 = this.mTrophy;
            num26 = 0;
            goto Label_120A;
        Label_11E9:
            param23 = paramArray2[num26];
            this.mTrophyInameDict.Add(param23.iname, param23);
            num26 += 1;
        Label_120A:
            if (num26 < ((int) paramArray2.Length))
            {
                goto Label_11E9;
            }
        Label_1215:
            dictionary3 = new Dictionary<string, ChallengeCategoryParam>();
            if (json.ChallengeCategory == null)
            {
                goto Label_1295;
            }
            list4 = new List<ChallengeCategoryParam>((int) json.ChallengeCategory.Length);
            num27 = 0;
            goto Label_1279;
        Label_123E:
            param24 = new ChallengeCategoryParam();
            if (param24.Deserialize(json.ChallengeCategory[num27]) == null)
            {
                goto Label_1273;
            }
            dictionary3[param24.iname] = param24;
            list4.Add(param24);
        Label_1273:
            num27 += 1;
        Label_1279:
            if (num27 < ((int) json.ChallengeCategory.Length))
            {
                goto Label_123E;
            }
            this.mChallengeCategory = list4.ToArray();
        Label_1295:
            if (json.Challenge == null)
            {
                goto Label_13A6;
            }
            list5 = new List<TrophyParam>((int) json.Challenge.Length);
            num28 = 0;
            goto Label_1312;
        Label_12B7:
            param25 = new TrophyParam();
            if (param25.Deserialize(json.Challenge[num28]) == null)
            {
                goto Label_130C;
            }
            if (dictionary3.ContainsKey(param25.Category) == null)
            {
                goto Label_12FB;
            }
            param25.ChallengeCategoryParam = dictionary3[param25.Category];
        Label_12FB:
            param25.Challenge = 1;
            list5.Add(param25);
        Label_130C:
            num28 += 1;
        Label_1312:
            if (num28 < ((int) json.Challenge.Length))
            {
                goto Label_12B7;
            }
            this.mChallenge = list5.ToArray();
            num29 = (int) this.mTrophy.Length;
            Array.Resize<TrophyParam>(&this.mTrophy, num29 + ((int) this.mChallenge.Length));
            Array.Copy(this.mChallenge, 0, this.mTrophy, num29, (int) this.mChallenge.Length);
            paramArray3 = this.mChallenge;
            num30 = 0;
            goto Label_139B;
        Label_137A:
            param26 = paramArray3[num30];
            this.mTrophyInameDict.Add(param26.iname, param26);
            num30 += 1;
        Label_139B:
            if (num30 < ((int) paramArray3.Length))
            {
                goto Label_137A;
            }
        Label_13A6:
            this.CreateTrophyDict();
            if (json.Unlock == null)
            {
                goto Label_1415;
            }
            list6 = new List<UnlockParam>((int) json.Unlock.Length);
            num31 = 0;
            goto Label_13F9;
        Label_13CE:
            param27 = new UnlockParam();
            if (param27.Deserialize(json.Unlock[num31]) == null)
            {
                goto Label_13F3;
            }
            list6.Add(param27);
        Label_13F3:
            num31 += 1;
        Label_13F9:
            if (num31 < ((int) json.Unlock.Length))
            {
                goto Label_13CE;
            }
            this.mUnlock = list6.ToArray();
        Label_1415:
            if (json.Vip == null)
            {
                goto Label_147E;
            }
            list7 = new List<VipParam>((int) json.Vip.Length);
            num32 = 0;
            goto Label_1462;
        Label_1437:
            param28 = new VipParam();
            if (param28.Deserialize(json.Vip[num32]) == null)
            {
                goto Label_145C;
            }
            list7.Add(param28);
        Label_145C:
            num32 += 1;
        Label_1462:
            if (num32 < ((int) json.Vip.Length))
            {
                goto Label_1437;
            }
            this.mVip = list7.ToArray();
        Label_147E:
            if (json.Mov == null)
            {
                goto Label_14FF;
            }
            this.mStreamingMovies = new JSON_StreamingMovie[(int) json.Mov.Length];
            num33 = 0;
            goto Label_14F0;
        Label_14A4:
            this.mStreamingMovies[num33] = new JSON_StreamingMovie();
            this.mStreamingMovies[num33].alias = json.Mov[num33].alias;
            this.mStreamingMovies[num33].path = json.Mov[num33].path;
            num33 += 1;
        Label_14F0:
            if (num33 < ((int) json.Mov.Length))
            {
                goto Label_14A4;
            }
        Label_14FF:
            if (json.Banner == null)
            {
                goto Label_1568;
            }
            list8 = new List<BannerParam>((int) json.Banner.Length);
            num34 = 0;
            goto Label_154C;
        Label_1521:
            param29 = new BannerParam();
            if (param29.Deserialize(json.Banner[num34]) == null)
            {
                goto Label_1546;
            }
            list8.Add(param29);
        Label_1546:
            num34 += 1;
        Label_154C:
            if (num34 < ((int) json.Banner.Length))
            {
                goto Label_1521;
            }
            this.mBanner = list8.ToArray();
        Label_1568:
            if (json.QuestClearUnlockUnitData == null)
            {
                goto Label_15C7;
            }
            list9 = new List<QuestClearUnlockUnitDataParam>((int) json.QuestClearUnlockUnitData.Length);
            num35 = 0;
            goto Label_15B0;
        Label_158A:
            param30 = new QuestClearUnlockUnitDataParam();
            param30.Deserialize(json.QuestClearUnlockUnitData[num35]);
            list9.Add(param30);
            num35 += 1;
        Label_15B0:
            if (num35 < ((int) json.QuestClearUnlockUnitData.Length))
            {
                goto Label_158A;
            }
            this.mUnlockUnitDataParam = list9;
        Label_15C7:
            if (json.Award == null)
            {
                goto Label_16E8;
            }
            if (this.mAwardParam != null)
            {
                goto Label_15F0;
            }
            this.mAwardParam = new List<AwardParam>((int) json.Award.Length);
        Label_15F0:
            if (this.mAwardDictionary != null)
            {
                goto Label_160E;
            }
            this.mAwardDictionary = new Dictionary<string, AwardParam>((int) json.Award.Length);
        Label_160E:
            num36 = 0;
            goto Label_16D9;
        Label_1616:
            storeyce = new <Deserialize>c__AnonStorey2CE();
            storeyce.data = json.Award[num36];
            if (storeyce.data.iname != null)
            {
                goto Label_1643;
            }
            goto Label_16D3;
        Label_1643:
            param31 = this.mAwardParam.Find(new Predicate<AwardParam>(storeyce.<>m__244));
            if (param31 != null)
            {
                goto Label_1678;
            }
            param31 = new AwardParam();
            this.mAwardParam.Add(param31);
        Label_1678:
            param31.Deserialize(storeyce.data);
            if (this.mAwardDictionary.ContainsKey(param31.iname) != null)
            {
                goto Label_16B7;
            }
            this.mAwardDictionary.Add(param31.iname, param31);
            goto Label_16D3;
        Label_16B7:
            throw new Exception("Overlap : Award[" + param31.iname + "]");
        Label_16D3:
            num36 += 1;
        Label_16D9:
            if (num36 < ((int) json.Award.Length))
            {
                goto Label_1616;
            }
        Label_16E8:
            if (json.LoginInfo == null)
            {
                goto Label_1751;
            }
            list10 = new List<LoginInfoParam>((int) json.LoginInfo.Length);
            num37 = 0;
            goto Label_1735;
        Label_170A:
            param32 = new LoginInfoParam();
            if (param32.Deserialize(json.LoginInfo[num37]) == null)
            {
                goto Label_172F;
            }
            list10.Add(param32);
        Label_172F:
            num37 += 1;
        Label_1735:
            if (num37 < ((int) json.LoginInfo.Length))
            {
                goto Label_170A;
            }
            this.mLoginInfoParam = list10.ToArray();
        Label_1751:
            if (json.CollaboSkill == null)
            {
                goto Label_17BB;
            }
            list11 = new List<CollaboSkillParam>((int) json.CollaboSkill.Length);
            num38 = 0;
            goto Label_1799;
        Label_1773:
            param33 = new CollaboSkillParam();
            param33.Deserialize(json.CollaboSkill[num38]);
            list11.Add(param33);
            num38 += 1;
        Label_1799:
            if (num38 < ((int) json.CollaboSkill.Length))
            {
                goto Label_1773;
            }
            this.mCollaboSkillParam = list11;
            CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam);
        Label_17BB:
            if (json.Trick == null)
            {
                goto Label_181A;
            }
            list12 = new List<TrickParam>((int) json.Trick.Length);
            num39 = 0;
            goto Label_1803;
        Label_17DD:
            param34 = new TrickParam();
            param34.Deserialize(json.Trick[num39]);
            list12.Add(param34);
            num39 += 1;
        Label_1803:
            if (num39 < ((int) json.Trick.Length))
            {
                goto Label_17DD;
            }
            this.mTrickParam = list12;
        Label_181A:
            if (json.BreakObj == null)
            {
                goto Label_1879;
            }
            list13 = new List<BreakObjParam>((int) json.BreakObj.Length);
            num40 = 0;
            goto Label_1862;
        Label_183C:
            param35 = new BreakObjParam();
            param35.Deserialize(json.BreakObj[num40]);
            list13.Add(param35);
            num40 += 1;
        Label_1862:
            if (num40 < ((int) json.BreakObj.Length))
            {
                goto Label_183C;
            }
            this.mBreakObjParam = list13;
        Label_1879:
            if (json.VersusMatchKey == null)
            {
                goto Label_18D8;
            }
            this.mVersusMatching = new List<VersusMatchingParam>((int) json.VersusMatchKey.Length);
            num41 = 0;
            goto Label_18C9;
        Label_189F:
            param36 = new VersusMatchingParam();
            param36.Deserialize(json.VersusMatchKey[num41]);
            this.mVersusMatching.Add(param36);
            num41 += 1;
        Label_18C9:
            if (num41 < ((int) json.VersusMatchKey.Length))
            {
                goto Label_189F;
            }
        Label_18D8:
            if (json.VersusMatchCond == null)
            {
                goto Label_1937;
            }
            this.mVersusMatchCond = new List<VersusMatchCondParam>((int) json.VersusMatchCond.Length);
            num42 = 0;
            goto Label_1928;
        Label_18FE:
            param37 = new VersusMatchCondParam();
            param37.Deserialize(json.VersusMatchCond[num42]);
            this.mVersusMatchCond.Add(param37);
            num42 += 1;
        Label_1928:
            if (num42 < ((int) json.VersusMatchCond.Length))
            {
                goto Label_18FE;
            }
        Label_1937:
            if (json.TowerScore == null)
            {
                goto Label_19DF;
            }
            this.mTowerScores = new Dictionary<string, TowerScoreParam[]>((int) json.TowerScore.Length);
            num43 = 0;
            goto Label_19D0;
        Label_195D:
            score = json.TowerScore[num43];
            num44 = (int) score.threshold_vals.Length;
            paramArray4 = new TowerScoreParam[num44];
            num45 = 0;
            goto Label_19AD;
        Label_1984:
            threshold = score.threshold_vals[num45];
            paramArray4[num45] = new TowerScoreParam();
            paramArray4[num45].Deserialize(threshold);
            num45 += 1;
        Label_19AD:
            if (num45 < num44)
            {
                goto Label_1984;
            }
            this.mTowerScores.Add(score.iname, paramArray4);
            num43 += 1;
        Label_19D0:
            if (num43 < ((int) json.TowerScore.Length))
            {
                goto Label_195D;
            }
        Label_19DF:
            if (json.TowerRank == null)
            {
                goto Label_1A3A;
            }
            this.mTowerRankTbl = new OInt[(int) json.TowerRank.Length];
            num46 = 0;
            goto Label_1A2B;
        Label_1A05:
            *(&(this.mTowerRankTbl[num46])) = json.TowerRank[num46];
            num46 += 1;
        Label_1A2B:
            if (num46 < ((int) json.TowerRank.Length))
            {
                goto Label_1A05;
            }
        Label_1A3A:
            if (json.MultilimitUnitLv == null)
            {
                goto Label_1A95;
            }
            this.mMultiLimitUnitLv = new OInt[(int) json.MultilimitUnitLv.Length];
            num47 = 0;
            goto Label_1A86;
        Label_1A60:
            *(&(this.mMultiLimitUnitLv[num47])) = json.MultilimitUnitLv[num47];
            num47 += 1;
        Label_1A86:
            if (num47 < ((int) json.MultilimitUnitLv.Length))
            {
                goto Label_1A60;
            }
        Label_1A95:
            if (json.FriendPresentItem == null)
            {
                goto Label_1AF4;
            }
            this.mFriendPresentItemParam = new Dictionary<string, FriendPresentItemParam>();
            num48 = 0;
            goto Label_1AE5;
        Label_1AB3:
            param38 = new FriendPresentItemParam();
            param38.Deserialize(json.FriendPresentItem[num48], this);
            this.mFriendPresentItemParam.Add(param38.iname, param38);
            num48 += 1;
        Label_1AE5:
            if (num48 < ((int) json.FriendPresentItem.Length))
            {
                goto Label_1AB3;
            }
        Label_1AF4:
            if (json.Weather == null)
            {
                goto Label_1B53;
            }
            list14 = new List<WeatherParam>((int) json.Weather.Length);
            num49 = 0;
            goto Label_1B3C;
        Label_1B16:
            param39 = new WeatherParam();
            param39.Deserialize(json.Weather[num49]);
            list14.Add(param39);
            num49 += 1;
        Label_1B3C:
            if (num49 < ((int) json.Weather.Length))
            {
                goto Label_1B16;
            }
            this.mWeatherParam = list14;
        Label_1B53:
            if (json.UnitUnlockTime == null)
            {
                goto Label_1BB2;
            }
            this.mUnitUnlockTimeParam = new Dictionary<string, UnitUnlockTimeParam>();
            num50 = 0;
            goto Label_1BA3;
        Label_1B71:
            param40 = new UnitUnlockTimeParam();
            param40.Deserialize(json.UnitUnlockTime[num50]);
            this.mUnitUnlockTimeParam.Add(param40.iname, param40);
            num50 += 1;
        Label_1BA3:
            if (num50 < ((int) json.UnitUnlockTime.Length))
            {
                goto Label_1B71;
            }
        Label_1BB2:
            if (json.Tobira == null)
            {
                goto Label_1BFE;
            }
            num51 = 0;
            goto Label_1BEF;
        Label_1BC5:
            param41 = new TobiraParam();
            param41.Deserialize(json.Tobira[num51]);
            this.mTobiraParam.Add(param41);
            num51 += 1;
        Label_1BEF:
            if (num51 < ((int) json.Tobira.Length))
            {
                goto Label_1BC5;
            }
        Label_1BFE:
            if (json.TobiraCategories == null)
            {
                goto Label_1C51;
            }
            num52 = 0;
            goto Label_1C42;
        Label_1C11:
            param42 = new TobiraCategoriesParam();
            param42.Deserialize(json.TobiraCategories[num52]);
            this.mTobiraCategoriesParam.Add(param42.TobiraCategory, param42);
            num52 += 1;
        Label_1C42:
            if (num52 < ((int) json.TobiraCategories.Length))
            {
                goto Label_1C11;
            }
        Label_1C51:
            if (json.TobiraConds == null)
            {
                goto Label_1C9D;
            }
            num53 = 0;
            goto Label_1C8E;
        Label_1C64:
            param43 = new TobiraCondsParam();
            param43.Deserialize(json.TobiraConds[num53]);
            this.mTobiraCondParam.Add(param43);
            num53 += 1;
        Label_1C8E:
            if (num53 < ((int) json.TobiraConds.Length))
            {
                goto Label_1C64;
            }
        Label_1C9D:
            if (json.TobiraCondsUnit == null)
            {
                goto Label_1CF0;
            }
            num54 = 0;
            goto Label_1CE1;
        Label_1CB0:
            param44 = new TobiraCondsUnitParam();
            param44.Deserialize(json.TobiraCondsUnit[num54]);
            this.mTobiraCondUnitParam.Add(param44.Id, param44);
            num54 += 1;
        Label_1CE1:
            if (num54 < ((int) json.TobiraCondsUnit.Length))
            {
                goto Label_1CB0;
            }
        Label_1CF0:
            if (json.TobiraRecipe == null)
            {
                goto Label_1D3C;
            }
            num55 = 0;
            goto Label_1D2D;
        Label_1D03:
            param45 = new TobiraRecipeParam();
            param45.Deserialize(json.TobiraRecipe[num55]);
            this.mTobiraRecipeParam.Add(param45);
            num55 += 1;
        Label_1D2D:
            if (num55 < ((int) json.TobiraRecipe.Length))
            {
                goto Label_1D03;
            }
        Label_1D3C:
            if (json.ConceptCard == null)
            {
                goto Label_1D9C;
            }
            this.mConceptCard = new Dictionary<string, ConceptCardParam>();
            num56 = 0;
            goto Label_1D8D;
        Label_1D5A:
            param46 = new ConceptCardParam();
            param46.Deserialize(json.ConceptCard[num56], this);
            this.mConceptCard.Add(param46.iname, param46);
            num56 += 1;
        Label_1D8D:
            if (num56 < ((int) json.ConceptCard.Length))
            {
                goto Label_1D5A;
            }
        Label_1D9C:
            numArrayArray1 = new int[][] { json.ConceptCardLvTbl1, json.ConceptCardLvTbl2, json.ConceptCardLvTbl3, json.ConceptCardLvTbl4, json.ConceptCardLvTbl5, json.ConceptCardLvTbl6 };
            numArray = numArrayArray1;
            if (0 >= ((int) numArray.Length))
            {
                goto Label_1E56;
            }
            if (0 >= ((int) numArray[0].Length))
            {
                goto Label_1E56;
            }
            this.mConceptCardLvTbl = new OInt[(int) numArray.Length, (int) numArray[0].Length];
            num57 = 0;
            goto Label_1E4B;
        Label_1E0D:
            num58 = 0;
            goto Label_1E37;
        Label_1E15:
            this.mConceptCardLvTbl[num57, num58] = numArray[num57][num58];
            num58 += 1;
        Label_1E37:
            if (num58 < ((int) numArray[num57].Length))
            {
                goto Label_1E15;
            }
            num57 += 1;
        Label_1E4B:
            if (num57 < ((int) numArray.Length))
            {
                goto Label_1E0D;
            }
        Label_1E56:
            if (json.ConceptCardConditions == null)
            {
                goto Label_1EB5;
            }
            this.mConceptCardConditions = new Dictionary<string, ConceptCardConditionsParam>();
            num59 = 0;
            goto Label_1EA6;
        Label_1E74:
            param47 = new ConceptCardConditionsParam();
            param47.Deserialize(json.ConceptCardConditions[num59]);
            this.mConceptCardConditions.Add(param47.iname, param47);
            num59 += 1;
        Label_1EA6:
            if (num59 < ((int) json.ConceptCardConditions.Length))
            {
                goto Label_1E74;
            }
        Label_1EB5:
            if (json.ConceptCardTrustReward == null)
            {
                goto Label_1F14;
            }
            this.mConceptCardTrustReward = new Dictionary<string, ConceptCardTrustRewardParam>();
            num60 = 0;
            goto Label_1F05;
        Label_1ED3:
            param48 = new ConceptCardTrustRewardParam();
            param48.Deserialize(json.ConceptCardTrustReward[num60]);
            this.mConceptCardTrustReward.Add(param48.iname, param48);
            num60 += 1;
        Label_1F05:
            if (num60 < ((int) json.ConceptCardTrustReward.Length))
            {
                goto Label_1ED3;
            }
        Label_1F14:
            if (json.UnitGroup == null)
            {
                goto Label_1F73;
            }
            this.mUnitGroup = new Dictionary<string, UnitGroupParam>();
            num61 = 0;
            goto Label_1F64;
        Label_1F32:
            param49 = new UnitGroupParam();
            param49.Deserialize(json.UnitGroup[num61]);
            this.mUnitGroup.Add(param49.iname, param49);
            num61 += 1;
        Label_1F64:
            if (num61 < ((int) json.UnitGroup.Length))
            {
                goto Label_1F32;
            }
        Label_1F73:
            if (json.JobGroup == null)
            {
                goto Label_1FD2;
            }
            this.mJobGroup = new Dictionary<string, JobGroupParam>();
            num62 = 0;
            goto Label_1FC3;
        Label_1F91:
            param50 = new JobGroupParam();
            param50.Deserialize(json.JobGroup[num62]);
            this.mJobGroup.Add(param50.iname, param50);
            num62 += 1;
        Label_1FC3:
            if (num62 < ((int) json.JobGroup.Length))
            {
                goto Label_1F91;
            }
        Label_1FD2:
            if (json.StatusCoefficient == null)
            {
                goto Label_2009;
            }
            if (((int) json.StatusCoefficient.Length) <= 0)
            {
                goto Label_2009;
            }
            this.mStatusCoefficient = new StatusCoefficientParam();
            this.mStatusCoefficient.Deserialize(json.StatusCoefficient[0]);
        Label_2009:
            if (json.CustomTarget == null)
            {
                goto Label_2068;
            }
            this.mCustomTarget = new Dictionary<string, CustomTargetParam>();
            num63 = 0;
            goto Label_2059;
        Label_2027:
            param51 = new CustomTargetParam();
            param51.Deserialize(json.CustomTarget[num63]);
            this.mCustomTarget.Add(param51.iname, param51);
            num63 += 1;
        Label_2059:
            if (num63 < ((int) json.CustomTarget.Length))
            {
                goto Label_2027;
            }
        Label_2068:
            if (json.SkillAbilityDerive == null)
            {
                goto Label_212D;
            }
            if (((int) json.SkillAbilityDerive.Length) <= 0)
            {
                goto Label_212D;
            }
            this.mSkillAbilityDeriveParam = new SkillAbilityDeriveParam[(int) json.SkillAbilityDerive.Length];
            num64 = 0;
            goto Label_20CA;
        Label_209C:
            this.mSkillAbilityDeriveParam[num64] = new SkillAbilityDeriveParam(num64);
            this.mSkillAbilityDeriveParam[num64].Deserialize(json.SkillAbilityDerive[num64], this);
            num64 += 1;
        Label_20CA:
            if (num64 < ((int) json.SkillAbilityDerive.Length))
            {
                goto Label_209C;
            }
            num65 = 0;
            goto Label_211E;
        Label_20E1:
            data = new SkillAbilityDeriveData();
            list15 = this.FindAditionalSkillAbilityDeriveParam(this.mSkillAbilityDeriveParam[num65]);
            data.Setup(this.mSkillAbilityDeriveParam[num65], list15);
            this.mSkillAbilityDerives.Add(data);
            num65 += 1;
        Label_211E:
            if (num65 < ((int) this.mSkillAbilityDeriveParam.Length))
            {
                goto Label_20E1;
            }
        Label_212D:
            if (json.Tips == null)
            {
                goto Label_219B;
            }
            if (((int) json.Tips.Length) <= 0)
            {
                goto Label_219B;
            }
            this.mTipsParam = new TipsParam[(int) json.Tips.Length];
            num66 = 0;
            goto Label_218C;
        Label_2161:
            this.mTipsParam[num66] = new TipsParam();
            this.mTipsParam[num66].Deserialize(json.Tips[num66]);
            num66 += 1;
        Label_218C:
            if (num66 < ((int) json.Tips.Length))
            {
                goto Label_2161;
            }
        Label_219B:
            this.Loaded = 1;
            return 1;
        }

        public unsafe bool Deserialize2(JSON_MasterParam json)
        {
            int[][] numArrayArray1;
            int num;
            JSON_UnitParam param;
            UnitParam param2;
            JSON_UnitJobOverwriteParam param3;
            JSON_UnitJobOverwriteParam[] paramArray;
            int num2;
            UnitJobOverwriteParam param4;
            Dictionary<string, UnitJobOverwriteParam> dictionary;
            int num3;
            JSON_SkillParam param5;
            SkillParam param6;
            int num4;
            JSON_BuffEffectParam param7;
            BuffEffectParam param8;
            int num5;
            JSON_CondEffectParam param9;
            CondEffectParam param10;
            int num6;
            JSON_AbilityParam param11;
            AbilityParam param12;
            int num7;
            JSON_ItemParam param13;
            ItemParam param14;
            int num8;
            JSON_ArtifactParam param15;
            ArtifactParam param16;
            int num9;
            JSON_WeaponParam param17;
            WeaponParam param18;
            int num10;
            JSON_RecipeParam param19;
            RecipeParam param20;
            int num11;
            JSON_JobParam param21;
            JobParam param22;
            int num12;
            JSON_JobSetParam param23;
            JobSetParam param24;
            List<JobSetParam> list;
            int num13;
            JSON_GrowParam param25;
            GrowParam param26;
            int num14;
            JSON_AIParam param27;
            AIParam param28;
            int num15;
            JSON_GeoParam param29;
            GeoParam param30;
            int num16;
            RarityParam param31;
            int num17;
            ShopParam param32;
            int num18;
            JSON_PlayerParam param33;
            int num19;
            int num20;
            int num21;
            int num22;
            int num23;
            Dictionary<int, TrophyCategoryParam> dictionary2;
            List<TrophyCategoryParam> list2;
            int num24;
            TrophyCategoryParam param34;
            List<TrophyParam> list3;
            int num25;
            TrophyParam param35;
            TrophyParam param36;
            TrophyParam[] paramArray2;
            int num26;
            Dictionary<string, ChallengeCategoryParam> dictionary3;
            List<ChallengeCategoryParam> list4;
            int num27;
            ChallengeCategoryParam param37;
            List<TrophyParam> list5;
            int num28;
            TrophyParam param38;
            int num29;
            TrophyParam param39;
            TrophyParam[] paramArray3;
            int num30;
            List<UnlockParam> list6;
            int num31;
            UnlockParam param40;
            List<VipParam> list7;
            int num32;
            VipParam param41;
            int num33;
            List<BannerParam> list8;
            int num34;
            BannerParam param42;
            List<QuestClearUnlockUnitDataParam> list9;
            int num35;
            QuestClearUnlockUnitDataParam param43;
            int num36;
            JSON_AwardParam param44;
            AwardParam param45;
            List<LoginInfoParam> list10;
            int num37;
            LoginInfoParam param46;
            List<CollaboSkillParam> list11;
            int num38;
            CollaboSkillParam param47;
            List<TrickParam> list12;
            int num39;
            TrickParam param48;
            List<BreakObjParam> list13;
            int num40;
            BreakObjParam param49;
            int num41;
            VersusMatchingParam param50;
            int num42;
            VersusMatchCondParam param51;
            int num43;
            JSON_TowerScore score;
            int num44;
            TowerScoreParam[] paramArray4;
            int num45;
            JSON_TowerScoreThreshold threshold;
            int num46;
            int num47;
            int num48;
            FriendPresentItemParam param52;
            List<WeatherParam> list14;
            int num49;
            WeatherParam param53;
            int num50;
            UnitUnlockTimeParam param54;
            int num51;
            TobiraParam param55;
            int num52;
            TobiraCategoriesParam param56;
            int num53;
            TobiraCondsParam param57;
            int num54;
            TobiraCondsUnitParam param58;
            int num55;
            TobiraRecipeParam param59;
            int num56;
            ConceptCardParam param60;
            int[][] numArray;
            int num57;
            int num58;
            int num59;
            ConceptCardConditionsParam param61;
            int num60;
            ConceptCardTrustRewardParam param62;
            int num61;
            UnitGroupParam param63;
            int num62;
            JobGroupParam param64;
            int num63;
            CustomTargetParam param65;
            int num64;
            int num65;
            SkillAbilityDeriveData data;
            List<SkillAbilityDeriveParam> list15;
            int num66;
            if (this.Loaded == null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            DebugUtility.Verify(json, typeof(JSON_MasterParam));
            this.mLocalNotificationParam = null;
            this.mFixParam.Deserialize(json.Fix[0]);
            if (json.Unit == null)
            {
                goto Label_00CD;
            }
            if (this.mUnitParam != null)
            {
                goto Label_0061;
            }
            this.mUnitParam = new List<UnitParam>((int) json.Unit.Length);
        Label_0061:
            if (this.mUnitDictionary != null)
            {
                goto Label_007F;
            }
            this.mUnitDictionary = new Dictionary<string, UnitParam>((int) json.Unit.Length);
        Label_007F:
            num = 0;
            goto Label_00BF;
        Label_0086:
            param = json.Unit[num];
            param2 = new UnitParam();
            this.mUnitParam.Add(param2);
            param2.Deserialize(param);
            this.mUnitDictionary.Add(param.iname, param2);
            num += 1;
        Label_00BF:
            if (num < ((int) json.Unit.Length))
            {
                goto Label_0086;
            }
        Label_00CD:
            if (json.UnitJobOverwrite == null)
            {
                goto Label_019E;
            }
            if (this.mUnitJobOverwriteParam != null)
            {
                goto Label_00EE;
            }
            this.mUnitJobOverwriteParam = new List<UnitJobOverwriteParam>();
        Label_00EE:
            if (this.mUnitJobOverwriteDictionary != null)
            {
                goto Label_0104;
            }
            this.mUnitJobOverwriteDictionary = new Dictionary<string, Dictionary<string, UnitJobOverwriteParam>>();
        Label_0104:
            paramArray = json.UnitJobOverwrite;
            num2 = 0;
            goto Label_0193;
        Label_0114:
            param3 = paramArray[num2];
            param4 = new UnitJobOverwriteParam();
            this.mUnitJobOverwriteParam.Add(param4);
            param4.Deserialize(param3);
            this.mUnitJobOverwriteDictionary.TryGetValue(param3.unit_iname, &dictionary);
            if (dictionary != null)
            {
                goto Label_016C;
            }
            dictionary = new Dictionary<string, UnitJobOverwriteParam>();
            this.mUnitJobOverwriteDictionary.Add(param3.unit_iname, dictionary);
        Label_016C:
            if (dictionary.ContainsKey(param3.job_iname) != null)
            {
                goto Label_018D;
            }
            dictionary.Add(param3.job_iname, param4);
        Label_018D:
            num2 += 1;
        Label_0193:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0114;
            }
        Label_019E:
            if (json.Skill == null)
            {
                goto Label_024A;
            }
            if (this.mSkillParam != null)
            {
                goto Label_01C7;
            }
            this.mSkillParam = new List<SkillParam>((int) json.Skill.Length);
        Label_01C7:
            if (this.mSkillDictionary != null)
            {
                goto Label_01E5;
            }
            this.mSkillDictionary = new Dictionary<string, SkillParam>((int) json.Skill.Length);
        Label_01E5:
            num3 = 0;
            goto Label_0230;
        Label_01ED:
            param5 = json.Skill[num3];
            param6 = new SkillParam();
            this.mSkillParam.Add(param6);
            param6.Deserialize(param5);
            this.mSkillDictionary.Add(param5.iname, param6);
            num3 += 1;
        Label_0230:
            if (num3 < ((int) json.Skill.Length))
            {
                goto Label_01ED;
            }
            SkillParam.UpdateReplaceSkill(this.mSkillParam);
        Label_024A:
            if (json.Buff == null)
            {
                goto Label_02B9;
            }
            if (this.mBuffEffectParam != null)
            {
                goto Label_0273;
            }
            this.mBuffEffectParam = new List<BuffEffectParam>((int) json.Buff.Length);
        Label_0273:
            num4 = 0;
            goto Label_02AA;
        Label_027B:
            param7 = json.Buff[num4];
            param8 = new BuffEffectParam();
            this.mBuffEffectParam.Add(param8);
            param8.Deserialize(param7);
            num4 += 1;
        Label_02AA:
            if (num4 < ((int) json.Buff.Length))
            {
                goto Label_027B;
            }
        Label_02B9:
            if (json.Cond == null)
            {
                goto Label_0328;
            }
            if (this.mCondEffectParam != null)
            {
                goto Label_02E2;
            }
            this.mCondEffectParam = new List<CondEffectParam>((int) json.Cond.Length);
        Label_02E2:
            num5 = 0;
            goto Label_0319;
        Label_02EA:
            param9 = json.Cond[num5];
            param10 = new CondEffectParam();
            this.mCondEffectParam.Add(param10);
            param10.Deserialize(param9);
            num5 += 1;
        Label_0319:
            if (num5 < ((int) json.Cond.Length))
            {
                goto Label_02EA;
            }
        Label_0328:
            if (json.Ability == null)
            {
                goto Label_03C9;
            }
            if (this.mAbilityParam != null)
            {
                goto Label_0351;
            }
            this.mAbilityParam = new List<AbilityParam>((int) json.Ability.Length);
        Label_0351:
            if (this.mAbilityDictionary != null)
            {
                goto Label_036F;
            }
            this.mAbilityDictionary = new Dictionary<string, AbilityParam>((int) json.Ability.Length);
        Label_036F:
            num6 = 0;
            goto Label_03BA;
        Label_0377:
            param11 = json.Ability[num6];
            param12 = new AbilityParam();
            this.mAbilityParam.Add(param12);
            param12.Deserialize(param11);
            this.mAbilityDictionary.Add(param11.iname, param12);
            num6 += 1;
        Label_03BA:
            if (num6 < ((int) json.Ability.Length))
            {
                goto Label_0377;
            }
        Label_03C9:
            if (json.Item == null)
            {
                goto Label_047B;
            }
            if (this.mItemParam != null)
            {
                goto Label_03F2;
            }
            this.mItemParam = new List<ItemParam>((int) json.Item.Length);
        Label_03F2:
            if (this.mItemDictionary != null)
            {
                goto Label_0410;
            }
            this.mItemDictionary = new Dictionary<string, ItemParam>((int) json.Item.Length);
        Label_0410:
            num7 = 0;
            goto Label_0466;
        Label_0418:
            param13 = json.Item[num7];
            param14 = new ItemParam();
            this.mItemParam.Add(param14);
            param14.Deserialize(param13);
            param14.no = num7 + 1;
            this.mItemDictionary.Add(param13.iname, param14);
            num7 += 1;
        Label_0466:
            if (num7 < ((int) json.Item.Length))
            {
                goto Label_0418;
            }
            this.AddUnitToItem();
        Label_047B:
            if (json.Artifact == null)
            {
                goto Label_052D;
            }
            if (this.mArtifactParam != null)
            {
                goto Label_04A4;
            }
            this.mArtifactParam = new List<ArtifactParam>((int) json.Artifact.Length);
        Label_04A4:
            if (this.mArtifactDictionary != null)
            {
                goto Label_04C2;
            }
            this.mArtifactDictionary = new Dictionary<string, ArtifactParam>((int) json.Artifact.Length);
        Label_04C2:
            num8 = 0;
            goto Label_051E;
        Label_04CA:
            param15 = json.Artifact[num8];
            if (param15.iname != null)
            {
                goto Label_04E6;
            }
            goto Label_0518;
        Label_04E6:
            param16 = new ArtifactParam();
            this.mArtifactParam.Add(param16);
            param16.Deserialize(param15);
            this.mArtifactDictionary.Add(param15.iname, param16);
        Label_0518:
            num8 += 1;
        Label_051E:
            if (num8 < ((int) json.Artifact.Length))
            {
                goto Label_04CA;
            }
        Label_052D:
            if (json.Weapon == null)
            {
                goto Label_059C;
            }
            if (this.mWeaponParam != null)
            {
                goto Label_0556;
            }
            this.mWeaponParam = new List<WeaponParam>((int) json.Weapon.Length);
        Label_0556:
            num9 = 0;
            goto Label_058D;
        Label_055E:
            param17 = json.Weapon[num9];
            param18 = new WeaponParam();
            this.mWeaponParam.Add(param18);
            param18.Deserialize(param17);
            num9 += 1;
        Label_058D:
            if (num9 < ((int) json.Weapon.Length))
            {
                goto Label_055E;
            }
        Label_059C:
            if (json.Recipe == null)
            {
                goto Label_060B;
            }
            if (this.mRecipeParam != null)
            {
                goto Label_05C5;
            }
            this.mRecipeParam = new List<RecipeParam>((int) json.Recipe.Length);
        Label_05C5:
            num10 = 0;
            goto Label_05FC;
        Label_05CD:
            param19 = json.Recipe[num10];
            param20 = new RecipeParam();
            this.mRecipeParam.Add(param20);
            param20.Deserialize(param19);
            num10 += 1;
        Label_05FC:
            if (num10 < ((int) json.Recipe.Length))
            {
                goto Label_05CD;
            }
        Label_060B:
            if (json.Job == null)
            {
                goto Label_068F;
            }
            if (this.mJobParam != null)
            {
                goto Label_0634;
            }
            this.mJobParam = new List<JobParam>((int) json.Job.Length);
        Label_0634:
            num11 = 0;
            goto Label_0680;
        Label_063C:
            param21 = json.Job[num11];
            param22 = new JobParam();
            this.mJobParam.Add(param22);
            this.mJobParamDict[param21.iname] = param22;
            param22.Deserialize(param21, this);
            num11 += 1;
        Label_0680:
            if (num11 < ((int) json.Job.Length))
            {
                goto Label_063C;
            }
        Label_068F:
            if (json.JobSet == null)
            {
                goto Label_0785;
            }
            if (this.mJobSetParam != null)
            {
                goto Label_06B8;
            }
            this.mJobSetParam = new List<JobSetParam>((int) json.JobSet.Length);
        Label_06B8:
            if (this.mJobsetDictionary != null)
            {
                goto Label_06D6;
            }
            this.mJobsetDictionary = new Dictionary<string, List<JobSetParam>>((int) json.Unit.Length);
        Label_06D6:
            num12 = 0;
            goto Label_0776;
        Label_06DE:
            param23 = json.JobSet[num12];
            param24 = new JobSetParam();
            this.mJobSetParam.Add(param24);
            param24.Deserialize(param23);
            if (string.IsNullOrEmpty(param24.target_unit) != null)
            {
                goto Label_0770;
            }
            list = null;
            if (this.mJobsetDictionary.ContainsKey(param24.target_unit) == null)
            {
                goto Label_074B;
            }
            list = this.mJobsetDictionary[param24.target_unit];
            goto Label_0767;
        Label_074B:
            list = new List<JobSetParam>(3);
            this.mJobsetDictionary.Add(param24.target_unit, list);
        Label_0767:
            list.Add(param24);
        Label_0770:
            num12 += 1;
        Label_0776:
            if (num12 < ((int) json.JobSet.Length))
            {
                goto Label_06DE;
            }
        Label_0785:
            if (json.Grow == null)
            {
                goto Label_07F4;
            }
            if (this.mGrowParam != null)
            {
                goto Label_07AE;
            }
            this.mGrowParam = new List<GrowParam>((int) json.Grow.Length);
        Label_07AE:
            num13 = 0;
            goto Label_07E5;
        Label_07B6:
            param25 = json.Grow[num13];
            param26 = new GrowParam();
            this.mGrowParam.Add(param26);
            param26.Deserialize(param25);
            num13 += 1;
        Label_07E5:
            if (num13 < ((int) json.Grow.Length))
            {
                goto Label_07B6;
            }
        Label_07F4:
            if (json.AI == null)
            {
                goto Label_0863;
            }
            if (this.mAIParam != null)
            {
                goto Label_081D;
            }
            this.mAIParam = new List<AIParam>((int) json.AI.Length);
        Label_081D:
            num14 = 0;
            goto Label_0854;
        Label_0825:
            param27 = json.AI[num14];
            param28 = new AIParam();
            this.mAIParam.Add(param28);
            param28.Deserialize(param27);
            num14 += 1;
        Label_0854:
            if (num14 < ((int) json.AI.Length))
            {
                goto Label_0825;
            }
        Label_0863:
            if (json.Geo == null)
            {
                goto Label_08D2;
            }
            if (this.mGeoParam != null)
            {
                goto Label_088C;
            }
            this.mGeoParam = new List<GeoParam>((int) json.Geo.Length);
        Label_088C:
            num15 = 0;
            goto Label_08C3;
        Label_0894:
            param29 = json.Geo[num15];
            param30 = new GeoParam();
            this.mGeoParam.Add(param30);
            param30.Deserialize(param29);
            num15 += 1;
        Label_08C3:
            if (num15 < ((int) json.Geo.Length))
            {
                goto Label_0894;
            }
        Label_08D2:
            if (json.Rarity == null)
            {
                goto Label_093D;
            }
            if (this.mRarityParam != null)
            {
                goto Label_08FB;
            }
            this.mRarityParam = new List<RarityParam>((int) json.Rarity.Length);
        Label_08FB:
            num16 = 0;
            goto Label_092E;
        Label_0903:
            param31 = new RarityParam();
            this.mRarityParam.Add(param31);
            param31.Deserialize(json.Rarity[num16]);
            num16 += 1;
        Label_092E:
            if (num16 < ((int) json.Rarity.Length))
            {
                goto Label_0903;
            }
        Label_093D:
            if (json.Shop == null)
            {
                goto Label_09A8;
            }
            if (this.mShopParam != null)
            {
                goto Label_0966;
            }
            this.mShopParam = new List<ShopParam>((int) json.Shop.Length);
        Label_0966:
            num17 = 0;
            goto Label_0999;
        Label_096E:
            param32 = new ShopParam();
            this.mShopParam.Add(param32);
            param32.Deserialize(json.Shop[num17]);
            num17 += 1;
        Label_0999:
            if (num17 < ((int) json.Shop.Length))
            {
                goto Label_096E;
            }
        Label_09A8:
            if (json.Player == null)
            {
                goto Label_0A0D;
            }
            this.mPlayerParamTbl = new PlayerParam[(int) json.Player.Length];
            num18 = 0;
            goto Label_09FE;
        Label_09CE:
            param33 = json.Player[num18];
            this.mPlayerParamTbl[num18] = new PlayerParam();
            this.mPlayerParamTbl[num18].Deserialize(param33);
            num18 += 1;
        Label_09FE:
            if (num18 < ((int) json.Player.Length))
            {
                goto Label_09CE;
            }
        Label_0A0D:
            if (json.PlayerLvTbl == null)
            {
                goto Label_0A68;
            }
            this.mPlayerExpTbl = new OInt[(int) json.PlayerLvTbl.Length];
            num19 = 0;
            goto Label_0A59;
        Label_0A33:
            *(&(this.mPlayerExpTbl[num19])) = json.PlayerLvTbl[num19];
            num19 += 1;
        Label_0A59:
            if (num19 < ((int) json.PlayerLvTbl.Length))
            {
                goto Label_0A33;
            }
        Label_0A68:
            if (json.UnitLvTbl == null)
            {
                goto Label_0AC3;
            }
            this.mUnitExpTbl = new OInt[(int) json.UnitLvTbl.Length];
            num20 = 0;
            goto Label_0AB4;
        Label_0A8E:
            *(&(this.mUnitExpTbl[num20])) = json.UnitLvTbl[num20];
            num20 += 1;
        Label_0AB4:
            if (num20 < ((int) json.UnitLvTbl.Length))
            {
                goto Label_0A8E;
            }
        Label_0AC3:
            if (json.ArtifactLvTbl == null)
            {
                goto Label_0B1E;
            }
            this.mArtifactExpTbl = new OInt[(int) json.ArtifactLvTbl.Length];
            num21 = 0;
            goto Label_0B0F;
        Label_0AE9:
            *(&(this.mArtifactExpTbl[num21])) = json.ArtifactLvTbl[num21];
            num21 += 1;
        Label_0B0F:
            if (num21 < ((int) json.ArtifactLvTbl.Length))
            {
                goto Label_0AE9;
            }
        Label_0B1E:
            if (json.AbilityRank == null)
            {
                goto Label_0B79;
            }
            this.mAbilityExpTbl = new OInt[(int) json.AbilityRank.Length];
            num22 = 0;
            goto Label_0B6A;
        Label_0B44:
            *(&(this.mAbilityExpTbl[num22])) = json.AbilityRank[num22];
            num22 += 1;
        Label_0B6A:
            if (num22 < ((int) json.AbilityRank.Length))
            {
                goto Label_0B44;
            }
        Label_0B79:
            if (json.AwakePieceTbl == null)
            {
                goto Label_0BD4;
            }
            this.mAwakePieceTbl = new OInt[(int) json.AwakePieceTbl.Length];
            num23 = 0;
            goto Label_0BC5;
        Label_0B9F:
            *(&(this.mAwakePieceTbl[num23])) = json.AwakePieceTbl[num23];
            num23 += 1;
        Label_0BC5:
            if (num23 < ((int) json.AwakePieceTbl.Length))
            {
                goto Label_0B9F;
            }
        Label_0BD4:
            this.mLocalNotificationParam = new SRPG.LocalNotificationParam();
            if (json.LocalNotification == null)
            {
                goto Label_0C32;
            }
            this.mLocalNotificationParam.msg_stamina = json.LocalNotification[0].msg_stamina;
            this.mLocalNotificationParam.iOSAct_stamina = json.LocalNotification[0].iOSAct_stamina;
            this.mLocalNotificationParam.limitSec_stamina = json.LocalNotification[0].limitSec_stamina;
        Label_0C32:
            dictionary2 = new Dictionary<int, TrophyCategoryParam>();
            if (json.TrophyCategory == null)
            {
                goto Label_0CCA;
            }
            list2 = new List<TrophyCategoryParam>((int) json.TrophyCategory.Length);
            num24 = 0;
            goto Label_0CAE;
        Label_0C5B:
            param34 = new TrophyCategoryParam();
            if (param34.Deserialize(json.TrophyCategory[num24]) == null)
            {
                goto Label_0CA8;
            }
            list2.Add(param34);
            if (dictionary2.ContainsKey(param34.hash_code) != null)
            {
                goto Label_0CA8;
            }
            dictionary2.Add(param34.hash_code, param34);
        Label_0CA8:
            num24 += 1;
        Label_0CAE:
            if (num24 < ((int) json.TrophyCategory.Length))
            {
                goto Label_0C5B;
            }
            this.mTrophyCategory = list2.ToArray();
        Label_0CCA:
            if (json.Trophy == null)
            {
                goto Label_0DCA;
            }
            if (dictionary2.Count <= 0)
            {
                goto Label_0DCA;
            }
            list3 = new List<TrophyParam>((int) json.Trophy.Length);
            num25 = 0;
            goto Label_0D67;
        Label_0CF9:
            param35 = new TrophyParam();
            if (param35.Deserialize(json.Trophy[num25]) == null)
            {
                goto Label_0D61;
            }
            if (dictionary2.ContainsKey(param35.category_hash_code) == null)
            {
                goto Label_0D42;
            }
            param35.CategoryParam = dictionary2[param35.category_hash_code];
            goto Label_0D58;
        Label_0D42:
            DebugUtility.LogError(param35.iname + " => 親カテゴリが未設定 or 入力ミス");
        Label_0D58:
            list3.Add(param35);
        Label_0D61:
            num25 += 1;
        Label_0D67:
            if (num25 < ((int) json.Trophy.Length))
            {
                goto Label_0CF9;
            }
            this.mTrophy = list3.ToArray();
            this.mTrophyInameDict = new Dictionary<string, TrophyParam>();
            paramArray2 = this.mTrophy;
            num26 = 0;
            goto Label_0DBF;
        Label_0D9E:
            param36 = paramArray2[num26];
            this.mTrophyInameDict.Add(param36.iname, param36);
            num26 += 1;
        Label_0DBF:
            if (num26 < ((int) paramArray2.Length))
            {
                goto Label_0D9E;
            }
        Label_0DCA:
            dictionary3 = new Dictionary<string, ChallengeCategoryParam>();
            if (json.ChallengeCategory == null)
            {
                goto Label_0E4A;
            }
            list4 = new List<ChallengeCategoryParam>((int) json.ChallengeCategory.Length);
            num27 = 0;
            goto Label_0E2E;
        Label_0DF3:
            param37 = new ChallengeCategoryParam();
            if (param37.Deserialize(json.ChallengeCategory[num27]) == null)
            {
                goto Label_0E28;
            }
            dictionary3[param37.iname] = param37;
            list4.Add(param37);
        Label_0E28:
            num27 += 1;
        Label_0E2E:
            if (num27 < ((int) json.ChallengeCategory.Length))
            {
                goto Label_0DF3;
            }
            this.mChallengeCategory = list4.ToArray();
        Label_0E4A:
            if (json.Challenge == null)
            {
                goto Label_0F5B;
            }
            list5 = new List<TrophyParam>((int) json.Challenge.Length);
            num28 = 0;
            goto Label_0EC7;
        Label_0E6C:
            param38 = new TrophyParam();
            if (param38.Deserialize(json.Challenge[num28]) == null)
            {
                goto Label_0EC1;
            }
            if (dictionary3.ContainsKey(param38.Category) == null)
            {
                goto Label_0EB0;
            }
            param38.ChallengeCategoryParam = dictionary3[param38.Category];
        Label_0EB0:
            param38.Challenge = 1;
            list5.Add(param38);
        Label_0EC1:
            num28 += 1;
        Label_0EC7:
            if (num28 < ((int) json.Challenge.Length))
            {
                goto Label_0E6C;
            }
            this.mChallenge = list5.ToArray();
            num29 = (int) this.mTrophy.Length;
            Array.Resize<TrophyParam>(&this.mTrophy, num29 + ((int) this.mChallenge.Length));
            Array.Copy(this.mChallenge, 0, this.mTrophy, num29, (int) this.mChallenge.Length);
            paramArray3 = this.mChallenge;
            num30 = 0;
            goto Label_0F50;
        Label_0F2F:
            param39 = paramArray3[num30];
            this.mTrophyInameDict.Add(param39.iname, param39);
            num30 += 1;
        Label_0F50:
            if (num30 < ((int) paramArray3.Length))
            {
                goto Label_0F2F;
            }
        Label_0F5B:
            this.CreateTrophyDict();
            if (json.Unlock == null)
            {
                goto Label_0FCA;
            }
            list6 = new List<UnlockParam>((int) json.Unlock.Length);
            num31 = 0;
            goto Label_0FAE;
        Label_0F83:
            param40 = new UnlockParam();
            if (param40.Deserialize(json.Unlock[num31]) == null)
            {
                goto Label_0FA8;
            }
            list6.Add(param40);
        Label_0FA8:
            num31 += 1;
        Label_0FAE:
            if (num31 < ((int) json.Unlock.Length))
            {
                goto Label_0F83;
            }
            this.mUnlock = list6.ToArray();
        Label_0FCA:
            if (json.Vip == null)
            {
                goto Label_1033;
            }
            list7 = new List<VipParam>((int) json.Vip.Length);
            num32 = 0;
            goto Label_1017;
        Label_0FEC:
            param41 = new VipParam();
            if (param41.Deserialize(json.Vip[num32]) == null)
            {
                goto Label_1011;
            }
            list7.Add(param41);
        Label_1011:
            num32 += 1;
        Label_1017:
            if (num32 < ((int) json.Vip.Length))
            {
                goto Label_0FEC;
            }
            this.mVip = list7.ToArray();
        Label_1033:
            if (json.Mov == null)
            {
                goto Label_10B4;
            }
            this.mStreamingMovies = new JSON_StreamingMovie[(int) json.Mov.Length];
            num33 = 0;
            goto Label_10A5;
        Label_1059:
            this.mStreamingMovies[num33] = new JSON_StreamingMovie();
            this.mStreamingMovies[num33].alias = json.Mov[num33].alias;
            this.mStreamingMovies[num33].path = json.Mov[num33].path;
            num33 += 1;
        Label_10A5:
            if (num33 < ((int) json.Mov.Length))
            {
                goto Label_1059;
            }
        Label_10B4:
            if (json.Banner == null)
            {
                goto Label_111D;
            }
            list8 = new List<BannerParam>((int) json.Banner.Length);
            num34 = 0;
            goto Label_1101;
        Label_10D6:
            param42 = new BannerParam();
            if (param42.Deserialize(json.Banner[num34]) == null)
            {
                goto Label_10FB;
            }
            list8.Add(param42);
        Label_10FB:
            num34 += 1;
        Label_1101:
            if (num34 < ((int) json.Banner.Length))
            {
                goto Label_10D6;
            }
            this.mBanner = list8.ToArray();
        Label_111D:
            if (json.QuestClearUnlockUnitData == null)
            {
                goto Label_117C;
            }
            list9 = new List<QuestClearUnlockUnitDataParam>((int) json.QuestClearUnlockUnitData.Length);
            num35 = 0;
            goto Label_1165;
        Label_113F:
            param43 = new QuestClearUnlockUnitDataParam();
            param43.Deserialize(json.QuestClearUnlockUnitData[num35]);
            list9.Add(param43);
            num35 += 1;
        Label_1165:
            if (num35 < ((int) json.QuestClearUnlockUnitData.Length))
            {
                goto Label_113F;
            }
            this.mUnlockUnitDataParam = list9;
        Label_117C:
            if (json.Award == null)
            {
                goto Label_121D;
            }
            if (this.mAwardParam != null)
            {
                goto Label_11A5;
            }
            this.mAwardParam = new List<AwardParam>((int) json.Award.Length);
        Label_11A5:
            if (this.mAwardDictionary != null)
            {
                goto Label_11C3;
            }
            this.mAwardDictionary = new Dictionary<string, AwardParam>((int) json.Award.Length);
        Label_11C3:
            num36 = 0;
            goto Label_120E;
        Label_11CB:
            param44 = json.Award[num36];
            param45 = new AwardParam();
            this.mAwardParam.Add(param45);
            param45.Deserialize(param44);
            this.mAwardDictionary.Add(param45.iname, param45);
            num36 += 1;
        Label_120E:
            if (num36 < ((int) json.Award.Length))
            {
                goto Label_11CB;
            }
        Label_121D:
            if (json.LoginInfo == null)
            {
                goto Label_1286;
            }
            list10 = new List<LoginInfoParam>((int) json.LoginInfo.Length);
            num37 = 0;
            goto Label_126A;
        Label_123F:
            param46 = new LoginInfoParam();
            if (param46.Deserialize(json.LoginInfo[num37]) == null)
            {
                goto Label_1264;
            }
            list10.Add(param46);
        Label_1264:
            num37 += 1;
        Label_126A:
            if (num37 < ((int) json.LoginInfo.Length))
            {
                goto Label_123F;
            }
            this.mLoginInfoParam = list10.ToArray();
        Label_1286:
            if (json.CollaboSkill == null)
            {
                goto Label_12F0;
            }
            list11 = new List<CollaboSkillParam>((int) json.CollaboSkill.Length);
            num38 = 0;
            goto Label_12CE;
        Label_12A8:
            param47 = new CollaboSkillParam();
            param47.Deserialize(json.CollaboSkill[num38]);
            list11.Add(param47);
            num38 += 1;
        Label_12CE:
            if (num38 < ((int) json.CollaboSkill.Length))
            {
                goto Label_12A8;
            }
            this.mCollaboSkillParam = list11;
            CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam);
        Label_12F0:
            if (json.Trick == null)
            {
                goto Label_134F;
            }
            list12 = new List<TrickParam>((int) json.Trick.Length);
            num39 = 0;
            goto Label_1338;
        Label_1312:
            param48 = new TrickParam();
            param48.Deserialize(json.Trick[num39]);
            list12.Add(param48);
            num39 += 1;
        Label_1338:
            if (num39 < ((int) json.Trick.Length))
            {
                goto Label_1312;
            }
            this.mTrickParam = list12;
        Label_134F:
            if (json.BreakObj == null)
            {
                goto Label_13AE;
            }
            list13 = new List<BreakObjParam>((int) json.BreakObj.Length);
            num40 = 0;
            goto Label_1397;
        Label_1371:
            param49 = new BreakObjParam();
            param49.Deserialize(json.BreakObj[num40]);
            list13.Add(param49);
            num40 += 1;
        Label_1397:
            if (num40 < ((int) json.BreakObj.Length))
            {
                goto Label_1371;
            }
            this.mBreakObjParam = list13;
        Label_13AE:
            if (json.VersusMatchKey == null)
            {
                goto Label_140D;
            }
            this.mVersusMatching = new List<VersusMatchingParam>((int) json.VersusMatchKey.Length);
            num41 = 0;
            goto Label_13FE;
        Label_13D4:
            param50 = new VersusMatchingParam();
            param50.Deserialize(json.VersusMatchKey[num41]);
            this.mVersusMatching.Add(param50);
            num41 += 1;
        Label_13FE:
            if (num41 < ((int) json.VersusMatchKey.Length))
            {
                goto Label_13D4;
            }
        Label_140D:
            if (json.VersusMatchCond == null)
            {
                goto Label_146C;
            }
            this.mVersusMatchCond = new List<VersusMatchCondParam>((int) json.VersusMatchCond.Length);
            num42 = 0;
            goto Label_145D;
        Label_1433:
            param51 = new VersusMatchCondParam();
            param51.Deserialize(json.VersusMatchCond[num42]);
            this.mVersusMatchCond.Add(param51);
            num42 += 1;
        Label_145D:
            if (num42 < ((int) json.VersusMatchCond.Length))
            {
                goto Label_1433;
            }
        Label_146C:
            if (json.TowerScore == null)
            {
                goto Label_1514;
            }
            this.mTowerScores = new Dictionary<string, TowerScoreParam[]>((int) json.TowerScore.Length);
            num43 = 0;
            goto Label_1505;
        Label_1492:
            score = json.TowerScore[num43];
            num44 = (int) score.threshold_vals.Length;
            paramArray4 = new TowerScoreParam[num44];
            num45 = 0;
            goto Label_14E2;
        Label_14B9:
            threshold = score.threshold_vals[num45];
            paramArray4[num45] = new TowerScoreParam();
            paramArray4[num45].Deserialize(threshold);
            num45 += 1;
        Label_14E2:
            if (num45 < num44)
            {
                goto Label_14B9;
            }
            this.mTowerScores.Add(score.iname, paramArray4);
            num43 += 1;
        Label_1505:
            if (num43 < ((int) json.TowerScore.Length))
            {
                goto Label_1492;
            }
        Label_1514:
            if (json.TowerRank == null)
            {
                goto Label_156F;
            }
            this.mTowerRankTbl = new OInt[(int) json.TowerRank.Length];
            num46 = 0;
            goto Label_1560;
        Label_153A:
            *(&(this.mTowerRankTbl[num46])) = json.TowerRank[num46];
            num46 += 1;
        Label_1560:
            if (num46 < ((int) json.TowerRank.Length))
            {
                goto Label_153A;
            }
        Label_156F:
            if (json.MultilimitUnitLv == null)
            {
                goto Label_15CA;
            }
            this.mMultiLimitUnitLv = new OInt[(int) json.MultilimitUnitLv.Length];
            num47 = 0;
            goto Label_15BB;
        Label_1595:
            *(&(this.mMultiLimitUnitLv[num47])) = json.MultilimitUnitLv[num47];
            num47 += 1;
        Label_15BB:
            if (num47 < ((int) json.MultilimitUnitLv.Length))
            {
                goto Label_1595;
            }
        Label_15CA:
            if (json.FriendPresentItem == null)
            {
                goto Label_1629;
            }
            this.mFriendPresentItemParam = new Dictionary<string, FriendPresentItemParam>();
            num48 = 0;
            goto Label_161A;
        Label_15E8:
            param52 = new FriendPresentItemParam();
            param52.Deserialize(json.FriendPresentItem[num48], null);
            this.mFriendPresentItemParam.Add(param52.iname, param52);
            num48 += 1;
        Label_161A:
            if (num48 < ((int) json.FriendPresentItem.Length))
            {
                goto Label_15E8;
            }
        Label_1629:
            if (json.Weather == null)
            {
                goto Label_1688;
            }
            list14 = new List<WeatherParam>((int) json.Weather.Length);
            num49 = 0;
            goto Label_1671;
        Label_164B:
            param53 = new WeatherParam();
            param53.Deserialize(json.Weather[num49]);
            list14.Add(param53);
            num49 += 1;
        Label_1671:
            if (num49 < ((int) json.Weather.Length))
            {
                goto Label_164B;
            }
            this.mWeatherParam = list14;
        Label_1688:
            if (json.UnitUnlockTime == null)
            {
                goto Label_16E7;
            }
            this.mUnitUnlockTimeParam = new Dictionary<string, UnitUnlockTimeParam>();
            num50 = 0;
            goto Label_16D8;
        Label_16A6:
            param54 = new UnitUnlockTimeParam();
            param54.Deserialize(json.UnitUnlockTime[num50]);
            this.mUnitUnlockTimeParam.Add(param54.iname, param54);
            num50 += 1;
        Label_16D8:
            if (num50 < ((int) json.UnitUnlockTime.Length))
            {
                goto Label_16A6;
            }
        Label_16E7:
            if (json.Tobira == null)
            {
                goto Label_1733;
            }
            num51 = 0;
            goto Label_1724;
        Label_16FA:
            param55 = new TobiraParam();
            param55.Deserialize(json.Tobira[num51]);
            this.mTobiraParam.Add(param55);
            num51 += 1;
        Label_1724:
            if (num51 < ((int) json.Tobira.Length))
            {
                goto Label_16FA;
            }
        Label_1733:
            if (json.TobiraCategories == null)
            {
                goto Label_1786;
            }
            num52 = 0;
            goto Label_1777;
        Label_1746:
            param56 = new TobiraCategoriesParam();
            param56.Deserialize(json.TobiraCategories[num52]);
            this.mTobiraCategoriesParam.Add(param56.TobiraCategory, param56);
            num52 += 1;
        Label_1777:
            if (num52 < ((int) json.TobiraCategories.Length))
            {
                goto Label_1746;
            }
        Label_1786:
            if (json.TobiraConds == null)
            {
                goto Label_17D2;
            }
            num53 = 0;
            goto Label_17C3;
        Label_1799:
            param57 = new TobiraCondsParam();
            param57.Deserialize(json.TobiraConds[num53]);
            this.mTobiraCondParam.Add(param57);
            num53 += 1;
        Label_17C3:
            if (num53 < ((int) json.TobiraConds.Length))
            {
                goto Label_1799;
            }
        Label_17D2:
            if (json.TobiraCondsUnit == null)
            {
                goto Label_1825;
            }
            num54 = 0;
            goto Label_1816;
        Label_17E5:
            param58 = new TobiraCondsUnitParam();
            param58.Deserialize(json.TobiraCondsUnit[num54]);
            this.mTobiraCondUnitParam.Add(param58.Id, param58);
            num54 += 1;
        Label_1816:
            if (num54 < ((int) json.TobiraCondsUnit.Length))
            {
                goto Label_17E5;
            }
        Label_1825:
            if (json.TobiraRecipe == null)
            {
                goto Label_1871;
            }
            num55 = 0;
            goto Label_1862;
        Label_1838:
            param59 = new TobiraRecipeParam();
            param59.Deserialize(json.TobiraRecipe[num55]);
            this.mTobiraRecipeParam.Add(param59);
            num55 += 1;
        Label_1862:
            if (num55 < ((int) json.TobiraRecipe.Length))
            {
                goto Label_1838;
            }
        Label_1871:
            if (json.ConceptCard == null)
            {
                goto Label_18D1;
            }
            this.mConceptCard = new Dictionary<string, ConceptCardParam>();
            num56 = 0;
            goto Label_18C2;
        Label_188F:
            param60 = new ConceptCardParam();
            param60.Deserialize(json.ConceptCard[num56], null);
            this.mConceptCard.Add(param60.iname, param60);
            num56 += 1;
        Label_18C2:
            if (num56 < ((int) json.ConceptCard.Length))
            {
                goto Label_188F;
            }
        Label_18D1:
            numArrayArray1 = new int[][] { json.ConceptCardLvTbl1, json.ConceptCardLvTbl2, json.ConceptCardLvTbl3, json.ConceptCardLvTbl4, json.ConceptCardLvTbl5, json.ConceptCardLvTbl6 };
            numArray = numArrayArray1;
            if (0 >= ((int) numArray.Length))
            {
                goto Label_198B;
            }
            if (0 >= ((int) numArray[0].Length))
            {
                goto Label_198B;
            }
            this.mConceptCardLvTbl = new OInt[(int) numArray.Length, (int) numArray[0].Length];
            num57 = 0;
            goto Label_1980;
        Label_1942:
            num58 = 0;
            goto Label_196C;
        Label_194A:
            this.mConceptCardLvTbl[num57, num58] = numArray[num57][num58];
            num58 += 1;
        Label_196C:
            if (num58 < ((int) numArray[num57].Length))
            {
                goto Label_194A;
            }
            num57 += 1;
        Label_1980:
            if (num57 < ((int) numArray.Length))
            {
                goto Label_1942;
            }
        Label_198B:
            if (json.ConceptCardConditions == null)
            {
                goto Label_19EA;
            }
            this.mConceptCardConditions = new Dictionary<string, ConceptCardConditionsParam>();
            num59 = 0;
            goto Label_19DB;
        Label_19A9:
            param61 = new ConceptCardConditionsParam();
            param61.Deserialize(json.ConceptCardConditions[num59]);
            this.mConceptCardConditions.Add(param61.iname, param61);
            num59 += 1;
        Label_19DB:
            if (num59 < ((int) json.ConceptCardConditions.Length))
            {
                goto Label_19A9;
            }
        Label_19EA:
            if (json.ConceptCardTrustReward == null)
            {
                goto Label_1A49;
            }
            this.mConceptCardTrustReward = new Dictionary<string, ConceptCardTrustRewardParam>();
            num60 = 0;
            goto Label_1A3A;
        Label_1A08:
            param62 = new ConceptCardTrustRewardParam();
            param62.Deserialize(json.ConceptCardTrustReward[num60]);
            this.mConceptCardTrustReward.Add(param62.iname, param62);
            num60 += 1;
        Label_1A3A:
            if (num60 < ((int) json.ConceptCardTrustReward.Length))
            {
                goto Label_1A08;
            }
        Label_1A49:
            if (json.UnitGroup == null)
            {
                goto Label_1AA8;
            }
            this.mUnitGroup = new Dictionary<string, UnitGroupParam>();
            num61 = 0;
            goto Label_1A99;
        Label_1A67:
            param63 = new UnitGroupParam();
            param63.Deserialize(json.UnitGroup[num61]);
            this.mUnitGroup.Add(param63.iname, param63);
            num61 += 1;
        Label_1A99:
            if (num61 < ((int) json.UnitGroup.Length))
            {
                goto Label_1A67;
            }
        Label_1AA8:
            if (json.JobGroup == null)
            {
                goto Label_1B07;
            }
            this.mJobGroup = new Dictionary<string, JobGroupParam>();
            num62 = 0;
            goto Label_1AF8;
        Label_1AC6:
            param64 = new JobGroupParam();
            param64.Deserialize(json.JobGroup[num62]);
            this.mJobGroup.Add(param64.iname, param64);
            num62 += 1;
        Label_1AF8:
            if (num62 < ((int) json.JobGroup.Length))
            {
                goto Label_1AC6;
            }
        Label_1B07:
            if (json.StatusCoefficient == null)
            {
                goto Label_1B3E;
            }
            if (((int) json.StatusCoefficient.Length) <= 0)
            {
                goto Label_1B3E;
            }
            this.mStatusCoefficient = new StatusCoefficientParam();
            this.mStatusCoefficient.Deserialize(json.StatusCoefficient[0]);
        Label_1B3E:
            if (json.CustomTarget == null)
            {
                goto Label_1B9D;
            }
            this.mCustomTarget = new Dictionary<string, CustomTargetParam>();
            num63 = 0;
            goto Label_1B8E;
        Label_1B5C:
            param65 = new CustomTargetParam();
            param65.Deserialize(json.CustomTarget[num63]);
            this.mCustomTarget.Add(param65.iname, param65);
            num63 += 1;
        Label_1B8E:
            if (num63 < ((int) json.CustomTarget.Length))
            {
                goto Label_1B5C;
            }
        Label_1B9D:
            if (json.SkillAbilityDerive == null)
            {
                goto Label_1C62;
            }
            if (((int) json.SkillAbilityDerive.Length) <= 0)
            {
                goto Label_1C62;
            }
            this.mSkillAbilityDeriveParam = new SkillAbilityDeriveParam[(int) json.SkillAbilityDerive.Length];
            num64 = 0;
            goto Label_1BFF;
        Label_1BD1:
            this.mSkillAbilityDeriveParam[num64] = new SkillAbilityDeriveParam(num64);
            this.mSkillAbilityDeriveParam[num64].Deserialize(json.SkillAbilityDerive[num64], this);
            num64 += 1;
        Label_1BFF:
            if (num64 < ((int) json.SkillAbilityDerive.Length))
            {
                goto Label_1BD1;
            }
            num65 = 0;
            goto Label_1C53;
        Label_1C16:
            data = new SkillAbilityDeriveData();
            list15 = this.FindAditionalSkillAbilityDeriveParam(this.mSkillAbilityDeriveParam[num65]);
            data.Setup(this.mSkillAbilityDeriveParam[num65], list15);
            this.mSkillAbilityDerives.Add(data);
            num65 += 1;
        Label_1C53:
            if (num65 < ((int) this.mSkillAbilityDeriveParam.Length))
            {
                goto Label_1C16;
            }
        Label_1C62:
            if (json.Tips == null)
            {
                goto Label_1CD0;
            }
            if (((int) json.Tips.Length) <= 0)
            {
                goto Label_1CD0;
            }
            this.mTipsParam = new TipsParam[(int) json.Tips.Length];
            num66 = 0;
            goto Label_1CC1;
        Label_1C96:
            this.mTipsParam[num66] = new TipsParam();
            this.mTipsParam[num66].Deserialize(json.Tips[num66]);
            num66 += 1;
        Label_1CC1:
            if (num66 < ((int) json.Tips.Length))
            {
                goto Label_1C96;
            }
        Label_1CD0:
            this.Loaded = 1;
            return 1;
        }

        public void DumpLoadedLog()
        {
        }

        public bool ExistSkillAbilityDeriveDataWithArtifact(string artifactIname)
        {
            List<SkillAbilityDeriveData> list;
            return (this.FindAllSkillAbilityDeriveDataWithArtifact(artifactIname).Count > 0);
        }

        public unsafe List<SkillAbilityDeriveParam> FindAditionalSkillAbilityDeriveParam(SkillAbilityDeriveParam skillAbilityDeriveParam)
        {
            List<SkillAbilityDeriveParam> list;
            SkillAbilityDeriveTriggerParam param;
            SkillAbilityDeriveTriggerParam[] paramArray;
            int num;
            List<SkillAbilityDeriveParam> list2;
            SkillAbilityDeriveParam param2;
            List<SkillAbilityDeriveParam>.Enumerator enumerator;
            list = new List<SkillAbilityDeriveParam>();
            if (skillAbilityDeriveParam != null)
            {
                goto Label_000E;
            }
            return list;
        Label_000E:
            paramArray = skillAbilityDeriveParam.deriveTriggers;
            num = 0;
            goto Label_0083;
        Label_001C:
            param = paramArray[num];
            enumerator = this.FindAditionalSkillAbilityDeriveParam(skillAbilityDeriveParam, param.m_TriggerType, param.m_TriggerIname).GetEnumerator();
        Label_003E:
            try
            {
                goto Label_0061;
            Label_0043:
                param2 = &enumerator.Current;
                if (list.Contains(param2) != null)
                {
                    goto Label_0061;
                }
                list.Add(param2);
            Label_0061:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0043;
                }
                goto Label_007F;
            }
            finally
            {
            Label_0072:
                ((List<SkillAbilityDeriveParam>.Enumerator) enumerator).Dispose();
            }
        Label_007F:
            num += 1;
        Label_0083:
            if (num < ((int) paramArray.Length))
            {
                goto Label_001C;
            }
            return list;
        }

        public List<SkillAbilityDeriveParam> FindAditionalSkillAbilityDeriveParam(SkillAbilityDeriveParam skillAbilityDeriveParam, ESkillAbilityDeriveConds triggerType, string triggerIname)
        {
            SkillAbilityDeriveTriggerParam[] paramArray3;
            SkillAbilityDeriveTriggerParam[] paramArray1;
            List<SkillAbilityDeriveParam> list;
            SkillAbilityDeriveTriggerParam[] paramArray;
            SkillAbilityDeriveTriggerParam param;
            int num;
            int num2;
            bool flag;
            SkillAbilityDeriveTriggerParam param2;
            SkillAbilityDeriveTriggerParam[] paramArray2;
            int num3;
            <FindAditionalSkillAbilityDeriveParam>c__AnonStorey2E2 storeye;
            storeye = new <FindAditionalSkillAbilityDeriveParam>c__AnonStorey2E2();
            storeye.triggerIname = triggerIname;
            list = new List<SkillAbilityDeriveParam>();
            if (this.mSkillAbilityDeriveParam != null)
            {
                goto Label_0022;
            }
            return list;
        Label_0022:
            paramArray = Enumerable.ToArray<SkillAbilityDeriveTriggerParam>(Enumerable.Where<SkillAbilityDeriveTriggerParam>(skillAbilityDeriveParam.deriveTriggers, new Func<SkillAbilityDeriveTriggerParam, bool>(storeye.<>m__258)));
            param = new SkillAbilityDeriveTriggerParam(triggerType, storeye.triggerIname);
            num = skillAbilityDeriveParam.m_OriginIndex + 1;
            num2 = num;
            goto Label_00F7;
        Label_005F:
            flag = 0;
            paramArray2 = paramArray;
            num3 = 0;
            goto Label_00B3;
        Label_006D:
            param2 = paramArray2[num3];
            paramArray1 = new SkillAbilityDeriveTriggerParam[] { param2, param };
            if (this.mSkillAbilityDeriveParam[num2].CheckContainsTriggerInames(paramArray1) == null)
            {
                goto Label_00AD;
            }
            list.Add(this.mSkillAbilityDeriveParam[num2]);
            flag = 1;
            goto Label_00BE;
        Label_00AD:
            num3 += 1;
        Label_00B3:
            if (num3 < ((int) paramArray2.Length))
            {
                goto Label_006D;
            }
        Label_00BE:
            if (flag != null)
            {
                goto Label_00F1;
            }
            paramArray3 = new SkillAbilityDeriveTriggerParam[] { param };
            if (this.mSkillAbilityDeriveParam[num2].CheckContainsTriggerInames(paramArray3) == null)
            {
                goto Label_00F1;
            }
            list.Add(this.mSkillAbilityDeriveParam[num2]);
        Label_00F1:
            num2 += 1;
        Label_00F7:
            if (num2 < ((int) this.mSkillAbilityDeriveParam.Length))
            {
                goto Label_005F;
            }
            return list;
        }

        public unsafe List<SkillAbilityDeriveData> FindAllSkillAbilityDeriveDataWithArtifact(string artifactIname)
        {
            List<SkillAbilityDeriveData> list;
            SkillAbilityDeriveData data;
            List<SkillAbilityDeriveData>.Enumerator enumerator;
            list = new List<SkillAbilityDeriveData>();
            enumerator = this.mSkillAbilityDerives.GetEnumerator();
        Label_0012:
            try
            {
                goto Label_0033;
            Label_0017:
                data = &enumerator.Current;
                if (data.CheckContainsTriggerIname(1, artifactIname) == null)
                {
                    goto Label_0033;
                }
                list.Add(data);
            Label_0033:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0017;
                }
                goto Label_0050;
            }
            finally
            {
            Label_0044:
                ((List<SkillAbilityDeriveData>.Enumerator) enumerator).Dispose();
            }
        Label_0050:
            return list;
        }

        public List<SkillAbilityDeriveParam> FindSkillAbilityDeriveParamWithArtifact(string artifactIname)
        {
            List<SkillAbilityDeriveParam> list;
            int num;
            list = new List<SkillAbilityDeriveParam>();
            if (this.mSkillAbilityDeriveParam != null)
            {
                goto Label_0013;
            }
            return list;
        Label_0013:
            num = 0;
            goto Label_0040;
        Label_001A:
            if (this.mSkillAbilityDeriveParam[num].CheckContainsTriggerIname(1, artifactIname) == null)
            {
                goto Label_003C;
            }
            list.Add(this.mSkillAbilityDeriveParam[num]);
        Label_003C:
            num += 1;
        Label_0040:
            if (num < ((int) this.mSkillAbilityDeriveParam.Length))
            {
                goto Label_001A;
            }
            return list;
        }

        public unsafe TowerScoreParam[] FindTowerScoreParam(string score_iname)
        {
            TowerScoreParam[] paramArray;
            paramArray = null;
            this.mTowerScores.TryGetValue(score_iname, &paramArray);
            return paramArray;
        }

        public UnlockParam FindUnlockParam(UnlockTargets value)
        {
            int num;
            num = ((int) this.mUnlock.Length) - 1;
            goto Label_0030;
        Label_0010:
            if (this.mUnlock[num].UnlockTarget != value)
            {
                goto Label_002C;
            }
            return this.mUnlock[num];
        Label_002C:
            num -= 1;
        Label_0030:
            if (num >= 0)
            {
                goto Label_0010;
            }
            return null;
        }

        public unsafe int GetAbilityNextGold(int rank)
        {
            DebugUtility.Assert((rank <= 0) ? 0 : ((rank > ((int) this.mAbilityExpTbl.Length)) == 0), "指定ランク" + ((int) rank) + "がアビリティのランク範囲に存在しない。");
            return *(&(this.mAbilityExpTbl[rank]));
        }

        public AbilityParam GetAbilityParam(string key)
        {
            AbilityParam param;
        Label_0000:
            try
            {
                param = this.mAbilityDictionary[key];
                goto Label_0024;
            }
            catch (Exception)
            {
            Label_0017:
                throw new KeyNotFoundException<AbilityParam>(key);
            }
        Label_0024:
            return param;
        }

        public LoginInfoParam[] GetActiveLoginInfos()
        {
            List<LoginInfoParam> list;
            int num;
            bool flag;
            int num2;
            if (this.mLoginInfoParam != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            list = new List<LoginInfoParam>();
            num = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
            flag = MonoSingleton<GameManager>.Instance.Player.IsBeginner();
            num2 = 0;
            goto Label_0065;
        Label_003A:
            if (this.mLoginInfoParam[num2].IsDisplayable(TimeManager.ServerTime, num, flag) == null)
            {
                goto Label_0061;
            }
            list.Add(this.mLoginInfoParam[num2]);
        Label_0061:
            num2 += 1;
        Label_0065:
            if (num2 < ((int) this.mLoginInfoParam.Length))
            {
                goto Label_003A;
            }
            return list.ToArray();
        }

        public AIParam GetAIParam(string key)
        {
            AIParam param;
            <GetAIParam>c__AnonStorey2DD storeydd;
            storeydd = new <GetAIParam>c__AnonStorey2DD();
            storeydd.key = key;
            if (string.IsNullOrEmpty(storeydd.key) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            param = this.mAIParam.Find(new Predicate<AIParam>(storeydd.<>m__253));
            if (param != null)
            {
                goto Label_0057;
            }
            DebugUtility.LogError("Failed AIParam iname \"" + storeydd.key + "\" not found.");
        Label_0057:
            return param;
        }

        public AbilityParam[] GetAllAbilities()
        {
            return ((this.mAbilityParam == null) ? new AbilityParam[0] : this.mAbilityParam.ToArray());
        }

        public AwardParam[] GetAllAwards()
        {
            return ((this.mAwardParam == null) ? new AwardParam[0] : this.mAwardParam.ToArray());
        }

        public JobParam[] GetAllJobs()
        {
            return ((this.mJobParam == null) ? new JobParam[0] : this.mJobParam.ToArray());
        }

        public LoginInfoParam[] GetAllLoginInfos()
        {
            return ((this.mLoginInfoParam == null) ? new LoginInfoParam[0] : this.mLoginInfoParam);
        }

        public RecipeParam[] GetAllRecipes()
        {
            return ((this.mRecipeParam == null) ? new RecipeParam[0] : this.mRecipeParam.ToArray());
        }

        public SkillParam[] GetAllSkills()
        {
            return ((this.mSkillParam == null) ? new SkillParam[0] : this.mSkillParam.ToArray());
        }

        public UnitParam[] GetAllUnits()
        {
            return ((this.mUnitParam == null) ? new UnitParam[0] : this.mUnitParam.ToArray());
        }

        public QuestClearUnlockUnitDataParam[] GetAllUnlockUnitDatas()
        {
            return ((this.mUnlockUnitDataParam == null) ? new QuestClearUnlockUnitDataParam[0] : this.mUnlockUnitDataParam.ToArray());
        }

        public OInt[] GetArtifactExpTable()
        {
            return this.mArtifactExpTbl;
        }

        public ArtifactParam GetArtifactParam(string key)
        {
            ArtifactParam param;
        Label_0000:
            try
            {
                param = this.mArtifactDictionary[key];
                goto Label_0039;
            }
            catch (Exception)
            {
            Label_0017:
                DebugUtility.LogError("Unknown ArtifactParam \"" + key + "\"");
                param = null;
                goto Label_0039;
            }
        Label_0039:
            return param;
        }

        public ArtifactParam GetArtifactParam(string key, bool showLogError)
        {
            if (showLogError == null)
            {
                goto Label_000E;
            }
            return this.GetArtifactParam(key);
        Label_000E:
            if (this.mArtifactDictionary.ContainsKey(key) != null)
            {
                goto Label_0021;
            }
            return null;
        Label_0021:
            return this.mArtifactDictionary[key];
        }

        public unsafe int GetAwakeNeedPieces(int awakeLv)
        {
            DebugUtility.Assert((awakeLv < 0) ? 0 : (awakeLv < ((int) this.mAwakePieceTbl.Length)), "覚醒回数" + ((int) awakeLv) + "が覚醒可能な範囲に存在しない。");
            return *(&(this.mAwakePieceTbl[awakeLv]));
        }

        public AwardParam GetAwardParam(string key)
        {
            AwardParam param;
        Label_0000:
            try
            {
                param = this.mAwardDictionary[key];
                goto Label_0039;
            }
            catch (Exception)
            {
            Label_0017:
                DebugUtility.LogError("Unknown AwardParam \"" + key + "\"");
                param = null;
                goto Label_0039;
            }
        Label_0039:
            return param;
        }

        public BreakObjParam GetBreakObjParam(string iname)
        {
            BreakObjParam param;
            <GetBreakObjParam>c__AnonStorey2D4 storeyd;
            storeyd = new <GetBreakObjParam>c__AnonStorey2D4();
            storeyd.iname = iname;
            if (string.IsNullOrEmpty(storeyd.iname) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            if (this.mBreakObjParam != null)
            {
                goto Label_0041;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetBreakObjParam no data!</color>", new object[0]));
            return null;
        Label_0041:
            param = this.mBreakObjParam.Find(new Predicate<BreakObjParam>(storeyd.<>m__24A));
            if (param != null)
            {
                goto Label_0074;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetBreakObjParam data not found! iname={0}</color>", storeyd.iname));
        Label_0074:
            return param;
        }

        public unsafe BuffEffectParam GetBuffEffectParam(string key)
        {
            BuffEffectParam param;
            List<BuffEffectParam>.Enumerator enumerator;
            BuffEffectParam param2;
            if (string.IsNullOrEmpty(key) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            enumerator = this.mBuffEffectParam.GetEnumerator();
        Label_0019:
            try
            {
                goto Label_003E;
            Label_001E:
                param = &enumerator.Current;
                if ((param.iname == key) == null)
                {
                    goto Label_003E;
                }
                param2 = param;
                goto Label_0072;
            Label_003E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001E;
                }
                goto Label_005B;
            }
            finally
            {
            Label_004F:
                ((List<BuffEffectParam>.Enumerator) enumerator).Dispose();
            }
        Label_005B:
            DebugUtility.LogError("Unknown BuffEffectParam \"" + key + "\"");
            return null;
        Label_0072:
            return param2;
        }

        public JobSetParam[] GetClassChangeJobSetParam(string key)
        {
            JobSetParam[] paramArray;
            JobSetParam[] paramArray2;
            if (string.IsNullOrEmpty(key) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            try
            {
                paramArray2 = this.mJobsetDictionary[key].ToArray();
                goto Label_0038;
            }
            catch
            {
            Label_002B:
                paramArray2 = null;
                goto Label_0038;
            }
        Label_0038:
            return paramArray2;
        }

        public CollaboSkillParam GetCollaboSkillData(string unit_iname)
        {
            CollaboSkillParam param;
            <GetCollaboSkillData>c__AnonStorey2D2 storeyd;
            storeyd = new <GetCollaboSkillData>c__AnonStorey2D2();
            storeyd.unit_iname = unit_iname;
            if (string.IsNullOrEmpty(storeyd.unit_iname) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            if (this.mCollaboSkillParam != null)
            {
                goto Label_0041;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetCollaboSkillData no data!</color>", new object[0]));
            return null;
        Label_0041:
            param = this.mCollaboSkillParam.Find(new Predicate<CollaboSkillParam>(storeyd.<>m__248));
            if (param != null)
            {
                goto Label_0074;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetCollaboSkillData data not found! unit_iname={0}</color>", storeyd.unit_iname));
        Label_0074:
            return param;
        }

        public unsafe ItemParam GetCommonEquip(ItemParam item_param, bool is_soul)
        {
            string str;
            ItemParam param;
            int num;
            ItemParam param2;
            if (is_soul != null)
            {
                goto Label_0044;
            }
            if (item_param.IsCommon != null)
            {
                goto Label_0013;
            }
            return null;
        Label_0013:
            str = *(&(this.FixParam.EquipCmn[item_param.cmn_type - 1]));
            return MonoSingleton<GameManager>.Instance.GetItemParam(str);
        Label_0044:
            num = item_param.rare;
            if (this.FixParam.SoulCommonPiece == null)
            {
                goto Label_006E;
            }
            if (((int) this.FixParam.SoulCommonPiece.Length) > num)
            {
                goto Label_0070;
            }
        Label_006E:
            return null;
        Label_0070:
            return MonoSingleton<GameManager>.Instance.GetItemParam(*(&(this.FixParam.SoulCommonPiece[num])));
        }

        public ConceptCardConditionsParam GetConceptCardConditions(string iname)
        {
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mConceptCardConditions.ContainsKey(iname) != null)
            {
                goto Label_0020;
            }
            return null;
        Label_0020:
            return this.mConceptCardConditions[iname];
        }

        public int GetConceptCardLevelExp(int rarity, int lv)
        {
            int num;
            int num2;
            num = 0;
            num2 = 0;
            goto Label_0022;
        Label_0009:
            num += this.mConceptCardLvTbl[rarity, num2];
            num2 += 1;
        Label_0022:
            if (num2 < lv)
            {
                goto Label_0009;
            }
            return num;
        }

        public int GetConceptCardNextExp(int rarity, int lv)
        {
            return this.mConceptCardLvTbl[rarity, lv - 1];
        }

        public ConceptCardParam GetConceptCardParam(string iname)
        {
            if (this.mConceptCard.ContainsKey(iname) != null)
            {
                goto Label_0013;
            }
            return null;
        Label_0013:
            return this.mConceptCard[iname];
        }

        public unsafe CondEffectParam GetCondEffectParam(string key)
        {
            CondEffectParam param;
            List<CondEffectParam>.Enumerator enumerator;
            CondEffectParam param2;
            if (string.IsNullOrEmpty(key) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            enumerator = this.mCondEffectParam.GetEnumerator();
        Label_0019:
            try
            {
                goto Label_003E;
            Label_001E:
                param = &enumerator.Current;
                if ((param.iname == key) == null)
                {
                    goto Label_003E;
                }
                param2 = param;
                goto Label_0072;
            Label_003E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001E;
                }
                goto Label_005B;
            }
            finally
            {
            Label_004F:
                ((List<CondEffectParam>.Enumerator) enumerator).Dispose();
            }
        Label_005B:
            DebugUtility.LogError("Unknown CondEffectParam \"" + key + "\"");
            return null;
        Label_0072:
            return param2;
        }

        public CustomTargetParam GetCustomTarget(string iname)
        {
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mCustomTarget.ContainsKey(iname) != null)
            {
                goto Label_0020;
            }
            return null;
        Label_0020:
            return this.mCustomTarget[iname];
        }

        public unsafe FriendPresentItemParam GetFriendPresentItemParam(string key)
        {
            FriendPresentItemParam param;
            if (this.mFriendPresentItemParam == null)
            {
                goto Label_003F;
            }
            if (string.IsNullOrEmpty(key) == null)
            {
                goto Label_0018;
            }
            return null;
        Label_0018:
            param = null;
            if (this.mFriendPresentItemParam.TryGetValue(key, &param) != null)
            {
                goto Label_003D;
            }
            DebugUtility.LogError("存在しないフレンドプレゼントアイテムパラメータを参照しています > " + key);
        Label_003D:
            return param;
        Label_003F:
            return null;
        }

        public FriendPresentItemParam[] GetFriendPresentItemParams()
        {
            FriendPresentItemParam[] paramArray;
            if (this.mFriendPresentItemParam == null)
            {
                goto Label_0035;
            }
            paramArray = new FriendPresentItemParam[this.mFriendPresentItemParam.Values.Count];
            this.mFriendPresentItemParam.Values.CopyTo(paramArray, 0);
            return paramArray;
        Label_0035:
            return new FriendPresentItemParam[0];
        }

        public GeoParam GetGeoParam(string key)
        {
            GeoParam param;
            <GetGeoParam>c__AnonStorey2DE storeyde;
            storeyde = new <GetGeoParam>c__AnonStorey2DE();
            storeyde.key = key;
            if (string.IsNullOrEmpty(storeyde.key) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            param = this.mGeoParam.Find(new Predicate<GeoParam>(storeyde.<>m__254));
            if (param != null)
            {
                goto Label_0057;
            }
            DebugUtility.LogError("Failed GeoParam iname \"" + storeyde.key + "\" not found.");
        Label_0057:
            return param;
        }

        public GrowParam GetGrowParam(string key)
        {
            GrowParam param;
            <GetGrowParam>c__AnonStorey2DC storeydc;
            storeydc = new <GetGrowParam>c__AnonStorey2DC();
            storeydc.key = key;
            if (string.IsNullOrEmpty(storeydc.key) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            param = this.mGrowParam.Find(new Predicate<GrowParam>(storeydc.<>m__252));
            if (param != null)
            {
                goto Label_0057;
            }
            DebugUtility.LogError("Unknown GrowParam \"" + storeydc.key + "\"");
        Label_0057:
            return param;
        }

        public ItemParam GetItemParam(string key)
        {
            ItemParam param;
        Label_0000:
            try
            {
                param = this.mItemDictionary[key];
                goto Label_0039;
            }
            catch (Exception)
            {
            Label_0017:
                DebugUtility.LogError("Unknown ItemParam \"" + key + "\"");
                param = null;
                goto Label_0039;
            }
        Label_0039:
            return param;
        }

        public ItemParam GetItemParam(string key, bool showLogError)
        {
            if (showLogError == null)
            {
                goto Label_000E;
            }
            return this.GetItemParam(key);
        Label_000E:
            if (this.mItemDictionary.ContainsKey(key) != null)
            {
                goto Label_0021;
            }
            return null;
        Label_0021:
            return this.mItemDictionary[key];
        }

        public JobGroupParam GetJobGroup(string iname)
        {
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mJobGroup.ContainsKey(iname) != null)
            {
                goto Label_0020;
            }
            return null;
        Label_0020:
            return this.mJobGroup[iname];
        }

        public JobParam GetJobParam(string key)
        {
            JobParam param;
        Label_0000:
            try
            {
                param = this.mJobParamDict[key];
                goto Label_0024;
            }
            catch (Exception)
            {
            Label_0017:
                throw new KeyNotFoundException<JobParam>(key);
            }
        Label_0024:
            return param;
        }

        public JobSetParam GetJobSetParam(string key)
        {
            JobSetParam param;
            <GetJobSetParam>c__AnonStorey2DB storeydb;
            storeydb = new <GetJobSetParam>c__AnonStorey2DB();
            storeydb.key = key;
            if (string.IsNullOrEmpty(storeydb.key) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            param = this.mJobSetParam.Find(new Predicate<JobSetParam>(storeydb.<>m__251));
            if (param != null)
            {
                goto Label_0049;
            }
            throw new KeyNotFoundException<JobSetParam>(storeydb.key);
        Label_0049:
            return param;
        }

        public OInt[] GetMultiPlayLimitUnitLv()
        {
            return this.mMultiLimitUnitLv;
        }

        public int GetPlayerLevelCap()
        {
            return (int) this.mPlayerExpTbl.Length;
        }

        public unsafe int GetPlayerLevelExp(int lv)
        {
            int num;
            int num2;
            DebugUtility.Assert((lv <= 0) ? 0 : ((lv > ((int) this.mPlayerExpTbl.Length)) == 0), "指定レベル" + ((int) lv) + "がプレイヤーのレベル範囲に存在しない。");
            num = 0;
            num2 = 0;
            goto Label_0058;
        Label_003B:
            num += *(&(this.mPlayerExpTbl[num2]));
            num2 += 1;
        Label_0058:
            if (num2 < lv)
            {
                goto Label_003B;
            }
            return num;
        }

        public unsafe int GetPlayerNextExp(int lv)
        {
            DebugUtility.Assert((lv <= 0) ? 0 : ((lv > ((int) this.mPlayerExpTbl.Length)) == 0), "指定レベル" + ((int) lv) + "がプレイヤーのレベル範囲に存在しない。");
            return *(&(this.mPlayerExpTbl[lv - 1]));
        }

        public PlayerParam GetPlayerParam(int lv)
        {
            if (lv <= 0)
            {
                goto Label_001E;
            }
            if (lv > this.GetPlayerLevelCap())
            {
                goto Label_001E;
            }
            return this.mPlayerParamTbl[lv - 1];
        Label_001E:
            return null;
        }

        public RarityParam GetRarityParam(int rarity)
        {
            if (this.mRarityParam == null)
            {
                goto Label_0023;
            }
            if (rarity >= 0)
            {
                goto Label_002F;
            }
            if (rarity < this.mRarityParam.Count)
            {
                goto Label_002F;
            }
        Label_0023:
            DebugUtility.LogError("mRarityParam Stack Overflow.");
            return null;
        Label_002F:
            return this.mRarityParam[rarity];
        }

        public RecipeParam GetRecipeParam(string key)
        {
            RecipeParam param;
            <GetRecipeParam>c__AnonStorey2D0 storeyd;
            storeyd = new <GetRecipeParam>c__AnonStorey2D0();
            storeyd.key = key;
            if (string.IsNullOrEmpty(storeyd.key) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            param = this.mRecipeParam.Find(new Predicate<RecipeParam>(storeyd.<>m__246));
            if (param != null)
            {
                goto Label_0057;
            }
            DebugUtility.LogError("Unknown RecipeParam \"" + storeyd.key + "\"");
        Label_0057:
            return param;
        }

        public ShopParam GetShopParam(EShopType type)
        {
            char[] chArray4;
            char[] chArray3;
            char[] chArray2;
            char[] chArray1;
            int num;
            int num2;
            string[] strArray;
            string[] strArray2;
            int num3;
            string[] strArray3;
            string[] strArray4;
            num = type;
            if (type != 9)
            {
                goto Label_00A6;
            }
            num = -1;
            num2 = 0;
            goto Label_005C;
        Label_0013:
            chArray1 = new char[] { 0x2d };
            strArray = GlobalVars.EventShopItem.shops.gname.Split(chArray1);
            if (this.mShopParam[num2].iname.Equals(strArray[0]) == null)
            {
                goto Label_0058;
            }
            num = num2;
            goto Label_006D;
        Label_0058:
            num2 += 1;
        Label_005C:
            if (num2 < this.mShopParam.Count)
            {
                goto Label_0013;
            }
        Label_006D:
            if (num >= 0)
            {
                goto Label_00A6;
            }
            chArray2 = new char[] { 0x2d };
            strArray2 = GlobalVars.EventShopItem.shops.gname.Split(chArray2);
            DebugUtility.LogError("mShopParam Data Error. Not found: " + strArray2);
            return null;
        Label_00A6:
            if (type != 10)
            {
                goto Label_0154;
            }
            num = -1;
            num3 = 0;
            goto Label_0107;
        Label_00B8:
            chArray3 = new char[] { 0x2d };
            strArray3 = GlobalVars.LimitedShopItem.shops.gname.Split(chArray3);
            if (this.mShopParam[num3].iname.Equals(strArray3[0]) == null)
            {
                goto Label_0101;
            }
            num = num3;
            goto Label_0119;
        Label_0101:
            num3 += 1;
        Label_0107:
            if (num3 < this.mShopParam.Count)
            {
                goto Label_00B8;
            }
        Label_0119:
            if (num >= 0)
            {
                goto Label_0154;
            }
            chArray4 = new char[] { 0x2d };
            strArray4 = GlobalVars.LimitedShopItem.shops.gname.Split(chArray4);
            DebugUtility.LogError("mShopParam Data Error. Not found: " + strArray4);
            return null;
        Label_0154:
            if (this.mShopParam == null)
            {
                goto Label_0177;
            }
            if (num < 0)
            {
                goto Label_0177;
            }
            if (num < this.mShopParam.Count)
            {
                goto Label_0183;
            }
        Label_0177:
            DebugUtility.LogError("mShopParam Stack Overflow.");
            return null;
        Label_0183:
            return this.mShopParam[num];
        }

        public int GetShopType(string iname)
        {
            int num;
            <GetShopType>c__AnonStorey2DF storeydf;
            storeydf = new <GetShopType>c__AnonStorey2DF();
            storeydf.iname = iname;
            num = this.mShopParam.FindIndex(new Predicate<ShopParam>(storeydf.<>m__255));
            if (num >= 0)
            {
                goto Label_0046;
            }
            DebugUtility.LogError("Failed GetShopParam iname \"" + storeydf.iname + "\" not found.");
        Label_0046:
            return num;
        }

        public SkillParam GetSkillParam(string key)
        {
            SkillParam param;
        Label_0000:
            try
            {
                param = this.mSkillDictionary[key];
                goto Label_0024;
            }
            catch (Exception)
            {
            Label_0017:
                throw new KeyNotFoundException<SkillParam>(key);
            }
        Label_0024:
            return param;
        }

        public ArtifactParam GetSkinParamFromItemId(string itemId)
        {
            <GetSkinParamFromItemId>c__AnonStorey2E1 storeye;
            storeye = new <GetSkinParamFromItemId>c__AnonStorey2E1();
            storeye.itemId = itemId;
            return Array.Find<ArtifactParam>(this.mArtifactParam.ToArray(), new Predicate<ArtifactParam>(storeye.<>m__257));
        }

        public Dictionary<TobiraParam.Category, TobiraConditionParam[]> GetTobiraConditionsForUnit(string unit_iname)
        {
            <GetTobiraConditionsForUnit>c__AnonStorey2D9 storeyd;
            storeyd = new <GetTobiraConditionsForUnit>c__AnonStorey2D9();
            storeyd.unit_iname = unit_iname;
            storeyd.<>f__this = this;
            if (string.IsNullOrEmpty(storeyd.unit_iname) == null)
            {
                goto Label_0026;
            }
            return null;
        Label_0026:
            if (this.mTobiraCondParam != null)
            {
                goto Label_0048;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraConditionsForUnit no data!</color>", new object[0]));
            return null;
        Label_0048:
            storeyd.condition_list = new Dictionary<TobiraParam.Category, TobiraConditionParam[]>();
            this.mTobiraCondParam.ForEach(new Action<TobiraCondsParam>(storeyd.<>m__24F));
            return storeyd.condition_list;
        }

        public unsafe TobiraConditionParam[] GetTobiraConditionsForUnit(string unit_iname, TobiraParam.Category category)
        {
            Dictionary<TobiraParam.Category, TobiraConditionParam[]> dictionary;
            TobiraConditionParam[] paramArray;
            dictionary = this.GetTobiraConditionsForUnit(unit_iname);
            if (dictionary != null)
            {
                goto Label_0010;
            }
            return null;
        Label_0010:
            dictionary.TryGetValue(category, &paramArray);
            if (paramArray != null)
            {
                goto Label_0022;
            }
            return null;
        Label_0022:
            return paramArray;
        }

        public TobiraParam[] GetTobiraListForUnit(string unit_iname)
        {
            <GetTobiraListForUnit>c__AnonStorey2D7 storeyd;
            storeyd = new <GetTobiraListForUnit>c__AnonStorey2D7();
            storeyd.unit_iname = unit_iname;
            if (string.IsNullOrEmpty(storeyd.unit_iname) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            if (this.mTobiraParam != null)
            {
                goto Label_0041;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraListForUnit no data!</color>", new object[0]));
            return null;
        Label_0041:
            storeyd.tobira_list = new TobiraParam[TobiraParam.MAX_TOBIRA_COUNT];
            this.mTobiraParam.ForEach(new Action<TobiraParam>(storeyd.<>m__24D));
            return storeyd.tobira_list;
        }

        public TobiraParam GetTobiraParam(string unit_iname, TobiraParam.Category category)
        {
            <GetTobiraParam>c__AnonStorey2D6 storeyd;
            storeyd = new <GetTobiraParam>c__AnonStorey2D6();
            storeyd.unit_iname = unit_iname;
            storeyd.category = category;
            if (string.IsNullOrEmpty(storeyd.unit_iname) == null)
            {
                goto Label_0026;
            }
            return null;
        Label_0026:
            if (this.mTobiraParam != null)
            {
                goto Label_0048;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraListForUnit no data!</color>", new object[0]));
            return null;
        Label_0048:
            return this.mTobiraParam.Find(new Predicate<TobiraParam>(storeyd.<>m__24C));
        }

        public TobiraRecipeParam GetTobiraRecipe(string recipe_iname, int level)
        {
            <GetTobiraRecipe>c__AnonStorey2DA storeyda;
            storeyda = new <GetTobiraRecipe>c__AnonStorey2DA();
            storeyda.recipe_iname = recipe_iname;
            storeyda.level = level;
            return this.mTobiraRecipeParam.Find(new Predicate<TobiraRecipeParam>(storeyda.<>m__250));
        }

        public TobiraRecipeParam GetTobiraRecipe(string unit_iname, TobiraParam.Category category, int level)
        {
            TobiraParam param;
            param = this.GetTobiraParam(unit_iname, category);
            if (param != null)
            {
                goto Label_0011;
            }
            return null;
        Label_0011:
            return this.GetTobiraRecipe(param.RecipeId, level);
        }

        public TrickParam GetTrickParam(string iname)
        {
            TrickParam param;
            <GetTrickParam>c__AnonStorey2D3 storeyd;
            storeyd = new <GetTrickParam>c__AnonStorey2D3();
            storeyd.iname = iname;
            if (string.IsNullOrEmpty(storeyd.iname) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            if (this.mTrickParam != null)
            {
                goto Label_0041;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTrickParam no data!</color>", new object[0]));
            return null;
        Label_0041:
            param = this.mTrickParam.Find(new Predicate<TrickParam>(storeyd.<>m__249));
            if (param != null)
            {
                goto Label_0074;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTrickParam data not found! iname={0}</color>", storeyd.iname));
        Label_0074:
            return param;
        }

        public TrophyObjective[] GetTrophiesOfType(TrophyConditionTypes type)
        {
            return this.mTrophyDict[type];
        }

        public unsafe TrophyParam GetTrophy(string iname)
        {
            TrophyParam param;
            if (this.mTrophy != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mTrophyInameDict.TryGetValue(iname, &param) == null)
            {
                goto Label_0022;
            }
            return param;
        Label_0022:
            return null;
        }

        public ConceptCardTrustRewardParam GetTrustReward(string iname)
        {
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mConceptCardTrustReward.ContainsKey(iname) != null)
            {
                goto Label_0020;
            }
            return null;
        Label_0020:
            return this.mConceptCardTrustReward[iname];
        }

        public UnitGroupParam GetUnitGroup(string iname)
        {
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mUnitGroup.ContainsKey(iname) != null)
            {
                goto Label_0020;
            }
            return null;
        Label_0020:
            return this.mUnitGroup[iname];
        }

        public unsafe Dictionary<string, UnitJobOverwriteParam> GetUnitJobOverwriteParamsForUnit(string unit_iname)
        {
            Dictionary<string, UnitJobOverwriteParam> dictionary;
            if (string.IsNullOrEmpty(unit_iname) == null)
            {
                goto Label_0022;
            }
            DebugUtility.LogError("Unknown UnitJobOverwriteParam \"" + unit_iname + "\"");
            return null;
        Label_0022:
            this.mUnitJobOverwriteDictionary.TryGetValue(unit_iname, &dictionary);
            return dictionary;
        }

        public unsafe int GetUnitLevelExp(int lv)
        {
            int num;
            int num2;
            DebugUtility.Assert((lv <= 0) ? 0 : ((lv > ((int) this.mUnitExpTbl.Length)) == 0), "指定レベル" + ((int) lv) + "がユニットのレベル範囲に存在しない。");
            num = 0;
            num2 = 0;
            goto Label_0058;
        Label_003B:
            num += *(&(this.mUnitExpTbl[num2]));
            num2 += 1;
        Label_0058:
            if (num2 < lv)
            {
                goto Label_003B;
            }
            return num;
        }

        public int GetUnitMaxLevel()
        {
            return (int) this.mUnitExpTbl.Length;
        }

        public unsafe int GetUnitNextExp(int lv)
        {
            DebugUtility.Assert((lv <= 0) ? 0 : ((lv > ((int) this.mUnitExpTbl.Length)) == 0), "指定レベル" + ((int) lv) + "がユニットのレベル範囲に存在しない。");
            return *(&(this.mUnitExpTbl[lv - 1]));
        }

        public UnitParam GetUnitParam(string key)
        {
            UnitParam param;
        Label_0000:
            try
            {
                param = this.mUnitDictionary[key];
                goto Label_0024;
            }
            catch (Exception)
            {
            Label_0017:
                throw new KeyNotFoundException<UnitParam>(key);
            }
        Label_0024:
            return param;
        }

        public UnitParam GetUnitParamForPiece(string key, bool doCheck)
        {
            UnitParam param;
            <GetUnitParamForPiece>c__AnonStorey2E0 storeye;
            storeye = new <GetUnitParamForPiece>c__AnonStorey2E0();
            storeye.key = key;
            if (string.IsNullOrEmpty(storeye.key) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            if ((storeye.key == this.FixParam.CommonPieceAll) != null)
            {
                goto Label_00FF;
            }
            if ((storeye.key == this.FixParam.CommonPieceDark) != null)
            {
                goto Label_00FF;
            }
            if ((storeye.key == this.FixParam.CommonPieceFire) != null)
            {
                goto Label_00FF;
            }
            if ((storeye.key == this.FixParam.CommonPieceShine) != null)
            {
                goto Label_00FF;
            }
            if ((storeye.key == this.FixParam.CommonPieceThunder) != null)
            {
                goto Label_00FF;
            }
            if ((storeye.key == this.FixParam.CommonPieceWater) != null)
            {
                goto Label_00FF;
            }
            if ((storeye.key == this.FixParam.CommonPieceWind) == null)
            {
                goto Label_0101;
            }
        Label_00FF:
            return null;
        Label_0101:
            param = this.mUnitParam.Find(new Predicate<UnitParam>(storeye.<>m__256));
            if (doCheck == null)
            {
                goto Label_013F;
            }
            if (param != null)
            {
                goto Label_013F;
            }
            DebugUtility.LogError("Failed UnitParam iname \"" + storeye.key + "\" not found.");
        Label_013F:
            return param;
        }

        public unsafe UnitUnlockTimeParam GetUnitUnlockTimeParam(string _key)
        {
            UnitUnlockTimeParam param;
            if (string.IsNullOrEmpty(_key) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mUnitUnlockTimeParam != null)
            {
                goto Label_001A;
            }
            return null;
        Label_001A:
            param = null;
            if (this.mUnitUnlockTimeParam.TryGetValue(_key, &param) != null)
            {
                goto Label_0031;
            }
            return null;
        Label_0031:
            return param;
        }

        public UnitUnlockTimeParam[] GetUnitUnlockTimeParams()
        {
            UnitUnlockTimeParam[] paramArray;
            if (this.mUnitUnlockTimeParam != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            paramArray = new UnitUnlockTimeParam[this.mUnitUnlockTimeParam.Values.Count];
            this.mUnitUnlockTimeParam.Values.CopyTo(paramArray, 0);
            return paramArray;
        }

        public UnlockParam GetUnlockParam(string iname)
        {
            int num;
            num = ((int) this.mUnlock.Length) - 1;
            goto Label_0035;
        Label_0010:
            if ((this.mUnlock[num].iname == iname) == null)
            {
                goto Label_0031;
            }
            return this.mUnlock[num];
        Label_0031:
            num -= 1;
        Label_0035:
            if (num >= 0)
            {
                goto Label_0010;
            }
            return null;
        }

        public QuestClearUnlockUnitDataParam GetUnlockUnitData(string key)
        {
            QuestClearUnlockUnitDataParam param;
            <GetUnlockUnitData>c__AnonStorey2D1 storeyd;
            storeyd = new <GetUnlockUnitData>c__AnonStorey2D1();
            storeyd.key = key;
            if (string.IsNullOrEmpty(storeyd.key) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            param = this.mUnlockUnitDataParam.Find(new Predicate<QuestClearUnlockUnitDataParam>(storeyd.<>m__247));
            if (param != null)
            {
                goto Label_0049;
            }
            throw new KeyNotFoundException<QuestClearUnlockUnitDataParam>(storeyd.key);
        Label_0049:
            return param;
        }

        public VersusMatchCondParam[] GetVersusMatchingCondition()
        {
            return this.mVersusMatchCond.ToArray();
        }

        public VersusMatchingParam[] GetVersusMatchingParam()
        {
            return this.mVersusMatching.ToArray();
        }

        public int GetVipArenaResetCount(int rank)
        {
            DebugUtility.Assert((rank < 0) ? 0 : (rank < ((int) this.mVip.Length)), "指定VIPランク" + ((int) rank) + "がVIPランクの範囲に存在しない。");
            return this.mVip[rank].ResetArenaNum;
        }

        public int GetVipBuyGoldLimit(int rank)
        {
            DebugUtility.Assert((rank < 0) ? 0 : (rank < ((int) this.mVip.Length)), "指定VIPランク" + ((int) rank) + "がVIPランクの範囲に存在しない。");
            return this.mVip[rank].BuyCoinNum;
        }

        public int GetVipBuyStaminaLimit(int rank)
        {
            DebugUtility.Assert((rank < 0) ? 0 : (rank < ((int) this.mVip.Length)), "指定VIPランク" + ((int) rank) + "がVIPランクの範囲に存在しない。");
            return this.mVip[rank].BuyStaminaNum;
        }

        public int GetVipRankCap()
        {
            if (this.mVip != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return Math.Max(((int) this.mVip.Length) - 1, 0);
        }

        public int GetVipRankNextPoint(int rank)
        {
            DebugUtility.Assert((rank < 0) ? 0 : (rank < ((int) this.mVip.Length)), "指定VIPランク" + ((int) rank) + "がVIPランクの範囲に存在しない。");
            return this.mVip[rank].NextRankNeedPoint;
        }

        public int GetVipRankTotalNeedPoint(int rank)
        {
            int num;
            int num2;
            DebugUtility.Assert((rank < 0) ? 0 : (rank < ((int) this.mVip.Length)), "指定VIPランク" + ((int) rank) + "がVIPランクの範囲に存在しない。");
            num = 0;
            num2 = 0;
            goto Label_004C;
        Label_0038:
            num += this.mVip[num2].NextRankNeedPoint;
            num2 += 1;
        Label_004C:
            if (num2 < rank)
            {
                goto Label_0038;
            }
            return num;
        }

        public WeaponParam GetWeaponParam(string key)
        {
            WeaponParam param;
            <GetWeaponParam>c__AnonStorey2CF storeycf;
            storeycf = new <GetWeaponParam>c__AnonStorey2CF();
            storeycf.key = key;
            if (string.IsNullOrEmpty(storeycf.key) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            param = this.mWeaponParam.Find(new Predicate<WeaponParam>(storeycf.<>m__245));
            if (param != null)
            {
                goto Label_0057;
            }
            DebugUtility.LogError("Unknown WeaponParam \"" + storeycf.key + "\"");
        Label_0057:
            return param;
        }

        public WeatherParam GetWeatherParam(string iname)
        {
            WeatherParam param;
            <GetWeatherParam>c__AnonStorey2D5 storeyd;
            storeyd = new <GetWeatherParam>c__AnonStorey2D5();
            storeyd.iname = iname;
            if (string.IsNullOrEmpty(storeyd.iname) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            if (this.mWeatherParam != null)
            {
                goto Label_0041;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetWeatherParam no data!</color>", new object[0]));
            return null;
        Label_0041:
            param = this.mWeatherParam.Find(new Predicate<WeatherParam>(storeyd.<>m__24B));
            if (param != null)
            {
                goto Label_0074;
            }
            DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetWeatherParam data not found! iname={0}</color>", storeyd.iname));
        Label_0074:
            return param;
        }

        public bool IsFriendPresentItemParamValid()
        {
            if (this.mFriendPresentItemParam == null)
            {
                goto Label_001A;
            }
            return (this.mFriendPresentItemParam.Count > 1);
        Label_001A:
            return 0;
        }

        public bool IsSkinItem(string itemId)
        {
            return ((this.GetSkinParamFromItemId(itemId) == null) ? 0 : 1);
        }

        public unsafe bool IsUnlockableUnit(string _key, DateTime _time)
        {
            UnitUnlockTimeParam param;
            if (string.IsNullOrEmpty(_key) == null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            param = null;
            if (this.mUnitUnlockTimeParam.TryGetValue(_key, &param) != null)
            {
                goto Label_0024;
            }
            return 1;
        Label_0024:
            if (&param.begin_at.CompareTo(_time) <= 0)
            {
                goto Label_0038;
            }
            return 0;
        Label_0038:
            if (&param.end_at.CompareTo(_time) >= 0)
            {
                goto Label_004C;
            }
            return 0;
        Label_004C:
            return 1;
        }

        public unsafe void MakeMapEffectHaveJobLists()
        {
            JobParam param;
            List<JobParam>.Enumerator enumerator;
            AbilityParam param2;
            LearningSkill skill;
            LearningSkill[] skillArray;
            int num;
            if (this.mJobParam != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (MapEffectParam.IsMakeHaveJobLists() == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            MapEffectParam.MakeHaveJobLists();
            enumerator = this.mJobParam.GetEnumerator();
        Label_0028:
            try
            {
                goto Label_00A5;
            Label_002D:
                param = &enumerator.Current;
                if (string.IsNullOrEmpty(param.MapEffectAbility) == null)
                {
                    goto Label_004A;
                }
                goto Label_00A5;
            Label_004A:
                if (param.IsMapEffectRevReso != null)
                {
                    goto Label_005A;
                }
                goto Label_00A5;
            Label_005A:
                param2 = this.GetAbilityParam(param.MapEffectAbility);
                if (param2 != null)
                {
                    goto Label_0072;
                }
                goto Label_00A5;
            Label_0072:
                skillArray = param2.skills;
                num = 0;
                goto Label_009A;
            Label_0082:
                skill = skillArray[num];
                MapEffectParam.AddHaveJob(skill.iname, param);
                num += 1;
            Label_009A:
                if (num < ((int) skillArray.Length))
                {
                    goto Label_0082;
                }
            Label_00A5:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002D;
                }
                goto Label_00C2;
            }
            finally
            {
            Label_00B6:
                ((List<JobParam>.Enumerator) enumerator).Dispose();
            }
        Label_00C2:
            return;
        }

        public string TranslateMoviePath(string path)
        {
            int num;
            if (this.mStreamingMovies != null)
            {
                goto Label_000D;
            }
            return path;
        Label_000D:
            num = 0;
            goto Label_003E;
        Label_0014:
            if ((this.mStreamingMovies[num].alias == path) == null)
            {
                goto Label_003A;
            }
            return this.mStreamingMovies[num].path;
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) this.mStreamingMovies.Length))
            {
                goto Label_0014;
            }
            return path;
        }

        public List<SkillAbilityDeriveData> SkillAbilityDerives
        {
            get
            {
                return this.mSkillAbilityDerives;
            }
        }

        public TipsParam[] Tips
        {
            get
            {
                return this.mTipsParam;
            }
        }

        public bool Loaded
        {
            [CompilerGenerated]
            get
            {
                return this.<Loaded>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<Loaded>k__BackingField = value;
                return;
            }
        }

        public SRPG.FixParam FixParam
        {
            get
            {
                return this.mFixParam;
            }
        }

        public SRPG.LocalNotificationParam LocalNotificationParam
        {
            get
            {
                return this.mLocalNotificationParam;
            }
        }

        public List<ItemParam> Items
        {
            get
            {
                return this.mItemParam;
            }
        }

        public JobSetParam[] JobSets
        {
            get
            {
                return this.mJobSetParam.ToArray();
            }
        }

        public List<ArtifactParam> Artifacts
        {
            get
            {
                return this.mArtifactParam;
            }
        }

        public List<CollaboSkillParam> CollaboSkills
        {
            get
            {
                return this.mCollaboSkillParam;
            }
        }

        public TrophyCategoryParam[] TrophyCategories
        {
            get
            {
                return this.mTrophyCategory;
            }
        }

        public ChallengeCategoryParam[] ChallengeCategories
        {
            get
            {
                return this.mChallengeCategory;
            }
        }

        public TrophyParam[] Trophies
        {
            get
            {
                return this.mTrophy;
            }
        }

        public UnlockParam[] Unlocks
        {
            get
            {
                return this.mUnlock;
            }
        }

        public BannerParam[] Banners
        {
            get
            {
                return this.mBanner;
            }
        }

        public OInt[] TowerRank
        {
            get
            {
                return this.mTowerRankTbl;
            }
        }

        [CompilerGenerated]
        private sealed class <CanUnlockTobira>c__AnonStorey2D8
        {
            internal string unit_iname;

            public <CanUnlockTobira>c__AnonStorey2D8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__24E(TobiraCondsParam param)
            {
                if ((param.UnitIname != this.unit_iname) == null)
                {
                    goto Label_0018;
                }
                return 0;
            Label_0018:
                if (param.TobiraCategory == null)
                {
                    goto Label_0025;
                }
                return 0;
            Label_0025:
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C1
        {
            internal JSON_UnitParam data;

            public <Deserialize>c__AnonStorey2C1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__237(UnitParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C2
        {
            internal JSON_SkillParam data;

            public <Deserialize>c__AnonStorey2C2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__238(SkillParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C3
        {
            internal JSON_BuffEffectParam data;

            public <Deserialize>c__AnonStorey2C3()
            {
                base..ctor();
                return;
            }

            internal bool <>m__239(BuffEffectParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C4
        {
            internal JSON_CondEffectParam data;

            public <Deserialize>c__AnonStorey2C4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__23A(CondEffectParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C5
        {
            internal JSON_AbilityParam data;

            public <Deserialize>c__AnonStorey2C5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__23B(AbilityParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C6
        {
            internal JSON_ItemParam data;

            public <Deserialize>c__AnonStorey2C6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__23C(ItemParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C7
        {
            internal JSON_ArtifactParam data;

            public <Deserialize>c__AnonStorey2C7()
            {
                base..ctor();
                return;
            }

            internal bool <>m__23D(ArtifactParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C8
        {
            internal JSON_WeaponParam data;

            public <Deserialize>c__AnonStorey2C8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__23E(WeaponParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2C9
        {
            internal JSON_JobParam data;

            public <Deserialize>c__AnonStorey2C9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__23F(JobParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2CA
        {
            internal JSON_JobSetParam data;

            public <Deserialize>c__AnonStorey2CA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__240(JobSetParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2CB
        {
            internal JSON_GrowParam data;

            public <Deserialize>c__AnonStorey2CB()
            {
                base..ctor();
                return;
            }

            internal bool <>m__241(GrowParam p)
            {
                return (p.type == this.data.type);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2CC
        {
            internal JSON_AIParam data;

            public <Deserialize>c__AnonStorey2CC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__242(AIParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2CD
        {
            internal JSON_GeoParam data;

            public <Deserialize>c__AnonStorey2CD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__243(GeoParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2CE
        {
            internal JSON_AwardParam data;

            public <Deserialize>c__AnonStorey2CE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__244(AwardParam p)
            {
                return (p.iname == this.data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindAditionalSkillAbilityDeriveParam>c__AnonStorey2E2
        {
            internal string triggerIname;

            public <FindAditionalSkillAbilityDeriveParam>c__AnonStorey2E2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__258(SkillAbilityDeriveTriggerParam triggerParam)
            {
                return (triggerParam.m_TriggerIname != this.triggerIname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetAIParam>c__AnonStorey2DD
        {
            internal string key;

            public <GetAIParam>c__AnonStorey2DD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__253(AIParam p)
            {
                return (p.iname == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <GetBreakObjParam>c__AnonStorey2D4
        {
            internal string iname;

            public <GetBreakObjParam>c__AnonStorey2D4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__24A(BreakObjParam d)
            {
                return (d.Iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetCollaboSkillData>c__AnonStorey2D2
        {
            internal string unit_iname;

            public <GetCollaboSkillData>c__AnonStorey2D2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__248(CollaboSkillParam d)
            {
                return (d.UnitIname == this.unit_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetGeoParam>c__AnonStorey2DE
        {
            internal string key;

            public <GetGeoParam>c__AnonStorey2DE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__254(GeoParam p)
            {
                return (p.iname == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <GetGrowParam>c__AnonStorey2DC
        {
            internal string key;

            public <GetGrowParam>c__AnonStorey2DC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__252(GrowParam p)
            {
                return (p.type == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <GetJobSetParam>c__AnonStorey2DB
        {
            internal string key;

            public <GetJobSetParam>c__AnonStorey2DB()
            {
                base..ctor();
                return;
            }

            internal bool <>m__251(JobSetParam p)
            {
                return (p.iname == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <GetRecipeParam>c__AnonStorey2D0
        {
            internal string key;

            public <GetRecipeParam>c__AnonStorey2D0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__246(RecipeParam p)
            {
                return (p.iname == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <GetShopType>c__AnonStorey2DF
        {
            internal string iname;

            public <GetShopType>c__AnonStorey2DF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__255(ShopParam p)
            {
                return (p.iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetSkinParamFromItemId>c__AnonStorey2E1
        {
            internal string itemId;

            public <GetSkinParamFromItemId>c__AnonStorey2E1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__257(ArtifactParam s)
            {
                return (s.kakera == this.itemId);
            }
        }

        [CompilerGenerated]
        private sealed class <GetTobiraConditionsForUnit>c__AnonStorey2D9
        {
            internal string unit_iname;
            internal Dictionary<TobiraParam.Category, TobiraConditionParam[]> condition_list;
            internal MasterParam <>f__this;

            public <GetTobiraConditionsForUnit>c__AnonStorey2D9()
            {
                base..ctor();
                return;
            }

            internal void <>m__24F(TobiraCondsParam param)
            {
                if ((param.UnitIname == this.unit_iname) == null)
                {
                    goto Label_0044;
                }
                this.condition_list.Add(param.TobiraCategory, param.Conditions);
                Array.ForEach<TobiraConditionParam>(param.Conditions, new Action<TobiraConditionParam>(this.<>m__25D));
            Label_0044:
                return;
            }

            internal unsafe void <>m__25D(TobiraConditionParam tcp)
            {
                TobiraCondsUnitParam param;
                if (tcp.CondType != 1)
                {
                    goto Label_0032;
                }
                this.<>f__this.mTobiraCondUnitParam.TryGetValue(tcp.CondIname, &param);
                if (param == null)
                {
                    goto Label_0032;
                }
                tcp.SetCondUnit(param);
            Label_0032:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <GetTobiraListForUnit>c__AnonStorey2D7
        {
            internal string unit_iname;
            internal TobiraParam[] tobira_list;

            public <GetTobiraListForUnit>c__AnonStorey2D7()
            {
                base..ctor();
                return;
            }

            internal void <>m__24D(TobiraParam param)
            {
                if ((param.UnitIname == this.unit_iname) == null)
                {
                    goto Label_0024;
                }
                this.tobira_list[param.TobiraCategory] = param;
            Label_0024:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <GetTobiraParam>c__AnonStorey2D6
        {
            internal string unit_iname;
            internal TobiraParam.Category category;

            public <GetTobiraParam>c__AnonStorey2D6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__24C(TobiraParam param)
            {
                return (((param.UnitIname == this.unit_iname) == null) ? 0 : (param.TobiraCategory == this.category));
            }
        }

        [CompilerGenerated]
        private sealed class <GetTobiraRecipe>c__AnonStorey2DA
        {
            internal string recipe_iname;
            internal int level;

            public <GetTobiraRecipe>c__AnonStorey2DA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__250(TobiraRecipeParam param)
            {
                return (((param.RecipeIname == this.recipe_iname) == null) ? 0 : (param.Level == this.level));
            }
        }

        [CompilerGenerated]
        private sealed class <GetTrickParam>c__AnonStorey2D3
        {
            internal string iname;

            public <GetTrickParam>c__AnonStorey2D3()
            {
                base..ctor();
                return;
            }

            internal bool <>m__249(TrickParam d)
            {
                return (d.Iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetUnitParamForPiece>c__AnonStorey2E0
        {
            internal string key;

            public <GetUnitParamForPiece>c__AnonStorey2E0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__256(UnitParam p)
            {
                return (p.piece == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <GetUnlockUnitData>c__AnonStorey2D1
        {
            internal string key;

            public <GetUnlockUnitData>c__AnonStorey2D1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__247(QuestClearUnlockUnitDataParam p)
            {
                return (p.iname == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <GetWeaponParam>c__AnonStorey2CF
        {
            internal string key;

            public <GetWeaponParam>c__AnonStorey2CF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__245(WeaponParam p)
            {
                return (p.iname == this.key);
            }
        }

        [CompilerGenerated]
        private sealed class <GetWeatherParam>c__AnonStorey2D5
        {
            internal string iname;

            public <GetWeatherParam>c__AnonStorey2D5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__24B(WeatherParam d)
            {
                return (d.Iname == this.iname);
            }
        }
    }
}

