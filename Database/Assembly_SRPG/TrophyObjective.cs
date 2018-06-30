namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class TrophyObjective
    {
        public TrophyParam Param;
        public int index;
        public TrophyConditionTypes type;
        public List<string> sval;
        public int ival;

        public TrophyObjective()
        {
            base..ctor();
            return;
        }

        public string sval_base
        {
            get
            {
                return (((this.sval == null) || (0 >= this.sval.Count)) ? string.Empty : this.sval[0]);
            }
        }

        public int RequiredCount
        {
            get
            {
                TrophyConditionTypes types;
                types = this.type;
                switch ((types - 0x4e))
                {
                    case 0:
                        goto Label_010D;

                    case 1:
                        goto Label_006C;

                    case 2:
                        goto Label_010D;

                    case 3:
                        goto Label_010D;

                    case 4:
                        goto Label_006C;

                    case 5:
                        goto Label_006C;

                    case 6:
                        goto Label_006C;

                    case 7:
                        goto Label_006C;

                    case 8:
                        goto Label_006C;

                    case 9:
                        goto Label_006C;

                    case 10:
                        goto Label_006C;

                    case 11:
                        goto Label_006C;

                    case 12:
                        goto Label_006C;

                    case 13:
                        goto Label_010D;

                    case 14:
                        goto Label_010F;

                    case 15:
                        goto Label_010F;

                    case 0x10:
                        goto Label_010F;

                    case 0x11:
                        goto Label_010F;

                    case 0x12:
                        goto Label_010F;

                    case 0x13:
                        goto Label_010F;

                    case 20:
                        goto Label_010F;

                    case 0x15:
                        goto Label_006C;

                    case 0x16:
                        goto Label_010D;
                }
            Label_006C:
                switch ((types - 0x21))
                {
                    case 0:
                        goto Label_010D;

                    case 1:
                        goto Label_00A9;

                    case 2:
                        goto Label_00A9;

                    case 3:
                        goto Label_00A9;

                    case 4:
                        goto Label_00A9;

                    case 5:
                        goto Label_00A9;

                    case 6:
                        goto Label_00A9;

                    case 7:
                        goto Label_010D;

                    case 8:
                        goto Label_010D;

                    case 9:
                        goto Label_010D;

                    case 10:
                        goto Label_010D;

                    case 11:
                        goto Label_010D;

                    case 12:
                        goto Label_010D;
                }
            Label_00A9:
                switch ((types - 0x11))
                {
                    case 0:
                        goto Label_010D;

                    case 1:
                        goto Label_010B;

                    case 2:
                        goto Label_00E2;

                    case 3:
                        goto Label_010D;

                    case 4:
                        goto Label_010D;

                    case 5:
                        goto Label_010D;

                    case 6:
                        goto Label_00E2;

                    case 7:
                        goto Label_00E2;

                    case 8:
                        goto Label_010D;

                    case 9:
                        goto Label_00E2;

                    case 10:
                        goto Label_00E2;

                    case 11:
                        goto Label_010D;
                }
            Label_00E2:
                switch ((types - 0x3a))
                {
                    case 0:
                        goto Label_010D;

                    case 1:
                        goto Label_010D;

                    case 2:
                        goto Label_00FF;

                    case 3:
                        goto Label_010D;

                    case 4:
                        goto Label_010D;
                }
            Label_00FF:
                if (types == 4)
                {
                    goto Label_010D;
                }
                goto Label_0128;
            Label_010B:
                return 0;
            Label_010D:
                return 1;
            Label_010F:
                if (string.IsNullOrEmpty(this.sval_base) == null)
                {
                    goto Label_0126;
                }
                return this.ival;
            Label_0126:
                return 1;
            Label_0128:
                return this.ival;
            }
        }
    }
}

