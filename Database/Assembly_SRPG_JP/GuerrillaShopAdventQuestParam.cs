// Decompiled with JetBrains decompiler
// Type: SRPG.GuerrillaShopAdventQuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class GuerrillaShopAdventQuestParam
  {
    public int id;
    public string qid;

    public bool Deserialize(JSON_GuerrillaShopAdventQuestParam json)
    {
      this.id = json.id;
      this.qid = json.qid;
      return true;
    }
  }
}
