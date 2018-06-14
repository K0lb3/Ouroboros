// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeUnitData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
    public int aiindex;
    public int aiturn;
    public int aipatrol;
    public int search;
    public int entry;
    public Grid targetgrid;
    public MultiPlayResumeBuff[] buff;
    public MultiPlayResumeBuff[] cond;
    public string[] skillname;
    public int[] skillcnt;
  }
}
