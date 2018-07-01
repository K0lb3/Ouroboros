// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedSystemMessageEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class EmbedSystemMessageEx : MonoBehaviour
  {
    public const string PrefabPath = "e/UI/EmbedSystemMessageEx";
    public Text Message;
    public GameObject ButtonTemplate;
    public GameObject ButtonBase;

    public EmbedSystemMessageEx()
    {
      base.\u002Ector();
    }

    public static EmbedSystemMessageEx Create(string msg)
    {
      EmbedSystemMessageEx embedSystemMessageEx = (EmbedSystemMessageEx) Object.Instantiate<EmbedSystemMessageEx>(Resources.Load<EmbedSystemMessageEx>("e/UI/EmbedSystemMessageEx"));
      embedSystemMessageEx.Body = msg;
      return embedSystemMessageEx;
    }

    public void AddButton(string btn_text, bool is_close, EmbedSystemMessageEx.SystemMessageEvent callback)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      EmbedSystemMessageEx.\u003CAddButton\u003Ec__AnonStorey32B buttonCAnonStorey32B = new EmbedSystemMessageEx.\u003CAddButton\u003Ec__AnonStorey32B();
      // ISSUE: reference to a compiler-generated field
      buttonCAnonStorey32B.callback = callback;
      if (Object.op_Equality((Object) this.ButtonTemplate, (Object) null) || Object.op_Equality((Object) this.ButtonBase, (Object) null))
        return;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ButtonTemplate);
      gameObject.SetActive(true);
      LText componentInChildren1 = (LText) gameObject.GetComponentInChildren<LText>();
      if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
        componentInChildren1.set_text(btn_text);
      Button componentInChildren2 = (Button) gameObject.GetComponentInChildren<Button>();
      if (Object.op_Inequality((Object) componentInChildren2, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) componentInChildren2.get_onClick()).AddListener(new UnityAction((object) buttonCAnonStorey32B, __methodptr(\u003C\u003Em__2F2)));
      }
      ButtonEvent componentInChildren3 = (ButtonEvent) gameObject.GetComponentInChildren<ButtonEvent>();
      if (Object.op_Inequality((Object) componentInChildren3, (Object) null))
        ((Behaviour) componentInChildren3).set_enabled(is_close);
      gameObject.get_transform().SetParent(this.ButtonBase.get_transform(), false);
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ButtonTemplate, (Object) null))
        return;
      this.ButtonTemplate.SetActive(false);
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

    public delegate void SystemMessageEvent(bool yes);
  }
}
