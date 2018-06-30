namespace SRPG
{
    using System;

    [Pin(2, "Result", 1, 2), NodeType("System/SetBoolPlayerPrefs", 0x7fe5), Pin(1, "SetTrue", 0, 1), Pin(0, "SetFalse", 0, 0)]
    public class FlowNode_SetBoolPlayerPrefs : FlowNode
    {
        private const int SET_FALSE = 0;
        private const int SET_TRUE = 1;
        public string Name;

        public FlowNode_SetBoolPlayerPrefs()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            bool flag;
            int num;
            flag = 0;
            num = pinID;
            if (num == null)
            {
                goto Label_0016;
            }
            if (num == 1)
            {
                goto Label_0029;
            }
            goto Label_003C;
        Label_0016:
            flag = PlayerPrefsUtility.SetInt(this.Name, 0, 1);
            goto Label_003C;
        Label_0029:
            flag = PlayerPrefsUtility.SetInt(this.Name, 1, 1);
        Label_003C:
            if (flag != null)
            {
                goto Label_004C;
            }
            DebugUtility.Log("PlayerPrefsの設定に失敗しました");
        Label_004C:
            return;
        }
    }
}

