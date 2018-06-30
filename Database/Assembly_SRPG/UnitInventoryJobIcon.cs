namespace SRPG
{
    using System;
    using UnityEngine;

    public class UnitInventoryJobIcon : MonoBehaviour
    {
        private static readonly string ANIM_PARAM_JOB_ICON_UNLOCK_BOOL;
        private static readonly string ANIM_PARAM_JOB_ICON_HILIT_BOOL;
        private static readonly string ANIM_PARAM_JOB_ICON_DISABLED_BOOL;
        private static readonly string ANIM_PARAM_JOB_ICON_ON_BOOL;
        private float SINGLE_ICON_HALF_SIZE_COEF;
        public eViewMode mMode;
        [SerializeField]
        private SRPG_Button base_job_icon_button;
        [SerializeField]
        private SRPG_Button cc_job_icon_button;
        [SerializeField]
        private GameObject active_job_indicator;

        static UnitInventoryJobIcon()
        {
            ANIM_PARAM_JOB_ICON_UNLOCK_BOOL = "unlocked";
            ANIM_PARAM_JOB_ICON_HILIT_BOOL = "hilit";
            ANIM_PARAM_JOB_ICON_DISABLED_BOOL = "disabled";
            ANIM_PARAM_JOB_ICON_ON_BOOL = "on";
            return;
        }

        public UnitInventoryJobIcon()
        {
            this.SINGLE_ICON_HALF_SIZE_COEF = 0.67f;
            base..ctor();
            return;
        }

        public bool IsDisabledBaseJobIcon()
        {
            Animator animator;
            return this.BaseJobIconButton.GetComponent<Animator>().GetBool(ANIM_PARAM_JOB_ICON_DISABLED_BOOL);
        }

        public void ResetParam()
        {
            Animator animator;
            this.base_job_icon_button.get_gameObject().set_name("-1");
            this.cc_job_icon_button.get_gameObject().set_name("-1");
            animator = this.base_job_icon_button.GetComponent<Animator>();
            animator.SetBool(ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, 0);
            animator.SetBool(ANIM_PARAM_JOB_ICON_ON_BOOL, 0);
            return;
        }

        private void SetAnimationParam(Animator anim, bool is_activated, bool unlockable, bool is_disabled, bool is_hilit)
        {
            if ((anim == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (this.mMode != 1)
            {
                goto Label_003F;
            }
            anim.SetBool(ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, unlockable);
            anim.SetBool(ANIM_PARAM_JOB_ICON_HILIT_BOOL, is_hilit);
            anim.SetBool(ANIM_PARAM_JOB_ICON_DISABLED_BOOL, is_disabled);
        Label_003F:
            if (this.mMode != 2)
            {
                goto Label_0076;
            }
            anim.SetBool(ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, is_activated);
            if (is_activated != null)
            {
                goto Label_0069;
            }
            anim.SetBool(ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, unlockable);
        Label_0069:
            anim.SetBool(ANIM_PARAM_JOB_ICON_DISABLED_BOOL, is_disabled);
        Label_0076:
            return;
        }

        private unsafe void SetIcon(SRPG_Button slot, UnitData unit, int job_data_index, bool is_cc_icon)
        {
            JobData data;
            bool flag;
            bool flag2;
            bool flag3;
            Animator animator;
            slot.get_gameObject().set_name(&job_data_index.ToString());
            data = unit.Jobs[job_data_index];
            flag = data.IsActivated;
            flag2 = (data.IsActivated != null) ? 0 : ((unit.CheckJobUnlock(job_data_index, 0) != null) ? 1 : unit.CheckJobRankUpAllEquip(job_data_index, 1));
            flag3 = (data.IsActivated != null) ? 0 : (unit.CheckJobUnlockable(job_data_index) == 0);
            slot.get_gameObject().SetActive(1);
            animator = slot.GetComponent<Animator>();
            this.SetAnimationParam(animator, data.IsActivated, flag, flag3, flag2);
            DataSource.Bind<JobData>(slot.get_gameObject(), unit.Jobs[job_data_index]);
            GameParameter.UpdateAll(slot.get_gameObject());
            if (this.mMode != 2)
            {
                goto Label_00C4;
            }
            if (is_cc_icon == null)
            {
                goto Label_00C4;
            }
            slot.get_gameObject().SetActive(0);
        Label_00C4:
            return;
        }

        public void SetParam(UnitData unit, int base_job_index, int cc_job_index, bool is_avtive_job, eViewMode mode)
        {
            this.mMode = mode;
            if (this.mMode != 1)
            {
                goto Label_0026;
            }
            this.active_job_indicator.SetActive(is_avtive_job);
            goto Label_0043;
        Label_0026:
            if ((this.active_job_indicator != null) == null)
            {
                goto Label_0043;
            }
            this.active_job_indicator.SetActive(0);
        Label_0043:
            if (base_job_index > -1)
            {
                goto Label_0083;
            }
            if (cc_job_index < 0)
            {
                goto Label_0083;
            }
            if ((this.base_job_icon_button != null) == null)
            {
                goto Label_0082;
            }
            this.cc_job_icon_button.get_gameObject().SetActive(0);
            this.SetIcon(this.base_job_icon_button, unit, cc_job_index, 0);
        Label_0082:
            return;
        Label_0083:
            if ((this.base_job_icon_button != null) == null)
            {
                goto Label_00BB;
            }
            this.base_job_icon_button.get_gameObject().SetActive(0);
            if (base_job_index < 0)
            {
                goto Label_00BB;
            }
            this.SetIcon(this.base_job_icon_button, unit, base_job_index, 0);
        Label_00BB:
            if ((this.cc_job_icon_button != null) == null)
            {
                goto Label_00F3;
            }
            this.cc_job_icon_button.get_gameObject().SetActive(0);
            if (cc_job_index < 0)
            {
                goto Label_00F3;
            }
            this.SetIcon(this.cc_job_icon_button, unit, cc_job_index, 1);
        Label_00F3:
            return;
        }

        public void SetSelectIconAnim(bool is_on)
        {
            Animator animator;
            this.BaseJobIconButton.GetComponent<Animator>().SetBool(ANIM_PARAM_JOB_ICON_ON_BOOL, is_on);
            return;
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
                RectTransform transform;
                float num;
                Vector2 vector;
                Vector2 vector2;
                transform = null;
                num = 0f;
                if (this.base_job_icon_button.get_gameObject().get_activeSelf() == null)
                {
                    goto Label_003F;
                }
                transform = this.base_job_icon_button.get_transform() as RectTransform;
                num += &transform.get_sizeDelta().x;
            Label_003F:
                if (this.cc_job_icon_button.get_gameObject().get_activeSelf() == null)
                {
                    goto Label_0076;
                }
                transform = this.cc_job_icon_button.get_transform() as RectTransform;
                num += &transform.get_sizeDelta().x;
            Label_0076:
                return num;
            }
        }

        public float HalfWidth
        {
            get
            {
                if (this.cc_job_icon_button.get_gameObject().get_activeSelf() == null)
                {
                    goto Label_0022;
                }
                return (this.Width * 0.5f);
            Label_0022:
                return (this.Width * this.SINGLE_ICON_HALF_SIZE_COEF);
            }
        }

        public bool IsSingleIcon
        {
            get
            {
                return (this.cc_job_icon_button.get_gameObject().get_activeSelf() == 0);
            }
        }

        public enum eViewMode
        {
            NONE,
            UNIT_DETAIL,
            UNIT_VIEWER
        }
    }
}

