// Decompiled with JetBrains decompiler
// Type: SRPG.VersusURLCopy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
      string str = string.Format(format, (object) WWW.EscapeURL(Convert.ToBase64String(inArray)), (object) GlobalVars.SelectedMultiPlayRoomID);
      DebugUtility.Log("LINE:" + str);
      GUIUtility.set_systemCopyBuffer(str);
      GlobalVars.VersusRoomReuse = true;
    }
  }
}
