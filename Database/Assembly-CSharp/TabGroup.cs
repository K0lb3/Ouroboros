// Decompiled with JetBrains decompiler
// Type: TabGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("UI/Tab Group")]
public class TabGroup : MonoBehaviour
{
  [FlexibleArray]
  public Toggle[] Tabs;

  public TabGroup()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if (Object.op_Inequality((Object) this.Tabs[index], (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.Tabs[index].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnValueChange)));
      }
    }
    this.OnValueChange(true);
  }

  private void OnValueChange(bool value)
  {
    if (!value)
      return;
    Transform transform = ((Component) this).get_transform();
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if (!this.Tabs[index].get_isOn() && Object.op_Inequality((Object) transform.GetChild(index), (Object) null))
        ((Component) transform.GetChild(index)).get_gameObject().SetActive(false);
    }
    for (int index = 0; index < this.Tabs.Length; ++index)
    {
      if (this.Tabs[index].get_isOn() && Object.op_Inequality((Object) transform.GetChild(index), (Object) null))
        ((Component) transform.GetChild(index)).get_gameObject().SetActive(true);
    }
  }
}
