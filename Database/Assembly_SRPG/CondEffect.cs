namespace SRPG
{
    using System;

    public class CondEffect
    {
        public CondEffectParam param;
        public OInt turn;
        public OInt rate;
        public OInt value;

        public CondEffect()
        {
            base..ctor();
            return;
        }

        public bool CheckEnableCondTarget(Unit target)
        {
            bool flag;
            if (this.param != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            flag = 1;
            if (this.param.sex == null)
            {
                goto Label_003A;
            }
            flag &= this.param.sex == target.UnitParam.sex;
        Label_003A:
            if (this.param.elem == null)
            {
                goto Label_0060;
            }
            flag &= this.param.elem == target.Element;
        Label_0060:
            if (string.IsNullOrEmpty(this.param.job) != null)
            {
                goto Label_00A3;
            }
            if (target.Job == null)
            {
                goto Label_00A3;
            }
            flag &= this.param.job == target.Job.Param.origin;
        Label_00A3:
            if (string.IsNullOrEmpty(this.param.buki) != null)
            {
                goto Label_00E6;
            }
            if (target.Job == null)
            {
                goto Label_00E6;
            }
            flag &= this.param.job == target.Job.Param.buki;
        Label_00E6:
            if (string.IsNullOrEmpty(this.param.birth) != null)
            {
                goto Label_011E;
            }
            flag &= this.param.birth == target.UnitParam.birth;
        Label_011E:
            return flag;
        }

        private void Clear()
        {
            this.param = null;
            this.rate = 0;
            this.turn = 0;
            this.value = 0;
            return;
        }

        public bool ContainsCondition(EUnitCondition condition)
        {
            int num;
            if (this.param == null)
            {
                goto Label_001B;
            }
            if (this.param.conditions != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 0;
        Label_001D:
            num = 0;
            goto Label_003D;
        Label_0024:
            if (this.param.conditions[num] != condition)
            {
                goto Label_0039;
            }
            return 1;
        Label_0039:
            num += 1;
        Label_003D:
            if (num < ((int) this.param.conditions.Length))
            {
                goto Label_0024;
            }
            return 0;
        }

        public static CondEffect CreateCondEffect(CondEffectParam param, int rank, int rankcap)
        {
            CondEffect effect;
            if (param == null)
            {
                goto Label_001E;
            }
            if (param.conditions == null)
            {
                goto Label_001E;
            }
            if (((int) param.conditions.Length) != null)
            {
                goto Label_0020;
            }
        Label_001E:
            return null;
        Label_0020:
            effect = new CondEffect();
            effect.param = param;
            effect.UpdateCurrentValues(rank, rankcap);
            return effect;
        }

        private int GetRankValue(int rank, int rankcap, int ini, int max)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = rankcap - 1;
            num2 = rank - 1;
            if (ini != max)
            {
                goto Label_0012;
            }
            return ini;
        Label_0012:
            if (num2 < 1)
            {
                goto Label_0020;
            }
            if (num >= 1)
            {
                goto Label_0022;
            }
        Label_0020:
            return ini;
        Label_0022:
            if (num2 < num)
            {
                goto Label_002C;
            }
            return max;
        Label_002C:
            num3 = ((max - ini) * 100) / num;
            num4 = ini + ((num3 * num2) / 100);
            return num4;
        }

        public void UpdateCurrentValues(int rank, int rankcap)
        {
            if (this.param == null)
            {
                goto Label_002D;
            }
            if (this.param.conditions == null)
            {
                goto Label_002D;
            }
            if (((int) this.param.conditions.Length) != null)
            {
                goto Label_0034;
            }
        Label_002D:
            this.Clear();
            return;
        Label_0034:
            this.rate = this.GetRankValue(rank, rankcap, this.param.rate_ini, this.param.rate_max);
            this.turn = this.GetRankValue(rank, rankcap, this.param.turn_ini, this.param.turn_max);
            this.value = this.GetRankValue(rank, rankcap, this.param.value_ini, this.param.value_max);
            return;
        }

        public bool IsCurse
        {
            get
            {
                return ((this.param == null) ? 0 : ((this.param.curse == 0) == 0));
            }
        }
    }
}

