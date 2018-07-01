// Decompiled with JetBrains decompiler
// Type: SRPG.AIUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public static class AIUtility
  {
    public static bool IsFailCondition(EUnitCondition condition)
    {
      return condition != EUnitCondition.AutoHeal && condition != EUnitCondition.GoodSleep && (condition != EUnitCondition.AutoJewel && condition != EUnitCondition.Fast) && (condition != EUnitCondition.DisableDebuff && condition != EUnitCondition.DisableKnockback);
    }

    public static bool IsFailCondition(Unit self, Unit target, EUnitCondition condition)
    {
      bool flag = SceneBattle.Instance.Battle.CheckEnemySide(self, target);
      if (AIUtility.IsFailCondition(condition))
        return flag;
      return !flag;
    }
  }
}
