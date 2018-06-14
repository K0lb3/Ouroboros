// Decompiled with JetBrains decompiler
// Type: SRPG.VersusURLCopy
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using gu3.Device;
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
      string str = string.Format(format, (object) WWW.EscapeURL(Convert.ToBase64String(inArray)));
      DebugUtility.Log("LINE:" + str);
      Application.SetClipboard(str);
      GlobalVars.VersusRoomReuse = true;
    }
  }
}
