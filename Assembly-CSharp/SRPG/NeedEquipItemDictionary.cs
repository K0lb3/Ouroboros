// Decompiled with JetBrains decompiler
// Type: SRPG.NeedEquipItemDictionary
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class NeedEquipItemDictionary
  {
    public List<NeedEquipItem> list = new List<NeedEquipItem>();
    private int need_picec;
    private ItemData data;
    public ItemParam CommonItemParam;

    public NeedEquipItemDictionary(ItemParam item_param, bool is_soul = false)
    {
      this.CommonItemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(item_param, is_soul);
      this.data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.CommonItemParam.iname);
    }

    public int CommonEquipItemNum
    {
      get
      {
        if (this.data != null)
          return this.data.Num;
        return 0;
      }
    }

    public bool IsEnough
    {
      get
      {
        return this.CommonEquipItemNum >= this.need_picec;
      }
    }

    public int NeedPicec
    {
      get
      {
        return this.need_picec;
      }
    }

    public void Add(ItemParam _param, int _need_picec)
    {
      this.list.Add(new NeedEquipItem(_param, _need_picec));
      this.need_picec += _need_picec;
    }

    public void Remove(ItemParam _param)
    {
      NeedEquipItem needEquipItem = this.list[this.list.Count - 1];
      if (needEquipItem == null)
        return;
      this.need_picec -= needEquipItem.NeedPiece;
      this.list.RemoveAt(this.list.Count - 1);
    }
  }
}
