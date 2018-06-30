namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class MaterialPanel : MonoBehaviour, IGameParameter
    {
        public Text Num;
        public Text Req;
        public Text Left;
        public UnityEngine.UI.Slider Slider;
        public string State;
        private ItemParam mItemParam;
        private int mReqNum;
        private int mHasNum;

        public MaterialPanel()
        {
            base..ctor();
            return;
        }

        public void SetMaterial(int reqNum, ItemParam material)
        {
            ItemData data;
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(material);
            DataSource.Bind<ItemParam>(base.get_gameObject(), material);
            this.mReqNum = reqNum;
            this.mHasNum = (data == null) ? 0 : data.Num;
            this.mItemParam = material;
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public unsafe void UpdateValue()
        {
            Animator animator;
            int num;
            if ((this.Num != null) == null)
            {
                goto Label_0027;
            }
            this.Num.set_text(&this.mHasNum.ToString());
        Label_0027:
            if ((this.Req != null) == null)
            {
                goto Label_004E;
            }
            this.Req.set_text(&this.mReqNum.ToString());
        Label_004E:
            if ((this.Left != null) == null)
            {
                goto Label_0085;
            }
            this.Left.set_text(&Mathf.Max(this.mReqNum - this.mHasNum, 0).ToString());
        Label_0085:
            if ((this.Slider != null) == null)
            {
                goto Label_00CA;
            }
            this.Slider.set_maxValue((float) this.mReqNum);
            this.Slider.set_minValue(0f);
            this.Slider.set_value((float) this.mHasNum);
        Label_00CA:
            if (this.mItemParam == null)
            {
                goto Label_010C;
            }
            animator = base.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_010C;
            }
            animator.SetInteger(this.State, (this.mReqNum > this.mHasNum) ? 0 : 1);
        Label_010C:
            return;
        }
    }
}

