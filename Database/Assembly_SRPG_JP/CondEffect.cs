// Decompiled with JetBrains decompiler
// Type: SRPG.CondEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class CondEffect
  {
    public CondEffectParam param;
    public OInt turn;
    public OInt rate;
    public OInt value;

    public bool IsCurse
    {
      get
      {
        if (this.param != null)
          return (int) this.param.curse != 0;
        return false;
      }
    }

    public static CondEffect CreateCondEffect(CondEffectParam param, int rank, int rankcap)
    {
      if (param == null || param.conditions == null || param.conditions.Length == 0)
        return (CondEffect) null;
      CondEffect condEffect = new CondEffect();
      condEffect.param = param;
      condEffect.UpdateCurrentValues(rank, rankcap);
      return condEffect;
    }

    public void UpdateCurrentValues(int rank, int rankcap)
    {
      if (this.param == null || this.param.conditions == null || this.param.conditions.Length == 0)
      {
        this.Clear();
      }
      else
      {
        this.rate = (OInt) this.GetRankValue(rank, rankcap, (int) this.param.rate_ini, (int) this.param.rate_max);
        this.turn = (OInt) this.GetRankValue(rank, rankcap, (int) this.param.turn_ini, (int) this.param.turn_max);
        this.value = (OInt) this.GetRankValue(rank, rankcap, (int) this.param.value_ini, (int) this.param.value_max);
      }
    }

    private int GetRankValue(int rank, int rankcap, int ini, int max)
    {
      int num1 = rankcap - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      int num3 = (max - ini) * 100 / num1;
      return ini + num3 * num2 / 100;
    }

    public bool ContainsCondition(EUnitCondition condition)
    {
      if (this.param == null || this.param.conditions == null)
        return false;
      for (int index = 0; index < this.param.conditions.Length; ++index)
      {
        if (this.param.conditions[index] == condition)
          return true;
      }
      return false;
    }

    private void Clear()
    {
      this.param = (CondEffectParam) null;
      this.rate = (OInt) 0;
      this.turn = (OInt) 0;
      this.value = (OInt) 0;
    }

    public bool CheckEnableCondTarget(Unit target)
    {
      if (this.param == null)
        return false;
      bool flag = true;
      if (this.param.sex != ESex.Unknown)
        flag &= this.param.sex == target.UnitParam.sex;
      if (this.param.elem != EElement.None)
        flag &= this.param.elem == target.Element;
      if (!string.IsNullOrEmpty(this.param.job) && target.Job != null)
        flag &= this.param.job == target.Job.Param.origin;
      if (!string.IsNullOrEmpty(this.param.buki) && target.Job != null)
        flag &= this.param.job == target.Job.Param.buki;
      if (!string.IsNullOrEmpty(this.param.birth))
        flag &= this.param.birth == (string) target.UnitParam.birth;
      return flag;
    }
  }
}
