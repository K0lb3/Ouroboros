// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class UnlockParam
  {
    public string iname;
    public UnlockTargets UnlockTarget;
    public int PlayerLevel;
    public int VipRank;

    public bool Deserialize(JSON_UnlockParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      try
      {
        this.UnlockTarget = (UnlockTargets) Enum.Parse(typeof (UnlockTargets), json.iname);
      }
      catch (Exception ex)
      {
        return false;
      }
      this.PlayerLevel = json.lv;
      this.VipRank = json.vip;
      return true;
    }
  }
}
