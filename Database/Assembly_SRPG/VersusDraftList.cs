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
    using UnityEngine.UI;

    [Pin(130, "Unit Select SE", 1, 30), Pin(0x6f, "Turn Enemy", 1, 11), Pin(110, "Turn Player", 1, 10), Pin(3, "Random Decide Unit", 0, 6), Pin(2, "Decide Unit", 0, 5), Pin(1, "Initialize", 0, 1), Pin(0x66, "Unit Selecting", 1, 3), Pin(4, "Start Drafting", 0, 4), Pin(0x65, "Initialize Complete", 1, 2), Pin(120, "Finish Draft", 1, 20)]
    public class VersusDraftList : MonoBehaviour, IFlowInterface
    {
        private const int DRAFT_UNIT_LIST_COL_MAX = 6;
        private const int SELECTING_UNIT_COUNT = 6;
        private const float SINGLE_ENEMY_TIME = 5f;
        private const int INPUT_PIN_INITIALIZE = 1;
        private const int INPUT_PIN_DECIDE_UNIT = 2;
        private const int INPUT_PIN_DECIDE_UNIT_RANDOM = 3;
        private const int INPUT_PIN_START_DRAFTING = 4;
        private const int OUTPUT_PIN_INITIALIZE = 0x65;
        private const int OUTPUT_PIN_UNIT_SELECTING = 0x66;
        private const int OUTPUT_PIN_TURN_PLAYER = 110;
        private const int OUTPUT_PIN_TURN_ENEMY = 0x6f;
        private const int OUTPUT_PIN_FINISH_DRAFT = 120;
        private const int OUTPUT_PIN_UNIT_SELECT_SE = 130;
        public static bool VersusDraftTurnOwn;
        public static List<VersusDraftUnitParam> VersusDraftUnitList;
        public static List<UnitData> VersusDraftPartyUnits;
        public static List<int> VersusDraftPartyPlaces;
        public static List<UnitData> VersusDraftUnitDataListPlayer;
        public static List<UnitData> VersusDraftUnitDataListEnemy;
        public static int DraftID;
        private readonly int[] DRAFT_UNIT_LIST_COLS;
        private readonly int[] SELECTABLE_UNIT_COUNT_OF_TURN;
        [SerializeField]
        private Transform[] mDraftUnitTransforms;
        [SerializeField]
        private VersusDraftUnit mDraftUnitItem;
        [SerializeField]
        private Transform mSelectedUnitTransform;
        [SerializeField]
        private VersusDraftSelectedUnit mSelectedUnitItem;
        [SerializeField]
        private Text mPlayerText;
        [SerializeField]
        private Text mEnemyText;
        [SerializeField]
        private GameObject mTimerGO;
        [SerializeField]
        private Text mTimerText;
        [SerializeField]
        private Text mTurnChangeUserText;
        [SerializeField]
        private Text mTurnChangeMessage;
        private bool mSingleMode;
        private bool mRandomSelecting;
        private float mDraftSec;
        private int mTurn;
        private float mEnemyTimer;
        private float mPlayerTimer;
        private List<VersusDraftUnit> mVersusDraftUnitList;
        private List<VersusDraftSelectedUnit> mVersusDraftSelectedUnit;
        private int mSelectingUnitIndex;
        private int mEnemyUnitIndex;
        [CompilerGenerated]
        private static Predicate<VersusDraftUnit> <>f__am$cache1D;
        [CompilerGenerated]
        private static Predicate<VersusDraftUnit> <>f__am$cache1E;

        static VersusDraftList()
        {
            VersusDraftUnitList = new List<VersusDraftUnitParam>();
            VersusDraftPartyUnits = new List<UnitData>();
            VersusDraftPartyPlaces = new List<int>();
            return;
        }

        public VersusDraftList()
        {
            this.DRAFT_UNIT_LIST_COLS = new int[] { 6, 12, 14, 0x10 };
            this.SELECTABLE_UNIT_COUNT_OF_TURN = new int[] { 1, 2, 2, 2, 2, 2, 1 };
            this.mTurn = -1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <DecideUnitRandom>m__492(VersusDraftUnit u)
        {
            return (u != null);
        }

        [CompilerGenerated]
        private static bool <GetRandomUnit>m__493(VersusDraftUnit unit)
        {
            return ((VersusDraftUnit.CurrentSelectCursors.Contains(unit) != null) ? 0 : (unit.IsSelected == 0));
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 1))
            {
                case 0:
                    goto Label_001F;

                case 1:
                    goto Label_002A;

                case 2:
                    goto Label_0035;

                case 3:
                    goto Label_0047;
            }
            goto Label_0052;
        Label_001F:
            this.Initialize();
            goto Label_0052;
        Label_002A:
            this.DecideUnit();
            goto Label_0052;
        Label_0035:
            base.StartCoroutine(this.RandomSelecting());
            goto Label_0052;
        Label_0047:
            this.StartDrafting();
        Label_0052:
            return;
        }

        private unsafe bool ChangeTurn(bool isPlayer)
        {
            string str;
            string str2;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            this.mTurn += 1;
            if (((int) this.SELECTABLE_UNIT_COUNT_OF_TURN.Length) > this.mTurn)
            {
                goto Label_0030;
            }
            base.StartCoroutine(this.DownloadUnitImage());
            return 1;
        Label_0030:
            VersusDraftTurnOwn = isPlayer;
            this.mTimerGO.SetActive(isPlayer);
            this.mPlayerText.get_gameObject().SetActive(isPlayer);
            this.mEnemyText.get_gameObject().SetActive(isPlayer == 0);
            VersusDraftUnit.ResetSelectUnit();
            if (isPlayer == null)
            {
                goto Label_0119;
            }
            this.mPlayerTimer = 0f;
            FlowNode_GameObject.ActivateOutputLinks(this, 110);
            str = LocalizedText.Get("sys.DRAFT_TURN_PLAYER");
            num3 = this.mSelectingUnitIndex + 1;
            str2 = &num3.ToString();
            num = 1;
            goto Label_00CC;
        Label_00A9:
            num4 = (this.mSelectingUnitIndex + 1) + num;
            str2 = str2 + "," + &num4.ToString();
            num += 1;
        Label_00CC:
            if (num < this.SelectableUnitCount)
            {
                goto Label_00A9;
            }
            num5 = (int) this.mDraftSec;
            this.mTimerText.set_text(&num5.ToString());
            this.mPlayerText.set_text(str + string.Format(LocalizedText.Get("sys.DRAFT_UNIT_SELECT_MESSAGE_PLAYER"), str2));
            goto Label_0195;
        Label_0119:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x6f);
            str = LocalizedText.Get("sys.DRAFT_TURN_ENEMY");
            num6 = this.mEnemyUnitIndex + 1;
            str2 = &num6.ToString();
            num2 = 1;
            goto Label_0168;
        Label_0145:
            num7 = (this.mEnemyUnitIndex + 1) + num2;
            str2 = str2 + "," + &num7.ToString();
            num2 += 1;
        Label_0168:
            if (num2 < this.SelectableUnitCount)
            {
                goto Label_0145;
            }
            this.mEnemyText.set_text(str + string.Format(LocalizedText.Get("sys.DRAFT_UNIT_SELECT_MESSAGE_ENEMY"), str2));
        Label_0195:
            this.mTurnChangeUserText.set_text(str);
            this.mTurnChangeMessage.set_text(string.Format(LocalizedText.Get("sys.DRAFT_CHANGE_TURN_MESSAGE"), str2));
            return 0;
        }

        private void DecideUnit()
        {
            VersusDraftUnit unit;
            int num;
            VersusDraftMessageData data;
            if (this.mRandomSelecting == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (VersusDraftTurnOwn != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (this.mSelectingUnitIndex < 6)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            data = new VersusDraftMessageData();
            data.h = 2;
            if ((VersusDraftUnit.CurrentSelectCursors[0] != null) == null)
            {
                goto Label_00A2;
            }
            unit = VersusDraftUnit.CurrentSelectCursors[0];
            num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[0]);
            unit.DecideUnit(1);
            data.uidx0 = num;
            this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(unit.UnitData);
            this.mSelectingUnitIndex += 1;
        Label_00A2:
            if ((VersusDraftUnit.CurrentSelectCursors[1] != null) == null)
            {
                goto Label_0113;
            }
            unit = VersusDraftUnit.CurrentSelectCursors[1];
            num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[1]);
            unit.DecideUnit(1);
            data.uidx1 = num;
            this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(unit.UnitData);
            this.mSelectingUnitIndex += 1;
        Label_0113:
            if ((VersusDraftUnit.CurrentSelectCursors[2] != null) == null)
            {
                goto Label_0184;
            }
            unit = VersusDraftUnit.CurrentSelectCursors[2];
            num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[2]);
            unit.DecideUnit(1);
            data.uidx2 = num;
            this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(unit.UnitData);
            this.mSelectingUnitIndex += 1;
        Label_0184:
            this.SendRoomMessage(data, 0);
            this.FinishTurn();
            return;
        }

        private void DecideUnitRandom(bool notice, bool spaceOnly)
        {
            int num;
            int num2;
            int num3;
            VersusDraftUnit unit;
            if (VersusDraftTurnOwn != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            num = this.SelectableUnitCount;
            if (spaceOnly == null)
            {
                goto Label_004E;
            }
            if (<>f__am$cache1D != null)
            {
                goto Label_0035;
            }
            <>f__am$cache1D = new Predicate<VersusDraftUnit>(VersusDraftList.<DecideUnitRandom>m__492);
        Label_0035:
            num2 = VersusDraftUnit.CurrentSelectCursors.FindAll(<>f__am$cache1D).Count;
            num = this.SelectableUnitCount - num2;
        Label_004E:
            num3 = 0;
            goto Label_0074;
        Label_0055:
            unit = this.GetRandomUnit();
            if ((unit == null) == null)
            {
                goto Label_0069;
            }
            return;
        Label_0069:
            unit.SelectUnit(1);
            num3 += 1;
        Label_0074:
            if (num3 < num)
            {
                goto Label_0055;
            }
            if (notice == null)
            {
                goto Label_0087;
            }
            this.SelectUnit();
        Label_0087:
            return;
        }

        [DebuggerHidden]
        private IEnumerator DownloadUnitImage()
        {
            <DownloadUnitImage>c__Iterator17E iteratore;
            iteratore = new <DownloadUnitImage>c__Iterator17E();
            iteratore.<>f__this = this;
            return iteratore;
        }

        private void FinishTurn()
        {
            VersusDraftMessageData data;
            if (VersusDraftTurnOwn != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            this.ChangeTurn(0);
            data = new VersusDraftMessageData();
            data.h = 3;
            this.SendRoomMessage(data, 1);
            return;
        }

        private VersusDraftUnit GetRandomUnit()
        {
            List<VersusDraftUnit> list;
            int num;
            if (<>f__am$cache1E != null)
            {
                goto Label_001E;
            }
            <>f__am$cache1E = new Predicate<VersusDraftUnit>(VersusDraftList.<GetRandomUnit>m__493);
        Label_001E:
            list = this.mVersusDraftUnitList.FindAll(<>f__am$cache1E);
            if (list == null)
            {
                goto Label_003B;
            }
            if (list.Count > 0)
            {
                goto Label_003D;
            }
        Label_003B:
            return null;
        Label_003D:
            num = Random.Range(0, list.Count);
            if (num >= 0)
            {
                goto Label_0058;
            }
            num = 0;
            goto Label_006D;
        Label_0058:
            if (num < list.Count)
            {
                goto Label_006D;
            }
            num = list.Count - 1;
        Label_006D:
            return list[num];
        }

        private void Initialize()
        {
            int num;
            Json_Unit unit;
            UnitData data;
            VersusDraftUnit unit2;
            Transform transform;
            int num2;
            int num3;
            int num4;
            VersusDraftSelectedUnit unit3;
            this.mSingleMode = 0;
            this.mVersusDraftUnitList = new List<VersusDraftUnit>();
            num = 0;
            goto Label_00FC;
        Label_0019:
            if (this.mDraftUnitTransforms == null)
            {
                goto Label_010C;
            }
            if (((int) this.mDraftUnitTransforms.Length) > 0)
            {
                goto Label_0037;
            }
            goto Label_010C;
        Label_0037:
            unit = VersusDraftUnitList[num].GetJson_Unit();
            if (unit != null)
            {
                goto Label_0053;
            }
            goto Label_00F8;
        Label_0053:
            data = new UnitData();
            data.Deserialize(unit);
            if (data != null)
            {
                goto Label_006B;
            }
            goto Label_00F8;
        Label_006B:
            unit2 = Object.Instantiate<VersusDraftUnit>(this.mDraftUnitItem);
            this.mVersusDraftUnitList.Add(unit2);
            transform = this.mDraftUnitTransforms[0];
            num2 = 0;
            num3 = 0;
            goto Label_00B6;
        Label_0098:
            if (num >= this.DRAFT_UNIT_LIST_COLS[num3])
            {
                goto Label_00B0;
            }
            num2 = num3;
            goto Label_00C5;
        Label_00B0:
            num3 += 1;
        Label_00B6:
            if (num3 < ((int) this.DRAFT_UNIT_LIST_COLS.Length))
            {
                goto Label_0098;
            }
        Label_00C5:
            if (((int) this.mDraftUnitTransforms.Length) <= num2)
            {
                goto Label_00DF;
            }
            transform = this.mDraftUnitTransforms[num2];
        Label_00DF:
            unit2.SetUp(data, transform, VersusDraftUnitList[num].IsHidden);
        Label_00F8:
            num += 1;
        Label_00FC:
            if (num < VersusDraftUnitList.Count)
            {
                goto Label_0019;
            }
        Label_010C:
            this.mVersusDraftSelectedUnit = new List<VersusDraftSelectedUnit>();
            num4 = 0;
            goto Label_017C;
        Label_011F:
            if ((this.mSelectedUnitTransform == null) == null)
            {
                goto Label_0135;
            }
            goto Label_0184;
        Label_0135:
            unit3 = Object.Instantiate<VersusDraftSelectedUnit>(this.mSelectedUnitItem);
            this.mVersusDraftSelectedUnit.Add(unit3);
            unit3.get_transform().SetParent(this.mSelectedUnitTransform, 0);
            unit3.get_gameObject().SetActive(1);
            unit3.Initialize();
            num4 += 1;
        Label_017C:
            if (num4 < 6)
            {
                goto Label_011F;
            }
        Label_0184:
            this.mRandomSelecting = 0;
            VersusDraftUnitDataListPlayer = new List<UnitData>();
            VersusDraftUnitDataListEnemy = new List<UnitData>();
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        [DebuggerHidden]
        private IEnumerator RandomSelecting()
        {
            <RandomSelecting>c__Iterator17D iteratord;
            iteratord = new <RandomSelecting>c__Iterator17D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        public void SelectUnit()
        {
            VersusDraftMessageData data;
            int num;
            data = new VersusDraftMessageData();
            data.h = 1;
            num = 0;
            if ((VersusDraftUnit.CurrentSelectCursors[0] != null) == null)
            {
                goto Label_0045;
            }
            data.uidx0 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[0]);
            num += 1;
        Label_0045:
            if ((VersusDraftUnit.CurrentSelectCursors[1] != null) == null)
            {
                goto Label_007B;
            }
            data.uidx1 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[1]);
            num += 1;
        Label_007B:
            if ((VersusDraftUnit.CurrentSelectCursors[2] != null) == null)
            {
                goto Label_00B1;
            }
            data.uidx2 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[2]);
            num += 1;
        Label_00B1:
            this.SendRoomMessage(data, 0);
            if (num >= this.SelectableUnitCount)
            {
                goto Label_00C6;
            }
            return;
        Label_00C6:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
            return;
        }

        private void SendRoomMessage(VersusDraftMessageData mess, bool immediate)
        {
            MyPhoton photon;
            MyPhoton.MyPlayer player;
            int num;
            int num2;
            byte[] buffer;
            if (mess != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            photon = PunMonoSingleton<MyPhoton>.Instance;
            player = photon.GetMyPlayer();
            num = photon.MyPlayerIndex;
            num2 = (player != null) ? player.playerID : 0;
            mess.pidx = num;
            mess.pid = num2;
            buffer = GameUtility.Object2Binary<VersusDraftMessageData>(mess);
            photon.SendRoomMessageBinary(1, buffer, 0, 0);
            if (immediate == null)
            {
                goto Label_005C;
            }
            photon.SendFlush();
        Label_005C:
            return;
        }

        public void SetUnit(UnitData unit, int offset)
        {
            this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex + offset].SetUnit(unit);
            return;
        }

        private void Start()
        {
            int num;
            if ((this.mDraftUnitItem != null) == null)
            {
                goto Label_0022;
            }
            this.mDraftUnitItem.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.mSelectedUnitItem != null) == null)
            {
                goto Label_0044;
            }
            this.mSelectedUnitItem.get_gameObject().SetActive(0);
        Label_0044:
            this.mDraftSec = (float) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DraftSelectSeconds;
            this.mTurn = -1;
            VersusDraftUnit.CurrentSelectCursors = new List<VersusDraftUnit>();
            num = 0;
            goto Label_00A1;
        Label_007C:
            VersusDraftUnit.CurrentSelectCursors.Add(null);
            VersusDraftUnit.CurrentSelectCursors.Add(null);
            VersusDraftUnit.CurrentSelectCursors.Add(null);
            num += 1;
        Label_00A1:
            if (num < 3)
            {
                goto Label_007C;
            }
            VersusDraftUnit.VersusDraftList = this;
            return;
        }

        public void StartDrafting()
        {
            this.mSelectingUnitIndex = 0;
            this.mEnemyUnitIndex = 0;
            this.mTurn = -1;
            if (VersusDraftTurnOwn == null)
            {
                goto Label_0042;
            }
            this.ChangeTurn(1);
            this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Selecting();
            goto Label_004A;
        Label_0042:
            this.ChangeTurn(0);
        Label_004A:
            return;
        }

        private void Update()
        {
            if (this.mTurn < 0)
            {
                goto Label_001F;
            }
            if (((int) this.SELECTABLE_UNIT_COUNT_OF_TURN.Length) > this.mTurn)
            {
                goto Label_0020;
            }
        Label_001F:
            return;
        Label_0020:
            this.UpdateTimer();
            this.UpdatePhotonMessage();
            this.UpdateSingleMode();
            return;
        }

        private unsafe void UpdatePhotonMessage()
        {
            MyPhoton photon;
            List<MyPhoton.MyEvent> list;
            MyPhoton.MyEvent event2;
            VersusDraftMessageData data;
            int num;
            VersusDraftMessageDataHeader header;
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
                goto Label_0281;
            }
            goto Label_0275;
        Label_0025:
            event2 = list[0];
            list.RemoveAt(0);
            if (event2.code == null)
            {
                goto Label_0044;
            }
            goto Label_0275;
        Label_0044:
            if (event2.binary != null)
            {
                goto Label_0054;
            }
            goto Label_0275;
        Label_0054:
            data = null;
            if (GameUtility.Binary2Object<VersusDraftMessageData>(&data, event2.binary) != null)
            {
                goto Label_006D;
            }
            goto Label_0275;
        Label_006D:
            if (data != null)
            {
                goto Label_0078;
            }
            goto Label_0275;
        Label_0078:
            switch ((data.h - 1))
            {
                case 0:
                    goto Label_009A;

                case 1:
                    goto Label_014F;

                case 2:
                    goto Label_0229;
            }
            goto Label_0275;
        Label_009A:
            VersusDraftUnit.ResetSelectUnit();
            if (data.uidx0 < 0)
            {
                goto Label_00D8;
            }
            if (this.mVersusDraftUnitList.Count <= data.uidx0)
            {
                goto Label_00D8;
            }
            this.mVersusDraftUnitList[data.uidx0].SelectUnit(0);
        Label_00D8:
            if (data.uidx1 < 0)
            {
                goto Label_0111;
            }
            if (this.mVersusDraftUnitList.Count <= data.uidx1)
            {
                goto Label_0111;
            }
            this.mVersusDraftUnitList[data.uidx1].SelectUnit(0);
        Label_0111:
            if (data.uidx2 < 0)
            {
                goto Label_0275;
            }
            if (this.mVersusDraftUnitList.Count <= data.uidx2)
            {
                goto Label_0275;
            }
            this.mVersusDraftUnitList[data.uidx2].SelectUnit(0);
            goto Label_0275;
        Label_014F:
            if (data.uidx0 < 0)
            {
                goto Label_0196;
            }
            if (this.mVersusDraftUnitList.Count <= data.uidx0)
            {
                goto Label_0196;
            }
            this.mVersusDraftUnitList[data.uidx0].DecideUnit(0);
            this.mEnemyUnitIndex += 1;
        Label_0196:
            if (data.uidx1 < 0)
            {
                goto Label_01DD;
            }
            if (this.mVersusDraftUnitList.Count <= data.uidx1)
            {
                goto Label_01DD;
            }
            this.mVersusDraftUnitList[data.uidx1].DecideUnit(0);
            this.mEnemyUnitIndex += 1;
        Label_01DD:
            if (data.uidx2 < 0)
            {
                goto Label_0275;
            }
            if (this.mVersusDraftUnitList.Count <= data.uidx2)
            {
                goto Label_0275;
            }
            this.mVersusDraftUnitList[data.uidx2].DecideUnit(0);
            this.mEnemyUnitIndex += 1;
            goto Label_0275;
        Label_0229:
            if (this.ChangeTurn(1) != null)
            {
                goto Label_0275;
            }
            num = 0;
            goto Label_025C;
        Label_023D:
            this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex + num].Selecting();
            num += 1;
        Label_025C:
            if (num < this.SELECTABLE_UNIT_COUNT_OF_TURN[this.mTurn])
            {
                goto Label_023D;
            }
        Label_0275:
            if (list.Count > 0)
            {
                goto Label_0025;
            }
        Label_0281:
            return;
        }

        private void UpdateSingleMode()
        {
            MyPhoton photon;
            List<MyPhoton.MyPlayer> list;
            int num;
            VersusDraftUnit unit;
            if (this.mSingleMode != null)
            {
                goto Label_0036;
            }
            list = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
            if (list == null)
            {
                goto Label_002A;
            }
            if (list.Count >= 2)
            {
                goto Label_00BC;
            }
        Label_002A:
            this.mSingleMode = 1;
            goto Label_00BC;
        Label_0036:
            if (VersusDraftTurnOwn != null)
            {
                goto Label_00BC;
            }
            this.mEnemyTimer += Time.get_unscaledDeltaTime();
            if (this.mEnemyTimer < 5f)
            {
                goto Label_00BC;
            }
            this.mEnemyTimer = 0f;
            num = 0;
            goto Label_00A8;
        Label_0074:
            unit = this.GetRandomUnit();
            if ((unit == null) == null)
            {
                goto Label_0088;
            }
            return;
        Label_0088:
            unit.SelectUnit(0);
            unit.DecideUnit(0);
            this.mEnemyUnitIndex += 1;
            num += 1;
        Label_00A8:
            if (num < this.SelectableUnitCount)
            {
                goto Label_0074;
            }
            this.ChangeTurn(1);
        Label_00BC:
            return;
        }

        private unsafe void UpdateTimer()
        {
            int num;
            if (VersusDraftTurnOwn != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            if (int.Parse(FlowNode_Variable.Get("START_PLAYER_TURN")) >= 1)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            this.mPlayerTimer += Time.get_unscaledDeltaTime();
            num = (int) (this.mDraftSec - this.mPlayerTimer);
            this.mTimerText.set_text(&num.ToString());
            if (this.mPlayerTimer < this.mDraftSec)
            {
                goto Label_008A;
            }
            if (this.mRandomSelecting == null)
            {
                goto Label_007C;
            }
            base.StopCoroutine(this.RandomSelecting());
        Label_007C:
            this.DecideUnitRandom(1, 1);
            this.DecideUnit();
        Label_008A:
            return;
        }

        public int SelectableUnitCount
        {
            get
            {
            Label_001F:
                return (((this.mTurn >= 0) && (((int) this.SELECTABLE_UNIT_COUNT_OF_TURN.Length) > this.mTurn)) ? this.SELECTABLE_UNIT_COUNT_OF_TURN[this.mTurn] : 0);
            }
        }

        [CompilerGenerated]
        private sealed class <DownloadUnitImage>c__Iterator17E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal int $PC;
            internal object $current;
            internal VersusDraftList <>f__this;

            public <DownloadUnitImage>c__Iterator17E()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_01A3;
                }
                goto Label_01C6;
            Label_0021:
                this.<i>__0 = 0;
                goto Label_0163;
            Label_002D:
                AssetManager.PrepareAssets(AssetPath.UnitSkinImage(VersusDraftList.VersusDraftUnitDataListPlayer[this.<i>__0].UnitParam, VersusDraftList.VersusDraftUnitDataListPlayer[this.<i>__0].GetSelectedSkin(-1), VersusDraftList.VersusDraftUnitDataListPlayer[this.<i>__0].CurrentJobId));
                AssetManager.PrepareAssets(AssetPath.UnitSkinImage(VersusDraftList.VersusDraftUnitDataListEnemy[this.<i>__0].UnitParam, VersusDraftList.VersusDraftUnitDataListEnemy[this.<i>__0].GetSelectedSkin(-1), VersusDraftList.VersusDraftUnitDataListEnemy[this.<i>__0].CurrentJobId));
                AssetManager.PrepareAssets(AssetPath.UnitSkinImage2(VersusDraftList.VersusDraftUnitDataListPlayer[this.<i>__0].UnitParam, VersusDraftList.VersusDraftUnitDataListPlayer[this.<i>__0].GetSelectedSkin(-1), VersusDraftList.VersusDraftUnitDataListPlayer[this.<i>__0].CurrentJobId));
                AssetManager.PrepareAssets(AssetPath.UnitSkinImage2(VersusDraftList.VersusDraftUnitDataListEnemy[this.<i>__0].UnitParam, VersusDraftList.VersusDraftUnitDataListEnemy[this.<i>__0].GetSelectedSkin(-1), VersusDraftList.VersusDraftUnitDataListEnemy[this.<i>__0].CurrentJobId));
                this.<i>__0 += 1;
            Label_0163:
                if (this.<i>__0 < 6)
                {
                    goto Label_002D;
                }
                if (AssetDownloader.isDone != null)
                {
                    goto Label_01A3;
                }
                ProgressWindow.OpenGenericDownloadWindow();
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_01A3;
            Label_018C:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_01C8;
            Label_01A3:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_018C;
                }
                ProgressWindow.Close();
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 120);
                this.$PC = -1;
            Label_01C6:
                return 0;
            Label_01C8:
                return 1;
                return flag;
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
        private sealed class <RandomSelecting>c__Iterator17D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <interval>__0;
            internal int <i>__1;
            internal int $PC;
            internal object $current;
            internal VersusDraftList <>f__this;

            public <RandomSelecting>c__Iterator17D()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_00A1;
                }
                goto Label_00DC;
            Label_0021:
                this.<>f__this.mRandomSelecting = 1;
                this.<interval>__0 = 0f;
                this.<i>__1 = 0;
                goto Label_00AF;
            Label_0044:
                this.<interval>__0 += Time.get_unscaledDeltaTime();
                if (this.<interval>__0 <= 0.05f)
                {
                    goto Label_008E;
                }
                this.<interval>__0 = 0f;
                this.<>f__this.DecideUnitRandom(0, 0);
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 130);
            Label_008E:
                this.$current = null;
                this.$PC = 1;
                goto Label_00DE;
            Label_00A1:
                this.<i>__1 += 1;
            Label_00AF:
                if (this.<i>__1 < 30)
                {
                    goto Label_0044;
                }
                this.<>f__this.DecideUnitRandom(1, 0);
                this.<>f__this.mRandomSelecting = 0;
                this.$PC = -1;
            Label_00DC:
                return 0;
            Label_00DE:
                return 1;
                return flag;
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

        public class VersusDraftMessageData
        {
            public int pidx;
            public int pid;
            public int h;
            public int uidx0;
            public int uidx1;
            public int uidx2;

            public VersusDraftMessageData()
            {
                this.uidx0 = -1;
                this.uidx1 = -1;
                this.uidx2 = -1;
                base..ctor();
                return;
            }
        }

        public enum VersusDraftMessageDataHeader
        {
            NOP,
            CURSOR,
            DECIDE,
            FINISH_TURN,
            NUM
        }
    }
}

