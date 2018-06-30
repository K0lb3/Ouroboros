namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class MultiPlayRoomCreateController : MonoBehaviour
    {
        public Toggle PassToggle;
        public Toggle LimitToggle;
        public Toggle ClearToggle;
        public GameObject Filter;
        public GameObject ClearEnableButton;
        public GameObject ClearDisableButton;
        public GameObject LimitEnableButton;
        public GameObject LimitDisableButton;
        public GameObject DetailFilter;
        public ScrollablePulldown UnitLvPulldown;
        public GraphicRaycaster UnitPulldownBG;
        private int mMaxLv;
        private int mNowSelIdx;

        public MultiPlayRoomCreateController()
        {
            base..ctor();
            return;
        }

        public void OnClearToggle(bool isOn)
        {
            GlobalVars.MultiPlayClearOnly = isOn;
            return;
        }

        public void OnDisableClear()
        {
            GlobalVars.MultiPlayClearOnly = 0;
            return;
        }

        public void OnDisableLimit()
        {
            GlobalVars.SelectedMultiPlayLimit = 0;
            if ((this.DetailFilter != null) == null)
            {
                goto Label_0023;
            }
            this.DetailFilter.SetActive(1);
        Label_0023:
            return;
        }

        public void OnDisablePass()
        {
            if ((this.DetailFilter != null) == null)
            {
                goto Label_0024;
            }
            this.DetailFilter.SetActive(GlobalVars.SelectedMultiPlayLimit == 0);
        Label_0024:
            return;
        }

        public void OnEnableClear()
        {
            GlobalVars.MultiPlayClearOnly = 1;
            return;
        }

        public void OnEnableLimit()
        {
            GlobalVars.SelectedMultiPlayLimit = 1;
            if ((this.DetailFilter != null) == null)
            {
                goto Label_0023;
            }
            this.DetailFilter.SetActive(0);
        Label_0023:
            return;
        }

        public void OnEnablePass()
        {
            if ((this.DetailFilter != null) == null)
            {
                goto Label_001D;
            }
            this.DetailFilter.SetActive(1);
        Label_001D:
            return;
        }

        public void OnLimitToggle(bool isOn)
        {
            GlobalVars.SelectedMultiPlayLimit = isOn;
            if ((this.DetailFilter != null) == null)
            {
                goto Label_0026;
            }
            this.DetailFilter.SetActive(isOn == 0);
        Label_0026:
            return;
        }

        public void OnPassToggle(bool isOn)
        {
            if (isOn == null)
            {
                goto Label_0037;
            }
            if ((this.DetailFilter != null) == null)
            {
                goto Label_0023;
            }
            this.DetailFilter.SetActive(1);
        Label_0023:
            FlowNode_Variable.Set("MultiPlayPasscode", "1");
            goto Label_006A;
        Label_0037:
            if ((this.DetailFilter != null) == null)
            {
                goto Label_005B;
            }
            this.DetailFilter.SetActive(GlobalVars.SelectedMultiPlayLimit == 0);
        Label_005B:
            FlowNode_Variable.Set("MultiPlayPasscode", "0");
        Label_006A:
            if ((this.Filter != null) == null)
            {
                goto Label_0087;
            }
            this.Filter.SetActive(isOn);
        Label_0087:
            return;
        }

        private unsafe void OnUnitLvChange(int index)
        {
            GameManager manager;
            OInt[] numArray;
            numArray = MonoSingleton<GameManager>.Instance.MasterParam.GetMultiPlayLimitUnitLv();
            if (numArray == null)
            {
                goto Label_0090;
            }
            if (index >= ((int) numArray.Length))
            {
                goto Label_0090;
            }
            if (this.mMaxLv >= *(&(numArray[index])))
            {
                goto Label_0073;
            }
            this.UnitLvPulldown.Selection = this.mNowSelIdx;
            this.UnitLvPulldown.PrevSelection = -1;
            UIUtility.SystemMessage(LocalizedText.Get("sys.MULTI_LIMIT_ERROR"), null, null, 0, -1);
            goto Label_0090;
        Label_0073:
            this.mNowSelIdx = index;
            GlobalVars.MultiPlayJoinUnitLv = *(&(numArray[index]));
        Label_0090:
            return;
        }

        private unsafe void Refresh()
        {
            GameManager manager;
            string str;
            int num;
            OInt[] numArray;
            int num2;
            manager = MonoSingleton<GameManager>.Instance;
            str = string.Empty;
            num = 0;
            numArray = manager.MasterParam.GetMultiPlayLimitUnitLv();
            if (((this.UnitLvPulldown != null) == null) || (numArray == null))
            {
                goto Label_00F1;
            }
            num = 0;
            this.UnitLvPulldown.ClearItems();
            num2 = 0;
            goto Label_00BD;
        Label_0046:
            str = (*(&(numArray[num2])) != null) ? (&(numArray[num2]).ToString() + LocalizedText.Get("sys.MULTI_JOINLIMIT_OVER")) : LocalizedText.Get("sys.MULTI_JOINLIMIT_NONE");
            this.UnitLvPulldown.AddItem(str, num2);
            if (*(&(numArray[num2])) != GlobalVars.MultiPlayJoinUnitLv)
            {
                goto Label_00B7;
            }
            num = num2;
        Label_00B7:
            num2 += 1;
        Label_00BD:
            if (num2 < ((int) numArray.Length))
            {
                goto Label_0046;
            }
            this.mNowSelIdx = num;
            this.UnitLvPulldown.Selection = num;
            this.UnitLvPulldown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.OnUnitLvChange);
        Label_00F1:
            if ((this.ClearEnableButton != null) == null)
            {
                goto Label_0115;
            }
            this.ClearEnableButton.SetActive(GlobalVars.MultiPlayClearOnly == 0);
        Label_0115:
            if ((this.ClearDisableButton != null) == null)
            {
                goto Label_0136;
            }
            this.ClearDisableButton.SetActive(GlobalVars.MultiPlayClearOnly);
        Label_0136:
            if ((this.DetailFilter != null) == null)
            {
                goto Label_015A;
            }
            this.DetailFilter.SetActive(GlobalVars.SelectedMultiPlayLimit == 0);
        Label_015A:
            if ((this.LimitEnableButton != null) == null)
            {
                goto Label_017E;
            }
            this.LimitEnableButton.SetActive(GlobalVars.SelectedMultiPlayLimit == 0);
        Label_017E:
            if ((this.LimitDisableButton != null) == null)
            {
                goto Label_019F;
            }
            this.LimitDisableButton.SetActive(GlobalVars.SelectedMultiPlayLimit);
        Label_019F:
            if ((this.UnitPulldownBG != null) == null)
            {
                goto Label_01BC;
            }
            this.UnitPulldownBG.set_enabled(1);
        Label_01BC:
            if ((this.LimitToggle != null) == null)
            {
                goto Label_01DD;
            }
            this.LimitToggle.set_isOn(GlobalVars.SelectedMultiPlayLimit);
        Label_01DD:
            if ((this.ClearToggle != null) == null)
            {
                goto Label_01FE;
            }
            this.ClearToggle.set_isOn(GlobalVars.MultiPlayClearOnly);
        Label_01FE:
            return;
        }

        private void Start()
        {
            PlayerData data;
            List<UnitData> list;
            int num;
            list = MonoSingleton<GameManager>.Instance.Player.Units;
            num = 0;
            goto Label_0046;
        Label_0019:
            if (this.mMaxLv >= list[num].Lv)
            {
                goto Label_0042;
            }
            this.mMaxLv = list[num].Lv;
        Label_0042:
            num += 1;
        Label_0046:
            if (num < list.Count)
            {
                goto Label_0019;
            }
            if ((this.PassToggle != null) == null)
            {
                goto Label_007F;
            }
            this.PassToggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnPassToggle));
        Label_007F:
            if ((this.LimitToggle != null) == null)
            {
                goto Label_00AC;
            }
            this.LimitToggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnLimitToggle));
        Label_00AC:
            if ((this.ClearToggle != null) == null)
            {
                goto Label_00D9;
            }
            this.ClearToggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnClearToggle));
        Label_00D9:
            this.Refresh();
            return;
        }
    }
}

