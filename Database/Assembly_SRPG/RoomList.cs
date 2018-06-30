namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(2, "現状表示", 0, 2), Pin(3, "選択可能ものを表示", 0, 3), Pin(0x65, "選択された", 1, 0x65), Pin(0xc9, "選択可能な部屋がある", 1, 0xc9), Pin(200, "選択可能な部屋がない", 1, 200), Pin(0, "ノーマル表示", 0, 0), AddComponentMenu("Multi/参加募集中の部屋一覧"), Pin(1, "イベント表示", 0, 1)]
    public class RoomList : MonoBehaviour, IFlowInterface
    {
        private const int PININ_DRAW_NORMAL = 0;
        private const int PININ_DRAW_EVENT = 1;
        private const int PININ_DRAW_CURRENT = 2;
        private const int PININ_DRAW_SELECTABLE = 3;
        private const int PINOUT_SELECTED = 0x65;
        private const int PINOUT_ROOM_EXISTS = 200;
        private const int PINOUT_ROOM_NOT_EXISTS = 0xc9;
        private readonly Color EnableColor;
        private readonly Color DisableColor;
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        [Description("詳細画面として使用するゲームオブジェクト")]
        public GameObject DetailTemplate;
        private GameObject mDetailInfo;
        public UnityEngine.UI.ScrollRect ScrollRect;
        private List<GameObject> mRoomList;

        public RoomList()
        {
            this.EnableColor = new Color(1f, 1f, 1f);
            this.DisableColor = new Color(0.3921569f, 0.3921569f, 0.3921569f);
            this.mRoomList = new List<GameObject>();
            base..ctor();
            return;
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
                    goto Label_002A;

                case 2:
                    goto Label_0037;

                case 3:
                    goto Label_0048;
            }
            goto Label_0055;
        Label_001D:
            this.Refresh(0, 0);
            goto Label_0055;
        Label_002A:
            this.Refresh(1, 0);
            goto Label_0055;
        Label_0037:
            this.Refresh(GlobalVars.SelectedMultiPlayQuestIsEvent, 0);
            goto Label_0055;
        Label_0048:
            this.Refresh(0, 1);
        Label_0055:
            return;
        }

        private void Awake()
        {
            GlobalVars.SelectedMultiPlayQuestIsEvent = 0;
            base.set_enabled(1);
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_003A;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_003A;
            }
            this.ItemTemplate.SetActive(0);
        Label_003A:
            if ((this.DetailTemplate != null) == null)
            {
                goto Label_0067;
            }
            if (this.DetailTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0067;
            }
            this.DetailTemplate.SetActive(0);
        Label_0067:
            return;
        }

        private bool IsChooseRoom(MultiPlayAPIRoom room)
        {
            GameManager manager;
            PlayerData data;
            PartyData data2;
            QuestParam param;
            bool flag;
            int num;
            long num2;
            UnitData data3;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            data2 = data.Partys[2];
            param = manager.FindQuest(room.quest.iname);
            flag = 0;
            if (room.limit != null)
            {
                goto Label_003C;
            }
            return 1;
        Label_003C:
            if (data2 == null)
            {
                goto Label_00B0;
            }
            flag = 1;
            num = 0;
            goto Label_009E;
        Label_004D:
            num2 = data2.GetUnitUniqueID(num);
            if (num2 > 0L)
            {
                goto Label_0068;
            }
            flag = 0;
            goto Label_00B0;
        Label_0068:
            data3 = data.FindUnitDataByUniqueID(num2);
            if (data3 != null)
            {
                goto Label_0081;
            }
            flag = 0;
            goto Label_00B0;
        Label_0081:
            flag &= (data3.CalcLevel() < room.unitlv) == 0;
            num += 1;
        Label_009E:
            if (num < param.unitNum)
            {
                goto Label_004D;
            }
        Label_00B0:
            return flag;
        }

        private void OnCloseItemDetail(GameObject go)
        {
            if ((this.mDetailInfo != null) == null)
            {
                goto Label_0028;
            }
            Object.DestroyImmediate(this.mDetailInfo.get_gameObject());
            this.mDetailInfo = null;
        Label_0028:
            return;
        }

        private void OnOpenItemDetail(GameObject go)
        {
            QuestParam param;
            param = DataSource.FindDataOfClass<QuestParam>(go, null);
            if ((this.mDetailInfo == null) == null)
            {
                goto Label_0048;
            }
            if (param == null)
            {
                goto Label_0048;
            }
            this.mDetailInfo = Object.Instantiate<GameObject>(this.DetailTemplate);
            DataSource.Bind<QuestParam>(this.mDetailInfo, param);
            this.mDetailInfo.SetActive(1);
        Label_0048:
            return;
        }

        private void OnSelectItem(GameObject go)
        {
            object[] objArray1;
            MultiPlayAPIRoom room;
            Transform transform;
            int num;
            Transform transform2;
            room = DataSource.FindDataOfClass<MultiPlayAPIRoom>(go, null);
            if (room == null)
            {
                goto Label_00E5;
            }
            GlobalVars.SelectedMultiPlayRoomID = room.roomid;
            GlobalVars.SelectedMultiPlayRoomPassCodeHash = room.pwd_hash;
            GlobalVars.SelectedMultiTowerFloor = room.floor;
            objArray1 = new object[] { "Select RoomID:", (int) GlobalVars.SelectedMultiPlayRoomID, " PassCodeHash:", GlobalVars.SelectedMultiPlayRoomPassCodeHash };
            DebugUtility.Log(string.Concat(objArray1));
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            transform = go.get_transform().Find("cursor");
            if ((transform != null) == null)
            {
                goto Label_00E5;
            }
            num = 0;
            goto Label_00C8;
        Label_0090:
            transform2 = this.mRoomList[num].get_transform().Find("cursor");
            if ((transform2 != null) == null)
            {
                goto Label_00C4;
            }
            transform2.get_gameObject().SetActive(0);
        Label_00C4:
            num += 1;
        Label_00C8:
            if (num < this.mRoomList.Count)
            {
                goto Label_0090;
            }
            transform.get_gameObject().SetActive(1);
        Label_00E5:
            return;
        }

        public void Refresh(bool isEvent, bool isSelect)
        {
            ListExtras extras;
            GlobalVars.SelectedMultiPlayQuestIsEvent = isEvent;
            this.RefreshItems(isSelect);
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0056;
            }
            extras = this.ScrollRect.GetComponent<ListExtras>();
            if ((extras != null) == null)
            {
                goto Label_0046;
            }
            extras.SetScrollPos(1f);
            goto Label_0056;
        Label_0046:
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        Label_0056:
            return;
        }

        private void RefreshItems(bool isSelect)
        {
            object[] objArray1;
            Transform transform;
            bool flag;
            int num;
            GameManager manager;
            int num2;
            MultiPlayAPIRoom[] roomArray;
            MultiTowerFilterMode mode;
            string str;
            int num3;
            MultiPlayAPIRoom room;
            QuestParam param;
            QuestParam param2;
            GameObject obj2;
            Json_Unit[] unitArray;
            UnitData data;
            ListItemEvents events;
            transform = base.get_transform();
            flag = 0;
            num = 0;
            manager = MonoSingleton<GameManager>.Instance;
            num2 = 0;
            goto Label_0031;
        Label_0019:
            Object.DestroyImmediate(this.mRoomList[num2]);
            num2 += 1;
        Label_0031:
            if (num2 < this.mRoomList.Count)
            {
                goto Label_0019;
            }
            this.mRoomList.Clear();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0060;
            }
            return;
        Label_0060:
            roomArray = (FlowNode_MultiPlayAPI.RoomList != null) ? FlowNode_MultiPlayAPI.RoomList.rooms : null;
            if (roomArray != null)
            {
                goto Label_008F;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
            return;
        Label_008F:
            if (((int) roomArray.Length) != null)
            {
                goto Label_00A4;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
            return;
        Label_00A4:
            mode = 0;
            str = FlowNode_Variable.Get("MT_ROOM_FILTER_MODE");
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_00C8;
            }
            mode = int.Parse(str);
        Label_00C8:
            num3 = 0;
            goto Label_033D;
        Label_00D0:
            room = roomArray[num3];
            if ((room == null) || (room.quest == null))
            {
                goto Label_0337;
            }
            if (string.IsNullOrEmpty(room.quest.iname) == null)
            {
                goto Label_0105;
            }
            goto Label_0337;
        Label_0105:
            param = MonoSingleton<GameManager>.Instance.FindQuest(room.quest.iname);
            if (param != null)
            {
                goto Label_0129;
            }
            goto Label_0337;
        Label_0129:
            if (param.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent)
            {
                goto Label_013F;
            }
            goto Label_0337;
        Label_013F:
            if (mode != 1)
            {
                goto Label_015D;
            }
            if (room.is_clear != null)
            {
                goto Label_0176;
            }
            goto Label_0337;
            goto Label_0176;
        Label_015D:
            if ((mode != 2) || (room.is_clear == null))
            {
                goto Label_0176;
            }
            goto Label_0337;
        Label_0176:
            flag = this.IsChooseRoom(room);
            if ((flag != null) || (isSelect == null))
            {
                goto Label_0190;
            }
            goto Label_0337;
        Label_0190:
            if ((room.clear == null) || (manager.FindQuest(room.quest.iname).state == 2))
            {
                goto Label_01C2;
            }
            goto Label_0337;
        Label_01C2:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.set_hideFlags(0x34);
            unitArray = (room.owner != null) ? room.owner.units : null;
            if ((unitArray == null) || (((int) unitArray.Length) <= 0))
            {
                goto Label_0224;
            }
            data = new UnitData();
            data.Deserialize(unitArray[0]);
            DataSource.Bind<UnitData>(obj2, data);
        Label_0224:
            DataSource.Bind<MultiPlayAPIRoom>(obj2, room);
            DataSource.Bind<QuestParam>(obj2, param);
            objArray1 = new object[] { "found roomid:", (int) room.roomid, " room:", room.comment, " iname:", param.iname, " playerNum:", (OShort) param.playerNum, " unitNum:", (OShort) param.unitNum };
            DebugUtility.Log(string.Concat(objArray1));
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0300;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        Label_0300:
            obj2.get_transform().SetParent(transform, 0);
            obj2.get_gameObject().SetActive(1);
            this.mRoomList.Add(obj2);
            this.SetSelectablParam(obj2, room, flag);
            num += 1;
        Label_0337:
            num3 += 1;
        Label_033D:
            if (num3 < ((int) roomArray.Length))
            {
                goto Label_00D0;
            }
            GameParameter.UpdateAll(transform.get_gameObject());
            FlowNode_GameObject.ActivateOutputLinks(this, (num != null) ? 0xc9 : 200);
            return;
        }

        private void SetSelectablParam(GameObject obj, MultiPlayAPIRoom room, bool isChoose)
        {
            SRPG_Button button;
            Transform transform;
            Transform transform2;
            Image image;
            Image image2;
            button = obj.GetComponent<SRPG_Button>();
            transform = obj.get_transform().FindChild("fil");
            transform2 = obj.get_transform().FindChild("basewindow");
            if (isChoose == null)
            {
                goto Label_008A;
            }
            if ((button != null) == null)
            {
                goto Label_0042;
            }
            button.set_interactable(1);
        Label_0042:
            if ((transform != null) == null)
            {
                goto Label_005A;
            }
            transform.get_gameObject().SetActive(0);
        Label_005A:
            if ((transform2 != null) == null)
            {
                goto Label_00E3;
            }
            image = transform2.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_00E3;
            }
            image.set_color(this.EnableColor);
            goto Label_00E3;
        Label_008A:
            if ((button != null) == null)
            {
                goto Label_009D;
            }
            button.set_interactable(0);
        Label_009D:
            if ((transform != null) == null)
            {
                goto Label_00B5;
            }
            transform.get_gameObject().SetActive(1);
        Label_00B5:
            if ((transform2 != null) == null)
            {
                goto Label_00E3;
            }
            image2 = transform2.GetComponent<Image>();
            if ((image2 != null) == null)
            {
                goto Label_00E3;
            }
            image2.set_color(this.DisableColor);
        Label_00E3:
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        private enum MultiTowerFilterMode
        {
            Default,
            Cleared,
            NotClear
        }
    }
}

