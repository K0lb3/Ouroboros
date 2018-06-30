namespace SRPG
{
    using System;

    public static class AIUtility
    {
        public static bool IsFailCondition(EUnitCondition condition)
        {
            if (condition == 0x80000L)
            {
                goto Label_0048;
            }
            if (condition == 0x400000L)
            {
                goto Label_0048;
            }
            if (condition == 0x800000L)
            {
                goto Label_0048;
            }
            if (condition == 0x20000L)
            {
                goto Label_0048;
            }
            if (condition == 0x8000L)
            {
                goto Label_0048;
            }
            if (condition != 0x2000L)
            {
                goto Label_004A;
            }
        Label_0048:
            return 0;
        Label_004A:
            return 1;
        }

        public static bool IsFailCondition(Unit self, Unit target, EUnitCondition condition)
        {
            bool flag;
            flag = SceneBattle.Instance.Battle.CheckEnemySide(self, target);
            if (IsFailCondition(condition) == null)
            {
                goto Label_001F;
            }
            return flag;
        Label_001F:
            return (flag == 0);
        }
    }
}

