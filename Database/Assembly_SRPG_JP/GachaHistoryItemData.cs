// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class GachaHistoryItemData
  {
    private GachaHistoryData[] mHistorys;
    private string mGachaTitle;
    private long mDropAt;

    public GachaHistoryItemData(GachaHistoryData[] historys, string title, long drop_at)
    {
      this.mHistorys = historys;
      this.mGachaTitle = title;
      this.mDropAt = drop_at;
    }

    public GachaHistoryData[] historys
    {
      get
      {
        return this.mHistorys;
      }
    }

    public string gachaTitle
    {
      get
      {
        return this.mGachaTitle;
      }
    }

    public long drop_at
    {
      get
      {
        return this.mDropAt;
      }
    }

    public DateTime GetDropAt()
    {
      return GameUtility.UnixtimeToLocalTime(this.mDropAt);
    }
  }
}
