// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TobiraParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_TobiraParam
  {
    public string unit_iname;
    public int enable;
    public int category;
    public string recipe_id;
    public string skill_iname;
    public JSON_TobiraLearnAbilityParam[] learn_abils;
    public string overwrite_ls_iname;
    public int priority;
  }
}
