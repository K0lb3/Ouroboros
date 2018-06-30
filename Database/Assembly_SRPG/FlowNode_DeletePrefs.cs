namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(11, "Failed", 1, 11), Pin(10, "Success", 1, 10), NodeType("System/DeletePrefs", 0x7fe5)]
    public class FlowNode_DeletePrefs : FlowNode
    {
        public PrefsType Type;

        public FlowNode_DeletePrefs()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            bool flag;
            if (pinID != null)
            {
                goto Label_0064;
            }
            if (this.Type == 1)
            {
                goto Label_001E;
            }
            if (this.Type != 3)
            {
                goto Label_0039;
            }
        Label_001E:
            flag = GameUtility.Config_UseAssetBundles.Value;
            PlayerPrefsUtility.DeleteAll();
            GameUtility.Config_UseAssetBundles.Value = flag;
        Label_0039:
            if (this.Type == 2)
            {
                goto Label_0051;
            }
            if (this.Type != 3)
            {
                goto Label_005B;
            }
        Label_0051:
            MonoSingleton<UserInfoManager>.Instance.Delete();
        Label_005B:
            base.ActivateOutputLinks(10);
        Label_0064:
            return;
        }

        public enum PrefsType : byte
        {
            None = 0,
            PlayerPrefs = 1,
            UserInfoManager = 2,
            All = 3
        }
    }
}

