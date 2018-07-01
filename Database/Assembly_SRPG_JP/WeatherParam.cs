// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class WeatherParam
  {
    private List<string> mBuffIdLists = new List<string>();
    private List<string> mCondIdLists = new List<string>();
    private string mIname;
    private string mName;
    private string mExpr;
    private string mIcon;
    private string mEffect;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public string Expr
    {
      get
      {
        return this.mExpr;
      }
    }

    public string Icon
    {
      get
      {
        return this.mIcon;
      }
    }

    public string Effect
    {
      get
      {
        return this.mEffect;
      }
    }

    public List<string> BuffIdLists
    {
      get
      {
        return this.mBuffIdLists;
      }
    }

    public List<string> CondIdLists
    {
      get
      {
        return this.mCondIdLists;
      }
    }

    public void Deserialize(JSON_WeatherParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mExpr = json.expr;
      this.mIcon = json.icon;
      this.mEffect = json.effect;
      this.mBuffIdLists.Clear();
      if (json.buff_ids != null)
      {
        foreach (string buffId in json.buff_ids)
          this.mBuffIdLists.Add(buffId);
      }
      this.mCondIdLists.Clear();
      if (json.cond_ids == null)
        return;
      foreach (string condId in json.cond_ids)
        this.mCondIdLists.Add(condId);
    }
  }
}
