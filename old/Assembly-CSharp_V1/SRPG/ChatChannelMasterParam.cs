// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelMasterParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ChatChannelMasterParam
  {
    private string localizedNameID;
    public int id;
    public byte category_id;
    public string name;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalisedChatChannelName, this.localizedNameID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.id.ToString(), "NAME");
    }

    public void Deserialize(string language, Json_ChatChannelMasterParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

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
