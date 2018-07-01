// Decompiled with JetBrains decompiler
// Type: SRPG.TrickMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class TrickMap
  {
    private Unit m_Owner;
    private GridMap<TrickMap.Data> m_DataMap;

    public TrickMap()
    {
      this.m_Owner = (Unit) null;
      this.m_DataMap = (GridMap<TrickMap.Data>) null;
    }

    public Unit owner
    {
      set
      {
        this.m_Owner = value;
      }
      get
      {
        return this.m_Owner;
      }
    }

    public void Initialize(int w, int h)
    {
      if (this.m_DataMap == null)
        this.m_DataMap = new GridMap<TrickMap.Data>(w, h);
      if (this.m_DataMap.w != w || this.m_DataMap.h != h)
        this.m_DataMap.resize(w, h);
      this.m_DataMap.fill((TrickMap.Data) null);
    }

    public void Clear()
    {
      if (this.m_DataMap == null)
        return;
      this.m_DataMap.fill((TrickMap.Data) null);
    }

    public void SetData(TrickMap.Data data)
    {
      if (this.m_DataMap == null)
        return;
      this.m_DataMap.set(data.x, data.y, data);
    }

    public TrickMap.Data GetData(int x, int y)
    {
      if (this.m_DataMap == null)
        return (TrickMap.Data) null;
      return this.m_DataMap.get(x, y);
    }

    public bool IsFailData(int x, int y)
    {
      TrickMap.Data data = this.GetData(x, y);
      if (data != null && data.IsVisual(this.owner) && data.IsVaild(this.owner))
        return data.IsFail(this.owner);
      return false;
    }

    public bool IsGoodData(int x, int y)
    {
      TrickMap.Data data = this.GetData(x, y);
      if (data != null && data.IsVisual(this.owner) && data.IsVaild(this.owner))
        return !data.IsFail(this.owner);
      return false;
    }

    public class Data
    {
      private TrickData data;
      private TrickParam param;

      public Data(TrickData _data)
      {
        this.data = _data;
        this.param = _data.TrickParam;
      }

      public int x
      {
        get
        {
          return (int) this.data.GridX;
        }
      }

      public int y
      {
        get
        {
          return (int) this.data.GridY;
        }
      }

      public int CalcHeal(Unit unit)
      {
        return this.data.calcHeal(unit);
      }

      public int CalcDamage(Unit unit)
      {
        return this.data.calcDamage(unit);
      }

      public bool IsVisual(Unit unit)
      {
        switch (this.param.VisualType)
        {
          case eTrickVisualType.PLAYER:
            EUnitSide eunitSide = EUnitSide.Enemy;
            if (this.data.CreateUnit != null)
              eunitSide = this.data.CreateUnit.Side;
            if (eunitSide == unit.Side)
              return true;
            break;
          case eTrickVisualType.ALL:
            return true;
        }
        return false;
      }

      public bool IsVaild(Unit unit)
      {
        bool flag = false;
        EUnitSide eunitSide = EUnitSide.Enemy;
        if (this.data.CreateUnit != null)
          eunitSide = this.data.CreateUnit.Side;
        switch (this.param.Target)
        {
          case ESkillTarget.Self:
            if (this.data.CreateUnit == unit)
            {
              flag = true;
              break;
            }
            break;
          case ESkillTarget.SelfSide:
            if (eunitSide == unit.Side)
            {
              flag = true;
              break;
            }
            break;
          case ESkillTarget.EnemySide:
            if (eunitSide != unit.Side)
            {
              flag = true;
              break;
            }
            break;
          case ESkillTarget.UnitAll:
            flag = true;
            break;
          case ESkillTarget.NotSelf:
            if (this.data.CreateUnit != unit)
            {
              flag = true;
              break;
            }
            break;
        }
        return flag && (this.data.CondEffect == null || this.data.CondEffect.CheckEnableCondTarget(unit));
      }

      public bool IsFail(Unit unit)
      {
        if (this.param.DamageType == eTrickDamageType.DAMAGE)
          return true;
        if (this.data.BuffEffect != null)
        {
          BuffEffect buffEffect = this.data.BuffEffect;
          for (int index = 0; index < buffEffect.targets.Count; ++index)
          {
            if (buffEffect.targets[index].buffType == BuffTypes.Debuff)
              return true;
          }
        }
        if (this.data.CondEffect != null)
        {
          CondEffectParam condEffectParam = this.data.CondEffect.param;
          if (condEffectParam.type == ConditionEffectTypes.CureCondition)
            return false;
          if (condEffectParam.type == ConditionEffectTypes.DisableCondition)
          {
            for (int index = 0; index < condEffectParam.conditions.Length; ++index)
            {
              if (!AIUtility.IsFailCondition(condEffectParam.conditions[index]))
                return true;
            }
          }
          else if (condEffectParam.type == ConditionEffectTypes.FailCondition || condEffectParam.type == ConditionEffectTypes.RandomFailCondition || condEffectParam.type == ConditionEffectTypes.ForcedFailCondition)
          {
            for (int index = 0; index < condEffectParam.conditions.Length; ++index)
            {
              if (AIUtility.IsFailCondition(condEffectParam.conditions[index]))
                return true;
            }
          }
        }
        return false;
      }

      public bool IsDamage()
      {
        return this.param.DamageType == eTrickDamageType.DAMAGE;
      }

      public bool IsHeal()
      {
        return this.param.DamageType == eTrickDamageType.HEAL;
      }

      public bool IsBuffEffect()
      {
        return this.data.BuffEffect != null;
      }

      public bool IsCondEffect()
      {
        return this.data.CondEffect != null;
      }

      public int GetBuffPriority(Unit self)
      {
        int val1 = 0;
        AIParam ai = self.AI;
        if (!this.IsBuffEffect() || ai == null || ai.BuffPriorities == null)
          return val1;
        BuffEffect buffEffect = this.data.BuffEffect;
        if (buffEffect != null && buffEffect.targets != null)
        {
          for (int index = 0; index < buffEffect.targets.Count; ++index)
          {
            int num = Array.IndexOf<ParamTypes>(ai.BuffPriorities, buffEffect.targets[index].paramType);
            if (num != -1)
              val1 = Math.Max(val1, ai.BuffPriorities.Length - num);
          }
        }
        return val1;
      }
    }
  }
}
