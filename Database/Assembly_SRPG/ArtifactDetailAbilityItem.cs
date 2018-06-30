namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

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
            base..ctor();
            return;
        }

        public void DestoryLastLine()
        {
            if ((this.mLineVertical == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            Object.Destroy(this.mLineVertical);
            return;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            HoldGestureObserver.StartHoldGesture(this);
            return;
        }

        public void OnPointerHoldEnd()
        {
        }

        public void OnPointerHoldStart()
        {
            ArtifactDetailWindow window;
            window = base.GetComponentInParent<ArtifactDetailWindow>();
            if ((window != null) == null)
            {
                goto Label_0024;
            }
            AbilityDetailWindow.SetBindAbility(this.mAbilityParam);
            window.OpenAbilityDetail();
        Label_0024:
            return;
        }

        public void SetActive(bool is_active)
        {
            if (this.mAbilityParam != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            base.get_gameObject().SetActive(is_active);
            return;
        }

        public void SetActiveLine(bool is_active)
        {
            if ((this.mLineVertical != null) == null)
            {
                goto Label_001D;
            }
            this.mLineVertical.SetActive(is_active);
        Label_001D:
            if ((this.mLineHorizontal != null) == null)
            {
                goto Label_003A;
            }
            this.mLineHorizontal.SetActive(is_active);
        Label_003A:
            return;
        }

        public void Setup(AbilityParam ability_param, bool is_derive_ability, bool is_enable, bool is_locked, bool has_derive_ability)
        {
            UnitParam param;
            JobParam param2;
            AbilityConditions conditions;
            this.mAbilityParam = ability_param;
            this.mIsEnable = is_enable;
            this.mHasDeriveAbility = has_derive_ability;
            if (is_locked == null)
            {
                goto Label_002F;
            }
            this.mLockImage.SetActive(is_locked);
            goto Label_0043;
        Label_002F:
            this.mDisableMask.SetActive(this.mIsEnable == 0);
        Label_0043:
            if ((this.mUnitIcon != null) == null)
            {
                goto Label_0073;
            }
            param = DataSource.FindDataOfClass<UnitParam>(base.get_gameObject(), null);
            this.mUnitIcon.SetActive((param == null) == 0);
        Label_0073:
            if ((this.mJobIcon != null) == null)
            {
                goto Label_00A3;
            }
            param2 = DataSource.FindDataOfClass<JobParam>(base.get_gameObject(), null);
            this.mJobIcon.SetActive((param2 == null) == 0);
        Label_00A3:
            if ((this.mConditionsText != null) == null)
            {
                goto Label_00DC;
            }
            conditions = new AbilityConditions();
            conditions.Setup(ability_param, MonoSingleton<GameManager>.Instance.MasterParam);
            this.mConditionsText.set_text(conditions.MakeConditionsText());
        Label_00DC:
            return;
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
    }
}

