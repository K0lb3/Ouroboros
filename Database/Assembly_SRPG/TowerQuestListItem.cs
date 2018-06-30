namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class TowerQuestListItem : MonoBehaviour
    {
        private const string FLOOR_NO_PREFIX = "floorNo_";
        [SerializeField]
        private GameObject mBody;
        [SerializeField]
        private GameObject mCleared;
        [SerializeField]
        private GameObject mLocked;
        [SerializeField]
        private Graphic mGraphicRoot;
        [SerializeField]
        private ImageArray[] mBanner;
        [SerializeField]
        private GameObject mCursor;
        [SerializeField]
        private Text mText;
        [SerializeField]
        private Text m_FloorText;
        public CanvasRenderer Source;
        private Color UnknownColor;
        private RectTransform mBodyTransform;
        private Type now_type;
        [CompilerGenerated]
        private RectTransform <rectTransform>k__BackingField;

        public TowerQuestListItem()
        {
            this.UnknownColor = new Color(0.4f, 0.4f, 0.4f, 1f);
            this.now_type = 3;
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.mBody != null) == null)
            {
                goto Label_0022;
            }
            this.mBodyTransform = this.mBody.GetComponent<RectTransform>();
        Label_0022:
            this.rectTransform = base.GetComponent<RectTransform>();
            return;
        }

        public void OnFocus(bool value)
        {
            if ((this.mBodyTransform != null) == null)
            {
                goto Label_005A;
            }
            if (value == null)
            {
                goto Label_003B;
            }
            this.mBodyTransform.set_localScale(new Vector3(1f, 1f, 1f));
            goto Label_005A;
        Label_003B:
            this.mBodyTransform.set_localScale(new Vector3(0.9f, 0.9f, 1f));
        Label_005A:
            return;
        }

        public void SetNowImage()
        {
            this.SetVisible(this.now_type);
            return;
        }

        private void SetVisible(Type type)
        {
            Type type2;
            this.now_type = type;
            GameUtility.SetGameObjectActive(this.mCleared, 0);
            GameUtility.SetGameObjectActive(this.mLocked, 0);
            GameUtility.SetGameObjectActive(this.mCursor, 0);
            GameUtility.SetGameObjectActive(this.m_FloorText, 0);
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0058;

                case 1:
                    goto Label_00A1;

                case 2:
                    goto Label_00EA;

                case 3:
                    goto Label_0133;

                case 4:
                    goto Label_0165;
            }
            goto Label_016A;
        Label_0058:
            this.Source.SetColor(Color.get_gray());
            this.mBanner[0].ImageIndex = 1;
            this.mBanner[1].ImageIndex = 1;
            GameUtility.SetGameObjectActive(this.mLocked, 1);
            GameUtility.SetGameObjectActive(this.m_FloorText, 1);
            goto Label_016A;
        Label_00A1:
            this.Source.SetColor(Color.get_white());
            this.mBanner[0].ImageIndex = 0;
            this.mBanner[1].ImageIndex = 0;
            GameUtility.SetGameObjectActive(this.mCleared, 1);
            GameUtility.SetGameObjectActive(this.m_FloorText, 1);
            goto Label_016A;
        Label_00EA:
            this.Source.SetColor(Color.get_white());
            this.mBanner[0].ImageIndex = 0;
            this.mBanner[1].ImageIndex = 0;
            GameUtility.SetGameObjectActive(this.m_FloorText, 1);
            GameUtility.SetGameObjectActive(this.mCursor, 1);
            goto Label_016A;
        Label_0133:
            this.Source.SetColor(this.UnknownColor);
            this.mBanner[0].ImageIndex = 1;
            this.mBanner[1].ImageIndex = 1;
            goto Label_016A;
        Label_0165:;
        Label_016A:
            return;
        }

        public void UpdateParam(TowerFloorParam param, int floorNo)
        {
            QuestParam param2;
            bool flag;
            if (param != null)
            {
                goto Label_000E;
            }
            this.SetVisible(3);
            return;
        Label_000E:
            param2 = param.Clone(null, 1);
            flag = param2.IsQuestCondition();
            if (flag == null)
            {
                goto Label_003C;
            }
            if (param2.state == 2)
            {
                goto Label_003C;
            }
            this.SetVisible(2);
            goto Label_0061;
        Label_003C:
            if (param2.state != 2)
            {
                goto Label_0054;
            }
            this.SetVisible(1);
            goto Label_0061;
        Label_0054:
            if (flag != null)
            {
                goto Label_0061;
            }
            this.SetVisible(0);
        Label_0061:
            if (param == null)
            {
                goto Label_0099;
            }
            if ((this.mText != null) == null)
            {
                goto Label_0099;
            }
            this.mText.set_text(param.title + " " + param.name);
        Label_0099:
            if ((this.m_FloorText != null) == null)
            {
                goto Label_00CA;
            }
            this.m_FloorText.set_text(((int) param.GetFloorNo()) + "!");
        Label_00CA:
            return;
        }

        public RectTransform rectTransform
        {
            [CompilerGenerated]
            get
            {
                return this.<rectTransform>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<rectTransform>k__BackingField = value;
                return;
            }
        }

        public ImageArray[] Banner
        {
            get
            {
                return this.mBanner;
            }
        }

        private enum Type
        {
            Locked,
            Cleared,
            Current,
            Unknown,
            TypeEnd
        }
    }
}

