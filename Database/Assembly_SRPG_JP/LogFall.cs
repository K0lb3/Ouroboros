// Decompiled with JetBrains decompiler
// Type: SRPG.LogFall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class LogFall : BattleLog
  {
    public List<LogFall.Param> mLists = new List<LogFall.Param>();
    public bool mIsPlayDamageMotion;

    public void Add(Unit self, Grid landing = null)
    {
      this.mLists.Add(new LogFall.Param()
      {
        mSelf = self,
        mLanding = landing
      });
    }

    public struct Param
    {
      public Unit mSelf;
      public Grid mLanding;
    }
  }
}
