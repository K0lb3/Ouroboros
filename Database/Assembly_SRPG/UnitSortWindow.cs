namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0x66, "キャンセル", 1, 2), Pin(0x65, "決定", 1, 1)]
    public class UnitSortWindow : MonoBehaviour, IFlowInterface
    {
        public Toggle[] ToggleElements;
        public Button BtnReset;
        public Button BtnOk;
        public Button BtnClose;
        private bool[] mToggleBkupValues;
        private bool mPlaySE;

        public UnitSortWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void OnCancel()
        {
            int num;
            num = 0;
            goto Label_0019;
        Label_0007:
            GameUtility.SetUnitShowSetting(num, this.mToggleBkupValues[num]);
            num += 1;
        Label_0019:
            if (num < ((int) this.mToggleBkupValues.Length))
            {
                goto Label_0007;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
            return;
        }

        private void OnDecide()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private void OnReset()
        {
            bool flag;
            int num;
            GameUtility.ResetUnitShowSetting();
            flag = this.mPlaySE;
            this.mPlaySE = 0;
            num = 0;
            goto Label_0031;
        Label_001A:
            this.ToggleElements[num].set_isOn(GameUtility.GetUnitShowSetting(num));
            num += 1;
        Label_0031:
            if (num < ((int) this.ToggleElements.Length))
            {
                goto Label_001A;
            }
            this.mPlaySE = flag;
            return;
        }

        private void SelectToggleElement(int index)
        {
            Toggle toggle;
            GameObject obj2;
            SystemSound sound;
            GameUtility.SetUnitShowSetting(index, this.ToggleElements[index].get_isOn());
            if (this.mPlaySE != null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:;
            toggle = (((this.ToggleElements != null) && (index >= 0)) && (index < ((int) this.ToggleElements.Length))) ? this.ToggleElements[index] : null;
            obj2 = ((toggle == null) == null) ? toggle.get_gameObject() : null;
            sound = ((obj2 == null) == null) ? obj2.GetComponentInChildren<SystemSound>() : null;
            if ((sound != null) == null)
            {
                goto Label_0092;
            }
            sound.Play();
        Label_0092:
            return;
        }

        private void Start()
        {
            int num;
            <Start>c__AnonStorey3DB storeydb;
            bool flag;
            if (this.ToggleElements == null)
            {
                goto Label_0087;
            }
            this.mToggleBkupValues = new bool[(int) this.ToggleElements.Length];
            num = 0;
            goto Label_0079;
        Label_0025:
            storeydb = new <Start>c__AnonStorey3DB();
            storeydb.<>f__this = this;
            storeydb.toggle_index = num;
            this.ToggleElements[num].onValueChanged.AddListener(new UnityAction<bool>(storeydb, this.<>m__47D));
            this.mToggleBkupValues[num] = flag = GameUtility.GetUnitShowSetting(num);
            this.ToggleElements[num].set_isOn(flag);
            num += 1;
        Label_0079:
            if (num < ((int) this.ToggleElements.Length))
            {
                goto Label_0025;
            }
        Label_0087:
            if ((this.BtnReset != null) == null)
            {
                goto Label_00B4;
            }
            this.BtnReset.get_onClick().AddListener(new UnityAction(this, this.OnReset));
        Label_00B4:
            if ((this.BtnOk != null) == null)
            {
                goto Label_00E1;
            }
            this.BtnOk.get_onClick().AddListener(new UnityAction(this, this.OnDecide));
        Label_00E1:
            if ((this.BtnClose != null) == null)
            {
                goto Label_010E;
            }
            this.BtnClose.get_onClick().AddListener(new UnityAction(this, this.OnCancel));
        Label_010E:
            this.mPlaySE = 1;
            return;
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey3DB
        {
            internal int toggle_index;
            internal UnitSortWindow <>f__this;

            public <Start>c__AnonStorey3DB()
            {
                base..ctor();
                return;
            }

            internal void <>m__47D(bool value)
            {
                this.<>f__this.SelectToggleElement(this.toggle_index);
                return;
            }
        }
    }
}

