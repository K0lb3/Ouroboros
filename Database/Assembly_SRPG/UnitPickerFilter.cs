namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(2, "Restore State", 0, 2), Pin(10, "Open", 1, 1)]
    public class UnitPickerFilter : MonoBehaviour, IFlowInterface
    {
        public string MenuID;
        public bool LocalizeCaption;
        public string DefaultCaption;
        public bool UseFilterCaption;
        public Button ToggleFiltersOn;
        public Button ToggleFiltersOff;
        public SortMenu.SortMenuItem[] Filters;
        public SRPG_Button DecideButton;
        public FilterEvent OnAccept;

        public UnitPickerFilter()
        {
            this.MenuID = "UNITLIST";
            this.Filters = new SortMenu.SortMenuItem[0];
            base..ctor();
            return;
        }

        private void Accecpt()
        {
            string[] strArray;
            string str;
            this.SaveState();
            strArray = this.GetFilters(1);
            str = (strArray == null) ? string.Empty : string.Join("&", strArray);
            PlayerPrefsUtility.SetString(this.MenuID + "&", str, 1);
            if (this.OnAccept == null)
            {
                goto Label_005F;
            }
            this.OnAccept(this.GetFilters(0));
        Label_005F:
            return;
        }

        public void Activated(int pinID)
        {
        }

        public unsafe string[] GetFilters(bool invert)
        {
            List<string> list;
            int num;
            int num2;
            list = new List<string>();
            if (invert == null)
            {
                goto Label_0085;
            }
            num = 0;
            goto Label_0065;
        Label_0013:
            if ((&(this.Filters[num]).Toggle != null) == null)
            {
                goto Label_0061;
            }
            if (&(this.Filters[num]).Toggle.get_isOn() != null)
            {
                goto Label_0061;
            }
            list.Add(&(this.Filters[num]).Method);
        Label_0061:
            num += 1;
        Label_0065:
            if (num < ((int) this.Filters.Length))
            {
                goto Label_0013;
            }
            if (list.Count != null)
            {
                goto Label_0101;
            }
            return null;
            goto Label_0101;
        Label_0085:
            num2 = 0;
            goto Label_00DE;
        Label_008C:
            if ((&(this.Filters[num2]).Toggle != null) == null)
            {
                goto Label_00DA;
            }
            if (&(this.Filters[num2]).Toggle.get_isOn() == null)
            {
                goto Label_00DA;
            }
            list.Add(&(this.Filters[num2]).Method);
        Label_00DA:
            num2 += 1;
        Label_00DE:
            if (num2 < ((int) this.Filters.Length))
            {
                goto Label_008C;
            }
            if (((int) this.Filters.Length) != list.Count)
            {
                goto Label_0101;
            }
            return null;
        Label_0101:
            return list.ToArray();
        }

        public unsafe string[] GetFiltersAll()
        {
            List<string> list;
            int num;
            list = new List<string>();
            num = 0;
            goto Label_005F;
        Label_000D:
            if ((&(this.Filters[num]).Toggle != null) == null)
            {
                goto Label_005B;
            }
            if (&(this.Filters[num]).Toggle.get_isOn() == null)
            {
                goto Label_005B;
            }
            list.Add(&(this.Filters[num]).Method);
        Label_005B:
            num += 1;
        Label_005F:
            if (num < ((int) this.Filters.Length))
            {
                goto Label_000D;
            }
            return list.ToArray();
        }

        public void Open()
        {
            this.RestoreState();
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            return;
        }

        public unsafe void RestoreState()
        {
            int num;
            num = 0;
            goto Label_004E;
        Label_0007:
            if ((&(this.Filters[num]).Toggle != null) == null)
            {
                goto Label_004A;
            }
            GameUtility.SetToggle(&(this.Filters[num]).Toggle, &(this.Filters[num]).LastState);
        Label_004A:
            num += 1;
        Label_004E:
            if (num < ((int) this.Filters.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void SaveState()
        {
            int num;
            num = 0;
            goto Label_004E;
        Label_0007:
            if ((&(this.Filters[num]).Toggle != null) == null)
            {
                goto Label_004A;
            }
            &(this.Filters[num]).LastState = &(this.Filters[num]).Toggle.get_isOn();
        Label_004A:
            num += 1;
        Label_004E:
            if (num < ((int) this.Filters.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void SetAllFiltersOff()
        {
            int num;
            num = 0;
            goto Label_003E;
        Label_0007:
            if ((&(this.Filters[num]).Toggle != null) == null)
            {
                goto Label_003A;
            }
            GameUtility.SetToggle(&(this.Filters[num]).Toggle, 0);
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) this.Filters.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void SetAllFiltersOn()
        {
            int num;
            num = 0;
            goto Label_003E;
        Label_0007:
            if ((&(this.Filters[num]).Toggle != null) == null)
            {
                goto Label_003A;
            }
            GameUtility.SetToggle(&(this.Filters[num]).Toggle, 1);
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) this.Filters.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void SetFilters(string[] filters, bool invert)
        {
            int num;
            bool flag;
            if (filters == null)
            {
                goto Label_000E;
            }
            if (((int) filters.Length) != null)
            {
                goto Label_0026;
            }
        Label_000E:
            if (invert == null)
            {
                goto Label_001F;
            }
            this.SetAllFiltersOn();
            goto Label_0025;
        Label_001F:
            this.SetAllFiltersOff();
        Label_0025:
            return;
        Label_0026:
            num = 0;
            goto Label_00A8;
        Label_002D:
            if (string.IsNullOrEmpty(&(this.Filters[num]).Method) != null)
            {
                goto Label_00A4;
            }
            if ((&(this.Filters[num]).Toggle != null) == null)
            {
                goto Label_00A4;
            }
            flag = (Array.IndexOf<string>(filters, &(this.Filters[num]).Method) < 0) == 0;
            if (invert == null)
            {
                goto Label_008D;
            }
            flag = flag == 0;
        Label_008D:
            GameUtility.SetToggle(&(this.Filters[num]).Toggle, flag);
        Label_00A4:
            num += 1;
        Label_00A8:
            if (num < ((int) this.Filters.Length))
            {
                goto Label_002D;
            }
            return;
        }

        private void Start()
        {
            if ((this.DecideButton != null) == null)
            {
                goto Label_002D;
            }
            this.DecideButton.get_onClick().AddListener(new UnityAction(this, this.Accecpt));
        Label_002D:
            if ((this.ToggleFiltersOff != null) == null)
            {
                goto Label_005A;
            }
            this.ToggleFiltersOff.get_onClick().AddListener(new UnityAction(this, this.SetAllFiltersOff));
        Label_005A:
            if ((this.ToggleFiltersOn != null) == null)
            {
                goto Label_0087;
            }
            this.ToggleFiltersOn.get_onClick().AddListener(new UnityAction(this, this.SetAllFiltersOn));
        Label_0087:
            return;
        }

        public string CurrentCaption
        {
            get
            {
                int num;
                if (this.UseFilterCaption == null)
                {
                    goto Label_008F;
                }
                num = 0;
                goto Label_0081;
            Label_0012:
                if ((&(this.Filters[num]).Toggle != null) == null)
                {
                    goto Label_007D;
                }
                if (&(this.Filters[num]).Toggle.get_isOn() == null)
                {
                    goto Label_007D;
                }
                if (this.LocalizeCaption == null)
                {
                    goto Label_006B;
                }
                return LocalizedText.Get(&(this.Filters[num]).Caption);
            Label_006B:
                return &(this.Filters[num]).Caption;
            Label_007D:
                num += 1;
            Label_0081:
                if (num < ((int) this.Filters.Length))
                {
                    goto Label_0012;
                }
            Label_008F:
                if (this.LocalizeCaption == null)
                {
                    goto Label_00A6;
                }
                return LocalizedText.Get(this.DefaultCaption);
            Label_00A6:
                return this.DefaultCaption;
            }
        }

        public delegate void FilterEvent(string[] filter);
    }
}

