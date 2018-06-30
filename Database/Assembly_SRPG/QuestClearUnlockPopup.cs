namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuestClearUnlockPopup : MonoBehaviour
    {
        public Button CloseButton;
        public GameObject ItemWindow;
        public string ItemCloseFlag;
        public string ItemEndAnimation;
        public string ItemOpendAnimation;
        public GameObject SkillNewItem;
        public GameObject SkillUpdateItem;
        public GameObject AbilityNewItem;
        public GameObject AbilityUpdateItem;
        public GameObject LeaderSkillNewItem;
        public GameObject LeaderSkillUpdateItem;
        public GameObject MasterAbilityNewItem;
        public GameObject MasterAbilityUpdateItem;
        private QuestClearUnlockUnitDataParam[] mUnlocks;
        private List<GameObject> mListItems;
        private UnitData mUnit;
        private Animator mWindowAnimator;

        public QuestClearUnlockPopup()
        {
            this.mListItems = new List<GameObject>();
            base..ctor();
            return;
        }

        private GameObject CreateInstance(GameObject template)
        {
            return (((template != null) == null) ? null : Object.Instantiate<GameObject>(template));
        }

        public void OnClick()
        {
            if (this.mListItems.Count <= 1)
            {
                goto Label_0027;
            }
            if (this.mWindowAnimator.GetBool(this.ItemCloseFlag) == null)
            {
                goto Label_0028;
            }
        Label_0027:
            return;
        Label_0028:
            GameUtility.SetAnimatorBool(this.mWindowAnimator, this.ItemCloseFlag, 1);
            return;
        }

        private void Start()
        {
            MasterParam param;
            int num;
            GameObject obj2;
            AbilityParam param2;
            SkillParam param3;
            QuestClearUnlockUnitDataParam.EUnlockTypes types;
            this.mUnlocks = DataSource.FindDataOfClass<QuestClearUnlockUnitDataParam[]>(base.get_gameObject(), null);
            if (this.mUnlocks != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            this.mUnit = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (this.mUnit != null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            if ((this.SkillNewItem != null) == null)
            {
                goto Label_0059;
            }
            this.SkillNewItem.SetActive(0);
        Label_0059:
            if ((this.SkillUpdateItem != null) == null)
            {
                goto Label_0076;
            }
            this.SkillUpdateItem.SetActive(0);
        Label_0076:
            if ((this.AbilityNewItem != null) == null)
            {
                goto Label_0093;
            }
            this.AbilityNewItem.SetActive(0);
        Label_0093:
            if ((this.AbilityUpdateItem != null) == null)
            {
                goto Label_00B0;
            }
            this.AbilityUpdateItem.SetActive(0);
        Label_00B0:
            if ((this.LeaderSkillNewItem != null) == null)
            {
                goto Label_00CD;
            }
            this.LeaderSkillNewItem.SetActive(0);
        Label_00CD:
            if ((this.LeaderSkillUpdateItem != null) == null)
            {
                goto Label_00EA;
            }
            this.LeaderSkillUpdateItem.SetActive(0);
        Label_00EA:
            if ((this.MasterAbilityNewItem != null) == null)
            {
                goto Label_0107;
            }
            this.MasterAbilityNewItem.SetActive(0);
        Label_0107:
            this.CloseButton.get_gameObject().SetActive(0);
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num = 0;
            goto Label_036E;
        Label_012A:
            obj2 = null;
            param2 = null;
            param3 = null;
            switch ((this.mUnlocks[num].type + 1))
            {
                case 0:
                    goto Label_0302;

                case 1:
                    goto Label_016A;

                case 2:
                    goto Label_016F;

                case 3:
                    goto Label_01CE;

                case 4:
                    goto Label_027A;

                case 5:
                    goto Label_0211;

                case 6:
                    goto Label_02BE;
            }
            goto Label_0302;
        Label_016A:
            goto Label_0307;
        Label_016F:
            if (this.mUnlocks[num].add == null)
            {
                goto Label_0193;
            }
            obj2 = this.CreateInstance(this.SkillNewItem);
            goto Label_01A0;
        Label_0193:
            obj2 = this.CreateInstance(this.SkillUpdateItem);
        Label_01A0:
            param2 = param.GetAbilityParam(this.mUnlocks[num].parent_id);
            param3 = param.GetSkillParam(this.mUnlocks[num].new_id);
            goto Label_0307;
        Label_01CE:
            obj2 = this.CreateInstance((this.mUnlocks[num].add == null) ? this.AbilityUpdateItem : this.AbilityNewItem);
            param2 = param.GetAbilityParam(this.mUnlocks[num].new_id);
            goto Label_0307;
        Label_0211:
            obj2 = this.CreateInstance((this.mUnlocks[num].add == null) ? this.MasterAbilityUpdateItem : this.MasterAbilityNewItem);
            param2 = param.GetAbilityParam(this.mUnlocks[num].new_id);
            if ((param2 == null) || (param2.skills == null))
            {
                goto Label_0307;
            }
            param3 = param.GetSkillParam(param2.skills[0].iname);
            goto Label_0307;
        Label_027A:
            obj2 = this.CreateInstance((this.mUnlocks[num].add == null) ? this.LeaderSkillUpdateItem : this.LeaderSkillNewItem);
            param3 = param.GetSkillParam(this.mUnlocks[num].new_id);
            goto Label_0307;
        Label_02BE:
            obj2 = this.CreateInstance((this.mUnlocks[num].add == null) ? this.MasterAbilityUpdateItem : this.MasterAbilityNewItem);
            param3 = param.GetSkillParam(this.mUnlocks[num].new_id);
            goto Label_0307;
        Label_0302:;
        Label_0307:
            if ((this.ItemWindow != null) == null)
            {
                goto Label_036A;
            }
            if ((obj2 != null) == null)
            {
                goto Label_036A;
            }
            if (param2 == null)
            {
                goto Label_0331;
            }
            DataSource.Bind<AbilityParam>(obj2, param2);
        Label_0331:
            if (param3 == null)
            {
                goto Label_0340;
            }
            DataSource.Bind<SkillParam>(obj2, param3);
        Label_0340:
            obj2.get_transform().SetParent(this.ItemWindow.get_transform(), 0);
            obj2.SetActive(0);
            this.mListItems.Add(obj2);
        Label_036A:
            num += 1;
        Label_036E:
            if (num < ((int) this.mUnlocks.Length))
            {
                goto Label_012A;
            }
            if (this.mListItems.Count > 0)
            {
                goto Label_038E;
            }
            return;
        Label_038E:
            if ((this.ItemWindow != null) == null)
            {
                goto Label_03B0;
            }
            this.mWindowAnimator = this.ItemWindow.GetComponent<Animator>();
        Label_03B0:
            this.mListItems[0].SetActive(1);
            return;
        }

        private void Update()
        {
            if (this.mListItems.Count > 0)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.CloseButton.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0071;
            }
            if (this.mListItems.Count > 1)
            {
                goto Label_0071;
            }
            if (GameUtility.CompareAnimatorStateName(this.mWindowAnimator, this.ItemOpendAnimation) == null)
            {
                goto Label_0071;
            }
            if (this.mWindowAnimator.IsInTransition(0) != null)
            {
                goto Label_0071;
            }
            this.CloseButton.get_gameObject().SetActive(1);
            return;
        Label_0071:
            if (GameUtility.CompareAnimatorStateName(this.mWindowAnimator, this.ItemEndAnimation) == null)
            {
                goto Label_00DA;
            }
            if (this.mWindowAnimator.IsInTransition(0) != null)
            {
                goto Label_00DA;
            }
            this.mListItems[0].SetActive(0);
            this.mListItems.RemoveAt(0);
            this.mListItems[0].SetActive(1);
            GameUtility.SetAnimatorBool(this.mWindowAnimator, this.ItemCloseFlag, 0);
        Label_00DA:
            return;
        }
    }
}

