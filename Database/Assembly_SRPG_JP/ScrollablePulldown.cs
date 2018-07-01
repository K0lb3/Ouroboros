// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollablePulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ScrollablePulldown : ScrollablePulldownBase
  {
    [SerializeField]
    private GameObject PulldownItemTemplate;

    protected override void Start()
    {
      base.Start();
      if (!Object.op_Inequality((Object) this.PulldownItemTemplate, (Object) null))
        return;
      this.PulldownItemTemplate.get_gameObject().SetActive(false);
    }

    protected override void OnDestroy()
    {
      this.ClearItems();
      base.OnDestroy();
    }

    public PulldownItem AddItem(string label, int value)
    {
      if (Object.op_Equality((Object) this.PulldownItemTemplate, (Object) null))
        return (PulldownItem) null;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.PulldownItemTemplate);
      ((SRPG_Button) gameObject.GetComponent<SRPG_Button>()).AddListener((SRPG_Button.ButtonClickEvent) (g =>
      {
        this.Selection = value;
        this.ClosePulldown(false);
        this.TriggerItemChange();
      }));
      PulldownItem component = (PulldownItem) gameObject.GetComponent<PulldownItem>();
      if (Object.op_Inequality((Object) component.Text, (Object) null))
        component.Text.set_text(label);
      component.Value = value;
      this.Items.Add(component);
      gameObject.get_transform().SetParent((Transform) this.ItemHolder, false);
      gameObject.SetActive(true);
      this.ScrollRect.set_verticalNormalizedPosition(1f);
      this.ScrollRect.set_horizontalNormalizedPosition(1f);
      return component;
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.Items.Count; ++index)
        Object.Destroy((Object) ((Component) this.Items[index]).get_gameObject());
      this.Items.Clear();
      this.ResetAllStatus();
    }
  }
}
