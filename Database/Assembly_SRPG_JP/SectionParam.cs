// Decompiled with JetBrains decompiler
// Type: SRPG.SectionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class SectionParam
  {
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
    public int storyPart;
    public string releaseKeyQuest;

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
      this.storyPart = json.story_part;
      this.releaseKeyQuest = json.release_key_quest;
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
