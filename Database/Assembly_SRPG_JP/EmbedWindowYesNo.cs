// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedWindowYesNo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class EmbedWindowYesNo : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedWindowYesNo";
    public EmbedWindowYesNo.YesNoWindowEvent Delegate;
    public Text Message;
    public Button ButtonOk;
    public Button ButtonCancel;

    public EmbedWindowYesNo()
    {
      base.\u002Ector();
    }

    public static EmbedWindowYesNo Create(string msg, EmbedWindowYesNo.YesNoWindowEvent callback)
    {
      EmbedWindowYesNo embedWindowYesNo = (EmbedWindowYesNo) Object.Instantiate<EmbedWindowYesNo>(Resources.Load<EmbedWindowYesNo>("e/UI/EmbedWindowYesNo"));
      embedWindowYesNo.Body = msg;
      embedWindowYesNo.Delegate = callback;
      return embedWindowYesNo;
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ButtonOk, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonOk.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnOk)));
      }
      if (!Object.op_Inequality((Object) this.ButtonCancel, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonCancel.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
    }

    public string Body
    {
      set
      {
        this.Message.set_text(value);
      }
      get
      {
        return this.Message.get_text();
      }
    }

    private void OnOk()
    {
      this.Delegate(true);
    }

    private void OnCancel()
    {
      this.Delegate(false);
    }

    public delegate void YesNoWindowEvent(bool yes);
  }
}
