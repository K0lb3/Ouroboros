// Decompiled with JetBrains decompiler
// Type: SRPG.EventTrigger
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class EventTrigger
  {
    private EEventTrigger mTrigger;
    private EEventType mEventType;
    private EEventGimmick mGimmickType;
    private string mStrValue;
    private int mIntValue;
    private int mCount;

    public EEventTrigger Trigger
    {
      get
      {
        return this.mTrigger;
      }
    }

    public EEventType EventType
    {
      get
      {
        return this.mEventType;
      }
    }

    public EEventGimmick GimmickType
    {
      get
      {
        return this.mGimmickType;
      }
    }

    public string StrValue
    {
      get
      {
        return this.mStrValue;
      }
    }

    public int IntValue
    {
      get
      {
        return this.mIntValue;
      }
    }

    public int Count
    {
      get
      {
        return this.mCount;
      }
      set
      {
        this.mCount = value;
      }
    }

    public bool IsAdvantage
    {
      get
      {
        switch (this.mEventType)
        {
          case EEventType.Treasure:
          case EEventType.Gem:
            return true;
          default:
            return false;
        }
      }
    }

    public void Setup(EventTrigger src)
    {
      this.mTrigger = src.Trigger;
      this.mEventType = src.EventType;
      this.mGimmickType = src.GimmickType;
      this.mStrValue = src.StrValue;
      this.mIntValue = src.IntValue;
      this.mCount = src.Count;
    }

    public void CopyTo(EventTrigger dsc)
    {
      dsc.mTrigger = this.mTrigger;
      dsc.mEventType = this.mEventType;
      dsc.mGimmickType = this.mGimmickType;
      dsc.mStrValue = this.mStrValue;
      dsc.mIntValue = this.mIntValue;
      dsc.mCount = this.mCount;
    }

    public bool Deserialize(JSON_EventTrigger json)
    {
      if (json == null)
        return false;
      this.mTrigger = (EEventTrigger) json.trg;
      this.mEventType = (EEventType) json.type;
      this.mGimmickType = (EEventGimmick) json.detail;
      this.mStrValue = json.sval;
      this.mIntValue = json.ival;
      this.mCount = json.cnt;
      return true;
    }

    public void ExecuteGimmickEffect(Unit gimmick, Unit target, LogMapEvent log = null)
    {
      switch (this.GimmickType)
      {
        case EEventGimmick.Heal:
          int hp = (int) target.MaximumStatus.param.hp;
          int num = Math.Min(hp * this.IntValue / 100, hp - (int) target.CurrentStatus.param.hp);
          target.Heal(num);
          if (log == null)
            break;
          log.heal = num;
          break;
        case EEventGimmick.AtkUp:
        case EEventGimmick.DefUp:
        case EEventGimmick.MagUp:
        case EEventGimmick.MndUp:
        case EEventGimmick.RecUp:
        case EEventGimmick.SpdUp:
        case EEventGimmick.CriUp:
        case EEventGimmick.LukUp:
        case EEventGimmick.MovUp:
          BuffAttachment buff = new BuffAttachment();
          FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
          int gemsBuffValue = (int) fixParam.GemsBuffValue;
          int gemsBuffTurn = (int) fixParam.GemsBuffTurn;
          SkillParamCalcTypes skillParamCalcTypes = SkillParamCalcTypes.Scale;
          if (this.GimmickType == EEventGimmick.AtkUp)
            buff.status.param.atk = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.DefUp)
            buff.status.param.def = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.MagUp)
            buff.status.param.mag = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.MndUp)
            buff.status.param.mnd = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.RecUp)
            buff.status.param.rec = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.SpdUp)
            buff.status.param.spd = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.CriUp)
            buff.status.param.cri = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.LukUp)
            buff.status.param.luk = (OShort) gemsBuffValue;
          if (this.GimmickType == EEventGimmick.MovUp)
          {
            buff.status.param.mov = (OShort) 2;
            skillParamCalcTypes = SkillParamCalcTypes.Add;
          }
          buff.user = gimmick;
          buff.BuffType = BuffTypes.Buff;
          buff.CalcType = skillParamCalcTypes;
          buff.CheckTarget = target;
          buff.CheckTiming = EffectCheckTimings.ActionStart;
          buff.UseCondition = ESkillCondition.None;
          buff.IsPassive = (OBool) false;
          buff.turn = (OInt) gemsBuffTurn;
          target.SetBuffAttachment(buff, false);
          if (log == null)
            break;
          BattleCore.SetBuffBits(buff.status, ref log.buff, ref log.debuff);
          break;
      }
    }
  }
}
