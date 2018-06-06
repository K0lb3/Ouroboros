// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGaugeMark
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class UnitGaugeMark : MonoBehaviour
  {
    private UnitGaugeMark.EGemIcon mGemIconType;
    public string EndAnimationName;
    public string EndTriggerName;
    public int EndTriggerValue;
    public UnitGauge UnitGauge;
    public UnitGaugeMark.EMarkType MarkType;
    public GameObject MapChest;
    public UnitGaugeGemIcon MapGem;
    private UnitGaugeMark.ObjectAnim[] mActiveMarks;
    private bool mIsGaugeUpdate;
    private bool mIsUnitDead;
    private bool mIsUseSkill;

    public UnitGaugeMark()
    {
      base.\u002Ector();
    }

    public bool IsGaugeUpdate
    {
      get
      {
        return this.mIsGaugeUpdate;
      }
      set
      {
        this.mIsGaugeUpdate = value;
      }
    }

    public bool IsUnitDead
    {
      get
      {
        return this.mIsUnitDead;
      }
      set
      {
        this.mIsUnitDead = value;
      }
    }

    public bool IsUseSkill
    {
      get
      {
        return this.mIsUseSkill;
      }
      set
      {
        this.mIsUseSkill = value;
      }
    }

    public bool IsUpdatable(UnitGaugeMark.EMarkType MarkType)
    {
      bool flag = false;
      if ((UnitGauge.GaugeMode) this.GetUnitGaugeMode() == UnitGauge.GaugeMode.Normal && !this.mIsGaugeUpdate && !this.mIsUnitDead)
        flag = true;
      return flag;
    }

    private int GetUnitGaugeMode()
    {
      return this.UnitGauge.Mode;
    }

    private GameObject CreateMarkObject()
    {
      GameObject gameObject = (GameObject) null;
      switch (this.MarkType)
      {
        case UnitGaugeMark.EMarkType.MapChest:
          if (Object.op_Implicit((Object) this.MapChest))
          {
            gameObject = Object.Instantiate((Object) this.MapChest, ((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_rotation()) as GameObject;
            break;
          }
          break;
        case UnitGaugeMark.EMarkType.MapGem:
          if (Object.op_Implicit((Object) this.MapGem))
          {
            UnitGaugeGemIcon unitGaugeGemIcon = Object.Instantiate((Object) this.MapGem, ((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_rotation()) as UnitGaugeGemIcon;
            unitGaugeGemIcon.IconImages.ImageIndex = (int) this.mGemIconType;
            gameObject = ((Component) unitGaugeGemIcon).get_gameObject();
            break;
          }
          break;
      }
      return gameObject;
    }

    public void SetEndAnimation(UnitGaugeMark.EMarkType Type)
    {
      if (this.mActiveMarks == null)
        return;
      UnitGaugeMark.ObjectAnim mActiveMark = this.mActiveMarks[(int) Type];
      if (mActiveMark == null || Object.op_Equality((Object) mActiveMark.Animator, (Object) null) || mActiveMark.IsEnd)
        return;
      mActiveMark.IsEnd = true;
      mActiveMark.Animator.SetInteger(this.EndTriggerName, this.EndTriggerValue);
    }

    public void SetEndAnimationAll()
    {
      if (this.mActiveMarks == null)
        return;
      for (int index = 0; index < this.mActiveMarks.Length; ++index)
        this.SetEndAnimation((UnitGaugeMark.EMarkType) index);
    }

    public void SetEndByUnitType(EUnitType Type)
    {
      switch (Type)
      {
        case EUnitType.Treasure:
          this.SetEndAnimation(UnitGaugeMark.EMarkType.MapChest);
          break;
        case EUnitType.Gem:
          this.SetEndAnimation(UnitGaugeMark.EMarkType.MapGem);
          break;
      }
    }

    public void ChangeAnimationByUnitType(EUnitType Type)
    {
      switch (Type)
      {
        case EUnitType.Treasure:
          this.MarkType = UnitGaugeMark.EMarkType.MapChest;
          break;
        case EUnitType.Gem:
          this.MarkType = UnitGaugeMark.EMarkType.MapGem;
          break;
      }
    }

    public void SetGemIcon(EEventGimmick EventType)
    {
      this.MarkType = UnitGaugeMark.EMarkType.MapGem;
      EEventGimmick eeventGimmick = EventType;
      switch (eeventGimmick)
      {
        case EEventGimmick.CriUp:
          this.mGemIconType = UnitGaugeMark.EGemIcon.CriUp;
          break;
        case EEventGimmick.MovUp:
          this.mGemIconType = UnitGaugeMark.EGemIcon.MovUp;
          break;
        default:
          if (eeventGimmick == EEventGimmick.Heal)
          {
            this.mGemIconType = UnitGaugeMark.EGemIcon.Heal;
            break;
          }
          this.mGemIconType = UnitGaugeMark.EGemIcon.Normal;
          break;
      }
    }

    public void DeleteIconAll()
    {
      if (this.mActiveMarks == null)
        return;
      for (int index = 0; index < this.mActiveMarks.Length; ++index)
      {
        UnitGaugeMark.ObjectAnim mActiveMark = this.mActiveMarks[index];
        if (mActiveMark != null)
        {
          mActiveMark.Release();
          this.mActiveMarks[index] = (UnitGaugeMark.ObjectAnim) null;
        }
      }
    }

    private void Start()
    {
      this.mActiveMarks = new UnitGaugeMark.ObjectAnim[Enum.GetNames(typeof (UnitGaugeMark.EMarkType)).Length];
    }

    private void Update()
    {
      if (this.mActiveMarks == null)
        return;
      for (int index = 0; index < this.mActiveMarks.Length; ++index)
      {
        UnitGaugeMark.ObjectAnim mActiveMark = this.mActiveMarks[index];
        if (mActiveMark != null)
        {
          if (!this.IsUpdatable((UnitGaugeMark.EMarkType) index))
          {
            mActiveMark.Object.SetActive(false);
          }
          else
          {
            if (!mActiveMark.Object.get_activeInHierarchy())
            {
              this.mActiveMarks[index] = (UnitGaugeMark.ObjectAnim) null;
              mActiveMark.Release();
              break;
            }
            if (mActiveMark.Object.get_activeInHierarchy() && Object.op_Inequality((Object) mActiveMark.Animator, (Object) null))
            {
              AnimatorStateInfo animatorStateInfo = mActiveMark.Animator.GetCurrentAnimatorStateInfo(0);
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              if (((AnimatorStateInfo) @animatorStateInfo).IsName(this.EndAnimationName) && !mActiveMark.Animator.IsInTransition(0) && (double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() >= 1.0)
              {
                this.mActiveMarks[index] = (UnitGaugeMark.ObjectAnim) null;
                mActiveMark.Release();
              }
            }
          }
        }
      }
      if (this.IsUpdatable(this.MarkType) && this.MarkType != UnitGaugeMark.EMarkType.None && this.mActiveMarks[(int) this.MarkType] == null)
      {
        GameObject markObject = this.CreateMarkObject();
        if (Object.op_Inequality((Object) markObject, (Object) null))
        {
          markObject.get_transform().SetParent(((Component) this).get_transform());
          markObject.get_transform().SetAsFirstSibling();
          Animator component = (Animator) markObject.GetComponent<Animator>();
          component.SetInteger(this.EndTriggerName, 0);
          this.mActiveMarks[(int) this.MarkType] = new UnitGaugeMark.ObjectAnim(markObject, component);
        }
      }
      this.MarkType = UnitGaugeMark.EMarkType.None;
    }

    private class ObjectAnim
    {
      public GameObject Object;
      public Animator Animator;
      public bool IsEnd;

      public ObjectAnim(GameObject Object, Animator Animator)
      {
        this.Object = Object;
        this.Animator = Animator;
        this.IsEnd = false;
      }

      public void Release()
      {
        this.Animator = (Animator) null;
        if (!Object.op_Inequality((Object) this.Object, (Object) null))
          return;
        Object.Destroy((Object) this.Object);
      }
    }

    [Serializable]
    public enum EMarkType
    {
      None,
      MapChest,
      MapGem,
    }

    [Serializable]
    public enum EGemIcon
    {
      Normal,
      Heal,
      CriUp,
      MovUp,
    }
  }
}
