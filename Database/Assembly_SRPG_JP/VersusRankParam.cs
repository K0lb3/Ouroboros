// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class VersusRankParam
  {
    private int mId;
    private VS_MODE mVSMode;
    private string mName;
    private int mLimit;
    private DateTime mBeginAt;
    private DateTime mEndAt;
    private int mWinPointBase;
    private int mLosePointBase;
    private List<DateTime> mDisableDateList;
    private string mHUrl;

    public int Id
    {
      get
      {
        return this.mId;
      }
    }

    public VS_MODE VSMode
    {
      get
      {
        return this.mVSMode;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public int Limit
    {
      get
      {
        return this.mLimit;
      }
    }

    public DateTime BeginAt
    {
      get
      {
        return this.mBeginAt;
      }
    }

    public DateTime EndAt
    {
      get
      {
        return this.mEndAt;
      }
    }

    public IList<DateTime> DisableDateList
    {
      get
      {
        return (IList<DateTime>) this.mDisableDateList.AsReadOnly();
      }
    }

    public int WinPointBase
    {
      get
      {
        return this.mWinPointBase;
      }
    }

    public int LosePointBase
    {
      get
      {
        return this.mLosePointBase;
      }
    }

    public string HelpURL
    {
      get
      {
        return this.mHUrl;
      }
    }

    public bool Deserialize(JSON_VersusRankParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mVSMode = (VS_MODE) json.btl_mode;
      this.mName = json.name;
      this.mLimit = json.limit;
      this.mWinPointBase = json.win_pt_base;
      this.mLosePointBase = json.lose_pt_base;
      this.mHUrl = json.hurl;
      this.mDisableDateList = new List<DateTime>();
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.mBeginAt = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.mEndAt = DateTime.Parse(json.end_at);
        if (json.disabledate != null)
        {
          for (int index = 0; index < json.disabledate.Length; ++index)
          {
            if (!string.IsNullOrEmpty(json.disabledate[index]))
              this.mDisableDateList.Add(DateTime.Parse(json.disabledate[index]));
          }
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
        return false;
      }
      return true;
    }
  }
}
