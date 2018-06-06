// Decompiled with JetBrains decompiler
// Type: FlowNode_LangageChangeEvent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[FlowNode.Pin(1, "Language Selected", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/OnLanguageSelected", 58751)]
[FlowNode.Pin(10, "Language Unchanged", FlowNode.PinTypes.Input, 0)]
public class FlowNode_LangageChangeEvent : FlowNodePersistent
{
  [SerializeField]
  protected Toggle target;
  [SerializeField]
  protected SGConfigWindow languageSelector;
  [SerializeField]
  protected string buttonLanguage;

  private void Start()
  {
    if (Object.op_Inequality((Object) this.target, (Object) null))
    {
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.target.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(onSelected)));
    }
    ((Behaviour) this).set_enabled(false);
  }

  private void onSelected(bool value)
  {
    if (value && this.buttonLanguage != GameUtility.Config_Language)
      this.Activate(1);
    ((Selectable) this.target).set_interactable(!value);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID == 1)
    {
      this.ActivateOutputLinks(1);
    }
    else
    {
      if (pinID != 10)
        return;
      this.languageSelector.resetToggle();
    }
  }
}
