// Decompiled with JetBrains decompiler
// Type: SRPG.BuffAttachment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class BuffAttachment
  {
    public BaseStatus status = new BaseStatus();
    private OInt mBuffType = (OInt) 0;
    private OInt mCalcType = (OInt) 0;
    public OInt UpBuffCount = (OInt) 0;
    public List<Unit> AagTargetLists = new List<Unit>();
    public Unit user;
    public Unit CheckTarget;
    private OInt mCheckTiming;
    private OInt mUseCondition;
    public OInt turn;
    public OBool IsPassive;
    public SkillData skill;
    public SkillEffectTargets skilltarget;
    private BuffEffectParam mParam;
    public bool IsNegativeValueIsBuff;
    public int DuplicateCount;
    public bool IsCalculated;
    public uint LinkageID;

    public BuffAttachment()
    {
    }

    public BuffAttachment(BuffEffectParam param)
    {
      this.mParam = param;
    }

    public EffectCheckTimings CheckTiming
    {
      get
      {
        return (EffectCheckTimings) (int) this.mCheckTiming;
      }
      set
      {
        this.mCheckTiming = (OInt) ((int) value);
      }
    }

    public ESkillCondition UseCondition
    {
      get
      {
        return (ESkillCondition) (int) this.mUseCondition;
      }
      set
      {
        this.mUseCondition = (OInt) ((int) value);
      }
    }

    public BuffEffectParam Param
    {
      get
      {
        return this.mParam;
      }
    }

    public BuffTypes BuffType
    {
      get
      {
        return (BuffTypes) (int) this.mBuffType;
      }
      set
      {
        this.mBuffType = (OInt) ((int) value);
      }
    }

    public SkillParamCalcTypes CalcType
    {
      get
      {
        return (SkillParamCalcTypes) (int) this.mCalcType;
      }
      set
      {
        this.mCalcType = (OInt) ((int) value);
      }
    }

    public void CopyTo(BuffAttachment dsc)
    {
      dsc.user = this.user;
      dsc.turn = this.turn;
      dsc.IsPassive = this.IsPassive;
      dsc.skill = this.skill;
      dsc.skilltarget = this.skilltarget;
      dsc.mParam = this.mParam;
      dsc.BuffType = this.BuffType;
      dsc.IsNegativeValueIsBuff = this.IsNegativeValueIsBuff;
      dsc.CalcType = this.CalcType;
      dsc.CheckTarget = this.CheckTarget;
      dsc.CheckTiming = this.CheckTiming;
      dsc.UseCondition = this.UseCondition;
      dsc.DuplicateCount = this.DuplicateCount;
      dsc.LinkageID = this.LinkageID;
      dsc.UpBuffCount = this.UpBuffCount;
      this.status.CopyTo(dsc.status);
    }

    public bool IsCalcLaterCondition
    {
      get
      {
        if (this.UseCondition != ESkillCondition.Dying)
          return this.UseCondition == ESkillCondition.JudgeHP;
        return true;
      }
    }
  }
}
