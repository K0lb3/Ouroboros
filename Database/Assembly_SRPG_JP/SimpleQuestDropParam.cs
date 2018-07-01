// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleQuestDropParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class SimpleQuestDropParam
  {
    public string item_iname;
    public string[] questlist;

    public bool Deserialize(JSON_SimpleQuestDropParam json)
    {
      this.item_iname = json.iname;
      this.questlist = json.questlist;
      return true;
    }
  }
}
