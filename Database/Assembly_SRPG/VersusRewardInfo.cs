namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class VersusRewardInfo : MonoBehaviour
    {
        private readonly float SPACE_SCALE;
        public Toggle arrivedTgl;
        public Toggle seasonTgl;
        public GameObject ArrivalView;
        public GameObject SeasonView;
        public ScrollRect Scroll;
        public RectTransform ListParent;

        public VersusRewardInfo()
        {
            this.SPACE_SCALE = 1.1f;
            base..ctor();
            return;
        }

        public unsafe void OnChangeArrival(bool flg)
        {
            ScrollListController controller;
            float num;
            RectTransform transform;
            GameManager manager;
            int num2;
            float num3;
            float num4;
            Vector2 vector;
            Rect rect;
            if (flg == null)
            {
                goto Label_0102;
            }
            if ((this.SeasonView != null) == null)
            {
                goto Label_0102;
            }
            if ((this.ArrivalView != null) == null)
            {
                goto Label_0102;
            }
            this.SeasonView.SetActive(0);
            this.ArrivalView.SetActive(1);
            this.SetScrollRect(this.ArrivalView.GetComponent<RectTransform>());
            controller = this.ArrivalView.GetComponent<ScrollListController>();
            if ((controller != null) == null)
            {
                goto Label_0102;
            }
            controller.Refresh();
            num = 0f;
            num = &this.ArrivalView.GetComponent<RectTransform>().get_sizeDelta().y;
            if ((this.ListParent != null) == null)
            {
                goto Label_00B9;
            }
            num -= &this.ListParent.get_rect().get_height();
        Label_00B9:
            num2 = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - 1, 0);
            num3 = controller.ItemScale * this.SPACE_SCALE;
            num4 = num - (num3 * ((float) num2));
            num4 = Mathf.Min(num4, num);
            controller.MovePos(num4, num4);
        Label_0102:
            return;
        }

        public unsafe void OnChangeSeason(bool flg)
        {
            ScrollListController controller;
            float num;
            RectTransform transform;
            GameManager manager;
            int num2;
            float num3;
            float num4;
            Vector2 vector;
            Rect rect;
            if (flg == null)
            {
                goto Label_0102;
            }
            if ((this.SeasonView != null) == null)
            {
                goto Label_0102;
            }
            if ((this.ArrivalView != null) == null)
            {
                goto Label_0102;
            }
            this.ArrivalView.SetActive(0);
            this.SeasonView.SetActive(1);
            this.SetScrollRect(this.SeasonView.GetComponent<RectTransform>());
            controller = this.SeasonView.GetComponent<ScrollListController>();
            if ((controller != null) == null)
            {
                goto Label_0102;
            }
            controller.Refresh();
            num = 0f;
            num = &this.SeasonView.GetComponent<RectTransform>().get_sizeDelta().y;
            if ((this.ListParent != null) == null)
            {
                goto Label_00B9;
            }
            num -= &this.ListParent.get_rect().get_height();
        Label_00B9:
            num2 = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - 1, 0);
            num3 = controller.ItemScale * this.SPACE_SCALE;
            num4 = num - (num3 * ((float) num2));
            num4 = Mathf.Min(num4, num);
            controller.MovePos(num4, num4);
        Label_0102:
            return;
        }

        private unsafe void SetScrollRect(RectTransform rect)
        {
            Vector2 vector;
            if ((this.Scroll != null) == null)
            {
                goto Label_0043;
            }
            if ((rect != null) == null)
            {
                goto Label_0043;
            }
            vector = rect.get_anchoredPosition();
            &vector.y = 0f;
            rect.set_anchoredPosition(vector);
            this.Scroll.set_content(rect);
        Label_0043:
            return;
        }

        private void Start()
        {
            if ((this.ArrivalView == null) != null)
            {
                goto Label_0022;
            }
            if ((this.SeasonView == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            if ((this.arrivedTgl != null) == null)
            {
                goto Label_0050;
            }
            this.arrivedTgl.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnChangeArrival));
        Label_0050:
            if ((this.seasonTgl != null) == null)
            {
                goto Label_007D;
            }
            this.seasonTgl.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnChangeSeason));
        Label_007D:
            return;
        }
    }
}

