// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapEnemyUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class JSON_MapEnemyUnit : JSON_MapPartyUnit
  {
    public string iname;
    public int side;
    public int lv;
    public int rare;
    public int awake;
    public int elem;
    public int exp;
    public int gems;
    public int gold;
    public int search;
    public int ctrl;
    public int no_st_drop;
    public int no_disp_drop;
    public string drop;
    public int notice_damage;
    public string[] notice_members;
    public JSON_MapEquipAbility[] abils;
    public JSON_AIActionTable acttbl;
    public AIPatrolTable patrol;
    public string fskl;
    public short weight;
    public byte tag;
    public int spawn_max;
    public MapBreakObj break_obj;

    public bool IsRandSymbol
    {
      get
      {
        return this.iname.StartsWith("enemy_");
      }
    }

    public int RandTagIndex
    {
      get
      {
        if (!this.IsRandSymbol)
          return -1;
        string[] strArray = this.iname.Split('_');
        return int.Parse(strArray[strArray.Length - 1]);
      }
    }
  }
}
