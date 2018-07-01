// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class WeatherParam
  {
    private List<string> mBuffIdLists = new List<string>();
    private List<string> mCondIdLists = new List<string>();
    private string localizedNameID;
    private string localizedExprID;
    private string originName;
    private string mIname;
    private string mName;
    private string mExpr;
    private string mIcon;
    private string mEffect;

    public string OriginName
    {
      get
      {
        return this.originName;
      }
    }

    protected void localizeFields(string language)
    {
      this.init();
      this.originName = this.mName;
      this.mName = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedNameID);
      this.mExpr = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedExprID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.mIname, "NAME");
      this.localizedExprID = this.GetType().GenerateLocalizedID(this.mIname, "EXPR");
    }

    public void Deserialize(string language, JSON_WeatherParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

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
