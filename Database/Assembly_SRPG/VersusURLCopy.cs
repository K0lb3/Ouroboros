namespace SRPG
{
    using System;
    using UnityEngine;

    public class VersusURLCopy : MonoBehaviour
    {
        public VersusURLCopy()
        {
            base..ctor();
            return;
        }

        public void OnClickURL()
        {
            string str;
            string str2;
            byte[] buffer;
            str = LocalizedText.Get("sys.MP_LINE_VERSUS_TEXT");
            str2 = (((string.Empty + "iname=" + GlobalVars.SelectedQuestID) + "&type=" + ((int) GlobalVars.SelectedMultiPlayRoomType)) + "&creatorFUID=" + JSON_MyPhotonRoomParam.GetMyCreatorFUID()) + "&roomid=" + ((int) GlobalVars.SelectedMultiPlayRoomID);
            buffer = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, str2, 0);
            str = string.Format(str, WWW.EscapeURL(Convert.ToBase64String(buffer)), (int) GlobalVars.SelectedMultiPlayRoomID);
            DebugUtility.Log("LINE:" + str);
            GUIUtility.set_systemCopyBuffer(str);
            GlobalVars.VersusRoomReuse = 1;
            return;
        }
    }
}

