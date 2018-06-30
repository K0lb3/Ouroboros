namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CharacterQuestData
    {
        private EStatus status;
        public EType questType;
        public QuestParam questParam;
        public UnitData unitData1;
        public UnitData unitData2;
        public UnitParam unitParam1;
        public UnitParam unitParam2;
        [CompilerGenerated]
        private static Predicate<KeyValuePair<QuestParam, bool>> <>f__am$cache7;
        [CompilerGenerated]
        private static Predicate<KeyValuePair<QuestParam, bool>> <>f__am$cache8;
        [CompilerGenerated]
        private static Predicate<KeyValuePair<QuestParam, bool>> <>f__am$cache9;
        [CompilerGenerated]
        private static Predicate<KeyValuePair<QuestParam, bool>> <>f__am$cacheA;

        public CharacterQuestData()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static unsafe bool <UpdateStatus>m__2B1(KeyValuePair<QuestParam, bool> pair)
        {
            return (&pair.Key.state == 2);
        }

        [CompilerGenerated]
        private static unsafe bool <UpdateStatus>m__2B2(KeyValuePair<QuestParam, bool> pair)
        {
            return ((&pair.Key.state != null) ? 0 : &pair.Value);
        }

        [CompilerGenerated]
        private static unsafe bool <UpdateStatus>m__2B3(KeyValuePair<QuestParam, bool> pair)
        {
            return (&pair.Key.state == 2);
        }

        [CompilerGenerated]
        private static unsafe bool <UpdateStatus>m__2B4(KeyValuePair<QuestParam, bool> pair)
        {
            return ((&pair.Key.state != null) ? 0 : &pair.Value);
        }

        public CollaboSkillParam.Pair GetPairUnit()
        {
            CollaboSkillParam.Pair pair;
            if (this.unitData1 == null)
            {
                goto Label_0016;
            }
            if (this.unitData2 != null)
            {
                goto Label_0018;
            }
        Label_0016:
            return null;
        Label_0018:
            pair = new CollaboSkillParam.Pair(this.unitData1.UnitParam, this.unitData2.UnitParam);
            return pair;
        }

        public void UpdateStatus()
        {
            bool flag;
            List<KeyValuePair<QuestParam, bool>> list;
            int num;
            int num2;
            List<KeyValuePair<QuestParam, bool>> list2;
            int num3;
            int num4;
            if (this.questType != null)
            {
                goto Label_00D5;
            }
            if (this.unitData1 == null)
            {
                goto Label_00C9;
            }
            if (this.unitData1.IsOpenCharacterQuest() == null)
            {
                goto Label_00BD;
            }
            list = CharacterQuestList.GetCharacterQuests(this.unitData1);
            if (<>f__am$cache7 != null)
            {
                goto Label_004D;
            }
            <>f__am$cache7 = new Predicate<KeyValuePair<QuestParam, bool>>(CharacterQuestData.<UpdateStatus>m__2B1);
        Label_004D:
            num = list.FindAll(<>f__am$cache7).Count;
            if (<>f__am$cache8 != null)
            {
                goto Label_0076;
            }
            <>f__am$cache8 = new Predicate<KeyValuePair<QuestParam, bool>>(CharacterQuestData.<UpdateStatus>m__2B2);
        Label_0076:
            if (list.FindAll(<>f__am$cache8).Count <= 0)
            {
                goto Label_0099;
            }
            this.status = 0;
            goto Label_00B8;
        Label_0099:
            if (num != list.Count)
            {
                goto Label_00B1;
            }
            this.status = 3;
            goto Label_00B8;
        Label_00B1:
            this.status = 1;
        Label_00B8:
            goto Label_00C4;
        Label_00BD:
            this.status = 2;
        Label_00C4:
            goto Label_00D0;
        Label_00C9:
            this.status = 2;
        Label_00D0:
            goto Label_0195;
        Label_00D5:
            if (this.unitData1 == null)
            {
                goto Label_018E;
            }
            if (this.unitData2 == null)
            {
                goto Label_018E;
            }
            list2 = CharacterQuestList.GetCollaboSkillQuests(this.unitData1, this.unitData2);
            if (<>f__am$cache9 != null)
            {
                goto Label_0118;
            }
            <>f__am$cache9 = new Predicate<KeyValuePair<QuestParam, bool>>(CharacterQuestData.<UpdateStatus>m__2B3);
        Label_0118:
            num3 = list2.FindAll(<>f__am$cache9).Count;
            if (<>f__am$cacheA != null)
            {
                goto Label_0143;
            }
            <>f__am$cacheA = new Predicate<KeyValuePair<QuestParam, bool>>(CharacterQuestData.<UpdateStatus>m__2B4);
        Label_0143:
            if (list2.FindAll(<>f__am$cacheA).Count <= 0)
            {
                goto Label_0168;
            }
            this.status = 0;
            goto Label_0189;
        Label_0168:
            if (num3 != list2.Count)
            {
                goto Label_0182;
            }
            this.status = 3;
            goto Label_0189;
        Label_0182:
            this.status = 1;
        Label_0189:
            goto Label_0195;
        Label_018E:
            this.status = 2;
        Label_0195:
            return;
        }

        public bool HasUnit
        {
            get
            {
                return ((this.unitData1 == null) == 0);
            }
        }

        public bool HasPairUnit
        {
            get
            {
                return ((this.unitData1 == null) ? 0 : ((this.unitData2 == null) == 0));
            }
        }

        public EStatus Status
        {
            get
            {
                return this.status;
            }
        }

        public bool IsLock
        {
            get
            {
                return (this.status == 2);
            }
        }

        public bool IsChallenged
        {
            get
            {
                return (this.status == 1);
            }
        }

        public bool IsComplete
        {
            get
            {
                return (this.status == 3);
            }
        }

        public bool IsNew
        {
            get
            {
                return (this.status == 0);
            }
        }

        public enum EStatus
        {
            New,
            Challenged,
            Lock,
            Complete
        }

        public enum EType
        {
            Chara,
            Collabo
        }
    }
}

