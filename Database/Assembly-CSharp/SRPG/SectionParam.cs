// Decompiled with JetBrains decompiler
// Type: SRPG.SectionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class SectionParam
  {
    private string localizedNameID;
    private string localizedExprID;
    public string iname;
    public string name;
    public string expr;
    public long start;
    public long end;
    public bool hidden;
    public string home;
    public string unit;
    public string prefabPath;
    public string shop;
    public string inn;
    public string bar;
    public string bgm;

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

    public void Deserialize(string language, JSON_SectionParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

    public void Deserialize(JSON_SectionParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.start = json.start;
      this.end = json.end;
      this.hidden = json.hide != 0;
      this.home = json.home;
      this.unit = json.unit;
      this.prefabPath = json.item;
      this.shop = json.shop;
      this.inn = json.inn;
      this.bar = json.bar;
      this.bgm = json.bgm;
    }

    public bool IsDateUnlock()
    {
      long serverTime = Network.GetServerTime();
      if (this.end == 0L)
        return !this.hidden;
      return this.start <= serverTime && serverTime < this.end;
    }
  }
}
