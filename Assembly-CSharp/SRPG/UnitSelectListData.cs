// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSelectListData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class UnitSelectListData
  {
    public List<UnitSelectListItemData> items;

    public void Deserialize(Json_UnitSelectResponse json)
    {
      if (json == null || json.select == null)
        return;
      this.items = new List<UnitSelectListItemData>();
      for (int index = 0; index < json.select.Length; ++index)
      {
        this.items.Add(new UnitSelectListItemData());
        this.items[index].Deserialize(json.select[index]);
        this.items[index].param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.items[index].iname);
      }
    }
  }
}
