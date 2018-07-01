// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_WeatherSetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_WeatherSetParam
  {
    public string iname;
    public string name;
    public string[] st_wth;
    public int[] st_rate;
    public int ch_cl_min;
    public int ch_cl_max;
    public string[] ch_wth;
    public int[] ch_rate;
  }
}
