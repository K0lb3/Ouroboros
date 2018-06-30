namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitTobiraParamWindow : MonoBehaviour
    {
        [SerializeField]
        private Text TobiraTitleText;
        [SerializeField]
        private ImageArray TobiraIcons;
        [SerializeField]
        private GameObject TobiraParamIconEnable;
        [SerializeField]
        private UnitTobiraParamLevel[] TobiraParamIconLevels;
        [SerializeField]
        private GameObject TobiraParamIconDisable;
        [SerializeField]
        private StatusList Status;
        [SerializeField]
        private Text AdditionalLevelCap;
        [SerializeField, HeaderBar("▼扉LvをMaxにした時の恩恵")]
        private Text TobiraMasterEffectText;
        [SerializeField]
        private Text TobiraMasterEffectDescText;
        [HeaderBar("▼扉が「未開放」時に表示するもの"), SerializeField]
        private GameObject TobiraOpenButton;
        [SerializeField]
        private Text TobiraOpenButtonText;
        [SerializeField]
        private GameObject mLockedLayout;
        [SerializeField]
        private GameObject mTobiraConditionsLayout;
        [SerializeField]
        private RectTransform mTobiraConditionsContent;
        [SerializeField]
        private UnitTobiraConditionsItem mTobiraConditionsTemplate;
        [SerializeField, HeaderBar("▼扉が「開放」時に表示するもの")]
        private GameObject TobiraLevelUpButton;
        [SerializeField]
        private Text TobiraLevelUpButtonText;
        [SerializeField]
        private GameObject mUnlockedLayout;
        [SerializeField]
        private GameObject mTobiraMasterBonusLayout;
        [HeaderBar("▼扉Lvが「Max」時に表示するもの"), SerializeField]
        private GameObject mTobiraMasterEbaleLayout;
        private List<GameObject> mTobiraParameters;
        private List<UnitTobiraConditionsItem> mTobiraConditions;
        [CompilerGenerated]
        private static Action<UnitTobiraParamLevel> <>f__am$cache16;
        [CompilerGenerated]
        private static Action<GameObject> <>f__am$cache17;
        [CompilerGenerated]
        private static Action<GameObject> <>f__am$cache18;

        public UnitTobiraParamWindow()
        {
            this.mTobiraParameters = new List<GameObject>();
            this.mTobiraConditions = new List<UnitTobiraConditionsItem>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <Refresh>m__488(UnitTobiraParamLevel paramLevel)
        {
            paramLevel.Refresh(0);
            return;
        }

        [CompilerGenerated]
        private static void <Refresh>m__489(GameObject paramGO)
        {
            Object.Destroy(paramGO);
            return;
        }

        [CompilerGenerated]
        private static void <Refresh>m__48B(GameObject paramGO)
        {
            Object.Destroy(paramGO);
            return;
        }

        private void CreateConditionsItems(UnitData unitData, TobiraConditionParam[] condition_params)
        {
            List<ConditionsResult> list;
            int num;
            int num2;
            UnitTobiraConditionsItem item;
            list = TobiraUtility.TobiraConditionsCheck(unitData, condition_params);
            num = Mathf.Max(this.mTobiraConditions.Count, list.Count);
            num2 = 0;
            goto Label_00B9;
        Label_0026:
            item = null;
            if (this.mTobiraConditions.Count > num2)
            {
                goto Label_0072;
            }
            item = Object.Instantiate<GameObject>(this.mTobiraConditionsTemplate.get_gameObject()).GetComponent<UnitTobiraConditionsItem>();
            item.get_transform().SetParent(this.mTobiraConditionsContent.get_transform(), 0);
            this.mTobiraConditions.Add(item);
        Label_0072:
            item = this.mTobiraConditions[num2];
            if (list.Count > num2)
            {
                goto Label_009C;
            }
            item.get_gameObject().SetActive(0);
            goto Label_00B5;
        Label_009C:
            item.Setup(list[num2]);
            item.get_gameObject().SetActive(1);
        Label_00B5:
            num2 += 1;
        Label_00B9:
            if (num2 < num)
            {
                goto Label_0026;
            }
            return;
        }

        private void LockView(UnitData unitData, TobiraParam tobiraParam)
        {
            string str;
            string str2;
            bool flag;
            str = TobiraParam.GetCategoryName(tobiraParam.TobiraCategory);
            str2 = string.Format(LocalizedText.Get("sys.TOBIRA_INVENTORY_OPEN_TITLE"), str);
            this.TobiraOpenButtonText.set_text(str2);
            this.mLockedLayout.SetActive(1);
            this.mTobiraConditionsLayout.SetActive(1);
            this.mUnlockedLayout.SetActive(0);
            this.mTobiraMasterBonusLayout.SetActive(0);
            this.mTobiraMasterEbaleLayout.SetActive(0);
            flag = TobiraUtility.IsClearAllToboraConditions(unitData, tobiraParam.TobiraCategory);
            this.TobiraOpenButton.GetComponent<Button>().set_interactable(flag);
            SetHilightAnimationEnable(this.TobiraOpenButton, flag);
            return;
        }

        public unsafe void Refresh(UnitData unitData, TobiraData tobiraData, TobiraParam tobiraParam)
        {
            string str;
            BaseStatus status;
            BaseStatus status2;
            TobiraConditionParam[] paramArray;
            BaseStatus status3;
            BaseStatus status4;
            <Refresh>c__AnonStorey3E0 storeye;
            if ((this.TobiraTitleText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            str = TobiraParam.GetCategoryName(tobiraParam.TobiraCategory);
            this.TobiraTitleText.set_text(str);
            if ((this.AdditionalLevelCap != null) == null)
            {
                goto Label_005F;
            }
            this.AdditionalLevelCap.set_text(&MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraUnitLvCapBonus.ToString());
        Label_005F:
            if (tobiraData != null)
            {
                goto Label_0139;
            }
            this.TobiraParamIconEnable.SetActive(0);
            this.TobiraParamIconDisable.SetActive(1);
            if (<>f__am$cache16 != null)
            {
                goto Label_009B;
            }
            <>f__am$cache16 = new Action<UnitTobiraParamLevel>(UnitTobiraParamWindow.<Refresh>m__488);
        Label_009B:
            Array.ForEach<UnitTobiraParamLevel>(this.TobiraParamIconLevels, <>f__am$cache16);
            this.LockView(unitData, tobiraParam);
            if (<>f__am$cache17 != null)
            {
                goto Label_00CB;
            }
            <>f__am$cache17 = new Action<GameObject>(UnitTobiraParamWindow.<Refresh>m__489);
        Label_00CB:
            this.mTobiraParameters.ForEach(<>f__am$cache17);
            this.mTobiraParameters.Clear();
            status = new BaseStatus();
            status2 = new BaseStatus();
            TobiraUtility.CalcTobiraParameter(unitData.UnitID, tobiraParam.TobiraCategory, 1, &status, &status2);
            this.Status.SetValues(status, status2, 1);
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(unitData.UnitID, tobiraParam.TobiraCategory);
            this.CreateConditionsItems(unitData, paramArray);
            goto Label_0249;
        Label_0139:
            storeye = new <Refresh>c__AnonStorey3E0();
            if ((this.TobiraIcons != null) == null)
            {
                goto Label_0167;
            }
            this.TobiraIcons.ImageIndex = tobiraData.Param.TobiraCategory;
        Label_0167:
            this.TobiraParamIconEnable.SetActive(tobiraData.IsUnlocked);
            this.TobiraParamIconDisable.SetActive(tobiraData.IsUnlocked == 0);
            storeye.level = tobiraData.ViewLv;
            Array.ForEach<UnitTobiraParamLevel>(this.TobiraParamIconLevels, new Action<UnitTobiraParamLevel>(storeye.<>m__48A));
            if (tobiraData.IsUnlocked == null)
            {
                goto Label_01C9;
            }
            this.UnlockView(unitData, tobiraData);
            goto Label_01D1;
        Label_01C9:
            this.LockView(unitData, tobiraParam);
        Label_01D1:
            if (<>f__am$cache18 != null)
            {
                goto Label_01EF;
            }
            <>f__am$cache18 = new Action<GameObject>(UnitTobiraParamWindow.<Refresh>m__48B);
        Label_01EF:
            this.mTobiraParameters.ForEach(<>f__am$cache18);
            this.mTobiraParameters.Clear();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            TobiraUtility.CalcTobiraParameter(unitData.UnitID, tobiraData.Param.TobiraCategory, tobiraData.Lv, &status3, &status4);
            this.Status.SetValues(status3, status4, 0);
            this.SetActiveConditionsItems(0);
        Label_0249:
            this.SetMasterBonusText(tobiraParam);
            return;
        }

        private void SetActiveConditionsItems(bool active)
        {
            int num;
            num = 0;
            goto Label_0039;
        Label_0007:
            if ((this.mTobiraConditions[num] != null) == null)
            {
                goto Label_0035;
            }
            this.mTobiraConditions[num].get_gameObject().SetActive(active);
        Label_0035:
            num += 1;
        Label_0039:
            if (num < this.mTobiraConditions.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private static void SetHilightAnimationEnable(GameObject obj, bool isEnable)
        {
            Animator animator;
            if ((obj == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            animator = obj.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_002C;
            }
            animator.SetBool("hilit", isEnable);
        Label_002C:
            return;
        }

        private unsafe void SetMasterBonusText(TobiraParam tobiraParam)
        {
            int num;
            List<AbilityData> list;
            List<AbilityData> list2;
            AbilityParam param;
            SkillData data;
            if ((this.TobiraMasterEffectText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.TobiraMasterEffectDescText == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap;
            this.TobiraMasterEffectText.set_text(string.Empty);
            this.TobiraMasterEffectDescText.set_text(string.Empty);
            list = new List<AbilityData>();
            list2 = new List<AbilityData>();
            TobiraUtility.TryCraeteAbilityData(tobiraParam, num, list, list2, 1);
            if (list.Count <= 0)
            {
                goto Label_015C;
            }
            param = (list2[0] == null) ? null : list2[0].Param;
            if (param == null)
            {
                goto Label_0106;
            }
            this.TobiraMasterEffectText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_OVERRIDE_NEW_ABILITY_TEXT"), param.name, list[0].Param.name));
            this.TobiraMasterEffectDescText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_TEXT_DESC"), list[0].Param.expr));
            goto Label_015C;
        Label_0106:
            this.TobiraMasterEffectText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_NEW_ABILITY_TEXT"), list[0].Param.name));
            this.TobiraMasterEffectDescText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_TEXT_DESC"), list[0].Param.expr));
        Label_015C:
            data = null;
            TobiraUtility.TryCraeteLeaderSkill(tobiraParam, num, &data, 1);
            if (data == null)
            {
                goto Label_01BC;
            }
            this.TobiraMasterEffectText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_NEW_LEADER_SKILL_TEXT"), data.SkillParam.name));
            this.TobiraMasterEffectDescText.set_text(string.Format(LocalizedText.Get("sys.TOBIRA_MASTER_TEXT_DESC"), data.SkillParam.expr));
        Label_01BC:
            return;
        }

        private void UnlockView(UnitData unitData, TobiraData tobiraData)
        {
            string str;
            string str2;
            bool flag;
            str = TobiraParam.GetCategoryName(tobiraData.Param.TobiraCategory);
            str2 = string.Format(LocalizedText.Get("sys.TOBIRA_INVENTORY_BTN_LV_UP"), str);
            this.TobiraLevelUpButtonText.set_text(str2);
            this.TobiraLevelUpButton.SetActive(1);
            this.mLockedLayout.SetActive(tobiraData.IsUnlocked == 0);
            this.mTobiraConditionsLayout.SetActive(tobiraData.IsUnlocked == 0);
            this.mUnlockedLayout.SetActive(tobiraData.IsUnlocked);
            this.mTobiraMasterBonusLayout.SetActive(tobiraData.IsUnlocked);
            if (tobiraData.IsMaxLv == null)
            {
                goto Label_00AC;
            }
            this.TobiraLevelUpButton.SetActive(0);
            this.mTobiraMasterEbaleLayout.SetActive(1);
            goto Label_00F9;
        Label_00AC:
            this.TobiraLevelUpButton.SetActive(1);
            this.mTobiraMasterEbaleLayout.SetActive(0);
            flag = TobiraUtility.IsClearAllToboraRecipe(unitData, tobiraData.Param.TobiraCategory, tobiraData.Lv);
            this.TobiraOpenButton.GetComponent<Button>().set_interactable(flag);
            SetHilightAnimationEnable(this.TobiraLevelUpButton, flag);
        Label_00F9:
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3E0
        {
            internal int level;

            public <Refresh>c__AnonStorey3E0()
            {
                base..ctor();
                return;
            }

            internal void <>m__48A(UnitTobiraParamLevel paramLevel)
            {
                paramLevel.Refresh(this.level);
                return;
            }
        }
    }
}

