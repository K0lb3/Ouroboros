// Decompiled with JetBrains decompiler
// Type: SRPG.MapParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class MapParam
  {
    public string mapSceneName;
    public string mapSetName;
    public string battleSceneName;
    public string eventSceneName;
    public string bgmName;

    public void Deserialize(JSON_MapParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.mapSceneName = json.scn;
      this.mapSetName = json.set;
      this.battleSceneName = json.btl;
      this.eventSceneName = json.ev;
      this.bgmName = json.bgm;
    }
  }
}
