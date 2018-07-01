// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartMultiPlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(101, "ResumeStart", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/StartMultiPlay", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_StartMultiPlay : FlowNode
  {
    private StateMachine<FlowNode_StartMultiPlay> mStateMachine;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          if (this.mStateMachine != null)
            break;
          this.mStateMachine = new StateMachine<FlowNode_StartMultiPlay>(this);
          this.mStateMachine.GotoState<FlowNode_StartMultiPlay.State_Start>();
          ((Behaviour) this).set_enabled(true);
          break;
        case 101:
          if (this.mStateMachine != null)
            break;
          this.mStateMachine = new StateMachine<FlowNode_StartMultiPlay>(this);
          this.mStateMachine.GotoState<FlowNode_StartMultiPlay.State_ResumeStart>();
          ((Behaviour) this).set_enabled(true);
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
      this.mStateMachine = (StateMachine<FlowNode_StartMultiPlay>) null;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
      DebugUtility.Log("StartMultiPlay success");
    }

    private void Failure()
    {
      this.mStateMachine = (StateMachine<FlowNode_StartMultiPlay>) null;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
      DebugUtility.Log("StartMultiPlay failure");
    }

    private void FailureStartMulti()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsOldestPlayer())
      {
        MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
        if (currentRoom != null)
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = !string.IsNullOrEmpty(currentRoom.json) ? JSON_MyPhotonRoomParam.Parse(currentRoom.json) : (JSON_MyPhotonRoomParam) null;
          myPhotonRoomParam.started = 0;
          instance.SetRoomParam(myPhotonRoomParam.Serialize());
        }
        instance.OpenRoom(true, false);
      }
      this.Failure();
    }

    public void GotoState<StateType>() where StateType : State<FlowNode_StartMultiPlay>, new()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.GotoState<StateType>();
    }

    public class PlayerList
    {
      public JSON_MyPhotonPlayerParam[] players;

      public string Serialize()
      {
        string str = "{\"players\":[";
        if (this.players != null)
        {
          bool flag = true;
          foreach (JSON_MyPhotonPlayerParam player in this.players)
          {
            if (flag)
              flag = false;
            else
              str += ",";
            str += player.Serialize();
          }
        }
        return str + "]}";
      }
    }

    public class RecvData
    {
      public int senderPlayerID;
      public int version;
      public Json_MyPhotonPlayerBinaryParam[] playerList;
      public string playerListJson;
    }

    private class State_Start : State<FlowNode_StartMultiPlay>
    {
      private int mPlayerNum;

      public override void Begin(FlowNode_StartMultiPlay self)
      {
        MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
        if (currentRoom == null)
          return;
        this.mPlayerNum = currentRoom.playerCount;
      }

      public override void Update(FlowNode_StartMultiPlay self)
      {
        DebugUtility.Log("StartMultiPlay State_Start Update");
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.CurrentState != MyPhoton.MyState.ROOM)
        {
          self.Failure();
        }
        else
        {
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom == null)
            self.Failure();
          else if (this.mPlayerNum != currentRoom.playerCount)
            self.FailureStartMulti();
          else if (!instance.IsOldestPlayer() && !currentRoom.start)
          {
            self.Failure();
          }
          else
          {
            bool flag = true;
            using (List<MyPhoton.MyPlayer>.Enumerator enumerator = instance.GetRoomPlayerList().GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(enumerator.Current.json);
                if (photonPlayerParam.state != 2)
                {
                  flag = false;
                  if (instance.IsOldestPlayer())
                  {
                    if (!instance.IsOldestPlayer(photonPlayerParam.playerID))
                    {
                      if (photonPlayerParam.state != 1)
                      {
                        self.FailureStartMulti();
                        return;
                      }
                    }
                    else
                      continue;
                  }
                  DebugUtility.Log("StartMultiPlay State_Start Update allStart is false");
                  break;
                }
              }
            }
            if (flag)
            {
              DebugUtility.Log("StartMultiPlay State_Start Update change state to game start");
              self.GotoState<FlowNode_StartMultiPlay.State_GameStart>();
            }
            else if (currentRoom.start)
            {
              DebugUtility.Log("StartMultiPlay State_Start Update room is closed");
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(instance.GetMyPlayer().json);
              if (photonPlayerParam.state == 2)
                return;
              photonPlayerParam.state = 2;
              instance.SetMyPlayerParam(photonPlayerParam.Serialize());
              DebugUtility.Log("StartMultiPlay State_Start Update update my state");
            }
            else
            {
              if (!instance.IsOldestPlayer())
                return;
              DebugUtility.Log("StartMultiPlay State_Start Update close room");
              instance.CloseRoom();
            }
          }
        }
      }

      public override void End(FlowNode_StartMultiPlay self)
      {
      }
    }

    private class State_ResumeStart : State<FlowNode_StartMultiPlay>
    {
      public override void Begin(FlowNode_StartMultiPlay self)
      {
      }

      public override void Update(FlowNode_StartMultiPlay self)
      {
        DebugUtility.Log("StartMultiPlay State_ResumeStart Update");
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.CurrentState != MyPhoton.MyState.ROOM)
        {
          self.Failure();
        }
        else
        {
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom == null)
          {
            self.Failure();
          }
          else
          {
            JSON_MyPhotonPlayerParam photonPlayerParam1 = JSON_MyPhotonPlayerParam.Parse(instance.GetMyPlayer().json);
            if (photonPlayerParam1.state != 2)
            {
              photonPlayerParam1.state = 2;
              instance.SetMyPlayerParam(photonPlayerParam1.Serialize());
            }
            JSON_MyPhotonRoomParam myPhotonRoomParam = !string.IsNullOrEmpty(currentRoom.json) ? JSON_MyPhotonRoomParam.Parse(currentRoom.json) : (JSON_MyPhotonRoomParam) null;
            if (myPhotonRoomParam == null)
            {
              self.Failure();
            }
            else
            {
              GlobalVars.SelectedQuestID = myPhotonRoomParam.iname;
              GlobalVars.SelectedFriendID = (string) null;
              GlobalVars.SelectedFriend = (FriendData) null;
              GlobalVars.SelectedSupport.Set((SupportData) null);
              self.Success();
              DebugUtility.Log("StartMultiPlay: " + myPhotonRoomParam.Serialize());
              List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
              List<JSON_MyPhotonPlayerParam> photonPlayerParamList = new List<JSON_MyPhotonPlayerParam>();
              for (int index = 0; index < roomPlayerList.Count; ++index)
              {
                JSON_MyPhotonPlayerParam photonPlayerParam2 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
                photonPlayerParam2.playerID = roomPlayerList[index].playerID;
                photonPlayerParamList.Add(photonPlayerParam2);
              }
              photonPlayerParamList.Sort((Comparison<JSON_MyPhotonPlayerParam>) ((a, b) => a.playerIndex - b.playerIndex));
              List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
              myPlayersStarted.Clear();
              string roomParam = instance.GetRoomParam("started");
              if (roomParam != null)
              {
                foreach (JSON_MyPhotonPlayerParam player in JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(roomParam).players)
                {
                  player.SetupUnits();
                  myPlayersStarted.Add(player);
                }
              }
              else
              {
                using (List<JSON_MyPhotonPlayerParam>.Enumerator enumerator = photonPlayerParamList.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    JSON_MyPhotonPlayerParam current = enumerator.Current;
                    current.SetupUnits();
                    myPlayersStarted.Add(current);
                  }
                }
              }
              photonPlayerParam1.state = 3;
              instance.SetMyPlayerParam(photonPlayerParam1.Serialize());
              instance.SetResumeMyPlayer(GlobalVars.ResumeMultiplayPlayerID);
              instance.MyPlayerIndex = GlobalVars.ResumeMultiplaySeatID;
            }
          }
        }
      }

      public override void End(FlowNode_StartMultiPlay self)
      {
      }
    }

    private class State_GameStart : State<FlowNode_StartMultiPlay>
    {
      private FlowNode_StartMultiPlay.RecvData mSend = new FlowNode_StartMultiPlay.RecvData();
      private List<FlowNode_StartMultiPlay.RecvData> mRecvList = new List<FlowNode_StartMultiPlay.RecvData>();
      private float mWait;
      private bool mConfirm;
      private float mStartWait;

      public override void Begin(FlowNode_StartMultiPlay self)
      {
        using (List<MyPhoton.MyPlayer>.Enumerator enumerator = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList().GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            if (JSON_MyPhotonPlayerParam.Parse(enumerator.Current.json).state != 2)
            {
              self.FailureStartMulti();
              break;
            }
          }
        }
      }

      public override void Update(FlowNode_StartMultiPlay self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.CurrentState != MyPhoton.MyState.ROOM)
        {
          self.Failure();
        }
        else
        {
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom == null)
          {
            self.Failure();
          }
          else
          {
            JSON_MyPhotonRoomParam myPhotonRoomParam = !string.IsNullOrEmpty(currentRoom.json) ? JSON_MyPhotonRoomParam.Parse(currentRoom.json) : (JSON_MyPhotonRoomParam) null;
            if (myPhotonRoomParam == null)
            {
              self.Failure();
            }
            else
            {
              if (myPhotonRoomParam.started == 0)
              {
                myPhotonRoomParam.started = 1;
                instance.SetRoomParam(myPhotonRoomParam.Serialize());
              }
              if ((double) this.mStartWait > 0.0)
              {
                this.mStartWait -= Time.get_deltaTime();
                if ((double) this.mStartWait > 0.0)
                  return;
                GlobalVars.SelectedQuestID = myPhotonRoomParam.iname;
                GlobalVars.SelectedFriendID = (string) null;
                GlobalVars.SelectedFriend = (FriendData) null;
                GlobalVars.SelectedSupport.Set((SupportData) null);
                self.Success();
                DebugUtility.Log("StartMultiPlay: " + myPhotonRoomParam.Serialize());
              }
              else if ((double) this.mWait > 0.0)
              {
                this.mWait -= Time.get_deltaTime();
              }
              else
              {
                List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
                if ((myPhotonRoomParam.type == 1 || myPhotonRoomParam.type == 3) && roomPlayerList.Count == 1)
                  self.FailureStartMulti();
                else if (this.mConfirm)
                {
                  using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(enumerator.Current.json);
                      if (photonPlayerParam.state != 3)
                        return;
                      if (photonPlayerParam.state < 2)
                      {
                        self.Failure();
                        return;
                      }
                    }
                  }
                  this.mStartWait = 0.1f;
                }
                else
                {
                  MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
                  if (this.mRecvList.Count <= 0)
                  {
                    this.mSend.senderPlayerID = myPlayer.photonPlayerID;
                    this.mSend.playerListJson = (string) null;
                    List<JSON_MyPhotonPlayerParam> photonPlayerParamList = new List<JSON_MyPhotonPlayerParam>();
                    for (int index = 0; index < roomPlayerList.Count; ++index)
                    {
                      JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
                      photonPlayerParamList.Add(photonPlayerParam);
                    }
                    photonPlayerParamList.Sort((Comparison<JSON_MyPhotonPlayerParam>) ((a, b) => a.playerIndex - b.playerIndex));
                    FlowNode_StartMultiPlay.PlayerList playerList = new FlowNode_StartMultiPlay.PlayerList();
                    playerList.players = photonPlayerParamList.ToArray();
                    Json_MyPhotonPlayerBinaryParam[] playerBinaryParamArray = new Json_MyPhotonPlayerBinaryParam[playerList.players.Length];
                    for (int index = 0; index < playerList.players.Length; ++index)
                    {
                      playerList.players[index].CreateJsonUnitData();
                      playerBinaryParamArray[index] = new Json_MyPhotonPlayerBinaryParam();
                      playerBinaryParamArray[index].Set(playerList.players[index]);
                    }
                    this.mSend.playerList = playerBinaryParamArray;
                    byte[] msg = GameUtility.Object2Binary<FlowNode_StartMultiPlay.RecvData>(this.mSend);
                    instance.SendRoomMessageBinary(true, msg, MyPhoton.SEND_TYPE.Sync, false);
                    this.mRecvList.Add(this.mSend);
                    this.mSend.playerListJson = playerList.Serialize();
                  }
                  List<MyPhoton.MyEvent> events = instance.GetEvents();
                  for (int index = events.Count - 1; index >= 0; --index)
                  {
                    FlowNode_StartMultiPlay.RecvData buffer = (FlowNode_StartMultiPlay.RecvData) null;
                    if (!GameUtility.Binary2Object<FlowNode_StartMultiPlay.RecvData>(out buffer, events[index].binary))
                    {
                      DebugUtility.LogError("[PUN] started player list version error: " + events[index].json);
                      instance.Disconnect();
                      return;
                    }
                    if (buffer == null || buffer.version < this.mSend.version)
                    {
                      DebugUtility.LogError("[PUN] started player list version error: " + events[index].json);
                      instance.Disconnect();
                      return;
                    }
                    if (buffer.version <= this.mSend.version)
                    {
                      buffer.senderPlayerID = events[index].playerID;
                      DebugUtility.Log("[PUN] recv started player list: " + events[index].json);
                      this.mRecvList.Add(buffer);
                      events.Remove(events[index]);
                    }
                  }
                  using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      MyPhoton.MyPlayer p = enumerator.Current;
                      if (this.mRecvList.FindIndex((Predicate<FlowNode_StartMultiPlay.RecvData>) (r => r.senderPlayerID == p.photonPlayerID)) < 0)
                        return;
                    }
                  }
                  bool flag = true;
                  using (List<FlowNode_StartMultiPlay.RecvData>.Enumerator enumerator = this.mRecvList.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      FlowNode_StartMultiPlay.RecvData current = enumerator.Current;
                      if (current.playerList.Length == this.mSend.playerList.Length)
                      {
                        for (int index = 0; index < this.mSend.playerList.Length; ++index)
                        {
                          if (!Json_MyPhotonPlayerBinaryParam.IsEqual(current.playerList[index], this.mSend.playerList[index]))
                            flag = false;
                        }
                        if (!flag)
                          break;
                      }
                      else
                      {
                        flag = false;
                        break;
                      }
                    }
                  }
                  if (!flag)
                  {
                    DebugUtility.Log("[PUN] started player list is not equal. ver:" + (object) this.mSend.version);
                    this.mRecvList.Clear();
                    ++this.mSend.version;
                    this.mWait = 1f;
                  }
                  else
                  {
                    DebugUtility.Log("[PUN]started player list decided. ver:" + (object) this.mSend.version);
                    List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
                    myPlayersStarted.Clear();
                    foreach (JSON_MyPhotonPlayerParam player in JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(this.mSend.playerListJson).players)
                    {
                      player.SetupUnits();
                      myPlayersStarted.Add(player);
                    }
                    if (instance.IsOldestPlayer())
                      instance.UpdateRoomParam("started", (object) this.mSend.playerListJson);
                    if (events.Count > 0)
                      DebugUtility.LogError("[PUN] event must be empty.");
                    events.Clear();
                    JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
                    photonPlayerParam.state = 3;
                    instance.SetMyPlayerParam(photonPlayerParam.Serialize());
                    this.mConfirm = true;
                  }
                }
              }
            }
          }
        }
      }

      public override void End(FlowNode_StartMultiPlay self)
      {
      }
    }
  }
}
