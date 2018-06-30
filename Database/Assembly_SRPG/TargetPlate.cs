namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class TargetPlate : MonoBehaviour
    {
        private const float COND_HIT_CHANGE_TIME = 1f;
        public GameObject NextTargetArrow;
        public GameObject PrevTargetArrow;
        public GameObject AttackInfoPlate;
        public GameObject HealValue;
        public GameObject HealOutPut;
        public GameObject DamageValue;
        public GameObject DamageOutPut;
        public GameObject CriticalRate;
        public GameObject CriticalRateOutPut;
        public GameObject HitRate;
        public GameObject HitRateOutPut;
        public GradientGauge HpGauge;
        public GradientGauge MpGauge;
        public GradientGauge HpMaxGauge;
        public StatusEffect StatusEffects;
        public GameObject GoElement;
        public GameObject GoLvIcon;
        public GameObject GoLvText;
        public GameObject GoHpGuage;
        public GameObject GoMpGuage;
        public GameObject GoCTGuage;
        public GameObject GoCondHit;
        public Text TextCondName;
        public ImageArray ImageCondIcon;
        public Text TextCondPer;
        public CanvasGroup[] CgCanTouchs;
        private List<LogSkill.Target.CondHit> mCondHitLists;
        private int mCondHitIndex;
        private float mCondHitPassedTime;
        private Unit mSelectedUnit;
        private SRPG.WindowController mWindowController;
        private CanvasGroup mTargetCg;
        private GaugeParam mHpGaugeParam;
        public GameObject GimmickDescription;
        public GameObject FlipButton;

        public TargetPlate()
        {
            this.mCondHitLists = new List<LogSkill.Target.CondHit>();
            this.mHpGaugeParam = new GaugeParam();
            base..ctor();
            return;
        }

        public void ActivateNextTargetArrow(ButtonExt.ButtonClickEvent listener)
        {
            this.toggleTargetArrow(listener, 1, 1);
            return;
        }

        public void ActivatePrevTargetArrow(ButtonExt.ButtonClickEvent listener)
        {
            this.toggleTargetArrow(listener, 1, 0);
            return;
        }

        public void Close()
        {
            this.CloseGimmickDescription();
            if (this.WindowController.IsClosing() == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (this.WindowController.IsOpening() == null)
            {
                goto Label_0033;
            }
            this.WindowController.ForceClose();
            return;
        Label_0033:
            this.WindowController.Close();
            return;
        }

        public void CloseGimmickDescription()
        {
        }

        public void DeactivateNextTargetArrow(ButtonExt.ButtonClickEvent listener)
        {
            this.toggleTargetArrow(listener, 0, 1);
            return;
        }

        public void DeactivatePrevTargetArrow(ButtonExt.ButtonClickEvent listener)
        {
            this.toggleTargetArrow(listener, 0, 0);
            return;
        }

        private unsafe void DispCondHit(int ch_idx)
        {
            LogSkill.Target.CondHit hit;
            List<EUnitCondition> list;
            int num;
            if (this.mCondHitLists.Count != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            hit = this.mCondHitLists[ch_idx];
            list = new List<EUnitCondition>((EUnitCondition[]) Enum.GetValues(typeof(EUnitCondition)));
            num = list.IndexOf(hit.Cond);
            if (this.TextCondName == null)
            {
                goto Label_007B;
            }
            if (num < 0)
            {
                goto Label_007B;
            }
            if (num >= ((int) Unit.StrNameUnitConds.Length))
            {
                goto Label_007B;
            }
            this.TextCondName.set_text(Unit.StrNameUnitConds[num]);
        Label_007B:
            if (this.ImageCondIcon == null)
            {
                goto Label_00B1;
            }
            if (num < 0)
            {
                goto Label_00B1;
            }
            if (num >= ((int) this.ImageCondIcon.Images.Length))
            {
                goto Label_00B1;
            }
            this.ImageCondIcon.ImageIndex = num;
        Label_00B1:
            if (this.TextCondPer == null)
            {
                goto Label_00D7;
            }
            this.TextCondPer.set_text(&hit.Per.ToString());
        Label_00D7:
            return;
        }

        public void ForceClose(bool isHide)
        {
            if (isHide == null)
            {
                goto Label_0011;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "TARGET_STATUS_BUTTON_HIDE");
        Label_0011:
            this.CloseGimmickDescription();
            this.WindowController.ForceClose();
            return;
        }

        public void HideButton()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "TARGET_STATUS_BUTTON_HIDE");
            return;
        }

        public void Open()
        {
            if ((this.StatusEffects != null) == null)
            {
                goto Label_002D;
            }
            if (this.mSelectedUnit == null)
            {
                goto Label_002D;
            }
            this.StatusEffects.SetStatus(this.mSelectedUnit);
        Label_002D:
            this.OpenGimmickDescription();
            if (this.WindowController.IsOpening() == null)
            {
                goto Label_0044;
            }
            return;
        Label_0044:
            if (this.WindowController.IsClosing() == null)
            {
                goto Label_0060;
            }
            this.WindowController.ForceOpen();
            return;
        Label_0060:
            this.WindowController.Open();
            return;
        }

        public void OpenGimmickDescription()
        {
            this.ToggleGimmickDescription(1);
            return;
        }

        private void reflectBreakObj(Unit unit)
        {
            bool flag;
            bool flag2;
            flag = (unit == null) ? 1 : (unit.IsBreakObj == 0);
            flag2 = ((unit == null) || (unit.IsBreakObj == null)) ? 1 : ((unit.Element == 0) == 0);
            this.SetActive(this.GoElement, flag2);
            this.SetActive(this.GoLvIcon, flag);
            this.SetActive(this.GoLvText, flag);
            this.SetActive(this.GoMpGuage, flag);
            this.SetActive(this.GoCTGuage, flag);
            return;
        }

        public void ResetHpGauge(EUnitSide Side, int CurrentHp, int MaxHp)
        {
            this.SetHpGaugeParam(Side, CurrentHp, MaxHp, 0, 0, 0);
            this.UpdateGauge(this.mHpGaugeParam, this.HpGauge, this.HpMaxGauge);
            return;
        }

        private void SetActive(GameObject targetObject, bool isActive)
        {
            if ((targetObject != null) == null)
            {
                goto Label_0013;
            }
            targetObject.SetActive(isActive);
        Label_0013:
            return;
        }

        public unsafe void SetAttackAction(Unit unit, int damageValue, int criticalRate, int hitRate, List<LogSkill.Target.CondHit> cond_hit_lists)
        {
            this.SetActive(this.HealValue, 0);
            this.SetActive(this.DamageValue, 1);
            this.SetActive(this.CriticalRate, 1);
            this.SetActive(this.HitRate, 1);
            this.SetActive(this.AttackInfoPlate, 1);
            this.reflectBreakObj(unit);
            this.SetCondHit(cond_hit_lists);
            this.SetTextValue(this.DamageOutPut, &damageValue.ToString());
            this.SetTextValue(this.CriticalRateOutPut, &criticalRate.ToString());
            this.SetTextValue(this.HitRateOutPut, &hitRate.ToString());
            this.mSelectedUnit = unit;
            DataSource.Bind<Unit>(base.get_gameObject(), unit);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void SetCondHit(List<LogSkill.Target.CondHit> cond_hit_lists)
        {
            if (cond_hit_lists != null)
            {
                goto Label_000D;
            }
            cond_hit_lists = new List<LogSkill.Target.CondHit>();
        Label_000D:
            this.mCondHitLists = cond_hit_lists;
            if (this.GoCondHit == null)
            {
                goto Label_0040;
            }
            this.GoCondHit.SetActive((this.mCondHitLists.Count == 0) == 0);
        Label_0040:
            if (this.mCondHitLists.Count != null)
            {
                goto Label_0051;
            }
            return;
        Label_0051:
            this.mCondHitIndex = 0;
            this.mCondHitPassedTime = 0f;
            this.DispCondHit(this.mCondHitIndex);
            return;
        }

        public void SetEnableFlipButton(bool is_enable)
        {
            if ((this.FlipButton == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.FlipButton.SetActive(is_enable);
            return;
        }

        private unsafe void SetGaugeParamInternal(ref GaugeParam Gauge, EGaugeType Type, int CurrentValue, int MaxValue, int PlusValue)
        {
            GameSettings settings;
            Color32 color;
            Color32 color2;
            Color32 color3;
            Color32[] colorArray;
            float num;
            EGaugeType type;
            settings = GameSettings.Instance;
            color = new Color32();
            color2 = new Color32();
            color3 = new Color32();
            type = Type;
            if (type == 1)
            {
                goto Label_005A;
            }
            if (type == 2)
            {
                goto Label_00A2;
            }
            if (type == 10)
            {
                goto Label_0079;
            }
            if (type == 11)
            {
                goto Label_00A2;
            }
            if (type == 20)
            {
                goto Label_00A2;
            }
            if (type == 0x15)
            {
                goto Label_00A2;
            }
            goto Label_00A2;
        Label_005A:
            color = settings.Gauge_PlayerHP_Base;
            color2 = settings.Gauge_PlayerHP_Damage;
            color3 = settings.Gauge_PlayerHP_Heal;
            goto Label_00A2;
            goto Label_00A2;
        Label_0079:
            color = settings.Gauge_EnemyHP_Base;
            color2 = settings.Gauge_EnemyHP_Damage;
            color3 = settings.Gauge_EnemyHP_Heal;
            goto Label_00A2;
            goto Label_00A2;
            goto Label_00A2;
        Label_00A2:
            colorArray = null;
            if (0 <= PlusValue)
            {
                goto Label_0132;
            }
            colorArray = new Color32[2];
            *(&(colorArray[0])) = color;
            &(colorArray[0]).a = (byte) (Mathf.Clamp01(((float) (CurrentValue + PlusValue)) / ((float) CurrentValue)) * 255f);
            *(&(colorArray[1])) = color2;
            &(colorArray[1]).a = (byte) (0xff - &(colorArray[0]).a);
            *(Gauge).Colors = colorArray;
            *(Gauge).Current = CurrentValue;
            *(Gauge).Max = MaxValue;
            goto Label_01FE;
        Label_0132:
            if (0 >= PlusValue)
            {
                goto Label_01CE;
            }
            colorArray = new Color32[2];
            *(&(colorArray[0])) = color;
            num = Mathf.Min(((float) CurrentValue) + ((float) PlusValue), (float) MaxValue);
            &(colorArray[0]).a = (byte) (Mathf.Clamp01(((float) CurrentValue) / num) * 255f);
            *(&(colorArray[1])) = color3;
            &(colorArray[1]).a = (byte) (0xff - &(colorArray[0]).a);
            *(Gauge).Colors = colorArray;
            *(Gauge).Current = (int) num;
            *(Gauge).Max = MaxValue;
            goto Label_01FE;
        Label_01CE:
            colorArray = new Color32[1];
            *(&(colorArray[0])) = color;
            *(Gauge).Colors = colorArray;
            *(Gauge).Current = CurrentValue;
            *(Gauge).Max = MaxValue;
        Label_01FE:
            return;
        }

        public unsafe void SetHealAction(Unit unit, int healValue, int criticalRate, int hitRate)
        {
            this.SetActive(this.HealValue, 1);
            this.SetActive(this.DamageValue, 0);
            this.SetActive(this.CriticalRate, 0);
            this.SetActive(this.HitRate, 0);
            this.SetActive(this.AttackInfoPlate, 1);
            this.reflectBreakObj(unit);
            this.SetCondHit(null);
            this.SetTextValue(this.HealOutPut, &healValue.ToString());
            this.mSelectedUnit = unit;
            DataSource.Bind<Unit>(base.get_gameObject(), unit);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public unsafe void SetHpGaugeParam(EUnitSide Side, int CurrentHp, int MaxHp, int YosokuDamageHp, int YosokuHealHp, bool is_max_damage)
        {
            int num;
            int num2;
            if ((this.HpGauge == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = YosokuHealHp - YosokuDamageHp;
            if ((is_max_damage == null) || (num == null))
            {
                goto Label_0068;
            }
            num2 = MaxHp;
            MaxHp = Math.Max(num2 + num, 0);
            this.mHpGaugeParam.MaxValue = (num2 == null) ? 1f : Mathf.Clamp01(((float) MaxHp) / ((float) num2));
            CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
            num = 0;
            goto Label_0078;
        Label_0068:
            this.mHpGaugeParam.MaxValue = 1f;
        Label_0078:
            if (Side != null)
            {
                goto Label_0093;
            }
            this.SetGaugeParamInternal(&this.mHpGaugeParam, 1, CurrentHp, MaxHp, num);
            goto Label_00A4;
        Label_0093:
            this.SetGaugeParamInternal(&this.mHpGaugeParam, 10, CurrentHp, MaxHp, num);
        Label_00A4:
            return;
        }

        public void SetNextTargetArrowActive(bool active)
        {
            this.NextTargetArrow.SetActive(active);
            return;
        }

        public void SetNoAction(Unit unit, List<LogSkill.Target.CondHit> cond_hit_lists)
        {
            this.SetActive(this.HealValue, 0);
            this.SetActive(this.DamageValue, 0);
            this.SetActive(this.CriticalRate, 0);
            this.SetActive(this.HitRate, 0);
            this.SetActive(this.AttackInfoPlate, 0);
            this.reflectBreakObj(unit);
            this.SetCondHit(cond_hit_lists);
            if (cond_hit_lists == null)
            {
                goto Label_006D;
            }
            if (cond_hit_lists.Count == null)
            {
                goto Label_006D;
            }
            this.SetActive(this.AttackInfoPlate, 1);
        Label_006D:
            this.mSelectedUnit = unit;
            DataSource.Bind<Unit>(base.get_gameObject(), unit);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void SetPrevTargetArrowActive(bool active)
        {
            this.PrevTargetArrow.SetActive(active);
            return;
        }

        public unsafe void SetSkillAction(Unit unit, int damageValue, int criticalRate, int hitRate, List<LogSkill.Target.CondHit> cond_hit_lists)
        {
            this.SetActive(this.HealValue, 0);
            this.SetActive(this.DamageValue, 1);
            this.SetActive(this.CriticalRate, 0);
            this.SetActive(this.HitRate, 1);
            this.SetActive(this.AttackInfoPlate, 1);
            this.reflectBreakObj(unit);
            this.SetCondHit(cond_hit_lists);
            this.SetTextValue(this.DamageOutPut, &damageValue.ToString());
            this.SetTextValue(this.HitRateOutPut, &hitRate.ToString());
            this.mSelectedUnit = unit;
            DataSource.Bind<Unit>(base.get_gameObject(), unit);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void SetTextValue(GameObject targetObject, string value)
        {
            Text text;
            if ((targetObject == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            text = targetObject.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_0027;
            }
            text.set_text(value);
        Label_0027:
            return;
        }

        public void SetTrick(TrickParam trick_param)
        {
            this.SetActive(this.HealValue, 0);
            this.SetActive(this.DamageValue, 0);
            this.SetActive(this.CriticalRate, 0);
            this.SetActive(this.HitRate, 0);
            this.SetActive(this.AttackInfoPlate, 0);
            this.mSelectedUnit = null;
            DataSource.Bind<TrickParam>(base.get_gameObject(), trick_param);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void ToggleGimmickDescription(bool Active)
        {
            if (this.mSelectedUnit == null)
            {
                goto Label_004A;
            }
            if ((this.GimmickDescription != null) == null)
            {
                goto Label_004A;
            }
            if (this.mSelectedUnit.UnitType != 2)
            {
                goto Label_003E;
            }
            this.GimmickDescription.SetActive(Active);
            goto Label_004A;
        Label_003E:
            this.GimmickDescription.SetActive(0);
        Label_004A:
            return;
        }

        private void toggleTargetArrow(ButtonExt.ButtonClickEvent listener, bool active, bool isNextArrow)
        {
            GameObject obj2;
            Button button;
            obj2 = (isNextArrow == null) ? this.PrevTargetArrow : this.NextTargetArrow;
            if (listener == null)
            {
                goto Label_002A;
            }
            if ((obj2 == null) == null)
            {
                goto Label_002B;
            }
        Label_002A:
            return;
        Label_002B:
            button = obj2.GetComponent<Button>();
            if ((button == null) == null)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            obj2.SetActive(active);
            if (active == null)
            {
                goto Label_0058;
            }
            SRPG_Extensions.AddClickListener(button, listener);
            goto Label_005F;
        Label_0058:
            SRPG_Extensions.RemoveClickListener(button, listener);
        Label_005F:
            return;
        }

        private void Update()
        {
            CanvasGroup group;
            CanvasGroup group2;
            CanvasGroup[] groupArray;
            int num;
            if (this.CgCanTouchs == null)
            {
                goto Label_0052;
            }
            group = this.TargetCg;
            if (group == null)
            {
                goto Label_0052;
            }
            groupArray = this.CgCanTouchs;
            num = 0;
            goto Label_0049;
        Label_002B:
            group2 = groupArray[num];
            group2.set_ignoreParentGroups((group.get_alpha() < 1f) == 0);
            num += 1;
        Label_0049:
            if (num < ((int) groupArray.Length))
            {
                goto Label_002B;
            }
        Label_0052:
            if (this.mCondHitLists.Count <= 1)
            {
                goto Label_00C7;
            }
            this.mCondHitPassedTime += Time.get_deltaTime();
            if (this.mCondHitPassedTime < 1f)
            {
                goto Label_00C7;
            }
            this.mCondHitPassedTime = 0f;
            this.mCondHitIndex += 1;
            if (this.mCondHitIndex < this.mCondHitLists.Count)
            {
                goto Label_00BB;
            }
            this.mCondHitIndex = 0;
        Label_00BB:
            this.DispCondHit(this.mCondHitIndex);
        Label_00C7:
            return;
        }

        private void UpdateGauge(GaugeParam param, GradientGauge gauge, GradientGauge max_gauge)
        {
            if (param == null)
            {
                goto Label_001D;
            }
            if (param.Colors == null)
            {
                goto Label_001D;
            }
            if ((gauge == null) == null)
            {
                goto Label_001E;
            }
        Label_001D:
            return;
        Label_001E:
            gauge.UpdateColors(param.Colors);
            gauge.AnimateRangedValue(param.Current, param.Max, 0f);
            if (max_gauge == null)
            {
                goto Label_0070;
            }
            max_gauge.UpdateValue(param.MaxValue);
            gauge.UpdateValue(Mathf.Clamp01(gauge.Value * max_gauge.Value));
        Label_0070:
            return;
        }

        public void UpdateHpGauge()
        {
            this.UpdateGauge(this.mHpGaugeParam, this.HpGauge, this.HpMaxGauge);
            return;
        }

        public Unit SelectedUnit
        {
            get
            {
                return this.mSelectedUnit;
            }
        }

        public SRPG.WindowController WindowController
        {
            get
            {
                if ((this.mWindowController == null) == null)
                {
                    goto Label_0022;
                }
                this.mWindowController = base.get_gameObject().GetComponent<SRPG.WindowController>();
            Label_0022:
                return this.mWindowController;
            }
        }

        private CanvasGroup TargetCg
        {
            get
            {
                if ((this.mTargetCg == null) == null)
                {
                    goto Label_001D;
                }
                this.mTargetCg = base.GetComponent<CanvasGroup>();
            Label_001D:
                return this.mTargetCg;
            }
        }

        private enum EGaugeType
        {
            PLAYER_HP = 1,
            PLAYER_MP = 2,
            ENEMY_HP = 10,
            ENEMY_MP = 11,
            NEUTRAL_HP = 20,
            NEUTRAL_MP = 0x15
        }

        private class GaugeParam
        {
            public Color32[] Colors;
            public int Current;
            public int Max;
            public float MaxValue;

            public GaugeParam()
            {
                this.MaxValue = 1f;
                base..ctor();
                return;
            }
        }
    }
}

