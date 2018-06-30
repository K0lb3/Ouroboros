namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SkillMap
    {
        private Unit m_Owner;
        private uint[] m_SkillSeed;
        private List<Data> m_List;
        private BattleCore.SkillResult m_UseSkill;
        private AIAction m_Action;
        private bool m_IsNoExecActionSkill;
        private List<SkillData> m_AllSkills;
        private int m_AttackHeight;
        private int m_UseSkillNum;
        private List<List<SkillData>> m_UseSkillLists;
        private List<SkillData> m_ForceSkillList;
        private List<SkillData> m_HealSkills;
        private List<SkillData> m_DamageSkills;
        private List<SkillData> m_SupportSkills;
        private List<SkillData> m_CureConditionSkills;
        private List<SkillData> m_FailConditionSkills;
        private List<SkillData> m_DisableConditionSkills;
        private List<SkillData> m_ExeSkills;

        public SkillMap()
        {
            base..ctor();
            this.m_Owner = null;
            this.m_SkillSeed = new uint[4];
            this.m_List = new List<Data>();
            this.m_AllSkills = new List<SkillData>();
            this.m_UseSkillLists = new List<List<SkillData>>();
            this.m_ForceSkillList = new List<SkillData>();
            this.m_HealSkills = new List<SkillData>(5);
            this.m_DamageSkills = new List<SkillData>(5);
            this.m_SupportSkills = new List<SkillData>(5);
            this.m_CureConditionSkills = new List<SkillData>(5);
            this.m_FailConditionSkills = new List<SkillData>(5);
            this.m_DisableConditionSkills = new List<SkillData>(5);
            this.m_ExeSkills = new List<SkillData>(5);
            return;
        }

        public void Add(Data data)
        {
            this.m_List.Add(data);
            return;
        }

        public void Clear()
        {
            this.m_AllSkills.Clear();
            this.m_UseSkill = null;
            this.m_Action = null;
            this.m_UseSkillNum = 0;
            this.m_UseSkillLists.Clear();
            this.m_ForceSkillList.Clear();
            this.m_HealSkills.Clear();
            this.m_DamageSkills.Clear();
            this.m_SupportSkills.Clear();
            this.m_CureConditionSkills.Clear();
            this.m_FailConditionSkills.Clear();
            this.m_DisableConditionSkills.Clear();
            this.m_ExeSkills.Clear();
            this.Reset();
            this.m_IsNoExecActionSkill = 0;
            return;
        }

        public Data Get(SkillData skill)
        {
            <Get>c__AnonStorey1CB storeycb;
            storeycb = new <Get>c__AnonStorey1CB();
            storeycb.skill = skill;
            if (storeycb.skill == null)
            {
                goto Label_0030;
            }
            return this.m_List.Find(new Predicate<Data>(storeycb.<>m__72));
        Label_0030:
            return null;
        }

        public AIAction GetAction()
        {
            return this.m_Action;
        }

        public static unsafe int GetHash(IntVector2 pos)
        {
            return GetHash(&pos.x, &pos.y);
        }

        public static int GetHash(int x, int y)
        {
            return ((y * 0x400) + x);
        }

        public List<SkillData> GetSkillList()
        {
            return this.GetSkillList(this.m_UseSkillNum);
        }

        public List<SkillData> GetSkillList(int n)
        {
            if (n >= this.m_UseSkillLists.Count)
            {
                goto Label_0034;
            }
            if (this.m_UseSkillLists[n] == null)
            {
                goto Label_004C;
            }
            return this.m_UseSkillLists[n];
            goto Label_004C;
        Label_0034:
            if (n != this.m_UseSkillLists.Count)
            {
                goto Label_004C;
            }
            return this.m_ForceSkillList;
        Label_004C:
            return null;
        }

        public BattleCore.SkillResult GetUseSkill()
        {
            return this.m_UseSkill;
        }

        public bool HasUseSkill()
        {
            return ((this.m_UseSkill == null) == 0);
        }

        public void NextAction(BattleCore.SkillResult result)
        {
            this.NextAction(0, result);
            return;
        }

        public void NextAction(bool isMove)
        {
            this.NextAction(isMove, null);
            return;
        }

        public void NextAction(bool isMove, BattleCore.SkillResult result)
        {
            bool flag;
            SkillData data;
            AIAction action;
            if (this.m_Action != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            flag = this.m_Action.notBlock;
            if (isMove == null)
            {
                goto Label_0046;
            }
            flag = (this.m_Action.type != 2) ? flag : 1;
            goto Label_012D;
        Label_0046:
            if ((result == null) || (result.skill == null))
            {
                goto Label_0110;
            }
            if (string.IsNullOrEmpty(this.m_Action.skill) != null)
            {
                goto Label_00A3;
            }
            flag = ((this.m_Action.skill == result.skill.SkillID) == null) ? flag : 1;
            goto Label_010B;
        Label_00A3:
            if (this.m_Action.type != 1)
            {
                goto Label_00F3;
            }
            data = this.owner.GetAttackSkill();
            if (data == null)
            {
                goto Label_012D;
            }
            flag = ((data.SkillID == result.skill.SkillID) == null) ? flag : 1;
            goto Label_010B;
        Label_00F3:
            if (this.m_Action.type != 2)
            {
                goto Label_012D;
            }
            flag = 1;
        Label_010B:
            goto Label_012D;
        Label_0110:
            flag = (this.m_Action.type != null) ? flag : 1;
        Label_012D:
            if (this.m_Action.nextTurnAct == null)
            {
                goto Label_01A0;
            }
            if (this.m_IsNoExecActionSkill == null)
            {
                goto Label_01A0;
            }
            flag = this.m_Action.nextTurnAct == 1;
            if (this.m_Action.nextTurnAct != 3)
            {
                goto Label_01A0;
            }
            if (this.m_Action.turnActIdx <= 0)
            {
                goto Label_01A0;
            }
            if (this.m_Owner.SetAIAction(this.m_Action.turnActIdx - 1) == null)
            {
                goto Label_01A0;
            }
            this.m_Action = null;
            return;
        Label_01A0:
            if (flag == null)
            {
                goto Label_01B8;
            }
            this.m_Action = null;
            this.m_Owner.NextAIAction();
        Label_01B8:
            return;
        }

        public void NextSkillList()
        {
            this.m_UseSkillNum += 1;
            return;
        }

        public void Reset()
        {
            this.m_List.Clear();
            this.m_AttackHeight = 0;
            return;
        }

        public void SetAction(AIAction action)
        {
            this.m_Action = action;
            return;
        }

        public AIAction SetAction(int index)
        {
            AIAction action;
            if (this.m_Owner != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            action = this.m_Owner.SetAIAction(index);
            if (action == null)
            {
                goto Label_0027;
            }
            this.m_Action = action;
        Label_0027:
            return action;
        }

        public void SetUseSkill(BattleCore.SkillResult result)
        {
            this.m_UseSkill = result;
            return;
        }

        public Data[] ToArray()
        {
            return this.m_List.ToArray();
        }

        public Unit owner
        {
            get
            {
                return this.m_Owner;
            }
            set
            {
                this.m_Owner = value;
                return;
            }
        }

        public uint[] skillSeed
        {
            get
            {
                return this.m_SkillSeed;
            }
            set
            {
                int num;
                num = 0;
                goto Label_0024;
            Label_0007:
                if (num >= ((int) this.m_SkillSeed.Length))
                {
                    goto Label_0020;
                }
                this.m_SkillSeed[num] = value[num];
            Label_0020:
                num += 1;
            Label_0024:
                if (num < ((int) value.Length))
                {
                    goto Label_0007;
                }
                return;
            }
        }

        public List<Data> list
        {
            get
            {
                return this.m_List;
            }
        }

        public bool isNoExecActionSkill
        {
            get
            {
                return this.m_IsNoExecActionSkill;
            }
            set
            {
                this.m_IsNoExecActionSkill = value;
                return;
            }
        }

        public List<SkillData> allSkills
        {
            get
            {
                return this.m_AllSkills;
            }
        }

        public int attackHeight
        {
            get
            {
                return this.m_AttackHeight;
            }
            set
            {
                this.m_AttackHeight = value;
                return;
            }
        }

        public int useSkillNum
        {
            get
            {
                return this.m_UseSkillNum;
            }
            set
            {
                this.m_UseSkillNum = value;
                return;
            }
        }

        public List<List<SkillData>> useSkillLists
        {
            get
            {
                return this.m_UseSkillLists;
            }
        }

        public List<SkillData> forceSkillList
        {
            get
            {
                return this.m_ForceSkillList;
            }
        }

        public List<SkillData> healSkills
        {
            get
            {
                return this.m_HealSkills;
            }
        }

        public List<SkillData> damageSkills
        {
            get
            {
                return this.m_DamageSkills;
            }
        }

        public List<SkillData> supportSkills
        {
            get
            {
                return this.m_SupportSkills;
            }
        }

        public List<SkillData> cureConditionSkills
        {
            get
            {
                return this.m_CureConditionSkills;
            }
        }

        public List<SkillData> failConditionSkills
        {
            get
            {
                return this.m_FailConditionSkills;
            }
        }

        public List<SkillData> disableConditionSkills
        {
            get
            {
                return this.m_DisableConditionSkills;
            }
        }

        public List<SkillData> exeSkills
        {
            get
            {
                return this.m_ExeSkills;
            }
        }

        [CompilerGenerated]
        private sealed class <Get>c__AnonStorey1CB
        {
            internal SkillData skill;

            public <Get>c__AnonStorey1CB()
            {
                base..ctor();
                return;
            }

            internal bool <>m__72(SkillMap.Data prop)
            {
                return ((prop.skill == null) ? 0 : (prop.skill.SkillID == this.skill.SkillID));
            }
        }

        public class Data
        {
            public SkillData skill;
            public Dictionary<int, SkillMap.Target> targets;

            public Data(SkillData _skill)
            {
                base..ctor();
                this.skill = _skill;
                this.targets = new Dictionary<int, SkillMap.Target>();
                return;
            }

            public void Add(SkillMap.Target range)
            {
                this.targets[SkillMap.GetHash(range.pos)] = range;
                return;
            }

            public unsafe SkillMap.Target Get(int x, int y)
            {
                SkillMap.Target target;
                target = null;
                this.targets.TryGetValue(SkillMap.GetHash(x, y), &target);
                return target;
            }

            public unsafe SkillMap.Score[] GetScores(int x, int y)
            {
                List<SkillMap.Score> list;
                KeyValuePair<int, SkillMap.Target> pair;
                Dictionary<int, SkillMap.Target>.Enumerator enumerator;
                SkillMap.Target target;
                SkillMap.Score score;
                list = new List<SkillMap.Score>();
                enumerator = this.targets.GetEnumerator();
            Label_0012:
                try
                {
                    goto Label_0040;
                Label_0017:
                    pair = &enumerator.Current;
                    score = &pair.Value.Get(x, y);
                    if (score == null)
                    {
                        goto Label_0040;
                    }
                    list.Add(score);
                Label_0040:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0017;
                    }
                    goto Label_005D;
                }
                finally
                {
                Label_0051:
                    ((Dictionary<int, SkillMap.Target>.Enumerator) enumerator).Dispose();
                }
            Label_005D:
                return list.ToArray();
            }

            public unsafe bool IsAttackRange(int x, int y)
            {
                KeyValuePair<int, SkillMap.Target> pair;
                Dictionary<int, SkillMap.Target>.Enumerator enumerator;
                SkillMap.Target target;
                bool flag;
                enumerator = this.targets.GetEnumerator();
            Label_000C:
                try
                {
                    goto Label_0035;
                Label_0011:
                    pair = &enumerator.Current;
                    if (&pair.Value.IsAttackRange(x, y) == null)
                    {
                        goto Label_0035;
                    }
                    flag = 1;
                    goto Label_0054;
                Label_0035:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0011;
                    }
                    goto Label_0052;
                }
                finally
                {
                Label_0046:
                    ((Dictionary<int, SkillMap.Target>.Enumerator) enumerator).Dispose();
                }
            Label_0052:
                return 0;
            Label_0054:
                return flag;
            }

            public unsafe bool IsRange(int x, int y)
            {
                KeyValuePair<int, SkillMap.Target> pair;
                Dictionary<int, SkillMap.Target>.Enumerator enumerator;
                SkillMap.Target target;
                bool flag;
                enumerator = this.targets.GetEnumerator();
            Label_000C:
                try
                {
                    goto Label_0035;
                Label_0011:
                    pair = &enumerator.Current;
                    if (&pair.Value.IsRange(x, y) == null)
                    {
                        goto Label_0035;
                    }
                    flag = 1;
                    goto Label_0054;
                Label_0035:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0011;
                    }
                    goto Label_0052;
                }
                finally
                {
                Label_0046:
                    ((Dictionary<int, SkillMap.Target>.Enumerator) enumerator).Dispose();
                }
            Label_0052:
                return 0;
            Label_0054:
                return flag;
            }
        }

        public class Score
        {
            public int priority;
            public IntVector2 pos;
            public SkillRange range;
            public LogSkill log;

            public unsafe Score(int _x, int _y, int _w, int _h)
            {
                base..ctor();
                this.priority = 0;
                &this.pos.x = _x;
                &this.pos.y = _y;
                this.range = new SkillRange(_w, _h);
                return;
            }

            public unsafe bool IsAttackRange(int x, int y)
            {
                return &this.range.Get(x, y);
            }
        }

        public class Target
        {
            public IntVector2 pos;
            public Dictionary<int, SkillMap.Score> scores;

            public Target()
            {
                base..ctor();
                return;
            }

            public unsafe void Add(SkillMap.Score score)
            {
                this.scores[SkillMap.GetHash(&score.pos.x, &score.pos.y)] = score;
                return;
            }

            public unsafe SkillMap.Score Get(int x, int y)
            {
                SkillMap.Score score;
                score = null;
                this.scores.TryGetValue(SkillMap.GetHash(x, y), &score);
                return score;
            }

            public unsafe bool IsAttackRange(int x, int y)
            {
                KeyValuePair<int, SkillMap.Score> pair;
                Dictionary<int, SkillMap.Score>.Enumerator enumerator;
                SkillMap.Score score;
                bool flag;
                enumerator = this.scores.GetEnumerator();
            Label_000C:
                try
                {
                    goto Label_0035;
                Label_0011:
                    pair = &enumerator.Current;
                    if (&pair.Value.IsAttackRange(x, y) == null)
                    {
                        goto Label_0035;
                    }
                    flag = 1;
                    goto Label_0054;
                Label_0035:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0011;
                    }
                    goto Label_0052;
                }
                finally
                {
                Label_0046:
                    ((Dictionary<int, SkillMap.Score>.Enumerator) enumerator).Dispose();
                }
            Label_0052:
                return 0;
            Label_0054:
                return flag;
            }

            public bool IsRange(int x, int y)
            {
                return this.scores.ContainsKey(SkillMap.GetHash(x, y));
            }
        }
    }
}

