// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectAmount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ItemSelectAmount : MonoBehaviour, IGameParameter
  {
    public ItemSelectAmount()
    {
      base.\u002Ector();
    }

    public void UpdateValue()
    {
      ItemSelectListItemData dataOfClass = DataSource.FindDataOfClass<ItemSelectListItemData>(((Component) this).get_gameObject(), (ItemSelectListItemData) null);
      Text component = (Text) ((Component) this).get_gameObject().GetComponent<Text>();
      if (!Object.op_Inequality((Object) component, (Object) null) || dataOfClass == null)
        return;
      component.set_text(dataOfClass.num.ToString());
    }
  }
}
