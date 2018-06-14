// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeUnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class MultiPlayResumeUnitData
  {
    public string name;
    public int hp;
    public int gem;
    public int dir;
    public int x;
    public int y;
    public int target;
    public int ragetarget;
    public string castskill;
    public int chargetime;
    public int casttime;
    public int[] castgrid;
    public int casttarget;
    public int castindex;
    public int grid_w;
    public int grid_h;
    public int isDead;
    public int deathcnt;
    public int autojewel;
    public int waitturn;
    public int moveturn;
    public int actcnt;
    public int turncnt;
    public int trgcnt;
    public int killcnt;
    public int[] etr;
    public int aiindex;
    public int aiturn;
    public int aipatrol;
    public int search;
    public int entry;
    public int to_dying;
    public int paralyse;
    public int ctx;
    public int cty;
    public string boi;
    public int boc;
    public int own;
    public MultiPlayResumeBuff[] buff;
    public MultiPlayResumeBuff[] cond;
    public MultiPlayResumeShield[] shields;
    public string[] skillname;
    public int[] skillcnt;
  }
}
