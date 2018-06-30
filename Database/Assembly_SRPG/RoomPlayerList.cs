namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(3, "階層更新完了", 1, 3), Pin(10, "データソース更新", 0, 4), Pin(200, "チーム編成ボタンが押された", 1, 200), AddComponentMenu("Multi/部屋に参加中のプレイヤー一覧"), Pin(0, "表示", 0, 0), Pin(1, "表示更新", 1, 1), Pin(2, "階層更新", 0, 2), Pin(0x65, "情報を見る", 1, 0x65)]
    public class RoomPlayerList : MonoBehaviour, IFlowInterface
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        [Description("大本ゲームオブジェクト")]
        public GameObject Root;
        [Description("編成ランキングボタン")]
        public GameObject RankButton;
        [Description("スキル名")]
        public GameObject SkillObj;
        [Description("スキル詳細用プレハブ")]
        public GameObject Prefab_LeaderSkillDetail;
        public UnityEngine.UI.ScrollRect ScrollRect;
        public List<GameObject> UIItemList;
        [Description("プレイヤーのパーティ情報表示用のゲームオブジェクト")]
        public GameObject PlayerInfo;
        private List<GameObject> PlayerInfoList;
        [CompilerGenerated]
        private static Predicate<GameObjectID> <>f__am$cache9;
        [CompilerGenerated]
        private static Predicate<MyPhoton.MyPlayer> <>f__am$cacheA;

        public RoomPlayerList()
        {
            this.UIItemList = new List<GameObject>();
            this.PlayerInfoList = new List<GameObject>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <RefreshItems>m__3F4(GameObjectID goID)
        {
            return goID.ID.Equals("unit0");
        }

        [CompilerGenerated]
        private static bool <RefreshItems>m__3F5(MyPhoton.MyPlayer member)
        {
            return (member.playerID == 1);
        }

        public void Activated(int pinID)
        {
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            if (pinID != 2)
            {
                goto Label_001E;
            }
            this.RefreshFloorQuest();
            goto Label_004B;
        Label_001E:
            if (pinID != 10)
            {
                goto Label_004B;
            }
            param = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
            DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, param);
        Label_004B:
            return;
        }

        private void Awake()
        {
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            QuestParam param2;
            base.set_enabled(1);
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0034;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0034;
            }
            this.ItemTemplate.SetActive(0);
        Label_0034:
            param = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(param.iname);
            DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, param);
            DataSource.Bind<QuestParam>(this.Root, param2);
            if ((this.RankButton != null) == null)
            {
                goto Label_009D;
            }
            this.RankButton.get_gameObject().SetActive(param2.IsJigen);
        Label_009D:
            GameParameter.UpdateAll(this.Root);
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        }

        private void OnCloseItemDetail(GameObject go)
        {
        }

        public void OnEditTeam()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
            return;
        }

        public void OnEditTeamMultiTower(int index)
        {
            int[] numArray1;
            int[] numArray;
            numArray1 = new int[3];
            numArray1[1] = 1;
            numArray1[2] = 2;
            numArray = numArray1;
            if (index < 0)
            {
                goto Label_0027;
            }
            if (index >= ((int) numArray.Length))
            {
                goto Label_0027;
            }
            GlobalVars.SelectedTowerMultiPartyIndex = numArray[index];
        Label_0027:
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
            return;
        }

        private void OnOpenItemDetail(GameObject go)
        {
            JSON_MyPhotonPlayerParam param;
            param = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(go, null);
            if (param == null)
            {
                goto Label_0028;
            }
            if (param.playerID <= 0)
            {
                goto Label_0028;
            }
            GlobalVars.SelectedMultiPlayerParam = param;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_0028:
            return;
        }

        private void OnSelectItem(GameObject go)
        {
        }

        public void OpenLeaderSkillDetail()
        {
            UnitData data;
            GameObject obj2;
            if ((this.SkillObj != null) == null)
            {
                goto Label_0048;
            }
            if ((this.Prefab_LeaderSkillDetail != null) == null)
            {
                goto Label_0048;
            }
            data = DataSource.FindDataOfClass<UnitData>(this.SkillObj, null);
            if (data == null)
            {
                goto Label_0048;
            }
            DataSource.Bind<UnitData>(Object.Instantiate<GameObject>(this.Prefab_LeaderSkillDetail), data);
        Label_0048:
            return;
        }

        public void Refresh()
        {
            ListExtras extras;
            this.RefreshItems();
            if ((this.ScrollRect != null) == null)
            {
                goto Label_004F;
            }
            extras = this.ScrollRect.GetComponent<ListExtras>();
            if ((extras != null) == null)
            {
                goto Label_003F;
            }
            extras.SetScrollPos(1f);
            goto Label_004F;
        Label_003F:
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        Label_004F:
            return;
        }

        public void RefreshFloorQuest()
        {
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            QuestParam param2;
            param = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(param.iname);
            DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, param);
            DataSource.Bind<QuestParam>(this.Root, param2);
            FlowNode_GameObject.ActivateOutputLinks(this, 3);
            return;
        }

        private unsafe void RefreshItems()
        {
            Transform transform;
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            JSON_MyPhotonPlayerParam param2;
            int num;
            List<MyPhoton.MyPlayer> list;
            int num2;
            GameObject obj2;
            GameObject obj3;
            int num3;
            int num4;
            JSON_MyPhotonPlayerParam param3;
            int num5;
            MyPhoton.MyPlayer player;
            List<MyPhoton.MyPlayer>.Enumerator enumerator;
            JSON_MyPhotonPlayerParam param4;
            GameObject obj4;
            GameObjectID[] tidArray;
            int num6;
            GameObjectID tid;
            GameObjectID tid2;
            Json_Unit unit;
            UnitData data;
            int num7;
            int num8;
            bool flag;
            UnitData data2;
            int num9;
            GameObjectID tid3;
            UnitIcon icon;
            bool flag2;
            UnitData data3;
            int num10;
            GameObjectID tid4;
            ListItemEvents events;
            QuestParam param5;
            bool flag3;
            List<MyPhoton.MyPlayer> list2;
            MyPhoton.MyPlayer player2;
            JSON_MyPhotonPlayerParam param6;
            UnitData data4;
            SRPG_Button button;
            transform = base.get_transform();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            photon = PunMonoSingleton<MyPhoton>.Instance;
            room = photon.GetCurrentRoom();
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            param2 = (param != null) ? param.GetOwner() : null;
            num = (param2 != null) ? param2.playerIndex : 0;
            list = photon.GetRoomPlayerList();
            num2 = this.UIItemList.Count;
            goto Label_00C7;
        Label_0076:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            this.UIItemList.Add(obj2);
            if (GlobalVars.SelectedMultiPlayRoomType != 2)
            {
                goto Label_00C1;
            }
            this.PlayerInfo.SetActive(0);
            obj3 = Object.Instantiate<GameObject>(this.PlayerInfo);
            this.PlayerInfoList.Add(obj3);
        Label_00C1:
            num2 += 1;
        Label_00C7:
            if (num2 < room.maxPlayers)
            {
                goto Label_0076;
            }
            num3 = room.maxPlayers;
            if (GlobalVars.SelectedMultiPlayRoomType != 1)
            {
                goto Label_00ED;
            }
            num3 -= 1;
        Label_00ED:
            num4 = 0;
            goto Label_05DD;
        Label_00F5:
            param3 = null;
            num5 = num4 + 1;
            if (num <= 0)
            {
                goto Label_0123;
            }
            if (num4 != null)
            {
                goto Label_0116;
            }
            num5 = num;
            goto Label_0123;
        Label_0116:
            if (num4 >= num)
            {
                goto Label_0123;
            }
            num5 = num4;
        Label_0123:
            enumerator = list.GetEnumerator();
        Label_012C:
            try
            {
                goto Label_017C;
            Label_0131:
                player = &enumerator.Current;
                if (player.json != null)
                {
                    goto Label_014B;
                }
                goto Label_017C;
            Label_014B:
                param4 = JSON_MyPhotonPlayerParam.Parse(player.json);
                if (param4 != null)
                {
                    goto Label_0165;
                }
                goto Label_017C;
            Label_0165:
                if (param4.playerIndex != num5)
                {
                    goto Label_017C;
                }
                param3 = param4;
                goto Label_0188;
            Label_017C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0131;
                }
            Label_0188:
                goto Label_019A;
            }
            finally
            {
            Label_018D:
                ((List<MyPhoton.MyPlayer>.Enumerator) enumerator).Dispose();
            }
        Label_019A:
            if (param3 != null)
            {
                goto Label_01B1;
            }
            param3 = new JSON_MyPhotonPlayerParam();
            param3.playerIndex = num5;
        Label_01B1:
            obj4 = this.UIItemList[num4];
            obj4.set_hideFlags(0x34);
            DataSource.Bind<JSON_MyPhotonPlayerParam>(obj4, param3);
            DataSource.Bind<JSON_MyPhotonRoomParam>(obj4, param);
            if (GlobalVars.SelectedMultiPlayRoomType != 2)
            {
                goto Label_024C;
            }
            DataSource.Bind<JSON_MyPhotonPlayerParam>(this.PlayerInfoList[num4], param3);
            DataSource.Bind<JSON_MyPhotonRoomParam>(this.PlayerInfoList[num4], param);
            this.PlayerInfoList[num4].get_transform().SetParent(this.PlayerInfo.get_transform().get_parent(), 0);
            this.PlayerInfoList[num4].get_gameObject().SetActive(1);
        Label_024C:
            tidArray = obj4.GetComponentsInChildren<GameObjectID>(1);
            if ((param3 == null) || (tidArray == null))
            {
                goto Label_056D;
            }
            num6 = 0;
            goto Label_0297;
        Label_026C:
            tid = tidArray[num6];
            if (tid.ID != null)
            {
                goto Label_0284;
            }
            goto Label_0291;
        Label_0284:
            DataSource.Bind<UnitData>(tid.get_gameObject(), null);
        Label_0291:
            num6 += 1;
        Label_0297:
            if (num6 < ((int) tidArray.Length))
            {
                goto Label_026C;
            }
            if ((param.draft_type != 1) || (string.IsNullOrEmpty(param3.support_unit) != null))
            {
                goto Label_032A;
            }
            if (<>f__am$cache9 != null)
            {
                goto Label_02D9;
            }
            <>f__am$cache9 = new Predicate<GameObjectID>(RoomPlayerList.<RefreshItems>m__3F4);
        Label_02D9:
            tid2 = Array.Find<GameObjectID>(tidArray, <>f__am$cache9);
            unit = JSONParser.parseJSONObject<Json_Unit>(param3.support_unit);
            if (((tid2 != null) == null) || (unit == null))
            {
                goto Label_056D;
            }
            data = new UnitData();
            data.Deserialize(unit);
            DataSource.Bind<UnitData>(tid2.get_gameObject(), data);
            goto Label_056D;
        Label_032A:
            if (param3.units == null)
            {
                goto Label_056D;
            }
            num7 = 0;
            goto Label_055D;
        Label_033E:
            num8 = param3.units[num7].slotID;
            flag = param3.units[num7].sub == 1;
            data2 = param3.units[num7].unit;
            if (data2 != null)
            {
                goto Label_0380;
            }
            goto Label_0557;
        Label_0380:
            if (GlobalVars.SelectedMultiPlayRoomType != 2)
            {
                goto Label_04F1;
            }
            num9 = 0;
            goto Label_04E1;
        Label_0393:
            tid3 = tidArray[num9];
            if (tid3.ID == null)
            {
                goto Label_04DB;
            }
            if ((tid3.ID.Equals("unit" + ((int) num8)) != null) || (flag != null))
            {
                goto Label_03D4;
            }
            goto Label_04DB;
        Label_03D4:
            if ((flag == null) || (num9 == (((int) tidArray.Length) - 1)))
            {
                goto Label_03ED;
            }
            goto Label_04DB;
        Label_03ED:
            data2.TempFlags |= 2;
            DataSource.Bind<UnitData>(tid3.get_gameObject(), data2);
            icon = tid3.get_gameObject().GetComponent<UnitIcon>();
            if (((icon != null) == null) || (param3.playerIndex <= 0))
            {
                goto Label_04A9;
            }
            DataSource.Bind<PlayerPartyTypes>(tid3.get_gameObject(), 8);
            flag2 = param3.playerIndex == PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex;
            icon.AllowJobChange = flag2;
            if (flag2 == null)
            {
                goto Label_04A9;
            }
            data3 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(data2.UnitParam.iname);
            if (data3 == null)
            {
                goto Label_04A9;
            }
            data3.TempFlags |= 2;
            DataSource.Bind<UnitData>(tid3.get_gameObject(), data3);
            data2 = data3;
        Label_04A9:
            if ((num9 != null) || (this.PlayerInfoList.Count <= num4))
            {
                goto Label_0557;
            }
            DataSource.Bind<UnitData>(this.PlayerInfoList[num4], data2);
            goto Label_04EC;
        Label_04DB:
            num9 += 1;
        Label_04E1:
            if (num9 < ((int) tidArray.Length))
            {
                goto Label_0393;
            }
        Label_04EC:
            goto Label_0557;
        Label_04F1:
            num10 = 0;
            goto Label_054C;
        Label_04F9:
            tid4 = tidArray[num10];
            if (tid4.ID == null)
            {
                goto Label_0546;
            }
            if (tid4.ID.Equals("unit" + ((int) num8)) != null)
            {
                goto Label_0533;
            }
            goto Label_0546;
        Label_0533:
            DataSource.Bind<UnitData>(tid4.get_gameObject(), data2);
            goto Label_0557;
        Label_0546:
            num10 += 1;
        Label_054C:
            if (num10 < ((int) tidArray.Length))
            {
                goto Label_04F9;
            }
        Label_0557:
            num7 += 1;
        Label_055D:
            if (num7 < ((int) param3.units.Length))
            {
                goto Label_033E;
            }
        Label_056D:
            events = obj4.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_05BC;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        Label_05BC:
            obj4.get_transform().SetParent(transform, 0);
            obj4.get_gameObject().SetActive(1);
            num4 += 1;
        Label_05DD:
            if (num4 < num3)
            {
                goto Label_00F5;
            }
            param5 = MonoSingleton<GameManager>.Instance.FindQuest(param.iname);
            DataSource.Bind<QuestParam>(this.Root, param5);
            flag3 = 0;
            if (((this.SkillObj != null) == null) || (param5 == null))
            {
                goto Label_0709;
            }
            if (param5.IsMultiLeaderSkill == null)
            {
                goto Label_06D7;
            }
            list2 = photon.GetRoomPlayerList();
            if (list2 == null)
            {
                goto Label_06D7;
            }
            if (<>f__am$cacheA != null)
            {
                goto Label_0655;
            }
            <>f__am$cacheA = new Predicate<MyPhoton.MyPlayer>(RoomPlayerList.<RefreshItems>m__3F5);
        Label_0655:
            player2 = list2.Find(<>f__am$cacheA);
            if (player2 == null)
            {
                goto Label_06D7;
            }
            param6 = JSON_MyPhotonPlayerParam.Parse(player2.json);
            if (((param6 == null) || (param6.units == null)) || (((int) param6.units.Length) <= 0))
            {
                goto Label_06D7;
            }
            data4 = new UnitData();
            if (data4 == null)
            {
                goto Label_06D7;
            }
            data4.Deserialize(param6.units[0].unitJson);
            DataSource.Bind<UnitData>(this.SkillObj, data4);
            flag3 = (data4.LeaderSkill == null) == 0;
        Label_06D7:
            button = this.SkillObj.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0709;
            }
            button.set_interactable((param5.IsMultiLeaderSkill == null) ? 0 : flag3);
        Label_0709:
            GameParameter.UpdateAll(this.Root);
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        }

        private void Start()
        {
            this.RefreshItems();
            return;
        }

        private void Update()
        {
        }
    }
}

