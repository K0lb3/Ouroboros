// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Job
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
    public string unit_image;
  }
}
