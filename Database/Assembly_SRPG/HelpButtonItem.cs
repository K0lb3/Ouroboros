namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class HelpButtonItem : MonoBehaviour
    {
        public GameObject HelpMainTemplate;
        public GameObject m_MainWindowBase;
        public GameObject m_HelpMain;

        public HelpButtonItem()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private UnityAction <UpdateParam>m__34A(int n)
        {
            <UpdateParam>c__AnonStorey34D storeyd;
            storeyd = new <UpdateParam>c__AnonStorey34D();
            storeyd.n = n;
            storeyd.<>f__this = this;
            return new UnityAction(storeyd, this.<>m__34B);
        }

        private void OnSelectMenu(int MenuID)
        {
            HelpWindow window;
            window = base.get_transform().GetComponentInParent<HelpWindow>();
            if ((window == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            if (window.MiddleHelp == null)
            {
                goto Label_0030;
            }
            window.CreateMainWindow(MenuID);
            goto Label_0038;
        Label_0030:
            window.UpdateHelpList(1, MenuID);
        Label_0038:
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        private void UpdateParam(int Idx)
        {
            HelpWindow window;
            int num;
            Transform transform;
            LText text;
            string str;
            string str2;
            int num2;
            int num3;
            int num4;
            string str3;
            Button button;
            Func<int, UnityAction> func;
            window = base.get_transform().GetComponentInParent<HelpWindow>();
            if ((window == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            num = Idx;
            transform = base.get_transform().FindChild("Label");
            if ((transform != null) == null)
            {
                goto Label_011C;
            }
            text = transform.GetComponent<LText>();
            if (window.MiddleHelp == null)
            {
                goto Label_00FF;
            }
            str = LocalizedText.Get("help.MENU_CATE_NAME_" + ((int) (window.SelectMiddleID + 1)));
            str2 = LocalizedText.Get("help.MENU_NUM");
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_0081;
            }
            return;
        Label_0081:
            num2 = int.Parse(str2);
            num3 = 0;
            num4 = 0;
            goto Label_00CC;
        Label_0095:
            str3 = LocalizedText.Get("help.MENU_CATE_" + ((int) (num4 + 1)));
            if (string.Equals(str, str3) == null)
            {
                goto Label_00C6;
            }
            num3 = num4;
            goto Label_00D5;
        Label_00C6:
            num4 += 1;
        Label_00CC:
            if (num4 < num2)
            {
                goto Label_0095;
            }
        Label_00D5:
            text.set_text(LocalizedText.Get("help.MENU_" + ((int) ((num3 + Idx) + 1))));
            num = num3 + Idx;
            goto Label_011C;
        Label_00FF:
            text.set_text(LocalizedText.Get("help.MENU_CATE_NAME_" + ((int) (Idx + 1))));
        Label_011C:
            button = base.get_transform().GetComponent<Button>();
            func = new Func<int, UnityAction>(this.<UpdateParam>m__34A);
            button.get_onClick().RemoveAllListeners();
            button.get_onClick().AddListener(func(num));
            return;
        }

        [CompilerGenerated]
        private sealed class <UpdateParam>c__AnonStorey34D
        {
            internal int n;
            internal HelpButtonItem <>f__this;

            public <UpdateParam>c__AnonStorey34D()
            {
                base..ctor();
                return;
            }

            internal void <>m__34B()
            {
                this.<>f__this.OnSelectMenu(this.n);
                return;
            }
        }
    }
}

