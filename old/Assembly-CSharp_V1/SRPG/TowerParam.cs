// Decompiled with JetBrains decompiler
// Type: SRPG.TowerParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TowerParam
  {
    protected string localizedNameID;
    protected string localizedExprID;
    public string iname;
    public string name;
    public string expr;
    public string banr;
    public string bg;
    public short unit_recover_minute;
    public short unit_recover_coin;
    public string prefabPath;
    public string eventURL;
    public bool can_unit_recover;
    public bool is_down;
    public bool is_view_ranking;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalizedQuestParamFileName, this.localizedNameID);
      this.expr = LocalizedText.SGGet(language, GameUtility.LocalizedQuestParamFileName, this.localizedExprID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
      this.localizedExprID = this.GetType().GenerateLocalizedID(this.iname, "EXPR");
    }

    public void Deserialize(string language, JSON_TowerParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

    public void Deserialize(JSON_TowerParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.banr = json.banr;
      this.prefabPath = json.item;
      this.bg = json.bg;
      this.can_unit_recover = (int) json.can_unit_recover == 1;
      this.unit_recover_minute = json.unit_recover_minute;
      this.unit_recover_coin = json.unit_recover_coin;
      this.eventURL = json.eventURL;
      this.is_down = (int) json.is_down > 0;
      this.is_view_ranking = (int) json.is_view_ranking > 0;
    }
  }
}
