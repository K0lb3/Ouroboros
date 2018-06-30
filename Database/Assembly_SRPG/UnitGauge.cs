namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitGauge : MonoBehaviour
    {
        public GradientGauge MainGauge;
        public UnitBuffDisplay BuffDisplay;
        public string ModeInt;
        public GradientGauge MaxGauge;
        private int mMode;
        public GameObject WeakTemplate;
        public GameObject ResistTemplate;
        public GameObject ElementIcon;
        public Image ElementIconImage;
        public GameObject ElementIconWeakOverlay;
        public GameObject ElementIconResistOverlay;
        private Unit mCurrentUnit;
        private GameObject ResistWeakPopup;

        public UnitGauge()
        {
            this.ModeInt = "mode";
            base..ctor();
            return;
        }

        public void ActivateElementIcon(bool resetOverlay)
        {
            this.ActivateElementIconInternal(resetOverlay);
            return;
        }

        private void ActivateElementIconInternal(bool resetOverlay)
        {
            bool flag;
            flag = 1;
            if (this.mCurrentUnit == null)
            {
                goto Label_002F;
            }
            if (this.mCurrentUnit.IsBreakObj == null)
            {
                goto Label_002F;
            }
            if (this.mCurrentUnit.Element != null)
            {
                goto Label_002F;
            }
            flag = 0;
        Label_002F:
            GameUtility.SetGameObjectActive(this.ElementIcon.get_gameObject(), flag);
            if (resetOverlay == null)
            {
                goto Label_004C;
            }
            this.ResetElementIconOverlay();
        Label_004C:
            return;
        }

        private void activateHpGauge(bool is_active)
        {
            Image image;
            Image[] imageArray;
            Image image2;
            Image[] imageArray2;
            int num;
            if (this.mCurrentUnit == null)
            {
                goto Label_00CE;
            }
            if (this.mCurrentUnit.IsBreakObj == null)
            {
                goto Label_00CE;
            }
            if (this.MainGauge == null)
            {
                goto Label_003C;
            }
            this.MainGauge.get_gameObject().SetActive(is_active);
        Label_003C:
            if (this.MaxGauge == null)
            {
                goto Label_005D;
            }
            this.MaxGauge.get_gameObject().SetActive(is_active);
        Label_005D:
            image = base.GetComponent<Image>();
            if (image == null)
            {
                goto Label_0076;
            }
            image.set_enabled(is_active);
        Label_0076:
            if (this.ElementIcon == null)
            {
                goto Label_00CE;
            }
            if (this.mCurrentUnit.Element == null)
            {
                goto Label_00CE;
            }
            imageArray = this.ElementIcon.GetComponentsInChildren<Image>();
            if (imageArray == null)
            {
                goto Label_00CE;
            }
            imageArray2 = imageArray;
            num = 0;
            goto Label_00C4;
        Label_00B2:
            image2 = imageArray2[num];
            image2.set_enabled(is_active);
            num += 1;
        Label_00C4:
            if (num < ((int) imageArray2.Length))
            {
                goto Label_00B2;
            }
        Label_00CE:
            return;
        }

        private int CalcElementRate(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
        {
            EElement element;
            EElement element2;
            int num;
            if ((skill == null) || (skill.IsIgnoreElement() == null))
            {
                goto Label_0017;
            }
            skillElement = 0;
            attackerElement = 0;
        Label_0017:
            element = UnitParam.GetWeakElement(this.mCurrentUnit.Element);
            element2 = UnitParam.GetResistElement(this.mCurrentUnit.Element);
            num = 0;
            if (attackerElement == null)
            {
                goto Label_0062;
            }
            num += (element != attackerElement) ? ((element2 != attackerElement) ? 0 : -1) : 1;
        Label_0062:
            if (skillElement == null)
            {
                goto Label_0086;
            }
            num += (element != skillElement) ? ((element2 != skillElement) ? 0 : -1) : 1;
        Label_0086:
            return num;
        }

        public void DeactivateElementIcon()
        {
            this.DeactivateElementIconInternal();
            return;
        }

        private void DeactivateElementIconInternal()
        {
            GameUtility.SetGameObjectActive(this.ElementIcon.get_gameObject(), 0);
            this.ResetElementIconOverlay();
            return;
        }

        public void Focus(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
        {
            int num;
            if (this.mCurrentUnit == null)
            {
                goto Label_001B;
            }
            if (this.mCurrentUnit.IsBreakObj != null)
            {
                goto Label_0047;
            }
        Label_001B:
            num = this.CalcElementRate(skill, skillElement, skillElemValue, attackerElement, attackerElemValue);
            if (num <= 0)
            {
                goto Label_0038;
            }
            this.ToggleElementIconOverlay(1);
            return;
        Label_0038:
            if (num >= 0)
            {
                goto Label_0047;
            }
            this.ToggleElementIconOverlay(0);
            return;
        Label_0047:
            this.ResetElementIconOverlay();
            return;
        }

        public Unit GetOwner()
        {
            return this.mCurrentUnit;
        }

        public void OnAttack(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
        {
            int num;
            if ((this.ResistWeakPopup != null) == null)
            {
                goto Label_0023;
            }
            Object.Destroy(this.ResistWeakPopup);
            this.ResistWeakPopup = null;
        Label_0023:
            if (this.mCurrentUnit == null)
            {
                goto Label_003F;
            }
            if (this.mCurrentUnit.IsBreakObj == null)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            num = this.CalcElementRate(skill, skillElement, skillElemValue, attackerElement, attackerElemValue);
            if (num <= 0)
            {
                goto Label_009F;
            }
            if ((this.WeakTemplate != null) == null)
            {
                goto Label_00EC;
            }
            this.ResistWeakPopup = Object.Instantiate<GameObject>(this.WeakTemplate);
            this.ResistWeakPopup.get_transform().SetParent(base.get_transform(), 0);
            this.ResistWeakPopup.SetActive(1);
            return;
            goto Label_00EC;
        Label_009F:
            if (num >= 0)
            {
                goto Label_00EC;
            }
            if ((this.ResistTemplate != null) == null)
            {
                goto Label_00EC;
            }
            this.ResistWeakPopup = Object.Instantiate<GameObject>(this.ResistTemplate);
            this.ResistWeakPopup.get_transform().SetParent(base.get_transform(), 0);
            this.ResistWeakPopup.SetActive(1);
            return;
        Label_00EC:
            return;
        }

        private void ResetElementIconOverlay()
        {
            GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, 0);
            GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, 0);
            return;
        }

        private void SetElementIconImage(EElement element)
        {
            if ((this.ElementIcon != null) == null)
            {
                goto Label_003F;
            }
            if ((this.ElementIconImage != null) == null)
            {
                goto Label_003F;
            }
            this.ElementIconImage.set_sprite(GameSettings.Instance.Elements_IconSmall[element]);
            this.ResetElementIconOverlay();
        Label_003F:
            return;
        }

        public void SetOwner(Unit owner)
        {
            this.mCurrentUnit = owner;
            this.SetElementIconImage(this.mCurrentUnit.Element);
            this.activateHpGauge(0);
            return;
        }

        protected void Start()
        {
            GameUtility.SetGameObjectActive(this.WeakTemplate, 0);
            GameUtility.SetGameObjectActive(this.ResistTemplate, 0);
            GameUtility.SetGameObjectActive(this.ElementIcon, 0);
            GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, 0);
            GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, 0);
            return;
        }

        private void ToggleElementIconOverlay(bool weakActive)
        {
            GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, weakActive);
            GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, weakActive == 0);
            return;
        }

        private void Update()
        {
            Animator animator;
            animator = base.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0035;
            }
            if (string.IsNullOrEmpty(this.ModeInt) != null)
            {
                goto Label_0035;
            }
            animator.SetInteger(this.ModeInt, this.mMode);
        Label_0035:
            this.ActivateElementIcon(0);
            if (this.MainGauge.IsAnimating != null)
            {
                goto Label_006F;
            }
            if ((this.ResistWeakPopup != null) == null)
            {
                goto Label_006F;
            }
            Object.Destroy(this.ResistWeakPopup);
            this.ResistWeakPopup = null;
        Label_006F:
            return;
        }

        public int Mode
        {
            get
            {
                return this.mMode;
            }
            set
            {
                GaugeMode mode;
                this.mMode = value;
                if (this.mMode == null)
                {
                    goto Label_0019;
                }
                goto Label_0025;
            Label_0019:
                this.activateHpGauge(0);
                goto Label_0031;
            Label_0025:
                this.activateHpGauge(1);
            Label_0031:
                return;
            }
        }

        public enum GaugeMode
        {
            Normal,
            Attack,
            Target,
            Change
        }
    }
}

