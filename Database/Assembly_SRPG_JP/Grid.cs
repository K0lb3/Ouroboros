// Decompiled with JetBrains decompiler
// Type: SRPG.Grid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
