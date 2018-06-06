// Decompiled with JetBrains decompiler
// Type: SRPG.ChatStampParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ChatStampParam
  {
    public int id;
    public string img_id;
    public string iname;
    public bool IsPrivate;

    public bool Deserialize(JSON_ChatStampParam json)
    {
      if (json == null || json.fields == null)
        return false;
      this.id = json.fields.id;
      this.img_id = json.fields.img_id;
      this.iname = json.fields.iname;
      this.IsPrivate = json.fields.is_private == 1;
      return true;
    }
  }
}
