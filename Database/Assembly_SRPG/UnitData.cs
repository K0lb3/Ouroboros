namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitData
    {
        public const int MAX_JOB = 4;
        public const int MAX_MASTER_ABILITY = 1;
        public const int MAX_JOB_ABILITY = 5;
        public const int MAX_EQUIP_ABILITY = 5;
        public const int MAX_USED_ABILITY = 11;
        private long mUniqueID;
        private SRPG.UnitParam mUnitParam;
        private BaseStatus mStatus;
        public float LastSyncTime;
        private OInt mLv;
        private OInt mExp;
        private OInt mRarity;
        private OInt mAwakeLv;
        private OInt mElement;
        private bool mFavorite;
        private EElement mSupportElement;
        private JobData[] mJobs;
        private OInt mJobIndex;
        private long[] mPartyJobs;
        public TemporaryFlags TempFlags;
        private List<ArtifactParam> mUnlockedSkins;
        private SkillData mLeaderSkill;
        private AbilityData mMasterAbility;
        private AbilityData mCollaboAbility;
        private AbilityData mMapEffectAbility;
        private List<AbilityData> mLearnAbilitys;
        private List<AbilityData> mBattleAbilitys;
        private List<SkillData> mBattleSkills;
        private List<AbilityData> mTobiraMasterAbilitys;
        private SkillData mNormalAttackSkill;
        private QuestClearUnlockUnitDataParam mUnlockedLeaderSkill;
        private List<QuestClearUnlockUnitDataParam> mUnlockedAbilitys;
        private List<QuestClearUnlockUnitDataParam> mUnlockedSkills;
        private List<QuestClearUnlockUnitDataParam> mSkillUnlocks;
        private Dictionary<string, UnitJobOverwriteParam> mUnitJobOverwriteParams;
        private ConceptCardData mConceptCard;
        private List<SRPG.TobiraData> mTobiraData;
        private OInt mUnlockTobiraNum;
        private Dictionary<string, SkillAbilityDeriveData> mJobSkillAbilityDeriveData;
        private SkillAbilityDeriveData mSkillAbilityDeriveData;
        private int mNumJobsAvailable;
        public UnitBadgeTypes BadgeState;
        private static BaseStatus UnitScaleStatus;
        private static BaseStatus WorkScaleStatus;
        private static readonly Dictionary<string, string[]> CONDITIONS_TARGET_NAMES;
        private List<QuestParam> mCharacterQuests;
        [CompilerGenerated]
        private static Predicate<SRPG.TobiraData> <>f__am$cache29;
        [CompilerGenerated]
        private static Action<AbilityData> <>f__am$cache2A;

        static UnitData()
        {
            string[] textArray4;
            string[] textArray3;
            string[] textArray2;
            string[] textArray1;
            Dictionary<string, string[]> dictionary;
            UnitScaleStatus = new BaseStatus();
            WorkScaleStatus = new BaseStatus();
            dictionary = new Dictionary<string, string[]>();
            textArray1 = new string[] { "voice_0017", "voice_0018", "voice_0019" };
            dictionary.Add("UNIT_JOB_MASTER1", textArray1);
            textArray2 = new string[] { "chara_0002", "chara_0003", "chara_0004", "chara_0005", "chara_0006", "chara_0007", "chara_0008", "chara_0009", "chara_0010", "chara_0011" };
            dictionary.Add("UNIT_LEVEL85", textArray2);
            textArray3 = new string[] { "sys_0001", "sys_0002", "sys_0003", "sys_0033", "sys_0035", "sys_0046", "sys_0047", "sys_0050" };
            dictionary.Add("UNIT_LEVEL75", textArray3);
            textArray4 = new string[] { "sys_0009", "sys_0013", "sys_0015", "sys_0017", "sys_0019", "sys_0023", "sys_0024", "sys_0026", "sys_0028" };
            dictionary.Add("UNIT_LEVEL65", textArray4);
            CONDITIONS_TARGET_NAMES = dictionary;
            return;
        }

        public UnitData()
        {
            this.mStatus = new BaseStatus();
            this.mLv = 1;
            this.mExp = 0;
            this.mRarity = 0;
            this.mAwakeLv = 0;
            this.mElement = 0;
            this.mJobIndex = 0;
            this.mUnlockedSkins = new List<ArtifactParam>();
            this.mLearnAbilitys = new List<AbilityData>();
            this.mBattleAbilitys = new List<AbilityData>(11);
            this.mBattleSkills = new List<SkillData>(11);
            this.mTobiraMasterAbilitys = new List<AbilityData>();
            this.mTobiraData = new List<SRPG.TobiraData>();
            this.mUnlockTobiraNum = 0;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <get_IsUnlockTobira>m__4AA(SRPG.TobiraData tobira)
        {
            if (tobira.Param != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return (tobira.Param.TobiraCategory == 0);
        }

        [CompilerGenerated]
        private static void <UpdateUnitBattleAbilityAll>m__4BC(AbilityData ability)
        {
            ability.ResetDeriveAbility();
            return;
        }

        private Json_Job[] AppendUnlockedJobs(Json_Job[] jobs)
        {
            List<Json_Job> list;
            GameManager manager;
            long num;
            int num2;
            List<JobSetParam> list2;
            int num3;
            JobSetParam param;
            JobSetParam[] paramArray;
            int num4;
            int num5;
            JobSetParam param2;
            bool flag;
            int num6;
            int num7;
            int num8;
            Json_Job job;
            <AppendUnlockedJobs>c__AnonStorey3FD storeyfd;
            if (jobs != null)
            {
                goto Label_0008;
            }
            return jobs;
        Label_0008:
            if (this.mUnitParam.jobsets != null)
            {
                goto Label_001A;
            }
            return jobs;
        Label_001A:
            list = new List<Json_Job>(jobs);
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            num = 0L;
            num2 = 0;
            goto Label_004C;
        Label_0031:
            if (jobs[num2].iid <= num)
            {
                goto Label_0048;
            }
            num = jobs[num2].iid;
        Label_0048:
            num2 += 1;
        Label_004C:
            if (num2 < ((int) jobs.Length))
            {
                goto Label_0031;
            }
            list2 = new List<JobSetParam>();
            num3 = 0;
            goto Label_0095;
        Label_0064:
            param = manager.GetJobSetParam(this.mUnitParam.jobsets[num3]);
            if (param != null)
            {
                goto Label_0086;
            }
            goto Label_008F;
        Label_0086:
            list2.Add(param);
        Label_008F:
            num3 += 1;
        Label_0095:
            if (num3 < ((int) this.mUnitParam.jobsets.Length))
            {
                goto Label_0064;
            }
            paramArray = manager.GetClassChangeJobSetParam(this.mUnitParam.iname);
            if (paramArray == null)
            {
                goto Label_00E8;
            }
            num4 = 0;
            goto Label_00DD;
        Label_00CB:
            list2.Add(paramArray[num4]);
            num4 += 1;
        Label_00DD:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_00CB;
            }
        Label_00E8:
            num5 = 0;
            goto Label_01F8;
        Label_00F0:
            param2 = list2[num5];
            flag = 1;
            num6 = -1;
            num7 = 0;
            goto Label_0143;
        Label_0109:
            if (jobs[num7] != null)
            {
                goto Label_0117;
            }
            goto Label_013D;
        Label_0117:
            if ((param2.job == jobs[num7].iname) == null)
            {
                goto Label_013D;
            }
            num6 = num7;
            flag = 0;
            goto Label_014D;
        Label_013D:
            num7 += 1;
        Label_0143:
            if (num7 < ((int) jobs.Length))
            {
                goto Label_0109;
            }
        Label_014D:
            if (flag != null)
            {
                goto Label_01C5;
            }
            if (string.IsNullOrEmpty(param2.jobchange) != null)
            {
                goto Label_01F2;
            }
            storeyfd = new <AppendUnlockedJobs>c__AnonStorey3FD();
            storeyfd.before = manager.GetJobSetParam(param2.jobchange);
            if (storeyfd.before == null)
            {
                goto Label_01F2;
            }
            if (jobs[num6].rank <= 0)
            {
                goto Label_01F2;
            }
            num8 = list.FindIndex(new Predicate<Json_Job>(storeyfd.<>m__4B8));
            if (num8 < 0)
            {
                goto Label_01F2;
            }
            list.RemoveAt(num8);
            goto Label_01F2;
        Label_01C5:
            job = new Json_Job();
            job.iname = param2.job;
            job.iid = num + 1L;
            num += 1L;
            list.Add(job);
        Label_01F2:
            num5 += 1;
        Label_01F8:
            if (num5 < list2.Count)
            {
                goto Label_00F0;
            }
            return list.ToArray();
        }

        public int CalcLevel()
        {
            MasterParam param;
            return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.CalcUnitLevel(this.mExp, this.GetLevelCap(0));
        }

        public unsafe void CalcStatus()
        {
            this.mLv = this.CalcLevel();
            this.CalcStatus(this.mLv, this.mJobIndex, &this.mStatus, -1);
            return;
        }

        public unsafe void CalcStatus(int lv, int jobNo, ref BaseStatus status, int disableJobMasterJobNo)
        {
            JobData data;
            UnitJobOverwriteParam param;
            EquipData[] dataArray;
            int num;
            EquipData data2;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            int num2;
            long num3;
            ArtifactData data3;
            ArtifactData data4;
            List<ConceptCardEquipEffect> list;
            int num4;
            List<SkillData> list2;
            int num5;
            int num6;
            SkillData data5;
            int num7;
            UnitScaleStatus.Clear();
            WorkScaleStatus.Clear();
            SRPG.UnitParam.CalcUnitLevelStatus(this.UnitParam, lv, status);
            data = this.GetJobData(jobNo);
            if (data == null)
            {
                goto Label_0315;
            }
            *(status).param.mov += data.Param.mov;
            *(status).param.jmp += data.Param.jmp;
            param = this.GetUnitJobOverwriteParam(data.JobID);
            if (param == null)
            {
                goto Label_00AE;
            }
            *(status).AddRate(param.mStatus);
            goto Label_00BB;
        Label_00AE:
            *(status).AddRate(data.GetJobRankStatus());
        Label_00BB:
            *(status).Add(data.GetJobTransfarStatus(this.Element));
            dataArray = data.Equips;
            num = 0;
            goto Label_013A;
        Label_00DC:
            data2 = dataArray[num];
            if ((data2 == null) || (data2.IsValid() == null))
            {
                goto Label_0136;
            }
            if (data2.IsEquiped() != null)
            {
                goto Label_0105;
            }
            goto Label_0136;
        Label_0105:
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(data2.Skill, this.Element, status, &WorkScaleStatus);
            UnitScaleStatus.Add(WorkScaleStatus);
        Label_0136:
            num += 1;
        Label_013A:
            if (num < ((int) dataArray.Length))
            {
                goto Label_00DC;
            }
            if ((data.Artifacts == null) || (((int) data.Artifacts.Length) == null))
            {
                goto Label_0246;
            }
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            status5 = new BaseStatus();
            status2.Clear();
            status3.Clear();
            num2 = 0;
            goto Label_0225;
        Label_018D:
            if (data.Artifacts[num2] != null)
            {
                goto Label_01A4;
            }
            goto Label_021F;
        Label_01A4:
            data3 = data.ArtifactDatas[num2];
            if (data3 != null)
            {
                goto Label_01BB;
            }
            goto Label_021F;
        Label_01BB:
            if (data3.EquipSkill == null)
            {
                goto Label_021F;
            }
            status4.Clear();
            status5.Clear();
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(data3.EquipSkill, this.Element, &status4, &status5, &status5, &status4, &WorkScaleStatus);
            status2.ReplaceHighest(status4);
            status3.ReplaceLowest(status5);
            UnitScaleStatus.Add(WorkScaleStatus);
        Label_021F:
            num2 += 1;
        Label_0225:
            if (num2 < ((int) data.Artifacts.Length))
            {
                goto Label_018D;
            }
            *(status).Add(status2);
            *(status).Add(status3);
        Label_0246:
            if (string.IsNullOrEmpty(data.SelectedSkin) != null)
            {
                goto Label_02A2;
            }
            data4 = data.GetSelectedSkinData();
            if ((data4 == null) || (data4.EquipSkill == null))
            {
                goto Label_02A2;
            }
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(data4.EquipSkill, this.Element, status, &WorkScaleStatus);
            UnitScaleStatus.Add(WorkScaleStatus);
        Label_02A2:
            if (this.mConceptCard == null)
            {
                goto Label_039B;
            }
            list = this.mConceptCard.GetEnableEquipEffects(this, data);
            num4 = 0;
            goto Label_0302;
        Label_02C4:
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(list[num4].EquipSkill, this.Element, status, &WorkScaleStatus);
            UnitScaleStatus.Add(WorkScaleStatus);
            num4 += 1;
        Label_0302:
            if (num4 < list.Count)
            {
                goto Label_02C4;
            }
            goto Label_039B;
        Label_0315:
            *(status).param.mov += (this.mUnitParam.no_job_status == null) ? 0 : this.mUnitParam.no_job_status.mov;
            *(status).param.jmp += (this.mUnitParam.no_job_status == null) ? 0 : this.mUnitParam.no_job_status.jmp;
        Label_039B:
            if (this.mTobiraData == null)
            {
                goto Label_0402;
            }
            list2 = TobiraUtility.GetParameterBuffSkills(this.mTobiraData);
            num5 = 0;
            goto Label_03F4;
        Label_03BB:
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(list2[num5], this.Element, status, &WorkScaleStatus);
            UnitScaleStatus.Add(WorkScaleStatus);
            num5 += 1;
        Label_03F4:
            if (num5 < list2.Count)
            {
                goto Label_03BB;
            }
        Label_0402:
            if (this.Jobs == null)
            {
                goto Label_0480;
            }
            num6 = 0;
            goto Label_0471;
        Label_0415:
            data5 = this.Jobs[num6].JobMaster;
            if (data5 != null)
            {
                goto Label_0431;
            }
            goto Label_046B;
        Label_0431:
            if (disableJobMasterJobNo != num6)
            {
                goto Label_043F;
            }
            goto Label_046B;
        Label_043F:
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(data5, this.Element, status, &WorkScaleStatus);
            UnitScaleStatus.Add(WorkScaleStatus);
        Label_046B:
            num6 += 1;
        Label_0471:
            if (num6 < ((int) this.Jobs.Length))
            {
                goto Label_0415;
            }
        Label_0480:
            SRPG.UnitParam.CalcUnitElementStatus(this.Element, status);
            num7 = 0;
            goto Label_04D1;
        Label_0494:
            WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(this.BattleSkills[num7], this.Element, status, &WorkScaleStatus);
            UnitScaleStatus.Add(WorkScaleStatus);
            num7 += 1;
        Label_04D1:
            if (num7 < this.BattleSkills.Count)
            {
                goto Label_0494;
            }
            *(status).AddRate(UnitScaleStatus);
            *(status).param.ApplyMinVal();
            return;
        }

        public int CalcTotalParameter()
        {
            int num;
            num = 0;
            num += this.mStatus.param.atk;
            num += this.mStatus.param.def;
            num += this.mStatus.param.mag;
            num += this.mStatus.param.mnd;
            num += this.mStatus.param.spd;
            num += this.mStatus.param.dex;
            num += this.mStatus.param.cri;
            num += this.mStatus.param.luk;
            return num;
        }

        public bool CanUnlockTobira()
        {
            return MonoSingleton<GameManager>.Instance.MasterParam.CanUnlockTobira(this.UnitID);
        }

        public bool CheckClassChangeJobExist()
        {
            return this.CheckClassChangeJobExist(this.mJobIndex);
        }

        public bool CheckClassChangeJobExist(int jobNo)
        {
            JobData data;
            JobSetParam param;
            int num;
            string str;
            bool flag;
            int num2;
            int num3;
            int num4;
            if (this.UnitParam.jobsets != null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            if (this.GetJobData(jobNo) != null)
            {
                goto Label_0022;
            }
            return 0;
        Label_0022:
            param = this.GetClassChangeJobSet(jobNo);
            if (param != null)
            {
                goto Label_0032;
            }
            return 0;
        Label_0032:
            if (this.Rarity >= param.lock_rarity)
            {
                goto Label_0045;
            }
            return 0;
        Label_0045:
            if (this.AwakeLv >= param.lock_awakelv)
            {
                goto Label_0058;
            }
            return 0;
        Label_0058:
            if (param.lock_jobs == null)
            {
                goto Label_011A;
            }
            num = 0;
            goto Label_010C;
        Label_006A:
            if (param.lock_jobs[num] != null)
            {
                goto Label_007C;
            }
            goto Label_0108;
        Label_007C:
            str = param.lock_jobs[num].iname;
            flag = 0;
            num2 = 0;
            goto Label_00CA;
        Label_0095:
            if (this.mJobs[num2] == null)
            {
                goto Label_00C4;
            }
            if ((this.mJobs[num2].JobID == str) == null)
            {
                goto Label_00C4;
            }
            flag = 1;
            goto Label_00D9;
        Label_00C4:
            num2 += 1;
        Label_00CA:
            if (num2 < ((int) this.mJobs.Length))
            {
                goto Label_0095;
            }
        Label_00D9:
            if (flag != null)
            {
                goto Label_00E5;
            }
            goto Label_0108;
        Label_00E5:
            num3 = param.lock_jobs[num].lv;
            num4 = this.GetJobLevelByJobID(str);
            if (num3 <= num4)
            {
                goto Label_0108;
            }
            return 0;
        Label_0108:
            num += 1;
        Label_010C:
            if (num < ((int) param.lock_jobs.Length))
            {
                goto Label_006A;
            }
        Label_011A:
            return 1;
        }

        public unsafe bool CheckCommon(int index, int slot)
        {
            GameManager manager;
            JobData data;
            string str;
            ItemData data2;
            ItemParam param;
            ItemParam param2;
            ItemData data3;
            int num;
            int num2;
            bool flag;
            manager = MonoSingleton<GameManager>.Instance;
            data = this.Jobs[index];
            str = data.GetRankupItems(data.Rank)[slot];
            data2 = manager.Player.FindItemDataByItemID(str);
            param = manager.GetItemParam(str);
            if ((param != null) && (param.IsCommon != null))
            {
                goto Label_0049;
            }
            return 0;
        Label_0049:
            if ((data2 == null) || (data2.Num < 1))
            {
                goto Label_005D;
            }
            return 0;
        Label_005D:
            param2 = manager.MasterParam.GetCommonEquip(param, data.Rank == 0);
            if (param2 != null)
            {
                goto Label_007E;
            }
            return 0;
        Label_007E:
            data3 = manager.Player.FindItemDataByItemID(param2.iname);
            if (data3 != null)
            {
                goto Label_009B;
            }
            return 0;
        Label_009B:
            if (manager.MasterParam.FixParam.EquipCommonPieceNum != null)
            {
                goto Label_00B2;
            }
            return 0;
        Label_00B2:
            num = *(&(manager.MasterParam.FixParam.EquipCommonPieceNum[param.rare]));
            num2 = (data.Rank <= 0) ? 1 : num;
            flag = (data3 == null) ? 0 : ((data3.Num < num2) == 0);
            return flag;
        }

        public bool CheckEnableEnhanceEquipment()
        {
            int num;
            JobData data;
            EquipData[] dataArray;
            int num2;
            int num3;
            int num4;
            num = 0;
            goto Label_009E;
        Label_0007:
            data = this.Jobs[num];
            if (data == null)
            {
                goto Label_009A;
            }
            if (data.IsActivated != null)
            {
                goto Label_0026;
            }
            goto Label_009A;
        Label_0026:
            dataArray = data.Equips;
            if (dataArray != null)
            {
                goto Label_0038;
            }
            goto Label_009A;
        Label_0038:
            num2 = 0;
            goto Label_0091;
        Label_003F:
            if (dataArray[num2] == null)
            {
                goto Label_008D;
            }
            if (dataArray[num2].IsValid() == null)
            {
                goto Label_008D;
            }
            if (dataArray[num2].IsEquiped() != null)
            {
                goto Label_0066;
            }
            goto Label_008D;
        Label_0066:
            num3 = dataArray[num2].Rank;
            num4 = dataArray[num2].GetRankCap();
            if (num4 <= 1)
            {
                goto Label_008D;
            }
            if (num3 >= num4)
            {
                goto Label_008D;
            }
            return 1;
        Label_008D:
            num2 += 1;
        Label_0091:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_003F;
            }
        Label_009A:
            num += 1;
        Label_009E:
            if (num < ((int) this.Jobs.Length))
            {
                goto Label_0007;
            }
            return 0;
        }

        public bool CheckEnableEquipment(ItemParam item)
        {
            int num;
            JobData data;
            int num2;
            EquipData data2;
            if (item == null)
            {
                goto Label_0012;
            }
            if (item.type == 3)
            {
                goto Label_0014;
            }
        Label_0012:
            return 0;
        Label_0014:
            num = 0;
            goto Label_00B3;
        Label_001B:
            data = this.GetJobData(num);
            if (data == null)
            {
                goto Label_00AF;
            }
            if (data.Equips != null)
            {
                goto Label_0039;
            }
            goto Label_00AF;
        Label_0039:
            num2 = 0;
            goto Label_00A1;
        Label_0040:
            data2 = data.Equips[num2];
            if (data2 == null)
            {
                goto Label_009D;
            }
            if (data2.IsValid() != null)
            {
                goto Label_005F;
            }
            goto Label_009D;
        Label_005F:
            if (data2.IsEquiped() == null)
            {
                goto Label_006F;
            }
            goto Label_009D;
        Label_006F:
            if (data2.ItemParam == item)
            {
                goto Label_0080;
            }
            goto Label_009D;
        Label_0080:
            if (data2.ItemParam.equipLv <= this.Lv)
            {
                goto Label_009B;
            }
            goto Label_009D;
        Label_009B:
            return 1;
        Label_009D:
            num2 += 1;
        Label_00A1:
            if (num2 < ((int) data.Equips.Length))
            {
                goto Label_0040;
            }
        Label_00AF:
            num += 1;
        Label_00B3:
            if (num < this.NumJobsAvailable)
            {
                goto Label_001B;
            }
            return 0;
        }

        private bool CheckEnableEquipmentBadge()
        {
            PlayerData data;
            int num;
            JobData data2;
            int num2;
            EquipData data3;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_00D5;
        Label_0012:
            data2 = this.GetJobData(num);
            if (data2 == null)
            {
                goto Label_00D1;
            }
            if (data2.Equips == null)
            {
                goto Label_00D1;
            }
            if (data2.IsActivated != null)
            {
                goto Label_003B;
            }
            goto Label_00D1;
        Label_003B:
            num2 = 0;
            goto Label_00C3;
        Label_0042:
            data3 = data2.Equips[num2];
            if (data3 == null)
            {
                goto Label_00BF;
            }
            if (data3.IsValid() != null)
            {
                goto Label_0064;
            }
            goto Label_00BF;
        Label_0064:
            if (data3.IsEquiped() == null)
            {
                goto Label_0075;
            }
            goto Label_00BF;
        Label_0075:
            if (data.HasItem(data3.ItemID) != null)
            {
                goto Label_00A1;
            }
            if (data.CheckEnableCreateItem(data3.ItemParam, 1, 1, null) != null)
            {
                goto Label_00A1;
            }
            goto Label_00BF;
        Label_00A1:
            if (data3.ItemParam.equipLv <= this.Lv)
            {
                goto Label_00BD;
            }
            goto Label_00BF;
        Label_00BD:
            return 1;
        Label_00BF:
            num2 += 1;
        Label_00C3:
            if (num2 < ((int) data2.Equips.Length))
            {
                goto Label_0042;
            }
        Label_00D1:
            num += 1;
        Label_00D5:
            if (num < this.NumJobsAvailable)
            {
                goto Label_0012;
            }
            return 0;
        }

        public bool CheckEnableEquipSlot(int jobNo, int slot)
        {
            JobData data;
            data = this.GetJobData(jobNo);
            return ((data == null) ? 0 : data.CheckEnableEquipSlot(slot));
        }

        public bool CheckGainExp()
        {
            int num;
            num = this.GetGainExpCap();
            return (this.Exp < num);
        }

        public bool CheckJobClassChange(int jobNo)
        {
            JobData data;
            data = this.GetJobData(jobNo);
            if (data != null)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            if (this.CheckJobRankUpAllEquip(Array.IndexOf<JobData>(this.mJobs, data), 1) != null)
            {
                goto Label_002A;
            }
            return 0;
        Label_002A:
            return 1;
        }

        public bool CheckJobRankUp()
        {
            return this.CheckJobRankUp(this.mJobIndex);
        }

        public bool CheckJobRankUp(int jobNo)
        {
            return this.CheckJobRankUpInternal(jobNo, 0, 1);
        }

        public bool CheckJobRankUpAllEquip()
        {
            return this.CheckJobRankUpAllEquip(this.mJobIndex, 1);
        }

        public bool CheckJobRankUpAllEquip(int jobNo, bool useCommon)
        {
            return this.CheckJobRankUpInternal(jobNo, 1, useCommon);
        }

        private bool CheckJobRankUpInternal(int jobNo, bool canCreate, bool useCommon)
        {
            JobData data;
            data = this.GetJobData(jobNo);
            if (data.IsActivated == null)
            {
                goto Label_001D;
            }
            return data.CheckJobRankUp(this, canCreate, useCommon);
        Label_001D:
            return this.CheckJobUnlock(jobNo, canCreate);
        }

        public bool CheckJobUnlock(int jobNo, bool canCreate)
        {
            JobData data;
            if (this.CheckJobUnlockable(jobNo) != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            return this.GetJobData(jobNo).CheckJobRankUp(this, canCreate, 1);
        }

        public bool CheckJobUnlockable(int jobNo)
        {
            JobData data;
            JobSetParam param;
            int num;
            string str;
            bool flag;
            int num2;
            int num3;
            int num4;
            data = this.GetJobData(jobNo);
            if (data != null)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            if (data.IsActivated == null)
            {
                goto Label_001D;
            }
            return 1;
        Label_001D:
            param = this.GetJobSetParam(jobNo);
            if (param == null)
            {
                goto Label_003C;
            }
            if (this.Rarity >= param.lock_rarity)
            {
                goto Label_003E;
            }
        Label_003C:
            return 0;
        Label_003E:
            if (this.AwakeLv >= param.lock_awakelv)
            {
                goto Label_0051;
            }
            return 0;
        Label_0051:
            if (param.lock_jobs == null)
            {
                goto Label_0113;
            }
            num = 0;
            goto Label_0105;
        Label_0063:
            if (param.lock_jobs[num] != null)
            {
                goto Label_0075;
            }
            goto Label_0101;
        Label_0075:
            str = param.lock_jobs[num].iname;
            flag = 0;
            num2 = 0;
            goto Label_00C3;
        Label_008E:
            if (this.mJobs[num2] == null)
            {
                goto Label_00BD;
            }
            if ((this.mJobs[num2].JobID == str) == null)
            {
                goto Label_00BD;
            }
            flag = 1;
            goto Label_00D2;
        Label_00BD:
            num2 += 1;
        Label_00C3:
            if (num2 < ((int) this.mJobs.Length))
            {
                goto Label_008E;
            }
        Label_00D2:
            if (flag != null)
            {
                goto Label_00DE;
            }
            goto Label_0101;
        Label_00DE:
            num3 = param.lock_jobs[num].lv;
            num4 = this.GetJobLevelByJobID(str);
            if (num3 <= num4)
            {
                goto Label_0101;
            }
            return 0;
        Label_0101:
            num += 1;
        Label_0105:
            if (num < ((int) param.lock_jobs.Length))
            {
                goto Label_0063;
            }
        Label_0113:
            return 1;
        }

        public bool CheckTobiraIsUnlocked(TobiraParam.Category category)
        {
            SRPG.TobiraData data;
            data = this.GetTobiraData(category);
            return ((data == null) ? 0 : data.IsUnlocked);
        }

        public bool CheckUnitAwaking()
        {
            int num;
            int num2;
            int num3;
            if (this.GetAwakeLevelCap() > this.mAwakeLv)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            num2 = (this.GetPieces() + this.GetElementPieces()) + this.GetCommonPieces();
            if (this.GetAwakeNeedPieces() <= num2)
            {
                goto Label_003F;
            }
            return 0;
        Label_003F:
            return 1;
        }

        public bool CheckUnitAwaking(int awakelv)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = this.GetAwakeLevelCap();
            if (num < this.mAwakeLv)
            {
                goto Label_001F;
            }
            if (num >= awakelv)
            {
                goto Label_0021;
            }
        Label_001F:
            return 0;
        Label_0021:
            num2 = (this.GetPieces() + this.GetElementPieces()) + this.GetCommonPieces();
            num3 = this.mAwakeLv;
            goto Label_0069;
        Label_0047:
            num4 = MonoSingleton<GameManager>.Instance.MasterParam.GetAwakeNeedPieces(num3);
            if (num4 <= num2)
            {
                goto Label_0061;
            }
            return 0;
        Label_0061:
            num2 -= num4;
            num3 += 1;
        Label_0069:
            if (num3 < awakelv)
            {
                goto Label_0047;
            }
            return 1;
        }

        public bool CheckUnitRarityUp()
        {
            int num;
            RecipeParam param;
            int num2;
            RecipeItem item;
            ItemData data;
            if (this.GetRarityLevelCap(this.Rarity) <= this.Lv)
            {
                goto Label_001B;
            }
            return 0;
        Label_001B:
            if (this.GetRarityCap() > this.Rarity)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            param = this.GetRarityUpRecipe();
            if (param != null)
            {
                goto Label_003D;
            }
            return 0;
        Label_003D:
            num2 = 0;
            goto Label_00A5;
        Label_0044:
            item = param.items[num2];
            if (item != null)
            {
                goto Label_0058;
            }
            goto Label_00A1;
        Label_0058:
            if (string.IsNullOrEmpty(item.iname) == null)
            {
                goto Label_006D;
            }
            goto Label_00A1;
        Label_006D:
            data = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemID(item.iname);
            if (data != null)
            {
                goto Label_008D;
            }
            return 0;
        Label_008D:
            if (data.Num >= item.num)
            {
                goto Label_00A1;
            }
            return 0;
        Label_00A1:
            num2 += 1;
        Label_00A5:
            if (num2 < ((int) param.items.Length))
            {
                goto Label_0044;
            }
            return 1;
        }

        public bool CheckUnlockPlaybackVoice()
        {
            if ((this.Rarity + 1) < 5)
            {
                goto Label_0010;
            }
            return 1;
        Label_0010:
            return 0;
        }

        public bool CheckUsedSkin(string afName)
        {
            bool flag;
            int num;
            ArtifactParam param;
            if (string.IsNullOrEmpty(afName) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.mUnlockedSkins == null)
            {
                goto Label_0029;
            }
            if (this.mUnlockedSkins.Count >= 0)
            {
                goto Label_002B;
            }
        Label_0029:
            return 0;
        Label_002B:
            flag = 0;
            num = 0;
            goto Label_0063;
        Label_0034:
            param = this.mUnlockedSkins[num];
            if (param == null)
            {
                goto Label_005F;
            }
            if ((param.iname == afName) == null)
            {
                goto Label_005F;
            }
            flag = 1;
            goto Label_0074;
        Label_005F:
            num += 1;
        Label_0063:
            if (num < this.mUnlockedSkins.Count)
            {
                goto Label_0034;
            }
        Label_0074:
            return flag;
        }

        public static int CompareTo_Iname(UnitData unit1, UnitData unit2)
        {
            return unit2.UnitParam.iname.CompareTo(unit1.UnitParam.iname);
        }

        public static int CompareTo_JobRank(UnitData unit1, UnitData unit2)
        {
            return (unit2.CurrentJob.Rank - unit1.CurrentJob.Rank);
        }

        public static int CompareTo_Lv(UnitData unit1, UnitData unit2)
        {
            return (unit2.Lv - unit1.Lv);
        }

        public static int CompareTo_Rarity(UnitData unit1, UnitData unit2)
        {
            return (unit2.Rarity - unit1.Rarity);
        }

        public static int CompareTo_RarityInit(UnitData unit1, UnitData unit2)
        {
            return (unit2.UnitParam.rare - unit1.UnitParam.rare);
        }

        public static int CompareTo_RarityMax(UnitData unit1, UnitData unit2)
        {
            return (unit2.UnitParam.raremax - unit1.UnitParam.raremax);
        }

        public AbilityData[] CreateEquipAbilitys()
        {
            return this.CreateEquipAbilitys(this.mJobIndex);
        }

        public AbilityData[] CreateEquipAbilitys(int jobIndex)
        {
            AbilityData[] dataArray;
            JobData data;
            int num;
            long num2;
            dataArray = new AbilityData[5];
            Array.Clear(dataArray, 0, (int) dataArray.Length);
            data = this.GetJobData(jobIndex);
            if (data == null)
            {
                goto Label_004B;
            }
            num = 0;
            goto Label_003D;
        Label_0026:
            num2 = data.AbilitySlots[num];
            dataArray[num] = this.GetAbilityData(num2);
            num += 1;
        Label_003D:
            if (num < ((int) data.AbilitySlots.Length))
            {
                goto Label_0026;
            }
        Label_004B:
            return dataArray;
        }

        public List<SkillData> CreateEquipSkills()
        {
            return this.CreateEquipSkills(this.mJobIndex);
        }

        public List<SkillData> CreateEquipSkills(int jobIndex)
        {
            List<SkillData> list;
            JobData data;
            int num;
            long num2;
            AbilityData data2;
            int num3;
            SkillData data3;
            list = new List<SkillData>();
            data = this.GetJobData(jobIndex);
            if (data == null)
            {
                goto Label_00A2;
            }
            num = 0;
            goto Label_0094;
        Label_001B:
            num2 = data.AbilitySlots[num];
            data2 = this.GetAbilityData(num2);
            if (data2 != null)
            {
                goto Label_0039;
            }
            goto Label_0090;
        Label_0039:
            num3 = 0;
            goto Label_007D;
        Label_0041:
            data3 = data2.Skills[num3];
            if (data3 != null)
            {
                goto Label_005D;
            }
            goto Label_0077;
        Label_005D:
            if (list.Contains(data3) == null)
            {
                goto Label_006F;
            }
            goto Label_0077;
        Label_006F:
            list.Add(data3);
        Label_0077:
            num3 += 1;
        Label_007D:
            if (num3 < data2.Skills.Count)
            {
                goto Label_0041;
            }
        Label_0090:
            num += 1;
        Label_0094:
            if (num < ((int) data.AbilitySlots.Length))
            {
                goto Label_001B;
            }
        Label_00A2:
            return list;
        }

        public List<AbilityData> CreateLearnAbilitys()
        {
            return this.CreateLearnAbilitys(this.mJobIndex);
        }

        public List<AbilityData> CreateLearnAbilitys(int jobIndex)
        {
            List<AbilityData> list;
            JobData data;
            int num;
            int num2;
            AbilityData data2;
            list = new List<AbilityData>();
            data = this.GetJobData(jobIndex);
            if (data == null)
            {
                goto Label_0059;
            }
            num = 0;
            goto Label_0048;
        Label_001B:
            if (list.Contains(data.LearnAbilitys[num]) != null)
            {
                goto Label_0044;
            }
            list.Add(data.LearnAbilitys[num]);
        Label_0044:
            num += 1;
        Label_0048:
            if (num < data.LearnAbilitys.Count)
            {
                goto Label_001B;
            }
        Label_0059:
            if (this.mMasterAbility == null)
            {
                goto Label_0081;
            }
            if (list.Contains(this.mMasterAbility) != null)
            {
                goto Label_0081;
            }
            list.Add(this.mMasterAbility);
        Label_0081:
            if (this.mMapEffectAbility == null)
            {
                goto Label_00A9;
            }
            if (list.Contains(this.mMapEffectAbility) != null)
            {
                goto Label_00A9;
            }
            list.Add(this.mMapEffectAbility);
        Label_00A9:
            if (this.mTobiraMasterAbilitys == null)
            {
                goto Label_00FF;
            }
            num2 = 0;
            goto Label_00EE;
        Label_00BB:
            data2 = this.mTobiraMasterAbilitys[num2];
            if (data2 == null)
            {
                goto Label_00EA;
            }
            if (list.Contains(data2) == null)
            {
                goto Label_00E2;
            }
            goto Label_00EA;
        Label_00E2:
            list.Add(data2);
        Label_00EA:
            num2 += 1;
        Label_00EE:
            if (num2 < this.mTobiraMasterAbilitys.Count)
            {
                goto Label_00BB;
            }
        Label_00FF:
            return list;
        }

        public unsafe void Deserialize(Json_Unit json)
        {
            GameManager manager;
            int num;
            string str;
            PlayerPartyTypes types;
            QuestClearUnlockUnitDataParam[] paramArray;
            int num2;
            QuestClearUnlockUnitDataParam param;
            List<int> list;
            int num3;
            int num4;
            int num5;
            int num6;
            QuestClearUnlockUnitDataParam[] paramArray2;
            int num7;
            ArtifactParam[] paramArray3;
            int num8;
            ArtifactParam param2;
            int num9;
            Json_Job[] jobArray;
            int num10;
            JobSetParam[] paramArray4;
            List<JobSetParam> list2;
            int num11;
            int num12;
            JobSetParam param3;
            int num13;
            bool flag;
            int num14;
            int num15;
            int num16;
            JobData data;
            int num17;
            string str2;
            int num18;
            Json_Tobira tobira;
            SRPG.TobiraData data2;
            string str3;
            long num19;
            int num20;
            int num21;
            Json_Ability ability;
            AbilityData data3;
            string str4;
            string str5;
            <Deserialize>c__AnonStorey3FE storeyfe;
            <Deserialize>c__AnonStorey3FF storeyff;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.UpdateSyncTime();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            this.mUnitParam = manager.GetUnitParam(json.iname);
            this.mUniqueID = json.iid;
            this.mRarity = Math.Min(Math.Max(json.rare, this.mUnitParam.rare), this.mUnitParam.raremax);
            this.mAwakeLv = json.plus;
            this.mUnlockTobiraNum = TobiraUtility.GetUnlockTobiraNum(json.doors);
            this.mElement = this.mUnitParam.element;
            this.mExp = json.exp;
            this.mLv = this.CalcLevel();
            this.mJobs = null;
            this.mJobIndex = 0;
            this.mFavorite = (json.fav != 1) ? 0 : 1;
            this.mSupportElement = json.elem;
            this.mPartyJobs = null;
            if (json.select.quests == null)
            {
                goto Label_0184;
            }
            this.mPartyJobs = new long[11];
            num = 0;
            goto Label_0171;
        Label_0129:
            str = json.select.quests[num].qtype;
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_014C;
            }
            goto Label_016D;
        Label_014C:
            types = PartyData.GetPartyTypeFromString(str);
            this.mPartyJobs[types] = json.select.quests[num].jiid;
        Label_016D:
            num += 1;
        Label_0171:
            if (num < ((int) json.select.quests.Length))
            {
                goto Label_0129;
            }
        Label_0184:
            this.mUnlockedLeaderSkill = null;
            this.mUnlockedAbilitys = null;
            this.mUnlockedSkills = null;
            this.mSkillUnlocks = null;
            if ((json.quest_clear_unlocks == null) || (((int) json.quest_clear_unlocks.Length) < 1))
            {
                goto Label_03D0;
            }
            paramArray = new QuestClearUnlockUnitDataParam[(int) json.quest_clear_unlocks.Length];
            num2 = 0;
            goto Label_01FA;
        Label_01D0:
            param = manager.MasterParam.GetUnlockUnitData(json.quest_clear_unlocks[num2]);
            if (param == null)
            {
                goto Label_01F4;
            }
            paramArray[num2] = param;
        Label_01F4:
            num2 += 1;
        Label_01FA:
            if (num2 < ((int) json.quest_clear_unlocks.Length))
            {
                goto Label_01D0;
            }
            if (((int) paramArray.Length) < 1)
            {
                goto Label_03D0;
            }
            list = new List<int>();
            num3 = 0;
            goto Label_02BD;
        Label_0222:
            if (paramArray[num3] == null)
            {
                goto Label_02B7;
            }
            if (paramArray[num3].add == null)
            {
                goto Label_0240;
            }
            goto Label_02B7;
        Label_0240:
            num4 = 0;
            goto Label_02AC;
        Label_0248:
            if (paramArray[num4] == null)
            {
                goto Label_02A6;
            }
            if (num3 != num4)
            {
                goto Label_0260;
            }
            goto Label_02A6;
        Label_0260:
            if ((paramArray[num3].old_id == paramArray[num4].new_id) == null)
            {
                goto Label_02A6;
            }
            if (paramArray[num4].add == null)
            {
                goto Label_0298;
            }
            paramArray[num4] = null;
            goto Label_02A1;
        Label_0298:
            list.Add(num4);
        Label_02A1:
            goto Label_02B7;
        Label_02A6:
            num4 += 1;
        Label_02AC:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_0248;
            }
        Label_02B7:
            num3 += 1;
        Label_02BD:
            if (num3 < ((int) paramArray.Length))
            {
                goto Label_0222;
            }
            num5 = 0;
            goto Label_02E3;
        Label_02D0:
            paramArray[list[num5]] = null;
            num5 += 1;
        Label_02E3:
            if (num5 < list.Count)
            {
                goto Label_02D0;
            }
            num6 = 0;
            goto Label_03C5;
        Label_02F9:
            if (paramArray[num6] != null)
            {
                goto Label_0308;
            }
            goto Label_03BF;
        Label_0308:
            if (paramArray[num6].type != 3)
            {
                goto Label_033E;
            }
            if (this.mUnlockedLeaderSkill != null)
            {
                goto Label_032E;
            }
            this.mUnlockedLeaderSkill = new QuestClearUnlockUnitDataParam();
        Label_032E:
            this.mUnlockedLeaderSkill = paramArray[num6];
            goto Label_03BF;
        Label_033E:
            if ((paramArray[num6].type != 2) && (paramArray[num6].type != 4))
            {
                goto Label_0389;
            }
            if (this.mUnlockedAbilitys != null)
            {
                goto Label_0374;
            }
            this.mUnlockedAbilitys = new List<QuestClearUnlockUnitDataParam>();
        Label_0374:
            this.mUnlockedAbilitys.Add(paramArray[num6]);
            goto Label_03BF;
        Label_0389:
            if (paramArray[num6].type != 1)
            {
                goto Label_03BF;
            }
            if (this.mUnlockedSkills != null)
            {
                goto Label_03AF;
            }
            this.mUnlockedSkills = new List<QuestClearUnlockUnitDataParam>();
        Label_03AF:
            this.mUnlockedSkills.Add(paramArray[num6]);
        Label_03BF:
            num6 += 1;
        Label_03C5:
            if (num6 < ((int) paramArray.Length))
            {
                goto Label_02F9;
            }
        Label_03D0:
            paramArray2 = manager.MasterParam.GetAllUnlockUnitDatas();
            if (paramArray2 == null)
            {
                goto Label_0432;
            }
            this.mSkillUnlocks = new List<QuestClearUnlockUnitDataParam>();
            num7 = 0;
            goto Label_0427;
        Label_03F7:
            if ((paramArray2[num7].uid == this.UnitID) == null)
            {
                goto Label_0421;
            }
            this.mSkillUnlocks.Add(paramArray2[num7]);
        Label_0421:
            num7 += 1;
        Label_0427:
            if (num7 < ((int) paramArray2.Length))
            {
                goto Label_03F7;
            }
        Label_0432:
            if (this.UnitParam.skins == null)
            {
                goto Label_04DE;
            }
            this.mUnlockedSkins.Clear();
            paramArray3 = manager.MasterParam.Artifacts.ToArray();
            num8 = 0;
            goto Label_04CA;
        Label_0467:
            storeyfe = new <Deserialize>c__AnonStorey3FE();
            storeyfe.skinName = this.UnitParam.skins[num8];
            param2 = Array.Find<ArtifactParam>(paramArray3, new Predicate<ArtifactParam>(storeyfe.<>m__4B9));
            if ((param2 == null) || (manager.Player.ItemEntryExists(param2.kakera) == null))
            {
                goto Label_04C4;
            }
            this.mUnlockedSkins.Add(param2);
        Label_04C4:
            num8 += 1;
        Label_04CA:
            if (num8 < ((int) this.UnitParam.skins.Length))
            {
                goto Label_0467;
            }
        Label_04DE:
            if (json.jobs == null)
            {
                goto Label_04FB;
            }
            json.jobs = this.AppendUnlockedJobs(json.jobs);
        Label_04FB:
            if (json.jobs == null)
            {
                goto Label_0512;
            }
            this.SetSkinLockedJob(json.jobs);
        Label_0512:
            this.mMapEffectAbility = null;
            if (json.jobs == null)
            {
                goto Label_08D3;
            }
            this.mJobs = new JobData[(int) json.jobs.Length];
            num9 = 0;
            jobArray = new Json_Job[(int) json.jobs.Length];
            num10 = 0;
            paramArray4 = manager.GetClassChangeJobSetParam(this.mUnitParam.iname);
            list2 = null;
            if ((paramArray4 == null) || (((int) paramArray4.Length) <= 0))
            {
                goto Label_057C;
            }
            list2 = new List<JobSetParam>(paramArray4);
        Label_057C:
            num11 = 0;
            goto Label_0725;
        Label_0584:
            num12 = 0;
            goto Label_070B;
        Label_058C:
            storeyff = new <Deserialize>c__AnonStorey3FF();
            storeyff.jobset = manager.GetJobSetParam(this.mUnitParam.jobsets[num12]);
            param3 = null;
            if ((list2 == null) || (list2.Count <= 0))
            {
                goto Label_05EA;
            }
            num13 = list2.FindIndex(new Predicate<JobSetParam>(storeyff.<>m__4BA));
            if (num13 < 0)
            {
                goto Label_05EA;
            }
            param3 = paramArray4[num13];
        Label_05EA:
            flag = 0;
            num14 = 0;
            goto Label_06A2;
        Label_05F5:
            if (json.jobs[num14] != null)
            {
                goto Label_0608;
            }
            goto Label_069C;
        Label_0608:
            if ((storeyff.jobset.job == json.jobs[num14].iname) == null)
            {
                goto Label_0651;
            }
            jobArray[num10++] = json.jobs[num14];
            json.jobs[num14] = null;
            flag = 1;
            goto Label_06B1;
        Label_0651:
            if ((param3 == null) || ((param3.job == json.jobs[num14].iname) == null))
            {
                goto Label_069C;
            }
            jobArray[num10++] = json.jobs[num14];
            json.jobs[num14] = null;
            flag = 1;
            goto Label_06B1;
        Label_069C:
            num14 += 1;
        Label_06A2:
            if (num14 < ((int) json.jobs.Length))
            {
                goto Label_05F5;
            }
        Label_06B1:
            if (flag == null)
            {
                goto Label_06BD;
            }
            goto Label_0705;
        Label_06BD:
            if (string.IsNullOrEmpty(storeyff.jobset.jobchange) != null)
            {
                goto Label_06F1;
            }
            storeyff.jobset = manager.GetJobSetParam(storeyff.jobset.jobchange);
            goto Label_06F9;
        Label_06F1:
            storeyff.jobset = null;
        Label_06F9:
            if (storeyff.jobset != null)
            {
                goto Label_05EA;
            }
        Label_0705:
            num12 += 1;
        Label_070B:
            if (num12 < ((int) this.mUnitParam.jobsets.Length))
            {
                goto Label_058C;
            }
            num11 += 1;
        Label_0725:
            if (num11 < 2)
            {
                goto Label_0584;
            }
            num15 = 0;
            goto Label_0783;
        Label_0735:
            if (json.jobs[num15] == null)
            {
                goto Label_077D;
            }
            jobArray[num10++] = json.jobs[num15];
            DebugUtility.LogError("JOB_DATA InputError :: INAME => " + json.jobs[num15].iname);
            json.jobs[num15] = null;
        Label_077D:
            num15 += 1;
        Label_0783:
            if (num15 < ((int) json.jobs.Length))
            {
                goto Label_0735;
            }
            json.jobs = jobArray;
            num16 = 0;
            goto Label_07D1;
        Label_07A2:
            data = new JobData();
            data.Deserialize(this, json.jobs[num16]);
            this.mJobs[num9] = data;
            num9 += 1;
            num16 += 1;
        Label_07D1:
            if (num16 < ((int) json.jobs.Length))
            {
                goto Label_07A2;
            }
            if (num9 == ((int) this.mJobs.Length))
            {
                goto Label_07FC;
            }
            Array.Resize<JobData>(&this.mJobs, num9);
        Label_07FC:
            num17 = 0;
            goto Label_0845;
        Label_0804:
            if ((json.select == null) || (this.mJobs[num17].UniqueID != json.select.job))
            {
                goto Label_083F;
            }
            this.mJobIndex = num17;
            goto Label_0854;
        Label_083F:
            num17 += 1;
        Label_0845:
            if (num17 < ((int) this.mJobs.Length))
            {
                goto Label_0804;
            }
        Label_0854:
            if (string.IsNullOrEmpty(this.mJobs[this.mJobIndex].Param.MapEffectAbility) != null)
            {
                goto Label_0912;
            }
            str2 = this.mJobs[this.mJobIndex].Param.MapEffectAbility;
            this.mMapEffectAbility = new AbilityData();
            this.mMapEffectAbility.Setup(this, -1L, str2, 0, 0);
            this.mMapEffectAbility.UpdateLearningsSkill(0, null);
            this.mMapEffectAbility.IsNoneCategory = 1;
            goto Label_0912;
        Label_08D3:
            this.mNormalAttackSkill = new SkillData();
            this.mNormalAttackSkill.Setup((this.UnitParam.no_job_status == null) ? null : this.UnitParam.no_job_status.default_skill, 1, 1, null);
        Label_0912:
            this.mConceptCard = null;
            if ((json.concept_card == null) || (json.concept_card.iid <= 0L))
            {
                goto Label_0953;
            }
            this.mConceptCard = new ConceptCardData();
            this.mConceptCard.Deserialize(json.concept_card);
        Label_0953:
            this.mTobiraData.Clear();
            if (json.doors == null)
            {
                goto Label_09C1;
            }
            num18 = 0;
            goto Label_09B2;
        Label_0971:
            tobira = json.doors[num18];
            data2 = new SRPG.TobiraData();
            data2.Setup(json.iname, tobira.category, tobira.lv);
            this.mTobiraData.Add(data2);
            num18 += 1;
        Label_09B2:
            if (num18 < ((int) json.doors.Length))
            {
                goto Label_0971;
            }
        Label_09C1:
            this.mMasterAbility = null;
            if ((json.abil == null) || (string.IsNullOrEmpty(json.abil.iname) != null))
            {
                goto Label_0A39;
            }
            this.mMasterAbility = new AbilityData();
            str3 = json.abil.iname;
            num19 = json.abil.iid;
            num20 = json.abil.exp;
            this.mMasterAbility.Setup(this, num19, str3, num20, 0);
            this.mMasterAbility.IsNoneCategory = 1;
        Label_0A39:
            this.mCollaboAbility = null;
            if ((json.c_abil == null) || (string.IsNullOrEmpty(json.c_abil.iname) != null))
            {
                goto Label_0ABB;
            }
            this.mCollaboAbility = new AbilityData();
            this.mCollaboAbility.Setup(this, json.c_abil.iid, json.c_abil.iname, json.c_abil.exp, 0);
            this.mCollaboAbility.IsNoneCategory = 1;
            this.mCollaboAbility.UpdateLearningsSkillCollabo(json.c_abil.skills);
        Label_0ABB:
            this.mTobiraMasterAbilitys.Clear();
            if (json.door_abils == null)
            {
                goto Label_0B50;
            }
            num21 = 0;
            goto Label_0B41;
        Label_0AD9:
            ability = json.door_abils[num21];
            if (ability == null)
            {
                goto Label_0B3B;
            }
            if (string.IsNullOrEmpty(ability.iname) == null)
            {
                goto Label_0B01;
            }
            goto Label_0B3B;
        Label_0B01:
            data3 = new AbilityData();
            data3.Setup(this, ability.iid, ability.iname, ability.exp, 0);
            data3.IsNoneCategory = 1;
            this.mTobiraMasterAbilitys.Add(data3);
        Label_0B3B:
            num21 += 1;
        Label_0B41:
            if (num21 < ((int) json.door_abils.Length))
            {
                goto Label_0AD9;
            }
        Label_0B50:
            str4 = TobiraUtility.GetOverwriteLeaderSkill(this.mTobiraData);
            str5 = (this.mUnlockedLeaderSkill == null) ? this.GetLeaderSkillIname(this.mRarity) : this.mUnlockedLeaderSkill.new_id;
            if (string.IsNullOrEmpty(str4) != null)
            {
                goto Label_0B9B;
            }
            str5 = str4;
        Label_0B9B:
            this.mLeaderSkill = null;
            if (string.IsNullOrEmpty(str5) != null)
            {
                goto Label_0BC9;
            }
            this.mLeaderSkill = new SkillData();
            this.mLeaderSkill.Setup(str5, 1, 1, null);
        Label_0BC9:
            this.mUnitJobOverwriteParams = manager.MasterParam.GetUnitJobOverwriteParamsForUnit(this.UnitID);
            this.mJobSkillAbilityDeriveData = manager.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(this.mJobs);
            this.UpdateAvailableJobs();
            this.UpdateUnitLearnAbilityAll();
            this.UpdateUnitBattleAbilityAll();
            this.CalcStatus();
            this.ResetCharacterQuestParams();
            this.FindCharacterQuestParams();
            return;
        }

        public ArtifactData FindArtifactDataBySkillParam(SkillParam param)
        {
            int num;
            JobData data;
            int num2;
            ArtifactData data2;
            int num3;
            AbilityData data3;
            int num4;
            LearningSkill skill;
            if (param == null)
            {
                goto Label_0011;
            }
            if (this.mJobs != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return null;
        Label_0013:
            num = 0;
            goto Label_010E;
        Label_001A:
            data = this.mJobs[num];
            if (data != null)
            {
                goto Label_002E;
            }
            goto Label_010A;
        Label_002E:
            if (data.ArtifactDatas != null)
            {
                goto Label_003E;
            }
            goto Label_010A;
        Label_003E:
            num2 = 0;
            goto Label_00FC;
        Label_0045:
            data2 = data.ArtifactDatas[num2];
            if (data2 != null)
            {
                goto Label_0059;
            }
            goto Label_00F8;
        Label_0059:
            if (data2.LearningAbilities != null)
            {
                goto Label_0069;
            }
            goto Label_00F8;
        Label_0069:
            num3 = 0;
            goto Label_00E6;
        Label_0071:
            data3 = data2.LearningAbilities[num3];
            if (data3 != null)
            {
                goto Label_008C;
            }
            goto Label_00E0;
        Label_008C:
            if (data3.LearningSkills != null)
            {
                goto Label_009D;
            }
            goto Label_00E0;
        Label_009D:
            num4 = 0;
            goto Label_00D0;
        Label_00A5:
            skill = data3.LearningSkills[num4];
            if ((skill.iname == param.iname) == null)
            {
                goto Label_00CA;
            }
            return data2;
        Label_00CA:
            num4 += 1;
        Label_00D0:
            if (num4 < ((int) data3.LearningSkills.Length))
            {
                goto Label_00A5;
            }
        Label_00E0:
            num3 += 1;
        Label_00E6:
            if (num3 < data2.LearningAbilities.Count)
            {
                goto Label_0071;
            }
        Label_00F8:
            num2 += 1;
        Label_00FC:
            if (num2 < ((int) data.ArtifactDatas.Length))
            {
                goto Label_0045;
            }
        Label_010A:
            num += 1;
        Label_010E:
            if (num < ((int) this.mJobs.Length))
            {
                goto Label_001A;
            }
            return null;
        }

        public void FindCharacterQuestParams()
        {
            string str;
            QuestParam[] paramArray;
            int num;
            if (this.mCharacterQuests == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mCharacterQuests = new List<QuestParam>();
            str = GameSettings.Instance.CharacterQuestSection;
            paramArray = MonoSingleton<GameManager>.Instance.Quests;
            num = 0;
            goto Label_0071;
        Label_0034:
            if ((paramArray[num].world == str) == null)
            {
                goto Label_006D;
            }
            if ((paramArray[num].ChapterID == this.UnitID) == null)
            {
                goto Label_006D;
            }
            this.mCharacterQuests.Add(paramArray[num]);
        Label_006D:
            num += 1;
        Label_0071:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0034;
            }
            return;
        }

        private JobSetParam FindClassChangeBase(string jobID)
        {
            SRPG.UnitParam param;
            MasterParam param2;
            int num;
            JobSetParam param3;
            JobSetParam param4;
            param = this.UnitParam;
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
            num = 0;
            goto Label_0092;
        Label_0019:
            if (string.IsNullOrEmpty(param.jobsets[num]) == null)
            {
                goto Label_0030;
            }
            goto Label_008E;
        Label_0030:
            param3 = param2.GetJobSetParam(param.jobsets[num]);
            goto Label_0088;
        Label_0044:
            if (string.IsNullOrEmpty(param3.jobchange) == null)
            {
                goto Label_0059;
            }
            goto Label_008E;
        Label_0059:
            param4 = param3;
            param3 = param2.GetJobSetParam(param3.jobchange);
            if (param3 != null)
            {
                goto Label_0074;
            }
            goto Label_008E;
        Label_0074:
            if ((param3.job == jobID) == null)
            {
                goto Label_0088;
            }
            return param4;
        Label_0088:
            if (param3 != null)
            {
                goto Label_0044;
            }
        Label_008E:
            num += 1;
        Label_0092:
            if (num < ((int) param.jobsets.Length))
            {
                goto Label_0019;
            }
            return null;
        }

        private JobSetParam FindClassChangeBase2(string jobID)
        {
            SRPG.UnitParam param;
            MasterParam param2;
            JobSetParam[] paramArray;
            int num;
            JobSetParam param3;
            JobSetParam param4;
            int num2;
            JobSetParam param5;
            param = this.UnitParam;
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
            paramArray = param2.GetClassChangeJobSetParam(param.iname);
            if (paramArray != null)
            {
                goto Label_0027;
            }
            return null;
        Label_0027:
            num = 0;
            goto Label_00B4;
        Label_002E:
            param3 = paramArray[num];
            if ((param3.job == jobID) == null)
            {
                goto Label_00B0;
            }
            param4 = param2.GetJobSetParam(param3.jobchange);
            if (param4 == null)
            {
                goto Label_00BD;
            }
            num2 = 0;
            goto Label_009C;
        Label_0063:
            param5 = param2.GetJobSetParam(param.jobsets[num2]);
            if (param5 == null)
            {
                goto Label_0096;
            }
            if ((param5.job == param4.job) == null)
            {
                goto Label_0096;
            }
            return param5;
        Label_0096:
            num2 += 1;
        Label_009C:
            if (num2 < ((int) param.jobsets.Length))
            {
                goto Label_0063;
            }
            goto Label_00BD;
        Label_00B0:
            num += 1;
        Label_00B4:
            if (num < ((int) paramArray.Length))
            {
                goto Label_002E;
            }
        Label_00BD:
            return null;
        }

        public List<QuestParam> FindCondQuests()
        {
            return this.mCharacterQuests;
        }

        public JobData FindJobDataBySkillData(SkillParam param)
        {
            int num;
            int num2;
            JobData data;
            int num3;
            OString[] strArray;
            int num4;
            LearningSkill skill;
            int num5;
            LearningSkill skill2;
            LearningSkill[] skillArray;
            int num6;
            int num7;
            <FindJobDataBySkillData>c__AnonStorey3F1 storeyf;
            storeyf = new <FindJobDataBySkillData>c__AnonStorey3F1();
            storeyf.param = param;
            if (storeyf.param == null)
            {
                goto Label_0026;
            }
            if (this.mJobs != null)
            {
                goto Label_0028;
            }
        Label_0026:
            return null;
        Label_0028:
            storeyf.hitAbliId = string.Empty;
            num = 0;
            goto Label_008A;
        Label_003B:
            if (Array.FindIndex<SkillData>(this.BattleAbilitys[num].Skills.ToArray(), new Predicate<SkillData>(storeyf.<>m__4AB)) != -1)
            {
                goto Label_006E;
            }
            goto Label_0086;
        Label_006E:
            storeyf.hitAbliId = this.BattleAbilitys[num].AbilityID;
        Label_0086:
            num += 1;
        Label_008A:
            if (num < this.BattleAbilitys.Count)
            {
                goto Label_003B;
            }
            if (string.IsNullOrEmpty(storeyf.hitAbliId) != null)
            {
                goto Label_0149;
            }
            num2 = 0;
            goto Label_013B;
        Label_00B3:
            data = this.mJobs[num2];
            if (data != null)
            {
                goto Label_00C7;
            }
            goto Label_0137;
        Label_00C7:
            num3 = 0;
            goto Label_0103;
        Label_00CE:
            strArray = data.GetLearningAbilitys(num3);
            if (strArray == null)
            {
                goto Label_00FF;
            }
            if (Array.FindIndex<OString>(strArray, new Predicate<OString>(storeyf.<>m__4AC)) != -1)
            {
                goto Label_00FD;
            }
            goto Label_00FF;
        Label_00FD:
            return data;
        Label_00FF:
            num3 += 1;
        Label_0103:
            if (num3 < JobParam.MAX_JOB_RANK)
            {
                goto Label_00CE;
            }
            if (data.Param == null)
            {
                goto Label_0137;
            }
            if ((data.Param.fixed_ability == storeyf.hitAbliId) == null)
            {
                goto Label_0137;
            }
            return data;
        Label_0137:
            num2 += 1;
        Label_013B:
            if (num2 < ((int) this.mJobs.Length))
            {
                goto Label_00B3;
            }
        Label_0149:
            if (string.IsNullOrEmpty(storeyf.param.job) != null)
            {
                goto Label_022E;
            }
            if (this.mMasterAbility == null)
            {
                goto Label_022E;
            }
            if (this.mMasterAbility.LearningSkills == null)
            {
                goto Label_022E;
            }
            num4 = 0;
            goto Label_021A;
        Label_0182:
            skill = this.mMasterAbility.LearningSkills[num4];
            if (skill.iname == null)
            {
                goto Label_0214;
            }
            if ((skill.iname == storeyf.param.iname) == null)
            {
                goto Label_0214;
            }
            num5 = 0;
            goto Label_0205;
        Label_01C3:
            if (this.mJobs[num5] == null)
            {
                goto Label_01FF;
            }
            if ((this.mJobs[num5].JobID == storeyf.param.job) == null)
            {
                goto Label_01FF;
            }
            return this.mJobs[num5];
        Label_01FF:
            num5 += 1;
        Label_0205:
            if (num5 < ((int) this.mJobs.Length))
            {
                goto Label_01C3;
            }
        Label_0214:
            num4 += 1;
        Label_021A:
            if (num4 < ((int) this.mMasterAbility.LearningSkills.Length))
            {
                goto Label_0182;
            }
        Label_022E:
            if (string.IsNullOrEmpty(storeyf.param.job) != null)
            {
                goto Label_030E;
            }
            if (this.mMapEffectAbility == null)
            {
                goto Label_030E;
            }
            if (this.mMapEffectAbility.LearningSkills == null)
            {
                goto Label_030E;
            }
            skillArray = this.mMapEffectAbility.LearningSkills;
            num6 = 0;
            goto Label_0303;
        Label_0274:
            skill2 = skillArray[num6];
            if (skill2.iname == null)
            {
                goto Label_02FD;
            }
            if ((skill2.iname == storeyf.param.iname) == null)
            {
                goto Label_02FD;
            }
            num7 = 0;
            goto Label_02EE;
        Label_02AC:
            if (this.mJobs[num7] == null)
            {
                goto Label_02E8;
            }
            if ((this.mJobs[num7].JobID == storeyf.param.job) == null)
            {
                goto Label_02E8;
            }
            return this.mJobs[num7];
        Label_02E8:
            num7 += 1;
        Label_02EE:
            if (num7 < ((int) this.mJobs.Length))
            {
                goto Label_02AC;
            }
        Label_02FD:
            num6 += 1;
        Label_0303:
            if (num6 < ((int) skillArray.Length))
            {
                goto Label_0274;
            }
        Label_030E:
            return null;
        }

        public void GainExp(int exp, int playerLv)
        {
            int num;
            int num2;
            GameManager manager;
            num = this.GetGainExpCap(playerLv);
            num2 = this.mLv;
            this.mExp = Math.Min(this.mExp + exp, num);
            this.mLv = this.CalcLevel();
            if (num2 == this.mLv)
            {
                goto Label_008A;
            }
            this.CalcStatus();
            MonoSingleton<GameManager>.GetInstanceDirect().Player.OnUnitLevelChange(this.UnitID, this.mLv - num2, this.mLv, 0);
        Label_008A:
            return;
        }

        public AbilityData GetAbilityData(long iid)
        {
            int num;
            JobData data;
            int num2;
            AbilityData data2;
            int num3;
            AbilityData data3;
            if (iid > 0L)
            {
                goto Label_000A;
            }
            return null;
        Label_000A:
            num = 0;
            goto Label_007A;
        Label_0011:
            data = this.GetJobData(num);
            if (data != null)
            {
                goto Label_0024;
            }
            goto Label_0076;
        Label_0024:
            num2 = 0;
            goto Label_0065;
        Label_002B:
            data2 = data.LearnAbilitys[num2];
            if (data2 == null)
            {
                goto Label_0061;
            }
            if (data2.IsValid() != null)
            {
                goto Label_004E;
            }
            goto Label_0061;
        Label_004E:
            if (data2.UniqueID == iid)
            {
                goto Label_005F;
            }
            goto Label_0061;
        Label_005F:
            return data2;
        Label_0061:
            num2 += 1;
        Label_0065:
            if (num2 < data.LearnAbilitys.Count)
            {
                goto Label_002B;
            }
        Label_0076:
            num += 1;
        Label_007A:
            if (num < ((int) this.mJobs.Length))
            {
                goto Label_0011;
            }
            if (this.mMasterAbility == null)
            {
                goto Label_00AB;
            }
            if (this.mMasterAbility.UniqueID != iid)
            {
                goto Label_00AB;
            }
            return this.mMasterAbility;
        Label_00AB:
            if (this.mMapEffectAbility == null)
            {
                goto Label_00CE;
            }
            if (this.mMapEffectAbility.UniqueID != iid)
            {
                goto Label_00CE;
            }
            return this.mMapEffectAbility;
        Label_00CE:
            if (this.mTobiraMasterAbilitys == null)
            {
                goto Label_0124;
            }
            num3 = 0;
            goto Label_0112;
        Label_00E1:
            data3 = this.mTobiraMasterAbilitys[num3];
            if (data3 != null)
            {
                goto Label_00FC;
            }
            goto Label_010C;
        Label_00FC:
            if (data3.UniqueID != iid)
            {
                goto Label_010C;
            }
            return data3;
        Label_010C:
            num3 += 1;
        Label_0112:
            if (num3 < this.mTobiraMasterAbilitys.Count)
            {
                goto Label_00E1;
            }
        Label_0124:
            return null;
        }

        public unsafe List<AbilityData> GetAllLearnedAbilities(bool enableDerive)
        {
            List<AbilityData> list;
            int num;
            JobData data;
            int num2;
            AbilityData data2;
            int num3;
            AbilityData data3;
            JobData data4;
            SkillAbilityDeriveData data5;
            List<SkillData> list2;
            AbilityData data6;
            List<AbilityData>.Enumerator enumerator;
            SkillData data7;
            List<SkillData>.Enumerator enumerator2;
            list = new List<AbilityData>(0x20);
            num = 0;
            goto Label_008E;
        Label_000F:
            data = this.mJobs[num];
            if (data == null)
            {
                goto Label_008A;
            }
            if (data.IsActivated != null)
            {
                goto Label_002E;
            }
            goto Label_008A;
        Label_002E:
            num2 = 0;
            goto Label_0079;
        Label_0035:
            data2 = data.LearnAbilitys[num2];
            if (data2 == null)
            {
                goto Label_0075;
            }
            if (data2.IsValid() != null)
            {
                goto Label_005B;
            }
            goto Label_0075;
        Label_005B:
            if (list.Contains(data2) == null)
            {
                goto Label_006D;
            }
            goto Label_0075;
        Label_006D:
            list.Add(data2);
        Label_0075:
            num2 += 1;
        Label_0079:
            if (num2 < data.LearnAbilitys.Count)
            {
                goto Label_0035;
            }
        Label_008A:
            num += 1;
        Label_008E:
            if (num < ((int) this.mJobs.Length))
            {
                goto Label_000F;
            }
            if (this.mMasterAbility == null)
            {
                goto Label_00C4;
            }
            if (list.Contains(this.mMasterAbility) != null)
            {
                goto Label_00C4;
            }
            list.Add(this.mMasterAbility);
        Label_00C4:
            if (this.mMapEffectAbility == null)
            {
                goto Label_00EC;
            }
            if (list.Contains(this.mMapEffectAbility) != null)
            {
                goto Label_00EC;
            }
            list.Add(this.mMapEffectAbility);
        Label_00EC:
            if (this.mTobiraMasterAbilitys == null)
            {
                goto Label_0147;
            }
            num3 = 0;
            goto Label_0135;
        Label_00FF:
            data3 = this.mTobiraMasterAbilitys[num3];
            if (data3 == null)
            {
                goto Label_012F;
            }
            if (list.Contains(data3) == null)
            {
                goto Label_0127;
            }
            goto Label_012F;
        Label_0127:
            list.Add(data3);
        Label_012F:
            num3 += 1;
        Label_0135:
            if (num3 < this.mTobiraMasterAbilitys.Count)
            {
                goto Label_00FF;
            }
        Label_0147:
            if (enableDerive == null)
            {
                goto Label_0234;
            }
            if (this.mJobSkillAbilityDeriveData == null)
            {
                goto Label_0234;
            }
            data4 = this.CurrentJob;
            data5 = null;
            if (this.mJobSkillAbilityDeriveData.TryGetValue(data4.Param.iname, &data5) == null)
            {
                goto Label_0234;
            }
            list2 = new List<SkillData>();
            enumerator = list.GetEnumerator();
        Label_0190:
            try
            {
                goto Label_020C;
            Label_0195:
                data6 = &enumerator.Current;
                if (data6.Skills == null)
                {
                    goto Label_020C;
                }
                enumerator2 = data6.Skills.GetEnumerator();
            Label_01B8:
                try
                {
                    goto Label_01EE;
                Label_01BD:
                    data7 = &enumerator2.Current;
                    if (data7 != null)
                    {
                        goto Label_01D2;
                    }
                    goto Label_01EE;
                Label_01D2:
                    if (list2.Contains(data7) == null)
                    {
                        goto Label_01E5;
                    }
                    goto Label_01EE;
                Label_01E5:
                    list2.Add(data7);
                Label_01EE:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_01BD;
                    }
                    goto Label_020C;
                }
                finally
                {
                Label_01FF:
                    ((List<SkillData>.Enumerator) enumerator2).Dispose();
                }
            Label_020C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0195;
                }
                goto Label_022A;
            }
            finally
            {
            Label_021D:
                ((List<AbilityData>.Enumerator) enumerator).Dispose();
            }
        Label_022A:
            RefrectionDeriveSkillAndAbility(data5, list, list2);
        Label_0234:
            return list;
        }

        public ArtifactParam[] GetAllSkins(int jobIndex)
        {
            List<ArtifactParam> list;
            ArtifactParam[] paramArray;
            ArtifactParam param;
            <GetAllSkins>c__AnonStorey3F6 storeyf;
            if (this.mUnitParam.skins != null)
            {
                goto Label_0017;
            }
            return new ArtifactParam[0];
        Label_0017:
            if (jobIndex != -1)
            {
                goto Label_002B;
            }
            jobIndex = this.mJobIndex;
        Label_002B:
            list = new List<ArtifactParam>();
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray();
            storeyf = new <GetAllSkins>c__AnonStorey3F6();
            storeyf.<>f__this = this;
            storeyf.i = 0;
            goto Label_009F;
        Label_005F:
            param = Array.Find<ArtifactParam>(paramArray, new Predicate<ArtifactParam>(storeyf.<>m__4B2));
            if ((param == null) || (param.CheckEnableEquip(this, this.JobIndex) == null))
            {
                goto Label_0091;
            }
            list.Add(param);
        Label_0091:
            storeyf.i += 1;
        Label_009F:
            if (storeyf.i < ((int) this.mUnitParam.skins.Length))
            {
                goto Label_005F;
            }
            return ((list.Count < 1) ? new ArtifactParam[0] : list.ToArray());
        }

        public int GetAttackHeight(SkillData skill, bool is_range)
        {
            return this.GetAttackHeight(this.mJobIndex, skill, is_range);
        }

        public int GetAttackHeight(int jobNo, SkillData skill, bool is_range)
        {
            SkillData data;
            int num;
            data = skill;
            if (data == null)
            {
                goto Label_001E;
            }
            if (data.IsReactionSkill() == null)
            {
                goto Label_002E;
            }
            if (data.IsBattleSkill() == null)
            {
                goto Label_002E;
            }
        Label_001E:
            data = this.GetAttackSkill(jobNo);
            if (data != null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            num = data.EnableAttackGridHeight;
            if (is_range == null)
            {
                goto Label_004D;
            }
            if (skill.TeleportType == null)
            {
                goto Label_004D;
            }
            num = data.TeleportHeight;
        Label_004D:
            return num;
        }

        public int GetAttackRangeMax(SkillData skill)
        {
            return this.GetAttackRangeMax(this.mJobIndex, skill);
        }

        public int GetAttackRangeMax(int jobNo, SkillData skill)
        {
            SkillData data;
            int num;
            data = skill;
            if (data == null)
            {
                goto Label_001E;
            }
            if (data.IsReactionSkill() == null)
            {
                goto Label_002E;
            }
            if (data.IsBattleSkill() == null)
            {
                goto Label_002E;
            }
        Label_001E:
            data = this.GetAttackSkill(jobNo);
            if (data != null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            return data.RangeMax;
        }

        public int GetAttackRangeMin(SkillData skill)
        {
            return this.GetAttackRangeMin(this.mJobIndex, skill);
        }

        public int GetAttackRangeMin(int jobNo, SkillData skill)
        {
            SkillData data;
            int num;
            data = skill;
            if (data == null)
            {
                goto Label_001E;
            }
            if (data.IsReactionSkill() == null)
            {
                goto Label_002E;
            }
            if (data.IsBattleSkill() == null)
            {
                goto Label_002E;
            }
        Label_001E:
            data = this.GetAttackSkill(jobNo);
            if (data != null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            return data.RangeMin;
        }

        public int GetAttackScope(SkillData skill)
        {
            return this.GetAttackScope(this.mJobIndex, skill);
        }

        public int GetAttackScope(int jobNo, SkillData skill)
        {
            SkillData data;
            int num;
            data = skill;
            if (data == null)
            {
                goto Label_001E;
            }
            if (data.IsReactionSkill() == null)
            {
                goto Label_002E;
            }
            if (data.IsBattleSkill() == null)
            {
                goto Label_002E;
            }
        Label_001E:
            data = this.GetAttackSkill(jobNo);
            if (data != null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            return data.Scope;
        }

        public SkillData GetAttackSkill()
        {
            return this.GetAttackSkill(this.mJobIndex);
        }

        public SkillData GetAttackSkill(int jobNo)
        {
            JobData data;
            data = this.GetJobData(jobNo);
            if (data == null)
            {
                goto Label_0015;
            }
            return data.GetAttackSkill();
        Label_0015:
            return this.mNormalAttackSkill;
        }

        public int GetAwakeCost()
        {
            return 0;
        }

        public int GetAwakeLevelCap()
        {
            RarityParam param;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.Rarity);
            return ((param == null) ? 0 : param.UnitAwakeLvCap);
        }

        public int GetAwakeNeedPieces()
        {
            return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetAwakeNeedPieces(this.AwakeLv);
        }

        public JobData GetBaseJob(string jobID)
        {
            JobSetParam param;
            int num;
            param = this.FindClassChangeBase2(jobID);
            if (param != null)
            {
                goto Label_0010;
            }
            return null;
        Label_0010:
            num = 0;
            goto Label_0041;
        Label_0017:
            if ((this.mJobs[num].JobID == param.job) == null)
            {
                goto Label_003D;
            }
            return this.mJobs[num];
        Label_003D:
            num += 1;
        Label_0041:
            if (num < this.mNumJobsAvailable)
            {
                goto Label_0017;
            }
            return null;
        }

        public int GetChangePieces()
        {
            RarityParam param;
            string str;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.Rarity);
            str = FlowNode_Variable.Get("UNIT_SELECT_TICKET");
            if ((string.IsNullOrEmpty(str) != null) || (int.Parse(str) != 1))
            {
                goto Label_0044;
            }
            return param.UnitSelectChangePieceNum;
        Label_0044:
            return ((param == null) ? 0 : param.UnitChangePieceNum);
        }

        public unsafe CharacterQuestParam[] GetCharaEpisodeList()
        {
            List<QuestParam> list;
            CharacterQuestParam[] paramArray;
            int num;
            CharacterQuestParam param;
            string str;
            if (this.IsSetCharacterQuest() != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            list = new List<QuestParam>();
            list = this.FindCondQuests();
            if (list.Count > 0)
            {
                goto Label_0028;
            }
            return null;
        Label_0028:
            paramArray = new CharacterQuestParam[list.Count];
            num = 0;
            goto Label_00A2;
        Label_003B:
            param = new CharacterQuestParam();
            param.EpisodeNum = num + 1;
            param.Param = list[num];
            param.EpisodeTitle = list[num].name;
            param.IsNew = list[num].state == 0;
            str = string.Empty;
            param.IsAvailable = list[num].IsEntryQuestConditionCh(this, &str);
            paramArray[num] = param;
            num += 1;
        Label_00A2:
            if (num < list.Count)
            {
                goto Label_003B;
            }
            return paramArray;
        }

        public JobData GetClassChangeJobData(int jobNo)
        {
            JobData[] dataArray;
            JobParam param;
            int num;
            dataArray = this.Jobs;
            if (dataArray == null)
            {
                goto Label_0020;
            }
            if (jobNo < 0)
            {
                goto Label_0020;
            }
            if (this.mNumJobsAvailable > jobNo)
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            param = this.GetClassChangeJobParam(jobNo);
            num = this.mNumJobsAvailable;
            goto Label_004C;
        Label_0036:
            if (dataArray[num].Param != param)
            {
                goto Label_0048;
            }
            return dataArray[num];
        Label_0048:
            num += 1;
        Label_004C:
            if (num < ((int) dataArray.Length))
            {
                goto Label_0036;
            }
            return null;
        }

        public JobParam GetClassChangeJobParam()
        {
            return this.GetClassChangeJobParam(this.mJobIndex);
        }

        public JobParam GetClassChangeJobParam(int jobNo)
        {
            JobSetParam param;
            param = this.GetClassChangeJobSet(jobNo);
            return ((param == null) ? null : MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(param.job));
        }

        public JobSetParam GetClassChangeJobSet(int jobNo)
        {
            JobData data;
            JobSetParam param;
            if (this.UnitParam.jobsets != null)
            {
                goto Label_0012;
            }
            return null;
        Label_0012:
            data = this.GetJobData(jobNo);
            if (data == null)
            {
                goto Label_0033;
            }
            if (jobNo < ((int) this.UnitParam.jobsets.Length))
            {
                goto Label_0035;
            }
        Label_0033:
            return null;
        Label_0035:
            param = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(this.UnitParam.jobsets[jobNo]);
            goto Label_008C;
        Label_0052:
            if ((param.job == data.JobID) == null)
            {
                goto Label_007B;
            }
            return MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(param.jobchange);
        Label_007B:
            param = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(param.jobchange);
        Label_008C:
            if (param != null)
            {
                goto Label_0052;
            }
            return null;
        }

        public int GetCombination()
        {
            return (7 + this.AwakeLv);
        }

        public int GetCombinationRange()
        {
            if (this.AwakeLv <= 20)
            {
                goto Label_000F;
            }
            return 3;
        Label_000F:
            if (this.AwakeLv <= 10)
            {
                goto Label_001E;
            }
            return 2;
        Label_001E:
            return 1;
        }

        public ItemData GetCommonPieceData()
        {
            ItemParam param;
            param = this.GetCommonPieceParam();
            if (param != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            return MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemParam(param);
        }

        public ItemParam GetCommonPieceParam()
        {
            FixParam param;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (string.IsNullOrEmpty(param.CommonPieceAll) == null)
            {
                goto Label_0027;
            }
            return null;
        Label_0027:
            return MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(param.CommonPieceAll);
        }

        public int GetCommonPieces()
        {
            ItemData data;
            data = this.GetCommonPieceData();
            if (data != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            return data.Num;
        }

        public CharacterQuestParam GetCurrentCharaEpisodeData()
        {
            CharacterQuestParam[] paramArray;
            int num;
            if (this.IsSetCharacterQuest() != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            paramArray = this.GetCharaEpisodeList();
            if (paramArray != null)
            {
                goto Label_001C;
            }
            return null;
        Label_001C:
            num = 0;
            goto Label_003E;
        Label_0023:
            if (paramArray[num].Param.state == 2)
            {
                goto Label_003A;
            }
            return paramArray[num];
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0023;
            }
            return null;
        }

        public ItemData GetElementPieceData()
        {
            ItemParam param;
            param = this.GetElementPieceParam();
            if (param != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            return MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemParam(param);
        }

        public ItemParam GetElementPieceParam()
        {
            FixParam param;
            string str;
            EElement element;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            str = string.Empty;
            switch ((this.Element - 1))
            {
                case 0:
                    goto Label_0042;

                case 1:
                    goto Label_0053;

                case 2:
                    goto Label_0075;

                case 3:
                    goto Label_0064;

                case 4:
                    goto Label_0086;

                case 5:
                    goto Label_0097;
            }
            goto Label_00A8;
        Label_0042:
            str = param.CommonPieceFire;
            goto Label_00AD;
        Label_0053:
            str = param.CommonPieceWater;
            goto Label_00AD;
        Label_0064:
            str = param.CommonPieceThunder;
            goto Label_00AD;
        Label_0075:
            str = param.CommonPieceWind;
            goto Label_00AD;
        Label_0086:
            str = param.CommonPieceShine;
            goto Label_00AD;
        Label_0097:
            str = param.CommonPieceDark;
            goto Label_00AD;
        Label_00A8:;
        Label_00AD:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_00BA;
            }
            return null;
        Label_00BA:
            return MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(str);
        }

        public int GetElementPieces()
        {
            ItemData data;
            data = this.GetElementPieceData();
            if (data != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            return data.Num;
        }

        public ArtifactParam[] GetEnableConceptCardSkins(int jobIndex)
        {
            List<ArtifactParam> list;
            ArtifactParam param;
            <GetEnableConceptCardSkins>c__AnonStorey3F7 storeyf;
            <GetEnableConceptCardSkins>c__AnonStorey3F8 storeyf2;
            storeyf = new <GetEnableConceptCardSkins>c__AnonStorey3F7();
            if (this.mConceptCard != null)
            {
                goto Label_0018;
            }
            return new ArtifactParam[0];
        Label_0018:
            if (jobIndex != -1)
            {
                goto Label_002C;
            }
            jobIndex = this.mJobIndex;
        Label_002C:
            list = new List<ArtifactParam>();
            storeyf.equip_effects = this.mConceptCard.GetEnableEquipEffects(this, this.mJobs[jobIndex]);
            storeyf2 = new <GetEnableConceptCardSkins>c__AnonStorey3F8();
            storeyf2.<>f__ref$1015 = storeyf;
            storeyf2.i = 0;
            goto Label_00C6;
        Label_0065:
            if (string.IsNullOrEmpty(storeyf.equip_effects[storeyf2.i].Skin) == null)
            {
                goto Label_008A;
            }
            goto Label_00B8;
        Label_008A:
            param = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.Find(new Predicate<ArtifactParam>(storeyf2.<>m__4B3));
            if (param == null)
            {
                goto Label_00B8;
            }
            list.Add(param);
        Label_00B8:
            storeyf2.i += 1;
        Label_00C6:
            if (storeyf2.i < storeyf.equip_effects.Count)
            {
                goto Label_0065;
            }
            return list.ToArray();
        }

        public ArtifactParam GetEquipArmArtifact()
        {
            return this.GetEquipArtifactParam(0, null);
        }

        public ArtifactData GetEquipArtifactData(int slot, JobData job)
        {
            JobData data;
            ArtifactData[] dataArray;
            data = job;
            if (job != null)
            {
                goto Label_000F;
            }
            data = this.CurrentJob;
        Label_000F:
            if (data == null)
            {
                goto Label_003F;
            }
            if (data.Artifacts == null)
            {
                goto Label_003F;
            }
            if (((int) data.Artifacts.Length) <= slot)
            {
                goto Label_003F;
            }
            return this.GetSortedArtifactDatas(data.ArtifactDatas)[slot];
        Label_003F:
            return null;
        }

        public ArtifactParam GetEquipArtifactParam(int slot, JobData job)
        {
            JobData data;
            ArtifactData data2;
            data = job;
            if (job != null)
            {
                goto Label_000F;
            }
            data = this.CurrentJob;
        Label_000F:
            if (job == null)
            {
                goto Label_004C;
            }
            data2 = this.GetEquipArtifactData(slot, data);
            if (data2 == null)
            {
                goto Label_002B;
            }
            return data2.ArtifactParam;
        Label_002B:
            if (slot != null)
            {
                goto Label_004C;
            }
            return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(data.Param.artifact);
        Label_004C:
            return null;
        }

        public int GetExp()
        {
            int num;
            int num2;
            num = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp(this.mLv);
            num2 = this.mExp - num;
            return num2;
        }

        public int GetGainExpCap()
        {
            return this.GetGainExpCap(MonoSingleton<GameManager>.Instance.Player.Lv);
        }

        public int GetGainExpCap(int playerLv)
        {
            int num;
            int num2;
            num = this.GetLevelCap(0);
            num2 = 0;
            if (num <= playerLv)
            {
                goto Label_002D;
            }
            num = playerLv + 1;
            num2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp(num) - 1;
            goto Label_003E;
        Label_002D:
            num2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp(num);
        Label_003E:
            return num2;
        }

        public JobData GetJobData(int jobNo)
        {
            JobData[] dataArray;
            dataArray = this.Jobs;
            if (dataArray == null)
            {
                goto Label_001D;
            }
            if (jobNo < 0)
            {
                goto Label_001D;
            }
            if (((int) dataArray.Length) > jobNo)
            {
                goto Label_001F;
            }
        Label_001D:
            return null;
        Label_001F:
            return dataArray[jobNo];
        }

        public JobData GetJobFor(PlayerPartyTypes type)
        {
            long num;
            int num2;
            if (this.mPartyJobs == null)
            {
                goto Label_0066;
            }
            if (type >= ((int) this.mPartyJobs.Length))
            {
                goto Label_0066;
            }
            if (this.mPartyJobs[type] == null)
            {
                goto Label_0066;
            }
            num = this.mPartyJobs[type];
            num2 = ((int) this.mJobs.Length) - 1;
            goto Label_005F;
        Label_003F:
            if (this.mJobs[num2].UniqueID != num)
            {
                goto Label_005B;
            }
            return this.mJobs[num2];
        Label_005B:
            num2 -= 1;
        Label_005F:
            if (num2 >= 0)
            {
                goto Label_003F;
            }
        Label_0066:
            return this.CurrentJob;
        }

        public int GetJobLevelByJobID(string iname)
        {
            int num;
            num = 0;
            goto Label_0036;
        Label_0007:
            if ((this.Jobs[num].Param.iname == iname) == null)
            {
                goto Label_0032;
            }
            return this.Jobs[num].Rank;
        Label_0032:
            num += 1;
        Label_0036:
            if (num < ((int) this.Jobs.Length))
            {
                goto Label_0007;
            }
            return 0;
        }

        public JobParam GetJobParam(int jobNo)
        {
            JobData data;
            data = this.GetJobData(jobNo);
            return ((data == null) ? null : data.Param);
        }

        public int GetJobRankCap()
        {
            return JobParam.GetJobRankCap(this.Rarity);
        }

        public unsafe List<ItemData> GetJobRankUpReturnItemData(int jobNo, bool ignoreEquiped)
        {
            JobData data;
            List<ItemData> list;
            int num;
            EquipData data2;
            List<ItemData> list2;
            ItemData data3;
            List<ItemData>.Enumerator enumerator;
            data = this.GetJobData(jobNo);
            if (data != null)
            {
                goto Label_0010;
            }
            return null;
        Label_0010:
            list = new List<ItemData>();
            num = 0;
            goto Label_00A2;
        Label_001D:
            data2 = data.Equips[num];
            if (data2 == null)
            {
                goto Label_009E;
            }
            if (data2.IsValid() == null)
            {
                goto Label_009E;
            }
            if (data2.IsEquiped() != null)
            {
                goto Label_004D;
            }
            if (ignoreEquiped != null)
            {
                goto Label_004D;
            }
            goto Label_009E;
        Label_004D:
            list2 = data2.GetReturnItemList();
            if (list2 != null)
            {
                goto Label_0061;
            }
            goto Label_009E;
        Label_0061:
            enumerator = list2.GetEnumerator();
        Label_006A:
            try
            {
                goto Label_0080;
            Label_006F:
                data3 = &enumerator.Current;
                list.Add(data3);
            Label_0080:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_006F;
                }
                goto Label_009E;
            }
            finally
            {
            Label_0091:
                ((List<ItemData>.Enumerator) enumerator).Dispose();
            }
        Label_009E:
            num += 1;
        Label_00A2:
            if (num < ((int) data.Equips.Length))
            {
                goto Label_001D;
            }
            return list;
        }

        public JobSetParam GetJobSetParam(int jobNo)
        {
            if (jobNo < 0)
            {
                goto Label_0015;
            }
            if (jobNo < ((int) this.mJobs.Length))
            {
                goto Label_0017;
            }
        Label_0015:
            return null;
        Label_0017:
            return this.GetJobSetParam2(this.mJobs[jobNo].JobID);
        }

        public JobSetParam GetJobSetParam(string jobID)
        {
            MasterParam param;
            int num;
            JobSetParam param2;
            if (this.UnitParam.jobsets != null)
            {
                goto Label_0012;
            }
            return null;
        Label_0012:
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
            num = 0;
            goto Label_0083;
        Label_0024:
            if (string.IsNullOrEmpty(this.UnitParam.jobsets[num]) == null)
            {
                goto Label_0040;
            }
            goto Label_007F;
        Label_0040:
            param2 = param.GetJobSetParam(this.UnitParam.jobsets[num]);
            goto Label_0079;
        Label_0059:
            if ((param2.job == jobID) == null)
            {
                goto Label_006C;
            }
            return param2;
        Label_006C:
            param2 = param.GetJobSetParam(param2.jobchange);
        Label_0079:
            if (param2 != null)
            {
                goto Label_0059;
            }
        Label_007F:
            num += 1;
        Label_0083:
            if (num < ((int) this.UnitParam.jobsets.Length))
            {
                goto Label_0024;
            }
            return null;
        }

        public JobSetParam GetJobSetParam2(string jobID)
        {
            MasterParam param;
            List<string> list;
            int num;
            JobSetParam[] paramArray;
            int num2;
            int num3;
            JobSetParam param2;
            if (this.UnitParam.jobsets != null)
            {
                goto Label_0012;
            }
            return null;
        Label_0012:
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
            list = new List<string>();
            num = 0;
            goto Label_0041;
        Label_002A:
            list.Add(this.UnitParam.jobsets[num]);
            num += 1;
        Label_0041:
            if (num < ((int) this.UnitParam.jobsets.Length))
            {
                goto Label_002A;
            }
            paramArray = param.GetClassChangeJobSetParam(this.UnitParam.iname);
            if (paramArray == null)
            {
                goto Label_009C;
            }
            if (((int) paramArray.Length) <= 0)
            {
                goto Label_009C;
            }
            num2 = 0;
            goto Label_0092;
        Label_007D:
            list.Add(paramArray[num2].iname);
            num2 += 1;
        Label_0092:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_007D;
            }
        Label_009C:
            num3 = 0;
            goto Label_0101;
        Label_00A4:
            if (string.IsNullOrEmpty(list[num3]) == null)
            {
                goto Label_00BB;
            }
            goto Label_00FB;
        Label_00BB:
            param2 = param.GetJobSetParam(list[num3]);
            goto Label_00F4;
        Label_00D0:
            if ((param2.job == jobID) == null)
            {
                goto Label_00E5;
            }
            return param2;
        Label_00E5:
            param2 = param.GetJobSetParam(param2.jobchange);
        Label_00F4:
            if (param2 != null)
            {
                goto Label_00D0;
            }
        Label_00FB:
            num3 += 1;
        Label_0101:
            if (num3 < list.Count)
            {
                goto Label_00A4;
            }
            return null;
        }

        public string GetLeaderSkillIname(int rarity)
        {
            if (rarity < 0)
            {
                goto Label_001A;
            }
            if (((int) this.mUnitParam.leader_skills.Length) > rarity)
            {
                goto Label_001C;
            }
        Label_001A:
            return null;
        Label_001C:
            return this.mUnitParam.leader_skills[rarity];
        }

        public int GetLevelCap(bool bPlayerLvCap)
        {
            int num;
            num = this.GetRarityLevelCap(this.Rarity) + this.AwakeLv;
            num += TobiraUtility.GetAdditionalUnitLevelCapWithUnlockNum(this.UnlockTobriaNum);
            if (bPlayerLvCap == null)
            {
                goto Label_0040;
            }
            num = Math.Min(num, MonoSingleton<GameManager>.Instance.Player.Lv);
        Label_0040:
            return num;
        }

        public int GetMoveCount()
        {
            return this.mStatus.param.mov;
        }

        public int GetMoveHeight()
        {
            return this.mStatus.param.jmp;
        }

        public int GetNextExp()
        {
            MasterParam param;
            int num;
            int num2;
            int num3;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
            num = this.GetLevelCap(0);
            num2 = 0;
            num3 = 0;
            goto Label_0050;
        Label_001C:
            num2 += param.GetUnitNextExp(num3 + 1);
            if (num2 > this.mExp)
            {
                goto Label_003E;
            }
            goto Label_004C;
        Label_003E:
            return (num2 - this.mExp);
        Label_004C:
            num3 += 1;
        Label_0050:
            if (num3 < num)
            {
                goto Label_001C;
            }
            return 0;
        }

        public int GetNextLevel()
        {
            return Math.Min(this.Lv + 1, this.GetLevelCap(0));
        }

        public int GetPieces()
        {
            SRPG.UnitParam param;
            ItemData data;
            param = this.UnitParam;
            if (string.IsNullOrEmpty(param.piece) == null)
            {
                goto Label_0019;
            }
            return 0;
        Label_0019:
            data = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemID(param.piece);
            if (data != null)
            {
                goto Label_0037;
            }
            return 0;
        Label_0037:
            return data.Num;
        }

        public List<string> GetQuestUnlockConditions(QuestParam quest)
        {
            List<string> list;
            int num;
            QuestClearUnlockUnitDataParam param;
            string str;
            <GetQuestUnlockConditions>c__AnonStorey3FC storeyfc;
            storeyfc = new <GetQuestUnlockConditions>c__AnonStorey3FC();
            storeyfc.quest = quest;
            if (this.mSkillUnlocks != null)
            {
                goto Label_001C;
            }
            return null;
        Label_001C:
            list = new List<string>();
            num = 0;
            goto Label_009D;
        Label_0029:
            param = this.mSkillUnlocks[num];
            if ((param.uid != this.UnitID) != null)
            {
                goto Label_0099;
            }
            if (Array.FindIndex<string>(param.qids, new Predicate<string>(storeyfc.<>m__4B7)) != -1)
            {
                goto Label_006F;
            }
            goto Label_0099;
        Label_006F:
            str = this.mSkillUnlocks[num].GetCondText(this.mUnitParam);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0099;
            }
            list.Add(str);
        Label_0099:
            num += 1;
        Label_009D:
            if (num < this.mSkillUnlocks.Count)
            {
                goto Label_0029;
            }
            return list;
        }

        public EquipData GetRankupEquipData(int jobNo, int slot)
        {
            JobData data;
            data = this.GetJobData(jobNo);
            if (data == null)
            {
                goto Label_0019;
            }
            if (data.Equips != null)
            {
                goto Label_001B;
            }
        Label_0019:
            return null;
        Label_001B:
            if (slot < 0)
            {
                goto Label_0030;
            }
            if (slot < ((int) data.Equips.Length))
            {
                goto Label_0032;
            }
        Label_0030:
            return null;
        Label_0032:
            return data.Equips[slot];
        }

        public EquipData[] GetRankupEquips(int jobNo)
        {
            JobData data;
            data = this.GetJobData(jobNo);
            return ((data == null) ? null : data.Equips);
        }

        public int GetRarityCap()
        {
            return this.UnitParam.raremax;
        }

        public int GetRarityLevelCap(int rarity)
        {
            MasterParam param;
            GrowParam param2;
            int num;
            RarityParam param3;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
            param2 = param.GetGrowParam(this.UnitParam.grow);
            num = (param2 == null) ? 0 : param2.GetLevelCap();
            param3 = param.GetRarityParam(rarity);
            if (param3 == null)
            {
                goto Label_0055;
            }
            num = Math.Min(num, param3.UnitLvCap);
        Label_0055:
            return num;
        }

        public int GetRarityUpCost()
        {
            RarityParam param;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.Rarity);
            return ((param == null) ? 0 : param.UnitRarityUpCost);
        }

        public RecipeParam GetRarityUpRecipe()
        {
            return this.GetRarityUpRecipe(this.Rarity);
        }

        public RecipeParam GetRarityUpRecipe(int rarity)
        {
            return ((this.UnitParam == null) ? null : this.UnitParam.GetRarityUpRecipe(rarity));
        }

        public ArtifactParam[] GetSelectableSkins(JobParam jobParam)
        {
            int num;
            <GetSelectableSkins>c__AnonStorey3F5 storeyf;
            storeyf = new <GetSelectableSkins>c__AnonStorey3F5();
            storeyf.jobParam = jobParam;
            if (storeyf.jobParam != null)
            {
                goto Label_001A;
            }
            return null;
        Label_001A:
            num = Array.FindIndex<JobData>(this.mJobs, new Predicate<JobData>(storeyf.<>m__4B1));
            if (num != -1)
            {
                goto Label_003B;
            }
            return null;
        Label_003B:
            return this.GetSelectableSkins(num);
        }

        public ArtifactParam[] GetSelectableSkins(int jobIndex)
        {
            List<ArtifactParam> list;
            int num;
            ArtifactParam param;
            if (jobIndex != -1)
            {
                goto Label_0014;
            }
            jobIndex = this.mJobIndex;
        Label_0014:
            list = new List<ArtifactParam>();
            num = 0;
            goto Label_0051;
        Label_0021:
            param = this.mUnlockedSkins[num];
            if ((param == null) || (param.CheckEnableEquip(this, this.JobIndex) == null))
            {
                goto Label_004D;
            }
            list.Add(param);
        Label_004D:
            num += 1;
        Label_0051:
            if (num < this.mUnlockedSkins.Count)
            {
                goto Label_0021;
            }
            return ((list.Count < 1) ? new ArtifactParam[0] : list.ToArray());
        }

        public ArtifactParam GetSelectedSkin(int jobIndex)
        {
            if (jobIndex != -1)
            {
                goto Label_0014;
            }
            jobIndex = this.mJobIndex;
        Label_0014:
            return this.GetSelectedSkinData(jobIndex);
        }

        public ArtifactParam GetSelectedSkinData(int jobIndex)
        {
            List<ArtifactParam> list;
            <GetSelectedSkinData>c__AnonStorey3F2 storeyf;
            storeyf = new <GetSelectedSkinData>c__AnonStorey3F2();
            storeyf.jobIndex = jobIndex;
            storeyf.<>f__this = this;
            if (storeyf.jobIndex != -1)
            {
                goto Label_0031;
            }
            storeyf.jobIndex = this.mJobIndex;
        Label_0031:
            if (this.mJobs == null)
            {
                goto Label_006A;
            }
            if (this.mJobs[storeyf.jobIndex] == null)
            {
                goto Label_006A;
            }
            if (string.IsNullOrEmpty(this.mJobs[storeyf.jobIndex].SelectedSkin) == null)
            {
                goto Label_006C;
            }
        Label_006A:
            return null;
        Label_006C:
            return Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), new Predicate<ArtifactParam>(storeyf.<>m__4AD));
        }

        public ArtifactParam[] GetSelectedSkins()
        {
            ArtifactParam[] paramArray;
            int num;
            if (this.Jobs != null)
            {
                goto Label_0020;
            }
            if (((int) this.Jobs.Length) >= 1)
            {
                goto Label_0020;
            }
            return new ArtifactParam[0];
        Label_0020:
            paramArray = new ArtifactParam[(int) this.Jobs.Length];
            num = 0;
            goto Label_0043;
        Label_0035:
            paramArray[num] = this.GetSelectedSkinData(num);
            num += 1;
        Label_0043:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0035;
            }
            return paramArray;
        }

        public SkillAbilityDeriveData GetSkillAbilityDeriveData(JobData target_job, ArtifactTypes replace_target_slot, ArtifactParam new_artifact)
        {
            JobData data;
            int num;
            List<ArtifactParam> list;
            int num2;
            ArtifactParam param;
            string[] strArray;
            int num3;
            data = (target_job != null) ? target_job : this.CurrentJob;
            num = JobData.GetArtifactSlotIndex(replace_target_slot);
            list = new List<ArtifactParam>();
            num2 = 0;
            goto Label_0054;
        Label_0027:
            param = this.GetEquipArtifactParam(num2, data);
            if (new_artifact == null)
            {
                goto Label_0041;
            }
            if (num2 != num)
            {
                goto Label_0041;
            }
            param = new_artifact;
        Label_0041:
            if (param == null)
            {
                goto Label_0050;
            }
            list.Add(param);
        Label_0050:
            num2 += 1;
        Label_0054:
            if (num2 < 3)
            {
                goto Label_0027;
            }
            strArray = new string[list.Count];
            num3 = 0;
            goto Label_0088;
        Label_0070:
            strArray[num3] = list[num3].iname;
            num3 += 1;
        Label_0088:
            if (num3 < list.Count)
            {
                goto Label_0070;
            }
            return MonoSingleton<GameManager>.Instance.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(strArray);
        }

        public unsafe SkillData GetSkillData(string iname)
        {
            int num;
            num = 0;
            return this.GetSkillData(iname, &num);
        }

        public unsafe SkillData GetSkillData(string iname, ref int jobIndex)
        {
            int num;
            JobData data;
            int num2;
            AbilityData data2;
            int num3;
            SkillData data3;
            string str;
            SkillData data4;
            List<SkillData>.Enumerator enumerator;
            SkillData data5;
            SkillData data6;
            List<SkillData>.Enumerator enumerator2;
            SkillData data7;
            SkillData data8;
            List<SkillData>.Enumerator enumerator3;
            SkillData data9;
            int num4;
            AbilityData data10;
            SkillData data11;
            List<SkillData>.Enumerator enumerator4;
            SkillData data12;
            num = 0;
            goto Label_010A;
        Label_0007:
            data = this.GetJobData(num);
            if (data != null)
            {
                goto Label_001A;
            }
            goto Label_0106;
        Label_001A:
            num2 = 0;
            goto Label_00F5;
        Label_0021:
            data2 = data.LearnAbilitys[num2];
            if (data2 == null)
            {
                goto Label_00F1;
            }
            if (data2.IsValid() != null)
            {
                goto Label_0044;
            }
            goto Label_00F1;
        Label_0044:
            num3 = 0;
            goto Label_00DF;
        Label_004C:
            data3 = data2.Skills[num3];
            if (data3 == null)
            {
                goto Label_00D9;
            }
            if (data3.IsValid() != null)
            {
                goto Label_0073;
            }
            goto Label_00D9;
        Label_0073:
            if ((data3.SkillID != iname) == null)
            {
                goto Label_00D6;
            }
            str = this.SearchReplacementSkill(iname);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00B0;
            }
            if ((data3.SkillID == str) == null)
            {
                goto Label_00B0;
            }
            return data3;
        Label_00B0:
            if (data3.IsDerivedSkill == null)
            {
                goto Label_00D9;
            }
            if ((data3.m_BaseSkillIname == iname) == null)
            {
                goto Label_00D9;
            }
            return data3;
            goto Label_00D9;
        Label_00D6:
            return data3;
        Label_00D9:
            num3 += 1;
        Label_00DF:
            if (num3 < data2.Skills.Count)
            {
                goto Label_004C;
            }
        Label_00F1:
            num2 += 1;
        Label_00F5:
            if (num2 < data.LearnAbilitys.Count)
            {
                goto Label_0021;
            }
        Label_0106:
            num += 1;
        Label_010A:
            if (num < ((int) this.mJobs.Length))
            {
                goto Label_0007;
            }
            if (this.mMasterAbility == null)
            {
                goto Label_01C6;
            }
            enumerator = this.mMasterAbility.Skills.GetEnumerator();
        Label_0135:
            try
            {
                goto Label_017B;
            Label_013A:
                data4 = &enumerator.Current;
                if (data4 == null)
                {
                    goto Label_017B;
                }
                if (data4.IsValid() != null)
                {
                    goto Label_015B;
                }
                goto Label_017B;
            Label_015B:
                if ((data4.SkillID != iname) == null)
                {
                    goto Label_0172;
                }
                goto Label_017B;
            Label_0172:
                data12 = data4;
                goto Label_03D2;
            Label_017B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_013A;
                }
                goto Label_0199;
            }
            finally
            {
            Label_018C:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_0199:
            if (this.mMasterAbility.IsDeriveBaseAbility == null)
            {
                goto Label_01C6;
            }
            data5 = this.mMasterAbility.DerivedAbility.FindSkillDataInSkills(iname);
            if (data5 == null)
            {
                goto Label_01C6;
            }
            return data5;
        Label_01C6:
            if (this.mCollaboAbility == null)
            {
                goto Label_026F;
            }
            enumerator2 = this.mCollaboAbility.Skills.GetEnumerator();
        Label_01E3:
            try
            {
                goto Label_0229;
            Label_01E8:
                data6 = &enumerator2.Current;
                if (data6 == null)
                {
                    goto Label_0229;
                }
                if (data6.IsValid() != null)
                {
                    goto Label_0209;
                }
                goto Label_0229;
            Label_0209:
                if ((data6.SkillID != iname) == null)
                {
                    goto Label_0220;
                }
                goto Label_0229;
            Label_0220:
                data12 = data6;
                goto Label_03D2;
            Label_0229:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_01E8;
                }
                goto Label_0247;
            }
            finally
            {
            Label_023A:
                ((List<SkillData>.Enumerator) enumerator2).Dispose();
            }
        Label_0247:
            if (this.mCollaboAbility.IsDeriveBaseAbility == null)
            {
                goto Label_026F;
            }
            data7 = this.mCollaboAbility.FindSkillDataInSkills(iname);
            if (data7 == null)
            {
                goto Label_026F;
            }
            return data7;
        Label_026F:
            if (this.mMapEffectAbility == null)
            {
                goto Label_0318;
            }
            enumerator3 = this.mMapEffectAbility.Skills.GetEnumerator();
        Label_028C:
            try
            {
                goto Label_02D2;
            Label_0291:
                data8 = &enumerator3.Current;
                if (data8 == null)
                {
                    goto Label_02D2;
                }
                if (data8.IsValid() != null)
                {
                    goto Label_02B2;
                }
                goto Label_02D2;
            Label_02B2:
                if ((data8.SkillID != iname) == null)
                {
                    goto Label_02C9;
                }
                goto Label_02D2;
            Label_02C9:
                data12 = data8;
                goto Label_03D2;
            Label_02D2:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0291;
                }
                goto Label_02F0;
            }
            finally
            {
            Label_02E3:
                ((List<SkillData>.Enumerator) enumerator3).Dispose();
            }
        Label_02F0:
            if (this.mMapEffectAbility.IsDeriveBaseAbility == null)
            {
                goto Label_0318;
            }
            data9 = this.mMapEffectAbility.FindSkillDataInSkills(iname);
            if (data9 == null)
            {
                goto Label_0318;
            }
            return data9;
        Label_0318:
            if (this.mTobiraMasterAbilitys == null)
            {
                goto Label_03D0;
            }
            num4 = 0;
            goto Label_03BE;
        Label_032B:
            data10 = this.mTobiraMasterAbilitys[num4];
            if (data10 != null)
            {
                goto Label_0346;
            }
            goto Label_03B8;
        Label_0346:
            enumerator4 = data10.Skills.GetEnumerator();
        Label_0354:
            try
            {
                goto Label_039A;
            Label_0359:
                data11 = &enumerator4.Current;
                if (data11 == null)
                {
                    goto Label_039A;
                }
                if (data11.IsValid() != null)
                {
                    goto Label_037A;
                }
                goto Label_039A;
            Label_037A:
                if ((data11.SkillID != iname) == null)
                {
                    goto Label_0391;
                }
                goto Label_039A;
            Label_0391:
                data12 = data11;
                goto Label_03D2;
            Label_039A:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_0359;
                }
                goto Label_03B8;
            }
            finally
            {
            Label_03AB:
                ((List<SkillData>.Enumerator) enumerator4).Dispose();
            }
        Label_03B8:
            num4 += 1;
        Label_03BE:
            if (num4 < this.mTobiraMasterAbilitys.Count)
            {
                goto Label_032B;
            }
        Label_03D0:
            return null;
        Label_03D2:
            return data12;
        }

        public int GetSkillUsedCost(SkillData skill)
        {
            return this.GetSkillUsedCost(skill.SkillParam);
        }

        public int GetSkillUsedCost(SkillParam skill)
        {
            int num;
            num = skill.cost;
            if (skill.effect_type == 14)
            {
                goto Label_0065;
            }
            if (skill.type == null)
            {
                goto Label_0065;
            }
            if (skill.type == 3)
            {
                goto Label_0065;
            }
            num = Math.Max(num + this.Status[0x2a], 0);
            num += (num * this.Status[14]) / 100;
        Label_0065:
            return num;
        }

        public ArtifactData[] GetSortedArtifactDatas(ArtifactData[] artifacts)
        {
            Dictionary<int, List<ArtifactData>> dictionary;
            int num;
            int num2;
            List<ArtifactData> list;
            int num3;
            int num4;
            int num5;
            int num6;
            if (artifacts == null)
            {
                goto Label_000F;
            }
            if (((int) artifacts.Length) > 0)
            {
                goto Label_0016;
            }
        Label_000F:
            return new ArtifactData[3];
        Label_0016:
            dictionary = new Dictionary<int, List<ArtifactData>>();
            num = 0;
            goto Label_0069;
        Label_0023:
            if (artifacts[num] != null)
            {
                goto Label_0030;
            }
            goto Label_0065;
        Label_0030:
            num2 = artifacts[num].ArtifactParam.type;
            if (dictionary.ContainsKey(num2) != null)
            {
                goto Label_0056;
            }
            dictionary.Add(num2, new List<ArtifactData>());
        Label_0056:
            dictionary[num2].Add(artifacts[num]);
        Label_0065:
            num += 1;
        Label_0069:
            if (num < ((int) artifacts.Length))
            {
                goto Label_0023;
            }
            list = new List<ArtifactData>();
            num3 = (int) Enum.GetNames(typeof(ArtifactTypes)).Length;
            num4 = 1;
            goto Label_00B9;
        Label_0093:
            if (dictionary.ContainsKey(num4) != null)
            {
                goto Label_00A5;
            }
            goto Label_00B3;
        Label_00A5:
            list.AddRange(dictionary[num4]);
        Label_00B3:
            num4 += 1;
        Label_00B9:
            if (num4 <= num3)
            {
                goto Label_0093;
            }
            num5 = 3 - list.Count;
            if (num5 <= 0)
            {
                goto Label_00F2;
            }
            num6 = 0;
            goto Label_00E9;
        Label_00DC:
            list.Add(null);
            num6 += 1;
        Label_00E9:
            if (num6 < num5)
            {
                goto Label_00DC;
            }
        Label_00F2:
            return list.ToArray();
        }

        public SRPG.TobiraData GetTobiraData(TobiraParam.Category category)
        {
            <GetTobiraData>c__AnonStorey40D storeyd;
            storeyd = new <GetTobiraData>c__AnonStorey40D();
            storeyd.category = category;
            return this.mTobiraData.Find(new Predicate<SRPG.TobiraData>(storeyd.<>m__4CE));
        }

        public unsafe UnitJobOverwriteParam GetUnitJobOverwriteParam(string job_iname)
        {
            UnitJobOverwriteParam param;
            param = null;
            if (this.mUnitJobOverwriteParams == null)
            {
                goto Label_001C;
            }
            this.mUnitJobOverwriteParams.TryGetValue(job_iname, &param);
        Label_001C:
            return param;
        }

        public string GetUnitJobVoiceSheetName(int jobIndex)
        {
            JobData data;
            if (jobIndex != -1)
            {
                goto Label_000F;
            }
            jobIndex = this.JobIndex;
        Label_000F:
            data = ((this.Jobs == null) || (this.Jobs[jobIndex] == null)) ? null : this.Jobs[jobIndex];
            return AssetPath.UnitVoiceFileName(this.UnitParam, null, this.UnitParam.GetJobVoice((data == null) ? string.Empty : data.JobID));
        }

        public UnitPlaybackVoiceData GetUnitPlaybackVoiceData()
        {
            string str;
            string str2;
            string str3;
            MySound.Voice voice;
            CriAtomExAcb acb;
            CriAtomEx.CueInfo[] infoArray;
            UnitPlaybackVoiceData data;
            str = this.GetUnitSkinVoiceSheetName(-1);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_001F;
            }
            DebugUtility.LogError("UnitDataにボイス設定が存在しません");
            return null;
        Label_001F:
            str2 = "VO_" + str;
            str3 = this.GetUnitSkinVoiceCueName(-1) + "_";
            voice = new MySound.Voice(str2, str, str3, 0);
            acb = voice.FindAcb(str2);
            if (acb != null)
            {
                goto Label_0063;
            }
            DebugUtility.LogError("Acbファイルが存在しません");
            return null;
        Label_0063:
            infoArray = acb.GetCueInfoList();
            if (infoArray == null)
            {
                goto Label_007D;
            }
            if (((int) infoArray.Length) > 0)
            {
                goto Label_0089;
            }
        Label_007D:
            DebugUtility.LogError("CueInfoが存在しません");
            return null;
        Label_0089:
            data = new UnitPlaybackVoiceData();
            data.Init(this, voice, infoArray, str);
            return data;
        }

        public string GetUnitSkinVoiceCueName(int jobIndex)
        {
            return AssetPath.UnitVoiceFileName(this.UnitParam, null, string.Empty);
        }

        public string GetUnitSkinVoiceSheetName(int jobIndex)
        {
            ArtifactParam param;
            JobData data;
            if (jobIndex != -1)
            {
                goto Label_000F;
            }
            jobIndex = this.JobIndex;
        Label_000F:
            param = this.GetSelectedSkinData(jobIndex);
            data = ((this.Jobs == null) || (this.Jobs[jobIndex] == null)) ? null : this.Jobs[jobIndex];
            return AssetPath.UnitVoiceFileName(this.UnitParam, param, this.UnitParam.GetJobVoice((data == null) ? string.Empty : data.JobID));
        }

        public unsafe string GetUnlockTobiraBirthID()
        {
            FixParam param;
            int num;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (this.mUnitParam.birthID == null)
            {
                goto Label_0032;
            }
            if (this.mUnitParam.birthID != 100)
            {
                goto Label_0038;
            }
        Label_0032:
            return string.Empty;
        Label_0038:
            num = this.mUnitParam.birthID - 1;
            if (num >= 0)
            {
                goto Label_005D;
            }
            DebugUtility.LogError("インデックスが有効範囲内にありません");
            return string.Empty;
        Label_005D:
            return *(&(param.TobiraUnlockBirth[num]));
        }

        public unsafe string GetUnlockTobiraElementID()
        {
            FixParam param;
            EElement element;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            switch ((this.Element - 1))
            {
                case 0:
                    goto Label_003C;

                case 1:
                    goto Label_0053;

                case 2:
                    goto Label_0081;

                case 3:
                    goto Label_006A;

                case 4:
                    goto Label_0098;

                case 5:
                    goto Label_00AF;
            }
            goto Label_00C6;
        Label_003C:
            return *(&(param.TobiraUnlockElem[0]));
        Label_0053:
            return *(&(param.TobiraUnlockElem[1]));
        Label_006A:
            return *(&(param.TobiraUnlockElem[3]));
        Label_0081:
            return *(&(param.TobiraUnlockElem[2]));
        Label_0098:
            return *(&(param.TobiraUnlockElem[4]));
        Label_00AF:
            return *(&(param.TobiraUnlockElem[5]));
        Label_00C6:
            return string.Empty;
        }

        public bool IsChQuestParentUnlocked(QuestParam quest)
        {
            bool flag;
            List<AbilityData> list;
            List<QuestClearUnlockUnitDataParam> list2;
            int num;
            AbilityData data;
            <IsChQuestParentUnlocked>c__AnonStorey40B storeyb;
            <IsChQuestParentUnlocked>c__AnonStorey40C storeyc;
            QuestClearUnlockUnitDataParam.EUnlockTypes types;
            storeyb = new <IsChQuestParentUnlocked>c__AnonStorey40B();
            storeyb.quest = quest;
            if (storeyb.quest != null)
            {
                goto Label_001D;
            }
            return 0;
        Label_001D:
            flag = 1;
            list = this.GetAllLearnedAbilities(0);
            list2 = this.SkillUnlocks.FindAll(new Predicate<QuestClearUnlockUnitDataParam>(storeyb.<>m__4C9));
            if (list2 != null)
            {
                goto Label_0048;
            }
            return 1;
        Label_0048:
            num = 0;
            goto Label_01B4;
        Label_004F:
            storeyc = new <IsChQuestParentUnlocked>c__AnonStorey40C();
            storeyc.unlock = list2[num];
            data = null;
            if (storeyc.unlock.qcnd == null)
            {
                goto Label_007D;
            }
            goto Label_01B0;
        Label_007D:
            types = storeyc.unlock.type;
            if (types == 1)
            {
                goto Label_00C4;
            }
            if (types == 2)
            {
                goto Label_00A0;
            }
            goto Label_00E7;
        Label_00A0:
            flag = (Array.Find<JobData>(this.Jobs, new Predicate<JobData>(storeyc.<>m__4CA)) == null) == 0;
            goto Label_00EC;
        Label_00C4:
            data = list.Find(new Predicate<AbilityData>(storeyc.<>m__4CB));
            flag = (data == null) == 0;
            goto Label_00EC;
        Label_00E7:;
        Label_00EC:
            if ((flag == null) || (storeyc.unlock.add != null))
            {
                goto Label_01A8;
            }
            switch ((storeyc.unlock.type - 1))
            {
                case 0:
                    goto Label_014E;

                case 1:
                    goto Label_012F;

                case 2:
                    goto Label_01A3;

                case 3:
                    goto Label_0173;
            }
            goto Label_01A3;
        Label_012F:
            flag = (list.Find(new Predicate<AbilityData>(storeyc.<>m__4CC)) == null) == 0;
            goto Label_01A8;
        Label_014E:
            flag = (Array.Find<LearningSkill>(data.LearningSkills, new Predicate<LearningSkill>(storeyc.<>m__4CD)) == null) == 0;
            goto Label_01A8;
        Label_0173:
            flag = (this.MasterAbility == null) ? 0 : (this.MasterAbility.AbilityID == storeyc.unlock.old_id);
            goto Label_01A8;
        Label_01A3:;
        Label_01A8:
            if (flag != null)
            {
                goto Label_01B0;
            }
            return 0;
        Label_01B0:
            num += 1;
        Label_01B4:
            if (num < list2.Count)
            {
                goto Label_004F;
            }
            return 1;
        }

        public bool IsJobAvailable(int jobNo)
        {
            return ((0 > jobNo) ? 0 : (jobNo < this.mNumJobsAvailable));
        }

        public bool IsOpenCharacterQuest()
        {
            OInt num;
            OInt num2;
            num = this.CharacterQuestRarity;
            num2 = SRPG.UnitParam.MASTER_QUEST_LV;
            if (num <= -1)
            {
                goto Label_002F;
            }
            if (num2 > 0)
            {
                goto Label_0031;
            }
        Label_002F:
            return 0;
        Label_0031:
            if (this.Rarity < num)
            {
                goto Label_0053;
            }
            if (this.Lv >= num2)
            {
                goto Label_0055;
            }
        Label_0053:
            return 0;
        Label_0055:
            if (this.IsSetCharacterQuest() != null)
            {
                goto Label_0062;
            }
            return 0;
        Label_0062:
            return 1;
        }

        public bool IsQuestClearUnlocked(string id, QuestClearUnlockUnitDataParam.EUnlockTypes type)
        {
            QuestClearUnlockUnitDataParam param;
            <IsQuestClearUnlocked>c__AnonStorey3FB storeyfb;
            QuestClearUnlockUnitDataParam.EUnlockTypes types;
            storeyfb = new <IsQuestClearUnlocked>c__AnonStorey3FB();
            storeyfb.id = id;
            storeyfb.type = type;
            if (string.IsNullOrEmpty(storeyfb.id) != null)
            {
                goto Label_00C5;
            }
            param = null;
            switch (storeyfb.type)
            {
                case 0:
                    goto Label_00B3;

                case 1:
                    goto Label_0074;

                case 2:
                    goto Label_004C;

                case 3:
                    goto Label_009C;

                case 4:
                    goto Label_004C;
            }
            goto Label_00B8;
        Label_004C:
            if (this.mUnlockedAbilitys == null)
            {
                goto Label_00BD;
            }
            param = this.mUnlockedAbilitys.Find(new Predicate<QuestClearUnlockUnitDataParam>(storeyfb.<>m__4B5));
            goto Label_00BD;
        Label_0074:
            if (this.mUnlockedSkills == null)
            {
                goto Label_00BD;
            }
            param = this.mUnlockedSkills.Find(new Predicate<QuestClearUnlockUnitDataParam>(storeyfb.<>m__4B6));
            goto Label_00BD;
        Label_009C:
            if (this.mUnlockedLeaderSkill == null)
            {
                goto Label_00BD;
            }
            param = this.mUnlockedLeaderSkill;
            goto Label_00BD;
        Label_00B3:
            goto Label_00BD;
        Label_00B8:;
        Label_00BD:
            if (param == null)
            {
                goto Label_00C5;
            }
            return 1;
        Label_00C5:
            return 0;
        }

        public bool IsQuestUnlocked(CharacterQuestUnlockProgress progress)
        {
            CharacterQuestParam param;
            bool flag;
            int num;
            int num2;
            param = this.GetCurrentCharaEpisodeData();
            flag = this.IsChQuestParentUnlocked(param.Param);
            num = param.Param.EntryConditionCh.ulvmin;
            num2 = param.Param.EntryConditionCh.rmin - 1;
            if (flag == null)
            {
                goto Label_00A8;
            }
            if (num > this.Lv)
            {
                goto Label_00A8;
            }
            if (num2 > this.Rarity)
            {
                goto Label_00A8;
            }
            if (progress == null)
            {
                goto Label_00A6;
            }
            if ((progress.CondQuest.iname == param.Param.iname) == null)
            {
                goto Label_00A8;
            }
            if (progress.ClearUnlocksCond == null)
            {
                goto Label_009F;
            }
            if (num > progress.Level)
            {
                goto Label_009F;
            }
            if (num2 <= progress.Rarity)
            {
                goto Label_00A8;
            }
        Label_009F:
            return 1;
            goto Label_00A8;
        Label_00A6:
            return 1;
        Label_00A8:
            return 0;
        }

        public bool IsSetCharacterQuest()
        {
            return ((this.mCharacterQuests == null) ? 0 : (this.mCharacterQuests.Count > 0));
        }

        public bool IsSetSkin(int jobIndex)
        {
            if (jobIndex != -1)
            {
                goto Label_0014;
            }
            jobIndex = this.mJobIndex;
        Label_0014:
            if (this.mJobs == null)
            {
                goto Label_004C;
            }
            if (this.mJobs[this.JobIndex] == null)
            {
                goto Label_004C;
            }
            return (string.IsNullOrEmpty(this.mJobs[this.JobIndex].SelectedSkin) == 0);
        Label_004C:
            return 0;
        }

        public bool IsSkinUnlocked()
        {
            int num;
            num = this.mUnlockedSkins.Count + ((int) this.GetEnableConceptCardSkins(-1).Length);
            return ((num < 1) == 0);
        }

        public void JobClassChange(int jobNo)
        {
            JobData data;
            JobData data2;
            JobData data3;
            List<JobData> list;
            data = this.GetJobData(jobNo);
            data2 = this.GetBaseJob(data.JobID);
            if (data2 == null)
            {
                goto Label_0021;
            }
            if (data != null)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            data.JobRankUp();
            data3 = this.CurrentJob;
            list = new List<JobData>(this.mJobs);
            list.Remove(data);
            list[list.IndexOf(data2)] = data;
            this.mJobs = list.ToArray();
            if (data3 != data2)
            {
                goto Label_0066;
            }
            data3 = data;
        Label_0066:
            this.mJobIndex = Array.IndexOf<JobData>(this.mJobs, data3);
            this.ReserveTemporaryJobs();
            this.UpdateAvailableJobs();
            this.UpdateUnitLearnAbilityAll();
            this.UpdateUnitBattleAbilityAll();
            this.CalcStatus();
            return;
        }

        public void JobRankUp(int jobNo)
        {
            JobData data;
            data = this.GetJobData(jobNo);
            if (data != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            data.JobRankUp();
            this.ReserveTemporaryJobs();
            this.UpdateAvailableJobs();
            this.UpdateUnitLearnAbilityAll();
            this.UpdateUnitBattleAbilityAll();
            this.CalcStatus();
            return;
        }

        public void JobUnlock(int jobNo)
        {
            this.JobRankUp(jobNo);
            return;
        }

        public unsafe bool MeetsTobiraConditions(TobiraParam.Category category)
        {
            List<TobiraConditioError> list;
            return this.MeetsTobiraConditions(category, &list);
        }

        public bool MeetsTobiraConditions(TobiraParam.Category category, out List<TobiraConditioError> errors)
        {
            TobiraConditionParam[] paramArray;
            TobiraConditionParam[] paramArray2;
            int num;
            UnitData data;
            SRPG.TobiraData data2;
            <MeetsTobiraConditions>c__AnonStorey400 storey;
            TobiraConditionParam.ConditionType type;
            *(errors) = new List<TobiraConditioError>();
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(this.UnitID, category);
            if (paramArray != null)
            {
                goto Label_0026;
            }
            return 0;
        Label_0026:
            storey = new <MeetsTobiraConditions>c__AnonStorey400();
            paramArray2 = paramArray;
            num = 0;
            goto Label_01F7;
        Label_0036:
            storey.cond = paramArray2[num];
            type = storey.cond.CondType;
            if (type == 1)
            {
                goto Label_00A0;
            }
            if (type == 2)
            {
                goto Label_0063;
            }
            goto Label_01F3;
        Label_0063:
            if (category != null)
            {
                goto Label_006E;
            }
            goto Label_01F3;
        Label_006E:
            if (MonoSingleton<GameManager>.Instance.Player.IsQuestCleared(storey.cond.CondIname) != null)
            {
                goto Label_01F3;
            }
            *(errors).Add(new TobiraConditioError(1));
            goto Label_01F3;
        Label_00A0:
            data = this;
            if (string.IsNullOrEmpty(storey.cond.CondUnit.UnitIname) != null)
            {
                goto Label_00F6;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(storey.cond.CondUnit.UnitIname);
            if (data != null)
            {
                goto Label_00F6;
            }
            *(errors).Add(new TobiraConditioError(2));
            goto Label_01F3;
        Label_00F6:
            if (storey.cond.CondUnit.Level <= data.Lv)
            {
                goto Label_011F;
            }
            *(errors).Add(new TobiraConditioError(3));
        Label_011F:
            if (storey.cond.CondUnit.AwakeLevel <= data.AwakeLv)
            {
                goto Label_0148;
            }
            *(errors).Add(new TobiraConditioError(4));
        Label_0148:
            if (category == null)
            {
                goto Label_01F3;
            }
            if (string.IsNullOrEmpty(storey.cond.CondUnit.JobIname) != null)
            {
                goto Label_01A3;
            }
            if (storey.cond.CondUnit.JobLevel <= data.GetJobLevelByJobID(storey.cond.CondUnit.JobIname))
            {
                goto Label_01A3;
            }
            *(errors).Add(new TobiraConditioError(5));
        Label_01A3:
            data2 = data.TobiraData.Find(new Predicate<SRPG.TobiraData>(storey.<>m__4BB));
            if (data2 == null)
            {
                goto Label_01E1;
            }
            if (storey.cond.CondUnit.TobiraLv <= data2.Lv)
            {
                goto Label_01F3;
            }
        Label_01E1:
            *(errors).Add(new TobiraConditioError(6));
        Label_01F3:
            num += 1;
        Label_01F7:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0036;
            }
            return (*(errors).Count == 0);
        }

        public bool OpenCharacterQuestOnLevelUp(int beforeLv)
        {
            OInt num;
            if (beforeLv < this.Lv)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            num = SRPG.UnitParam.MASTER_QUEST_LV;
            if (num > 0)
            {
                goto Label_0027;
            }
            return 0;
        Label_0027:
            if (this.IsSetCharacterQuest() != null)
            {
                goto Label_0034;
            }
            return 0;
        Label_0034:
            return ((num <= beforeLv) ? 0 : ((num > this.Lv) == 0));
        }

        public bool OpenCharacterQuestOnQuestResult(QuestParam startParam, int beforeLv)
        {
            CharacterQuestParam param;
            param = this.GetCurrentCharaEpisodeData();
            if (param != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            if (this.IsChQuestParentUnlocked(param.Param) != null)
            {
                goto Label_0022;
            }
            return 0;
        Label_0022:
            if ((startParam.iname != param.Param.iname) == null)
            {
                goto Label_004A;
            }
            if (param.IsAvailable == null)
            {
                goto Label_004A;
            }
            return 1;
        Label_004A:
            if (this.OpenNewCharacterEpisodeOnLevelUp(beforeLv, param) != null)
            {
                goto Label_0059;
            }
            return 0;
        Label_0059:
            return 1;
        }

        public bool OpenCharacterQuestOnRarityUp(int beforeRarity)
        {
            OInt num;
            if (beforeRarity < this.Rarity)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            num = SRPG.UnitParam.MASTER_QUEST_RARITY - 1;
            if (this.IsSetCharacterQuest() != null)
            {
                goto Label_0028;
            }
            return 0;
        Label_0028:
            return ((num <= beforeRarity) ? 0 : ((num > this.Rarity) == 0));
        }

        public bool OpenNewCharacterEpisodeOnLevelUp(int beforeLv, CharacterQuestParam targetQuset)
        {
            CharacterQuestParam param;
            int num;
            bool flag;
            if (beforeLv < this.Lv)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            param = (targetQuset != null) ? targetQuset : this.GetCurrentCharaEpisodeData();
            if (param != null)
            {
                goto Label_0029;
            }
            return 0;
        Label_0029:
            if (this.IsChQuestParentUnlocked(param.Param) != null)
            {
                goto Label_003C;
            }
            return 0;
        Label_003C:
            if (param.Param.EntryConditionCh != null)
            {
                goto Label_004E;
            }
            return 0;
        Label_004E:
            num = param.Param.EntryConditionCh.ulvmin;
            return ((((num > this.Lv) ? 0 : (num > beforeLv)) == null) ? 0 : param.IsAvailable);
        }

        private static unsafe void RefrectionDeriveSkillAndAbility(SkillAbilityDeriveData skillAbilityDeriveData, List<AbilityData> refAbilitys, List<SkillData> refSkills)
        {
            List<AbilityDeriveParam> list;
            List<SkillDeriveParam> list2;
            List<AbilityData> list3;
            List<SkillData> list4;
            List<AbilityDeriveParam>.Enumerator enumerator;
            AbilityData data;
            int num;
            AbilityData data2;
            int num2;
            List<SkillDeriveParam>.Enumerator enumerator2;
            SkillData data3;
            int num3;
            SkillData data4;
            <RefrectionDeriveSkillAndAbility>c__AnonStorey408 storey;
            <RefrectionDeriveSkillAndAbility>c__AnonStorey407 storey2;
            <RefrectionDeriveSkillAndAbility>c__AnonStorey409 storey3;
            <RefrectionDeriveSkillAndAbility>c__AnonStorey40A storeya;
            storey = new <RefrectionDeriveSkillAndAbility>c__AnonStorey408();
            storey.refSkills = refSkills;
            storey.refAbilitys = refAbilitys;
            if (skillAbilityDeriveData != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            if (storey.refAbilitys != null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            if (storey.refSkills != null)
            {
                goto Label_0038;
            }
            return;
        Label_0038:
            list = skillAbilityDeriveData.GetAvailableAbilityDeriveParams();
            list2 = skillAbilityDeriveData.GetAvailableSkillDeriveParams();
            list3 = new List<AbilityData>();
            list4 = new List<SkillData>();
            storey2 = new <RefrectionDeriveSkillAndAbility>c__AnonStorey407();
            storey2.<>f__ref$1032 = storey;
            enumerator = list.GetEnumerator();
        Label_006A:
            try
            {
                goto Label_01CB;
            Label_006F:
                storey2.abilityDeriveParam = &enumerator.Current;
                data = null;
                num = storey.refAbilitys.FindIndex(new Predicate<AbilityData>(storey2.<>m__4C3));
                if (num == -1)
                {
                    goto Label_00B3;
                }
                data = storey.refAbilitys[num];
            Label_00B3:
                if (data == null)
                {
                    goto Label_01CB;
                }
                data2 = data.CreateDeriveAbility(storey2.abilityDeriveParam);
                data.AddDeriveAbility(data2);
                if (data.Skills == null)
                {
                    goto Label_00F8;
                }
                data.Skills.ForEach(new Action<SkillData>(storey2.<>m__4C4));
            Label_00F8:
                if (data2.Skills == null)
                {
                    goto Label_01A6;
                }
                num2 = 0;
                goto Label_0193;
            Label_010C:
                storey3 = new <RefrectionDeriveSkillAndAbility>c__AnonStorey409();
                storey3.skill = data2.Skills[num2];
                if (storey3.skill != null)
                {
                    goto Label_0139;
                }
                goto Label_018D;
            Label_0139:
                if (storey.refSkills.Contains(storey3.skill) == null)
                {
                    goto Label_0156;
                }
                goto Label_018D;
            Label_0156:
                if (storey.refSkills.FindIndex(new Predicate<SkillData>(storey3.<>m__4C5)) < 0)
                {
                    goto Label_017A;
                }
                goto Label_018D;
            Label_017A:
                storey.refSkills.Add(storey3.skill);
            Label_018D:
                num2 += 1;
            Label_0193:
                if (num2 < data2.Skills.Count)
                {
                    goto Label_010C;
                }
            Label_01A6:
                if (list3.Contains(data) != null)
                {
                    goto Label_01BB;
                }
                list3.Add(data);
            Label_01BB:
                storey.refAbilitys.Insert(num, data2);
            Label_01CB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_006F;
                }
                goto Label_01E9;
            }
            finally
            {
            Label_01DC:
                ((List<AbilityDeriveParam>.Enumerator) enumerator).Dispose();
            }
        Label_01E9:
            storeya = new <RefrectionDeriveSkillAndAbility>c__AnonStorey40A();
            enumerator2 = list2.GetEnumerator();
        Label_01F8:
            try
            {
                goto Label_0297;
            Label_01FD:
                storeya.skillDeriveParam = &enumerator2.Current;
                data3 = null;
                num3 = storey.refSkills.FindIndex(new Predicate<SkillData>(storeya.<>m__4C6));
                if (num3 == -1)
                {
                    goto Label_0241;
                }
                data3 = storey.refSkills[num3];
            Label_0241:
                if (data3 == null)
                {
                    goto Label_0297;
                }
                data4 = data3.CreateDeriveSkill(storeya.skillDeriveParam);
                if (data3.OwnerAbiliy == null)
                {
                    goto Label_0272;
                }
                data3.OwnerAbiliy.AddDeriveSkill(data4);
            Label_0272:
                if (list4.Contains(data3) != null)
                {
                    goto Label_0287;
                }
                list4.Add(data3);
            Label_0287:
                storey.refSkills.Insert(num3, data4);
            Label_0297:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_01FD;
                }
                goto Label_02B5;
            }
            finally
            {
            Label_02A8:
                ((List<SkillDeriveParam>.Enumerator) enumerator2).Dispose();
            }
        Label_02B5:
            list3.ForEach(new Action<AbilityData>(storey.<>m__4C7));
            list4.ForEach(new Action<SkillData>(storey.<>m__4C8));
            return;
        }

        public void Release()
        {
            this.mJobs = null;
            this.mStatus = null;
            return;
        }

        public unsafe void ReserveTemporaryJobs()
        {
            int num;
            JobSetParam param;
            bool flag;
            int num2;
            JobData data;
            Json_Job job;
            num = 0;
            goto Label_00B5;
        Label_0007:
            param = this.GetClassChangeJobSet(num);
            if (param != null)
            {
                goto Label_001A;
            }
            goto Label_00B1;
        Label_001A:
            flag = 0;
            num2 = 0;
            goto Label_004B;
        Label_0023:
            if ((this.mJobs[num2].JobID == param.job) == null)
            {
                goto Label_0047;
            }
            flag = 1;
            goto Label_0059;
        Label_0047:
            num2 += 1;
        Label_004B:
            if (num2 < ((int) this.mJobs.Length))
            {
                goto Label_0023;
            }
        Label_0059:
            if (flag == null)
            {
                goto Label_0064;
            }
            goto Label_00B1;
        Label_0064:
            data = new JobData();
            job = new Json_Job();
            job.iname = param.job;
            data.Deserialize(this, job);
            Array.Resize<JobData>(&this.mJobs, ((int) this.mJobs.Length) + 1);
            this.mJobs[((int) this.mJobs.Length) - 1] = data;
        Label_00B1:
            num += 1;
        Label_00B5:
            if (num < ((int) this.mJobs.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void ResetCharacterQuestParams()
        {
            this.mCharacterQuests = null;
            return;
        }

        public void ResetJobSkin(int jobIndex)
        {
            if (jobIndex != -1)
            {
                goto Label_0014;
            }
            jobIndex = this.mJobIndex;
        Label_0014:
            this.mJobs[jobIndex].SelectedSkin = null;
            return;
        }

        public void ResetJobSkinAll()
        {
            int num;
            num = 0;
            goto Label_0019;
        Label_0007:
            this.mJobs[num].SelectedSkin = null;
            num += 1;
        Label_0019:
            if (num < ((int) this.mJobs.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public CharacterQuestUnlockProgress SaveUnlockProgress()
        {
            CharacterQuestParam param;
            CharacterQuestUnlockProgress progress;
            param = this.GetCurrentCharaEpisodeData();
            if (param != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            progress = new CharacterQuestUnlockProgress();
            progress.Level = this.Lv;
            progress.Rarity = this.Rarity;
            progress.CondQuest = param.Param;
            progress.ClearUnlocksCond = this.IsChQuestParentUnlocked(param.Param);
            return progress;
        }

        public unsafe string SearchAbilityReplacementSkill(string ability_id)
        {
            SkillData data;
            List<SkillData>.Enumerator enumerator;
            int num;
            string str;
            enumerator = this.mBattleSkills.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_00BF;
            Label_0011:
                data = &enumerator.Current;
                if (data.SkillParam.effect_type == 0x17)
                {
                    goto Label_0030;
                }
                goto Label_00BF;
            Label_0030:
                if (data.SkillParam.AbilityReplaceTargetIdLists == null)
                {
                    goto Label_00BF;
                }
                if (data.SkillParam.AbilityReplaceChangeIdLists != null)
                {
                    goto Label_0055;
                }
                goto Label_00BF;
            Label_0055:
                num = 0;
                goto Label_00A9;
            Label_005C:
                if ((data.SkillParam.AbilityReplaceTargetIdLists[num] == ability_id) == null)
                {
                    goto Label_00A5;
                }
                if (num >= data.SkillParam.AbilityReplaceChangeIdLists.Count)
                {
                    goto Label_00A5;
                }
                str = data.SkillParam.AbilityReplaceChangeIdLists[num];
                goto Label_00DE;
            Label_00A5:
                num += 1;
            Label_00A9:
                if (num < data.SkillParam.AbilityReplaceTargetIdLists.Count)
                {
                    goto Label_005C;
                }
            Label_00BF:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_00DC;
            }
            finally
            {
            Label_00D0:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_00DC:
            return null;
        Label_00DE:
            return str;
        }

        public unsafe List<AbilityData> SearchDerivedAbilitys(AbilityData baseAbility)
        {
            List<AbilityData> list;
            SkillAbilityDeriveData data;
            List<AbilityDeriveParam> list2;
            List<SkillDeriveParam> list3;
            List<AbilityDeriveParam> list4;
            List<SkillData> list5;
            AbilityDeriveParam param;
            List<AbilityDeriveParam>.Enumerator enumerator;
            AbilityData data2;
            List<SkillData>.Enumerator enumerator2;
            List<SkillDeriveParam>.Enumerator enumerator3;
            SkillData data3;
            SkillData data4;
            <SearchDerivedAbilitys>c__AnonStorey403 storey;
            <SearchDerivedAbilitys>c__AnonStorey404 storey2;
            <SearchDerivedAbilitys>c__AnonStorey405 storey3;
            storey = new <SearchDerivedAbilitys>c__AnonStorey403();
            storey.baseAbility = baseAbility;
            list = new List<AbilityData>();
            if (this.CurrentJob == null)
            {
                goto Label_01CE;
            }
            if (this.mJobSkillAbilityDeriveData == null)
            {
                goto Label_01CE;
            }
            data = null;
            if (this.mJobSkillAbilityDeriveData.TryGetValue(this.CurrentJob.Param.iname, &data) == null)
            {
                goto Label_01CE;
            }
            list2 = data.GetAvailableAbilityDeriveParams();
            list3 = data.GetAvailableSkillDeriveParams();
            list4 = list2.FindAll(new Predicate<AbilityDeriveParam>(storey.<>m__4BF));
            list5 = new List<SkillData>();
            enumerator = list4.GetEnumerator();
        Label_0082:
            try
            {
                goto Label_0126;
            Label_0087:
                param = &enumerator.Current;
                data2 = storey.baseAbility.CreateDeriveAbility(param);
                list.Add(data2);
                if (data2.Skills != null)
                {
                    goto Label_00B9;
                }
                goto Label_0126;
            Label_00B9:
                storey2 = new <SearchDerivedAbilitys>c__AnonStorey404();
                enumerator2 = data2.Skills.GetEnumerator();
            Label_00CE:
                try
                {
                    goto Label_0108;
                Label_00D3:
                    storey2.derivedAbilitySkill = &enumerator2.Current;
                    if (list5.Find(new Predicate<SkillData>(storey2.<>m__4C0)) != null)
                    {
                        goto Label_0108;
                    }
                    list5.Add(storey2.derivedAbilitySkill);
                Label_0108:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_00D3;
                    }
                    goto Label_0126;
                }
                finally
                {
                Label_0119:
                    ((List<SkillData>.Enumerator) enumerator2).Dispose();
                }
            Label_0126:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0087;
                }
                goto Label_0144;
            }
            finally
            {
            Label_0137:
                ((List<AbilityDeriveParam>.Enumerator) enumerator).Dispose();
            }
        Label_0144:
            storey3 = new <SearchDerivedAbilitys>c__AnonStorey405();
            enumerator3 = list3.GetEnumerator();
        Label_0153:
            try
            {
                goto Label_01B0;
            Label_0158:
                storey3.skillDeriveParam = &enumerator3.Current;
                data3 = null;
                data3 = list5.Find(new Predicate<SkillData>(storey3.<>m__4C1));
                if (data3 == null)
                {
                    goto Label_01B0;
                }
                data4 = data3.CreateDeriveSkill(storey3.skillDeriveParam);
                if (data3.OwnerAbiliy == null)
                {
                    goto Label_01B0;
                }
                data3.OwnerAbiliy.AddDeriveSkill(data4);
            Label_01B0:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0158;
                }
                goto Label_01CE;
            }
            finally
            {
            Label_01C1:
                ((List<SkillDeriveParam>.Enumerator) enumerator3).Dispose();
            }
        Label_01CE:
            return list;
        }

        public unsafe List<SkillData> SearchDerivedSkills(SkillData baseSkill)
        {
            List<SkillData> list;
            SkillAbilityDeriveData data;
            List<SkillDeriveParam> list2;
            List<SkillDeriveParam>.Enumerator enumerator;
            SkillData data2;
            <SearchDerivedSkills>c__AnonStorey406 storey;
            list = new List<SkillData>();
            if (this.CurrentJob == null)
            {
                goto Label_00DF;
            }
            if (this.mJobSkillAbilityDeriveData == null)
            {
                goto Label_00DF;
            }
            data = null;
            if (this.mJobSkillAbilityDeriveData.TryGetValue(this.CurrentJob.Param.iname, &data) == null)
            {
                goto Label_00DF;
            }
            list2 = data.GetAvailableSkillDeriveParams();
            storey = new <SearchDerivedSkills>c__AnonStorey406();
            enumerator = list2.GetEnumerator();
        Label_0055:
            try
            {
                goto Label_00C2;
            Label_005A:
                storey.skillDeriveParam = &enumerator.Current;
                if ((baseSkill.SkillParam.iname != storey.skillDeriveParam.BaseSkillIname) == null)
                {
                    goto Label_008E;
                }
                goto Label_00C2;
            Label_008E:
                if (list.Exists(new Predicate<SkillData>(storey.<>m__4C2)) == null)
                {
                    goto Label_00AB;
                }
                goto Label_00C2;
            Label_00AB:
                data2 = baseSkill.CreateDeriveSkill(storey.skillDeriveParam);
                list.Add(data2);
            Label_00C2:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_005A;
                }
                goto Label_00DF;
            }
            finally
            {
            Label_00D3:
                ((List<SkillDeriveParam>.Enumerator) enumerator).Dispose();
            }
        Label_00DF:
            return list;
        }

        public unsafe string SearchReplacementSkill(string skill_id)
        {
            SkillData data;
            List<SkillData>.Enumerator enumerator;
            int num;
            string str;
            enumerator = this.mBattleSkills.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_00BF;
            Label_0011:
                data = &enumerator.Current;
                if (data.SkillParam.effect_type == 0x17)
                {
                    goto Label_0030;
                }
                goto Label_00BF;
            Label_0030:
                if (data.SkillParam.ReplaceTargetIdLists == null)
                {
                    goto Label_00BF;
                }
                if (data.SkillParam.ReplaceChangeIdLists != null)
                {
                    goto Label_0055;
                }
                goto Label_00BF;
            Label_0055:
                num = 0;
                goto Label_00A9;
            Label_005C:
                if ((data.SkillParam.ReplaceTargetIdLists[num] == skill_id) == null)
                {
                    goto Label_00A5;
                }
                if (num >= data.SkillParam.ReplaceChangeIdLists.Count)
                {
                    goto Label_00A5;
                }
                str = data.SkillParam.ReplaceChangeIdLists[num];
                goto Label_00DE;
            Label_00A5:
                num += 1;
            Label_00A9:
                if (num < data.SkillParam.ReplaceTargetIdLists.Count)
                {
                    goto Label_005C;
                }
            Label_00BF:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_00DC;
            }
            finally
            {
            Label_00D0:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_00DC:
            return null;
        Label_00DE:
            return str;
        }

        public string Serialize()
        {
            object[] objArray6;
            object[] objArray5;
            object[] objArray4;
            object[] objArray3;
            string[] textArray4;
            string[] textArray3;
            string[] textArray2;
            string[] textArray1;
            object[] objArray2;
            object[] objArray1;
            string str;
            int num;
            SkillData data;
            string str2;
            int num2;
            int num3;
            int num4;
            JobData data2;
            int num5;
            EquipData data3;
            int num6;
            AbilityData data4;
            bool flag;
            int num7;
            int num8;
            int num9;
            ArtifactData data5;
            long num10;
            int num11;
            int num12;
            int num13;
            string str3;
            str = ((((((string.Empty + "{\"iid\":" + ((long) this.UniqueID)) + ",\"iname\":\"" + this.UnitParam.iname + "\"") + ",\"rare\":" + ((int) this.Rarity)) + ",\"plus\":" + ((int) this.AwakeLv)) + ",\"lv\":" + ((int) this.Lv)) + ",\"exp\":" + ((int) this.Exp)) + ",\"fav\":" + ((this.IsFavorite == null) ? "0" : "1");
            if (this.MasterAbility == null)
            {
                goto Label_0133;
            }
            str3 = str;
            objArray1 = new object[] { str3, ",\"abil\":{\"iid\":", (long) this.MasterAbility.UniqueID, ",\"iname\":\"", this.MasterAbility.Param.iname, "\",\"exp\":", (int) this.MasterAbility.Exp, "}" };
            str = string.Concat(objArray1);
        Label_0133:
            if (this.CollaboAbility == null)
            {
                goto Label_023F;
            }
            str3 = str;
            objArray2 = new object[] { str3, ",\"c_abil\":{\"iid\":", (long) this.CollaboAbility.UniqueID, ",\"iname\":\"", this.CollaboAbility.Param.iname, "\",\"exp\":", (int) this.CollaboAbility.Exp, ",\"skills\":[" };
            str = string.Concat(objArray2);
            num = 0;
            goto Label_021D;
        Label_01B2:
            data = this.CollaboAbility.Skills[num];
            if (data != null)
            {
                goto Label_01CF;
            }
            goto Label_0219;
        Label_01CF:
            str3 = str;
            textArray1 = new string[] { str3, (num == null) ? string.Empty : ",", "{\"iname\":\"", data.SkillParam.iname, "\"}" };
            str = string.Concat(textArray1);
        Label_0219:
            num += 1;
        Label_021D:
            if (num < this.CollaboAbility.Skills.Count)
            {
                goto Label_01B2;
            }
            str = str + "]}";
        Label_023F:
            str2 = string.Empty;
            if (this.mUnlockedAbilitys == null)
            {
                goto Label_02C3;
            }
            num2 = 0;
            goto Label_02B1;
        Label_0258:
            str3 = str2;
            textArray2 = new string[] { str3, (num2 <= 0) ? string.Empty : ",", "\"", this.mUnlockedAbilitys[num2].iname, "\"" };
            str2 = string.Concat(textArray2);
            num2 += 1;
        Label_02B1:
            if (num2 < this.mUnlockedAbilitys.Count)
            {
                goto Label_0258;
            }
        Label_02C3:
            if (this.mUnlockedSkills == null)
            {
                goto Label_0358;
            }
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_02E5;
            }
            str2 = str2 + ",";
        Label_02E5:
            num3 = 0;
            goto Label_0346;
        Label_02ED:
            str3 = str2;
            textArray3 = new string[] { str3, (num3 <= 0) ? string.Empty : ",", "\"", this.mUnlockedSkills[num3].iname, "\"" };
            str2 = string.Concat(textArray3);
            num3 += 1;
        Label_0346:
            if (num3 < this.mUnlockedSkills.Count)
            {
                goto Label_02ED;
            }
        Label_0358:
            if (this.mUnlockedLeaderSkill == null)
            {
                goto Label_03B2;
            }
            str3 = str2;
            textArray4 = new string[] { str3, (string.IsNullOrEmpty(str2) != null) ? string.Empty : ",", "\"", this.mUnlockedLeaderSkill.iname, "\"" };
            str2 = string.Concat(textArray4);
        Label_03B2:
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_03CF;
            }
            str = str + ",\"quest_clear_unlocks\":[" + str2 + "]";
        Label_03CF:
            if ((this.Jobs == null) || (((int) this.Jobs.Length) <= 0))
            {
                goto Label_089E;
            }
            str = str + ",\"jobs\":[";
            num4 = 0;
            goto Label_0883;
        Label_03FC:
            data2 = this.Jobs[num4];
            if (num4 <= 0)
            {
                goto Label_041B;
            }
            str = str + ",";
        Label_041B:
            str3 = str;
            objArray3 = new object[] { str3, "{\"iid\":", (long) data2.UniqueID, ",\"iname\":\"", data2.JobID, "\",\"rank\":", (int) data2.Rank };
            str = string.Concat(objArray3);
            if ((data2.Equips == null) || (((int) data2.Equips.Length) <= 0))
            {
                goto Label_053C;
            }
            str = str + ",\"equips\":[";
            num5 = 0;
            goto Label_0520;
        Label_049E:
            data3 = data2.Equips[num5];
            if (num5 <= 0)
            {
                goto Label_04BE;
            }
            str = str + ",";
        Label_04BE:
            str3 = str;
            objArray4 = new object[] { str3, "{\"iid\":", (long) data3.UniqueID, ",\"iname\":\"", data3.ItemID, "\",\"exp\":", (int) data3.Exp, "}" };
            str = string.Concat(objArray4);
            num5 += 1;
        Label_0520:
            if (num5 < ((int) data2.Equips.Length))
            {
                goto Label_049E;
            }
            str = str + "]";
        Label_053C:
            if ((data2.LearnAbilitys == null) || (data2.LearnAbilitys.Count <= 0))
            {
                goto Label_0618;
            }
            str = str + ",\"abils\":[";
            num6 = 0;
            goto Label_05F9;
        Label_056E:
            data4 = data2.LearnAbilitys[num6];
            if (num6 <= 0)
            {
                goto Label_0592;
            }
            str = str + ",";
        Label_0592:
            str3 = str;
            objArray5 = new object[] { str3, "{\"iid\":", (long) data4.UniqueID, ",\"iname\":\"", data4.Param.iname, "\",\"exp\":", (int) data4.Exp, "}" };
            str = string.Concat(objArray5);
            num6 += 1;
        Label_05F9:
            if (num6 < data2.LearnAbilitys.Count)
            {
                goto Label_056E;
            }
            str = str + "]";
        Label_0618:
            if ((data2.AbilitySlots == null) && (data2.Artifacts == null))
            {
                goto Label_0739;
            }
            str = str + ",\"select\":{";
            flag = 0;
            if (data2.AbilitySlots == null)
            {
                goto Label_06AE;
            }
            str = str + "\"abils\":[";
            num7 = 0;
            goto Label_068F;
        Label_065F:
            if (num7 <= 0)
            {
                goto Label_0673;
            }
            str = str + ",";
        Label_0673:
            str = str + ((long) data2.AbilitySlots[num7]);
            num7 += 1;
        Label_068F:
            if (num7 < ((int) data2.AbilitySlots.Length))
            {
                goto Label_065F;
            }
            str = str + "]";
            flag = 1;
        Label_06AE:
            if (data2.Artifacts == null)
            {
                goto Label_072D;
            }
            if (flag == null)
            {
                goto Label_06CD;
            }
            str = str + ",";
        Label_06CD:
            str = str + "\"artifacts\":[";
            num8 = 0;
            goto Label_0711;
        Label_06E1:
            if (num8 <= 0)
            {
                goto Label_06F5;
            }
            str = str + ",";
        Label_06F5:
            str = str + ((long) data2.Artifacts[num8]);
            num8 += 1;
        Label_0711:
            if (num8 < ((int) data2.Artifacts.Length))
            {
                goto Label_06E1;
            }
            str = str + "]";
        Label_072D:
            str = str + "}";
        Label_0739:
            if (data2.ArtifactDatas == null)
            {
                goto Label_0848;
            }
            str = str + ",\"artis\":[";
            num9 = 0;
            goto Label_082C;
        Label_0759:
            data5 = data2.ArtifactDatas[num9];
            if (num9 <= 0)
            {
                goto Label_0779;
            }
            str = str + ",";
        Label_0779:
            if (data5 != null)
            {
                goto Label_0791;
            }
            str = str + "null";
            goto Label_0826;
        Label_0791:
            str = (((((str + "{\"iid\":" + ((OLong) data5.UniqueID)) + ",\"iname\":\"" + data5.ArtifactParam.iname + "\"") + ",\"exp\":" + ((int) data5.Exp)) + ",\"rare\":" + ((OInt) data5.Rarity)) + ",\"fav\":" + ((int) ((data5.IsFavorite == null) ? 0 : 1))) + "}";
        Label_0826:
            num9 += 1;
        Label_082C:
            if (num9 < ((int) data2.ArtifactDatas.Length))
            {
                goto Label_0759;
            }
            str = str + "]";
        Label_0848:
            if (string.IsNullOrEmpty(data2.SelectedSkin) != null)
            {
                goto Label_0871;
            }
            str = str + ",\"cur_skin\":\"" + data2.SelectedSkin + "\"";
        Label_0871:
            str = str + "}";
            num4 += 1;
        Label_0883:
            if (num4 < ((int) this.Jobs.Length))
            {
                goto Label_03FC;
            }
            str = str + "]";
        Label_089E:
            num10 = (this.CurrentJob == null) ? 0L : this.CurrentJob.UniqueID;
            if (num10 == null)
            {
                goto Label_09B8;
            }
            str = str + ",\"select\":{";
            if (num10 == null)
            {
                goto Label_09AC;
            }
            str = str + "\"job\":" + ((long) num10);
            if ((this.mPartyJobs == null) || (((int) this.mPartyJobs.Length) <= 0))
            {
                goto Label_09AC;
            }
            str = str + ",\"quests\":[";
            num11 = 0;
            num12 = 0;
            goto Label_0991;
        Label_091A:
            if (this.mPartyJobs[num12] == null)
            {
                goto Label_098B;
            }
            if (num11 <= 0)
            {
                goto Label_093E;
            }
            str = str + ((char) 0x2c);
        Label_093E:
            str3 = str;
            objArray6 = new object[] { str3, "{\"qtype\":\"", PartyData.GetStringFromPartyType(num12), "\",\"jiid\":", (long) this.mPartyJobs[num12], "}" };
            str = string.Concat(objArray6);
            num11 += 1;
        Label_098B:
            num12 += 1;
        Label_0991:
            if (num12 < ((int) this.mPartyJobs.Length))
            {
                goto Label_091A;
            }
            str = str + "]";
        Label_09AC:
            str = str + "}";
        Label_09B8:
            if ((this.mTobiraData == null) || (this.mTobiraData.Count <= 0))
            {
                goto Label_0ACE;
            }
            str = str + "," + TobiraUtility.ToJsonString(this.mTobiraData);
            if (this.mTobiraMasterAbilitys.Count <= 0)
            {
                goto Label_0ACE;
            }
            str = str + ",\"door_abils\":[";
            num13 = 0;
            goto Label_0AB0;
        Label_0A10:
            if (num13 <= 0)
            {
                goto Label_0A24;
            }
            str = str + ",";
        Label_0A24:
            str = ((((str + "{") + "\"iid\":" + ((long) this.mTobiraMasterAbilitys[num13].UniqueID)) + ",\"exp\":" + ((int) this.mTobiraMasterAbilitys[num13].Exp)) + ",\"iname\":\"" + this.mTobiraMasterAbilitys[num13].Param.iname + "\"") + "}";
            num13 += 1;
        Label_0AB0:
            if (num13 < this.mTobiraMasterAbilitys.Count)
            {
                goto Label_0A10;
            }
            str = str + "]";
        Label_0ACE:
            if (this.mConceptCard == null)
            {
                goto Label_0BDE;
            }
            str = (((((((((str + ",\"concept_card\":{") + "\"iid\":" + ((OLong) this.mConceptCard.UniqueID)) + ",\"iname\":\"" + this.mConceptCard.Param.iname + "\"") + ",\"exp\":" + ((OInt) this.mConceptCard.Exp)) + ",\"trust\":" + ((OInt) this.mConceptCard.Trust)) + ",\"fav\":" + ((int) ((this.mConceptCard.Favorite == null) ? 0 : 1))) + ",\"is_new\":0") + ",\"trust_bonus\":" + ((int) ((this.mConceptCard.TrustBonus == null) ? 0 : 1))) + ",\"plus\":" + ((OInt) this.mConceptCard.AwakeCount)) + "}";
        Label_0BDE:
            return (str + "}");
        }

        public string Serialize2()
        {
            object[] objArray6;
            object[] objArray5;
            object[] objArray4;
            object[] objArray3;
            string[] textArray4;
            string[] textArray3;
            string[] textArray2;
            string[] textArray1;
            object[] objArray2;
            object[] objArray1;
            string str;
            int num;
            SkillData data;
            string str2;
            int num2;
            int num3;
            long num4;
            int num5;
            JobData data2;
            int num6;
            EquipData data3;
            int num7;
            AbilityData data4;
            bool flag;
            int num8;
            int num9;
            int num10;
            ArtifactData data5;
            int num11;
            ArtifactData data6;
            int num12;
            int num13;
            int num14;
            string str3;
            str = (((((string.Empty + "{\"iid\":" + ((long) this.UniqueID)) + ",\"iname\":\"" + this.UnitParam.iname + "\"") + ",\"rare\":" + ((int) this.Rarity)) + ",\"plus\":" + ((int) this.AwakeLv)) + ",\"lv\":" + ((int) this.Lv)) + ",\"exp\":" + ((int) this.Exp);
            if (this.MasterAbility == null)
            {
                goto Label_010D;
            }
            str3 = str;
            objArray1 = new object[] { str3, ",\"abil\":{\"iid\":", (long) this.MasterAbility.UniqueID, ",\"iname\":\"", this.MasterAbility.Param.iname, "\",\"exp\":", (int) this.MasterAbility.Exp, "}" };
            str = string.Concat(objArray1);
        Label_010D:
            if (this.CollaboAbility == null)
            {
                goto Label_0219;
            }
            str3 = str;
            objArray2 = new object[] { str3, ",\"c_abil\":{\"iid\":", (long) this.CollaboAbility.UniqueID, ",\"iname\":\"", this.CollaboAbility.Param.iname, "\",\"exp\":", (int) this.CollaboAbility.Exp, ",\"skills\":[" };
            str = string.Concat(objArray2);
            num = 0;
            goto Label_01F7;
        Label_018C:
            data = this.CollaboAbility.Skills[num];
            if (data != null)
            {
                goto Label_01A9;
            }
            goto Label_01F3;
        Label_01A9:
            str3 = str;
            textArray1 = new string[] { str3, (num == null) ? string.Empty : ",", "{\"iname\":\"", data.SkillParam.iname, "\"}" };
            str = string.Concat(textArray1);
        Label_01F3:
            num += 1;
        Label_01F7:
            if (num < this.CollaboAbility.Skills.Count)
            {
                goto Label_018C;
            }
            str = str + "]}";
        Label_0219:
            str2 = string.Empty;
            if (this.mUnlockedAbilitys == null)
            {
                goto Label_029D;
            }
            num2 = 0;
            goto Label_028B;
        Label_0232:
            str3 = str2;
            textArray2 = new string[] { str3, (num2 <= 0) ? string.Empty : ",", "\"", this.mUnlockedAbilitys[num2].iname, "\"" };
            str2 = string.Concat(textArray2);
            num2 += 1;
        Label_028B:
            if (num2 < this.mUnlockedAbilitys.Count)
            {
                goto Label_0232;
            }
        Label_029D:
            if (this.mUnlockedSkills == null)
            {
                goto Label_0332;
            }
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_02BF;
            }
            str2 = str2 + ",";
        Label_02BF:
            num3 = 0;
            goto Label_0320;
        Label_02C7:
            str3 = str2;
            textArray3 = new string[] { str3, (num3 <= 0) ? string.Empty : ",", "\"", this.mUnlockedSkills[num3].iname, "\"" };
            str2 = string.Concat(textArray3);
            num3 += 1;
        Label_0320:
            if (num3 < this.mUnlockedSkills.Count)
            {
                goto Label_02C7;
            }
        Label_0332:
            if (this.mUnlockedLeaderSkill == null)
            {
                goto Label_038C;
            }
            str3 = str2;
            textArray4 = new string[] { str3, (string.IsNullOrEmpty(str2) != null) ? string.Empty : ",", "\"", this.mUnlockedLeaderSkill.iname, "\"" };
            str2 = string.Concat(textArray4);
        Label_038C:
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_03A9;
            }
            str = str + ",\"quest_clear_unlocks\":[" + str2 + "]";
        Label_03A9:
            num4 = (this.CurrentJob == null) ? 0L : this.CurrentJob.UniqueID;
            if ((this.Jobs == null) || (((int) this.Jobs.Length) <= 0))
            {
                goto Label_09A6;
            }
            str = str + ",\"jobs\":[";
            num5 = 0;
            goto Label_098B;
        Label_03F5:
            data2 = this.Jobs[num5];
            if (num5 <= 0)
            {
                goto Label_0414;
            }
            str = str + ",";
        Label_0414:
            str3 = str;
            objArray3 = new object[] { str3, "{\"iid\":", (long) data2.UniqueID, ",\"iname\":\"", data2.JobID, "\",\"rank\":", (int) data2.Rank };
            str = string.Concat(objArray3);
            if ((data2.Equips == null) || (((int) data2.Equips.Length) <= 0))
            {
                goto Label_0535;
            }
            str = str + ",\"equips\":[";
            num6 = 0;
            goto Label_0519;
        Label_0497:
            data3 = data2.Equips[num6];
            if (num6 <= 0)
            {
                goto Label_04B7;
            }
            str = str + ",";
        Label_04B7:
            str3 = str;
            objArray4 = new object[] { str3, "{\"iid\":", (long) data3.UniqueID, ",\"iname\":\"", data3.ItemID, "\",\"exp\":", (int) data3.Exp, "}" };
            str = string.Concat(objArray4);
            num6 += 1;
        Label_0519:
            if (num6 < ((int) data2.Equips.Length))
            {
                goto Label_0497;
            }
            str = str + "]";
        Label_0535:
            if ((data2.LearnAbilitys == null) || (data2.LearnAbilitys.Count <= 0))
            {
                goto Label_0611;
            }
            str = str + ",\"abils\":[";
            num7 = 0;
            goto Label_05F2;
        Label_0567:
            data4 = data2.LearnAbilitys[num7];
            if (num7 <= 0)
            {
                goto Label_058B;
            }
            str = str + ",";
        Label_058B:
            str3 = str;
            objArray5 = new object[] { str3, "{\"iid\":", (long) data4.UniqueID, ",\"iname\":\"", data4.Param.iname, "\",\"exp\":", (int) data4.Exp, "}" };
            str = string.Concat(objArray5);
            num7 += 1;
        Label_05F2:
            if (num7 < data2.LearnAbilitys.Count)
            {
                goto Label_0567;
            }
            str = str + "]";
        Label_0611:
            if (((data2.AbilitySlots == null) && (data2.Artifacts == null)) || (num4 != data2.UniqueID))
            {
                goto Label_0740;
            }
            str = str + ",\"select\":{";
            flag = 0;
            if (data2.AbilitySlots == null)
            {
                goto Label_06B5;
            }
            str = str + "\"abils\":[";
            num8 = 0;
            goto Label_0696;
        Label_0666:
            if (num8 <= 0)
            {
                goto Label_067A;
            }
            str = str + ",";
        Label_067A:
            str = str + ((long) data2.AbilitySlots[num8]);
            num8 += 1;
        Label_0696:
            if (num8 < ((int) data2.AbilitySlots.Length))
            {
                goto Label_0666;
            }
            str = str + "]";
            flag = 1;
        Label_06B5:
            if (data2.Artifacts == null)
            {
                goto Label_0734;
            }
            if (flag == null)
            {
                goto Label_06D4;
            }
            str = str + ",";
        Label_06D4:
            str = str + "\"artifacts\":[";
            num9 = 0;
            goto Label_0718;
        Label_06E8:
            if (num9 <= 0)
            {
                goto Label_06FC;
            }
            str = str + ",";
        Label_06FC:
            str = str + ((long) data2.Artifacts[num9]);
            num9 += 1;
        Label_0718:
            if (num9 < ((int) data2.Artifacts.Length))
            {
                goto Label_06E8;
            }
            str = str + "]";
        Label_0734:
            str = str + "}";
        Label_0740:
            if (data2.ArtifactDatas == null)
            {
                goto Label_0942;
            }
            str = str + ",\"artis\":[";
            if (data2.UniqueID != num4)
            {
                goto Label_0832;
            }
            num10 = 0;
            goto Label_081D;
        Label_076E:
            data5 = data2.ArtifactDatas[num10];
            if (num10 <= 0)
            {
                goto Label_078E;
            }
            str = str + ",";
        Label_078E:
            if (data5 != null)
            {
                goto Label_07A6;
            }
            str = str + "null";
            goto Label_0817;
        Label_07A6:
            str = ((((str + "{\"iid\":" + ((OLong) data5.UniqueID)) + ",\"iname\":\"" + data5.ArtifactParam.iname + "\"") + ",\"exp\":" + ((int) data5.Exp)) + ",\"rare\":" + ((OInt) data5.Rarity)) + "}";
        Label_0817:
            num10 += 1;
        Label_081D:
            if (num10 < ((int) data2.ArtifactDatas.Length))
            {
                goto Label_076E;
            }
            goto Label_0936;
        Label_0832:
            num11 = 0;
            goto Label_0926;
        Label_083A:
            data6 = data2.ArtifactDatas[num11];
            if (num11 <= 0)
            {
                goto Label_085A;
            }
            str = str + ",";
        Label_085A:
            if ((data6 != null) && ((data6 == null) || (data6.ArtifactParam.type == 1)))
            {
                goto Label_088B;
            }
            str = str + "null";
            goto Label_0920;
        Label_088B:
            str = (((((str + "{\"iid\":" + ((OLong) data6.UniqueID)) + ",\"iname\":\"" + data6.ArtifactParam.iname + "\"") + ",\"exp\":" + ((int) data6.Exp)) + ",\"rare\":" + ((OInt) data6.Rarity)) + ",\"fav\":" + ((int) ((data6.IsFavorite == null) ? 0 : 1))) + "}";
        Label_0920:
            num11 += 1;
        Label_0926:
            if (num11 < ((int) data2.ArtifactDatas.Length))
            {
                goto Label_083A;
            }
        Label_0936:
            str = str + "]";
        Label_0942:
            if ((string.IsNullOrEmpty(data2.SelectedSkin) != null) || (num4 != data2.UniqueID))
            {
                goto Label_0979;
            }
            str = str + ",\"cur_skin\":\"" + data2.SelectedSkin + "\"";
        Label_0979:
            str = str + "}";
            num5 += 1;
        Label_098B:
            if (num5 < ((int) this.Jobs.Length))
            {
                goto Label_03F5;
            }
            str = str + "]";
        Label_09A6:
            if (num4 == null)
            {
                goto Label_0AA1;
            }
            str = str + ",\"select\":{";
            if (num4 == null)
            {
                goto Label_0A95;
            }
            str = str + "\"job\":" + ((long) num4);
            if ((this.mPartyJobs == null) || (((int) this.mPartyJobs.Length) <= 0))
            {
                goto Label_0A95;
            }
            str = str + ",\"quests\":[";
            num12 = 0;
            num13 = 0;
            goto Label_0A7A;
        Label_0A03:
            if (this.mPartyJobs[num13] == null)
            {
                goto Label_0A74;
            }
            if (num12 <= 0)
            {
                goto Label_0A27;
            }
            str = str + ((char) 0x2c);
        Label_0A27:
            str3 = str;
            objArray6 = new object[] { str3, "{\"qtype\":\"", PartyData.GetStringFromPartyType(num13), "\",\"jiid\":", (long) this.mPartyJobs[num13], "}" };
            str = string.Concat(objArray6);
            num12 += 1;
        Label_0A74:
            num13 += 1;
        Label_0A7A:
            if (num13 < ((int) this.mPartyJobs.Length))
            {
                goto Label_0A03;
            }
            str = str + "]";
        Label_0A95:
            str = str + "}";
        Label_0AA1:
            if ((this.mTobiraData == null) || (this.mTobiraData.Count <= 0))
            {
                goto Label_0BB7;
            }
            str = str + "," + TobiraUtility.ToJsonString(this.mTobiraData);
            if (this.mTobiraMasterAbilitys.Count <= 0)
            {
                goto Label_0BB7;
            }
            str = str + ",\"door_abils\":[";
            num14 = 0;
            goto Label_0B99;
        Label_0AF9:
            if (num14 <= 0)
            {
                goto Label_0B0D;
            }
            str = str + ",";
        Label_0B0D:
            str = ((((str + "{") + "\"iid\":" + ((long) this.mTobiraMasterAbilitys[num14].UniqueID)) + ",\"exp\":" + ((int) this.mTobiraMasterAbilitys[num14].Exp)) + ",\"iname\":\"" + this.mTobiraMasterAbilitys[num14].Param.iname + "\"") + "}";
            num14 += 1;
        Label_0B99:
            if (num14 < this.mTobiraMasterAbilitys.Count)
            {
                goto Label_0AF9;
            }
            str = str + "]";
        Label_0BB7:
            if (this.mConceptCard == null)
            {
                goto Label_0CC7;
            }
            str = (((((((((str + ",\"concept_card\":{") + "\"iid\":" + ((OLong) this.mConceptCard.UniqueID)) + ",\"iname\":\"" + this.mConceptCard.Param.iname + "\"") + ",\"exp\":" + ((OInt) this.mConceptCard.Exp)) + ",\"trust\":" + ((OInt) this.mConceptCard.Trust)) + ",\"fav\":" + ((int) ((this.mConceptCard.Favorite == null) ? 0 : 1))) + ",\"is_new\":0") + ",\"trust_bonus\":" + ((int) ((this.mConceptCard.TrustBonus == null) ? 0 : 1))) + ",\"plus\":" + ((OInt) this.mConceptCard.AwakeCount)) + "}";
        Label_0CC7:
            return (str + "}");
        }

        private void SetBadgeState(UnitBadgeTypes type, bool flag)
        {
            if (flag == null)
            {
                goto Label_0019;
            }
            this.BadgeState |= type;
            goto Label_0028;
        Label_0019:
            this.BadgeState &= ~type;
        Label_0028:
            return;
        }

        public void SetEquipAbility(int slot, long iid)
        {
            this.SetEquipAbility(this.mJobIndex, slot, iid);
            return;
        }

        public void SetEquipAbility(int jobIndex, int slot, long iid)
        {
            JobData data;
            data = this.GetJobData(jobIndex);
            if (data == null)
            {
                goto Label_001C;
            }
            data.SetAbilitySlot(slot, this.GetAbilityData(iid));
        Label_001C:
            this.UpdateUnitBattleAbilityAll(jobIndex);
            this.CalcStatus();
            return;
        }

        public bool SetEquipArtifactData(int slot, ArtifactData artifact)
        {
            return this.SetEquipArtifactData(this.mJobIndex, slot, artifact, 1);
        }

        public bool SetEquipArtifactData(int job_index, int slot, ArtifactData artifact, bool is_calc)
        {
            JobData data;
            data = this.GetJobData(job_index);
            if (data != null)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            if (data.SetEquipArtifact(slot, artifact) != null)
            {
                goto Label_001F;
            }
            return 0;
        Label_001F:
            this.UpdateArtifact(job_index, is_calc, 0);
            return 1;
        }

        public void SetJob(JobData job)
        {
            int num;
            num = Array.IndexOf<JobData>(this.mJobs, job);
            if (0 > num)
            {
                goto Label_001B;
            }
            this.SetJobIndex(num);
        Label_001B:
            return;
        }

        public void SetJob(PlayerPartyTypes type)
        {
            JobData data;
            data = this.GetJobFor(type);
            if (data == this.CurrentJob)
            {
                goto Label_001B;
            }
            this.SetJob(data);
        Label_001B:
            return;
        }

        public unsafe void SetJobFor(PlayerPartyTypes type, JobData job)
        {
            if (this.mPartyJobs != null)
            {
                goto Label_001D;
            }
            this.mPartyJobs = new long[11];
            goto Label_0039;
        Label_001D:
            if (((int) this.mPartyJobs.Length) == 11)
            {
                goto Label_0039;
            }
            Array.Resize<long>(&this.mPartyJobs, 11);
        Label_0039:
            this.mPartyJobs[type] = job.UniqueID;
            return;
        }

        public void SetJobIndex(int jobNo)
        {
            this.mJobIndex = jobNo;
            this.UpdateUnitLearnAbilityAll();
            this.UpdateUnitBattleAbilityAll();
            this.CalcStatus();
            return;
        }

        public void SetJobSkin(string afName, int jobIndex, bool is_need_check)
        {
            ArtifactParam[] paramArray;
            ArtifactParam param;
            List<ConceptCardEquipEffect> list;
            <SetJobSkin>c__AnonStorey3F3 storeyf;
            storeyf = new <SetJobSkin>c__AnonStorey3F3();
            storeyf.afName = afName;
            if (jobIndex != -1)
            {
                goto Label_0021;
            }
            jobIndex = this.mJobIndex;
        Label_0021:
            if (string.IsNullOrEmpty(storeyf.afName) == null)
            {
                goto Label_0040;
            }
            this.mJobs[jobIndex].SelectedSkin = null;
            return;
        Label_0040:
            param = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), new Predicate<ArtifactParam>(storeyf.<>m__4AE));
            if (param != null)
            {
                goto Label_006F;
            }
            return;
        Label_006F:
            if (this.mConceptCard == null)
            {
                goto Label_00BB;
            }
            if (this.mConceptCard.GetEnableEquipEffects(this, this.mJobs[jobIndex]).FindIndex(new Predicate<ConceptCardEquipEffect>(storeyf.<>m__4AF)) < 0)
            {
                goto Label_00BB;
            }
            this.mJobs[jobIndex].SelectedSkin = storeyf.afName;
            return;
        Label_00BB:
            if (is_need_check == null)
            {
                goto Label_00E1;
            }
            if (param.CheckEnableEquip(this, jobIndex) != null)
            {
                goto Label_00CF;
            }
            return;
        Label_00CF:
            if (this.CheckUsedSkin(param.iname) != null)
            {
                goto Label_00E1;
            }
            return;
        Label_00E1:
            this.mJobs[jobIndex].SelectedSkin = storeyf.afName;
            return;
        }

        public void SetJobSkinAll(string afName)
        {
            ArtifactParam[] paramArray;
            ArtifactParam param;
            int num;
            <SetJobSkinAll>c__AnonStorey3F4 storeyf;
            storeyf = new <SetJobSkinAll>c__AnonStorey3F4();
            storeyf.afName = afName;
            if (string.IsNullOrEmpty(storeyf.afName) == null)
            {
                goto Label_0024;
            }
            this.ResetJobSkinAll();
            return;
        Label_0024:
            param = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), new Predicate<ArtifactParam>(storeyf.<>m__4B0));
            if (param != null)
            {
                goto Label_0059;
            }
            this.ResetJobSkinAll();
            return;
        Label_0059:
            if (this.CheckUsedSkin(param.iname) != null)
            {
                goto Label_006B;
            }
            return;
        Label_006B:
            num = 0;
            goto Label_00A9;
        Label_0072:
            if (param.CheckEnableEquip(this, num) == null)
            {
                goto Label_0097;
            }
            this.mJobs[num].SelectedSkin = storeyf.afName;
            goto Label_00A5;
        Label_0097:
            this.mJobs[num].SelectedSkin = null;
        Label_00A5:
            num += 1;
        Label_00A9:
            if (num < ((int) this.mJobs.Length))
            {
                goto Label_0072;
            }
            return;
        }

        private void SetSkinLockedJob(Json_Job[] jobs)
        {
            string str;
            int num;
            int num2;
            if (jobs != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            str = string.Empty;
            num = 0;
            goto Label_0040;
        Label_0014:
            if (jobs[num] == null)
            {
                goto Label_003C;
            }
            if (string.IsNullOrEmpty(jobs[num].cur_skin) != null)
            {
                goto Label_003C;
            }
            str = jobs[num].cur_skin;
            goto Label_0049;
        Label_003C:
            num += 1;
        Label_0040:
            if (num < ((int) jobs.Length))
            {
                goto Label_0014;
            }
        Label_0049:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0055;
            }
            return;
        Label_0055:
            num2 = 0;
            goto Label_0071;
        Label_005C:
            if (jobs[num2] == null)
            {
                goto Label_006D;
            }
            jobs[num2].cur_skin = str;
        Label_006D:
            num2 += 1;
        Label_0071:
            if (num2 < ((int) jobs.Length))
            {
                goto Label_005C;
            }
            return;
        }

        public void SetTobiraData(List<SRPG.TobiraData> tobiraData)
        {
            this.mTobiraData = tobiraData;
            return;
        }

        public void SetUniqueID(long uniqueID)
        {
            this.mUniqueID = uniqueID;
            return;
        }

        public void Setup(UnitData src)
        {
            string str;
            Json_Unit unit;
            unit = JSONParser.parseJSONObject<Json_Unit>(src.Serialize());
            this.Deserialize(unit);
            return;
        }

        public bool Setup(string unit_iname, int exp, int rare, int awakeLv, string job_iname, int jobrank, EElement elem, int unlockTobiraNum)
        {
            GameManager manager;
            int num;
            int num2;
            JobSetParam param;
            JobData data;
            Json_Job job;
            Exception exception;
            int num3;
            int num4;
            int num5;
            SkillData data2;
            string str;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            this.mUnitParam = manager.GetUnitParam(unit_iname);
            DebugUtility.Assert((this.mUnitParam == null) == 0, "Failed UnitParam iname \"" + unit_iname + "\" not found.");
            this.mRarity = Math.Min(Math.Max(rare, this.mUnitParam.rare), this.mUnitParam.raremax);
            this.mAwakeLv = Math.Min(awakeLv, this.GetAwakeLevelCap());
            this.mUnlockTobiraNum = unlockTobiraNum;
            this.mElement = elem;
            this.mExp = exp;
            this.mLv = this.CalcLevel();
            this.mJobs = null;
            this.mJobIndex = 0;
            if (((this.mUnitParam.jobsets == null) || (((int) this.mUnitParam.jobsets.Length) <= 0)) || (string.IsNullOrEmpty(this.mUnitParam.jobsets[0]) != null))
            {
                goto Label_01E3;
            }
            this.mJobs = new JobData[(int) this.mUnitParam.jobsets.Length];
            num = 0;
            num2 = 0;
            goto Label_01CB;
        Label_011D:
            param = manager.GetJobSetParam(this.mUnitParam.jobsets[num]);
            if (param != null)
            {
                goto Label_013C;
            }
            goto Label_01C7;
        Label_013C:
            if (string.IsNullOrEmpty(param.job) == null)
            {
                goto Label_0151;
            }
            goto Label_01C7;
        Label_0151:
            data = new JobData();
            job = new Json_Job();
            job.iname = param.job;
            job.rank = 1;
            if ((job.iname == job_iname) == null)
            {
                goto Label_019C;
            }
            job.rank = jobrank;
            this.mJobIndex = num2;
        Label_019C:
            try
            {
                data.Deserialize(this, job);
                this.mJobs[num2++] = data;
                goto Label_01C7;
            }
            catch (Exception exception1)
            {
            Label_01B9:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_01C7;
            }
        Label_01C7:
            num += 1;
        Label_01CB:
            if (num < ((int) this.mUnitParam.jobsets.Length))
            {
                goto Label_011D;
            }
            goto Label_0222;
        Label_01E3:
            this.mNormalAttackSkill = new SkillData();
            this.mNormalAttackSkill.Setup((this.UnitParam.no_job_status == null) ? null : this.UnitParam.no_job_status.default_skill, 1, 1, null);
        Label_0222:
            this.UpdateAvailableJobs();
            if (this.mJobs == null)
            {
                goto Label_025E;
            }
            num3 = 0;
            goto Label_024F;
        Label_023B:
            this.mJobs[num3].UnlockSkillAll();
            num3 += 1;
        Label_024F:
            if (num3 < ((int) this.mJobs.Length))
            {
                goto Label_023B;
            }
        Label_025E:
            this.UpdateUnitLearnAbilityAll();
            this.UpdateUnitBattleAbilityAll();
            num4 = 0;
            goto Label_0327;
        Label_0272:
            this.BattleAbilitys[num4].Setup(this, (long) (num4 + 1), this.BattleAbilitys[num4].AbilityID, this.BattleAbilitys[num4].Exp, 0);
            num5 = 0;
            goto Label_0303;
        Label_02B7:
            data2 = this.BattleAbilitys[num4].Skills[num5];
            if (data2 == null)
            {
                goto Label_02FD;
            }
            if (this.mBattleSkills.Contains(data2) == null)
            {
                goto Label_02F0;
            }
            goto Label_02FD;
        Label_02F0:
            this.mBattleSkills.Add(data2);
        Label_02FD:
            num5 += 1;
        Label_0303:
            if (num5 < this.BattleAbilitys[num4].Skills.Count)
            {
                goto Label_02B7;
            }
            num4 += 1;
        Label_0327:
            if (num4 < this.BattleAbilitys.Count)
            {
                goto Label_0272;
            }
            this.mLeaderSkill = null;
            str = this.GetLeaderSkillIname(this.mRarity);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_037A;
            }
            this.mLeaderSkill = new SkillData();
            this.mLeaderSkill.Setup(str, 1, 1, null);
        Label_037A:
            this.CalcStatus();
            return 1;
        }

        public void SetVirtualAwakeLv(int target_awake_lv)
        {
            this.mAwakeLv = target_awake_lv;
            return;
        }

        public void ShowTooltip(GameObject targetGO, bool allowJobChange, UnitJobDropdown.ParentObjectEvent updateValue)
        {
            string str;
            PlayerPartyTypes types;
            GameObject obj2;
            GameObject obj3;
            UnitData data;
            UnitJobDropdown dropdown;
            bool flag;
            Selectable selectable;
            Image image;
            ArtifactSlots slots;
            AbilitySlots slots2;
            bool flag2;
            ConceptCardSlots slots3;
            bool flag3;
            types = DataSource.FindDataOfClass<PlayerPartyTypes>(targetGO, 11);
            obj3 = Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/UnitTooltip_1"));
            data = new UnitData();
            data.Setup(this);
            data.TempFlags = this.TempFlags;
            DataSource.Bind<UnitData>(obj3, data);
            DataSource.Bind<PlayerPartyTypes>(obj3, types);
            dropdown = obj3.GetComponentInChildren<UnitJobDropdown>();
            if ((dropdown != null) == null)
            {
                goto Label_00FC;
            }
            flag = (((data.TempFlags & 2) == null) || (allowJobChange == null)) ? 0 : ((types == 11) == 0);
            dropdown.get_gameObject().SetActive(1);
            dropdown.UpdateValue = updateValue;
            selectable = dropdown.get_gameObject().GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_00B5;
            }
            selectable.set_interactable(flag);
        Label_00B5:
            image = dropdown.get_gameObject().GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_00FC;
            }
            image.set_color((flag == null) ? new Color(0.5f, 0.5f, 0.5f) : Color.get_white());
        Label_00FC:
            slots = obj3.GetComponentInChildren<ArtifactSlots>();
            slots2 = obj3.GetComponentInChildren<AbilitySlots>();
            if (((slots != null) == null) || ((slots2 != null) == null))
            {
                goto Label_0159;
            }
            flag2 = (((data.TempFlags & 2) == null) || (allowJobChange == null)) ? 0 : ((types == 11) == 0);
            slots.Refresh(flag2);
            slots2.Refresh(flag2);
        Label_0159:
            slots3 = obj3.GetComponentInChildren<ConceptCardSlots>();
            if ((slots3 != null) == null)
            {
                goto Label_0198;
            }
            flag3 = (((data.TempFlags & 2) == null) || (allowJobChange == null)) ? 0 : ((types == 11) == 0);
            slots3.Refresh(flag3);
        Label_0198:
            GameParameter.UpdateAll(obj3);
            return;
        }

        public override string ToString()
        {
            return (this.UnitParam.name + "(" + base.GetType().Name + ")");
        }

        public bool UnitAwaking()
        {
            int num;
            OInt num2;
            num = this.GetAwakeLevelCap();
            this.mAwakeLv = Math.Min(this.mAwakeLv = OInt.op_Increment(this.mAwakeLv), num);
            return 1;
        }

        public bool UnitRarityUp()
        {
            string str;
            OInt num;
            this.mRarity = Math.Min(this.mRarity = OInt.op_Increment(this.mRarity), this.GetRarityCap());
            str = this.GetLeaderSkillIname(this.mRarity);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0071;
            }
            if (this.mLeaderSkill != null)
            {
                goto Label_0062;
            }
            this.mLeaderSkill = new SkillData();
        Label_0062:
            this.mLeaderSkill.Setup(str, 1, 1, null);
        Label_0071:
            this.CalcStatus();
            return 1;
        }

        public string UnlockedCollaboSkillIds()
        {
            string str;
            int num;
            str = string.Empty;
            if (this.mCollaboAbility == null)
            {
                goto Label_006B;
            }
            num = 0;
            goto Label_0055;
        Label_0018:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_002F;
            }
            str = str + ",";
        Label_002F:
            str = str + this.mCollaboAbility.Skills[num].SkillParam.iname;
            num += 1;
        Label_0055:
            if (num < this.mCollaboAbility.Skills.Count)
            {
                goto Label_0018;
            }
        Label_006B:
            return str;
        }

        public QuestClearUnlockUnitDataParam[] UnlockedSkillDiff(string oldIds)
        {
            char[] chArray2;
            char[] chArray1;
            string str;
            string str2;
            string str3;
            string[] strArray;
            MasterParam param;
            List<QuestClearUnlockUnitDataParam> list;
            QuestClearUnlockUnitDataParam param2;
            <UnlockedSkillDiff>c__AnonStorey3F9 storeyf;
            <UnlockedSkillDiff>c__AnonStorey3FA storeyfa;
            storeyf = new <UnlockedSkillDiff>c__AnonStorey3F9();
            str = this.UnlockedSkillIds();
            str2 = (string.IsNullOrEmpty(oldIds) == null) ? oldIds : string.Empty;
            str3 = (string.IsNullOrEmpty(str) == null) ? str : string.Empty;
            if (str2.Length < str3.Length)
            {
                goto Label_0054;
            }
            return new QuestClearUnlockUnitDataParam[0];
        Label_0054:
            chArray1 = new char[] { 0x2c };
            strArray = str2.Split(chArray1);
            chArray2 = new char[] { 0x2c };
            storeyf.newUnlocks = str3.Split(chArray2);
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            list = new List<QuestClearUnlockUnitDataParam>();
            storeyfa = new <UnlockedSkillDiff>c__AnonStorey3FA();
            storeyfa.<>f__ref$1017 = storeyf;
            storeyfa.i = 0;
            goto Label_00FF;
        Label_00AE:
            if (Array.FindIndex<string>(strArray, new Predicate<string>(storeyfa.<>m__4B4)) != -1)
            {
                goto Label_00EF;
            }
            param2 = param.GetUnlockUnitData(storeyf.newUnlocks[storeyfa.i]);
            if (param2 == null)
            {
                goto Label_00EF;
            }
            list.Add(param2);
        Label_00EF:
            storeyfa.i += 1;
        Label_00FF:
            if (storeyfa.i < ((int) storeyf.newUnlocks.Length))
            {
                goto Label_00AE;
            }
            return list.ToArray();
        }

        public string UnlockedSkillIds()
        {
            string str;
            int num;
            int num2;
            str = string.Empty;
            if (this.mUnlockedLeaderSkill == null)
            {
                goto Label_0023;
            }
            str = str + this.mUnlockedLeaderSkill.iname;
        Label_0023:
            if (this.mUnlockedAbilitys == null)
            {
                goto Label_008D;
            }
            num = 0;
            goto Label_007C;
        Label_0035:
            str = str + ((string.IsNullOrEmpty(str) != null) ? this.mUnlockedAbilitys[num].iname : ("," + this.mUnlockedAbilitys[num].iname));
            num += 1;
        Label_007C:
            if (num < this.mUnlockedAbilitys.Count)
            {
                goto Label_0035;
            }
        Label_008D:
            if (this.mUnlockedSkills == null)
            {
                goto Label_00F7;
            }
            num2 = 0;
            goto Label_00E6;
        Label_009F:
            str = str + ((string.IsNullOrEmpty(str) != null) ? this.mUnlockedSkills[num2].iname : ("," + this.mUnlockedSkills[num2].iname));
            num2 += 1;
        Label_00E6:
            if (num2 < this.mUnlockedSkills.Count)
            {
                goto Label_009F;
            }
        Label_00F7:
            return str;
        }

        public void UpdateAbilityRankUp()
        {
            this.UpdateUnitLearnAbilityAll();
            this.UpdateUnitBattleAbilityAll();
            this.CalcStatus();
            return;
        }

        public unsafe void UpdateArtifact(int job_index, bool is_calc, bool refreshSkillAbilityDeriveData)
        {
            if (refreshSkillAbilityDeriveData == null)
            {
                goto Label_0021;
            }
            this.mJobSkillAbilityDeriveData = MonoSingleton<GameManager>.Instance.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(this.mJobs);
        Label_0021:
            this.UpdateUnitLearnAbilityAll(job_index);
            this.UpdateUnitBattleAbilityAll(job_index);
            if (is_calc == null)
            {
                goto Label_004E;
            }
            this.CalcStatus(this.mLv, job_index, &this.mStatus, -1);
        Label_004E:
            return;
        }

        private void UpdateAvailableJobs()
        {
            int num;
            string str;
            JobSetParam param;
            bool flag;
            int num2;
            this.mNumJobsAvailable = 0;
            if (this.mJobs != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            num = 0;
            goto Label_00B7;
        Label_001A:
            if (this.mJobs[num] != null)
            {
                goto Label_002C;
            }
            goto Label_00B3;
        Label_002C:
            str = this.mJobs[num].JobID;
            param = this.FindClassChangeBase2(str);
            if (param != null)
            {
                goto Label_005B;
            }
            this.mNumJobsAvailable += 1;
            goto Label_00B3;
        Label_005B:
            flag = 0;
            num2 = 0;
            goto Label_0090;
        Label_0065:
            if ((this.mJobs[num2].JobID == param.job) == null)
            {
                goto Label_008A;
            }
            flag = 1;
            goto Label_009F;
        Label_008A:
            num2 += 1;
        Label_0090:
            if (num2 < ((int) this.mJobs.Length))
            {
                goto Label_0065;
            }
        Label_009F:
            if (flag != null)
            {
                goto Label_00B3;
            }
            this.mNumJobsAvailable += 1;
        Label_00B3:
            num += 1;
        Label_00B7:
            if (num < ((int) this.mJobs.Length))
            {
                goto Label_001A;
            }
            return;
        }

        public void UpdateBadge()
        {
            this.SetBadgeState(2, this.CheckUnitAwaking());
            return;
        }

        private void UpdateSyncTime()
        {
            this.LastSyncTime = Time.get_realtimeSinceStartup();
            return;
        }

        private void UpdateUnitBattleAbilityAll()
        {
            this.UpdateUnitBattleAbilityAll(this.mJobIndex);
            return;
        }

        private unsafe void UpdateUnitBattleAbilityAll(int jobIndex)
        {
            JobData data;
            int num;
            long num2;
            AbilityData data2;
            int num3;
            SkillData data3;
            int num4;
            long num5;
            ArtifactData data4;
            int num6;
            AbilityData data5;
            int num7;
            SkillData data6;
            ArtifactData data7;
            int num8;
            AbilityData data8;
            int num9;
            SkillData data9;
            List<ConceptCardEquipEffect> list;
            int num10;
            int num11;
            int num12;
            SkillData data10;
            SkillData data11;
            List<SkillData>.Enumerator enumerator;
            SkillData data12;
            List<SkillData>.Enumerator enumerator2;
            int num13;
            AbilityData data13;
            SkillData data14;
            List<SkillData>.Enumerator enumerator3;
            SkillData data15;
            List<SkillData>.Enumerator enumerator4;
            string str;
            string str2;
            <UpdateUnitBattleAbilityAll>c__AnonStorey401 storey;
            <UpdateUnitBattleAbilityAll>c__AnonStorey402 storey2;
            if (<>f__am$cache2A != null)
            {
                goto Label_001E;
            }
            <>f__am$cache2A = new Action<AbilityData>(UnitData.<UpdateUnitBattleAbilityAll>m__4BC);
        Label_001E:
            this.mBattleAbilitys.ForEach(<>f__am$cache2A);
            this.mBattleAbilitys.Clear();
            this.mBattleSkills.Clear();
            data = this.GetJobData(jobIndex);
            if (data == null)
            {
                goto Label_0361;
            }
            num = 0;
            goto Label_00FF;
        Label_0053:
            num2 = data.AbilitySlots[num];
            data2 = this.GetAbilityData(num2);
            if (data2 != null)
            {
                goto Label_006F;
            }
            goto Label_00FB;
        Label_006F:
            if (this.mBattleAbilitys.Contains(data2) == null)
            {
                goto Label_0085;
            }
            goto Label_00FB;
        Label_0085:
            this.mBattleAbilitys.Add(data2);
            if (data2.Skills == null)
            {
                goto Label_00FB;
            }
            num3 = 0;
            goto Label_00E9;
        Label_00A4:
            data3 = data2.Skills[num3];
            if (data3 != null)
            {
                goto Label_00BF;
            }
            goto Label_00E3;
        Label_00BF:
            if (this.mBattleSkills.Contains(data3) == null)
            {
                goto Label_00D6;
            }
            goto Label_00E3;
        Label_00D6:
            this.mBattleSkills.Add(data3);
        Label_00E3:
            num3 += 1;
        Label_00E9:
            if (num3 < data2.Skills.Count)
            {
                goto Label_00A4;
            }
        Label_00FB:
            num += 1;
        Label_00FF:
            if (num < ((int) data.AbilitySlots.Length))
            {
                goto Label_0053;
            }
            if (data.Artifacts == null)
            {
                goto Label_0255;
            }
            num4 = 0;
            goto Label_0246;
        Label_0120:
            if (data.Artifacts[num4] != null)
            {
                goto Label_0137;
            }
            goto Label_0240;
        Label_0137:
            data4 = data.ArtifactDatas[num4];
            if (data4 != null)
            {
                goto Label_014E;
            }
            goto Label_0240;
        Label_014E:
            if (data4.LearningAbilities != null)
            {
                goto Label_015F;
            }
            goto Label_0240;
        Label_015F:
            num6 = 0;
            goto Label_022D;
        Label_0167:
            data5 = data4.LearningAbilities[num6];
            if (data5 != null)
            {
                goto Label_0183;
            }
            goto Label_0227;
        Label_0183:
            if (data5.CheckEnableUseAbility(this, jobIndex) != null)
            {
                goto Label_0196;
            }
            goto Label_0227;
        Label_0196:
            if (this.mBattleAbilitys.Contains(data5) == null)
            {
                goto Label_01AD;
            }
            goto Label_0227;
        Label_01AD:
            this.mBattleAbilitys.Add(data5);
            if (data5.Skills == null)
            {
                goto Label_0227;
            }
            num7 = 0;
            goto Label_0214;
        Label_01CE:
            data6 = data5.Skills[num7];
            if (data6 != null)
            {
                goto Label_01EA;
            }
            goto Label_020E;
        Label_01EA:
            if (this.mBattleSkills.Contains(data6) == null)
            {
                goto Label_0201;
            }
            goto Label_020E;
        Label_0201:
            this.mBattleSkills.Add(data6);
        Label_020E:
            num7 += 1;
        Label_0214:
            if (num7 < data5.Skills.Count)
            {
                goto Label_01CE;
            }
        Label_0227:
            num6 += 1;
        Label_022D:
            if (num6 < data4.LearningAbilities.Count)
            {
                goto Label_0167;
            }
        Label_0240:
            num4 += 1;
        Label_0246:
            if (num4 < ((int) data.Artifacts.Length))
            {
                goto Label_0120;
            }
        Label_0255:
            if (string.IsNullOrEmpty(data.SelectedSkin) != null)
            {
                goto Label_0361;
            }
            data7 = data.GetSelectedSkinData();
            if (data7 == null)
            {
                goto Label_0361;
            }
            if (data7.LearningAbilities == null)
            {
                goto Label_0361;
            }
            num8 = 0;
            goto Label_034E;
        Label_0288:
            data8 = data7.LearningAbilities[num8];
            if (data8 != null)
            {
                goto Label_02A4;
            }
            goto Label_0348;
        Label_02A4:
            if (data8.CheckEnableUseAbility(this, jobIndex) != null)
            {
                goto Label_02B7;
            }
            goto Label_0348;
        Label_02B7:
            if (this.mBattleAbilitys.Contains(data8) == null)
            {
                goto Label_02CE;
            }
            goto Label_0348;
        Label_02CE:
            this.mBattleAbilitys.Add(data8);
            if (data8.Skills == null)
            {
                goto Label_0348;
            }
            num9 = 0;
            goto Label_0335;
        Label_02EF:
            data9 = data8.Skills[num9];
            if (data9 != null)
            {
                goto Label_030B;
            }
            goto Label_032F;
        Label_030B:
            if (this.mBattleSkills.Contains(data9) == null)
            {
                goto Label_0322;
            }
            goto Label_032F;
        Label_0322:
            this.mBattleSkills.Add(data9);
        Label_032F:
            num9 += 1;
        Label_0335:
            if (num9 < data8.Skills.Count)
            {
                goto Label_02EF;
            }
        Label_0348:
            num8 += 1;
        Label_034E:
            if (num8 < data7.LearningAbilities.Count)
            {
                goto Label_0288;
            }
        Label_0361:
            if (this.mConceptCard == null)
            {
                goto Label_04CF;
            }
            list = this.mConceptCard.GetEnableEquipEffects(this, data);
            num10 = 0;
            goto Label_04C1;
        Label_0383:
            storey = new <UpdateUnitBattleAbilityAll>c__AnonStorey401();
            storey.ability = list[num10].Ability;
            if (storey.ability != null)
            {
                goto Label_03B0;
            }
            goto Label_04BB;
        Label_03B0:
            if (this.mBattleAbilitys.Contains(storey.ability) == null)
            {
                goto Label_03CC;
            }
            goto Label_04BB;
        Label_03CC:
            if (this.mBattleAbilitys.FindIndex(new Predicate<AbilityData>(storey.<>m__4BD)) < 0)
            {
                goto Label_03EF;
            }
            goto Label_04BB;
        Label_03EF:
            this.mBattleAbilitys.Add(storey.ability);
            if (storey.ability.Skills == null)
            {
                goto Label_04BB;
            }
            num11 = 0;
            goto Label_04A3;
        Label_041A:
            storey2 = new <UpdateUnitBattleAbilityAll>c__AnonStorey402();
            storey2.skill = storey.ability.Skills[num11];
            if (storey2.skill != null)
            {
                goto Label_044C;
            }
            goto Label_049D;
        Label_044C:
            if (this.mBattleSkills.Contains(storey2.skill) == null)
            {
                goto Label_0468;
            }
            goto Label_049D;
        Label_0468:
            if (this.mBattleSkills.FindIndex(new Predicate<SkillData>(storey2.<>m__4BE)) < 0)
            {
                goto Label_048B;
            }
            goto Label_049D;
        Label_048B:
            this.mBattleSkills.Add(storey2.skill);
        Label_049D:
            num11 += 1;
        Label_04A3:
            if (num11 < storey.ability.Skills.Count)
            {
                goto Label_041A;
            }
        Label_04BB:
            num10 += 1;
        Label_04C1:
            if (num10 < list.Count)
            {
                goto Label_0383;
            }
        Label_04CF:
            if (this.mMasterAbility == null)
            {
                goto Label_057A;
            }
            if (this.mBattleAbilitys.Contains(this.mMasterAbility) != null)
            {
                goto Label_057A;
            }
            this.mBattleAbilitys.Add(this.mMasterAbility);
            if (this.mMasterAbility.Skills == null)
            {
                goto Label_057A;
            }
            num12 = 0;
            goto Label_0563;
        Label_0519:
            data10 = this.mMasterAbility.Skills[num12];
            if (data10 != null)
            {
                goto Label_0539;
            }
            goto Label_055D;
        Label_0539:
            if (this.mBattleSkills.Contains(data10) == null)
            {
                goto Label_0550;
            }
            goto Label_055D;
        Label_0550:
            this.mBattleSkills.Add(data10);
        Label_055D:
            num12 += 1;
        Label_0563:
            if (num12 < this.mMasterAbility.Skills.Count)
            {
                goto Label_0519;
            }
        Label_057A:
            if (this.mCollaboAbility == null)
            {
                goto Label_062A;
            }
            if (this.mBattleAbilitys.Contains(this.mCollaboAbility) != null)
            {
                goto Label_062A;
            }
            this.mBattleAbilitys.Add(this.mCollaboAbility);
            if (this.mCollaboAbility.Skills == null)
            {
                goto Label_062A;
            }
            enumerator = this.mCollaboAbility.Skills.GetEnumerator();
        Label_05CE:
            try
            {
                goto Label_060C;
            Label_05D3:
                data11 = &enumerator.Current;
                if (data11 != null)
                {
                    goto Label_05E8;
                }
                goto Label_060C;
            Label_05E8:
                if (this.mBattleSkills.Contains(data11) == null)
                {
                    goto Label_05FF;
                }
                goto Label_060C;
            Label_05FF:
                this.mBattleSkills.Add(data11);
            Label_060C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_05D3;
                }
                goto Label_062A;
            }
            finally
            {
            Label_061D:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_062A:
            if (this.mMapEffectAbility == null)
            {
                goto Label_06DA;
            }
            if (this.mBattleAbilitys.Contains(this.mMapEffectAbility) != null)
            {
                goto Label_06DA;
            }
            this.mBattleAbilitys.Add(this.mMapEffectAbility);
            if (this.mMapEffectAbility.Skills == null)
            {
                goto Label_06DA;
            }
            enumerator2 = this.mMapEffectAbility.Skills.GetEnumerator();
        Label_067E:
            try
            {
                goto Label_06BC;
            Label_0683:
                data12 = &enumerator2.Current;
                if (data12 != null)
                {
                    goto Label_0698;
                }
                goto Label_06BC;
            Label_0698:
                if (this.mBattleSkills.Contains(data12) == null)
                {
                    goto Label_06AF;
                }
                goto Label_06BC;
            Label_06AF:
                this.mBattleSkills.Add(data12);
            Label_06BC:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0683;
                }
                goto Label_06DA;
            }
            finally
            {
            Label_06CD:
                ((List<SkillData>.Enumerator) enumerator2).Dispose();
            }
        Label_06DA:
            if (this.mTobiraMasterAbilitys == null)
            {
                goto Label_07B5;
            }
            num13 = 0;
            goto Label_07A3;
        Label_06ED:
            data13 = this.mTobiraMasterAbilitys[num13];
            if (data13 == null)
            {
                goto Label_079D;
            }
            if (this.mBattleAbilitys.Contains(data13) == null)
            {
                goto Label_071A;
            }
            goto Label_079D;
        Label_071A:
            this.mBattleAbilitys.Add(data13);
            if (data13.Skills == null)
            {
                goto Label_079D;
            }
            enumerator3 = data13.Skills.GetEnumerator();
        Label_0741:
            try
            {
                goto Label_077F;
            Label_0746:
                data14 = &enumerator3.Current;
                if (data14 != null)
                {
                    goto Label_075B;
                }
                goto Label_077F;
            Label_075B:
                if (this.mBattleSkills.Contains(data14) == null)
                {
                    goto Label_0772;
                }
                goto Label_077F;
            Label_0772:
                this.mBattleSkills.Add(data14);
            Label_077F:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0746;
                }
                goto Label_079D;
            }
            finally
            {
            Label_0790:
                ((List<SkillData>.Enumerator) enumerator3).Dispose();
            }
        Label_079D:
            num13 += 1;
        Label_07A3:
            if (num13 < this.mTobiraMasterAbilitys.Count)
            {
                goto Label_06ED;
            }
        Label_07B5:
            enumerator4 = this.mBattleSkills.GetEnumerator();
        Label_07C2:
            try
            {
                goto Label_084B;
            Label_07C7:
                data15 = &enumerator4.Current;
                if (string.IsNullOrEmpty(data15.ReplaceSkillId) != null)
                {
                    goto Label_0806;
                }
                data15.Setup(data15.ReplaceSkillId, data15.Rank, data15.GetRankCap(), null);
                data15.ReplaceSkillId = null;
            Label_0806:
                str = data15.SkillParam.iname;
                str2 = this.SearchReplacementSkill(str);
                if (str2 != null)
                {
                    goto Label_082A;
                }
                goto Label_084B;
            Label_082A:
                data15.Setup(str2, data15.Rank, data15.GetRankCap(), null);
                data15.ReplaceSkillId = str;
            Label_084B:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_07C7;
                }
                goto Label_0869;
            }
            finally
            {
            Label_085C:
                ((List<SkillData>.Enumerator) enumerator4).Dispose();
            }
        Label_0869:
            if (data == null)
            {
                goto Label_08B2;
            }
            if (this.mJobSkillAbilityDeriveData == null)
            {
                goto Label_08B2;
            }
            if (this.mJobSkillAbilityDeriveData.TryGetValue(data.Param.iname, &this.mSkillAbilityDeriveData) == null)
            {
                goto Label_08B2;
            }
            RefrectionDeriveSkillAndAbility(this.mSkillAbilityDeriveData, this.mBattleAbilitys, this.mBattleSkills);
        Label_08B2:
            return;
        }

        private void UpdateUnitLearnAbilityAll()
        {
            this.UpdateUnitLearnAbilityAll(this.mJobIndex);
            return;
        }

        private void UpdateUnitLearnAbilityAll(int jobIndex)
        {
            JobData data;
            int num;
            AbilityData data2;
            int num2;
            this.mLearnAbilitys.Clear();
            data = this.GetJobData(jobIndex);
            if (data == null)
            {
                goto Label_0059;
            }
            num = 0;
            goto Label_0048;
        Label_0020:
            data2 = data.LearnAbilitys[num];
            if (data2 != null)
            {
                goto Label_0038;
            }
            goto Label_0044;
        Label_0038:
            this.mLearnAbilitys.Add(data2);
        Label_0044:
            num += 1;
        Label_0048:
            if (num < data.LearnAbilitys.Count)
            {
                goto Label_0020;
            }
        Label_0059:
            if (this.mMasterAbility == null)
            {
                goto Label_0075;
            }
            this.mLearnAbilitys.Add(this.mMasterAbility);
        Label_0075:
            if (this.mMapEffectAbility == null)
            {
                goto Label_0091;
            }
            this.mLearnAbilitys.Add(this.mMapEffectAbility);
        Label_0091:
            if (this.mTobiraMasterAbilitys == null)
            {
                goto Label_00E5;
            }
            num2 = 0;
            goto Label_00D4;
        Label_00A3:
            if (this.mTobiraMasterAbilitys[num2] != null)
            {
                goto Label_00B9;
            }
            goto Label_00D0;
        Label_00B9:
            this.mLearnAbilitys.Add(this.mTobiraMasterAbilitys[num2]);
        Label_00D0:
            num2 += 1;
        Label_00D4:
            if (num2 < this.mTobiraMasterAbilitys.Count)
            {
                goto Label_00A3;
            }
        Label_00E5:
            return;
        }

        public List<QuestClearUnlockUnitDataParam> SkillUnlocks
        {
            get
            {
                return this.mSkillUnlocks;
            }
        }

        public long UniqueID
        {
            get
            {
                return this.mUniqueID;
            }
        }

        public SRPG.UnitParam UnitParam
        {
            get
            {
                return this.mUnitParam;
            }
        }

        public string UnitID
        {
            get
            {
                return ((this.UnitParam == null) ? null : this.UnitParam.iname);
            }
        }

        public BaseStatus Status
        {
            get
            {
                return this.mStatus;
            }
        }

        public int Lv
        {
            get
            {
                return this.mLv;
            }
        }

        public int Exp
        {
            get
            {
                return this.mExp;
            }
        }

        public int Rarity
        {
            get
            {
                return this.mRarity;
            }
        }

        public int AwakeLv
        {
            get
            {
                return this.mAwakeLv;
            }
        }

        public EquipData[] CurrentEquips
        {
            get
            {
                return ((this.CurrentJob == null) ? null : this.CurrentJob.Equips);
            }
        }

        public SkillData LeaderSkill
        {
            get
            {
                return this.mLeaderSkill;
            }
        }

        public AbilityData MasterAbility
        {
            get
            {
                return this.mMasterAbility;
            }
        }

        public AbilityData CollaboAbility
        {
            get
            {
                return this.mCollaboAbility;
            }
        }

        public AbilityData MapEffectAbility
        {
            get
            {
                return this.mMapEffectAbility;
            }
        }

        public List<AbilityData> TobiraMasterAbilitys
        {
            get
            {
                return this.mTobiraMasterAbilitys;
            }
        }

        public long[] CurrentAbilitySlots
        {
            get
            {
                return ((this.CurrentJob == null) ? null : this.CurrentJob.AbilitySlots);
            }
        }

        public List<AbilityData> LearnAbilitys
        {
            get
            {
                return this.mLearnAbilitys;
            }
        }

        public List<AbilityData> BattleAbilitys
        {
            get
            {
                return this.mBattleAbilitys;
            }
        }

        public List<SkillData> BattleSkills
        {
            get
            {
                return this.mBattleSkills;
            }
        }

        public EElement Element
        {
            get
            {
                return this.mElement;
            }
        }

        public JobTypes JobType
        {
            get
            {
                return ((this.CurrentJob == null) ? ((this.UnitParam.no_job_status == null) ? 0 : this.UnitParam.no_job_status.jobtype) : this.CurrentJob.JobType);
            }
        }

        public RoleTypes RoleType
        {
            get
            {
                return ((this.CurrentJob == null) ? ((this.UnitParam.no_job_status == null) ? 0 : this.UnitParam.no_job_status.role) : this.CurrentJob.RoleType);
            }
        }

        public ConceptCardData ConceptCard
        {
            get
            {
                return this.mConceptCard;
            }
            set
            {
                this.mConceptCard = value;
                return;
            }
        }

        public int NumJobsAvailable
        {
            get
            {
                return this.mNumJobsAvailable;
            }
        }

        public JobData CurrentJob
        {
            get
            {
                if (this.mJobs == null)
                {
                    goto Label_0034;
                }
                if (this.mJobIndex < 0)
                {
                    goto Label_0034;
                }
                if (((int) this.mJobs.Length) > this.mJobIndex)
                {
                    goto Label_0036;
                }
            Label_0034:
                return null;
            Label_0036:
                return this.mJobs[this.mJobIndex];
            }
        }

        public string CurrentJobId
        {
            get
            {
                return ((this.CurrentJob == null) ? string.Empty : this.CurrentJob.JobID);
            }
        }

        public bool IsUnlockTobira
        {
            get
            {
                SRPG.TobiraData data;
                if (<>f__am$cache29 != null)
                {
                    goto Label_001E;
                }
                <>f__am$cache29 = new Predicate<SRPG.TobiraData>(UnitData.<get_IsUnlockTobira>m__4AA);
            Label_001E:
                data = this.mTobiraData.Find(<>f__am$cache29);
                return ((data == null) ? 0 : data.IsUnlocked);
            }
        }

        public List<SRPG.TobiraData> TobiraData
        {
            get
            {
                return this.mTobiraData;
            }
        }

        public int UnlockTobriaNum
        {
            get
            {
                return this.mUnlockTobiraNum;
            }
        }

        public JobData[] Jobs
        {
            get
            {
                return this.mJobs;
            }
        }

        public int JobCount
        {
            get
            {
                return (int) this.mJobs.Length;
            }
        }

        public int JobIndex
        {
            get
            {
                return this.mJobIndex;
            }
        }

        public string SexPrefix
        {
            get
            {
                return this.UnitParam.SexPrefix;
            }
        }

        public bool IsFavorite
        {
            get
            {
                return this.mFavorite;
            }
            set
            {
                this.mFavorite = value;
                return;
            }
        }

        public EElement SupportElement
        {
            get
            {
                return this.mSupportElement;
            }
        }

        public bool IsIntoUnit
        {
            get
            {
                return ((this.UnitParam == null) ? 0 : this.UnitParam.IsStopped());
            }
        }

        public QuestClearUnlockUnitDataParam[] UnlockedSkills
        {
            get
            {
                return ((this.mUnlockedSkills == null) ? null : this.mUnlockedSkills.ToArray());
            }
        }

        public int CharacterQuestRarity
        {
            get
            {
                return (SRPG.UnitParam.MASTER_QUEST_RARITY - 1);
            }
        }

        public bool IsThrow
        {
            get
            {
                return ((this.UnitParam == null) ? 0 : this.UnitParam.IsThrow());
            }
        }

        public bool IsKnockBack
        {
            get
            {
                return ((this.UnitParam == null) ? 0 : this.UnitParam.IsKnockBack());
            }
        }

        [CompilerGenerated]
        private sealed class <AppendUnlockedJobs>c__AnonStorey3FD
        {
            internal JobSetParam before;

            public <AppendUnlockedJobs>c__AnonStorey3FD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B8(Json_Job p)
            {
                return (p.iname == this.before.job);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey3FE
        {
            internal string skinName;

            public <Deserialize>c__AnonStorey3FE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B9(ArtifactParam af)
            {
                return (af.iname == this.skinName);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey3FF
        {
            internal JobSetParam jobset;

            public <Deserialize>c__AnonStorey3FF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4BA(JobSetParam p)
            {
                return (p.jobchange == this.jobset.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindJobDataBySkillData>c__AnonStorey3F1
        {
            internal SkillParam param;
            internal string hitAbliId;

            public <FindJobDataBySkillData>c__AnonStorey3F1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4AB(SkillData p)
            {
                return (p.SkillID == this.param.iname);
            }

            internal bool <>m__4AC(OString name)
            {
                return (name == this.hitAbliId);
            }
        }

        [CompilerGenerated]
        private sealed class <GetAllSkins>c__AnonStorey3F6
        {
            internal int i;
            internal UnitData <>f__this;

            public <GetAllSkins>c__AnonStorey3F6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B2(ArtifactParam af)
            {
                return (af.iname == this.<>f__this.mUnitParam.skins[this.i]);
            }
        }

        [CompilerGenerated]
        private sealed class <GetEnableConceptCardSkins>c__AnonStorey3F7
        {
            internal List<ConceptCardEquipEffect> equip_effects;

            public <GetEnableConceptCardSkins>c__AnonStorey3F7()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <GetEnableConceptCardSkins>c__AnonStorey3F8
        {
            internal int i;
            internal UnitData.<GetEnableConceptCardSkins>c__AnonStorey3F7 <>f__ref$1015;

            public <GetEnableConceptCardSkins>c__AnonStorey3F8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B3(ArtifactParam art)
            {
                return (art.iname == this.<>f__ref$1015.equip_effects[this.i].Skin);
            }
        }

        [CompilerGenerated]
        private sealed class <GetQuestUnlockConditions>c__AnonStorey3FC
        {
            internal QuestParam quest;

            public <GetQuestUnlockConditions>c__AnonStorey3FC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B7(string p)
            {
                return (p == this.quest.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetSelectableSkins>c__AnonStorey3F5
        {
            internal JobParam jobParam;

            public <GetSelectableSkins>c__AnonStorey3F5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B1(JobData j)
            {
                return (j.Param.iname == this.jobParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetSelectedSkinData>c__AnonStorey3F2
        {
            internal int jobIndex;
            internal UnitData <>f__this;

            public <GetSelectedSkinData>c__AnonStorey3F2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4AD(ArtifactParam p)
            {
                return (p.iname == this.<>f__this.mJobs[this.jobIndex].SelectedSkin);
            }
        }

        [CompilerGenerated]
        private sealed class <GetTobiraData>c__AnonStorey40D
        {
            internal TobiraParam.Category category;

            public <GetTobiraData>c__AnonStorey40D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4CE(TobiraData tobira)
            {
                return ((tobira.Param == null) ? 0 : (tobira.Param.TobiraCategory == this.category));
            }
        }

        [CompilerGenerated]
        private sealed class <IsChQuestParentUnlocked>c__AnonStorey40B
        {
            internal QuestParam quest;

            public <IsChQuestParentUnlocked>c__AnonStorey40B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4C9(QuestClearUnlockUnitDataParam p)
            {
                return ((p.qids == null) ? 0 : ((-1 == Array.FindIndex<string>(p.qids, new Predicate<string>(this.<>m__4CF))) == 0));
            }

            internal bool <>m__4CF(string s)
            {
                return (s == this.quest.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <IsChQuestParentUnlocked>c__AnonStorey40C
        {
            internal QuestClearUnlockUnitDataParam unlock;

            public <IsChQuestParentUnlocked>c__AnonStorey40C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4CA(JobData p)
            {
                return (((p.JobID == this.unlock.parent_id) == null) ? 0 : p.IsActivated);
            }

            internal bool <>m__4CB(AbilityData p)
            {
                return (p.AbilityID == this.unlock.parent_id);
            }

            internal bool <>m__4CC(AbilityData p)
            {
                return (p.AbilityID == this.unlock.old_id);
            }

            internal bool <>m__4CD(LearningSkill p)
            {
                return (p.iname == this.unlock.old_id);
            }
        }

        [CompilerGenerated]
        private sealed class <IsQuestClearUnlocked>c__AnonStorey3FB
        {
            internal string id;
            internal QuestClearUnlockUnitDataParam.EUnlockTypes type;

            public <IsQuestClearUnlocked>c__AnonStorey3FB()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B5(QuestClearUnlockUnitDataParam p)
            {
                return (((p.new_id == this.id) == null) ? 0 : (p.type == this.type));
            }

            internal bool <>m__4B6(QuestClearUnlockUnitDataParam p)
            {
                return (((p.new_id == this.id) == null) ? 0 : (p.type == this.type));
            }
        }

        [CompilerGenerated]
        private sealed class <MeetsTobiraConditions>c__AnonStorey400
        {
            internal TobiraConditionParam cond;

            public <MeetsTobiraConditions>c__AnonStorey400()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4BB(TobiraData tobira)
            {
                return (tobira.Param.TobiraCategory == this.cond.CondUnit.TobiraCategory);
            }
        }

        [CompilerGenerated]
        private sealed class <RefrectionDeriveSkillAndAbility>c__AnonStorey407
        {
            internal AbilityDeriveParam abilityDeriveParam;
            internal UnitData.<RefrectionDeriveSkillAndAbility>c__AnonStorey408 <>f__ref$1032;

            public <RefrectionDeriveSkillAndAbility>c__AnonStorey407()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4C3(AbilityData battleAbility)
            {
                return (battleAbility.Param.iname == this.abilityDeriveParam.m_BaseParam.iname);
            }

            internal void <>m__4C4(SkillData skill)
            {
                this.<>f__ref$1032.refSkills.Remove(skill);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <RefrectionDeriveSkillAndAbility>c__AnonStorey408
        {
            internal List<SkillData> refSkills;
            internal List<AbilityData> refAbilitys;

            public <RefrectionDeriveSkillAndAbility>c__AnonStorey408()
            {
                base..ctor();
                return;
            }

            internal void <>m__4C7(AbilityData ability)
            {
                this.refAbilitys.Remove(ability);
                return;
            }

            internal void <>m__4C8(SkillData skill)
            {
                this.refSkills.Remove(skill);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <RefrectionDeriveSkillAndAbility>c__AnonStorey409
        {
            internal SkillData skill;

            public <RefrectionDeriveSkillAndAbility>c__AnonStorey409()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4C5(SkillData btl_skill)
            {
                return (btl_skill.SkillParam.iname == this.skill.SkillParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefrectionDeriveSkillAndAbility>c__AnonStorey40A
        {
            internal SkillDeriveParam skillDeriveParam;

            public <RefrectionDeriveSkillAndAbility>c__AnonStorey40A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4C6(SkillData battleSkill)
            {
                return (battleSkill.SkillParam.iname == this.skillDeriveParam.m_BaseParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SearchDerivedAbilitys>c__AnonStorey403
        {
            internal AbilityData baseAbility;

            public <SearchDerivedAbilitys>c__AnonStorey403()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4BF(AbilityDeriveParam abilityDeriveParam)
            {
                return (abilityDeriveParam.BaseAbilityIname == this.baseAbility.Param.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SearchDerivedAbilitys>c__AnonStorey404
        {
            internal SkillData derivedAbilitySkill;

            public <SearchDerivedAbilitys>c__AnonStorey404()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4C0(SkillData skill)
            {
                return (skill.SkillParam.iname == this.derivedAbilitySkill.SkillParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SearchDerivedAbilitys>c__AnonStorey405
        {
            internal SkillDeriveParam skillDeriveParam;

            public <SearchDerivedAbilitys>c__AnonStorey405()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4C1(SkillData battleSkill)
            {
                return (battleSkill.SkillParam.iname == this.skillDeriveParam.m_BaseParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SearchDerivedSkills>c__AnonStorey406
        {
            internal SkillDeriveParam skillDeriveParam;

            public <SearchDerivedSkills>c__AnonStorey406()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4C2(SkillData skill)
            {
                return (skill.SkillParam.iname == this.skillDeriveParam.DeriveSkillIname);
            }
        }

        [CompilerGenerated]
        private sealed class <SetJobSkin>c__AnonStorey3F3
        {
            internal string afName;

            public <SetJobSkin>c__AnonStorey3F3()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4AE(ArtifactParam p)
            {
                return (p.iname == this.afName);
            }

            internal bool <>m__4AF(ConceptCardEquipEffect effect)
            {
                return (effect.Skin == this.afName);
            }
        }

        [CompilerGenerated]
        private sealed class <SetJobSkinAll>c__AnonStorey3F4
        {
            internal string afName;

            public <SetJobSkinAll>c__AnonStorey3F4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B0(ArtifactParam p)
            {
                return (p.iname == this.afName);
            }
        }

        [CompilerGenerated]
        private sealed class <UnlockedSkillDiff>c__AnonStorey3F9
        {
            internal string[] newUnlocks;

            public <UnlockedSkillDiff>c__AnonStorey3F9()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <UnlockedSkillDiff>c__AnonStorey3FA
        {
            internal int i;
            internal UnitData.<UnlockedSkillDiff>c__AnonStorey3F9 <>f__ref$1017;

            public <UnlockedSkillDiff>c__AnonStorey3FA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4B4(string p)
            {
                return (p == this.<>f__ref$1017.newUnlocks[this.i]);
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateUnitBattleAbilityAll>c__AnonStorey401
        {
            internal AbilityData ability;

            public <UpdateUnitBattleAbilityAll>c__AnonStorey401()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4BD(AbilityData btl_abil)
            {
                return (btl_abil.Param.iname == this.ability.Param.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateUnitBattleAbilityAll>c__AnonStorey402
        {
            internal SkillData skill;

            public <UpdateUnitBattleAbilityAll>c__AnonStorey402()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4BE(SkillData btl_skill)
            {
                return (btl_skill.SkillParam.iname == this.skill.SkillParam.iname);
            }
        }

        public class CharacterQuestParam
        {
            public int EpisodeNum;
            public string EpisodeTitle;
            public bool IsNew;
            public bool IsAvailable;
            public QuestParam Param;

            public CharacterQuestParam()
            {
                base..ctor();
                return;
            }
        }

        public class CharacterQuestUnlockProgress
        {
            public int Level;
            public int Rarity;
            public QuestParam CondQuest;
            public bool ClearUnlocksCond;

            public CharacterQuestUnlockProgress()
            {
                base..ctor();
                return;
            }
        }

        public class Json_PlaybackVoiceData
        {
            public int playback_voice_unlocked;
            public List<string> cue_names;

            public Json_PlaybackVoiceData()
            {
                this.cue_names = new List<string>();
                base..ctor();
                return;
            }
        }

        [Flags]
        public enum TemporaryFlags
        {
            TemporaryUnitData = 1,
            AllowJobChange = 2
        }

        public class TobiraConditioError
        {
            public ErrorType Type;

            public TobiraConditioError(ErrorType type)
            {
                base..ctor();
                this.Type = type;
                return;
            }

            public enum ErrorType
            {
                None,
                Quest,
                UnitNone,
                UnitLevel,
                UnitAwakeLevel,
                UnitJobLevel,
                UnitTobiraLevel,
                UnitOpenAllJobs
            }
        }

        public class UnitPlaybackVoiceData
        {
            private UnitData unit;
            private MySound.Voice voice;
            private List<UnitData.UnitVoiceCueInfo> voice_cue_list;
            [CompilerGenerated]
            private static Dictionary<string, int> <>f__switch$map15;
            [CompilerGenerated]
            private static Comparison<UnitData.UnitVoiceCueInfo> <>f__am$cache4;
            [CompilerGenerated]
            private static Comparison<UnitData.UnitVoiceCueInfo> <>f__am$cache5;
            [CompilerGenerated]
            private static Comparison<UnitData.UnitVoiceCueInfo> <>f__am$cache6;
            [CompilerGenerated]
            private static Comparison<UnitData.UnitVoiceCueInfo> <>f__am$cache7;
            [CompilerGenerated]
            private static Dictionary<string, int> <>f__switch$map16;

            public UnitPlaybackVoiceData()
            {
                base..ctor();
                return;
            }

            [CompilerGenerated]
            private static int <Init>m__4D0(UnitData.UnitVoiceCueInfo a, UnitData.UnitVoiceCueInfo b)
            {
                return (a.number - b.number);
            }

            [CompilerGenerated]
            private static int <Init>m__4D1(UnitData.UnitVoiceCueInfo a, UnitData.UnitVoiceCueInfo b)
            {
                return (a.number - b.number);
            }

            [CompilerGenerated]
            private static int <Init>m__4D2(UnitData.UnitVoiceCueInfo a, UnitData.UnitVoiceCueInfo b)
            {
                return (a.number - b.number);
            }

            [CompilerGenerated]
            private static int <Init>m__4D3(UnitData.UnitVoiceCueInfo a, UnitData.UnitVoiceCueInfo b)
            {
                return (a.number - b.number);
            }

            private bool CheckConditions(UnitData _unit, string _tmp_name)
            {
                if (_unit.CheckUnlockPlaybackVoice() != null)
                {
                    goto Label_000D;
                }
                return 0;
            Label_000D:
                if (this.CheckConditionsUnitLevel(_tmp_name, 0x55) != null)
                {
                    goto Label_001D;
                }
                return 0;
            Label_001D:
                if (this.CheckConditionsUnitLevel(_tmp_name, 0x4b) != null)
                {
                    goto Label_002D;
                }
                return 0;
            Label_002D:
                if (this.CheckConditionsUnitLevel(_tmp_name, 0x41) != null)
                {
                    goto Label_003D;
                }
                return 0;
            Label_003D:
                if (this.CheckConditionsJobMaster1(_tmp_name) != null)
                {
                    goto Label_004B;
                }
                return 0;
            Label_004B:
                return 1;
            }

            private bool CheckConditionsJobMaster1(string _tmp_name)
            {
                int num;
                if (this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES["UNIT_JOB_MASTER1"], _tmp_name) != null)
                {
                    goto Label_001D;
                }
                return 1;
            Label_001D:
                num = 0;
                goto Label_0041;
            Label_0024:
                if (this.unit.Jobs[num].CheckJobMaster() == null)
                {
                    goto Label_003D;
                }
                return 1;
            Label_003D:
                num += 1;
            Label_0041:
                if (num < ((int) this.unit.Jobs.Length))
                {
                    goto Label_0024;
                }
                return 0;
            }

            private unsafe bool CheckConditionsUnitLevel(string _tmp_name, int _level)
            {
                if (this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES["UNIT_LEVEL" + &_level.ToString()], _tmp_name) != null)
                {
                    goto Label_0029;
                }
                return 1;
            Label_0029:
                if (this.unit.Lv < _level)
                {
                    goto Label_003C;
                }
                return 1;
            Label_003C:
                return 0;
            }

            public void Cleanup()
            {
                if (this.voice == null)
                {
                    goto Label_002D;
                }
                this.voice.StopAll(1f);
                this.voice.Cleanup();
                this.voice = null;
            Label_002D:
                return;
            }

            private bool ContainsArray(string[] _array, string _target)
            {
                int num;
                num = 0;
                goto Label_001B;
            Label_0007:
                if ((_array[num] == _target) == null)
                {
                    goto Label_0017;
                }
                return 1;
            Label_0017:
                num += 1;
            Label_001B:
                if (num < ((int) _array.Length))
                {
                    goto Label_0007;
                }
                return 0;
            }

            private unsafe string GetUnlockConditionsText(string _dictionary_key)
            {
                string str;
                Dictionary<string, int> dictionary;
                int num;
                str = _dictionary_key;
                if (str == null)
                {
                    goto Label_00D7;
                }
                if (<>f__switch$map16 != null)
                {
                    goto Label_004F;
                }
                dictionary = new Dictionary<string, int>(4);
                dictionary.Add("UNIT_LEVEL85", 0);
                dictionary.Add("UNIT_LEVEL75", 1);
                dictionary.Add("UNIT_LEVEL65", 2);
                dictionary.Add("UNIT_JOB_MASTER1", 3);
                <>f__switch$map16 = dictionary;
            Label_004F:
                if (<>f__switch$map16.TryGetValue(str, &num) == null)
                {
                    goto Label_00D7;
                }
                switch (num)
                {
                    case 0:
                        goto Label_007C;

                    case 1:
                        goto Label_0093;

                    case 2:
                        goto Label_00AA;

                    case 3:
                        goto Label_00C1;
                }
                goto Label_00D7;
            Label_007C:
                return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (int) 0x55);
            Label_0093:
                return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (int) 0x4b);
            Label_00AA:
                return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (int) 0x41);
            Label_00C1:
                return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_JOB_MASTER"), new object[0]);
            Label_00D7:
                return string.Empty;
            }

            public unsafe void Init(UnitData _unit, MySound.Voice _voice, CriAtomEx.CueInfo[] _cueInfo, string _voice_name)
            {
                char[] chArray1;
                List<UnitData.UnitVoiceCueInfo> list;
                List<UnitData.UnitVoiceCueInfo> list2;
                List<UnitData.UnitVoiceCueInfo> list3;
                List<UnitData.UnitVoiceCueInfo> list4;
                string str;
                UnitData.Json_PlaybackVoiceData data;
                string[] strArray;
                string str2;
                string str3;
                int num;
                bool flag;
                string str4;
                string str5;
                UnitData.UnitVoiceCueInfo info;
                Exception exception;
                long num2;
                string str6;
                Dictionary<string, int> dictionary;
                int num3;
                this.unit = _unit;
                this.voice = _voice;
                this.voice_cue_list = new List<UnitData.UnitVoiceCueInfo>();
                list = new List<UnitData.UnitVoiceCueInfo>();
                list2 = new List<UnitData.UnitVoiceCueInfo>();
                list3 = new List<UnitData.UnitVoiceCueInfo>();
                list4 = new List<UnitData.UnitVoiceCueInfo>();
            Label_0031:
                try
                {
                    data = JsonUtility.FromJson<UnitData.Json_PlaybackVoiceData>(PlayerPrefsUtility.GetString(&this.unit.UniqueID.ToString(), string.Empty));
                    strArray = null;
                    str2 = string.Empty;
                    str3 = string.Empty;
                    num = 0;
                    goto Label_02A6;
                Label_0073:
                    chArray1 = new char[] { 0x5f };
                    strArray = &(_cueInfo[num]).name.Split(chArray1);
                    if (((int) strArray.Length) >= 2)
                    {
                        goto Label_00A1;
                    }
                    goto Label_02A0;
                Label_00A1:
                    str2 = strArray[((int) strArray.Length) - 2];
                    str3 = strArray[((int) strArray.Length) - 1];
                    flag = 0;
                    str4 = str2 + "_" + str3;
                    str5 = LocalizedText.Get("playback_list." + str4.ToUpper(), &flag);
                    if (flag != null)
                    {
                        goto Label_00F0;
                    }
                    goto Label_02A0;
                Label_00F0:
                    info = new UnitData.UnitVoiceCueInfo();
                    info.cueInfo = *(&(_cueInfo[num]));
                    info.voice_name = str5;
                    info.is_locked = 0;
                    info.is_new = 0;
                    info.number = int.Parse(str3);
                    this.SetConditions(str4, &info);
                    if (info.has_conditions == null)
                    {
                        goto Label_01D8;
                    }
                    info.is_locked = this.CheckConditions(this.unit, str4) == 0;
                    if (info.is_locked != null)
                    {
                        goto Label_01AB;
                    }
                    if (data == null)
                    {
                        goto Label_01A3;
                    }
                    if (data.cue_names.Count <= 0)
                    {
                        goto Label_01A3;
                    }
                    if (data.cue_names.Contains(&(_cueInfo[num]).name) != null)
                    {
                        goto Label_01AB;
                    }
                Label_01A3:
                    info.is_new = 1;
                Label_01AB:
                    if (data == null)
                    {
                        goto Label_01D8;
                    }
                    if (data.cue_names.Contains(&(_cueInfo[num]).name) == null)
                    {
                        goto Label_01D8;
                    }
                    info.is_locked = 0;
                Label_01D8:
                    str6 = str2;
                    if (str6 == null)
                    {
                        goto Label_0293;
                    }
                    if (<>f__switch$map15 != null)
                    {
                        goto Label_0230;
                    }
                    dictionary = new Dictionary<string, int>(4);
                    dictionary.Add("chara", 0);
                    dictionary.Add("voice", 1);
                    dictionary.Add("battle", 2);
                    dictionary.Add("sys", 3);
                    <>f__switch$map15 = dictionary;
                Label_0230:
                    if (<>f__switch$map15.TryGetValue(str6, &num3) == null)
                    {
                        goto Label_0293;
                    }
                    switch (num3)
                    {
                        case 0:
                            goto Label_025F;

                        case 1:
                            goto Label_026C;

                        case 2:
                            goto Label_0279;

                        case 3:
                            goto Label_0286;
                    }
                    goto Label_0293;
                Label_025F:
                    list.Add(info);
                    goto Label_02A0;
                Label_026C:
                    list.Add(info);
                    goto Label_02A0;
                Label_0279:
                    list2.Add(info);
                    goto Label_02A0;
                Label_0286:
                    list3.Add(info);
                    goto Label_02A0;
                Label_0293:
                    list4.Add(info);
                Label_02A0:
                    num += 1;
                Label_02A6:
                    if (num < ((int) _cueInfo.Length))
                    {
                        goto Label_0073;
                    }
                    if (<>f__am$cache4 != null)
                    {
                        goto Label_02C9;
                    }
                    <>f__am$cache4 = new Comparison<UnitData.UnitVoiceCueInfo>(UnitData.UnitPlaybackVoiceData.<Init>m__4D0);
                Label_02C9:
                    list.Sort(<>f__am$cache4);
                    if (<>f__am$cache5 != null)
                    {
                        goto Label_02EC;
                    }
                    <>f__am$cache5 = new Comparison<UnitData.UnitVoiceCueInfo>(UnitData.UnitPlaybackVoiceData.<Init>m__4D1);
                Label_02EC:
                    list2.Sort(<>f__am$cache5);
                    if (<>f__am$cache6 != null)
                    {
                        goto Label_030F;
                    }
                    <>f__am$cache6 = new Comparison<UnitData.UnitVoiceCueInfo>(UnitData.UnitPlaybackVoiceData.<Init>m__4D2);
                Label_030F:
                    list3.Sort(<>f__am$cache6);
                    if (<>f__am$cache7 != null)
                    {
                        goto Label_0332;
                    }
                    <>f__am$cache7 = new Comparison<UnitData.UnitVoiceCueInfo>(UnitData.UnitPlaybackVoiceData.<Init>m__4D3);
                Label_0332:
                    list4.Sort(<>f__am$cache7);
                    this.voice_cue_list.AddRange(list);
                    this.voice_cue_list.AddRange(list2);
                    this.voice_cue_list.AddRange(list3);
                    this.voice_cue_list.AddRange(list4);
                    goto Label_037F;
                }
                catch (Exception exception1)
                {
                Label_0371:
                    exception = exception1;
                    DebugUtility.LogException(exception);
                    goto Label_037F;
                }
            Label_037F:
                return;
            }

            private unsafe void SetConditions(string _tmp_name, ref UnitData.UnitVoiceCueInfo _info)
            {
                string str;
                Dictionary<string, string[]>.KeyCollection.Enumerator enumerator;
                *(_info).has_conditions = 0;
                enumerator = UnitData.CONDITIONS_TARGET_NAMES.Keys.GetEnumerator();
            Label_0018:
                try
                {
                    goto Label_0057;
                Label_001D:
                    str = &enumerator.Current;
                    if (this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES[str], _tmp_name) == null)
                    {
                        goto Label_0057;
                    }
                    *(_info).has_conditions = 1;
                    *(_info).unlock_conditions_text = this.GetUnlockConditionsText(str);
                    goto Label_0063;
                Label_0057:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_001D;
                    }
                Label_0063:
                    goto Label_0074;
                }
                finally
                {
                Label_0068:
                    ((Dictionary<string, string[]>.KeyCollection.Enumerator) enumerator).Dispose();
                }
            Label_0074:
                return;
            }

            public MySound.Voice Voice
            {
                get
                {
                    return this.voice;
                }
            }

            public List<UnitData.UnitVoiceCueInfo> VoiceCueList
            {
                get
                {
                    return this.voice_cue_list;
                }
            }
        }

        public class UnitVoiceCueInfo
        {
            public CriAtomEx.CueInfo cueInfo;
            public string voice_name;
            public bool has_conditions;
            public bool is_locked;
            public bool is_new;
            public string unlock_conditions_text;
            public int number;

            public UnitVoiceCueInfo()
            {
                base..ctor();
                return;
            }
        }
    }
}

