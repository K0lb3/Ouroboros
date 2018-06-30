namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class AwardListItem : MonoBehaviour
    {
        [SerializeField]
        private Image BackGround;
        [SerializeField]
        private Text AwardName;
        [SerializeField]
        private GameObject Cursor;
        [SerializeField]
        private GameObject Badge;
        public static readonly string EXTRA_GRADE_IMAGEPATH;
        public static readonly int MAX_GRADE;
        private string mAwardIname;
        private bool mIsSelected;
        private bool mHasItem;
        private bool mIsRefresh;
        private bool mRemove;
        private GameManager gm;

        static AwardListItem()
        {
            EXTRA_GRADE_IMAGEPATH = "AwardImage/ExtraAwards";
            MAX_GRADE = 6;
            return;
        }

        public AwardListItem()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.BackGround != null) == null)
            {
                goto Label_0022;
            }
            this.BackGround.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.AwardName != null) == null)
            {
                goto Label_0059;
            }
            this.AwardName.set_text(LocalizedText.Get("sys.TEXT_NO_AWARD"));
            this.AwardName.get_gameObject().SetActive(0);
        Label_0059:
            if ((this.Cursor != null) == null)
            {
                goto Label_0076;
            }
            this.Cursor.SetActive(0);
        Label_0076:
            if ((this.Badge != null) == null)
            {
                goto Label_0093;
            }
            this.Badge.SetActive(0);
        Label_0093:
            return;
        }

        private AwardParam CreateRemoveAwardData()
        {
            AwardParam param;
            param = new AwardParam();
            param.grade = -1;
            param.iname = string.Empty;
            param.name = LocalizedText.Get("sys.TEXT_REMOVE_AWARD");
            return param;
        }

        private void Refresh()
        {
            AwardParam param;
            ImageArray array;
            if ((this.gm == null) == null)
            {
                goto Label_001C;
            }
            this.gm = MonoSingleton<GameManager>.Instance;
        Label_001C:
            param = null;
            if (this.mRemove != null)
            {
                goto Label_004C;
            }
            param = this.gm.MasterParam.GetAwardParam(this.mAwardIname);
            if (param != null)
            {
                goto Label_0053;
            }
            return;
            goto Label_0053;
        Label_004C:
            param = this.CreateRemoveAwardData();
        Label_0053:
            if ((this.BackGround != null) == null)
            {
                goto Label_00F6;
            }
            array = this.BackGround.GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_00F6;
            }
            if (this.mHasItem == null)
            {
                goto Label_00DE;
            }
            if (MAX_GRADE > param.grade)
            {
                goto Label_00CB;
            }
            array.ImageIndex = 0;
            if (string.IsNullOrEmpty(param.bg) != null)
            {
                goto Label_00BB;
            }
            this.SetExtraAwardImage(param.bg);
        Label_00BB:
            param.name = string.Empty;
            goto Label_00D9;
        Label_00CB:
            array.ImageIndex = param.grade + 1;
        Label_00D9:
            goto Label_00F6;
        Label_00DE:
            array.ImageIndex = (this.mRemove == null) ? 1 : 0;
        Label_00F6:
            if ((this.AwardName != null) == null)
            {
                goto Label_017B;
            }
            if (param.grade != -1)
            {
                goto Label_013A;
            }
            this.AwardName.set_text(param.name);
            this.AwardName.get_gameObject().SetActive(1);
            goto Label_017B;
        Label_013A:
            this.AwardName.set_text((this.mHasItem == null) ? LocalizedText.Get("sys.TEXT_NO_AWARD") : param.name);
            this.AwardName.get_gameObject().SetActive(this.mHasItem);
        Label_017B:
            if (this.mRemove != null)
            {
                goto Label_01CA;
            }
            if ((this.Cursor != null) == null)
            {
                goto Label_01CA;
            }
            if ((this.Badge != null) == null)
            {
                goto Label_01CA;
            }
            this.Cursor.SetActive(this.mIsSelected);
            this.Badge.SetActive(this.mIsSelected);
        Label_01CA:
            return;
        }

        private bool SetExtraAwardImage(string bg)
        {
            ImageArray array;
            SpriteSheet sheet;
            if (string.IsNullOrEmpty(bg) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            array = this.BackGround.GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_004B;
            }
            sheet = AssetManager.Load<SpriteSheet>(EXTRA_GRADE_IMAGEPATH);
            if ((sheet != null) == null)
            {
                goto Label_0049;
            }
            array.set_sprite(sheet.GetSprite(bg));
        Label_0049:
            return 1;
        Label_004B:
            return 0;
        }

        public void SetUp(string iname, bool hasItem, bool selected, bool remove)
        {
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_001D;
            }
            if (remove != null)
            {
                goto Label_001D;
            }
            DebugUtility.LogError("iname is null!");
            return;
        Label_001D:
            this.mAwardIname = iname;
            this.mHasItem = hasItem;
            this.mIsSelected = selected;
            this.mRemove = remove;
            this.mIsRefresh = 1;
            return;
        }

        private void Update()
        {
            if (this.mIsRefresh == null)
            {
                goto Label_0018;
            }
            this.mIsRefresh = 0;
            this.Refresh();
        Label_0018:
            return;
        }
    }
}

