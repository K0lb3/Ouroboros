// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkErrorWindowEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class NetworkErrorWindowEx : MonoBehaviour
  {
    [SerializeField]
    private Text Message;
    [SerializeField]
    private Button ButtonOk;

    public NetworkErrorWindowEx()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ButtonOk, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonOk.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnOk)));
    }

    private void Start()
    {
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
    }
  }
}
