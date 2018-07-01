// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class TrophyObjective
  {
    public TrophyParam Param;
    public int index;
    public TrophyConditionTypes type;
    public List<string> sval;
    public int ival;

    public string sval_base
    {
      get
      {
        if (this.sval != null && 0 < this.sval.Count)
          return this.sval[0];
        return string.Empty;
      }
    }

    public int RequiredCount
    {
      get
      {
        TrophyConditionTypes type = this.type;
        switch (type)
        {
          case TrophyConditionTypes.damage_over:
          case TrophyConditionTypes.has_gold_over:
          case TrophyConditionTypes.read_tips:
          case TrophyConditionTypes.unlock_tobira_unit:
          case TrophyConditionTypes.complete_all_quest_mission_total:
label_6:
            return 1;
          case TrophyConditionTypes.envy_unlock_unit:
          case TrophyConditionTypes.sloth_unlock_unit:
          case TrophyConditionTypes.lust_unlock_unit:
          case TrophyConditionTypes.greed_unlock_unit:
          case TrophyConditionTypes.wrath_unlock_unit:
          case TrophyConditionTypes.gluttonny_unlock_unit:
          case TrophyConditionTypes.pride_unlock_unit:
            if (string.IsNullOrEmpty(this.sval_base))
              return this.ival;
            return 1;
          default:
            switch (type - 33)
            {
              case TrophyConditionTypes.none:
              case TrophyConditionTypes.gacha:
              case TrophyConditionTypes.multiplay:
              case TrophyConditionTypes.ability:
              case TrophyConditionTypes.soubi:
              case TrophyConditionTypes.buygold:
              case TrophyConditionTypes.vip:
                goto label_6;
              default:
                switch (type - 17)
                {
                  case TrophyConditionTypes.none:
                  case TrophyConditionTypes.getitem:
                  case TrophyConditionTypes.playerlv:
                  case TrophyConditionTypes.winelite:
                  case TrophyConditionTypes.multiplay:
                  case TrophyConditionTypes.buygold:
                    goto label_6;
                  case TrophyConditionTypes.winquest:
                    return 0;
                  default:
                    switch (type - 58)
                    {
                      case TrophyConditionTypes.none:
                      case TrophyConditionTypes.winquest:
                      case TrophyConditionTypes.getitem:
                      case TrophyConditionTypes.playerlv:
                        goto label_6;
                      default:
                        if (type != TrophyConditionTypes.playerlv)
                          return this.ival;
                        goto label_6;
                    }
                }
            }
        }
      }
    }
  }
}
