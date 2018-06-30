namespace SRPG
{
    using DeviceKit;
    using GR;
    using System;

    [Pin(0, "コピー", 0, 0), NodeType("System/CopyFriendID", 0x7fe5), Pin(1, "成功", 1, 1)]
    public class FlowNode_CopyFriendID : FlowNode
    {
        public FlowNode_CopyFriendID()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            PlayerData data;
            string str;
            if (pinID != null)
            {
                goto Label_002D;
            }
            App.SetClipboard(MonoSingleton<GameManager>.Instance.Player.FUID);
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
        Label_002D:
            return;
        }
    }
}

