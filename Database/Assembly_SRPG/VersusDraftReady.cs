namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(2, "Finish Place", 1, 2), Pin(1, "Finish Place", 0, 1), Pin(3, "Finish Scene", 1, 3)]
    public class VersusDraftReady : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_PIN_FINISH_PLACE = 1;
        private const int OUTPUT_PIN_FINISH_PLACE = 2;
        private const int OUTPUT_PIN_FINISH_SCENE = 3;
        [SerializeField]
        private Text mTimerText;
        private StateMachine<VersusDraftReady> mStateMachine;
        private float mPlaceSec;
        private List<VersusReadyMessageData> mMessageDataList;

        public VersusDraftReady()
        {
            this.mMessageDataList = new List<VersusReadyMessageData>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_000E;
            }
            goto Label_0019;
        Label_000E:
            this.GotoState<State_UpdatePlayer>();
        Label_0019:
            return;
        }

        public void GotoState<StateType>() where StateType: State<VersusDraftReady>, new()
        {
            if (this.mStateMachine == null)
            {
                goto Label_0016;
            }
            this.mStateMachine.GotoState<StateType>();
        Label_0016:
            return;
        }

        private void Start()
        {
            this.mPlaceSec = (float) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DraftPlaceSeconds;
            this.mStateMachine = new StateMachine<VersusDraftReady>(this);
            this.GotoState<State_UnitPlacing>();
            return;
        }

        private void Update()
        {
            this.UpdatePhotonMessage();
            if (this.mStateMachine == null)
            {
                goto Label_002B;
            }
            if ((this.mStateMachine.StateName == "NULL") == null)
            {
                goto Label_002C;
            }
        Label_002B:
            return;
        Label_002C:
            this.mStateMachine.Update();
            return;
        }

        private unsafe void UpdatePhotonMessage()
        {
            MyPhoton photon;
            List<MyPhoton.MyEvent> list;
            MyPhoton.MyEvent event2;
            VersusReadyMessageData data;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            list = photon.GetEvents();
            if (list == null)
            {
                goto Label_0090;
            }
            goto Label_0084;
        Label_0025:
            event2 = list[0];
            list.RemoveAt(0);
            if (event2.code == null)
            {
                goto Label_0044;
            }
            goto Label_0084;
        Label_0044:
            if (event2.binary != null)
            {
                goto Label_0054;
            }
            goto Label_0084;
        Label_0054:
            data = null;
            if (GameUtility.Binary2Object<VersusReadyMessageData>(&data, event2.binary) != null)
            {
                goto Label_006D;
            }
            goto Label_0084;
        Label_006D:
            if (data != null)
            {
                goto Label_0078;
            }
            goto Label_0084;
        Label_0078:
            this.mMessageDataList.Add(data);
        Label_0084:
            if (list.Count > 0)
            {
                goto Label_0025;
            }
        Label_0090:
            return;
        }

        private Text TimerText
        {
            get
            {
                return this.mTimerText;
            }
        }

        private class State_RoomUpdate : State<VersusDraftReady>
        {
            private const int PARTY_SLOT_COUNT = 3;

            public State_RoomUpdate()
            {
                base..ctor();
                return;
            }

            public override void Begin(VersusDraftReady self)
            {
                MyPhoton photon;
                List<JSON_MyPhotonPlayerParam> list;
                List<MyPhoton.MyPlayer> list2;
                int num;
                int num2;
                JSON_MyPhotonPlayerParam param;
                int num3;
                int num4;
                int num5;
                List<JSON_MyPhotonPlayerParam.UnitDataElem> list3;
                int num6;
                UnitData data;
                JSON_MyPhotonPlayerParam.UnitDataElem elem;
                FlowNode_StartMultiPlay.PlayerList list4;
                <Begin>c__AnonStorey3E7 storeye;
                <Begin>c__AnonStorey3E8 storeye2;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                list = photon.GetMyPlayersStarted();
                list2 = photon.GetRoomPlayerList();
                num = 0;
                goto Label_006A;
            Label_001B:
                storeye = new <Begin>c__AnonStorey3E7();
                storeye.param = JSON_MyPhotonPlayerParam.Parse(list2[num].json);
                num2 = list.FindIndex(new Predicate<JSON_MyPhotonPlayerParam>(storeye.<>m__496));
                if (num2 <= -1)
                {
                    goto Label_0066;
                }
                list[num2] = storeye.param;
            Label_0066:
                num += 1;
            Label_006A:
                if (num < list2.Count)
                {
                    goto Label_001B;
                }
                if (list2.Count >= 2)
                {
                    goto Label_03E2;
                }
                storeye2 = new <Begin>c__AnonStorey3E8();
                storeye2.player = photon.GetMyPlayer();
                param = list.Find(new Predicate<JSON_MyPhotonPlayerParam>(storeye2.<>m__497));
                num3 = 0;
                num4 = 0;
                num5 = 0;
                list3 = new List<JSON_MyPhotonPlayerParam.UnitDataElem>();
                num6 = 0;
                goto Label_0393;
            Label_00C3:
                data = VersusDraftList.VersusDraftUnitDataListEnemy[num6];
                if (data != null)
                {
                    goto Label_00DD;
                }
                goto Label_038D;
            Label_00DD:
                elem = new JSON_MyPhotonPlayerParam.UnitDataElem();
                elem.slotID = num3;
                elem.place = num6;
                elem.unit = data;
                list3.Add(elem);
                num4 += data.Status.param.atk;
                num4 += data.Status.param.mag;
                num5 += (int) (((float) data.Status.param.hp) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.HP);
                num5 += (int) (((float) data.Status.param.atk) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Attack);
                num5 += (int) (((float) data.Status.param.def) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Defense);
                num5 += (int) (((float) data.Status.param.mag) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagAttack);
                num5 += (int) (((float) data.Status.param.mnd) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagDefense);
                num5 += (int) (((float) data.Status.param.dex) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Dex);
                num5 += (int) (((float) data.Status.param.spd) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Speed);
                num5 += (int) (((float) data.Status.param.cri) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Critical);
                num5 += (int) (((float) data.Status.param.luk) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Luck);
                num5 += (int) (((float) data.GetCombination()) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Combo);
                num5 += (int) (((float) data.Status.param.mov) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Move);
                num5 += (int) (((float) data.Status.param.jmp) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Jump);
                num3 += 1;
            Label_038D:
                num6 += 1;
            Label_0393:
                if (num6 >= VersusDraftList.VersusDraftUnitDataListEnemy.Count)
                {
                    goto Label_03AC;
                }
                if (num6 < 3)
                {
                    goto Label_00C3;
                }
            Label_03AC:
                param.units = list3.ToArray();
                param.totalAtk = num4;
                param.totalStatus = Mathf.FloorToInt((float) (num5 / list3.Count));
                param.draft_id = -1;
            Label_03E2:
                if (photon.IsOldestPlayer() == null)
                {
                    goto Label_0414;
                }
                list4 = new FlowNode_StartMultiPlay.PlayerList();
                list4.players = list.ToArray();
                photon.UpdateRoomParam("started", list4.Serialize());
            Label_0414:
                FlowNode_GameObject.ActivateOutputLinks(self, 3);
                return;
            }

            [CompilerGenerated]
            private sealed class <Begin>c__AnonStorey3E7
            {
                internal JSON_MyPhotonPlayerParam param;

                public <Begin>c__AnonStorey3E7()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__496(JSON_MyPhotonPlayerParam sp)
                {
                    return (sp.playerID == this.param.playerID);
                }
            }

            [CompilerGenerated]
            private sealed class <Begin>c__AnonStorey3E8
            {
                internal MyPhoton.MyPlayer player;

                public <Begin>c__AnonStorey3E8()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__497(JSON_MyPhotonPlayerParam sp)
                {
                    return ((sp.playerID == this.player.playerID) == 0);
                }
            }
        }

        private class State_UnitPlacing : State<VersusDraftReady>
        {
            private bool mEnable;
            private float mTimer;
            private MultiPlayVersusReady mMPVR;

            public State_UnitPlacing()
            {
                base..ctor();
                return;
            }

            public override void Begin(VersusDraftReady self)
            {
                this.mTimer = 0f;
                this.mEnable = 1;
                this.mMPVR = self.GetComponent<MultiPlayVersusReady>();
                return;
            }

            public override unsafe void Update(VersusDraftReady self)
            {
                MultiPlayVersusReady ready;
                int num;
                if (this.mMPVR.IsReady != null)
                {
                    goto Label_0011;
                }
                return;
            Label_0011:
                if (this.mEnable != null)
                {
                    goto Label_001D;
                }
                return;
            Label_001D:
                this.mTimer += Time.get_unscaledDeltaTime();
                num = (int) (self.mPlaceSec - this.mTimer);
                self.TimerText.set_text(&num.ToString());
                if (this.mTimer < self.mPlaceSec)
                {
                    goto Label_00AD;
                }
                this.mEnable = 0;
                ready = self.GetComponent<MultiPlayVersusReady>();
                if ((ready == null) != null)
                {
                    goto Label_009C;
                }
                if ((ready.GoButton == null) != null)
                {
                    goto Label_009C;
                }
                if (ready.GoButton.get_onClick() != null)
                {
                    goto Label_009D;
                }
            Label_009C:
                return;
            Label_009D:
                ready.GoButton.get_onClick().Invoke();
            Label_00AD:
                return;
            }
        }

        private class State_UpdatePlayer : State<VersusDraftReady>
        {
            public State_UpdatePlayer()
            {
                base..ctor();
                return;
            }

            public override void Begin(VersusDraftReady self)
            {
                MyPhoton photon;
                MyPhoton.MyPlayer player;
                int num;
                int num2;
                GameManager manager;
                JSON_MyPhotonPlayerParam param;
                PlayerData data;
                int num3;
                int num4;
                int num5;
                List<JSON_MyPhotonPlayerParam.UnitDataElem> list;
                int num6;
                UnitData data2;
                JSON_MyPhotonPlayerParam.UnitDataElem elem;
                VersusDraftReady.VersusReadyMessageData data3;
                byte[] buffer;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                player = photon.GetMyPlayer();
                num = (player != null) ? player.playerID : 0;
                num2 = photon.MyPlayerIndex;
                manager = MonoSingleton<GameManager>.Instance;
                param = new JSON_MyPhotonPlayerParam();
                data = manager.Player;
                param.playerID = num;
                param.playerIndex = num2;
                param.playerName = data.Name;
                param.playerLevel = data.Lv;
                param.FUID = data.FUID;
                param.UID = MonoSingleton<GameManager>.Instance.DeviceId;
                param.award = data.SelectedAward;
                num3 = 0;
                num4 = 0;
                num5 = 0;
                list = new List<JSON_MyPhotonPlayerParam.UnitDataElem>();
                num6 = 0;
                goto Label_03A8;
            Label_00AF:
                data2 = VersusDraftList.VersusDraftPartyUnits[num6];
                if (data2 != null)
                {
                    goto Label_00C9;
                }
                goto Label_03A2;
            Label_00C9:
                elem = new JSON_MyPhotonPlayerParam.UnitDataElem();
                elem.slotID = num3;
                if (VersusDraftList.VersusDraftPartyPlaces.Count <= num6)
                {
                    goto Label_0102;
                }
                elem.place = VersusDraftList.VersusDraftPartyPlaces[num6];
                goto Label_010B;
            Label_0102:
                elem.place = num6;
            Label_010B:
                elem.unit = data2;
                list.Add(elem);
                num4 += data2.Status.param.atk;
                num4 += data2.Status.param.mag;
                num5 += (int) (((float) data2.Status.param.hp) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.HP);
                num5 += (int) (((float) data2.Status.param.atk) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Attack);
                num5 += (int) (((float) data2.Status.param.def) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Defense);
                num5 += (int) (((float) data2.Status.param.mag) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagAttack);
                num5 += (int) (((float) data2.Status.param.mnd) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagDefense);
                num5 += (int) (((float) data2.Status.param.dex) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Dex);
                num5 += (int) (((float) data2.Status.param.spd) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Speed);
                num5 += (int) (((float) data2.Status.param.cri) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Critical);
                num5 += (int) (((float) data2.Status.param.luk) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Luck);
                num5 += (int) (((float) data2.GetCombination()) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Combo);
                num5 += (int) (((float) data2.Status.param.mov) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Move);
                num5 += (int) (((float) data2.Status.param.jmp) * MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Jump);
                num3 += 1;
            Label_03A2:
                num6 += 1;
            Label_03A8:
                if (num6 < VersusDraftList.VersusDraftPartyUnits.Count)
                {
                    goto Label_00AF;
                }
                param.units = list.ToArray();
                param.totalAtk = num4;
                param.totalStatus = Mathf.FloorToInt((float) (num5 / list.Count));
                param.rankpoint = data.VERSUS_POINT;
                param.draft_id = VersusDraftList.DraftID;
                photon.SetMyPlayerParam(param.Serialize());
                data3 = new VersusDraftReady.VersusReadyMessageData();
                data3.h = 1;
                data3.pidx = num2;
                data3.pid = num;
                buffer = GameUtility.Object2Binary<VersusDraftReady.VersusReadyMessageData>(data3);
                photon.SendRoomMessageBinary(1, buffer, 0, 0);
                FlowNode_GameObject.ActivateOutputLinks(self, 2);
                return;
            }

            public override void Update(VersusDraftReady self)
            {
                MyPhoton photon;
                List<MyPhoton.MyPlayer> list;
                int num;
                VersusDraftReady.VersusReadyMessageData data;
                list = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
                if (list == null)
                {
                    goto Label_001F;
                }
                if (list.Count >= 2)
                {
                    goto Label_0025;
                }
            Label_001F:
                self.GotoState<VersusDraftReady.State_RoomUpdate>();
            Label_0025:
                num = 0;
                goto Label_004F;
            Label_002C:
                data = self.mMessageDataList[num];
                if (data.h != 1)
                {
                    goto Label_004B;
                }
                self.GotoState<VersusDraftReady.State_RoomUpdate>();
            Label_004B:
                num += 1;
            Label_004F:
                if (num < self.mMessageDataList.Count)
                {
                    goto Label_002C;
                }
                return;
            }
        }

        public class VersusReadyMessageData
        {
            public int pidx;
            public int pid;
            public int h;

            public VersusReadyMessageData()
            {
                base..ctor();
                return;
            }
        }

        public enum VersusReadyMessageDataHeader
        {
            NOP,
            COMPLETE,
            NUM
        }
    }
}

