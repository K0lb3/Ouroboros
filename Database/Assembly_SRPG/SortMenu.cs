namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(2, "Restore State", 0, 2), Pin(1, "Open", 1, 1)]
    public class SortMenu : MonoBehaviour, IFlowInterface
    {
        public bool LocalizeCaption;
        public string DefaultCaption;
        public Toggle Ascending;
        public Toggle Descending;
        public SortMenuEvent OnAccept;
        public SortMenuItem[] Items;
        public SortMenuItem[] Filters;
        public Button ToggleFiltersOn;
        public Button ToggleFiltersOff;
        private bool mSelectedAscending;
        public bool UseFilterCaption;

        public SortMenu()
        {
            this.Items = new SortMenuItem[0];
            this.Filters = new SortMenuItem[0];
            base..ctor();
            return;
        }

        public void Accept()
        {
            this.SaveState();
            if (this.OnAccept == null)
            {
                goto Label_001D;
            }
            this.OnAccept(this);
        Label_001D:
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 2)
            {
                goto Label_000E;
            }
            this.RestoreState();
            return;
        Label_000E:
            return;
        }

        public unsafe bool Contains(string method)
        {
            int num;
            num = 0;
            goto Label_0029;
        Label_0007:
            if ((&(this.Items[num]).Method == method) == null)
            {
                goto Label_0025;
            }
            return 1;
        Label_0025:
            num += 1;
        Label_0029:
            if (num < ((int) this.Items.Length))
            {
                goto Label_0007;
            }
            return 0;
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

        public void Open()
        {
            this.RestoreState();
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        }

        public unsafe void Reset()
        {
            int num;
            num = 0;
            goto Label_003E;
        Label_0007:
            if ((&(this.Items[num]).Toggle != null) == null)
            {
                goto Label_003A;
            }
            GameUtility.SetToggle(&(this.Items[num]).Toggle, 0);
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) this.Items.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void RestoreState()
        {
            int num;
            int num2;
            this.IsAscending = this.mSelectedAscending;
            num = 0;
            goto Label_005A;
        Label_0013:
            if ((&(this.Items[num]).Toggle != null) == null)
            {
                goto Label_0056;
            }
            GameUtility.SetToggle(&(this.Items[num]).Toggle, &(this.Items[num]).LastState);
        Label_0056:
            num += 1;
        Label_005A:
            if (num < ((int) this.Items.Length))
            {
                goto Label_0013;
            }
            num2 = 0;
            goto Label_00B6;
        Label_006F:
            if ((&(this.Filters[num2]).Toggle != null) == null)
            {
                goto Label_00B2;
            }
            GameUtility.SetToggle(&(this.Filters[num2]).Toggle, &(this.Filters[num2]).LastState);
        Label_00B2:
            num2 += 1;
        Label_00B6:
            if (num2 < ((int) this.Filters.Length))
            {
                goto Label_006F;
            }
            return;
        }

        public unsafe void SaveState()
        {
            int num;
            int num2;
            this.mSelectedAscending = this.IsAscending;
            num = 0;
            goto Label_005A;
        Label_0013:
            if ((&(this.Items[num]).Toggle != null) == null)
            {
                goto Label_0056;
            }
            &(this.Items[num]).LastState = &(this.Items[num]).Toggle.get_isOn();
        Label_0056:
            num += 1;
        Label_005A:
            if (num < ((int) this.Items.Length))
            {
                goto Label_0013;
            }
            num2 = 0;
            goto Label_00B6;
        Label_006F:
            if ((&(this.Filters[num2]).Toggle != null) == null)
            {
                goto Label_00B2;
            }
            &(this.Filters[num2]).LastState = &(this.Filters[num2]).Toggle.get_isOn();
        Label_00B2:
            num2 += 1;
        Label_00B6:
            if (num2 < ((int) this.Filters.Length))
            {
                goto Label_006F;
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
            if ((this.ToggleFiltersOff != null) == null)
            {
                goto Label_002D;
            }
            this.ToggleFiltersOff.get_onClick().AddListener(new UnityAction(this, this.SetAllFiltersOff));
        Label_002D:
            if ((this.ToggleFiltersOn != null) == null)
            {
                goto Label_005A;
            }
            this.ToggleFiltersOn.get_onClick().AddListener(new UnityAction(this, this.SetAllFiltersOn));
        Label_005A:
            return;
        }

        public string CurrentCaption
        {
            get
            {
                int num;
                int num2;
                num = 0;
                goto Label_0076;
            Label_0007:
                if ((&(this.Items[num]).Toggle != null) == null)
                {
                    goto Label_0072;
                }
                if (&(this.Items[num]).Toggle.get_isOn() == null)
                {
                    goto Label_0072;
                }
                if (this.LocalizeCaption == null)
                {
                    goto Label_0060;
                }
                return LocalizedText.Get(&(this.Items[num]).Caption);
            Label_0060:
                return &(this.Items[num]).Caption;
            Label_0072:
                num += 1;
            Label_0076:
                if (num < ((int) this.Items.Length))
                {
                    goto Label_0007;
                }
                if (this.UseFilterCaption == null)
                {
                    goto Label_0113;
                }
                num2 = 0;
                goto Label_0105;
            Label_0096:
                if ((&(this.Filters[num2]).Toggle != null) == null)
                {
                    goto Label_0101;
                }
                if (&(this.Filters[num2]).Toggle.get_isOn() == null)
                {
                    goto Label_0101;
                }
                if (this.LocalizeCaption == null)
                {
                    goto Label_00EF;
                }
                return LocalizedText.Get(&(this.Filters[num2]).Caption);
            Label_00EF:
                return &(this.Filters[num2]).Caption;
            Label_0101:
                num2 += 1;
            Label_0105:
                if (num2 < ((int) this.Filters.Length))
                {
                    goto Label_0096;
                }
            Label_0113:
                if (this.LocalizeCaption == null)
                {
                    goto Label_012A;
                }
                return LocalizedText.Get(this.DefaultCaption);
            Label_012A:
                return this.DefaultCaption;
            }
        }

        public string SortMethod
        {
            get
            {
                int num;
                num = 0;
                goto Label_0054;
            Label_0007:
                if ((&(this.Items[num]).Toggle != null) == null)
                {
                    goto Label_0050;
                }
                if (&(this.Items[num]).Toggle.get_isOn() == null)
                {
                    goto Label_0050;
                }
                return &(this.Items[num]).Method;
            Label_0050:
                num += 1;
            Label_0054:
                if (num < ((int) this.Items.Length))
                {
                    goto Label_0007;
                }
                return null;
            }
            set
            {
                int num;
                num = 0;
                goto Label_0054;
            Label_0007:
                if ((&(this.Items[num]).Toggle != null) == null)
                {
                    goto Label_0050;
                }
                GameUtility.SetToggle(&(this.Items[num]).Toggle, &(this.Items[num]).Method == value);
            Label_0050:
                num += 1;
            Label_0054:
                if (num < ((int) this.Items.Length))
                {
                    goto Label_0007;
                }
                return;
            }
        }

        public bool IsAscending
        {
            get
            {
                return (this.IsDescending == 0);
            }
            set
            {
                this.IsDescending = value == 0;
                return;
            }
        }

        public bool IsDescending
        {
            get
            {
                if ((this.Ascending != null) == null)
                {
                    goto Label_0020;
                }
                return (this.Ascending.get_isOn() == 0);
            Label_0020:
                return 0;
            }
            set
            {
                if ((this.Ascending != null) == null)
                {
                    goto Label_0020;
                }
                GameUtility.SetToggle(this.Ascending, value == 0);
            Label_0020:
                if ((this.Descending != null) == null)
                {
                    goto Label_003D;
                }
                GameUtility.SetToggle(this.Descending, value);
            Label_003D:
                return;
            }
        }

        public delegate void SortMenuEvent(SortMenu menu);

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct SortMenuItem
        {
            public string Method;
            public UnityEngine.UI.Toggle Toggle;
            public string Caption;
            [NonSerialized]
            public bool LastState;
        }
    }
}

