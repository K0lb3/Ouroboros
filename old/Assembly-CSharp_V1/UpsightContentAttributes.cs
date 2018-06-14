// Decompiled with JetBrains decompiler
// Type: UpsightContentAttributes
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UpsightMiniJSON;

public class UpsightContentAttributes
{
  public UpsightContentAttributes.Type ContentType { get; private set; }

  public string ContentProperties { get; private set; }

  public static UpsightContentAttributes FromJson(string json, out string scope)
  {
    scope = (string) null;
    UpsightContentAttributes contentAttributes = new UpsightContentAttributes();
    Dictionary<string, object> jsonObject = Json.ToJsonObject(json);
    if (jsonObject != null)
    {
      if (jsonObject.ContainsKey(nameof (scope)))
        scope = jsonObject[nameof (scope)].ToString();
      if (jsonObject.ContainsKey("type"))
        contentAttributes.ContentType = (UpsightContentAttributes.Type) jsonObject.GetPrimitive<int>("type");
      if (jsonObject.ContainsKey("properties"))
        contentAttributes.ContentProperties = Json.Serialize(jsonObject["properties"]);
    }
    return contentAttributes;
  }

  public override string ToString()
  {
    return string.Format("[UpsightContentAttributes] ContentType: {0}, ContentProperties: {1}", (object) this.ContentType, (object) this.ContentProperties);
  }

  public enum Type
  {
    UNKNOWN,
    ANNOUNCEMENT,
    INTERNAL_CROSS_PROMOTION,
    REWARDS,
    VIRTUAL_GOODS_PROMOTION,
    OPT_IN,
    ADS,
    MORE_GAMES,
    VIDEO_CAMPAIGN,
    CUSTOM_VIEW,
    MEDIATION,
  }
}
