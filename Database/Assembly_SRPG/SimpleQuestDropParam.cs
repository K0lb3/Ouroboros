// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleQuestDropParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
