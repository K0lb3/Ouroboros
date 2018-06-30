namespace SRPG
{
    using Gsc.Auth;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [NodeType("GscSystem/GscChainData", 0x7fe5), Pin(100, "Requested", 1, 100), Pin(0x16, "ChainLocked", 1, 0x16), Pin(0x15, "ChainEmail", 1, 0x15), Pin(20, "ChainMissing", 1, 20), Pin(14, "RegistDuplicate", 1, 14), Pin(13, "RegistPassword", 1, 13), Pin(12, "RegistEmail", 1, 12), Pin(11, "Failed", 1, 11), Pin(10, "Success", 1, 10), Pin(1, "AddUser", 0, 1), Pin(0, "Register", 0, 0)]
    public class FlowNode_GscChainData : FlowNode
    {
        private const int PIN_REGISTER = 0;
        private const int PIN_ADD_USER = 1;
        private const int PIN_SUCCESS = 10;
        private const int PIN_FAILED = 11;
        private const int PIN_REG_EMAIL = 12;
        private const int PIN_REG_PASSWORD = 13;
        private const int PIN_REG_DUPLICATE = 14;
        private const int PIN_CHN_MISSING = 20;
        private const int PIN_CHN_EMAIL = 0x15;
        private const int PIN_CHN_LOCKED = 0x16;
        private const int PIN_CHN_IDPASS = 0x17;
        private const int PIN_REQUESTED = 100;
        [SerializeField]
        private Text okyakusama_code_txt;
        [SerializeField]
        private Text pass_txt;

        public FlowNode_GscChainData()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <AddUserResponse>m__19D(GameObject obj)
        {
            base.ActivateOutputLinks(0x16);
            return;
        }

        private unsafe void AddUserResponse(AddDeviceWithEmailAddressAndPasswordResult res)
        {
            object[] objArray2;
            object[] objArray1;
            DateTime time;
            string str;
            string str2;
            AddDeviceWithEmailAddressAndPasswordResultCode code;
            DateTime time2;
            switch (&res.ResultCode)
            {
                case 0:
                    goto Label_0023;

                case 1:
                    goto Label_0036;

                case 2:
                    goto Label_0044;

                case 3:
                    goto Label_0052;
            }
            goto Label_00FA;
        Label_0023:
            GameUtility.ClearPreferences();
            base.ActivateOutputLinks(10);
            goto Label_0108;
        Label_0036:
            base.ActivateOutputLinks(20);
            goto Label_0108;
        Label_0044:
            base.ActivateOutputLinks(0x15);
            goto Label_0108;
        Label_0052:
            time = &TimeManager.ServerTime.AddSeconds((double) &res.LockedExpiresIn);
            objArray1 = new object[] { (int) &time.Year, (int) &time.Month, (int) &time.Day, (int) &time.Hour, (int) &time.Minute };
            str = string.Format("{0}/{1:D2}/{2:D2} {3:D2}:{4:D2}", objArray1);
            objArray2 = new object[] { str };
            str2 = LocalizedText.Get("sys.CHAINDATA_LOCKED", objArray2);
            UIUtility.NegativeSystemMessage(string.Empty, str2, new UIUtility.DialogResultEvent(this.<AddUserResponse>m__19D), null, 1, -1);
            goto Label_0108;
        Label_00FA:
            base.ActivateOutputLinks(11);
        Label_0108:
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            string str2;
            str = string.Empty;
            str2 = string.Empty;
            if ((this.okyakusama_code_txt != null) == null)
            {
                goto Label_0029;
            }
            str = this.okyakusama_code_txt.get_text();
        Label_0029:
            if ((this.pass_txt != null) == null)
            {
                goto Label_0046;
            }
            str2 = this.pass_txt.get_text();
        Label_0046:
            if (pinID != null)
            {
                goto Label_0074;
            }
            Session.DefaultSession.RegisterEmailAddressAndPassword(str, str2, 1, new Action<RegisterEmailAddressAndPasswordResult>(this.RegistResponse));
            base.ActivateOutputLinks(100);
            goto Label_009D;
        Label_0074:
            if (pinID != 1)
            {
                goto Label_009D;
            }
            Session.DefaultSession.AddDeviceWithEmailAddressAndPassword(str, str2, new Action<AddDeviceWithEmailAddressAndPasswordResult>(this.AddUserResponse));
            base.ActivateOutputLinks(100);
        Label_009D:
            return;
        }

        private unsafe void RegistResponse(RegisterEmailAddressAndPasswordResult res)
        {
            RegisterEmailAddressAndPasswordResultCode code;
            switch (&res.ResultCode)
            {
                case 0:
                    goto Label_0023;

                case 1:
                    goto Label_0031;

                case 2:
                    goto Label_003F;

                case 3:
                    goto Label_004D;
            }
            goto Label_005B;
        Label_0023:
            base.ActivateOutputLinks(10);
            goto Label_0069;
        Label_0031:
            base.ActivateOutputLinks(12);
            goto Label_0069;
        Label_003F:
            base.ActivateOutputLinks(13);
            goto Label_0069;
        Label_004D:
            base.ActivateOutputLinks(14);
            goto Label_0069;
        Label_005B:
            base.ActivateOutputLinks(11);
        Label_0069:
            return;
        }
    }
}

