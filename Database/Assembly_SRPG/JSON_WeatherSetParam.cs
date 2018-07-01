// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_WeatherSetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
