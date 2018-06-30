namespace SRPG
{
    using System;

    public class TrickMap
    {
        private Unit m_Owner;
        private GridMap<Data> m_DataMap;

        public TrickMap()
        {
            base..ctor();
            this.m_Owner = null;
            this.m_DataMap = null;
            return;
        }

        public void Clear()
        {
            if (this.m_DataMap != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.m_DataMap.fill(null);
            return;
        }

        public Data GetData(int x, int y)
        {
            if (this.m_DataMap != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            return this.m_DataMap.get(x, y);
        }

        public void Initialize(int w, int h)
        {
            if (this.m_DataMap != null)
            {
                goto Label_0018;
            }
            this.m_DataMap = new GridMap<Data>(w, h);
        Label_0018:
            if (this.m_DataMap.w != w)
            {
                goto Label_003A;
            }
            if (this.m_DataMap.h == h)
            {
                goto Label_0047;
            }
        Label_003A:
            this.m_DataMap.resize(w, h);
        Label_0047:
            this.m_DataMap.fill(null);
            return;
        }

        public bool IsFailData(int x, int y)
        {
            Data data;
            data = this.GetData(x, y);
            if (data == null)
            {
                goto Label_003E;
            }
            if (data.IsVisual(this.owner) == null)
            {
                goto Label_003E;
            }
            if (data.IsVaild(this.owner) == null)
            {
                goto Label_003E;
            }
            return data.IsFail(this.owner);
        Label_003E:
            return 0;
        }

        public bool IsGoodData(int x, int y)
        {
            Data data;
            data = this.GetData(x, y);
            if (data == null)
            {
                goto Label_0041;
            }
            if (data.IsVisual(this.owner) == null)
            {
                goto Label_0041;
            }
            if (data.IsVaild(this.owner) == null)
            {
                goto Label_0041;
            }
            return (data.IsFail(this.owner) == 0);
        Label_0041:
            return 0;
        }

        public void SetData(Data data)
        {
            if (this.m_DataMap != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.m_DataMap.set(data.x, data.y, data);
            return;
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

        public class Data
        {
            private TrickData data;
            private TrickParam param;

            public Data(TrickData _data)
            {
                base..ctor();
                this.data = _data;
                this.param = _data.TrickParam;
                return;
            }

            public int CalcDamage(Unit unit)
            {
                return this.data.calcDamage(unit);
            }

            public int CalcHeal(Unit unit)
            {
                return this.data.calcHeal(unit);
            }

            public int GetBuffPriority(Unit self)
            {
                int num;
                AIParam param;
                BuffEffect effect;
                int num2;
                int num3;
                num = 0;
                param = self.AI;
                if (this.IsBuffEffect() == null)
                {
                    goto Label_0025;
                }
                if (param == null)
                {
                    goto Label_0025;
                }
                if (param.BuffPriorities != null)
                {
                    goto Label_0027;
                }
            Label_0025:
                return num;
            Label_0027:
                effect = this.data.BuffEffect;
                if (effect == null)
                {
                    goto Label_0098;
                }
                if (effect.targets == null)
                {
                    goto Label_0098;
                }
                num2 = 0;
                goto Label_0087;
            Label_004B:
                num3 = Array.IndexOf<ParamTypes>(param.BuffPriorities, effect.targets[num2].paramType);
                if (num3 == -1)
                {
                    goto Label_0083;
                }
                num = Math.Max(num, ((int) param.BuffPriorities.Length) - num3);
            Label_0083:
                num2 += 1;
            Label_0087:
                if (num2 < effect.targets.Count)
                {
                    goto Label_004B;
                }
            Label_0098:
                return num;
            }

            public bool IsBuffEffect()
            {
                return ((this.data.BuffEffect == null) == 0);
            }

            public bool IsCondEffect()
            {
                return ((this.data.CondEffect == null) == 0);
            }

            public bool IsDamage()
            {
                return (this.param.DamageType == 1);
            }

            public bool IsFail(Unit unit)
            {
                BuffEffect effect;
                int num;
                BuffEffect.BuffTarget target;
                CondEffectParam param;
                int num2;
                int num3;
                if (this.param.DamageType != 1)
                {
                    goto Label_0013;
                }
                return 1;
            Label_0013:
                if (this.data.BuffEffect == null)
                {
                    goto Label_0066;
                }
                effect = this.data.BuffEffect;
                num = 0;
                goto Label_0055;
            Label_0036:
                target = effect.targets[num];
                if (target.buffType != 1)
                {
                    goto Label_0051;
                }
                return 1;
            Label_0051:
                num += 1;
            Label_0055:
                if (num < effect.targets.Count)
                {
                    goto Label_0036;
                }
            Label_0066:
                if (this.data.CondEffect == null)
                {
                    goto Label_012E;
                }
                param = this.data.CondEffect.param;
                if (param.type != 1)
                {
                    goto Label_0095;
                }
                return 0;
            Label_0095:
                if (param.type != 5)
                {
                    goto Label_00D8;
                }
                num2 = 0;
                goto Label_00C4;
            Label_00A9:
                if (AIUtility.IsFailCondition(param.conditions[num2]) != null)
                {
                    goto Label_00BE;
                }
                return 1;
            Label_00BE:
                num2 += 1;
            Label_00C4:
                if (num2 < ((int) param.conditions.Length))
                {
                    goto Label_00A9;
                }
                goto Label_012E;
            Label_00D8:
                if (param.type == 2)
                {
                    goto Label_00FC;
                }
                if (param.type == 4)
                {
                    goto Label_00FC;
                }
                if (param.type != 3)
                {
                    goto Label_012E;
                }
            Label_00FC:
                num3 = 0;
                goto Label_011F;
            Label_0104:
                if (AIUtility.IsFailCondition(param.conditions[num3]) == null)
                {
                    goto Label_0119;
                }
                return 1;
            Label_0119:
                num3 += 1;
            Label_011F:
                if (num3 < ((int) param.conditions.Length))
                {
                    goto Label_0104;
                }
            Label_012E:
                return 0;
            }

            public bool IsHeal()
            {
                return (this.param.DamageType == 2);
            }

            public bool IsVaild(Unit unit)
            {
                bool flag;
                EUnitSide side;
                CondEffect effect;
                ESkillTarget target;
                flag = 0;
                side = 1;
                if (this.data.CreateUnit == null)
                {
                    goto Label_0025;
                }
                side = this.data.CreateUnit.Side;
            Label_0025:
                switch (this.param.Target)
                {
                    case 0:
                        goto Label_0057;

                    case 1:
                        goto Label_0087;

                    case 2:
                        goto Label_009A;

                    case 3:
                        goto Label_0050;

                    case 4:
                        goto Label_006F;
                }
                goto Label_00AD;
            Label_0050:
                flag = 1;
                goto Label_00AD;
            Label_0057:
                if (this.data.CreateUnit != unit)
                {
                    goto Label_00AD;
                }
                flag = 1;
                goto Label_00AD;
            Label_006F:
                if (this.data.CreateUnit == unit)
                {
                    goto Label_00AD;
                }
                flag = 1;
                goto Label_00AD;
            Label_0087:
                if (side != unit.Side)
                {
                    goto Label_00AD;
                }
                flag = 1;
                goto Label_00AD;
            Label_009A:
                if (side == unit.Side)
                {
                    goto Label_00AD;
                }
                flag = 1;
            Label_00AD:
                if (flag == null)
                {
                    goto Label_00DF;
                }
                if (this.data.CondEffect == null)
                {
                    goto Label_00DD;
                }
                if (this.data.CondEffect.CheckEnableCondTarget(unit) != null)
                {
                    goto Label_00DD;
                }
                return 0;
            Label_00DD:
                return 1;
            Label_00DF:
                return 0;
            }

            public bool IsVisual(Unit unit)
            {
                eTrickVisualType type;
                EUnitSide side;
                type = this.param.VisualType;
                if (type != 2)
                {
                    goto Label_0015;
                }
                return 1;
            Label_0015:
                if (type != 1)
                {
                    goto Label_004D;
                }
                side = 1;
                if (this.data.CreateUnit == null)
                {
                    goto Label_003F;
                }
                side = this.data.CreateUnit.Side;
            Label_003F:
                if (side != unit.Side)
                {
                    goto Label_004D;
                }
                return 1;
            Label_004D:
                return 0;
            }

            public int x
            {
                get
                {
                    return this.data.GridX;
                }
            }

            public int y
            {
                get
                {
                    return this.data.GridY;
                }
            }
        }
    }
}

