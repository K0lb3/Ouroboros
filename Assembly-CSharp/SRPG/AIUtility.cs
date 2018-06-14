// Decompiled with JetBrains decompiler
// Type: SRPG.AIUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
