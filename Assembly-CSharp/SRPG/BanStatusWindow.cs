// Decompiled with JetBrains decompiler
// Type: SRPG.BanStatusWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BanStatusWindow : MonoBehaviour
  {
    public Text Title;
    public Text LimitDate;
    public Text Message;
    public Text CustomerID;

    public BanStatusWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.Title, (Object) null))
        this.Title.set_text(LocalizedText.Get("sys.BAN_USER_TITLE"));
      if (Object.op_Inequality((Object) this.Message, (Object) null))
        this.Message.set_text(LocalizedText.Get("sys.BAN_USER_MESSAGE"));
      if (Object.op_Inequality((Object) this.LimitDate, (Object) null))
      {
        int banStatus = GlobalVars.BanStatus;
        if (banStatus == 1)
          this.LimitDate.set_text(LocalizedText.Get("sys.BAN_USER_INDEFINITE"));
        else
          this.LimitDate.set_text(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double) banStatus).ToString());
      }
      if (!Object.op_Inequality((Object) this.CustomerID, (Object) null))
        return;
      this.CustomerID.set_text(GlobalVars.CustomerID);
    }
  }
}
