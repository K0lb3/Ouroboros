// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentItemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class FriendPresentItemParam
  {
    public const string DEAULT_ID = "FP_DEFAULT";
    public static FriendPresentItemParam DefaultParam;
    private string m_Id;
    private string m_Name;
    private string m_Expr;
    private ItemParam m_Item;
    private int m_Num;
    private int m_Zeny;
    private long m_BeginAt;
    private long m_EndAt;

    public string iname
    {
      get
      {
        return this.m_Id;
      }
    }

    public string name
    {
      get
      {
        return this.m_Name;
      }
    }

    public string expr
    {
      get
      {
        return this.m_Expr;
      }
    }

    public ItemParam item
    {
      get
      {
        return this.m_Item;
      }
    }

    public int num
    {
      get
      {
        return this.m_Num;
      }
    }

    public int zeny
    {
      get
      {
        return this.m_Zeny;
      }
    }

    public long begin_at
    {
      get
      {
        return this.m_BeginAt;
      }
    }

    public long end_at
    {
      get
      {
        return this.m_EndAt;
      }
    }

    public long GetRestTime(long serverTime)
    {
      long num = this.m_EndAt - serverTime;
      if (num < 0L)
        num = 0L;
      return num;
    }

    public bool IsItem()
    {
      return this.m_Item != null;
    }

    public bool IsZeny()
    {
      return this.m_Item == null;
    }

    public bool HasTimeLimit()
    {
      if (this.m_BeginAt <= 0L)
        return this.m_EndAt > 0L;
      return true;
    }

    public bool IsValid(long nowSec)
    {
      if (!this.HasTimeLimit())
        return true;
      if (this.m_BeginAt <= nowSec)
        return nowSec < this.m_EndAt;
      return false;
    }

    public bool IsDefault()
    {
      return this.m_Id == "FP_DEFAULT";
    }

    public void Deserialize(JSON_FriendPresentItemParam json, MasterParam master = null)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.m_Id = json.iname;
      this.m_Name = json.name;
      this.m_Expr = json.expr;
      if (!string.IsNullOrEmpty(json.item))
        this.m_Item = MonoSingleton<GameManager>.Instance.GetItemParam(json.item);
      this.m_Num = json.num;
      this.m_Zeny = json.zeny;
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.m_BeginAt = TimeManager.GetUnixSec(DateTime.Parse(json.begin_at));
        if (!string.IsNullOrEmpty(json.end_at))
          this.m_EndAt = TimeManager.GetUnixSec(DateTime.Parse(json.end_at));
        if (!(this.m_Id == "FP_DEFAULT"))
          return;
        FriendPresentItemParam.DefaultParam = this;
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.ToString());
      }
    }
  }
}
