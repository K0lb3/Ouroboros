// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayJoinRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayJoinRoom", 32741)]
  [FlowNode.Pin(101, "CreateRoom", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(102, "JoinRoom", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(103, "CreateOrJoinLINE", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(200, "VersusCreateRoom", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "VersusJoinRoom", FlowNode.PinTypes.Input, 201)]
  [FlowNode.Pin(202, "VersusTowerJoinRoom", FlowNode.PinTypes.Input, 202)]
  [FlowNode.Pin(300, "MTCreateRoom", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(301, "MTJoinRoom", FlowNode.PinTypes.Input, 301)]
  [FlowNode.Pin(302, "MTResumeJoinRoom", FlowNode.PinTypes.Input, 302)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(5, "FullMember", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(4, "IllegalQuest", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(3, "FailureLobby", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlayJoinRoom : FlowNode
  {
    private readonly byte VERSUS_PLAYER_MAX = 3;
    private StateMachine<FlowNode_MultiPlayJoinRoom> mStateMachine;
    private JSON_MyPhotonPlayerParam mJoinPlayerParam;

    private bool IsLINE { get; set; }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 101:
          DebugUtility.Log("Start Create Room");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_CreateRoom>();
          break;
        case 102:
          DebugUtility.Log("Start Join Room");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_JoinRoom>();
          break;
        case 103:
          DebugUtility.Log("Start Create/Join Room LINE");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = true;
          if (JSON_MyPhotonRoomParam.GetMyCreatorFUID().Equals(FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID))
          {
            DebugUtility.Log("Creating LINERoom iname:" + GlobalVars.SelectedQuestID + " type:" + (object) GlobalVars.SelectedMultiPlayRoomType + " creatorFUID:" + FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID);
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
            this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_CreateRoom>();
            break;
          }
          DebugUtility.Log("Joining LINERoom iname:" + GlobalVars.SelectedQuestID + " type:" + (object) GlobalVars.SelectedMultiPlayRoomType + " creatorFUID:" + FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID + " name:" + GlobalVars.SelectedMultiPlayRoomName);
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_JoinRoom>();
          break;
        case 200:
          DebugUtility.Log("Start Versus Create Room");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_VersusCreate>();
          break;
        case 201:
          DebugUtility.Log("Start Versus Join Room");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_VersusJoin>();
          break;
        case 202:
          DebugUtility.Log("Start Versus Rank Join Room");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_VersusTowerJoin>();
          break;
        case 300:
          DebugUtility.Log("Start MultiTower CreateRoom");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_TowerCreate>();
          break;
        case 301:
          DebugUtility.Log("Start MultiTower JoinRoom");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_TowerJoin>();
          break;
        case 302:
          DebugUtility.Log("Start MultiTower Resume JoinRoom");
          ((Behaviour) this).set_enabled(true);
          this.IsLINE = false;
          this.mStateMachine = new StateMachine<FlowNode_MultiPlayJoinRoom>(this);
          this.mStateMachine.GotoState<FlowNode_MultiPlayJoinRoom.State_ResumeTowerJoinRoom>();
          break;
      }
    }

    private void Update()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.Update();
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
      DebugUtility.Log("Create/Join Room Success.");
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.NOP)
        instance.Disconnect();
      this.ActivateOutputLinks(2);
      DebugUtility.Log("Create/Join Room Failure.");
    }

    private void FailureLobby()
    {
      ((Behaviour) this).set_enabled(false);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.LOBBY)
      {
        instance.Disconnect();
        this.ActivateOutputLinks(2);
      }
      else
        this.ActivateOutputLinks(3);
      DebugUtility.Log("Create/Join Room FailureLobby.");
    }

    private void IllegalQuest()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(4);
      DebugUtility.Log("Create/Join Room IllegalQuest.");
    }

    private void FailureFullMember()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(5);
      DebugUtility.Log("Join Room FullMember.");
    }

    public void GotoState<StateType>() where StateType : State<FlowNode_MultiPlayJoinRoom>, new()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.GotoState<StateType>();
    }

    private class State_CreateRoom : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest == null || !quest.IsMulti || ((int) quest.playerNum < 1 || (int) quest.unitNum < 1) || ((int) quest.unitNum > 9 || quest.map == null || quest.map.Count <= 0))
        {
          DebugUtility.Log("illegal iname:" + GlobalVars.SelectedQuestID);
          self.IllegalQuest();
        }
        else
        {
          DebugUtility.Log("CreateRoom quest:" + quest.iname + " desc:" + quest.name);
          self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
          if (self.mJoinPlayerParam == null)
          {
            self.FailureLobby();
          }
          else
          {
            JSON_MyPhotonRoomParam myPhotonRoomParam = new JSON_MyPhotonRoomParam();
            myPhotonRoomParam.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
            myPhotonRoomParam.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
            myPhotonRoomParam.creatorFUID = JSON_MyPhotonRoomParam.GetMyCreatorFUID();
            myPhotonRoomParam.roomid = GlobalVars.SelectedMultiPlayRoomID;
            myPhotonRoomParam.comment = GlobalVars.SelectedMultiPlayRoomComment;
            myPhotonRoomParam.passCode = GlobalVars.EditMultiPlayRoomPassCode;
            myPhotonRoomParam.iname = GlobalVars.SelectedQuestID;
            myPhotonRoomParam.type = (int) GlobalVars.SelectedMultiPlayRoomType;
            myPhotonRoomParam.isLINE = !self.IsLINE ? 0 : 1;
            myPhotonRoomParam.unitlv = !GlobalVars.SelectedMultiPlayLimit ? 0 : GlobalVars.MultiPlayJoinUnitLv;
            DebugUtility.Log("create isLINE:" + (object) myPhotonRoomParam.isLINE + " iname:" + myPhotonRoomParam.iname + " roomid:" + (object) myPhotonRoomParam.roomid + " appID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " token:" + GlobalVars.SelectedMultiPlayRoomName + " comment:" + myPhotonRoomParam.comment + " pass:" + myPhotonRoomParam.passCode + " type:" + (object) myPhotonRoomParam.type + " json:" + myPhotonRoomParam.Serialize());
            if (instance.CreateRoom((int) quest.playerNum, GlobalVars.SelectedMultiPlayRoomName, myPhotonRoomParam.Serialize(), self.mJoinPlayerParam.Serialize(), (string) null, -1, -1, (string) null, (string) null, -1, false))
              return;
            self.FailureLobby();
          }
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).get_enabled())
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]create room failed, back to lobby.");
            if (instance.LastError != MyPhoton.MyError.NOP)
            {
              DebugUtility.Log(instance.LastError.ToString());
              instance.ResetLastError();
            }
            self.Failure();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            if ((double) (Time.get_realtimeSinceStartup() - FlowNode_MultiPlayAPI.RoomMakeTime) > 25.0)
            {
              self.Failure();
              DebugUtility.Log("[PUN]create room too late, give up.");
              break;
            }
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]create room failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_JoinRoom : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomName))
        {
          self.FailureLobby();
        }
        else
        {
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          if (quest == null)
          {
            DebugUtility.Log("illegal iname:" + GlobalVars.SelectedQuestID);
            self.IllegalQuest();
          }
          else
          {
            self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
            if (self.mJoinPlayerParam == null)
            {
              self.FailureLobby();
            }
            else
            {
              DebugUtility.Log("Joining name:" + GlobalVars.SelectedMultiPlayRoomName + " pnum:" + (object) quest.playerNum + " unum:" + (object) quest.unitNum);
              if (instance.JoinRoom(GlobalVars.SelectedMultiPlayRoomName, self.mJoinPlayerParam.Serialize(), GlobalVars.ResumeMultiplayPlayerID != 0))
                return;
              DebugUtility.Log("error:" + (object) instance.LastError);
              self.FailureLobby();
            }
          }
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).get_enabled())
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]joining failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_DecidePlayerIndex : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.CurrentState != MyPhoton.MyState.ROOM)
        {
          self.Failure();
        }
        else
        {
          MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
          List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
          using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              MyPhoton.MyPlayer current = enumerator.Current;
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(current.json);
              if (current.playerID < myPlayer.playerID && photonPlayerParam.playerIndex <= 0)
              {
                DebugUtility.Log("[PUN]waiting for player index turn..." + (object) current.playerID + " me:" + (object) myPlayer.playerID);
                return;
              }
            }
          }
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom.maxPlayers == 0)
          {
            Debug.LogError((object) ("Invalid Room:" + currentRoom.name));
            PhotonNetwork.room.IsOpen = false;
            self.Failure();
          }
          else
          {
            bool[] flagArray = new bool[currentRoom.maxPlayers];
            for (int index = 0; index < flagArray.Length; ++index)
              flagArray[index] = false;
            using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                MyPhoton.MyPlayer current = enumerator.Current;
                JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(current.json);
                if (current.playerID < myPlayer.playerID && photonPlayerParam.playerIndex > 0)
                {
                  flagArray[photonPlayerParam.playerIndex - 1] = true;
                  DebugUtility.Log("[PUN]player index " + (object) photonPlayerParam.playerIndex + " is used. (room:" + (object) currentRoom.maxPlayers + ")");
                }
              }
            }
            for (int index = 0; index < currentRoom.maxPlayers; ++index)
            {
              if (!flagArray[index])
              {
                int num = index + 1;
                DebugUtility.Log("[PUN]empty player index found: " + (object) num);
                self.mJoinPlayerParam.playerID = myPlayer.playerID;
                self.mJoinPlayerParam.playerIndex = num;
                self.mJoinPlayerParam.UpdateMultiTowerPlacement(true);
                instance.SetMyPlayerParam(self.mJoinPlayerParam.Serialize());
                instance.MyPlayerIndex = num;
                self.Success();
                break;
              }
            }
          }
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_VersusCreate : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = new JSON_MyPhotonRoomParam();
          myPhotonRoomParam.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
          myPhotonRoomParam.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
          myPhotonRoomParam.creatorFUID = JSON_MyPhotonRoomParam.GetMyCreatorFUID();
          myPhotonRoomParam.roomid = GlobalVars.SelectedMultiPlayRoomID;
          myPhotonRoomParam.comment = GlobalVars.SelectedMultiPlayRoomComment;
          myPhotonRoomParam.passCode = GlobalVars.EditMultiPlayRoomPassCode;
          myPhotonRoomParam.iname = GlobalVars.SelectedQuestID;
          myPhotonRoomParam.type = (int) GlobalVars.SelectedMultiPlayRoomType;
          myPhotonRoomParam.isLINE = !self.IsLINE ? 0 : 1;
          int plv = -1;
          int floor = -1;
          int audienceMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AudienceMax;
          string uid = (string) null;
          string luid = (string) null;
          if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Tower)
          {
            plv = myPhotonRoomParam.creatorLV;
            floor = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
            uid = MonoSingleton<GameManager>.Instance.DeviceId;
            luid = MonoSingleton<GameManager>.Instance.VersusLastUid;
          }
          if (instance.CreateRoom((int) self.VERSUS_PLAYER_MAX, GlobalVars.SelectedMultiPlayRoomName, myPhotonRoomParam.Serialize(), self.mJoinPlayerParam.Serialize(), GlobalVars.MultiPlayVersusKey, floor, plv, luid, uid, audienceMax, false))
            return;
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).get_enabled())
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]create room failed, back to lobby.");
            if (instance.LastError != MyPhoton.MyError.NOP)
            {
              DebugUtility.Log(instance.LastError.ToString());
              instance.ResetLastError();
            }
            self.Failure();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            if ((double) (Time.get_realtimeSinceStartup() - FlowNode_MultiPlayAPI.RoomMakeTime) > 25.0)
            {
              self.Failure();
              DebugUtility.Log("[PUN]create room too late, give up.");
              break;
            }
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]create room failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_VersusJoin : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          if (instance.JoinRandomRoom(self.VERSUS_PLAYER_MAX, self.mJoinPlayerParam.Serialize(), GlobalVars.MultiPlayVersusKey, GlobalVars.SelectedMultiPlayRoomName, -1, -1))
            return;
          DebugUtility.Log("error:" + (object) instance.LastError);
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyState currentState = instance.CurrentState;
        if (!((Behaviour) self).get_enabled())
          return;
        switch (currentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            GlobalVars.SelectedMultiPlayRoomName = instance.GetCurrentRoom().name;
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_VersusTowerJoin : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          int lrange = -1;
          int frange = -1;
          int lv = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
          int versusTowerFloor = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
          string versusLastUid = MonoSingleton<GameManager>.Instance.VersusLastUid;
          string deviceId = MonoSingleton<GameManager>.Instance.DeviceId;
          MonoSingleton<GameManager>.Instance.GetRankMatchCondition(out lrange, out frange);
          if (instance.JoinRoomCheckParam(GlobalVars.MultiPlayVersusKey, self.mJoinPlayerParam.Serialize(), lrange, frange, lv, versusTowerFloor, versusLastUid, deviceId))
            return;
          DebugUtility.Log("error:" + (object) instance.LastError);
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyState currentState = instance.CurrentState;
        if (!((Behaviour) self).get_enabled())
          return;
        switch (currentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            GlobalVars.SelectedMultiPlayRoomName = instance.GetCurrentRoom().name;
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_TowerCreate : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = new JSON_MyPhotonRoomParam();
          myPhotonRoomParam.creatorName = MonoSingleton<GameManager>.Instance.Player.Name;
          myPhotonRoomParam.creatorLV = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
          myPhotonRoomParam.creatorFUID = JSON_MyPhotonRoomParam.GetMyCreatorFUID();
          myPhotonRoomParam.roomid = GlobalVars.SelectedMultiPlayRoomID;
          myPhotonRoomParam.comment = GlobalVars.SelectedMultiPlayRoomComment;
          myPhotonRoomParam.passCode = GlobalVars.EditMultiPlayRoomPassCode;
          myPhotonRoomParam.iname = GlobalVars.SelectedQuestID;
          myPhotonRoomParam.type = (int) GlobalVars.SelectedMultiPlayRoomType;
          myPhotonRoomParam.isLINE = !self.IsLINE ? 0 : 1;
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(quest.iname);
          int selectedMultiTowerFloor = GlobalVars.SelectedMultiTowerFloor;
          if (instance.CreateRoom((int) quest.playerNum, GlobalVars.SelectedMultiPlayRoomName, myPhotonRoomParam.Serialize(), self.mJoinPlayerParam.Serialize(), mtFloorParam.tower_id, selectedMultiTowerFloor, -1, (string) null, (string) null, -1, true))
            return;
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).get_enabled())
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]create room failed, back to lobby.");
            if (instance.LastError != MyPhoton.MyError.NOP)
            {
              DebugUtility.Log(instance.LastError.ToString());
              instance.ResetLastError();
            }
            self.Failure();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            if ((double) (Time.get_realtimeSinceStartup() - FlowNode_MultiPlayAPI.RoomMakeTime) > 25.0)
            {
              self.Failure();
              DebugUtility.Log("[PUN]create room too late, give up.");
              break;
            }
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]create room failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_TowerJoin : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
        self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
        if (self.mJoinPlayerParam == null)
        {
          self.FailureLobby();
        }
        else
        {
          GameManager instance2 = MonoSingleton<GameManager>.Instance;
          QuestParam quest = instance2.FindQuest(GlobalVars.SelectedQuestID);
          MultiTowerFloorParam mtFloorParam = instance2.GetMTFloorParam(GlobalVars.SelectedQuestID);
          if (instance1.JoinRandomRoom((byte) (short) quest.playerNum, self.mJoinPlayerParam.Serialize(), mtFloorParam.tower_id, (string) null, GlobalVars.SelectedMultiTowerFloor, 1))
            return;
          DebugUtility.Log("error:" + (object) instance1.LastError);
          self.FailureLobby();
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyState currentState = instance.CurrentState;
        if (!((Behaviour) self).get_enabled())
          return;
        switch (currentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            GlobalVars.SelectedMultiPlayRoomName = instance.GetCurrentRoom().name;
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }

    private class State_ResumeTowerJoinRoom : State<FlowNode_MultiPlayJoinRoom>
    {
      public override void Begin(FlowNode_MultiPlayJoinRoom self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomName))
        {
          self.FailureLobby();
        }
        else
        {
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          if (quest == null || !quest.IsMulti || ((int) quest.playerNum < 1 || (int) quest.unitNum < 1) || ((int) quest.unitNum > 9 || quest.map == null || quest.map.Count <= 0))
          {
            DebugUtility.Log("illegal iname:" + GlobalVars.SelectedQuestID);
            self.IllegalQuest();
          }
          else if (!instance.IsBattle(GlobalVars.SelectedMultiPlayRoomName))
          {
            self.FailureLobby();
          }
          else
          {
            self.mJoinPlayerParam = JSON_MyPhotonPlayerParam.Create(0, 0);
            if (self.mJoinPlayerParam == null)
            {
              self.FailureLobby();
            }
            else
            {
              DebugUtility.Log("Joining name:" + GlobalVars.SelectedMultiPlayRoomName + " pnum:" + (object) quest.playerNum + " unum:" + (object) quest.unitNum);
              if (instance.JoinRoom(GlobalVars.SelectedMultiPlayRoomName, self.mJoinPlayerParam.Serialize(), GlobalVars.ResumeMultiplayPlayerID != 0))
                return;
              DebugUtility.Log("error:" + (object) instance.LastError);
              self.FailureLobby();
            }
          }
        }
      }

      public override void Update(FlowNode_MultiPlayJoinRoom self)
      {
        if (!((Behaviour) self).get_enabled())
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.LOBBY:
            DebugUtility.Log("[PUN]joining failed, back to lobby." + (object) instance.LastError);
            if (instance.LastError == MyPhoton.MyError.ROOM_IS_FULL)
            {
              self.FailureFullMember();
              break;
            }
            self.FailureLobby();
            break;
          case MyPhoton.MyState.JOINING:
            break;
          case MyPhoton.MyState.ROOM:
            self.GotoState<FlowNode_MultiPlayJoinRoom.State_DecidePlayerIndex>();
            break;
          default:
            self.Failure();
            DebugUtility.Log("[PUN]joining failed, error.");
            break;
        }
      }

      public override void End(FlowNode_MultiPlayJoinRoom self)
      {
      }
    }
  }
}
