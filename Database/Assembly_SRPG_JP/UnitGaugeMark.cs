// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGaugeMark
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
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
    private List<UnitGaugeMark.ObjectAnim> mActiveMarkLists;
    private bool mIsGaugeUpdate;
    private bool mIsUnitDead;
    private bool mIsUseSkill;

    public UnitGaugeMark()
    {
      base.\u002Ector();
    }

    public UnitGaugeMark.EGemIcon GemIconType
    {
      get
      {
        return this.mGemIconType;
      }
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
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.MapChest))
          {
            gameObject = UnityEngine.Object.Instantiate((UnityEngine.Object) this.MapChest, ((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_rotation()) as GameObject;
            break;
          }
          break;
        case UnitGaugeMark.EMarkType.MapGem:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.MapGem))
          {
            UnitGaugeGemIcon unitGaugeGemIcon = UnityEngine.Object.Instantiate((UnityEngine.Object) this.MapGem, ((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_rotation()) as UnitGaugeGemIcon;
            unitGaugeGemIcon.IconImages.ImageIndex = (int) this.mGemIconType;
            gameObject = ((Component) unitGaugeGemIcon).get_gameObject();
            break;
          }
          break;
      }
      return gameObject;
    }

    private void SetEndAnimation(UnitGaugeMark.ObjectAnim mark)
    {
      if (mark == null || UnityEngine.Object.op_Equality((UnityEngine.Object) mark.Animator, (UnityEngine.Object) null) || mark.IsEnd)
        return;
      mark.IsEnd = true;
      mark.Animator.SetInteger(this.EndTriggerName, this.EndTriggerValue);
    }

    public void SetEndAnimation(UnitGaugeMark.EMarkType Type)
    {
      using (List<UnitGaugeMark.ObjectAnim>.Enumerator enumerator = this.mActiveMarkLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          UnitGaugeMark.ObjectAnim current = enumerator.Current;
          if (current.MarkType == Type)
            this.SetEndAnimation(current);
        }
      }
    }

    public void SetEndAnimationAll()
    {
      using (List<UnitGaugeMark.ObjectAnim>.Enumerator enumerator = this.mActiveMarkLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.SetEndAnimation(enumerator.Current);
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
      this.mGemIconType = UnitGaugeMark.EGemIcon.Normal;
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
      int num;
      for (int index1 = 0; index1 < this.mActiveMarkLists.Count; index1 = num + 1)
      {
        UnitGaugeMark.ObjectAnim mActiveMarkList = this.mActiveMarkLists[index1];
        if (mActiveMarkList != null)
          mActiveMarkList.Release();
        List<UnitGaugeMark.ObjectAnim> mActiveMarkLists = this.mActiveMarkLists;
        int index2 = index1;
        num = index2 - 1;
        mActiveMarkLists.RemoveAt(index2);
      }
    }

    private void Update()
    {
      for (int index = 0; index < this.mActiveMarkLists.Count; ++index)
      {
        UnitGaugeMark.ObjectAnim mActiveMarkList = this.mActiveMarkLists[index];
        if (!this.IsUpdatable(mActiveMarkList.MarkType))
          mActiveMarkList.Object.SetActive(false);
        else if (!mActiveMarkList.Object.get_activeInHierarchy())
        {
          mActiveMarkList.Release();
          this.mActiveMarkLists.RemoveAt(index--);
        }
        else if (mActiveMarkList.Object.get_activeInHierarchy() && UnityEngine.Object.op_Inequality((UnityEngine.Object) mActiveMarkList.Animator, (UnityEngine.Object) null))
        {
          AnimatorStateInfo animatorStateInfo = mActiveMarkList.Animator.GetCurrentAnimatorStateInfo(0);
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if (((AnimatorStateInfo) @animatorStateInfo).IsName(this.EndAnimationName) && !mActiveMarkList.Animator.IsInTransition(0) && (double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() >= 1.0)
          {
            mActiveMarkList.Release();
            this.mActiveMarkLists.RemoveAt(index--);
          }
        }
      }
      if (this.MarkType != UnitGaugeMark.EMarkType.None && this.IsUpdatable(this.MarkType) && this.mActiveMarkLists.Find((Predicate<UnitGaugeMark.ObjectAnim>) (am =>
      {
        if (am.MarkType == this.MarkType)
          return am.GemIconType == this.mGemIconType;
        return false;
      })) == null)
      {
        GameObject markObject = this.CreateMarkObject();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) markObject, (UnityEngine.Object) null))
        {
          markObject.get_transform().SetParent(((Component) this).get_transform());
          markObject.get_transform().SetAsLastSibling();
          Animator component = (Animator) markObject.GetComponent<Animator>();
          component.SetInteger(this.EndTriggerName, 0);
          this.mActiveMarkLists.Add(new UnitGaugeMark.ObjectAnim(markObject, component, this.MarkType, this.mGemIconType));
        }
      }
      this.MarkType = UnitGaugeMark.EMarkType.None;
    }

    private class ObjectAnim
    {
      public GameObject Object;
      public Animator Animator;
      public bool IsEnd;
      public UnitGaugeMark.EMarkType MarkType;
      public UnitGaugeMark.EGemIcon GemIconType;

      public ObjectAnim(GameObject Object, Animator Animator, UnitGaugeMark.EMarkType mark_type, UnitGaugeMark.EGemIcon gem_icon)
      {
        this.Object = Object;
        this.Animator = Animator;
        this.MarkType = mark_type;
        this.GemIconType = gem_icon;
        this.IsEnd = false;
      }

      public void Release()
      {
        this.Animator = (Animator) null;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Object, (UnityEngine.Object) null))
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.Object);
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
