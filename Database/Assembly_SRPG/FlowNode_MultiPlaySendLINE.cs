namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    [NodeType("Multi/MultiPlaySendLINE", 0x7fe5), Pin(0, "Send", 0, 0), Pin(1, "Send Done", 1, 0), Pin(100, "Read", 0, 0), Pin(0x65, "ToCreate", 1, 0), Pin(0x66, "ToJoin", 1, 0), Pin(200, "SendVersus", 0, 0)]
    public class FlowNode_MultiPlaySendLINE : FlowNode
    {
        public FlowNode_MultiPlaySendLINE()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            string str2;
            byte[] buffer;
            string str3;
            string str4;
            byte[] buffer2;
            if (pinID != null)
            {
                goto Label_00C0;
            }
            str = LocalizedText.Get("sys.MP_LINE_TEXT");
            str2 = (((string.Empty + "iname=" + GlobalVars.SelectedQuestID) + "&type=" + ((int) GlobalVars.SelectedMultiPlayRoomType)) + "&creatorFUID=" + JSON_MyPhotonRoomParam.GetMyCreatorFUID()) + "&roomid=" + ((int) GlobalVars.SelectedMultiPlayRoomID);
            buffer = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, str2, 0);
            str = string.Format(str, WWW.EscapeURL(Convert.ToBase64String(buffer)));
            DebugUtility.Log("LINE:" + str);
            Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(str, Encoding.UTF8));
            base.ActivateOutputLinks(1);
            goto Label_01D9;
        Label_00C0:
            if (pinID != 100)
            {
                goto Label_00FD;
            }
            if (JSON_MyPhotonRoomParam.GetMyCreatorFUID().Equals(FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID) == null)
            {
                goto Label_00EF;
            }
            base.ActivateOutputLinks(0x65);
            goto Label_00F8;
        Label_00EF:
            base.ActivateOutputLinks(0x66);
        Label_00F8:
            goto Label_01D9;
        Label_00FD:
            if (pinID != 200)
            {
                goto Label_01D9;
            }
            str3 = LocalizedText.Get("sys.MP_LINE_VERSUS_TEXT");
            str4 = (((string.Empty + "iname=" + GlobalVars.SelectedQuestID) + "&type=" + ((int) GlobalVars.SelectedMultiPlayRoomType)) + "&creatorFUID=" + JSON_MyPhotonRoomParam.GetMyCreatorFUID()) + "&roomid=" + ((int) GlobalVars.SelectedMultiPlayRoomID);
            buffer2 = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, str4, 0);
            str3 = string.Format(str3, WWW.EscapeURL(Convert.ToBase64String(buffer2)), (int) GlobalVars.SelectedMultiPlayRoomID);
            DebugUtility.Log("LINE:" + str3);
            Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(str3, Encoding.UTF8));
            GlobalVars.VersusRoomReuse = 1;
            base.ActivateOutputLinks(1);
        Label_01D9:
            return;
        }
    }
}

