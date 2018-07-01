// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapTrick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_MapTrick
  {
    public string id;
    public int gx;
    public int gy;
    public string tag;

    public void CopyTo(JSON_MapTrick dst)
    {
      dst.id = this.id;
      dst.gx = this.gx;
      dst.gy = this.gy;
      dst.tag = this.tag;
    }
  }
}
