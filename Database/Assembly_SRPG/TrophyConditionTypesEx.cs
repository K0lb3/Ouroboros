// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyConditionTypesEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public static class TrophyConditionTypesEx
  {
    public static bool IsExtraClear(this TrophyConditionTypes type)
    {
      switch (type)
      {
        case TrophyConditionTypes.exclear_fire:
        case TrophyConditionTypes.exclear_water:
        case TrophyConditionTypes.exclear_wind:
        case TrophyConditionTypes.exclear_thunder:
        case TrophyConditionTypes.exclear_light:
        case TrophyConditionTypes.exclear_dark:
        case TrophyConditionTypes.exclear_fire_nocon:
        case TrophyConditionTypes.exclear_water_nocon:
        case TrophyConditionTypes.exclear_wind_nocon:
        case TrophyConditionTypes.exclear_thunder_nocon:
        case TrophyConditionTypes.exclear_light_nocon:
        case TrophyConditionTypes.exclear_dark_nocon:
          return true;
        default:
          return false;
      }
    }
  }
}
