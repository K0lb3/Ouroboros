// Decompiled with JetBrains decompiler
// Type: SRPG.VersusURLCopy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using DeviceKit;
using System;
using UnityEngine;

namespace SRPG
{
  public class VersusURLCopy : MonoBehaviour
  {
    public VersusURLCopy()
    {
      base.\u002Ector();
    }

    public void OnClickURL()
    {
      string format = LocalizedText.Get("sys.MP_LINE_VERSUS_TEXT");
      string msg = string.Empty + "iname=" + GlobalVars.SelectedQuestID + "&type=" + (object) GlobalVars.SelectedMultiPlayRoomType + "&creatorFUID=" + JSON_MyPhotonRoomParam.GetMyCreatorFUID() + "&roomid=" + (object) GlobalVars.SelectedMultiPlayRoomID;
      byte[] inArray = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, msg, false);
      string text = string.Format(format, (object) WWW.EscapeURL(Convert.ToBase64String(inArray)), (object) GlobalVars.SelectedMultiPlayRoomID);
      DebugUtility.Log("LINE:" + text);
      App.SetClipboard(text);
      GlobalVars.VersusRoomReuse = true;
    }
  }
}
