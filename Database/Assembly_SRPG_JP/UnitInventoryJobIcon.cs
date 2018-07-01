// Decompiled with JetBrains decompiler
// Type: SRPG.UnitInventoryJobIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitInventoryJobIcon : MonoBehaviour
  {
    private static readonly string ANIM_PARAM_JOB_ICON_UNLOCK_BOOL = "unlocked";
    private static readonly string ANIM_PARAM_JOB_ICON_HILIT_BOOL = "hilit";
    private static readonly string ANIM_PARAM_JOB_ICON_DISABLED_BOOL = "disabled";
    private static readonly string ANIM_PARAM_JOB_ICON_ON_BOOL = "on";
    private float SINGLE_ICON_HALF_SIZE_COEF;
    public UnitInventoryJobIcon.eViewMode mMode;
    [SerializeField]
    private SRPG_Button base_job_icon_button;
    [SerializeField]
    private SRPG_Button cc_job_icon_button;
    [SerializeField]
    private GameObject active_job_indicator;

    public UnitInventoryJobIcon()
    {
      base.\u002Ector();
    }

    public SRPG_Button BaseJobIconButton
    {
      get
      {
        return this.base_job_icon_button;
      }
    }

    public SRPG_Button CcJobButton
    {
      get
      {
        return this.cc_job_icon_button;
      }
    }

    public float Width
    {
      get
      {
        float num = 0.0f;
        if (((Component) this.base_job_icon_button).get_gameObject().get_activeSelf())
        {
          RectTransform transform = ((Component) this.base_job_icon_button).get_transform() as RectTransform;
          num += (float) transform.get_sizeDelta().x;
        }
        if (((Component) this.cc_job_icon_button).get_gameObject().get_activeSelf())
        {
          RectTransform transform = ((Component) this.cc_job_icon_button).get_transform() as RectTransform;
          num += (float) transform.get_sizeDelta().x;
        }
        return num;
      }
    }

    public float HalfWidth
    {
      get
      {
        if (((Component) this.cc_job_icon_button).get_gameObject().get_activeSelf())
          return this.Width * 0.5f;
        return this.Width * this.SINGLE_ICON_HALF_SIZE_COEF;
      }
    }

    public bool IsSingleIcon
    {
      get
      {
        return !((Component) this.cc_job_icon_button).get_gameObject().get_activeSelf();
      }
    }

    public void ResetParam()
    {
      ((Object) ((Component) this.base_job_icon_button).get_gameObject()).set_name("-1");
      ((Object) ((Component) this.cc_job_icon_button).get_gameObject()).set_name("-1");
      Animator component = (Animator) ((Component) this.base_job_icon_button).GetComponent<Animator>();
      component.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, false);
      component.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_ON_BOOL, false);
    }

    public void SetParam(UnitData unit, int base_job_index, int cc_job_index, bool is_avtive_job, UnitInventoryJobIcon.eViewMode mode)
    {
      this.mMode = mode;
      if (this.mMode == UnitInventoryJobIcon.eViewMode.UNIT_DETAIL)
        this.active_job_indicator.SetActive(is_avtive_job);
      else if (Object.op_Inequality((Object) this.active_job_indicator, (Object) null))
        this.active_job_indicator.SetActive(false);
      if (base_job_index <= -1 && cc_job_index >= 0)
      {
        if (!Object.op_Inequality((Object) this.base_job_icon_button, (Object) null))
          return;
        ((Component) this.cc_job_icon_button).get_gameObject().SetActive(false);
        this.SetIcon(this.base_job_icon_button, unit, cc_job_index, false);
      }
      else
      {
        if (Object.op_Inequality((Object) this.base_job_icon_button, (Object) null))
        {
          ((Component) this.base_job_icon_button).get_gameObject().SetActive(false);
          if (base_job_index >= 0)
            this.SetIcon(this.base_job_icon_button, unit, base_job_index, false);
        }
        if (!Object.op_Inequality((Object) this.cc_job_icon_button, (Object) null))
          return;
        ((Component) this.cc_job_icon_button).get_gameObject().SetActive(false);
        if (cc_job_index < 0)
          return;
        this.SetIcon(this.cc_job_icon_button, unit, cc_job_index, true);
      }
    }

    private void SetIcon(SRPG_Button slot, UnitData unit, int job_data_index, bool is_cc_icon)
    {
      ((Object) ((Component) slot).get_gameObject()).set_name(job_data_index.ToString());
      JobData job = unit.Jobs[job_data_index];
      bool isActivated = job.IsActivated;
      bool is_hilit = !job.IsActivated && (unit.CheckJobUnlock(job_data_index, false) || unit.CheckJobRankUpAllEquip(job_data_index, true));
      bool is_disabled = !job.IsActivated && !unit.CheckJobUnlockable(job_data_index);
      ((Component) slot).get_gameObject().SetActive(true);
      this.SetAnimationParam((Animator) ((Component) slot).GetComponent<Animator>(), job.IsActivated, isActivated, is_disabled, is_hilit);
      DataSource.Bind<JobData>(((Component) slot).get_gameObject(), unit.Jobs[job_data_index]);
      GameParameter.UpdateAll(((Component) slot).get_gameObject());
      if (this.mMode != UnitInventoryJobIcon.eViewMode.UNIT_VIEWER || !is_cc_icon)
        return;
      ((Component) slot).get_gameObject().SetActive(false);
    }

    private void SetAnimationParam(Animator anim, bool is_activated, bool unlockable, bool is_disabled, bool is_hilit)
    {
      if (Object.op_Equality((Object) anim, (Object) null))
        return;
      if (this.mMode == UnitInventoryJobIcon.eViewMode.UNIT_DETAIL)
      {
        anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, unlockable);
        anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_HILIT_BOOL, is_hilit);
        anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_DISABLED_BOOL, is_disabled);
      }
      if (this.mMode != UnitInventoryJobIcon.eViewMode.UNIT_VIEWER)
        return;
      anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, is_activated);
      if (!is_activated)
        anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, unlockable);
      anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_DISABLED_BOOL, is_disabled);
    }

    public void SetSelectIconAnim(bool is_on)
    {
      ((Animator) ((Component) this.BaseJobIconButton).GetComponent<Animator>()).SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_ON_BOOL, is_on);
    }

    public bool IsDisabledBaseJobIcon()
    {
      return ((Animator) ((Component) this.BaseJobIconButton).GetComponent<Animator>()).GetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_DISABLED_BOOL);
    }

    public enum eViewMode
    {
      NONE,
      UNIT_DETAIL,
      UNIT_VIEWER,
    }
  }
}
