// Decompiled with JetBrains decompiler
// Type: SRPG.TrickSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TrickSetting
  {
    public string mId;
    public OInt mGx;
    public OInt mGy;
    public string mTag;

    public TrickSetting()
    {
    }

    public TrickSetting(JSON_MapTrick json)
    {
      this.mId = json.id;
      this.mGx = (OInt) json.gx;
      this.mGy = (OInt) json.gy;
      this.mTag = json.tag;
    }
  }
}
