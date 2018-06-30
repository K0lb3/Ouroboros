namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class HelpWindow : MonoBehaviour
    {
        public static readonly string VAR_NAME_MENU_ID;
        public bool ReferenceFlowVariable;
        public Button BackButton;
        public Button MiddleBackButton;
        private bool bMiddleHelp;
        private int SelMiddleIdx;
        private GameObject[] mHelpMenuButtons;
        public GameObject m_HelpMain;

        static HelpWindow()
        {
            VAR_NAME_MENU_ID = "HELP_MENU_ID";
            return;
        }

        public HelpWindow()
        {
            base..ctor();
            return;
        }

        public unsafe void CreateMainWindow(int MenuID)
        {
            string str;
            int num;
            int num2;
            float num3;
            Transform transform;
            Transform transform2;
            LText text;
            string str2;
            Transform transform3;
            LayoutElement element;
            bool flag;
            LayoutElement element2;
            Transform transform4;
            Transform transform5;
            LText text2;
            string str3;
            string str4;
            LayoutElement element3;
            HELP_ID help_id;
            RectTransform transform6;
            Vector2 vector;
            Transform transform7;
            RectTransform transform8;
            Vector2 vector2;
            Vector2 vector3;
            HELP_ID help_id2;
            str = LocalizedText.Get("help.MENU_NUM");
            if (str != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = int.Parse(str);
            if (MenuID < 0)
            {
                goto Label_0027;
            }
            if (MenuID < num)
            {
                goto Label_0028;
            }
        Label_0027:
            return;
        Label_0028:
            if ((this.m_HelpMain == null) == null)
            {
                goto Label_003A;
            }
            return;
        Label_003A:
            num2 = MenuID + 1;
            num3 = 0f;
            transform = this.m_HelpMain.get_transform().FindChild("header");
            if ((transform != null) == null)
            {
                goto Label_00D9;
            }
            text = transform.FindChild("Text").GetComponent<LText>();
            str2 = LocalizedText.Get("help.MAIN_TITLE_" + ((int) num2));
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_00BE;
            }
            if ((str2 == ("MAIN_TITLE_" + ((int) num2))) == null)
            {
                goto Label_00D0;
            }
        Label_00BE:
            transform.get_gameObject().SetActive(0);
            goto Label_00D9;
        Label_00D0:
            text.set_text(str2);
        Label_00D9:
            transform3 = this.m_HelpMain.get_transform().Find("page/template/contents/viewport/layout/contents_image");
            if (this.SetImageData(transform3, "Image", "Helps/help_image_" + ((int) num2)) == null)
            {
                goto Label_0125;
            }
            element = transform3.GetComponent<LayoutElement>();
            num3 += element.get_minHeight();
        Label_0125:
            flag = 0;
            transform3 = this.m_HelpMain.get_transform().Find("page/template/contents/viewport/layout/contents_image_small");
            flag |= this.SetImageData(transform3, "Image0", "Helps/help_image_" + ((int) num2) + "_0");
            if ((flag | this.SetImageData(transform3, "Image1", "Helps/help_image_" + ((int) num2) + "_1")) == null)
            {
                goto Label_01B4;
            }
            transform3.get_gameObject().SetActive(1);
            element2 = transform3.GetComponent<LayoutElement>();
            num3 += element2.get_minHeight();
        Label_01B4:
            transform4 = this.m_HelpMain.get_transform().FindChild("page/template/contents/viewport/layout/contents_text");
            if ((transform4 != null) == null)
            {
                goto Label_033C;
            }
            transform5 = transform4.FindChild("Text");
            text2 = transform5.GetComponent<LText>();
            str3 = LocalizedText.Get("help.MAIN_TEXT_" + ((int) num2));
            str4 = "help.MAIN_TEXT_" + ((int) num2);
            element3 = transform4.GetComponent<LayoutElement>();
            if (string.IsNullOrEmpty(str3) != null)
            {
                goto Label_0249;
            }
            if ((str3 == ("MAIN_TEXT_" + ((int) num2))) == null)
            {
                goto Label_025B;
            }
        Label_0249:
            transform4.get_gameObject().SetActive(0);
            goto Label_033C;
        Label_025B:
            text2.set_text(str4);
            transform4.get_gameObject().SetActive(1);
            num3 += element3.get_preferredHeight();
            help_id = num2;
            transform6 = transform5.GetComponent<RectTransform>();
            vector = transform6.get_anchoredPosition();
            help_id2 = help_id;
            switch ((help_id2 - 0x3d))
            {
                case 0:
                    goto Label_02DA;

                case 1:
                    goto Label_02B8;

                case 2:
                    goto Label_02B8;
            }
            if (help_id2 == 0x60)
            {
                goto Label_02FE;
            }
            goto Label_0322;
        Label_02B8:
            &vector.y = 250f;
            transform6.set_anchoredPosition(vector);
            num3 = element3.get_preferredHeight();
            goto Label_033C;
        Label_02DA:
            &vector.y = 150f;
            transform6.set_anchoredPosition(vector);
            num3 -= element3.get_preferredHeight();
            goto Label_033C;
        Label_02FE:
            &vector.y = 200f;
            transform6.set_anchoredPosition(vector);
            num3 -= element3.get_preferredHeight();
            goto Label_033C;
        Label_0322:
            &vector.y = 0f;
            transform6.set_anchoredPosition(vector);
        Label_033C:
            transform8 = this.m_HelpMain.get_transform().FindChild("page/template/contents/viewport/layout") as RectTransform;
            vector2 = transform8.get_anchoredPosition();
            vector3 = transform8.get_sizeDelta();
            &vector3.y = num3;
            transform8.set_sizeDelta(vector3);
            &vector2.y = 0f;
            transform8.set_anchoredPosition(vector2);
            this.m_HelpMain.SetActive(1);
            if (this.MiddleBackButton == null)
            {
                goto Label_03C1;
            }
            this.MiddleBackButton.get_gameObject().SetActive(0);
        Label_03C1:
            this.BackButton.get_gameObject().SetActive(1);
            return;
        }

        public void OnBackList()
        {
            if (this.MiddleBackButton == null)
            {
                goto Label_0021;
            }
            this.MiddleBackButton.get_gameObject().SetActive(0);
        Label_0021:
            this.UpdateHelpList(0, 0);
            return;
        }

        private void OnCloseMain()
        {
            this.m_HelpMain.SetActive(0);
            if (this.MiddleBackButton == null)
            {
                goto Label_002D;
            }
            this.MiddleBackButton.get_gameObject().SetActive(1);
        Label_002D:
            this.BackButton.get_gameObject().SetActive(0);
            return;
        }

        private void OnDestroy()
        {
            GameUtility.DestroyGameObjects(this.mHelpMenuButtons);
            return;
        }

        private bool SetImageData(Transform image, string childname, string texname)
        {
            Transform transform;
            RawImage image2;
            Texture2D textured;
            if ((image == null) == null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            transform = image.FindChild(childname);
            image2 = transform.GetComponent<RawImage>();
            textured = AssetManager.Load<Texture2D>(texname);
            if ((textured == null) != null)
            {
                goto Label_003C;
            }
            if ((image2 == null) == null)
            {
                goto Label_0056;
            }
        Label_003C:
            image.get_gameObject().SetActive(0);
            transform.get_gameObject().SetActive(0);
            return 0;
        Label_0056:
            image2.set_texture(textured);
            transform.get_gameObject().SetActive(1);
            image.get_gameObject().SetActive(1);
            return 1;
        }

        private unsafe void Start()
        {
            Transform transform;
            LText text;
            Transform transform2;
            LText text2;
            string str;
            int num;
            string str2;
            int num2;
            if ((this.BackButton != null) == null)
            {
                goto Label_005A;
            }
            this.BackButton.get_onClick().AddListener(new UnityAction(this, this.OnCloseMain));
            this.BackButton.get_transform().FindChild("Text").GetComponent<LText>().set_text(LocalizedText.Get("help.BACK_BUTTON"));
        Label_005A:
            if ((this.MiddleBackButton != null) == null)
            {
                goto Label_00B4;
            }
            this.MiddleBackButton.get_onClick().AddListener(new UnityAction(this, this.OnBackList));
            this.MiddleBackButton.get_transform().FindChild("Text").GetComponent<LText>().set_text(LocalizedText.Get("help.BACK_BUTTON"));
        Label_00B4:
            str = LocalizedText.Get("help.MENU_NUM");
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_00CD;
            }
            return;
        Label_00CD:
            num = int.Parse(str);
            this.mHelpMenuButtons = new GameObject[num];
            if (this.ReferenceFlowVariable == null)
            {
                goto Label_012D;
            }
            str2 = FlowNode_Variable.Get(VAR_NAME_MENU_ID);
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_012D;
            }
            if (int.TryParse(str2, &num2) == null)
            {
                goto Label_011E;
            }
            this.CreateMainWindow(num2 - 1);
        Label_011E:
            FlowNode_Variable.Set(VAR_NAME_MENU_ID, string.Empty);
        Label_012D:
            return;
        }

        private float TextHeight(string text, LText ltext)
        {
            float num;
            int num2;
            num = ((float) ltext.get_fontSize()) * 2f;
            num2 = 0;
            goto Label_0038;
        Label_0015:
            if (text[num2] != 10)
            {
                goto Label_0034;
            }
            num += ((float) ltext.get_fontSize()) + ltext.get_lineSpacing();
        Label_0034:
            num2 += 1;
        Label_0038:
            if (num2 < text.Length)
            {
                goto Label_0015;
            }
            return num;
        }

        public void UpdateHelpList(bool flag, int Idx)
        {
            GameObject obj2;
            ScrollListController controller;
            Transform transform;
            Transform transform2;
            LText text;
            string str;
            obj2 = GameObject.Find("View");
            if ((obj2 == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            controller = obj2.GetComponent<ScrollListController>();
            this.bMiddleHelp = flag;
            this.SelMiddleIdx = Idx;
            controller.UpdateList();
            if (this.MiddleHelp == null)
            {
                goto Label_004F;
            }
            this.MiddleBackButton.get_gameObject().SetActive(1);
        Label_004F:
            transform = base.get_transform().FindChild("window/header");
            if ((transform != null) == null)
            {
                goto Label_00DE;
            }
            text = transform.FindChild("Text").GetComponent<LText>();
            if ((text != null) == null)
            {
                goto Label_00DE;
            }
            str = LocalizedText.Get("help.TITLE");
            if (this.MiddleHelp == null)
            {
                goto Label_00C9;
            }
            str = str + "-" + LocalizedText.Get("help.MENU_CATE_NAME_" + ((int) (Idx + 1)));
        Label_00C9:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00DE;
            }
            text.set_text(str);
        Label_00DE:
            return;
        }

        public bool MiddleHelp
        {
            get
            {
                return this.bMiddleHelp;
            }
        }

        public int SelectMiddleID
        {
            get
            {
                return this.SelMiddleIdx;
            }
        }

        private enum HELP_ID
        {
            ACTION = 0x3d,
            REACTION = 0x3e,
            SUPPORT = 0x3f,
            SHOP = 0x60
        }
    }
}

