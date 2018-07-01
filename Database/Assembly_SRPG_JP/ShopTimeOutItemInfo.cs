// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTimeOutItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class ShopTimeOutItemInfo
  {
    public string ShopId;
    public int ItemId;
    public long End;

    public ShopTimeOutItemInfo(string shopId, int itemId, long end)
    {
      this.ShopId = shopId;
      this.ItemId = itemId;
      this.End = end;
    }
  }
}
