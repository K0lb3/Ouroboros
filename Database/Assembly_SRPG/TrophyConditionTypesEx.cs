namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;

    [Extension]
    public static class TrophyConditionTypesEx
    {
        [Extension]
        public static bool IsExtraClear(TrophyConditionTypes type)
        {
            TrophyConditionTypes types;
            types = type;
            switch ((types - 0x3f))
            {
                case 0:
                    goto Label_0040;

                case 1:
                    goto Label_0040;

                case 2:
                    goto Label_0040;

                case 3:
                    goto Label_0040;

                case 4:
                    goto Label_0040;

                case 5:
                    goto Label_0040;

                case 6:
                    goto Label_0040;

                case 7:
                    goto Label_0040;

                case 8:
                    goto Label_0040;

                case 9:
                    goto Label_0040;

                case 10:
                    goto Label_0040;

                case 11:
                    goto Label_0040;
            }
            goto Label_0042;
        Label_0040:
            return 1;
        Label_0042:
            return 0;
        }
    }
}

