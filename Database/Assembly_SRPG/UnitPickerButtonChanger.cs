namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitPickerButtonChanger : MonoBehaviour
    {
        [CustomField("ウィンド", 1), CustomGroup("ウィンド")]
        public GameObject m_Root;
        [CustomGroup("差し替えボタン画像"), CustomField("OFF", 5)]
        public Sprite m_ImageDefault;
        [CustomField("ON", 5), CustomGroup("差し替えボタン画像")]
        public Sprite m_ImageOn;
        [CustomGroup("オブジェクト"), CustomField("イメージ", 4)]
        public Image m_Image;
        [CustomField("テキスト", 2), CustomGroup("オブジェクト")]
        public Text m_Text;
        private UnitListWindow m_Window;
        private UnitListSortWindow.SelectType m_Sort;
        private UnitListFilterWindow.SelectType m_Filter;

        public UnitPickerButtonChanger()
        {
            this.m_Filter = 0x7fffdf;
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.m_Root != null) == null)
            {
                goto Label_0022;
            }
            this.m_Window = this.m_Root.GetComponent<UnitListWindow>();
        Label_0022:
            return;
        }

        private void OnDisable()
        {
        }

        private void OnEnable()
        {
        }

        private void Start()
        {
        }

        private void Update()
        {
            string str;
            UnitListFilterWindow.SelectType type;
            if ((this.m_Window == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.m_Text != null) == null)
            {
                goto Label_009D;
            }
            if (this.m_Window.sortWindow == null)
            {
                goto Label_009D;
            }
            if (this.m_Sort == this.m_Window.sortWindow.GetSectionReg())
            {
                goto Label_009D;
            }
            this.m_Sort = this.m_Window.sortWindow.GetSectionReg();
            str = UnitListSortWindow.GetText(this.m_Sort);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_009D;
            }
            if ((this.m_Text.get_text() != str) == null)
            {
                goto Label_009D;
            }
            this.m_Text.set_text(str);
        Label_009D:
            if ((this.m_Image != null) == null)
            {
                goto Label_011F;
            }
            if (this.m_Window.filterWindow == null)
            {
                goto Label_011F;
            }
            type = this.m_Window.filterWindow.GetSelectReg(0x7fffdf);
            if (this.m_Filter == type)
            {
                goto Label_011F;
            }
            this.m_Filter = type;
            if ((this.m_Filter ^ 0x7fffdf) != null)
            {
                goto Label_010E;
            }
            this.m_Image.set_sprite(this.m_ImageDefault);
            goto Label_011F;
        Label_010E:
            this.m_Image.set_sprite(this.m_ImageOn);
        Label_011F:
            return;
        }
    }
}

