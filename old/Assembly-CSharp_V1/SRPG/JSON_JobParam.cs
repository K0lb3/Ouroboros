// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_JobParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_JobParam
  {
    public string iname;
    public string name;
    public string expr;
    public string mdl;
    public string mdlp;
    public string pet;
    public string buki;
    public string origin;
    public int type;
    public int role;
    public int jmov;
    public int jjmp;
    public string wepmdl;
    public string atkskl;
    public string atkfi;
    public string atkwa;
    public string atkwi;
    public string atkth;
    public string atksh;
    public string atkda;
    public string fixabl;
    public string artifact;
    public string ai;
    public string master;
    public JSON_JobRankParam[] ranks;
  }
}
