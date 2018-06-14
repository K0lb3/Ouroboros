// Decompiled with JetBrains decompiler
// Type: SRPG.AwardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class AwardParam
  {
    private string localizedNameID;
    private string localizedExprID;
    public int id;
    public string iname;
    public string name;
    public string expr;
    public string icon;
    public string bg;
    public string txt_img;
    public DateTime start_at;
    public int grade;
    public int hash;
    public int tab;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedNameID);
      this.expr = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedExprID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
      this.localizedExprID = this.GetType().GenerateLocalizedID(this.iname, "EXPR");
    }

    public void Deserialize(string language, JSON_AwardParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

    public bool Deserialize(JSON_AwardParam json)
    {
      if (json == null)
        return false;
      this.id = json.id;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.icon = json.icon;
      this.bg = json.bg;
      this.txt_img = json.txt_img;
      this.start_at = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.start_at))
        DateTime.TryParse(json.start_at, out this.start_at);
      this.grade = json.grade;
      this.hash = json.iname.GetHashCode();
      this.tab = json.tab;
      return true;
    }

    public bool IsAvailableStart(DateTime now)
    {
      return this.start_at < now;
    }

    public ItemParam ToItemParam()
    {
      JSON_ItemParam json = new JSON_ItemParam();
      json.iname = this.iname;
      json.name = this.name;
      json.icon = this.icon;
      json.rare = this.grade - 1;
      json.type = 19;
      ItemParam itemParam = new ItemParam();
      itemParam.Deserialize(json);
      return itemParam;
    }

    public enum AwardGrade
    {
      None,
      Grade1,
      Grade2,
      Grade3,
      Grade4,
      Grade5,
      GradeEx,
    }

    public enum Tab
    {
      None = -1,
      Normal = 0,
      Extra = 1,
    }
  }
}
