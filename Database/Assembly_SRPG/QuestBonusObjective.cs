namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class QuestBonusObjective
    {
        public const string TXT_ID_SUFFIX_PROGRESS = "_PROGRESS";
        public const string TXT_ID_SUFFIX_PROGRESS_TARGET = "_PROGRESS_TARGET";
        public const string TXT_ID_SUFFIX_PROGRESS_OK = "_PROGRESS_OK";
        public const string TXT_ID_SUFFIX_PROGRESS_NG = "_PROGRESS_NG";
        public const int FAILED_MISSION_VALUE = 1;
        public string item;
        public int itemNum;
        public RewardType itemType;
        public EMissionType Type;
        public string TypeParam;
        public bool IsTakeoverProgress;
        private static Dictionary<EMissionType, QuestMissionTypeAttribute> m_QuestMissionTypeDic;
        private static Dictionary<EMissionType, TowerQuestMissionTypeAttribute> m_TowerQuestMissionTypeDic;

        static QuestBonusObjective()
        {
            m_QuestMissionTypeDic = new Dictionary<EMissionType, QuestMissionTypeAttribute>();
            m_TowerQuestMissionTypeDic = new Dictionary<EMissionType, TowerQuestMissionTypeAttribute>();
            return;
        }

        public QuestBonusObjective()
        {
            base..ctor();
            return;
        }

        public bool CheckHomeMissionValueAchievable(int missions_val)
        {
            EMissionType type;
            if (this.Type == 0x37)
            {
                goto Label_0014;
            }
            goto Label_001E;
        Label_0014:
            missions_val += 1;
        Label_001E:
            return this.CheckMissionValueAchievable(missions_val);
        }

        public unsafe bool CheckMissionValueAchievable(int missions_val)
        {
            TowerQuestMissionTypeAttribute attribute;
            QuestMissionTypeAttribute attribute2;
            string str;
            int num;
            QuestMissionProgressJudgeType type;
            if (this.IsProgressMission() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            attribute = this.GetTowerQuestMissionTypeAttribute(this.Type);
            if (attribute != null)
            {
                goto Label_003C;
            }
            DebugUtility.LogError(string.Format("塔ではサポートされていないミッションです。⇒ type : {0}", (int) this.Type));
            return 0;
        Label_003C:
            attribute2 = this.GetQuestMissionTypeAttribute(this.Type);
            str = string.Empty;
            num = 0;
            if (attribute2.ValueType != 1)
            {
                goto Label_006B;
            }
            this.TryGetIntValue(&num);
            goto Label_0082;
        Label_006B:
            if (attribute2.ValueType != 5)
            {
                goto Label_0082;
            }
            this.TryGetKeyValue(&str, &num);
        Label_0082:
            switch ((attribute.ProgressJudgeType - 1))
            {
                case 0:
                    goto Label_00B0;

                case 1:
                    goto Label_00B8;

                case 2:
                    goto Label_00BD;

                case 3:
                    goto Label_00C5;

                case 4:
                    goto Label_00CA;

                case 5:
                    goto Label_00CF;
            }
            goto Label_00D4;
        Label_00B0:
            return ((missions_val < num) == 0);
        Label_00B8:
            return (missions_val > num);
        Label_00BD:
            return ((missions_val > num) == 0);
        Label_00C5:
            return (missions_val < num);
        Label_00CA:
            return (missions_val == num);
        Label_00CF:
            return (missions_val == 0);
        Label_00D4:
            return 0;
        }

        private unsafe QuestMissionTypeAttribute GetQuestMissionTypeAttribute(EMissionType missionType)
        {
            QuestMissionTypeAttribute attribute;
            attribute = null;
            if (m_QuestMissionTypeDic.TryGetValue(missionType, &attribute) != null)
            {
                goto Label_002D;
            }
            attribute = GameUtility.GetCustomAttribute<QuestMissionTypeAttribute>((EMissionType) missionType, 0);
            m_QuestMissionTypeDic.Add(missionType, attribute);
        Label_002D:
            return attribute;
        }

        private unsafe TowerQuestMissionTypeAttribute GetTowerQuestMissionTypeAttribute(EMissionType missionType)
        {
            TowerQuestMissionTypeAttribute attribute;
            attribute = null;
            if (m_TowerQuestMissionTypeDic.TryGetValue(missionType, &attribute) != null)
            {
                goto Label_002D;
            }
            attribute = GameUtility.GetCustomAttribute<TowerQuestMissionTypeAttribute>((EMissionType) missionType, 0);
            m_TowerQuestMissionTypeDic.Add(missionType, attribute);
        Label_002D:
            return attribute;
        }

        public bool IsMissionTypeAllowLose()
        {
            if (this.IsTakeoverProgress != null)
            {
                goto Label_0060;
            }
            if (this.Type == 10)
            {
                goto Label_0059;
            }
            if (this.Type == 14)
            {
                goto Label_0059;
            }
            if (this.Type == 0x10)
            {
                goto Label_0059;
            }
            if (this.Type == 20)
            {
                goto Label_0059;
            }
            if (this.Type == 0x17)
            {
                goto Label_0059;
            }
            if (this.Type != 0x33)
            {
                goto Label_007E;
            }
        Label_0059:
            return 1;
            goto Label_007E;
        Label_0060:
            if (this.IsProgressMission() != null)
            {
                goto Label_006D;
            }
            return 0;
        Label_006D:
            if (this.Type != 0x26)
            {
                goto Label_007C;
            }
            return 0;
        Label_007C:
            return 1;
        Label_007E:
            return 0;
        }

        public bool IsMissionTypeExecSkill()
        {
            if (this.Type == 0x31)
            {
                goto Label_0027;
            }
            if (this.Type == 0x29)
            {
                goto Label_0027;
            }
            if (this.Type != 40)
            {
                goto Label_0029;
            }
        Label_0027:
            return 1;
        Label_0029:
            return 0;
        }

        public bool IsProgressMission()
        {
            TowerQuestMissionTypeAttribute attribute;
            if (this.IsTakeoverProgress != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            attribute = this.GetTowerQuestMissionTypeAttribute(this.Type);
            if (attribute != null)
            {
                goto Label_003C;
            }
            DebugUtility.LogError(string.Format("塔ではサポートされていないミッションです。⇒ type : {0}", (int) this.Type));
            return 0;
        Label_003C:
            if (attribute.ProgressJudgeType != null)
            {
                goto Label_0063;
            }
            DebugUtility.LogError(string.Format("このミッションはバトルを跨ぐ設定をサポートしていません。⇒ type : {0}", (int) this.Type));
            return 0;
        Label_0063:
            return 1;
        }

        public bool TryGetArray(ref string[] values)
        {
            char[] chArray1;
            string str;
            if (string.IsNullOrEmpty(this.TypeParam) == null)
            {
                goto Label_001C;
            }
            DebugUtility.LogError("条件が設定されていません。");
            return 0;
        Label_001C:
            str = this.TypeParam.Replace(" ", string.Empty);
            chArray1 = new char[] { 0x2c };
            *(values) = str.Split(chArray1);
            return 1;
        }

        public bool TryGetIntValue(ref int value)
        {
            return int.TryParse(this.TypeParam, value);
        }

        public bool TryGetKeyValue(ref string key, ref int value)
        {
            char[] chArray1;
            string str;
            string[] strArray;
            if (string.IsNullOrEmpty(this.TypeParam) == null)
            {
                goto Label_001C;
            }
            DebugUtility.LogError("条件が設定されていません。");
            return 0;
        Label_001C:
            chArray1 = new char[] { 0x2c };
            strArray = this.TypeParam.Replace(" ", string.Empty).Split(chArray1);
            if (((int) strArray.Length) >= 2)
            {
                goto Label_004F;
            }
            return 0;
        Label_004F:
            *(key) = strArray[0].Trim();
            return int.TryParse(strArray[1].Trim(), value);
        }
    }
}

