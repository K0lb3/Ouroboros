// Decompiled with JetBrains decompiler
// Type: SRPG.Grid
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
