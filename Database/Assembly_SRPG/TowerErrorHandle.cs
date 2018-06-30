namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class TowerErrorHandle
    {
        public TowerErrorHandle()
        {
            base..ctor();
            return;
        }

        public static bool Error(FlowNode_Network check)
        {
            Network.EErrCode code;
            if (Network.IsError != null)
            {
                goto Label_000C;
            }
            return 0;
        Label_000C:
            code = Network.ErrCode;
            switch ((code - 0x2031))
            {
                case 0:
                    goto Label_00BB;

                case 1:
                    goto Label_009A;

                case 2:
                    goto Label_00DC;

                case 3:
                    goto Label_009A;
            }
            switch ((code - 0x2009))
            {
                case 0:
                    goto Label_009A;

                case 1:
                    goto Label_00BB;

                case 2:
                    goto Label_00BB;
            }
            switch ((code - 0x2013))
            {
                case 0:
                    goto Label_009A;

                case 1:
                    goto Label_009A;

                case 2:
                    goto Label_00BB;
            }
            if (code == 0x201d)
            {
                goto Label_009A;
            }
            if (code == 0x201e)
            {
                goto Label_00BB;
            }
            if (code == 0x2027)
            {
                goto Label_00BB;
            }
            if (code == 0x2028)
            {
                goto Label_009A;
            }
            if (code == 0x2328)
            {
                goto Label_00BB;
            }
            goto Label_00FD;
        Label_009A:
            if ((check == null) == null)
            {
                goto Label_00B0;
            }
            FlowNode_Network.Failed();
            goto Label_00B6;
        Label_00B0:
            check.OnFailed();
        Label_00B6:
            goto Label_011E;
        Label_00BB:
            if ((check == null) == null)
            {
                goto Label_00D1;
            }
            FlowNode_Network.Back();
            goto Label_00D7;
        Label_00D1:
            check.OnBack();
        Label_00D7:
            goto Label_011E;
        Label_00DC:
            if ((check == null) == null)
            {
                goto Label_00F2;
            }
            FlowNode_Network.Retry();
            goto Label_00F8;
        Label_00F2:
            check.OnRetry();
        Label_00F8:
            goto Label_011E;
        Label_00FD:
            if ((check == null) == null)
            {
                goto Label_0113;
            }
            FlowNode_Network.Failed();
            goto Label_0119;
        Label_0113:
            check.OnFailed();
        Label_0119:;
        Label_011E:
            return 1;
        }
    }
}

