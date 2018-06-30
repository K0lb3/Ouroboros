namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class AwardSelectConfirmWindow : MonoBehaviour
    {
        [SerializeField]
        private GameObject AwardImg;
        [SerializeField]
        private Text AwardName;
        [SerializeField]
        private Text ExpText;
        private GameManager gm;
        private ImageArray mImageArray;

        public AwardSelectConfirmWindow()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            ImageArray array;
            if ((this.AwardImg != null) == null)
            {
                goto Label_0030;
            }
            array = this.AwardImg.GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_0030;
            }
            this.mImageArray = array;
        Label_0030:
            return;
        }

        private void Refresh()
        {
            string str;
            AwardParam param;
            str = FlowNode_Variable.Get("CONFIRM_SELECT_AWARD");
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0021;
            }
            DebugUtility.LogError("AwardSelectConfirmWindow:select_iname is Null or Empty");
            return;
        Label_0021:
            param = this.gm.MasterParam.GetAwardParam(str);
            if (param == null)
            {
                goto Label_00E5;
            }
            if ((this.AwardImg != null) == null)
            {
                goto Label_00A1;
            }
            if ((this.mImageArray != null) == null)
            {
                goto Label_00A1;
            }
            if (((int) this.mImageArray.Images.Length) > param.grade)
            {
                goto Label_0090;
            }
            this.SetExtraAwardImage(param.bg);
            param.name = string.Empty;
            goto Label_00A1;
        Label_0090:
            this.mImageArray.ImageIndex = param.grade;
        Label_00A1:
            if ((this.AwardName != null) == null)
            {
                goto Label_00C3;
            }
            this.AwardName.set_text(param.name);
        Label_00C3:
            if ((this.ExpText != null) == null)
            {
                goto Label_00E5;
            }
            this.ExpText.set_text(param.expr);
        Label_00E5:
            return;
        }

        private bool SetExtraAwardImage(string bg)
        {
            SpriteSheet sheet;
            if (string.IsNullOrEmpty(bg) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            sheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
            if ((sheet != null) == null)
            {
                goto Label_0038;
            }
            this.mImageArray.set_sprite(sheet.GetSprite(bg));
            return 1;
        Label_0038:
            return 0;
        }

        private void Start()
        {
            this.gm = MonoSingleton<GameManager>.Instance;
            this.Refresh();
            return;
        }
    }
}

