// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_VersusTowerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_VersusTowerParam
  {
    public string vstower_id;
    public string iname;
    public int floor;
    public int rankup_num;
    public int win_num;
    public int lose_num;
    public int bonus_num;
    public int downfloor;
    public int resetfloor;
    public string[] winitem;
    public int[] win_itemnum;
    public string[] joinitem;
    public int[] join_itemnum;
    public string arrival_item;
    public string arrival_type;
    public int arrival_num;
    public string[] spbtl_item;
    public int[] spbtl_itemnum;
    public string[] season_item;
    public string[] season_itype;
    public int[] season_itemnum;
  }
}
