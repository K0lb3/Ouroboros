// Decompiled with JetBrains decompiler
// Type: SRPG.ChatSendRes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ChatSendRes
  {
    public byte is_success;

    public bool IsSuccess
    {
      get
      {
        return this.is_success == (byte) 1;
      }
    }

    public void Deserialize(JSON_ChatSendRes json)
    {
      if (json == null)
        return;
      this.is_success = json.is_success;
    }
  }
}
