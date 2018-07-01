// Decompiled with JetBrains decompiler
// Type: SRPG.EmbedSystemMessageEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
      EmbedSystemMessageEx.\u003CAddButton\u003Ec__AnonStorey323 buttonCAnonStorey323 = new EmbedSystemMessageEx.\u003CAddButton\u003Ec__AnonStorey323();
      // ISSUE: reference to a compiler-generated field
      buttonCAnonStorey323.callback = callback;
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
        ((UnityEvent) componentInChildren2.get_onClick()).AddListener(new UnityAction((object) buttonCAnonStorey323, __methodptr(\u003C\u003Em__357)));
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
