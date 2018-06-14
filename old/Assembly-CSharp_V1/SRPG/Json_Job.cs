// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Job
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class Json_Job
  {
    public long iid;
    public string iname;
    public int rank;
    public string cur_skin;
    public Json_Equip[] equips;
    public Json_Ability[] abils;
    public Json_Artifact[] artis;
    public Json_JobSelectable select;
  }
}
