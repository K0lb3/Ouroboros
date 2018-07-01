// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ChatChannelMasterParam
  {
    public int id;
    public byte category_id;
    public string name;

    public void Deserialize(Json_ChatChannelMasterParam json)
    {
      if (json == null)
        throw new InvalidCastException();
      this.id = json.fields.id;
      this.category_id = json.fields.category_id;
      this.name = json.fields.name;
    }
  }
}
