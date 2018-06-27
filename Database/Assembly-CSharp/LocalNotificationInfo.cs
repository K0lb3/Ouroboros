// Decompiled with JetBrains decompiler
// Type: LocalNotificationInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public class LocalNotificationInfo
{
  private string localizedPushWord;
  public int id;
  public int push_flg;
  public string trophy_iname;
  public string push_word;
  public int custom_type;

  protected void localizeFields(string language)
  {
    this.init();
    this.push_word = LocalizedText.SGGet(language, GameUtility.LocalizedNotificationFileName, this.localizedPushWord);
  }

  protected void init()
  {
    this.localizedPushWord = this.GetType().GenerateLocalizedID(this.trophy_iname, "PUSH");
  }

  public bool Deserialize(string language, JSON_LocalNotificationInfo json)
  {
    if (!this.Deserialize(json))
      return false;
    this.localizeFields(language);
    return true;
  }

  public bool Deserialize(JSON_LocalNotificationInfo json)
  {
    if (json == null)
      return false;
    this.id = json.fields.id;
    this.trophy_iname = json.fields.trophy_iname;
    this.push_flg = json.fields.push_flg;
    this.push_word = json.fields.push_word;
    this.custom_type = json.fields.custom_type;
    return true;
  }
}
