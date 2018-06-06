// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectAmount
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
