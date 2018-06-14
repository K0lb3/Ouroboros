// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
          case TrophyConditionTypes.makeabilitylevel:
          case TrophyConditionTypes.winmultimore:
          case TrophyConditionTypes.winmultiless:
          case TrophyConditionTypes.collectunits:
          case TrophyConditionTypes.totaljoblv11:
          case TrophyConditionTypes.totalunitlvs:
          case TrophyConditionTypes.childrencomp:
label_6:
            return 1;
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
                    switch (type - 78)
                    {
                      case TrophyConditionTypes.none:
                      case TrophyConditionTypes.killenemy:
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
