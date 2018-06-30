namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class SortMenuButton : SRPG_Button
    {
        public GameObject Target;
        public SortMenu Menu;
        public Text Caption;
        private SortMenu mMenu;
        private GameObject mMenuObject;
        public string MenuID;
        public string FilterActive;
        public bool CreateMenuInstance;

        public SortMenuButton()
        {
            this.CreateMenuInstance = 1;
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            if (Application.get_isPlaying() == null)
            {
                goto Label_0093;
            }
            if ((this.Menu != null) == null)
            {
                goto Label_007C;
            }
            if (this.CreateMenuInstance == null)
            {
                goto Label_0059;
            }
            this.mMenu = Object.Instantiate<SortMenu>(this.Menu);
            this.mMenu.OnAccept = new SortMenu.SortMenuEvent(this.OnSortChange);
            goto Label_007C;
        Label_0059:
            this.mMenu = this.Menu;
            this.mMenu.OnAccept = new SortMenu.SortMenuEvent(this.OnSortChange);
        Label_007C:
            base.get_onClick().AddListener(new UnityAction(this, this.OpenSortMenu));
        Label_0093:
            return;
        }

        public void ForceReloadFilter()
        {
            char[] chArray1;
            string str;
            bool flag;
            string str2;
            string str3;
            if (Application.get_isPlaying() == null)
            {
                goto Label_012D;
            }
            if ((this.mMenu != null) == null)
            {
                goto Label_012D;
            }
            str = null;
            if (string.IsNullOrEmpty(this.MenuID) != null)
            {
                goto Label_00FB;
            }
            if (PlayerPrefsUtility.HasKey(this.MenuID) == null)
            {
                goto Label_0053;
            }
            str = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
            goto Label_005F;
        Label_0053:
            str = this.mMenu.SortMethod;
        Label_005F:
            str = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
            flag = (PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) == 0) == 0;
            this.mMenu.IsAscending = flag;
            if (this.mMenu.Contains(str) == null)
            {
                goto Label_00B6;
            }
            this.mMenu.SortMethod = str;
        Label_00B6:
            str2 = this.MenuID + "&";
            if (PlayerPrefsUtility.HasKey(str2) == null)
            {
                goto Label_00FB;
            }
            str3 = PlayerPrefsUtility.GetString(str2, string.Empty);
            chArray1 = new char[] { 0x7c };
            this.mMenu.SetFilters(str3.Split(chArray1), 1);
        Label_00FB:
            this.mMenu.SaveState();
            if ((this.Caption != null) == null)
            {
                goto Label_012D;
            }
            this.Caption.set_text(this.mMenu.CurrentCaption);
        Label_012D:
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mMenu != null) == null)
            {
                goto Label_0028;
            }
            Object.Destroy(this.mMenu.get_gameObject());
            this.mMenu = null;
        Label_0028:
            base.OnDestroy();
            return;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if ((this.mMenu != null) == null)
            {
                goto Label_002F;
            }
            this.UpdateFilterState((this.mMenu.GetFilters(0) == null) == 0);
        Label_002F:
            return;
        }

        private void OnSortChange(SortMenu menu)
        {
            string str;
            string[] strArray;
            string str2;
            str = menu.SortMethod;
            if (string.IsNullOrEmpty(this.MenuID) != null)
            {
                goto Label_0099;
            }
            PlayerPrefsUtility.SetString(this.MenuID, str, 0);
            PlayerPrefsUtility.SetInt(this.MenuID + "#", (this.mMenu.IsAscending == null) ? 0 : 1, 0);
            strArray = this.mMenu.GetFilters(1);
            str2 = (strArray == null) ? string.Empty : string.Join("|", strArray);
            PlayerPrefsUtility.SetString(this.MenuID + "&", str2, 0);
            PlayerPrefsUtility.Save();
        Label_0099:
            if ((this.Caption != null) == null)
            {
                goto Label_00C0;
            }
            this.Caption.set_text(this.mMenu.CurrentCaption);
        Label_00C0:
            this.UpdateTarget(str, menu.IsAscending);
            return;
        }

        public void OpenSortMenu()
        {
            this.mMenu.Open();
            return;
        }

        protected override void Start()
        {
            char[] chArray1;
            string str;
            bool flag;
            string str2;
            string str3;
            base.Start();
            if (Application.get_isPlaying() == null)
            {
                goto Label_0145;
            }
            if ((this.mMenu != null) == null)
            {
                goto Label_0145;
            }
            str = null;
            if (string.IsNullOrEmpty(this.MenuID) != null)
            {
                goto Label_0101;
            }
            if (PlayerPrefsUtility.HasKey(this.MenuID) == null)
            {
                goto Label_0059;
            }
            str = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
            goto Label_0065;
        Label_0059:
            str = this.mMenu.SortMethod;
        Label_0065:
            str = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
            flag = (PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) == 0) == 0;
            this.mMenu.IsAscending = flag;
            if (this.mMenu.Contains(str) == null)
            {
                goto Label_00BC;
            }
            this.mMenu.SortMethod = str;
        Label_00BC:
            str2 = this.MenuID + "&";
            if (PlayerPrefsUtility.HasKey(str2) == null)
            {
                goto Label_0101;
            }
            str3 = PlayerPrefsUtility.GetString(str2, string.Empty);
            chArray1 = new char[] { 0x7c };
            this.mMenu.SetFilters(str3.Split(chArray1), 1);
        Label_0101:
            this.mMenu.SaveState();
            if ((this.Caption != null) == null)
            {
                goto Label_0133;
            }
            this.Caption.set_text(this.mMenu.CurrentCaption);
        Label_0133:
            this.UpdateTarget(str, this.mMenu.IsAscending);
        Label_0145:
            return;
        }

        protected virtual void UpdateFilterState(bool active)
        {
            Animator animator;
            if (string.IsNullOrEmpty(this.FilterActive) != null)
            {
                goto Label_0030;
            }
            animator = base.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0030;
            }
            animator.SetBool(this.FilterActive, active);
        Label_0030:
            return;
        }

        private void UpdateTarget(string method, bool ascending)
        {
            string[] strArray;
            ISortableList list;
            if ((this.mMenu == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            strArray = this.mMenu.GetFilters(0);
            this.UpdateFilterState((strArray == null) == 0);
            if ((this.Target != null) == null)
            {
                goto Label_0058;
            }
            list = this.Target.GetComponent<ISortableList>();
            if (list == null)
            {
                goto Label_0058;
            }
            list.SetSortMethod(method, ascending, strArray);
        Label_0058:
            return;
        }
    }
}

