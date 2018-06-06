// Decompiled with JetBrains decompiler
// Type: SRPG.BuffEffectParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class BuffEffectParam
  {
    public string iname;
    public string job;
    public string buki;
    public string birth;
    public ESex sex;
    public int elem;
    public ESkillCondition cond;
    public OInt rate;
    public OInt turn;
    public EffectCheckTargets chk_target;
    public EffectCheckTimings chk_timing;
    public EAppType mAppType;
    public int mAppMct;
    public EEffRange mEffRange;
    public BuffEffectParam.Buff[] buffs;

    public BuffEffectParam.Buff this[ParamTypes type]
    {
      get
      {
        if (this.buffs != null)
          return Array.Find<BuffEffectParam.Buff>(this.buffs, (Predicate<BuffEffectParam.Buff>) (p => p.type == type));
        return (BuffEffectParam.Buff) null;
      }
    }

    public bool Deserialize(JSON_BuffEffectParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.job = json.job;
      this.buki = json.buki;
      this.birth = json.birth;
      this.sex = (ESex) json.sex;
      this.elem = Convert.ToInt32(json.elem.ToString("d7"), 2);
      this.rate = (OInt) json.rate;
      this.turn = (OInt) json.turn;
      this.chk_target = (EffectCheckTargets) json.chktgt;
      this.chk_timing = (EffectCheckTimings) json.timing;
      this.cond = (ESkillCondition) json.cond;
      this.mAppType = (EAppType) json.app_type;
      this.mAppMct = json.app_mct;
      this.mEffRange = (EEffRange) json.eff_range;
      ParamTypes type1 = (ParamTypes) json.type1;
      ParamTypes type2 = (ParamTypes) json.type2;
      ParamTypes type3 = (ParamTypes) json.type3;
      ParamTypes type4 = (ParamTypes) json.type4;
      ParamTypes type5 = (ParamTypes) json.type5;
      ParamTypes type6 = (ParamTypes) json.type6;
      ParamTypes type7 = (ParamTypes) json.type7;
      ParamTypes type8 = (ParamTypes) json.type8;
      ParamTypes type9 = (ParamTypes) json.type9;
      ParamTypes type10 = (ParamTypes) json.type10;
      int length = 0;
      if (type1 != ParamTypes.None)
        ++length;
      if (type2 != ParamTypes.None)
        ++length;
      if (type3 != ParamTypes.None)
        ++length;
      if (type4 != ParamTypes.None)
        ++length;
      if (type5 != ParamTypes.None)
        ++length;
      if (type6 != ParamTypes.None)
        ++length;
      if (type7 != ParamTypes.None)
        ++length;
      if (type8 != ParamTypes.None)
        ++length;
      if (type9 != ParamTypes.None)
        ++length;
      if (type10 != ParamTypes.None)
        ++length;
      if (length > 0)
      {
        this.buffs = new BuffEffectParam.Buff[length];
        int index = 0;
        if (type1 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type1;
          this.buffs[index].value_ini = (OInt) json.vini1;
          this.buffs[index].value_max = (OInt) json.vmax1;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc1;
          ++index;
        }
        if (type2 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type2;
          this.buffs[index].value_ini = (OInt) json.vini2;
          this.buffs[index].value_max = (OInt) json.vmax2;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc2;
          ++index;
        }
        if (type3 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type3;
          this.buffs[index].value_ini = (OInt) json.vini3;
          this.buffs[index].value_max = (OInt) json.vmax3;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc3;
          ++index;
        }
        if (type4 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type4;
          this.buffs[index].value_ini = (OInt) json.vini4;
          this.buffs[index].value_max = (OInt) json.vmax4;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc4;
          ++index;
        }
        if (type5 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type5;
          this.buffs[index].value_ini = (OInt) json.vini5;
          this.buffs[index].value_max = (OInt) json.vmax5;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc5;
          ++index;
        }
        if (type6 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type6;
          this.buffs[index].value_ini = (OInt) json.vini6;
          this.buffs[index].value_max = (OInt) json.vmax6;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc6;
          ++index;
        }
        if (type7 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type7;
          this.buffs[index].value_ini = (OInt) json.vini7;
          this.buffs[index].value_max = (OInt) json.vmax7;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc7;
          ++index;
        }
        if (type8 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type8;
          this.buffs[index].value_ini = (OInt) json.vini8;
          this.buffs[index].value_max = (OInt) json.vmax8;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc8;
          ++index;
        }
        if (type9 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type9;
          this.buffs[index].value_ini = (OInt) json.vini9;
          this.buffs[index].value_max = (OInt) json.vmax9;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc9;
          ++index;
        }
        if (type10 != ParamTypes.None)
        {
          this.buffs[index] = new BuffEffectParam.Buff();
          this.buffs[index].type = type10;
          this.buffs[index].value_ini = (OInt) json.vini10;
          this.buffs[index].value_max = (OInt) json.vmax10;
          this.buffs[index].calc = (SkillParamCalcTypes) json.calc10;
          int num = index + 1;
        }
      }
      return true;
    }

    public class Buff
    {
      public ParamTypes type;
      public OInt value_ini;
      public OInt value_max;
      public SkillParamCalcTypes calc;
    }
  }
}
