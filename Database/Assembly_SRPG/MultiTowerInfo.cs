namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "階層選択", 0, 1), Pin(2, "部屋一覧から階層決定", 0, 2), Pin(3, "部屋一覧から閉じた", 0, 3), Pin(10, "部屋一覧から階層選択完了", 1, 10), Pin(0, "マルチタワートップ", 0, 0)]
    public class MultiTowerInfo : MonoBehaviour, IFlowInterface
    {
        private const int PININ_MULTI_TOWER_TOP = 0;
        private const int PININ_SELECT_FLOOR = 1;
        private const int PININ_SELECT_FLOOR_FROM_LIST = 2;
        private const int PININ_CLOSE_LIST = 3;
        private const int PINOUT_SELECTED_FLOOR = 10;
        private readonly int OFFSET;
        public ScrollAutoFit AutoFit;
        public GameObject QuestInfo;
        public Button ScrollUp;
        public Button ScrollDw;
        public SRPG_Button Make;
        public SRPG_Button OK;
        public RectTransform ListRect;
        public ScrollListController ScrollList;
        public RectTransform Cursor;
        private bool IsMultiTowerTop;
        private int max_tower_floor_num;
        public Text RewardText;
        public GameObject ItemRoot;
        public GameObject ArtifactRoot;
        public GameObject CoinRoot;
        public Text QuestAP;
        public Text ChangeQuestAP;
        public GameObject PassButton;
        private bool mIsActivatePinAfterSelectedFloor;
        [CompilerGenerated]
        private static Converter<RectTransform, MultiTowerFloorInfo> <>f__am$cache14;

        public MultiTowerInfo()
        {
            this.OFFSET = 2;
            base..ctor();
            return;
        }

        private void _SetScrollTo(float pos)
        {
            pos = this.Clamp(pos);
            this.AutoFit.SetScrollTo(pos);
            return;
        }

        private unsafe void _SetScrollToQuick(float pos)
        {
            Vector2 vector;
            pos = this.Clamp(pos);
            vector = this.ListRect.get_anchoredPosition();
            &vector.y = pos;
            this.ListRect.set_anchoredPosition(vector);
            return;
        }

        [CompilerGenerated]
        private static MultiTowerFloorInfo <FocusUpdate>m__375(RectTransform item)
        {
            return item.GetComponent<MultiTowerFloorInfo>();
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_001D;

                case 1:
                    goto Label_0029;

                case 2:
                    goto Label_003C;

                case 3:
                    goto Label_0035;
            }
            goto Label_005A;
        Label_001D:
            this.IsMultiTowerTop = 1;
            goto Label_005A;
        Label_0029:
            this.IsMultiTowerTop = 0;
            goto Label_005A;
        Label_0035:
            this.OnScrollStop();
            return;
        Label_003C:
            if (GlobalVars.SelectedMultiTowerFloor <= 0)
            {
                goto Label_0059;
            }
            this.mIsActivatePinAfterSelectedFloor = 1;
            this.Setup(GlobalVars.SelectedMultiTowerFloor);
        Label_0059:
            return;
        Label_005A:
            this.PassButton.SetActive(this.IsMultiTowerTop);
            return;
        }

        public unsafe float Clamp(float pos)
        {
            float num;
            float num2;
            float num3;
            float num4;
            float num5;
            float num6;
            float num7;
            float num8;
            Rect rect;
            Vector2 vector;
            Rect rect2;
            Vector2 vector2;
            Vector2 vector3;
            num = &&this.AutoFit.get_viewport().get_rect().get_size().y;
            num3 = (&&this.AutoFit.rect.get_size().y * 0.5f) - (num * 0.5f);
            num4 = 0f;
            num5 = 0f;
            num6 = 1f;
            num4 = num3;
            num5 = (num3 + num) - &this.ListRect.get_sizeDelta().y;
            num4 *= num6;
            num5 *= num6;
            num7 = Mathf.Min(num4, num5);
            num8 = Mathf.Max(num4, num5);
            return Mathf.Clamp(pos, num7, num8);
        }

        public int ConvertFloor(int floor)
        {
            return ((this.OFFSET + 1) - floor);
        }

        private unsafe void FocusUpdate()
        {
            Rect rect;
            List<MultiTowerFloorInfo> list;
            MultiTowerFloorInfo info;
            List<MultiTowerFloorInfo>.Enumerator enumerator;
            Vector2 vector;
            Vector2 vector2;
            rect = this.Cursor.get_rect();
            &rect.set_center(new Vector2(0f, (this.AutoFit.ItemScale * 3f) - (this.AutoFit.ItemScale * 0.5f)));
            &rect.set_size(this.Cursor.get_sizeDelta());
            if (<>f__am$cache14 != null)
            {
                goto Label_0070;
            }
            <>f__am$cache14 = new Converter<RectTransform, MultiTowerFloorInfo>(MultiTowerInfo.<FocusUpdate>m__375);
        Label_0070:
            enumerator = this.ItemList.ConvertAll<MultiTowerFloorInfo>(<>f__am$cache14).GetEnumerator();
        Label_0082:
            try
            {
                goto Label_00DE;
            Label_0087:
                info = &enumerator.Current;
                if (info.get_gameObject().get_activeInHierarchy() == null)
                {
                    goto Label_00DE;
                }
                vector = info.rectTransform.get_anchoredPosition();
                &vector.y = &this.ListRect.get_anchoredPosition().y + &vector.y;
                info.OnFocus(&rect.Contains(vector));
            Label_00DE:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0087;
                }
                goto Label_00FB;
            }
            finally
            {
            Label_00EF:
                ((List<MultiTowerFloorInfo>.Enumerator) enumerator).Dispose();
            }
        Label_00FB:
            return;
        }

        public int GetCanCharengeFloor()
        {
            MyPhoton photon;
            List<MyPhoton.MyPlayer> list;
            int num;
            int num2;
            MyPhoton.MyPlayer player;
            JSON_MyPhotonPlayerParam param;
            list = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
            num = 0x7fffffff;
            num2 = 0;
            goto Label_004A;
        Label_001A:
            player = list[num2];
            param = JSON_MyPhotonPlayerParam.Parse(player.json);
            if (num <= param.mtChallengeFloor)
            {
                goto Label_0046;
            }
            num = param.mtChallengeFloor;
        Label_0046:
            num2 += 1;
        Label_004A:
            if (num2 < list.Count)
            {
                goto Label_001A;
            }
            return num;
        }

        public void Init()
        {
            GameManager manager;
            int num;
            List<MultiTowerFloorParam> list;
            manager = MonoSingleton<GameManager>.Instance;
            num = (this.IsMultiTowerTop == null) ? GlobalVars.SelectedMultiTowerFloor : manager.GetMTChallengeFloor();
            this.Setup(num);
            this.ScrollToFloorQuick(this.ConvertFloor(num));
            list = manager.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
            this.max_tower_floor_num = list.Count;
            if ((this.AutoFit != null) == null)
            {
                goto Label_0097;
            }
            this.AutoFit.OnScrollStop.AddListener(new UnityAction(this, this.OnScrollStop));
            this.AutoFit.OnScrollBegin.AddListener(new UnityAction(this, this.OnScrollStart));
        Label_0097:
            return;
        }

        public void OnCurrentScroll()
        {
            int num;
            num = MonoSingleton<GameManager>.Instance.GetMTChallengeFloor();
            this.ScrollToFloor(this.ConvertFloor(num));
            return;
        }

        public void OnScrollDw(int val)
        {
            int num;
            num = this.AutoFit.GetCurrent() + val;
            this.ScrollToFloor(num);
            return;
        }

        public void OnScrollStart()
        {
            this.SetButtonIntractable(0);
            return;
        }

        private unsafe void OnScrollStop()
        {
            float num;
            float num2;
            int num3;
            Vector2 vector;
            if ((this.AutoFit == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = this.AutoFit.ItemScale;
            num2 = num * ((float) this.OFFSET);
            num3 = Mathf.RoundToInt((&this.AutoFit.get_content().get_anchoredPosition().y - num2) / num) + 1;
            num3 = Mathf.Abs(num3 - this.OFFSET);
            this.Setup(num3);
            return;
        }

        public void OnScrollUp(int val)
        {
            int num;
            num = this.AutoFit.GetCurrent() - val;
            this.ScrollToFloor(num);
            return;
        }

        public void OnTapFloor(int floor)
        {
            this.ScrollToFloor(this.ConvertFloor(floor));
            return;
        }

        private void ScrollToFloor(int index)
        {
            this.SetButtonIntractable(0);
            this._SetScrollTo((((float) index) * this.AutoFit.ItemScale) + this.AutoFit.Offset);
            return;
        }

        private void ScrollToFloorQuick(int index)
        {
            this._SetScrollToQuick((((float) index) * this.AutoFit.ItemScale) + this.AutoFit.Offset);
            return;
        }

        public void SetButtonIntractable(bool intaractable)
        {
            if ((this.Make != null) == null)
            {
                goto Label_001D;
            }
            this.Make.set_interactable(intaractable);
        Label_001D:
            if ((this.OK != null) == null)
            {
                goto Label_003A;
            }
            this.OK.set_interactable(intaractable);
        Label_003A:
            return;
        }

        private unsafe void Setup(int idx)
        {
            GameManager manager;
            MultiTowerFloorParam param;
            int num;
            int num2;
            int num3;
            bool flag;
            MultiTowerFloorParam param2;
            QuestParam param3;
            MultiTowerQuestInfo info;
            int num4;
            if (this.mIsActivatePinAfterSelectedFloor == null)
            {
                goto Label_001A;
            }
            this.mIsActivatePinAfterSelectedFloor = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_001A:
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, idx);
            if ((param == null) || ((this.QuestInfo != null) == null))
            {
                goto Label_01A5;
            }
            num = manager.GetMTChallengeFloor();
            num2 = manager.GetMTClearedMaxFloor();
            num3 = 0x7fffffff;
            if (this.IsMultiTowerTop != null)
            {
                goto Label_006C;
            }
            num3 = this.GetCanCharengeFloor();
        Label_006C:;
        Label_0084:
            flag = ((param.floor > num2) && (param.floor != num)) ? 0 : ((param.floor > num3) == 0);
            this.SetButtonIntractable(flag);
            param2 = DataSource.FindDataOfClass<MultiTowerFloorParam>(this.QuestInfo, null);
            if (param2 == null)
            {
                goto Label_00C6;
            }
            if (param.floor != param2.floor)
            {
                goto Label_00C6;
            }
            return;
        Label_00C6:
            DebugUtility.Log("設定" + param.name);
            param3 = param.GetQuestParam();
            DataSource.Bind<MultiTowerFloorParam>(this.QuestInfo, param);
            DataSource.Bind<QuestParam>(this.QuestInfo, param3);
            GameParameter.UpdateAll(this.QuestInfo);
            info = this.QuestInfo.GetComponent<MultiTowerQuestInfo>();
            if ((info != null) == null)
            {
                goto Label_0128;
            }
            info.Refresh();
        Label_0128:
            GlobalVars.SelectedMultiTowerID = param.tower_id;
            GlobalVars.SelectedQuestID = param3.iname;
            GlobalVars.SelectedMultiTowerFloor = param.floor;
            num4 = param3.RequiredApWithPlayerLv(manager.Player.Lv, 1);
            if ((this.QuestAP != null) == null)
            {
                goto Label_0182;
            }
            this.QuestAP.set_text(&num4.ToString());
        Label_0182:
            if ((this.ChangeQuestAP != null) == null)
            {
                goto Label_01A5;
            }
            this.ChangeQuestAP.set_text(&num4.ToString());
        Label_01A5:
            return;
        }

        private void Start()
        {
        }

        private unsafe void Update()
        {
            float num;
            float num2;
            int num3;
            Vector2 vector;
            this.FocusUpdate();
            num = this.AutoFit.ItemScale;
            num2 = num * ((float) this.OFFSET);
            num3 = Mathf.RoundToInt((&this.AutoFit.get_content().get_anchoredPosition().y - num2) / num) + 1;
            num3 = Mathf.Abs(num3 - this.OFFSET);
            if ((this.ScrollUp != null) == null)
            {
                goto Label_0073;
            }
            this.ScrollUp.set_interactable(num3 < this.max_tower_floor_num);
        Label_0073:
            if ((this.ScrollDw != null) == null)
            {
                goto Label_0093;
            }
            this.ScrollDw.set_interactable(num3 > 1);
        Label_0093:
            return;
        }

        public List<RectTransform> ItemList
        {
            get
            {
                return this.ScrollList.m_ItemList;
            }
        }

        public bool MultiTowerTop
        {
            get
            {
                return this.IsMultiTowerTop;
            }
        }
    }
}

