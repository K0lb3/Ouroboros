// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
