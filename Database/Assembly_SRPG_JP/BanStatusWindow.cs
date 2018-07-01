// Decompiled with JetBrains decompiler
// Type: SRPG.BanStatusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Title, (UnityEngine.Object) null))
        this.Title.set_text("ログイン制限");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Message, (UnityEngine.Object) null))
        this.Message.set_text("再びログイン可能になるのは以下の日時です。");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitDate, (UnityEngine.Object) null))
      {
        int banStatus = GlobalVars.BanStatus;
        if (banStatus == 1)
          this.LimitDate.set_text("無期限ログイン不可");
        else
          this.LimitDate.set_text(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double) banStatus).ToString());
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CustomerID, (UnityEngine.Object) null))
        return;
      this.CustomerID.set_text(GlobalVars.CustomerID);
    }
  }
}
