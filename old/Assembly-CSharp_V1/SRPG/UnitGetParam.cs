// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class UnitGetParam
  {
    public List<UnitGetParam.Set> Params = new List<UnitGetParam.Set>();

    public UnitGetParam(ItemParam param)
    {
      this.Add(param);
    }

    public UnitGetParam(ItemParam[] paramLsit)
    {
      this.AddArary(paramLsit);
    }

    public UnitGetParam(ItemData data)
    {
      this.Add(data.Param);
    }

    public UnitGetParam(ItemData[] paramLsit)
    {
      this.AddArary(paramLsit);
    }

    public UnitGetParam(GiftData[] paramList)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      for (int index = 0; index < paramList.Length; ++index)
      {
        if (paramList[index].CheckGiftTypeIncluded(GiftTypes.Unit))
        {
          ItemParam itemParam = masterParam.GetItemParam(paramList[index].iname);
          if (itemParam != null)
            this.Add(itemParam);
        }
      }
    }

    public void Add(ItemParam param)
    {
      this.AddInternal(param, (List<UnitData>) null);
    }

    private void AddInternal(ItemParam param, List<UnitData> units = null)
    {
      if (param.type != EItemType.Unit)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      UnitData unitData = (units != null ? units : instanceDirect.Player.Units).Find((Predicate<UnitData>) (p => p.UnitParam.iname == param.iname));
      UnitGetParam.Set set = new UnitGetParam.Set();
      set.ItemId = param.iname;
      set.ItemType = param.type;
      set.IsConvert = unitData != null;
      if (unitData == null)
      {
        UnitParam unitParam = instanceDirect.MasterParam.GetUnitParam(param.iname);
        set.UnitParam = unitParam;
      }
      this.Params.Add(set);
    }

    public void AddArary(ItemParam[] list)
    {
      List<UnitData> units = MonoSingleton<GameManager>.GetInstanceDirect().Player.Units;
      for (int index = 0; index < list.Length; ++index)
        this.AddInternal(list[index], units);
    }

    public void AddArary(ItemData[] list)
    {
      List<UnitData> units = MonoSingleton<GameManager>.GetInstanceDirect().Player.Units;
      for (int index = 0; index < list.Length; ++index)
        this.AddInternal(list[index].Param, units);
    }

    public class Set
    {
      public string ItemId;
      public EItemType ItemType;
      public bool IsConvert;
      public UnitParam UnitParam;
    }
  }
}
