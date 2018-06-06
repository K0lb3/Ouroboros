// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTopParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GachaTopParam
  {
    public string[] iname = new string[4];
    public string[] category = new string[4];
    public int[] coin = new int[4];
    public int[] gold = new int[4];
    public int[] coin_p = new int[4];
    public int[] num = new int[4];
    public string[] ticket = new string[4];
    public int[] ticket_num = new int[4];
    public bool[] step = new bool[4];
    public int[] step_num = new int[4];
    public int[] step_index = new int[4];
    public bool[] limit = new bool[4];
    public int[] limit_num = new int[4];
    public int[] limit_stock = new int[4];
    public string[] btext = new string[4];
    public string[] confirm = new string[4];
    public List<int> sort = new List<int>();
    public List<UnitParam> units;
    public string type;
    public string asset_title;
    public string asset_bg;
    public string group;
  }
}
