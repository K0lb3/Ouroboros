namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class MultiPlayVersusReady : MonoBehaviour
    {
        public TargetPlate TargetTemplate;
        public TargetPlate TargetObjTemplate;
        public TargetPlate TargetTrickTemplate;
        public GameObject TargetParent;
        public GameObject TargetMarker;
        public Button CameraRotateL;
        public Button CameraRotateR;
        public SRPG.TouchController TouchController;
        public Button GoButton;
        public GameObject QuestObj;
        public bool TowerMode;
        public bool RankMatchMode;
        public bool DraftMode;
        private bool m_Ready;
        private bool m_SyncLoad;
        private QuestParam m_CurrentQuest;
        private Vector3 m_CameraPos;
        private Vector3 m_CameraRot;
        private Vector3 m_CameraNextPos;
        private int m_SelectParty;
        private List<TacticsUnitController> m_Units;
        private TacticsSceneSettings m_SceneRoot;
        private TargetPlate m_Status;
        private TargetPlate m_StatusObj;
        private TargetPlate m_StatusTrick;
        private List<BattleMap> m_Map;
        private TargetCamera m_Camera;
        private readonly float CAM_ROTATE_TIME;
        private readonly float CAM_ROTATE_VAL;
        private float m_CamAngle;
        private float m_CamAngleStart;
        private float m_CamAngleGoal;
        private float m_CamRotateTime;
        private float m_CamYawMin;
        private float m_CamYawMax;
        private bool m_CamMove;
        private IntVector2 m_SelectGrid;
        private GameObject m_Marker;
        private GameObject m_TrickMarkerObj;
        private Dictionary<string, GameObject> m_TrickMarkers;
        private List<MyPhoton.MyPlayer> m_Players;
        private static MultiPlayVersusReady m_Instance;

        static MultiPlayVersusReady()
        {
        }

        public MultiPlayVersusReady()
        {
            this.m_CameraPos = Vector3.get_zero();
            this.m_CameraRot = Vector3.get_zero();
            this.m_CameraNextPos = Vector3.get_zero();
            this.m_Units = new List<TacticsUnitController>();
            this.m_Map = new List<BattleMap>();
            this.CAM_ROTATE_TIME = 0.5f;
            this.CAM_ROTATE_VAL = 45f;
            this.m_SelectGrid = new IntVector2(-1, -1);
            this.m_TrickMarkers = new Dictionary<string, GameObject>();
            base..ctor();
            return;
        }

        public unsafe IntVector2 CalcCoord(Vector3 position)
        {
            int num;
            int num2;
            Vector3 vector;
            num = Mathf.FloorToInt(&position.x);
            num2 = Mathf.FloorToInt(&position.z - &this.m_SceneRoot.get_transform().get_position().z);
            return new IntVector2(num, num2);
        }

        private unsafe void CalcPosition(TacticsUnitController controller)
        {
            Vector3 vector;
            &vector..ctor(((float) controller.Unit.x) + 0.5f, 100f, ((float) controller.Unit.y) + 0.5f);
            controller.get_gameObject().get_transform().set_position(vector);
            return;
        }

        private List<int> CheckExistUnit(int x, int y)
        {
            List<int> list;
            int num;
            TacticsUnitController controller;
            list = new List<int>();
            num = 0;
            goto Label_0047;
        Label_000D:
            controller = this.m_Units[num];
            if (controller.Unit.x != x)
            {
                goto Label_0043;
            }
            if (controller.Unit.y != y)
            {
                goto Label_0043;
            }
            list.Add(num);
        Label_0043:
            num += 1;
        Label_0047:
            if (num < this.m_Units.Count)
            {
                goto Label_000D;
            }
            return list;
        }

        private void CloseUnitStatus()
        {
            this.m_Status.Close();
            this.UpdateMarker(null);
            return;
        }

        private void DebugPlacement(GameObject go)
        {
            GUILayoutOption[] optionArray4;
            GUILayoutOption[] optionArray3;
            GUILayoutOption[] optionArray2;
            GUILayoutOption[] optionArray1;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            if (this.m_Map == null)
            {
                goto Label_001B;
            }
            if (this.m_Map.Count != null)
            {
                goto Label_001C;
            }
        Label_001B:
            return;
        Label_001C:
            num = this.CurrentMap.Width;
            num2 = this.CurrentMap.Height;
            optionArray1 = new GUILayoutOption[] { GUILayout.Width(400f), GUILayout.Height(500f) };
            GUILayout.Box(string.Empty, optionArray1);
            GUILayout.BeginArea(new Rect(20f, 30f, 400f, 500f));
            optionArray2 = new GUILayoutOption[] { GUILayout.Width(300f) };
            GUILayout.BeginHorizontal(optionArray2);
            GUILayout.Label("┌", new GUILayoutOption[0]);
            GUILayout.Space(-10f);
            num3 = 0;
            goto Label_00F3;
        Label_00B5:
            if (num3 == null)
            {
                goto Label_00D5;
            }
            GUILayout.Label("┬", new GUILayoutOption[0]);
            GUILayout.Space(-10f);
        Label_00D5:
            GUILayout.Label("─", new GUILayoutOption[0]);
            GUILayout.Space(-10f);
            num3 += 1;
        Label_00F3:
            if (num3 < num)
            {
                goto Label_00B5;
            }
            GUILayout.Label("┐", new GUILayoutOption[0]);
            GUILayout.EndHorizontal();
            num4 = 0;
            goto Label_019F;
        Label_0116:
            GUILayout.Space(-10f);
            optionArray3 = new GUILayoutOption[] { GUILayout.Width(300f) };
            GUILayout.BeginHorizontal(optionArray3);
            num5 = 0;
            goto Label_017E;
        Label_0140:
            GUILayout.Label("│", new GUILayoutOption[0]);
            GUILayout.Space(-10f);
            GUILayout.Label(this.DebugSearchPos(num5, num4), new GUILayoutOption[0]);
            GUILayout.Space(-10f);
            num5 += 1;
        Label_017E:
            if (num5 < num)
            {
                goto Label_0140;
            }
            GUILayout.Label("│", new GUILayoutOption[0]);
            GUILayout.EndHorizontal();
            num4 += 1;
        Label_019F:
            if (num4 < num2)
            {
                goto Label_0116;
            }
            GUILayout.Space(-10f);
            optionArray4 = new GUILayoutOption[] { GUILayout.Width(300f) };
            GUILayout.BeginHorizontal(optionArray4);
            GUILayout.Label("└", new GUILayoutOption[0]);
            GUILayout.Space(-10f);
            num6 = 0;
            goto Label_022B;
        Label_01EA:
            if (num6 == null)
            {
                goto Label_020B;
            }
            GUILayout.Label("┴", new GUILayoutOption[0]);
            GUILayout.Space(-10f);
        Label_020B:
            GUILayout.Label("─", new GUILayoutOption[0]);
            GUILayout.Space(-10f);
            num6 += 1;
        Label_022B:
            if (num6 < num)
            {
                goto Label_01EA;
            }
            GUILayout.Label("┘", new GUILayoutOption[0]);
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            return;
        }

        private unsafe string DebugSearchPos(int x, int y)
        {
            List<UnitSetting> list;
            List<UnitSetting> list2;
            string str;
            int num;
            int num2;
            list = this.CurrentMap.PartyUnitSettings;
            list2 = this.CurrentMap.ArenaUnitSettings;
            str = string.Empty;
            if (list == null)
            {
                goto Label_0093;
            }
            num = 0;
            goto Label_0087;
        Label_002B:
            if (x != &list[num].pos.x)
            {
                goto Label_0083;
            }
            if (y != &list[num].pos.y)
            {
                goto Label_0083;
            }
            str = "p" + string.Format("{0:D2}", (int) num);
            goto Label_0093;
        Label_0083:
            num += 1;
        Label_0087:
            if (num < list.Count)
            {
                goto Label_002B;
            }
        Label_0093:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_011A;
            }
            if (list2 == null)
            {
                goto Label_011A;
            }
            num2 = 0;
            goto Label_010D;
        Label_00AC:
            if (x != &list2[num2].pos.x)
            {
                goto Label_0107;
            }
            if (y != &list2[num2].pos.y)
            {
                goto Label_0107;
            }
            str = "e" + string.Format("{0:D2}", (int) num2);
            goto Label_011A;
        Label_0107:
            num2 += 1;
        Label_010D:
            if (num2 < list2.Count)
            {
                goto Label_00AC;
            }
        Label_011A:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_012B;
            }
            str = "   ";
        Label_012B:
            return str;
        }

        public unsafe int GetDisplayHeight(Unit unit)
        {
            TacticsUnitController controller;
            IntVector2 vector;
            Grid grid;
            <GetDisplayHeight>c__AnonStorey367 storey;
            storey = new <GetDisplayHeight>c__AnonStorey367();
            storey.unit = unit;
            controller = this.m_Units.Find(new Predicate<TacticsUnitController>(storey.<>m__371));
            if ((controller != null) == null)
            {
                goto Label_0065;
            }
            vector = this.CalcCoord(controller.CenterPosition);
            grid = this.CurrentMap[&vector.x, &vector.y];
            if (grid == null)
            {
                goto Label_0065;
            }
            return grid.height;
        Label_0065:
            return 0;
        }

        private unsafe int GetPlacementID(int x, int y)
        {
            int num;
            int num2;
            num = 0;
            if (this.CurrentMap == null)
            {
                goto Label_0081;
            }
            num2 = 0;
            goto Label_006B;
        Label_0014:
            if (&this.CurrentMap.PartyUnitSettings[num2].pos.x != x)
            {
                goto Label_0067;
            }
            if (&this.CurrentMap.PartyUnitSettings[num2].pos.y != y)
            {
                goto Label_0067;
            }
            num = num2;
            goto Label_0081;
        Label_0067:
            num2 += 1;
        Label_006B:
            if (num2 < this.CurrentMap.PartyUnitSettings.Count)
            {
                goto Label_0014;
            }
        Label_0081:
            return num;
        }

        private unsafe void InitCamera()
        {
            Camera camera;
            GameSettings settings;
            if ((Camera.get_main() != null) == null)
            {
                goto Label_015D;
            }
            camera = Camera.get_main();
            settings = GameSettings.Instance;
            Camera.get_main().get_gameObject().SetActive(1);
            RenderPipeline.Setup(Camera.get_main());
            this.m_Camera = GameUtility.RequireComponent<TargetCamera>(Camera.get_main().get_gameObject());
            this.m_Camera.CameraDistance = settings.GameCamera_DefaultDistance;
            if (this.CurrentMap == null)
            {
                goto Label_00BE;
            }
            &this.m_CameraPos.x = ((float) this.CurrentMap.Width) * 0.5f;
            &this.m_CameraPos.z = ((float) this.CurrentMap.Height) * 0.5f;
            &this.m_CameraRot.Set(settings.GameCamera_AngleX, -settings.GameCamera_YawMin, 0f);
        Label_00BE:
            camera.get_transform().set_position(this.m_CameraPos);
            camera.get_transform().set_rotation(Quaternion.Euler(this.m_CameraRot));
            camera.set_fieldOfView(settings.GameCamera_TacticsSceneFOV);
            this.m_CamAngle = settings.GameCamera_YawMin;
            if ((this.CameraRotateL != null) == null)
            {
                goto Label_012A;
            }
            this.CameraRotateL.get_onClick().AddListener(new UnityAction(this, this.OnCameraRotateL));
        Label_012A:
            if ((this.CameraRotateR != null) == null)
            {
                goto Label_0157;
            }
            this.CameraRotateR.get_onClick().AddListener(new UnityAction(this, this.OnCameraRotateR));
        Label_0157:
            this.UpdateCameraPosition();
        Label_015D:
            return;
        }

        private void InitMap()
        {
            int num;
            BattleMap map;
            num = 0;
            goto Label_003F;
        Label_0007:
            map = new BattleMap();
            if (map.Initialize(null, this.m_CurrentQuest.map[num]) != null)
            {
                goto Label_002F;
            }
            goto Label_0055;
        Label_002F:
            this.m_Map.Add(map);
            num += 1;
        Label_003F:
            if (num < this.m_CurrentQuest.map.Count)
            {
                goto Label_0007;
            }
        Label_0055:
            return;
        }

        private void InitStatusWindow()
        {
            if ((this.TargetTemplate != null) == null)
            {
                goto Label_008E;
            }
            this.m_Status = Object.Instantiate<TargetPlate>(this.TargetTemplate);
            if ((this.m_Status != null) == null)
            {
                goto Label_008E;
            }
            if ((this.TargetParent != null) == null)
            {
                goto Label_0060;
            }
            this.m_Status.get_gameObject().get_transform().SetParent(base.get_transform(), 0);
        Label_0060:
            this.m_Status.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextUnit));
            this.m_Status.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevUnit));
        Label_008E:
            if ((this.TargetObjTemplate != null) == null)
            {
                goto Label_011C;
            }
            this.m_StatusObj = Object.Instantiate<TargetPlate>(this.TargetObjTemplate);
            if ((this.m_StatusObj != null) == null)
            {
                goto Label_011C;
            }
            if ((this.TargetParent != null) == null)
            {
                goto Label_00EE;
            }
            this.m_StatusObj.get_gameObject().get_transform().SetParent(base.get_transform(), 0);
        Label_00EE:
            this.m_StatusObj.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextUnit));
            this.m_StatusObj.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevUnit));
        Label_011C:
            if ((this.TargetTrickTemplate != null) == null)
            {
                goto Label_01AA;
            }
            this.m_StatusTrick = Object.Instantiate<TargetPlate>(this.TargetTrickTemplate);
            if ((this.m_StatusTrick != null) == null)
            {
                goto Label_01AA;
            }
            if ((this.TargetParent != null) == null)
            {
                goto Label_017C;
            }
            this.m_StatusTrick.get_gameObject().get_transform().SetParent(base.get_transform(), 0);
        Label_017C:
            this.m_StatusTrick.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextUnit));
            this.m_StatusTrick.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevUnit));
        Label_01AA:
            return;
        }

        private void InitTargetMarker()
        {
            if ((this.TargetMarker != null) == null)
            {
                goto Label_0053;
            }
            this.m_Marker = Object.Instantiate(this.TargetMarker, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
            this.m_Marker.get_gameObject().SetActive(0);
            GameUtility.SetLayer(this.m_Marker, GameUtility.LayerUI, 1);
        Label_0053:
            return;
        }

        private void InitTouchArea()
        {
            if ((this.TouchController != null) == null)
            {
                goto Label_0028;
            }
            this.TouchController.OnClick = new SRPG.TouchController.ClickEvent(this.OnTouchClick);
        Label_0028:
            return;
        }

        private bool IsSamePosition()
        {
            int num;
            int num2;
            num = 0;
            goto Label_008D;
        Label_0007:
            num2 = num + 1;
            goto Label_0078;
        Label_0010:
            if (this.m_Units[num].Unit.x != this.m_Units[num2].Unit.x)
            {
                goto Label_0074;
            }
            if (this.m_Units[num].Unit.y != this.m_Units[num2].Unit.y)
            {
                goto Label_0074;
            }
            return 1;
        Label_0074:
            num2 += 1;
        Label_0078:
            if (num2 < this.m_Units.Count)
            {
                goto Label_0010;
            }
            num += 1;
        Label_008D:
            if (num < this.m_Units.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        private List<Unit> LoadMultiTower()
        {
            MyPhoton photon;
            List<MyPhoton.MyPlayer> list;
            List<Unit> list2;
            int num;
            JSON_MyPhotonPlayerParam param;
            int num2;
            int num3;
            UnitData data;
            Unit unit;
            int num4;
            List<NPCSetting> list3;
            int num5;
            Unit unit2;
            list = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
            list2 = new List<Unit>();
            if (this.m_Players != null)
            {
                goto Label_002A;
            }
            this.m_Players = new List<MyPhoton.MyPlayer>(list);
        Label_002A:
            num = 0;
            goto Label_0148;
        Label_0031:
            param = JSON_MyPhotonPlayerParam.Parse(list[num].json);
            if (param != null)
            {
                goto Label_0050;
            }
            goto Label_0144;
        Label_0050:
            num2 = 0;
            goto Label_0134;
        Label_0058:
            if (param.units[num2] != null)
            {
                goto Label_006C;
            }
            goto Label_012E;
        Label_006C:
            if (param.units[num2].sub == null)
            {
                goto Label_0085;
            }
            goto Label_012E;
        Label_0085:
            num3 = (param.units[num2].place >= 0) ? param.units[num2].place : num2;
            data = new UnitData();
            if (data == null)
            {
                goto Label_012E;
            }
            data.Deserialize(param.units[num2].unitJson);
            DownloadUtility.DownloadUnit(data.UnitParam, null);
            unit = new Unit();
            if (unit != null)
            {
                goto Label_00F6;
            }
            goto Label_012E;
        Label_00F6:
            if (unit.Setup(data, this.CurrentMap.PartyUnitSettings[num3], null, null) == null)
            {
                goto Label_012E;
            }
            unit.OwnerPlayerIndex = param.playerIndex;
            list2.Add(unit);
        Label_012E:
            num2 += 1;
        Label_0134:
            if (num2 < ((int) param.units.Length))
            {
                goto Label_0058;
            }
        Label_0144:
            num += 1;
        Label_0148:
            if (num < list.Count)
            {
                goto Label_0031;
            }
            num4 = this.CurrentMap.NPCUnitSettings.Count;
            list3 = this.CurrentMap.NPCUnitSettings;
            num5 = 0;
            goto Label_01BB;
        Label_017B:
            DownloadUtility.DownloadUnit(list3[num5]);
            unit2 = new Unit();
            if (unit2.Setup(null, list3[num5], null, null) != null)
            {
                goto Label_01AD;
            }
            goto Label_01B5;
        Label_01AD:
            list2.Add(unit2);
        Label_01B5:
            num5 += 1;
        Label_01BB:
            if (num5 < num4)
            {
                goto Label_017B;
            }
            return list2;
        }

        private List<Unit> LoadRankMatchParty()
        {
            GameManager manager;
            PlayerData data;
            PartyData data2;
            int num;
            long num2;
            List<Unit> list;
            int num3;
            UnitData data3;
            Unit unit;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = data.Partys[10];
            num = -1;
            num2 = 0L;
            list = new List<Unit>();
            num3 = 0;
            goto Label_00C2;
        Label_0030:
            num2 = data2.GetUnitUniqueID(num3);
            if (num2 == null)
            {
                goto Label_00BC;
            }
            data3 = data.FindUnitDataByUniqueID(num2);
            DownloadUtility.DownloadUnit(data3.UnitParam, null);
            unit = new Unit();
            num = data.GetVersusPlacement(PlayerPrefsUtility.RANKMATCH_ID_KEY + ((int) num3));
            if (num < 0)
            {
                goto Label_0094;
            }
            if (num < this.CurrentMap.PartyUnitSettings.Count)
            {
                goto Label_0096;
            }
        Label_0094:
            num = 0;
        Label_0096:
            unit.Setup(data3, this.CurrentMap.PartyUnitSettings[num], null, null);
            list.Add(unit);
        Label_00BC:
            num3 += 1;
        Label_00C2:
            if (num3 < data2.MAX_UNIT)
            {
                goto Label_0030;
            }
            return list;
        }

        [DebuggerHidden]
        private IEnumerator LoadSceneAsync()
        {
            <LoadSceneAsync>c__Iterator127 iterator;
            iterator = new <LoadSceneAsync>c__Iterator127();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator LoadUnit()
        {
            <LoadUnit>c__Iterator128 iterator;
            iterator = new <LoadUnit>c__Iterator128();
            iterator.<>f__this = this;
            return iterator;
        }

        private List<Unit> LoadVersusDraftParty()
        {
            List<Unit> list;
            int num;
            UnitData data;
            int num2;
            Unit unit;
            list = new List<Unit>();
            num = 0;
            goto Label_006E;
        Label_000D:
            data = VersusDraftList.VersusDraftPartyUnits[num];
            DownloadUtility.DownloadUnit(data.UnitParam, null);
            num2 = num;
            if (num2 < this.CurrentMap.PartyUnitSettings.Count)
            {
                goto Label_003F;
            }
            num2 = 0;
        Label_003F:
            unit = new Unit();
            unit.Setup(data, this.CurrentMap.PartyUnitSettings[num2], null, null);
            list.Add(unit);
            num += 1;
        Label_006E:
            if (num < VersusDraftList.VersusDraftPartyUnits.Count)
            {
                goto Label_000D;
            }
            return list;
        }

        private List<Unit> LoadVersusParty()
        {
            GameManager manager;
            PlayerData data;
            PartyData data2;
            int num;
            long num2;
            List<Unit> list;
            int num3;
            UnitData data3;
            Unit unit;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = data.Partys[7];
            num = -1;
            num2 = 0L;
            list = new List<Unit>();
            num3 = 0;
            goto Label_00CC;
        Label_002F:
            num2 = data2.GetUnitUniqueID(num3);
            if (num2 == null)
            {
                goto Label_00C6;
            }
            data3 = data.FindUnitDataByUniqueID(num2);
            DownloadUtility.DownloadUnit(data3.UnitParam, null);
            unit = new Unit();
            if (data2.PartyType != 10)
            {
                goto Label_0088;
            }
            num = data.GetVersusPlacement(PlayerPrefsUtility.RANKMATCH_ID_KEY + ((int) num3));
            goto Label_00A0;
        Label_0088:
            num = data.GetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + ((int) num3));
        Label_00A0:
            unit.Setup(data3, this.CurrentMap.PartyUnitSettings[num], null, null);
            list.Add(unit);
        Label_00C6:
            num3 += 1;
        Label_00CC:
            if (num3 < data2.MAX_UNIT)
            {
                goto Label_002F;
            }
            return list;
        }

        private void OnCameraRotateL()
        {
            if (this.m_SyncLoad == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.m_CamMove != null)
            {
                goto Label_0048;
            }
            this.m_CamAngleStart = this.m_CamAngle;
            this.m_CamAngleGoal = this.m_CamAngle - this.CAM_ROTATE_VAL;
            this.m_CamRotateTime = 0f;
            this.m_CamMove = 1;
        Label_0048:
            return;
        }

        private void OnCameraRotateR()
        {
            if (this.m_SyncLoad == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.m_CamMove != null)
            {
                goto Label_0048;
            }
            this.m_CamAngleStart = this.m_CamAngle;
            this.m_CamAngleGoal = this.m_CamAngle + this.CAM_ROTATE_VAL;
            this.m_CamRotateTime = 0f;
            this.m_CamMove = 1;
        Label_0048:
            return;
        }

        private void OnClickGo()
        {
            GameManager manager;
            PlayerData data;
            int num;
            int num2;
            string str;
            if (this.m_SyncLoad == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.IsSamePosition() == null)
            {
                goto Label_0023;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "SAME_POSITION");
            return;
        Label_0023:
            this.m_Ready = 0;
            data = MonoSingleton<GameManager>.Instance.Player;
            VersusDraftList.VersusDraftPartyPlaces.Clear();
            num = 0;
            goto Label_00D2;
        Label_0048:
            num2 = this.GetPlacementID(this.m_Units[num].Unit.x, this.m_Units[num].Unit.y);
            if (this.DraftMode != null)
            {
                goto Label_00C3;
            }
            str = PlayerPrefsUtility.VERSUS_ID_KEY + ((int) num);
            if (this.RankMatchMode == null)
            {
                goto Label_00B5;
            }
            str = PlayerPrefsUtility.RANKMATCH_ID_KEY + ((int) num);
        Label_00B5:
            data.SetVersusPlacement(str, num2);
            goto Label_00CE;
        Label_00C3:
            VersusDraftList.VersusDraftPartyPlaces.Add(num2);
        Label_00CE:
            num += 1;
        Label_00D2:
            if (num < this.m_Units.Count)
            {
                goto Label_0048;
            }
            if (this.DraftMode != null)
            {
                goto Label_00F4;
            }
            data.SavePlayerPrefs();
        Label_00F4:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "FINISH_PLACEMENT");
            return;
        }

        private void OnDestroy()
        {
            if (this.CurrentMap == null)
            {
                goto Label_001B;
            }
            AssetManager.UnloadScene(this.CurrentMap.MapSceneName);
        Label_001B:
            m_Instance = null;
            return;
        }

        private void OnLoadScene(GameObject go)
        {
            RenderPipeline pipeline;
            this.m_SceneRoot = go.GetComponent<TacticsSceneSettings>();
            if ((this.m_SceneRoot != null) == null)
            {
                goto Label_0087;
            }
            if ((Camera.get_main() != null) == null)
            {
                goto Label_0087;
            }
            pipeline = Camera.get_main().GetComponent<RenderPipeline>();
            if ((pipeline != null) == null)
            {
                goto Label_0066;
            }
            pipeline.BackgroundImage = this.m_SceneRoot.BackgroundImage;
            pipeline.ScreenFilter = this.m_SceneRoot.ScreenFilter;
        Label_0066:
            this.m_SceneRoot.GenerateGridMesh(this.CurrentMap.Width, this.CurrentMap.Height);
        Label_0087:
            go.SetActive(1);
            return;
        }

        private void OnNextUnit(GameObject obj)
        {
            if (this.m_SyncLoad == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.UpdateUnitStatus(1, 0);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0002", 0f);
            return;
        }

        private void OnPrevUnit(GameObject obj)
        {
            if (this.m_SyncLoad == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.UpdateUnitStatus(-1, 0);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0002", 0f);
            return;
        }

        private unsafe void OnTouchClick(Vector2 screenPos)
        {
            Ray ray;
            RaycastHit hit;
            Vector3 vector;
            Vector3 vector2;
            if (this.m_SyncLoad == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((Camera.get_main() != null) == null)
            {
                goto Label_0089;
            }
            if (this.CurrentMap == null)
            {
                goto Label_0089;
            }
            if (Physics.Raycast(Camera.get_main().ScreenPointToRay(screenPos), &hit) == null)
            {
                goto Label_0089;
            }
            &this.m_SelectGrid.x = Mathf.FloorToInt(&&hit.get_point().x);
            &this.m_SelectGrid.y = Mathf.FloorToInt(&&hit.get_point().z);
            this.UpdateSelectGrid();
        Label_0089:
            return;
        }

        private void SendPlacementInfo()
        {
            MyPhoton.MyPlayer player;
            TacticsUnitController controller;
            <SendPlacementInfo>c__AnonStorey365 storey;
            <SendPlacementInfo>c__AnonStorey366 storey2;
            storey = new <SendPlacementInfo>c__AnonStorey365();
            storey.pt = PunMonoSingleton<MyPhoton>.Instance;
            player = storey.pt.GetMyPlayer();
            if (player != null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            storey.param = JSON_MyPhotonPlayerParam.Parse(player.json);
            if (storey.param.units == null)
            {
                goto Label_010E;
            }
            storey2 = new <SendPlacementInfo>c__AnonStorey366();
            storey2.<>f__ref$869 = storey;
            storey2.i = 0;
            goto Label_00F6;
        Label_005E:
            controller = this.m_Units.Find(new Predicate<TacticsUnitController>(storey2.<>m__370));
            if ((controller != null) == null)
            {
                goto Label_00E8;
            }
            storey.param.units[storey2.i].place = this.GetPlacementID(controller.Unit.x, controller.Unit.y);
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTITW_ID_KEY + ((int) storey2.i), storey.param.units[storey2.i].place, 0);
        Label_00E8:
            storey2.i += 1;
        Label_00F6:
            if (storey2.i < ((int) storey.param.units.Length))
            {
                goto Label_005E;
            }
        Label_010E:
            PlayerPrefsUtility.Save();
            storey.pt.SetMyPlayerParam(storey.param.Serialize());
            return;
        }

        private void Start()
        {
            GameManager manager;
            GUIEventListener listener;
            manager = MonoSingleton<GameManager>.Instance;
            if (GameUtility.IsDebugBuild == null)
            {
                goto Label_002E;
            }
            base.get_gameObject().AddComponent<GUIEventListener>().Listeners = new GUIEventListener.GUIEvent(this.DebugPlacement);
        Label_002E:
            this.m_SelectParty = 0;
            this.m_CurrentQuest = manager.FindQuest(GlobalVars.SelectedQuestID);
            if (this.m_CurrentQuest == null)
            {
                goto Label_0073;
            }
            if ((this.QuestObj != null) == null)
            {
                goto Label_0073;
            }
            DataSource.Bind<QuestParam>(this.QuestObj, this.m_CurrentQuest);
        Label_0073:
            if ((this.GoButton != null) == null)
            {
                goto Label_00A0;
            }
            this.GoButton.get_onClick().AddListener(new UnityAction(this, this.OnClickGo));
        Label_00A0:
            this.InitTouchArea();
            this.InitStatusWindow();
            this.InitTargetMarker();
            base.StartCoroutine(this.LoadSceneAsync());
            m_Instance = this;
            return;
        }

        private unsafe void SyncRoomPlayer()
        {
            bool flag;
            bool flag2;
            MyPhoton photon;
            List<MyPhoton.MyPlayer> list;
            int num;
            JSON_MyPhotonPlayerParam param;
            JSON_MyPhotonPlayerParam param2;
            int num2;
            UnitData data;
            UnitData data2;
            int num3;
            int num4;
            TacticsUnitController controller;
            OIntVector2 vector;
            <SyncRoomPlayer>c__AnonStorey364 storey;
            <SyncRoomPlayer>c__AnonStorey363 storey2;
            flag = 0;
            flag2 = 0;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            list = photon.GetRoomPlayerList();
            if (this.m_Players != null)
            {
                goto Label_0023;
            }
            flag2 = 1;
            goto Label_01B4;
        Label_0023:
            if (list.Count == this.m_Players.Count)
            {
                goto Label_0040;
            }
            flag2 = 1;
            goto Label_01B4;
        Label_0040:
            num = 0;
            goto Label_01A7;
        Label_0048:
            if (list[num].json.Equals(this.m_Players[num].json) != null)
            {
                goto Label_01A1;
            }
            param = JSON_MyPhotonPlayerParam.Parse(this.m_Players[num].json);
            param2 = JSON_MyPhotonPlayerParam.Parse(list[num].json);
            if (param2 == null)
            {
                goto Label_01A1;
            }
            if (param == null)
            {
                goto Label_01A1;
            }
            if (param2.playerIndex == photon.MyPlayerIndex)
            {
                goto Label_01A1;
            }
            if (param.playerID == param2.playerID)
            {
                goto Label_00D8;
            }
            flag2 = 1;
            goto Label_01B4;
        Label_00D8:
            if (((int) param2.units.Length) == ((int) param.units.Length))
            {
                goto Label_00F6;
            }
            flag2 = 1;
            goto Label_01B4;
        Label_00F6:
            num2 = 0;
            goto Label_0191;
        Label_00FE:
            data = new UnitData();
            data2 = new UnitData();
            data.Deserialize(param2.units[num2].unitJson);
            data2.Deserialize(param.units[num2].unitJson);
            if ((data.UnitParam.iname != data2.UnitParam.iname) == null)
            {
                goto Label_0161;
            }
            flag2 = 1;
            goto Label_01A1;
        Label_0161:
            if (param2.units[num2].place == param.units[num2].place)
            {
                goto Label_018B;
            }
            flag = 1;
            goto Label_01A1;
        Label_018B:
            num2 += 1;
        Label_0191:
            if (num2 < ((int) param2.units.Length))
            {
                goto Label_00FE;
            }
        Label_01A1:
            num += 1;
        Label_01A7:
            if (num < list.Count)
            {
                goto Label_0048;
            }
        Label_01B4:
            this.m_Players = list;
            if (flag2 == null)
            {
                goto Label_01E0;
            }
            this.CloseUnitStatus();
            this.m_SyncLoad = 1;
            base.StartCoroutine(this.LoadUnit());
            goto Label_0322;
        Label_01E0:
            if (flag == null)
            {
                goto Label_0322;
            }
            num3 = 0;
            goto Label_0315;
        Label_01EE:
            storey = new <SyncRoomPlayer>c__AnonStorey364();
            storey.param = JSON_MyPhotonPlayerParam.Parse(list[num3].json);
            if (storey.param == null)
            {
                goto Label_030F;
            }
            num4 = 0;
            goto Label_02FA;
        Label_0222:
            storey2 = new <SyncRoomPlayer>c__AnonStorey363();
            storey2.<>f__ref$868 = storey;
            storey2.unitData = new UnitData();
            if (storey2.unitData == null)
            {
                goto Label_02F4;
            }
            storey2.unitData.Deserialize(storey.param.units[num4].unitJson);
            controller = this.m_Units.Find(new Predicate<TacticsUnitController>(storey2.<>m__36F));
            if ((controller != null) == null)
            {
                goto Label_02F4;
            }
            vector = this.CurrentMap.PartyUnitSettings[storey.param.units[num4].place].pos;
            controller.Unit.x = &vector.x;
            controller.Unit.y = &vector.y;
            this.CalcPosition(controller);
        Label_02F4:
            num4 += 1;
        Label_02FA:
            if (num4 < ((int) storey.param.units.Length))
            {
                goto Label_0222;
            }
        Label_030F:
            num3 += 1;
        Label_0315:
            if (num3 < list.Count)
            {
                goto Label_01EE;
            }
        Label_0322:
            this.UpdateGridColor();
            return;
        }

        private void Update()
        {
            MyPhoton photon;
            this.UpdateCamera();
            if (this.m_Ready != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (this.TowerMode == null)
            {
                goto Label_0046;
            }
            if (this.m_SyncLoad != null)
            {
                goto Label_0046;
            }
            if (photon.IsUpdatePlayerProperty == null)
            {
                goto Label_0046;
            }
            photon.IsUpdatePlayerProperty = 0;
            this.SyncRoomPlayer();
        Label_0046:
            return;
        }

        private unsafe void UpdateCamera()
        {
            Camera camera;
            Vector2 vector;
            Vector3 vector2;
            Vector3 vector3;
            Vector3 vector4;
            Vector3 vector5;
            Vector2 vector6;
            Vector2 vector7;
            Vector3 vector8;
            this.UpdateCameraRotate();
            if ((this.TouchController != null) == null)
            {
                goto Label_025B;
            }
            if ((Camera.get_main() != null) == null)
            {
                goto Label_025B;
            }
            if (this.m_CamMove != null)
            {
                goto Label_025B;
            }
            camera = Camera.get_main();
            if (&this.TouchController.Velocity.get_magnitude() <= 0f)
            {
                goto Label_0215;
            }
            vector = this.TouchController.Velocity;
            vector2 = camera.get_transform().get_right();
            vector3 = camera.get_transform().get_forward();
            &vector2.y = 0f;
            &vector3.y = 0f;
            &vector2.Normalize();
            &vector3.Normalize();
            vector4 = camera.WorldToScreenPoint(this.m_CameraPos);
            vector6 = camera.WorldToScreenPoint((this.m_CameraPos + vector2) + vector3) - vector4;
            &vector.x /= Mathf.Abs(&vector6.x);
            &vector.y /= Mathf.Abs(&vector6.y);
            &vector7..ctor((&vector2.x * &vector.x) + (&vector3.x * &vector.y), (&vector2.z * &vector.x) + (&vector3.z * &vector.y));
            &this.m_CameraPos.x -= &vector7.x;
            &this.m_CameraPos.z -= &vector7.y;
            if (this.CurrentMap == null)
            {
                goto Label_01EE;
            }
            &this.m_CameraPos.x = Mathf.Clamp(&this.m_CameraPos.x, 0.1f, ((float) this.CurrentMap.Width) - 0.1f);
            &this.m_CameraPos.z = Mathf.Clamp(&this.m_CameraPos.z, 0.1f, ((float) this.CurrentMap.Height) - 0.1f);
        Label_01EE:
            this.TouchController.Velocity = Vector2.get_zero();
            this.UpdateCameraPosition();
            this.m_CameraNextPos = this.m_CameraPos;
            goto Label_025B;
        Label_0215:
            vector8 = this.m_CameraPos - this.m_CameraNextPos;
            if (&vector8.get_magnitude() <= 0.01f)
            {
                goto Label_025B;
            }
            this.m_CameraPos = Vector3.Lerp(this.m_CameraPos, this.m_CameraNextPos, 0.1f);
            this.UpdateCameraPosition();
        Label_025B:
            return;
        }

        private unsafe void UpdateCameraPosition()
        {
            GameSettings settings;
            settings = GameSettings.Instance;
            &this.m_CameraPos.y = GameUtility.CalcHeight(&this.m_CameraPos.x, &this.m_CameraPos.z);
            this.m_Camera.Pitch = &this.m_CameraRot.x;
            this.m_Camera.Yaw = &this.m_CameraRot.y;
            this.m_Camera.SetPositionYaw(this.m_CameraPos + (Vector3.get_up() * settings.GameCamera_UnitHeightOffset), this.m_CamAngle);
            return;
        }

        private void UpdateCameraRotate()
        {
            float num;
            float num2;
            if (this.m_CamMove != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.m_CamRotateTime += Time.get_deltaTime();
            if (this.m_CamRotateTime <= this.CAM_ROTATE_TIME)
            {
                goto Label_0042;
            }
            this.m_CamRotateTime = this.CAM_ROTATE_TIME;
            this.m_CamMove = 0;
        Label_0042:
            num = Mathf.Sin(1.570796f * (this.m_CamRotateTime / this.CAM_ROTATE_TIME));
            num2 = Mathf.Lerp(this.m_CamAngleStart, this.m_CamAngleGoal, num);
            this.m_CamAngle = num2;
            this.UpdateCameraPosition();
            return;
        }

        private unsafe void UpdateGridColor()
        {
            GameSettings settings;
            GridMap<Color32> map;
            MyPhoton photon;
            UnitSetting setting;
            List<UnitSetting>.Enumerator enumerator;
            TacticsUnitController controller;
            List<TacticsUnitController>.Enumerator enumerator2;
            NPCSetting setting2;
            List<NPCSetting>.Enumerator enumerator3;
            if (this.CurrentMap != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            settings = GameSettings.Instance;
            map = new GridMap<Color32>(this.CurrentMap.Width, this.CurrentMap.Height);
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (this.CurrentMap.PartyUnitSettings == null)
            {
                goto Label_00B7;
            }
            enumerator = this.CurrentMap.PartyUnitSettings.GetEnumerator();
        Label_0056:
            try
            {
                goto Label_0099;
            Label_005B:
                setting = &enumerator.Current;
                map.set(&setting.pos.x, &setting.pos.y, &settings.Colors.WalkableArea);
            Label_0099:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_005B;
                }
                goto Label_00B7;
            }
            finally
            {
            Label_00AA:
                ((List<UnitSetting>.Enumerator) enumerator).Dispose();
            }
        Label_00B7:
            enumerator2 = this.m_Units.GetEnumerator();
        Label_00C4:
            try
            {
                goto Label_0117;
            Label_00C9:
                controller = &enumerator2.Current;
                if (controller.Unit.OwnerPlayerIndex == photon.MyPlayerIndex)
                {
                    goto Label_0117;
                }
                map.set(controller.Unit.x, controller.Unit.y, &settings.Colors.Helper);
            Label_0117:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00C9;
                }
                goto Label_0135;
            }
            finally
            {
            Label_0128:
                ((List<TacticsUnitController>.Enumerator) enumerator2).Dispose();
            }
        Label_0135:
            if (this.CurrentMap.NPCUnitSettings == null)
            {
                goto Label_01BB;
            }
            enumerator3 = this.CurrentMap.NPCUnitSettings.GetEnumerator();
        Label_0157:
            try
            {
                goto Label_019D;
            Label_015C:
                setting2 = &enumerator3.Current;
                map.set(&setting2.pos.x, &setting2.pos.y, &settings.Colors.Enemy);
            Label_019D:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_015C;
                }
                goto Label_01BB;
            }
            finally
            {
            Label_01AE:
                ((List<NPCSetting>.Enumerator) enumerator3).Dispose();
            }
        Label_01BB:
            this.m_SceneRoot.ShowGridLayer(0, map, 1);
            return;
        }

        private void UpdateMarker(TacticsUnitController controller)
        {
            if ((controller != null) == null)
            {
                goto Label_0069;
            }
            if ((this.m_Marker != null) == null)
            {
                goto Label_0069;
            }
            this.m_Marker.get_transform().SetParent(controller.get_transform(), 0);
            this.m_Marker.get_transform().set_localPosition(Vector3.get_up() * 1.5f);
            this.m_Marker.get_gameObject().SetActive(1);
            goto Label_0091;
        Label_0069:
            this.m_Marker.get_gameObject().SetActive(0);
            this.m_Marker.get_transform().SetParent(base.get_transform(), 0);
        Label_0091:
            return;
        }

        private unsafe void UpdateSelectGrid()
        {
            int num;
            TacticsUnitController controller;
            MyPhoton photon;
            List<int> list;
            int num2;
            int num3;
            UnitSetting setting;
            List<UnitSetting>.Enumerator enumerator;
            if (&this.m_SelectGrid.x < 0)
            {
                goto Label_0022;
            }
            if (&this.m_SelectGrid.y >= 0)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            if (this.m_Ready != null)
            {
                goto Label_002F;
            }
            return;
        Label_002F:
            if (this.m_SelectParty >= 0)
            {
                goto Label_00BF;
            }
            num = 0;
            goto Label_0095;
        Label_0042:
            controller = this.m_Units[num];
            if (controller.Unit.x != &this.m_SelectGrid.x)
            {
                goto Label_0091;
            }
            if (controller.Unit.y != &this.m_SelectGrid.y)
            {
                goto Label_0091;
            }
            this.m_SelectParty = num;
            goto Label_00A6;
        Label_0091:
            num += 1;
        Label_0095:
            if (num < this.m_Units.Count)
            {
                goto Label_0042;
            }
        Label_00A6:
            if (this.m_SelectParty < 0)
            {
                goto Label_031C;
            }
            this.UpdateUnitStatus(0, 0);
            goto Label_031C;
        Label_00BF:
            photon = PunMonoSingleton<MyPhoton>.Instance;
            list = this.CheckExistUnit(&this.m_SelectGrid.x, &this.m_SelectGrid.y);
            if (list.Count <= 0)
            {
                goto Label_016E;
            }
            num2 = list[0];
            num3 = 0;
            goto Label_013C;
        Label_00FF:
            if (this.m_Units[list[num3]].Unit.OwnerPlayerIndex != photon.MyPlayerIndex)
            {
                goto Label_0136;
            }
            num2 = list[num3];
            goto Label_0149;
        Label_0136:
            num3 += 1;
        Label_013C:
            if (num3 < list.Count)
            {
                goto Label_00FF;
            }
        Label_0149:
            this.m_SelectParty = num2;
            this.UpdateUnitStatus(0, 1);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0005", 0f);
            return;
        Label_016E:
            if (this.TowerMode == null)
            {
                goto Label_01C1;
            }
            if (this.m_Units[this.m_SelectParty].Unit.Side == null)
            {
                goto Label_019A;
            }
            return;
        Label_019A:
            if (this.m_Units[this.m_SelectParty].Unit.OwnerPlayerIndex == photon.MyPlayerIndex)
            {
                goto Label_01C1;
            }
            return;
        Label_01C1:
            enumerator = this.CurrentMap.PartyUnitSettings.GetEnumerator();
        Label_01D3:
            try
            {
                goto Label_02FE;
            Label_01D8:
                setting = &enumerator.Current;
                if (this.CheckExistUnit(&setting.pos.x, &setting.pos.y).Count <= 0)
                {
                    goto Label_021B;
                }
                goto Label_02FE;
            Label_021B:
                if (&setting.pos.x != &this.m_SelectGrid.x)
                {
                    goto Label_02FE;
                }
                if (&setting.pos.y != &this.m_SelectGrid.y)
                {
                    goto Label_02FE;
                }
                this.m_Units[this.m_SelectParty].Unit.x = &setting.pos.x;
                this.m_Units[this.m_SelectParty].Unit.y = &setting.pos.y;
                this.CalcPosition(this.m_Units[this.m_SelectParty]);
                this.UpdateUnitStatus(0, 1);
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0002", 0f);
                if (this.TowerMode == null)
                {
                    goto Label_030A;
                }
                this.SendPlacementInfo();
                goto Label_030A;
            Label_02FE:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_01D8;
                }
            Label_030A:
                goto Label_031C;
            }
            finally
            {
            Label_030F:
                ((List<UnitSetting>.Enumerator) enumerator).Dispose();
            }
        Label_031C:
            return;
        }

        private unsafe void UpdateUnitStatus(int add, bool lerp)
        {
            int num;
            int num2;
            int num3;
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            Vector3 vector4;
            num = this.m_SelectParty;
        Label_0007:
            this.m_SelectParty += add;
            if (this.m_SelectParty >= 0)
            {
                goto Label_0039;
            }
            this.m_SelectParty = this.m_Units.Count - 1;
            goto Label_0056;
        Label_0039:
            if (this.m_SelectParty < this.m_Units.Count)
            {
                goto Label_0056;
            }
            this.m_SelectParty = 0;
        Label_0056:
            if (num != this.m_SelectParty)
            {
                goto Label_0067;
            }
            goto Label_00B2;
        Label_0067:
            if (this.m_Units[this.m_SelectParty].Unit.IsGimmick == null)
            {
                goto Label_00B2;
            }
            if (this.m_Units[this.m_SelectParty].Unit.IsBreakObj == null)
            {
                goto Label_00AC;
            }
            goto Label_00B2;
        Label_00AC:
            if (add != null)
            {
                goto Label_0007;
            }
        Label_00B2:
            num2 = this.m_SelectParty;
            num3 = this.m_Units[num2].Unit.CurrentStatus.param.hp;
            if (this.m_Units[num2].Unit.IsGimmick == null)
            {
                goto Label_017A;
            }
            if (this.m_Units[num2].Unit.IsBreakObj != null)
            {
                goto Label_017A;
            }
            if ((this.m_Status != null) == null)
            {
                goto Label_0131;
            }
            this.m_Status.Close();
        Label_0131:
            if ((this.m_StatusTrick != null) == null)
            {
                goto Label_014D;
            }
            this.m_StatusTrick.Close();
        Label_014D:
            this.m_StatusObj.SetNoAction(this.m_Units[num2].Unit, null);
            this.m_StatusObj.Open();
            goto Label_0201;
        Label_017A:
            if ((this.m_StatusObj != null) == null)
            {
                goto Label_0196;
            }
            this.m_StatusObj.Close();
        Label_0196:
            if ((this.m_StatusTrick != null) == null)
            {
                goto Label_01B2;
            }
            this.m_StatusTrick.Close();
        Label_01B2:
            this.m_Status.SetNoAction(this.m_Units[num2].Unit, null);
            this.m_Status.SetHpGaugeParam(0, num3, num3, 0, 0, 0);
            this.m_Status.Open();
            this.m_Status.UpdateHpGauge();
            this.m_Status.HideButton();
        Label_0201:
            this.UpdateMarker(this.m_Units[num2]);
            if (lerp == null)
            {
                goto Label_027D;
            }
            this.m_CameraNextPos = this.m_CameraPos;
            &this.m_CameraNextPos.x = &this.m_Units[num2].get_transform().get_position().x;
            &this.m_CameraNextPos.z = &this.m_Units[num2].get_transform().get_position().z;
            goto Label_02DD;
        Label_027D:
            &this.m_CameraPos.x = &this.m_Units[num2].get_transform().get_position().x;
            &this.m_CameraPos.z = &this.m_Units[num2].get_transform().get_position().z;
            this.m_CameraNextPos = this.m_CameraPos;
        Label_02DD:
            this.UpdateCameraPosition();
            return;
        }

        public bool IsReady
        {
            get
            {
                return this.m_Ready;
            }
        }

        private BattleMap CurrentMap
        {
            get
            {
                return (((this.m_Map == null) || (this.m_Map.Count <= 0)) ? null : this.m_Map[0]);
            }
        }

        public static MultiPlayVersusReady Instance
        {
            get
            {
                return m_Instance;
            }
        }

        [CompilerGenerated]
        private sealed class <GetDisplayHeight>c__AnonStorey367
        {
            internal Unit unit;

            public <GetDisplayHeight>c__AnonStorey367()
            {
                base..ctor();
                return;
            }

            internal bool <>m__371(TacticsUnitController ctrl)
            {
                return (ctrl.Unit == this.unit);
            }
        }

        [CompilerGenerated]
        private sealed class <LoadSceneAsync>c__Iterator127 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal MyPhoton <pt>__0;
            internal SceneRequest <req>__1;
            internal List<Unit> <units>__2;
            internal List<Unit>.Enumerator <$s_977>__3;
            internal Unit <unit>__4;
            internal List<Unit>.Enumerator <$s_978>__5;
            internal Unit <unit>__6;
            internal GameObject <go>__7;
            internal TacticsUnitController <preview>__8;
            internal List<TacticsUnitController>.Enumerator <$s_979>__9;
            internal TacticsUnitController <tuc>__10;
            internal int <job_idx>__11;
            internal LoadRequest <loadReq>__12;
            internal QuestAssets <assets>__13;
            internal int <idx>__14;
            internal List<TrickSetting> <trick_settings>__15;
            internal List<TrickSetting>.Enumerator <$s_980>__16;
            internal TrickSetting <trick_setting>__17;
            internal int <i>__18;
            internal int $PC;
            internal object $current;
            internal MultiPlayVersusReady <>f__this;

            public <LoadSceneAsync>c__Iterator127()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                uint num;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_007A;

                    case 1:
                        goto Label_007A;

                    case 2:
                        goto Label_007A;

                    case 3:
                        goto Label_007A;

                    case 4:
                        goto Label_007A;

                    case 5:
                        goto Label_007A;

                    case 6:
                        goto Label_007A;

                    case 7:
                        goto Label_0049;

                    case 8:
                        goto Label_0049;

                    case 9:
                        goto Label_0049;

                    case 10:
                        goto Label_0064;

                    case 11:
                        goto Label_007A;
                }
                goto Label_007A;
            Label_0049:
                try
                {
                    goto Label_005F;
                }
                finally
                {
                Label_004E:
                    ((List<Unit>.Enumerator) this.<$s_978>__5).Dispose();
                }
            Label_005F:
                goto Label_007A;
            Label_0064:
                try
                {
                    goto Label_007A;
                }
                finally
                {
                Label_0069:
                    ((List<TacticsUnitController>.Enumerator) this.<$s_979>__9).Dispose();
                }
            Label_007A:
                return;
            }

            public unsafe bool MoveNext()
            {
                Type[] typeArray1;
                uint num;
                bool flag;
                bool flag2;
                num = this.$PC;
                this.$PC = -1;
                flag = 0;
                switch (num)
                {
                    case 0:
                        goto Label_004B;

                    case 1:
                        goto Label_0087;

                    case 2:
                        goto Label_00C2;

                    case 3:
                        goto Label_0136;

                    case 4:
                        goto Label_016E;

                    case 5:
                        goto Label_01A1;

                    case 6:
                        goto Label_02BA;

                    case 7:
                        goto Label_02D8;

                    case 8:
                        goto Label_02D8;

                    case 9:
                        goto Label_02D8;

                    case 10:
                        goto Label_0491;

                    case 11:
                        goto Label_05C1;
                }
                goto Label_085C;
            Label_004B:
                this.<pt>__0 = PunMonoSingleton<MyPhoton>.Instance;
                DownloadUtility.DownloadQuestBase(this.<>f__this.m_CurrentQuest);
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_0087;
            Label_0074:
                this.$current = null;
                this.$PC = 1;
                goto Label_085E;
            Label_0087:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0074;
                }
                DownloadUtility.DownloadQuestMaps(this.<>f__this.m_CurrentQuest);
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_00C2;
            Label_00AF:
                this.$current = null;
                this.$PC = 2;
                goto Label_085E;
            Label_00C2:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_00AF;
                }
                this.<>f__this.InitMap();
                this.<>f__this.InitCamera();
                TacticsSceneSettings.AutoDeactivateScene = 0;
                SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.<>f__this.OnLoadScene));
                this.<req>__1 = AssetManager.LoadSceneAsync(this.<>f__this.CurrentMap.MapSceneName, 1);
                goto Label_0136;
            Label_011F:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_085E;
            Label_0136:
                if (this.<req>__1.canBeActivated == null)
                {
                    goto Label_011F;
                }
                this.<req>__1.ActivateScene();
                goto Label_016E;
            Label_0157:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 4;
                goto Label_085E;
            Label_016E:
                if (this.<req>__1.isDone == null)
                {
                    goto Label_0157;
                }
                this.<req>__1.ActivateScene();
                this.$current = new WaitForEndOfFrame();
                this.$PC = 5;
                goto Label_085E;
            Label_01A1:
                this.<units>__2 = null;
                if (this.<>f__this.TowerMode == null)
                {
                    goto Label_01CE;
                }
                this.<units>__2 = this.<>f__this.LoadMultiTower();
                goto Label_0235;
            Label_01CE:
                if (this.<>f__this.RankMatchMode == null)
                {
                    goto Label_01F4;
                }
                this.<units>__2 = this.<>f__this.LoadRankMatchParty();
                goto Label_0235;
            Label_01F4:
                if (this.<>f__this.DraftMode == null)
                {
                    goto Label_0224;
                }
                VersusDraftList.VersusDraftPartyPlaces = new List<int>();
                this.<units>__2 = this.<>f__this.LoadVersusDraftParty();
                goto Label_0235;
            Label_0224:
                this.<units>__2 = this.<>f__this.LoadVersusParty();
            Label_0235:
                this.<$s_977>__3 = this.<units>__2.GetEnumerator();
            Label_0246:
                try
                {
                    goto Label_0269;
                Label_024B:
                    this.<unit>__4 = &this.<$s_977>__3.Current;
                    DownloadUtility.DownloadUnit(this.<unit>__4, 0, 0);
                Label_0269:
                    if (&this.<$s_977>__3.MoveNext() != null)
                    {
                        goto Label_024B;
                    }
                    goto Label_028F;
                }
                finally
                {
                Label_027E:
                    ((List<Unit>.Enumerator) this.<$s_977>__3).Dispose();
                }
            Label_028F:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_02C4;
                }
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_02BA;
            Label_02A7:
                this.$current = null;
                this.$PC = 6;
                goto Label_085E;
            Label_02BA:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_02A7;
                }
            Label_02C4:
                this.<$s_978>__5 = this.<units>__2.GetEnumerator();
                num = -3;
            Label_02D8:
                try
                {
                    switch ((num - 7))
                    {
                        case 0:
                            goto Label_03B2;

                        case 1:
                            goto Label_0412;

                        case 2:
                            goto Label_044E;
                    }
                    goto Label_044E;
                Label_02F1:
                    this.<unit>__6 = &this.<$s_978>__5.Current;
                    if (this.<unit>__6.IsEntry == null)
                    {
                        goto Label_044E;
                    }
                    if (this.<unit>__6.IsSub == null)
                    {
                        goto Label_0327;
                    }
                    goto Label_044E;
                Label_0327:
                    typeArray1 = new Type[] { typeof(TacticsUnitController) };
                    this.<go>__7 = new GameObject(this.<unit>__6.UnitData.UnitID, typeArray1);
                    this.<preview>__8 = this.<go>__7.GetComponent<TacticsUnitController>();
                    this.<preview>__8.SetupUnit(this.<unit>__6);
                    this.<preview>__8.KeepUnitHidden = 1;
                    this.<>f__this.CalcPosition(this.<preview>__8);
                    goto Label_03B2;
                Label_0399:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 7;
                    flag = 1;
                    goto Label_085E;
                Label_03B2:
                    if (this.<preview>__8.IsLoading != null)
                    {
                        goto Label_0399;
                    }
                    if ((this.<>f__this.m_SceneRoot != null) == null)
                    {
                        goto Label_03F9;
                    }
                    this.<go>__7.get_transform().SetParent(this.<>f__this.m_SceneRoot.get_transform(), 0);
                Label_03F9:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 8;
                    flag = 1;
                    goto Label_085E;
                Label_0412:
                    this.<preview>__8.SetVisible(1);
                    this.<>f__this.m_Units.Add(this.<preview>__8);
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 9;
                    flag = 1;
                    goto Label_085E;
                Label_044E:
                    if (&this.<$s_978>__5.MoveNext() != null)
                    {
                        goto Label_02F1;
                    }
                    goto Label_0478;
                }
                finally
                {
                Label_0463:
                    if (flag == null)
                    {
                        goto Label_0467;
                    }
                Label_0467:
                    ((List<Unit>.Enumerator) this.<$s_978>__5).Dispose();
                }
            Label_0478:
                this.<$s_979>__9 = this.<>f__this.m_Units.GetEnumerator();
                num = -3;
            Label_0491:
                try
                {
                    switch ((num - 10))
                    {
                        case 0:
                            goto Label_050B;
                    }
                    goto Label_0560;
                Label_04A3:
                    this.<tuc>__10 = &this.<$s_979>__9.Current;
                    if (this.<tuc>__10.Unit.IsBreakObj != null)
                    {
                        goto Label_04CE;
                    }
                    goto Label_0560;
                Label_04CE:
                    this.<job_idx>__11 = 1;
                    goto Label_0529;
                Label_04DA:
                    if (this.<tuc>__10.LoadAddModels(this.<job_idx>__11) == null)
                    {
                        goto Label_051B;
                    }
                    goto Label_050B;
                Label_04F5:
                    this.$current = null;
                    this.$PC = 10;
                    flag = 1;
                    goto Label_085E;
                Label_050B:
                    if (this.<tuc>__10.IsLoading != null)
                    {
                        goto Label_04F5;
                    }
                Label_051B:
                    this.<job_idx>__11 += 1;
                Label_0529:
                    if (this.<job_idx>__11 < this.<tuc>__10.Unit.UnitParam.search)
                    {
                        goto Label_04DA;
                    }
                    this.<tuc>__10.SetVisible(1);
                    this.<tuc>__10.ReflectDispModel();
                Label_0560:
                    if (&this.<$s_979>__9.MoveNext() != null)
                    {
                        goto Label_04A3;
                    }
                    goto Label_058A;
                }
                finally
                {
                Label_0575:
                    if (flag == null)
                    {
                        goto Label_0579;
                    }
                Label_0579:
                    ((List<TacticsUnitController>.Enumerator) this.<$s_979>__9).Dispose();
                }
            Label_058A:
                this.<loadReq>__12 = AssetManager.LoadAsync("UI/QuestAssets", typeof(QuestAssets));
                goto Label_05C1;
            Label_05A9:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 11;
                goto Label_085E;
            Label_05C1:
                if (this.<loadReq>__12.isDone == null)
                {
                    goto Label_05A9;
                }
                this.<assets>__13 = (QuestAssets) this.<loadReq>__12.asset;
                this.<>f__this.m_TrickMarkerObj = this.<assets>__13.TrickMarker;
                this.<>f__this.m_TrickMarkers.Clear();
                if (this.<assets>__13.TrickMarkerGos == null)
                {
                    goto Label_06B2;
                }
                if (this.<assets>__13.TrickMarkerIds == null)
                {
                    goto Label_06B2;
                }
                if (((int) this.<assets>__13.TrickMarkerGos.Length) != ((int) this.<assets>__13.TrickMarkerIds.Length))
                {
                    goto Label_06B2;
                }
                this.<idx>__14 = 0;
                goto Label_069A;
            Label_0658:
                this.<>f__this.m_TrickMarkers.Add(this.<assets>__13.TrickMarkerIds[this.<idx>__14], this.<assets>__13.TrickMarkerGos[this.<idx>__14]);
                this.<idx>__14 += 1;
            Label_069A:
                if (this.<idx>__14 < ((int) this.<assets>__13.TrickMarkerGos.Length))
                {
                    goto Label_0658;
                }
            Label_06B2:
                this.<trick_settings>__15 = this.<>f__this.CurrentMap.TrickSettings;
                this.<$s_980>__16 = this.<trick_settings>__15.GetEnumerator();
            Label_06D9:
                try
                {
                    goto Label_072F;
                Label_06DE:
                    this.<trick_setting>__17 = &this.<$s_980>__16.Current;
                    TrickData.EntryEffect(this.<trick_setting>__17.mId, this.<trick_setting>__17.mGx, this.<trick_setting>__17.mGy, this.<trick_setting>__17.mTag, null, 0, 1, 1);
                Label_072F:
                    if (&this.<$s_980>__16.MoveNext() != null)
                    {
                        goto Label_06DE;
                    }
                    goto Label_0755;
                }
                finally
                {
                Label_0744:
                    ((List<TrickSetting>.Enumerator) this.<$s_980>__16).Dispose();
                }
            Label_0755:
                if (this.<trick_settings>__15.Count == null)
                {
                    goto Label_0790;
                }
                TrickData.UpdateMarker(this.<>f__this.m_SceneRoot.get_transform(), this.<>f__this.m_TrickMarkers, this.<>f__this.m_TrickMarkerObj);
            Label_0790:
                SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.<>f__this.OnLoadScene));
                this.<>f__this.UpdateGridColor();
                this.<i>__18 = 0;
                goto Label_0811;
            Label_07BD:
                if (this.<>f__this.m_Units[this.<i>__18].Unit.OwnerPlayerIndex != this.<pt>__0.MyPlayerIndex)
                {
                    goto Label_0803;
                }
                this.<>f__this.m_SelectParty = this.<i>__18;
                goto Label_082C;
            Label_0803:
                this.<i>__18 += 1;
            Label_0811:
                if (this.<i>__18 < this.<>f__this.m_Units.Count)
                {
                    goto Label_07BD;
                }
            Label_082C:
                this.<>f__this.UpdateUnitStatus(0, 0);
                FlowNode_TriggerLocalEvent.TriggerLocalEvent(this.<>f__this, "FINISH_LOAD");
                this.<>f__this.m_Ready = 1;
                this.$PC = -1;
            Label_085C:
                return 0;
            Label_085E:
                return 1;
                return flag2;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <LoadUnit>c__Iterator128 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<Unit> <newUnits>__0;
            internal List<Unit>.Enumerator <$s_981>__1;
            internal Unit <unit>__2;
            internal List<Unit>.Enumerator <$s_982>__3;
            internal Unit <unit>__4;
            internal TacticsUnitController <controller>__5;
            internal GameObject <go>__6;
            internal TacticsUnitController <preview>__7;
            internal List<TacticsUnitController> <allUnit>__8;
            internal List<TacticsUnitController>.Enumerator <$s_983>__9;
            internal TacticsUnitController <ctrl>__10;
            internal Unit <find>__11;
            internal int $PC;
            internal object $current;
            internal MultiPlayVersusReady <>f__this;
            private static Predicate<TacticsUnitController> <>f__am$cacheF;

            public <LoadUnit>c__Iterator128()
            {
                base..ctor();
                return;
            }

            internal bool <>m__372(TacticsUnitController ctrl)
            {
                return ((ctrl.Unit.OwnerPlayerIndex != this.<unit>__4.OwnerPlayerIndex) ? 0 : (ctrl.Unit.UnitName == this.<unit>__4.UnitName));
            }

            private static bool <>m__373(TacticsUnitController ctrl)
            {
                return (ctrl.Unit.OwnerPlayerIndex > 0);
            }

            internal bool <>m__374(Unit unit)
            {
                return ((unit.OwnerPlayerIndex != this.<ctrl>__10.Unit.OwnerPlayerIndex) ? 0 : (unit.UnitName == this.<ctrl>__10.Unit.UnitName));
            }

            [DebuggerHidden]
            public void Dispose()
            {
                uint num;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0047;

                    case 1:
                        goto Label_0047;

                    case 2:
                        goto Label_0031;

                    case 3:
                        goto Label_0031;

                    case 4:
                        goto Label_0031;

                    case 5:
                        goto Label_0047;
                }
                goto Label_0047;
            Label_0031:
                try
                {
                    goto Label_0047;
                }
                finally
                {
                Label_0036:
                    ((List<Unit>.Enumerator) this.<$s_982>__3).Dispose();
                }
            Label_0047:
                return;
            }

            public unsafe bool MoveNext()
            {
                Type[] typeArray1;
                uint num;
                bool flag;
                bool flag2;
                num = this.$PC;
                this.$PC = -1;
                flag = 0;
                switch (num)
                {
                    case 0:
                        goto Label_0033;

                    case 1:
                        goto Label_00D5;

                    case 2:
                        goto Label_00F8;

                    case 3:
                        goto Label_00F8;

                    case 4:
                        goto Label_00F8;

                    case 5:
                        goto Label_03F7;
                }
                goto Label_03FE;
            Label_0033:
                this.<newUnits>__0 = null;
                this.<newUnits>__0 = this.<>f__this.LoadMultiTower();
                this.<$s_981>__1 = this.<newUnits>__0.GetEnumerator();
            Label_005C:
                try
                {
                    goto Label_007F;
                Label_0061:
                    this.<unit>__2 = &this.<$s_981>__1.Current;
                    DownloadUtility.DownloadUnit(this.<unit>__2, 0, 0);
                Label_007F:
                    if (&this.<$s_981>__1.MoveNext() != null)
                    {
                        goto Label_0061;
                    }
                    goto Label_00A5;
                }
                finally
                {
                Label_0094:
                    ((List<Unit>.Enumerator) this.<$s_981>__1).Dispose();
                }
            Label_00A5:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_00E4;
                }
                ProgressWindow.OpenGenericDownloadWindow();
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_00D5;
            Label_00C2:
                this.$current = null;
                this.$PC = 1;
                goto Label_0400;
            Label_00D5:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_00C2;
                }
                ProgressWindow.Close();
            Label_00E4:
                this.<$s_982>__3 = this.<newUnits>__0.GetEnumerator();
                num = -3;
            Label_00F8:
                try
                {
                    switch ((num - 2))
                    {
                        case 0:
                            goto Label_0216;

                        case 1:
                            goto Label_0276;

                        case 2:
                            goto Label_02B1;
                    }
                    goto Label_02B1;
                Label_0111:
                    this.<unit>__4 = &this.<$s_982>__3.Current;
                    if (this.<unit>__4.OwnerPlayerIndex <= 0)
                    {
                        goto Label_02B1;
                    }
                    this.<controller>__5 = this.<>f__this.m_Units.Find(new Predicate<TacticsUnitController>(this.<>m__372));
                    if ((this.<controller>__5 == null) == null)
                    {
                        goto Label_02B1;
                    }
                    if (this.<unit>__4.IsEntry == null)
                    {
                        goto Label_02B1;
                    }
                    if (this.<unit>__4.IsSub == null)
                    {
                        goto Label_018B;
                    }
                    goto Label_02B1;
                Label_018B:
                    typeArray1 = new Type[] { typeof(TacticsUnitController) };
                    this.<go>__6 = new GameObject(this.<unit>__4.UnitData.UnitID, typeArray1);
                    this.<preview>__7 = this.<go>__6.GetComponent<TacticsUnitController>();
                    this.<preview>__7.SetupUnit(this.<unit>__4);
                    this.<preview>__7.KeepUnitHidden = 1;
                    this.<>f__this.CalcPosition(this.<preview>__7);
                    goto Label_0216;
                Label_01FD:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 2;
                    flag = 1;
                    goto Label_0400;
                Label_0216:
                    if (this.<preview>__7.IsLoading != null)
                    {
                        goto Label_01FD;
                    }
                    if ((this.<>f__this.m_SceneRoot != null) == null)
                    {
                        goto Label_025D;
                    }
                    this.<go>__6.get_transform().SetParent(this.<>f__this.m_SceneRoot.get_transform(), 0);
                Label_025D:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 3;
                    flag = 1;
                    goto Label_0400;
                Label_0276:
                    this.<preview>__7.SetVisible(1);
                    this.<>f__this.m_Units.Add(this.<preview>__7);
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 4;
                    flag = 1;
                    goto Label_0400;
                Label_02B1:
                    if (&this.<$s_982>__3.MoveNext() != null)
                    {
                        goto Label_0111;
                    }
                    goto Label_02DB;
                }
                finally
                {
                Label_02C6:
                    if (flag == null)
                    {
                        goto Label_02CA;
                    }
                Label_02CA:
                    ((List<Unit>.Enumerator) this.<$s_982>__3).Dispose();
                }
            Label_02DB:
                this.<>f__this.UpdateMarker(null);
                this.<>f__this.m_SelectParty = -1;
                if (<>f__am$cacheF != null)
                {
                    goto Label_0317;
                }
                <>f__am$cacheF = new Predicate<TacticsUnitController>(MultiPlayVersusReady.<LoadUnit>c__Iterator128.<>m__373);
            Label_0317:
                this.<allUnit>__8 = this.<>f__this.m_Units.FindAll(<>f__am$cacheF);
                if (this.<allUnit>__8 == null)
                {
                    goto Label_03CD;
                }
                this.<$s_983>__9 = this.<allUnit>__8.GetEnumerator();
            Label_0342:
                try
                {
                    goto Label_03A7;
                Label_0347:
                    this.<ctrl>__10 = &this.<$s_983>__9.Current;
                    this.<find>__11 = this.<newUnits>__0.Find(new Predicate<Unit>(this.<>m__374));
                    if (this.<find>__11 != null)
                    {
                        goto Label_03A7;
                    }
                    this.<>f__this.m_Units.Remove(this.<ctrl>__10);
                    Object.Destroy(this.<ctrl>__10.get_gameObject());
                Label_03A7:
                    if (&this.<$s_983>__9.MoveNext() != null)
                    {
                        goto Label_0347;
                    }
                    goto Label_03CD;
                }
                finally
                {
                Label_03BC:
                    ((List<TacticsUnitController>.Enumerator) this.<$s_983>__9).Dispose();
                }
            Label_03CD:
                this.<>f__this.UpdateGridColor();
                this.<>f__this.m_SyncLoad = 0;
                this.$current = null;
                this.$PC = 5;
                goto Label_0400;
            Label_03F7:
                this.$PC = -1;
            Label_03FE:
                return 0;
            Label_0400:
                return 1;
                return flag2;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <SendPlacementInfo>c__AnonStorey365
        {
            internal MyPhoton pt;
            internal JSON_MyPhotonPlayerParam param;

            public <SendPlacementInfo>c__AnonStorey365()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <SendPlacementInfo>c__AnonStorey366
        {
            internal int i;
            internal MultiPlayVersusReady.<SendPlacementInfo>c__AnonStorey365 <>f__ref$869;

            public <SendPlacementInfo>c__AnonStorey366()
            {
                base..ctor();
                return;
            }

            internal bool <>m__370(TacticsUnitController data)
            {
                return ((data.Unit.OwnerPlayerIndex != this.<>f__ref$869.pt.MyPlayerIndex) ? 0 : (data.UnitData.UnitParam.iname == this.<>f__ref$869.param.units[this.i].unit.UnitParam.iname));
            }
        }

        [CompilerGenerated]
        private sealed class <SyncRoomPlayer>c__AnonStorey363
        {
            internal UnitData unitData;
            internal MultiPlayVersusReady.<SyncRoomPlayer>c__AnonStorey364 <>f__ref$868;

            public <SyncRoomPlayer>c__AnonStorey363()
            {
                base..ctor();
                return;
            }

            internal bool <>m__36F(TacticsUnitController data)
            {
                return (((data.Unit.UnitParam.iname == this.unitData.UnitParam.iname) == null) ? 0 : (data.Unit.OwnerPlayerIndex == this.<>f__ref$868.param.playerIndex));
            }
        }

        [CompilerGenerated]
        private sealed class <SyncRoomPlayer>c__AnonStorey364
        {
            internal JSON_MyPhotonPlayerParam param;

            public <SyncRoomPlayer>c__AnonStorey364()
            {
                base..ctor();
                return;
            }
        }
    }
}

