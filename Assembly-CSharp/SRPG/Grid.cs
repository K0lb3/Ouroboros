// Decompiled with JetBrains decompiler
// Type: SRPG.Grid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class Grid
  {
    public int cost = 1;
    public byte step = 127;
    public int x;
    public int y;
    public int height;
    public string tile;
    public GeoParam geo;
  }
}
