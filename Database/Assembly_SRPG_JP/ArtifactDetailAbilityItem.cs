// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactDetailAbilityItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactDetailAbilityItem : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IHoldGesture
  {
    [SerializeField]
    private GameObject mUnitIcon;
    [SerializeField]
    private GameObject mJobIcon;
    [SerializeField]
    private GameObject mDisableMask;
    [SerializeField]
    private GameObject mLockImage;
    [SerializeField]
    private Text mConditionsText;
    [SerializeField]
    private GameObject mLineVertical;
    [SerializeField]
    private GameObject mLineHorizontal;
    private AbilityParam mAbilityParam;
    private bool mIsEnable;
    private bool mHasDeriveAbility;

    public ArtifactDetailAbilityItem()
    {
      base.\u002Ector();
    }

    public bool IsEnable
    {
      get
      {
        return this.mIsEnable;
      }
    }

    public bool HasDeriveAbility
    {
      get
      {
        return this.mHasDeriveAbility;
      }
    }

    public void Setup(AbilityParam ability_param, bool is_derive_ability, bool is_enable, bool is_locked, bool has_derive_ability)
    {
      this.mAbilityParam = ability_param;
      this.mIsEnable = is_enable;
      this.mHasDeriveAbility = has_derive_ability;
      if (is_locked)
        this.mLockImage.SetActive(is_locked);
      else
        this.mDisableMask.SetActive(!this.mIsEnable);
      if (Object.op_Inequality((Object) this.mUnitIcon, (Object) null))
        this.mUnitIcon.SetActive(DataSource.FindDataOfClass<UnitParam>(((Component) this).get_gameObject(), (UnitParam) null) != null);
      if (Object.op_Inequality((Object) this.mJobIcon, (Object) null))
        this.mJobIcon.SetActive(DataSource.FindDataOfClass<JobParam>(((Component) this).get_gameObject(), (JobParam) null) != null);
      if (!Object.op_Inequality((Object) this.mConditionsText, (Object) null))
        return;
      AbilityConditions abilityConditions = new AbilityConditions();
      abilityConditions.Setup(ability_param, MonoSingleton<GameManager>.Instance.MasterParam);
      this.mConditionsText.set_text(abilityConditions.MakeConditionsText());
    }

    public void SetActive(bool is_active)
    {
      if (this.mAbilityParam == null)
        return;
      ((Component) this).get_gameObject().SetActive(is_active);
    }

    public void SetActiveLine(bool is_active)
    {
      if (Object.op_Inequality((Object) this.mLineVertical, (Object) null))
        this.mLineVertical.SetActive(is_active);
      if (!Object.op_Inequality((Object) this.mLineHorizontal, (Object) null))
        return;
      this.mLineHorizontal.SetActive(is_active);
    }

    public void DestoryLastLine()
    {
      if (Object.op_Equality((Object) this.mLineVertical, (Object) null))
        return;
      Object.Destroy((Object) this.mLineVertical);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
    }

    public void OnPointerHoldStart()
    {
      ArtifactDetailWindow componentInParent = (ArtifactDetailWindow) ((Component) this).GetComponentInParent<ArtifactDetailWindow>();
      if (!Object.op_Inequality((Object) componentInParent, (Object) null))
        return;
      AbilityDetailWindow.SetBindAbility(this.mAbilityParam);
      componentInParent.OpenAbilityDetail();
    }

    public void OnPointerHoldEnd()
    {
    }
  }
}
