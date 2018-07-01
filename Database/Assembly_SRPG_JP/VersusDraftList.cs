// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(130, "Unit Select SE", FlowNode.PinTypes.Output, 30)]
  [FlowNode.Pin(111, "Turn Enemy", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(110, "Turn Player", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(3, "Random Decide Unit", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(2, "Decide Unit", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(102, "Unit Selecting", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "Start Drafting", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(101, "Initialize Complete", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(120, "Finish Draft", FlowNode.PinTypes.Output, 20)]
  public class VersusDraftList : MonoBehaviour, IFlowInterface
  {
    public static List<VersusDraftUnitParam> VersusDraftUnitList = new List<VersusDraftUnitParam>();
    public static List<UnitData> VersusDraftPartyUnits = new List<UnitData>();
    public static List<int> VersusDraftPartyPlaces = new List<int>();
    private const int DRAFT_UNIT_LIST_COL_MAX = 6;
    private const int SELECTING_UNIT_COUNT = 6;
    private const float SINGLE_ENEMY_TIME = 5f;
    private const int INPUT_PIN_INITIALIZE = 1;
    private const int INPUT_PIN_DECIDE_UNIT = 2;
    private const int INPUT_PIN_DECIDE_UNIT_RANDOM = 3;
    private const int INPUT_PIN_START_DRAFTING = 4;
    private const int OUTPUT_PIN_INITIALIZE = 101;
    private const int OUTPUT_PIN_UNIT_SELECTING = 102;
    private const int OUTPUT_PIN_TURN_PLAYER = 110;
    private const int OUTPUT_PIN_TURN_ENEMY = 111;
    private const int OUTPUT_PIN_FINISH_DRAFT = 120;
    private const int OUTPUT_PIN_UNIT_SELECT_SE = 130;
    public static bool VersusDraftTurnOwn;
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

    public VersusDraftList()
    {
      base.\u002Ector();
    }

    public int SelectableUnitCount
    {
      get
      {
        if (this.mTurn < 0 || this.SELECTABLE_UNIT_COUNT_OF_TURN.Length <= this.mTurn)
          return 0;
        return this.SELECTABLE_UNIT_COUNT_OF_TURN[this.mTurn];
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDraftUnitItem, (UnityEngine.Object) null))
        ((Component) this.mDraftUnitItem).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedUnitItem, (UnityEngine.Object) null))
        ((Component) this.mSelectedUnitItem).get_gameObject().SetActive(false);
      this.mDraftSec = (float) (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DraftSelectSeconds;
      this.mTurn = -1;
      VersusDraftUnit.CurrentSelectCursors = new List<VersusDraftUnit>();
      for (int index = 0; index < 3; ++index)
      {
        VersusDraftUnit.CurrentSelectCursors.Add((VersusDraftUnit) null);
        VersusDraftUnit.CurrentSelectCursors.Add((VersusDraftUnit) null);
        VersusDraftUnit.CurrentSelectCursors.Add((VersusDraftUnit) null);
      }
      VersusDraftUnit.VersusDraftList = this;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Initialize();
          break;
        case 2:
          this.DecideUnit();
          break;
        case 3:
          this.StartCoroutine(this.RandomSelecting());
          break;
        case 4:
          this.StartDrafting();
          break;
      }
    }

    [DebuggerHidden]
    private IEnumerator RandomSelecting()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusDraftList.\u003CRandomSelecting\u003Ec__Iterator17D()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void Update()
    {
      if (this.mTurn < 0 || this.SELECTABLE_UNIT_COUNT_OF_TURN.Length <= this.mTurn)
        return;
      this.UpdateTimer();
      this.UpdatePhotonMessage();
      this.UpdateSingleMode();
    }

    private void UpdateTimer()
    {
      if (!VersusDraftList.VersusDraftTurnOwn || int.Parse(FlowNode_Variable.Get("START_PLAYER_TURN")) < 1)
        return;
      this.mPlayerTimer += Time.get_unscaledDeltaTime();
      this.mTimerText.set_text(((int) ((double) this.mDraftSec - (double) this.mPlayerTimer)).ToString());
      if ((double) this.mPlayerTimer < (double) this.mDraftSec)
        return;
      if (this.mRandomSelecting)
        this.StopCoroutine(this.RandomSelecting());
      this.DecideUnitRandom(true, true);
      this.DecideUnit();
    }

    private void UpdatePhotonMessage()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      List<MyPhoton.MyEvent> events = instance.GetEvents();
      if (events == null)
        return;
      while (events.Count > 0)
      {
        MyPhoton.MyEvent myEvent = events[0];
        events.RemoveAt(0);
        if (myEvent.code == MyPhoton.SEND_TYPE.Normal && myEvent.binary != null)
        {
          VersusDraftList.VersusDraftMessageData buffer = (VersusDraftList.VersusDraftMessageData) null;
          if (GameUtility.Binary2Object<VersusDraftList.VersusDraftMessageData>(out buffer, myEvent.binary) && buffer != null)
          {
            switch (buffer.h)
            {
              case 1:
                VersusDraftUnit.ResetSelectUnit();
                if (buffer.uidx0 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx0)
                  this.mVersusDraftUnitList[buffer.uidx0].SelectUnit(false);
                if (buffer.uidx1 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx1)
                  this.mVersusDraftUnitList[buffer.uidx1].SelectUnit(false);
                if (buffer.uidx2 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx2)
                {
                  this.mVersusDraftUnitList[buffer.uidx2].SelectUnit(false);
                  continue;
                }
                continue;
              case 2:
                if (buffer.uidx0 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx0)
                {
                  this.mVersusDraftUnitList[buffer.uidx0].DecideUnit(false);
                  ++this.mEnemyUnitIndex;
                }
                if (buffer.uidx1 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx1)
                {
                  this.mVersusDraftUnitList[buffer.uidx1].DecideUnit(false);
                  ++this.mEnemyUnitIndex;
                }
                if (buffer.uidx2 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx2)
                {
                  this.mVersusDraftUnitList[buffer.uidx2].DecideUnit(false);
                  ++this.mEnemyUnitIndex;
                  continue;
                }
                continue;
              case 3:
                if (!this.ChangeTurn(true))
                {
                  for (int index = 0; index < this.SELECTABLE_UNIT_COUNT_OF_TURN[this.mTurn]; ++index)
                    this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex + index].Selecting();
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
    }

    private void UpdateSingleMode()
    {
      if (!this.mSingleMode)
      {
        List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
        if (roomPlayerList != null && roomPlayerList.Count >= 2)
          return;
        this.mSingleMode = true;
      }
      else
      {
        if (VersusDraftList.VersusDraftTurnOwn)
          return;
        this.mEnemyTimer += Time.get_unscaledDeltaTime();
        if ((double) this.mEnemyTimer < 5.0)
          return;
        this.mEnemyTimer = 0.0f;
        for (int index = 0; index < this.SelectableUnitCount; ++index)
        {
          VersusDraftUnit randomUnit = this.GetRandomUnit();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) randomUnit, (UnityEngine.Object) null))
            return;
          randomUnit.SelectUnit(false);
          randomUnit.DecideUnit(false);
          ++this.mEnemyUnitIndex;
        }
        this.ChangeTurn(true);
      }
    }

    private void Initialize()
    {
      this.mSingleMode = false;
      this.mVersusDraftUnitList = new List<VersusDraftUnit>();
      for (int index1 = 0; index1 < VersusDraftList.VersusDraftUnitList.Count && (this.mDraftUnitTransforms != null && this.mDraftUnitTransforms.Length > 0); ++index1)
      {
        Json_Unit jsonUnit = VersusDraftList.VersusDraftUnitList[index1].GetJson_Unit();
        if (jsonUnit != null)
        {
          UnitData unit = new UnitData();
          unit.Deserialize(jsonUnit);
          if (unit != null)
          {
            VersusDraftUnit versusDraftUnit = (VersusDraftUnit) UnityEngine.Object.Instantiate<VersusDraftUnit>((M0) this.mDraftUnitItem);
            this.mVersusDraftUnitList.Add(versusDraftUnit);
            Transform draftUnitTransform = this.mDraftUnitTransforms[0];
            int index2 = 0;
            for (int index3 = 0; index3 < this.DRAFT_UNIT_LIST_COLS.Length; ++index3)
            {
              if (index1 < this.DRAFT_UNIT_LIST_COLS[index3])
              {
                index2 = index3;
                break;
              }
            }
            if (this.mDraftUnitTransforms.Length > index2)
              draftUnitTransform = this.mDraftUnitTransforms[index2];
            versusDraftUnit.SetUp(unit, draftUnitTransform, VersusDraftList.VersusDraftUnitList[index1].IsHidden);
          }
        }
      }
      this.mVersusDraftSelectedUnit = new List<VersusDraftSelectedUnit>();
      for (int index = 0; index < 6 && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSelectedUnitTransform, (UnityEngine.Object) null); ++index)
      {
        VersusDraftSelectedUnit draftSelectedUnit = (VersusDraftSelectedUnit) UnityEngine.Object.Instantiate<VersusDraftSelectedUnit>((M0) this.mSelectedUnitItem);
        this.mVersusDraftSelectedUnit.Add(draftSelectedUnit);
        ((Component) draftSelectedUnit).get_transform().SetParent(this.mSelectedUnitTransform, false);
        ((Component) draftSelectedUnit).get_gameObject().SetActive(true);
        draftSelectedUnit.Initialize();
      }
      this.mRandomSelecting = false;
      VersusDraftList.VersusDraftUnitDataListPlayer = new List<UnitData>();
      VersusDraftList.VersusDraftUnitDataListEnemy = new List<UnitData>();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void StartDrafting()
    {
      this.mSelectingUnitIndex = 0;
      this.mEnemyUnitIndex = 0;
      this.mTurn = -1;
      if (VersusDraftList.VersusDraftTurnOwn)
      {
        this.ChangeTurn(true);
        this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Selecting();
      }
      else
        this.ChangeTurn(false);
    }

    private void DecideUnitRandom(bool notice = false, bool spaceOnly = false)
    {
      if (!VersusDraftList.VersusDraftTurnOwn)
        return;
      int num = this.SelectableUnitCount;
      if (spaceOnly)
        num = this.SelectableUnitCount - VersusDraftUnit.CurrentSelectCursors.FindAll((Predicate<VersusDraftUnit>) (u => UnityEngine.Object.op_Inequality((UnityEngine.Object) u, (UnityEngine.Object) null))).Count;
      for (int index = 0; index < num; ++index)
      {
        VersusDraftUnit randomUnit = this.GetRandomUnit();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) randomUnit, (UnityEngine.Object) null))
          return;
        randomUnit.SelectUnit(true);
      }
      if (!notice)
        return;
      this.SelectUnit();
    }

    private VersusDraftUnit GetRandomUnit()
    {
      List<VersusDraftUnit> all = this.mVersusDraftUnitList.FindAll((Predicate<VersusDraftUnit>) (unit =>
      {
        if (!VersusDraftUnit.CurrentSelectCursors.Contains(unit))
          return !unit.IsSelected;
        return false;
      }));
      if (all == null || all.Count <= 0)
        return (VersusDraftUnit) null;
      int index = Random.Range(0, all.Count);
      if (index < 0)
        index = 0;
      else if (index >= all.Count)
        index = all.Count - 1;
      return all[index];
    }

    public void SetUnit(UnitData unit, int offset)
    {
      this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex + offset].SetUnit(unit);
    }

    private void DecideUnit()
    {
      if (this.mRandomSelecting || !VersusDraftList.VersusDraftTurnOwn || this.mSelectingUnitIndex >= 6)
        return;
      VersusDraftList.VersusDraftMessageData mess = new VersusDraftList.VersusDraftMessageData();
      mess.h = 2;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[0], (UnityEngine.Object) null))
      {
        VersusDraftUnit currentSelectCursor = VersusDraftUnit.CurrentSelectCursors[0];
        int num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[0]);
        currentSelectCursor.DecideUnit(true);
        mess.uidx0 = num;
        this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(currentSelectCursor.UnitData);
        ++this.mSelectingUnitIndex;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[1], (UnityEngine.Object) null))
      {
        VersusDraftUnit currentSelectCursor = VersusDraftUnit.CurrentSelectCursors[1];
        int num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[1]);
        currentSelectCursor.DecideUnit(true);
        mess.uidx1 = num;
        this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(currentSelectCursor.UnitData);
        ++this.mSelectingUnitIndex;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[2], (UnityEngine.Object) null))
      {
        VersusDraftUnit currentSelectCursor = VersusDraftUnit.CurrentSelectCursors[2];
        int num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[2]);
        currentSelectCursor.DecideUnit(true);
        mess.uidx2 = num;
        this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(currentSelectCursor.UnitData);
        ++this.mSelectingUnitIndex;
      }
      this.SendRoomMessage(mess, false);
      this.FinishTurn();
    }

    private void FinishTurn()
    {
      if (!VersusDraftList.VersusDraftTurnOwn)
        return;
      this.ChangeTurn(false);
      this.SendRoomMessage(new VersusDraftList.VersusDraftMessageData()
      {
        h = 3
      }, true);
    }

    public void SelectUnit()
    {
      VersusDraftList.VersusDraftMessageData mess = new VersusDraftList.VersusDraftMessageData();
      mess.h = 1;
      int num = 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[0], (UnityEngine.Object) null))
      {
        mess.uidx0 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[0]);
        ++num;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[1], (UnityEngine.Object) null))
      {
        mess.uidx1 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[1]);
        ++num;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[2], (UnityEngine.Object) null))
      {
        mess.uidx2 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[2]);
        ++num;
      }
      this.SendRoomMessage(mess, false);
      if (num < this.SelectableUnitCount)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void SendRoomMessage(VersusDraftList.VersusDraftMessageData mess, bool immediate = false)
    {
      if (mess == null)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
      int myPlayerIndex = instance.MyPlayerIndex;
      int num = myPlayer != null ? myPlayer.playerID : 0;
      mess.pidx = myPlayerIndex;
      mess.pid = num;
      byte[] msg = GameUtility.Object2Binary<VersusDraftList.VersusDraftMessageData>(mess);
      instance.SendRoomMessageBinary(true, msg, MyPhoton.SEND_TYPE.Normal, false);
      if (!immediate)
        return;
      instance.SendFlush();
    }

    private bool ChangeTurn(bool isPlayer = true)
    {
      ++this.mTurn;
      if (this.SELECTABLE_UNIT_COUNT_OF_TURN.Length <= this.mTurn)
      {
        this.StartCoroutine(this.DownloadUnitImage());
        return true;
      }
      VersusDraftList.VersusDraftTurnOwn = isPlayer;
      this.mTimerGO.SetActive(isPlayer);
      ((Component) this.mPlayerText).get_gameObject().SetActive(isPlayer);
      ((Component) this.mEnemyText).get_gameObject().SetActive(!isPlayer);
      VersusDraftUnit.ResetSelectUnit();
      string str1;
      string str2;
      if (isPlayer)
      {
        this.mPlayerTimer = 0.0f;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
        str1 = LocalizedText.Get("sys.DRAFT_TURN_PLAYER");
        str2 = (this.mSelectingUnitIndex + 1).ToString();
        for (int index = 1; index < this.SelectableUnitCount; ++index)
          str2 = str2 + "," + (this.mSelectingUnitIndex + 1 + index).ToString();
        this.mTimerText.set_text(((int) this.mDraftSec).ToString());
        this.mPlayerText.set_text(str1 + string.Format(LocalizedText.Get("sys.DRAFT_UNIT_SELECT_MESSAGE_PLAYER"), (object) str2));
      }
      else
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
        str1 = LocalizedText.Get("sys.DRAFT_TURN_ENEMY");
        str2 = (this.mEnemyUnitIndex + 1).ToString();
        for (int index = 1; index < this.SelectableUnitCount; ++index)
          str2 = str2 + "," + (this.mEnemyUnitIndex + 1 + index).ToString();
        this.mEnemyText.set_text(str1 + string.Format(LocalizedText.Get("sys.DRAFT_UNIT_SELECT_MESSAGE_ENEMY"), (object) str2));
      }
      this.mTurnChangeUserText.set_text(str1);
      this.mTurnChangeMessage.set_text(string.Format(LocalizedText.Get("sys.DRAFT_CHANGE_TURN_MESSAGE"), (object) str2));
      return false;
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusDraftList.\u003CDownloadUnitImage\u003Ec__Iterator17E()
      {
        \u003C\u003Ef__this = this
      };
    }

    public class VersusDraftMessageData
    {
      public int uidx0 = -1;
      public int uidx1 = -1;
      public int uidx2 = -1;
      public int pidx;
      public int pid;
      public int h;
    }

    public enum VersusDraftMessageDataHeader
    {
      NOP,
      CURSOR,
      DECIDE,
      FINISH_TURN,
      NUM,
    }
  }
}
