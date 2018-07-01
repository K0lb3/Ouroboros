// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftReady
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Finish Place", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Finish Place", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "Finish Scene", FlowNode.PinTypes.Output, 3)]
  public class VersusDraftReady : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_PIN_FINISH_PLACE = 1;
    private const int OUTPUT_PIN_FINISH_PLACE = 2;
    private const int OUTPUT_PIN_FINISH_SCENE = 3;
    [SerializeField]
    private Text mTimerText;
    private StateMachine<VersusDraftReady> mStateMachine;
    private float mPlaceSec;
    private List<VersusDraftReady.VersusReadyMessageData> mMessageDataList;

    public VersusDraftReady()
    {
      base.\u002Ector();
    }

    private Text TimerText
    {
      get
      {
        return this.mTimerText;
      }
    }

    private void Start()
    {
      this.mPlaceSec = (float) (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DraftPlaceSeconds;
      this.mStateMachine = new StateMachine<VersusDraftReady>(this);
      this.GotoState<VersusDraftReady.State_UnitPlacing>();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.GotoState<VersusDraftReady.State_UpdatePlayer>();
    }

    private void Update()
    {
      this.UpdatePhotonMessage();
      if (this.mStateMachine == null || this.mStateMachine.StateName == "NULL")
        return;
      this.mStateMachine.Update();
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
          VersusDraftReady.VersusReadyMessageData buffer = (VersusDraftReady.VersusReadyMessageData) null;
          if (GameUtility.Binary2Object<VersusDraftReady.VersusReadyMessageData>(out buffer, myEvent.binary) && buffer != null)
            this.mMessageDataList.Add(buffer);
        }
      }
    }

    public void GotoState<StateType>() where StateType : State<VersusDraftReady>, new()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.GotoState<StateType>();
    }

    public class VersusReadyMessageData
    {
      public int pidx;
      public int pid;
      public int h;
    }

    public enum VersusReadyMessageDataHeader
    {
      NOP,
      COMPLETE,
      NUM,
    }

    private class State_UnitPlacing : State<VersusDraftReady>
    {
      private bool mEnable;
      private float mTimer;
      private MultiPlayVersusReady mMPVR;

      public override void Begin(VersusDraftReady self)
      {
        this.mTimer = 0.0f;
        this.mEnable = true;
        this.mMPVR = (MultiPlayVersusReady) ((Component) self).GetComponent<MultiPlayVersusReady>();
      }

      public override void Update(VersusDraftReady self)
      {
        if (!this.mMPVR.IsReady || !this.mEnable)
          return;
        this.mTimer += Time.get_unscaledDeltaTime();
        self.TimerText.set_text(((int) ((double) self.mPlaceSec - (double) this.mTimer)).ToString());
        if ((double) this.mTimer < (double) self.mPlaceSec)
          return;
        this.mEnable = false;
        MultiPlayVersusReady component = (MultiPlayVersusReady) ((Component) self).GetComponent<MultiPlayVersusReady>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) component.GoButton, (UnityEngine.Object) null) || component.GoButton.get_onClick() == null)
          return;
        ((UnityEvent) component.GoButton.get_onClick()).Invoke();
      }
    }

    private class State_UpdatePlayer : State<VersusDraftReady>
    {
      public override void Begin(VersusDraftReady self)
      {
        MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyPlayer myPlayer = instance1.GetMyPlayer();
        int num1 = myPlayer != null ? myPlayer.playerID : 0;
        int myPlayerIndex = instance1.MyPlayerIndex;
        GameManager instance2 = MonoSingleton<GameManager>.Instance;
        JSON_MyPhotonPlayerParam photonPlayerParam = new JSON_MyPhotonPlayerParam();
        PlayerData player = instance2.Player;
        photonPlayerParam.playerID = num1;
        photonPlayerParam.playerIndex = myPlayerIndex;
        photonPlayerParam.playerName = player.Name;
        photonPlayerParam.playerLevel = player.Lv;
        photonPlayerParam.FUID = player.FUID;
        photonPlayerParam.UID = MonoSingleton<GameManager>.Instance.DeviceId;
        photonPlayerParam.award = player.SelectedAward;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        List<JSON_MyPhotonPlayerParam.UnitDataElem> unitDataElemList = new List<JSON_MyPhotonPlayerParam.UnitDataElem>();
        for (int index = 0; index < VersusDraftList.VersusDraftPartyUnits.Count; ++index)
        {
          UnitData versusDraftPartyUnit = VersusDraftList.VersusDraftPartyUnits[index];
          if (versusDraftPartyUnit != null)
          {
            unitDataElemList.Add(new JSON_MyPhotonPlayerParam.UnitDataElem()
            {
              slotID = num2,
              place = VersusDraftList.VersusDraftPartyPlaces.Count <= index ? index : VersusDraftList.VersusDraftPartyPlaces[index],
              unit = versusDraftPartyUnit
            });
            num3 = num3 + (int) versusDraftPartyUnit.Status.param.atk + (int) versusDraftPartyUnit.Status.param.mag;
            num4 = num4 + (int) ((double) (int) versusDraftPartyUnit.Status.param.hp * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.HP) + (int) ((double) (int) versusDraftPartyUnit.Status.param.atk * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Attack) + (int) ((double) (int) versusDraftPartyUnit.Status.param.def * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Defense) + (int) ((double) (int) versusDraftPartyUnit.Status.param.mag * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagAttack) + (int) ((double) (int) versusDraftPartyUnit.Status.param.mnd * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagDefense) + (int) ((double) (int) versusDraftPartyUnit.Status.param.dex * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Dex) + (int) ((double) (int) versusDraftPartyUnit.Status.param.spd * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Speed) + (int) ((double) (int) versusDraftPartyUnit.Status.param.cri * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Critical) + (int) ((double) (int) versusDraftPartyUnit.Status.param.luk * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Luck) + (int) ((double) versusDraftPartyUnit.GetCombination() * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Combo) + (int) ((double) (int) versusDraftPartyUnit.Status.param.mov * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Move) + (int) ((double) (int) versusDraftPartyUnit.Status.param.jmp * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Jump);
            ++num2;
          }
        }
        photonPlayerParam.units = unitDataElemList.ToArray();
        photonPlayerParam.totalAtk = num3;
        photonPlayerParam.totalStatus = Mathf.FloorToInt((float) (num4 / unitDataElemList.Count));
        photonPlayerParam.rankpoint = player.VERSUS_POINT;
        photonPlayerParam.draft_id = VersusDraftList.DraftID;
        instance1.SetMyPlayerParam(photonPlayerParam.Serialize());
        byte[] msg = GameUtility.Object2Binary<VersusDraftReady.VersusReadyMessageData>(new VersusDraftReady.VersusReadyMessageData()
        {
          h = 1,
          pidx = myPlayerIndex,
          pid = num1
        });
        instance1.SendRoomMessageBinary(true, msg, MyPhoton.SEND_TYPE.Normal, false);
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 2);
      }

      public override void Update(VersusDraftReady self)
      {
        List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
        if (roomPlayerList == null || roomPlayerList.Count < 2)
          self.GotoState<VersusDraftReady.State_RoomUpdate>();
        for (int index = 0; index < self.mMessageDataList.Count; ++index)
        {
          if (self.mMessageDataList[index].h == 1)
            self.GotoState<VersusDraftReady.State_RoomUpdate>();
        }
      }
    }

    private class State_RoomUpdate : State<VersusDraftReady>
    {
      private const int PARTY_SLOT_COUNT = 3;

      public override void Begin(VersusDraftReady self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        for (int index1 = 0; index1 < roomPlayerList.Count; ++index1)
        {
          JSON_MyPhotonPlayerParam param = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index1].json);
          int index2 = myPlayersStarted.FindIndex((Predicate<JSON_MyPhotonPlayerParam>) (sp => sp.playerID == param.playerID));
          if (index2 > -1)
            myPlayersStarted[index2] = param;
        }
        if (roomPlayerList.Count < 2)
        {
          MyPhoton.MyPlayer player = instance.GetMyPlayer();
          JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted.Find((Predicate<JSON_MyPhotonPlayerParam>) (sp => sp.playerID != player.playerID));
          int num1 = 0;
          int num2 = 0;
          int num3 = 0;
          List<JSON_MyPhotonPlayerParam.UnitDataElem> unitDataElemList = new List<JSON_MyPhotonPlayerParam.UnitDataElem>();
          for (int index = 0; index < VersusDraftList.VersusDraftUnitDataListEnemy.Count && index < 3; ++index)
          {
            UnitData unitData = VersusDraftList.VersusDraftUnitDataListEnemy[index];
            if (unitData != null)
            {
              unitDataElemList.Add(new JSON_MyPhotonPlayerParam.UnitDataElem()
              {
                slotID = num1,
                place = index,
                unit = unitData
              });
              num2 = num2 + (int) unitData.Status.param.atk + (int) unitData.Status.param.mag;
              num3 = num3 + (int) ((double) (int) unitData.Status.param.hp * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.HP) + (int) ((double) (int) unitData.Status.param.atk * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Attack) + (int) ((double) (int) unitData.Status.param.def * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Defense) + (int) ((double) (int) unitData.Status.param.mag * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagAttack) + (int) ((double) (int) unitData.Status.param.mnd * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.MagDefense) + (int) ((double) (int) unitData.Status.param.dex * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Dex) + (int) ((double) (int) unitData.Status.param.spd * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Speed) + (int) ((double) (int) unitData.Status.param.cri * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Critical) + (int) ((double) (int) unitData.Status.param.luk * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Luck) + (int) ((double) unitData.GetCombination() * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Combo) + (int) ((double) (int) unitData.Status.param.mov * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Move) + (int) ((double) (int) unitData.Status.param.jmp * (double) MonoSingleton<GameManager>.Instance.MasterParam.mStatusCoefficient.Jump);
              ++num1;
            }
          }
          photonPlayerParam.units = unitDataElemList.ToArray();
          photonPlayerParam.totalAtk = num2;
          photonPlayerParam.totalStatus = Mathf.FloorToInt((float) (num3 / unitDataElemList.Count));
          photonPlayerParam.draft_id = -1;
        }
        if (instance.IsOldestPlayer())
          instance.UpdateRoomParam("started", (object) new FlowNode_StartMultiPlay.PlayerList()
          {
            players = myPlayersStarted.ToArray()
          }.Serialize());
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 3);
      }
    }
  }
}
