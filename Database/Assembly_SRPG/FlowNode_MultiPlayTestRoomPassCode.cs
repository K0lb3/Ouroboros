namespace SRPG
{
    using System;

    [Pin(2, "NG", 1, 0), Pin(1, "OK", 1, 0), Pin(0, "Test", 0, 0), NodeType("Multi/MultiPlayTestRoomPassCode", 0x7fe5)]
    public class FlowNode_MultiPlayTestRoomPassCode : FlowNode
    {
        public FlowNode_MultiPlayTestRoomPassCode()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string[] textArray1;
            string str;
            if (pinID != null)
            {
                goto Label_008E;
            }
            if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomPassCodeHash) == null)
            {
                goto Label_0022;
            }
            base.ActivateOutputLinks(1);
            goto Label_008E;
        Label_0022:
            str = MultiPlayAPIRoom.CalcHash(GlobalVars.EditMultiPlayRoomPassCode);
            textArray1 = new string[] { "CheckPass...:", GlobalVars.EditMultiPlayRoomPassCode, " > ", str, " vs ", GlobalVars.SelectedMultiPlayRoomPassCodeHash };
            DebugUtility.Log(string.Concat(textArray1));
            if (GlobalVars.SelectedMultiPlayRoomPassCodeHash.Equals(str) == null)
            {
                goto Label_0086;
            }
            base.ActivateOutputLinks(1);
            goto Label_008E;
        Label_0086:
            base.ActivateOutputLinks(2);
        Label_008E:
            return;
        }
    }
}

