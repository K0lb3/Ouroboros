// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlaySendLINE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(102, "ToJoin", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(200, "SendVersus", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiPlaySendLINE", 32741)]
  [FlowNode.Pin(0, "Send", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Send Done", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(100, "Read", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "ToCreate", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlaySendLINE : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          string format1 = LocalizedText.Get("sys.MP_LINE_TEXT");
          string msg1 = string.Empty + "iname=" + GlobalVars.SelectedQuestID + "&type=" + (object) GlobalVars.SelectedMultiPlayRoomType + "&creatorFUID=" + JSON_MyPhotonRoomParam.GetMyCreatorFUID() + "&roomid=" + (object) GlobalVars.SelectedMultiPlayRoomID;
          byte[] inArray1 = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, msg1, false);
          string str1 = string.Format(format1, (object) WWW.EscapeURL(Convert.ToBase64String(inArray1)));
          DebugUtility.Log("LINE:" + str1);
          Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(str1, Encoding.UTF8));
          this.ActivateOutputLinks(1);
          break;
        case 100:
          if (JSON_MyPhotonRoomParam.GetMyCreatorFUID().Equals(FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID))
          {
            this.ActivateOutputLinks(101);
            break;
          }
          this.ActivateOutputLinks(102);
          break;
        case 200:
          string format2 = LocalizedText.Get("sys.MP_LINE_VERSUS_TEXT");
          string msg2 = string.Empty + "iname=" + GlobalVars.SelectedQuestID + "&type=" + (object) GlobalVars.SelectedMultiPlayRoomType + "&creatorFUID=" + JSON_MyPhotonRoomParam.GetMyCreatorFUID() + "&roomid=" + (object) GlobalVars.SelectedMultiPlayRoomID;
          byte[] inArray2 = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, msg2, false);
          string str2 = string.Format(format2, (object) WWW.EscapeURL(Convert.ToBase64String(inArray2)), (object) GlobalVars.SelectedMultiPlayRoomID);
          DebugUtility.Log("LINE:" + str2);
          Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(str2, Encoding.UTF8));
          GlobalVars.VersusRoomReuse = true;
          this.ActivateOutputLinks(1);
          break;
      }
    }
  }
}
