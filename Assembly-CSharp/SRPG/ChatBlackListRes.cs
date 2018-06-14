// Decompiled with JetBrains decompiler
// Type: SRPG.ChatBlackListRes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
