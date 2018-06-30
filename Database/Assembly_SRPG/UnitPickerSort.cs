namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(10, "Open", 1, 10)]
    public class UnitPickerSort : MonoBehaviour, IFlowInterface
    {
        public string MenuID;
        public bool LocalizeCaption;
        public string DefaultCaption;
        public bool UseFilterCaption;
        public bool mSelectedAscending;
        public Toggle Ascending;
        public Toggle Descending;
        public SortMenu.SortMenuItem[] Items;
        public SRPG_Button DecideButton;
        public SortEvent OnAccept;

        public UnitPickerSort()
        {
            this.MenuID = "UNITLIST";
            this.Items = new SortMenu.SortMenuItem[0];
            base..ctor();
            return;
        }

        private void Accept()
        {
            this.SaveState();
            PlayerPrefsUtility.SetString(this.MenuID, this.SortMethod, 0);
            PlayerPrefsUtility.SetInt(this.MenuID + "#", (this.IsAscending == null) ? 0 : 1, 0);
            PlayerPrefsUtility.Save();
            if (this.OnAccept == null)
            {
                goto Label_0069;
            }
            this.OnAccept(this.SortMethod, this.IsAscending);
        Label_0069:
            return;
        }

        public void Activated(int pinID)
        {
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

        public string GetSortMethod()
        {
            string str;
            str = string.Empty;
            str = this.SortMethod.ToLower();
            return (((char) char.ToUpper(str[0])) + str.Substring(1));
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
            return;
        }

        public unsafe void SaveState()
        {
            int num;
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
            return;
        }

        public void SetUp(string method)
        {
            string str;
            bool flag;
            if (PlayerPrefsUtility.HasKey(this.MenuID) == null)
            {
                goto Label_0028;
            }
            str = PlayerPrefsUtility.GetString(this.MenuID, "TIME");
            this.SortMethod = str;
        Label_0028:
            if (PlayerPrefsUtility.HasKey(this.MenuID + "#") == null)
            {
                goto Label_006C;
            }
            flag = (PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != null) ? 1 : 0;
            this.IsAscending = flag;
        Label_006C:
            this.SaveState();
            return;
        }

        public void SetUp(string method, bool ascending)
        {
            this.SortMethod = (string.IsNullOrEmpty(method) != null) ? "TIME" : method;
            this.IsAscending = ascending;
            this.SaveState();
            return;
        }

        private void Start()
        {
            if ((this.DecideButton != null) == null)
            {
                goto Label_002D;
            }
            this.DecideButton.get_onClick().AddListener(new UnityAction(this, this.Accept));
        Label_002D:
            return;
        }

        public string CurrentCaption
        {
            get
            {
                int num;
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
                if (this.LocalizeCaption == null)
                {
                    goto Label_009B;
                }
                return LocalizedText.Get(this.DefaultCaption);
            Label_009B:
                return this.DefaultCaption;
            }
        }

        public string SortMethod
        {
            get
            {
                int num;
                if (this.Items == null)
                {
                    goto Label_007B;
                }
                if (((int) this.Items.Length) <= 0)
                {
                    goto Label_007B;
                }
                num = 0;
                goto Label_006D;
            Label_0020:
                if ((&(this.Items[num]).Toggle != null) == null)
                {
                    goto Label_0069;
                }
                if (&(this.Items[num]).Toggle.get_isOn() == null)
                {
                    goto Label_0069;
                }
                return &(this.Items[num]).Method;
            Label_0069:
                num += 1;
            Label_006D:
                if (num < ((int) this.Items.Length))
                {
                    goto Label_0020;
                }
            Label_007B:
                return null;
            }
            set
            {
                int num;
                if (this.Items == null)
                {
                    goto Label_007B;
                }
                if (((int) this.Items.Length) <= 0)
                {
                    goto Label_007B;
                }
                num = 0;
                goto Label_006D;
            Label_0020:
                if ((&(this.Items[num]).Toggle != null) == null)
                {
                    goto Label_0069;
                }
                GameUtility.SetToggle(&(this.Items[num]).Toggle, &(this.Items[num]).Method == value);
            Label_0069:
                num += 1;
            Label_006D:
                if (num < ((int) this.Items.Length))
                {
                    goto Label_0020;
                }
            Label_007B:
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

        public delegate void SortEvent(string method, bool ascending);
    }
}

