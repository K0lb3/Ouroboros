// Decompiled with JetBrains decompiler
// Type: SRPG.ChatInspectionMaster
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ChatInspectionMaster
  {
    public bool reflection = true;
    public int id;
    public string word;

    public bool Deserialize(JSON_ChatInspectionMaster json)
    {
      if (json == null || json.fields == null)
        return false;
      this.id = json.fields.id;
      this.word = json.fields.ngword;
      this.reflection = json.fields.reflection == 1;
      return true;
    }
  }
}
