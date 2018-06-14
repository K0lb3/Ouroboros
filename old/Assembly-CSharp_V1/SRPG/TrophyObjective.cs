// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjective
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class TrophyObjective
  {
    public TrophyParam Param;
    public int index;
    public TrophyConditionTypes type;
    public string sval;
    public int ival;

    public bool Deserialize(JSON_TrophyObjective json)
    {
      if (json == null)
        return false;
      if (string.IsNullOrEmpty(json.type))
        return false;
      try
      {
        this.type = (TrophyConditionTypes) Enum.Parse(typeof (TrophyConditionTypes), json.type, true);
      }
      catch (Exception ex)
      {
        return false;
      }
      this.sval = json.sval;
      this.ival = json.ival;
      return true;
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
label_4:
            return 1;
          default:
            switch (type - 16)
            {
              case TrophyConditionTypes.winquest:
              case TrophyConditionTypes.playerlv:
              case TrophyConditionTypes.winelite:
              case TrophyConditionTypes.winevent:
              case TrophyConditionTypes.ability:
              case TrophyConditionTypes.vip:
                goto label_4;
              case TrophyConditionTypes.killenemy:
                return 0;
              default:
                if (type != TrophyConditionTypes.playerlv)
                  return this.ival;
                goto label_4;
            }
        }
      }
    }
  }
}
