// Decompiled with JetBrains decompiler
// Type: SRPG.ChatBlackListRes
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ChatBlackListRes
  {
    public byte is_success;

    public bool IsSuccess
    {
      get
      {
        return (int) this.is_success == 1;
      }
    }

    public void Deserialize(JSON_ChatBlackListRes json)
    {
      if (json == null)
        return;
      this.is_success = json.is_success;
    }
  }
}
