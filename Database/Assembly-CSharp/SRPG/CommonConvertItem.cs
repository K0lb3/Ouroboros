// Decompiled with JetBrains decompiler
// Type: SRPG.CommonConvertItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class CommonConvertItem : MonoBehaviour
  {
    public GameObject Obj;
    public GameObject CommonObj;
    public LText Amount;
    public LText ItemName;
    public Text ItemUseNum;
    public Text CommonItemUseNum;

    public CommonConvertItem()
    {
      base.\u002Ector();
    }

    public void Bind(ItemData data, ItemData cmmon_data, int need_num)
    {
      DataSource.Bind<ItemData>(this.Obj, data);
      DataSource.Bind<ItemData>(this.CommonObj, cmmon_data);
      this.Amount.set_text(LocalizedText.Get("sys.COMMON_EQUIP_NUM", new object[1]
      {
        (object) cmmon_data.Num
      }));
      this.ItemName.set_text(LocalizedText.Get("sys.COMMON_EQUIP_NAME", (object) cmmon_data.Param.name, (object) need_num));
      Text itemUseNum = this.ItemUseNum;
      string str1 = need_num.ToString();
      this.CommonItemUseNum.set_text(str1);
      string str2 = str1;
      itemUseNum.set_text(str2);
    }

    public void Refresh(ItemData data, ItemData cmmon_data)
    {
    }
  }
}
