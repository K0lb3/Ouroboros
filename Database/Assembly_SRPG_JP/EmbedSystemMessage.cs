// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedSystemMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class EmbedSystemMessage : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedSystemMessage";
    public EmbedSystemMessage.SystemMessageEvent Delegate;
    public Text Message;
    public Button ButtonOk;

    public EmbedSystemMessage()
    {
      base.\u002Ector();
    }

    public static EmbedSystemMessage Create(string msg, EmbedSystemMessage.SystemMessageEvent callback)
    {
      EmbedSystemMessage embedSystemMessage = (EmbedSystemMessage) Object.Instantiate<EmbedSystemMessage>(Resources.Load<EmbedSystemMessage>("e/UI/EmbedSystemMessage"));
      embedSystemMessage.Body = msg;
      embedSystemMessage.Delegate = callback;
      return embedSystemMessage;
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ButtonOk, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonOk.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnOk)));
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

    public delegate void SystemMessageEvent(bool yes);
  }
}
