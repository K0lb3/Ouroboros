// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayAPI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using Gsc.App;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(23, "MultiTowerRoom", FlowNode.PinTypes.Input, 23)]
  [FlowNode.Pin(11000, "NotChallengeFloor", FlowNode.PinTypes.Output, 11000)]
  [FlowNode.Pin(10000, "VersusFaildSeasonGift", FlowNode.PinTypes.Output, 10000)]
  [FlowNode.Pin(9000, "VersusNotPhotonID", FlowNode.PinTypes.Output, 9000)]
  [FlowNode.Pin(8000, "VersusFailRoomID", FlowNode.PinTypes.Output, 8000)]
  [FlowNode.Pin(7000, "VersusNotLineRoom", FlowNode.PinTypes.Output, 7000)]
  [FlowNode.Pin(6000, "MultiMaintenance", FlowNode.PinTypes.Output, 6000)]
  [FlowNode.Pin(5000, "NoMatchVersion", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(4902, "RepelledBlockList", FlowNode.PinTypes.Output, 4902)]
  [FlowNode.Pin(4900, "NoRoom", FlowNode.PinTypes.Output, 4900)]
  [FlowNode.Pin(4802, "OutOfDateQuest", FlowNode.PinTypes.Output, 4802)]
  [FlowNode.Pin(4801, "IllegalComment", FlowNode.PinTypes.Output, 4801)]
  [FlowNode.Pin(4800, "FailedMakeRoom", FlowNode.PinTypes.Output, 4800)]
  [FlowNode.Pin(101, "Failure", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(80, "Lobby", FlowNode.PinTypes.Input, 80)]
  [FlowNode.Pin(71, "RankMatchCreateRoom", FlowNode.PinTypes.Input, 71)]
  [FlowNode.Pin(70, "RankMatchStatus", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(72, "RankMatchStart", FlowNode.PinTypes.Input, 72)]
  [FlowNode.Pin(60, "MultiRoom_WithGPS", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(56, "PassLock", FlowNode.PinTypes.Input, 56)]
  [FlowNode.Pin(55, "PassRelease", FlowNode.PinTypes.Input, 55)]
  [FlowNode.Pin(51, "RoomJoinInvitation", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(50, "RoomInvitation", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(33, "VersusDraftPartySelected", FlowNode.PinTypes.Input, 33)]
  [FlowNode.Pin(32, "VersusDraftUnitSelected", FlowNode.PinTypes.Input, 32)]
  [FlowNode.Pin(31, "VersusDraftUnitList", FlowNode.PinTypes.Input, 31)]
  [FlowNode.Pin(30, "MultiTowerRoomJoinList", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(29, "MultiTowerInRoom", FlowNode.PinTypes.Input, 29)]
  [FlowNode.Pin(28, "MultiTowerJoinInvitation", FlowNode.PinTypes.Input, 28)]
  [FlowNode.Pin(27, "MultiTowerStatus", FlowNode.PinTypes.Input, 27)]
  [FlowNode.Pin(26, "MultiTowerRoomUpdate", FlowNode.PinTypes.Input, 26)]
  [FlowNode.Pin(25, "MultiTowerRoomJoinID", FlowNode.PinTypes.Input, 25)]
  [FlowNode.Pin(24, "MultiTowerRoomJoin", FlowNode.PinTypes.Input, 24)]
  [FlowNode.Pin(22, "MultiTowerRoomMake", FlowNode.PinTypes.Input, 22)]
  [FlowNode.Pin(21, "MultiTowerAutoCreate", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(20, "VersusFriendScore", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(19, "VersusRecvSeasonGift", FlowNode.PinTypes.Input, 19)]
  [FlowNode.Pin(18, "VersusStatus", FlowNode.PinTypes.Input, 18)]
  [FlowNode.Pin(17, "VersusLineJoin", FlowNode.PinTypes.Input, 17)]
  [FlowNode.Pin(16, "VersusLineMake", FlowNode.PinTypes.Input, 16)]
  [FlowNode.Pin(15, "VersusLineReq", FlowNode.PinTypes.Input, 15)]
  [FlowNode.Pin(14, "VersusReset", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(13, "VersusRoomUpdate", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(12, "VersusRoomJoinID", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(11, "VersusCreateRoom", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(10, "VersusStart", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(9, "CheckVersion", FlowNode.PinTypes.Input, 9)]
  [FlowNode.Pin(8, "RoomJoinID", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(7, "RoomUpdate", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(6, "RoomJoinLINE", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(5, "RoomLINE", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(4, "RoomMakeLINE", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(3, "RoomJoin", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(2, "Room", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(0, "RoomMake", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiPlayAPI", 32741)]
  public class FlowNode_MultiPlayAPI : FlowNode_Network
  {
    public static readonly int ROOMID_VALIDATE_MAX = 99999;
    private readonly int MULTI_TOWER_UNIT_MAX = 6;
    private const int PIN_IN_VERSUS_CREATE_ROOM = 11;
    private const int PIN_IN_VERSUS_LINE_REQ = 15;
    private const int PIN_IN_VERSUS_LINE_MAKE = 16;
    private const int PIN_IN_VERSUS_LINE_JOIN = 17;
    private const int PIN_IN_VERSUS_STATUS = 18;
    private const int PIN_IN_MULTI_TOWER_ROOM_JOIN_LIST = 30;
    private const int PIN_IN_VERSUS_DRAFT_UNIT_LIST = 31;
    private const int PIN_IN_VERSUS_DRAFT_UNIT_SELECTED = 32;
    private const int PIN_IN_VERSUS_DRAFT_PARTY_SELECTED = 33;
    private const int PIN_IN_RANKMATCH_START = 72;
    private const int PIN_IN_RANKMATCH_STATUS = 70;
    private const int PIN_IN_RANKMATCH_CREATE_ROOM = 71;
    private const int PIN_IN_VERSUS_LOBBY = 80;
    public static float RoomMakeTime;
    public static ReqMPRoom.Response RoomList;
    public static readonly int ROOMID_VALIDATE_MIN;

    private FlowNode_MultiPlayAPI.EAPI API { get; set; }

    private void ResetPlacementParam()
    {
      for (int index = 0; index < this.MULTI_TOWER_UNIT_MAX; ++index)
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTITW_ID_KEY + (object) index, index, false);
      PlayerPrefsUtility.Save();
    }

    private bool IsMultiAreaQuest()
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest != null)
          flag = quest.IsMultiAreaQuest;
      }
      return flag;
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
        case 4:
        case 22:
          this.ResetPlacementParam();
          string empty1 = string.Empty;
          string str1 = !PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ROOM_COMMENT_KEY) ? LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT") : PlayerPrefsUtility.GetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, string.Empty);
          if (string.IsNullOrEmpty(str1))
            str1 = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
          if (!MyMsgInput.isLegal(str1))
          {
            DebugUtility.Log("comment is not legal");
            this.ActivateOutputLinks(4801);
            break;
          }
          GlobalVars.ResumeMultiplayPlayerID = 0;
          GlobalVars.ResumeMultiplaySeatID = 0;
          GlobalVars.SelectedMultiPlayRoomComment = str1;
          DebugUtility.Log("MakeRoom Comment:" + GlobalVars.SelectedMultiPlayRoomComment);
          bool flag = false;
          if (pinID == 4)
          {
            GlobalVars.SelectedQuestID = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.iname;
            GlobalVars.SelectedMultiPlayRoomType = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.type;
            GlobalVars.EditMultiPlayRoomPassCode = "0";
            flag = true;
            DebugUtility.Log("MakeRoom for LINE Quest:" + GlobalVars.SelectedQuestID + " Type:" + (object) GlobalVars.SelectedMultiPlayRoomType + " PassCodeHash:" + GlobalVars.SelectedMultiPlayRoomPassCodeHash);
          }
          GlobalVars.EditMultiPlayRoomPassCode = "0";
          string s = FlowNode_Variable.Get("MultiPlayPasscode");
          if (!string.IsNullOrEmpty(s))
          {
            int result = 0;
            if (int.TryParse(s, out result))
              GlobalVars.EditMultiPlayRoomPassCode = result.ToString();
          }
          bool isPrivate = flag;
          FlowNode_MultiPlayAPI.RoomMakeTime = Time.get_realtimeSinceStartup();
          bool limit = GlobalVars.SelectedMultiPlayLimit & GlobalVars.EditMultiPlayRoomPassCode == "0";
          int unitlv = isPrivate || !limit ? 0 : GlobalVars.MultiPlayJoinUnitLv;
          bool clear = !isPrivate && limit && GlobalVars.MultiPlayClearOnly;
          int selectedMultiTowerFloor1 = GlobalVars.SelectedMultiTowerFloor;
          if (Network.Mode != Network.EConnectMode.Online)
          {
            GlobalVars.SelectedMultiPlayRoomID = (int) (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            GlobalVars.SelectedMultiPlayPhotonAppID = string.Empty;
            GlobalVars.SelectedMultiPlayRoomName = Guid.NewGuid().ToString();
            DebugUtility.Log("MakeRoom RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID + " AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
            this.Success();
            break;
          }
          MultiInvitationSendWindow.ClearInvited();
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.MAKE;
          if (this.IsMultiAreaQuest())
          {
            Vector2 location = GlobalVars.Location;
            this.ExecRequest((WebAPI) new ReqMultiAreaRoomMake(GlobalVars.SelectedQuestID, str1, GlobalVars.EditMultiPlayRoomPassCode, isPrivate, limit, unitlv, clear, location, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          if (pinID != 22)
          {
            this.ExecRequest((WebAPI) new ReqMPRoomMake(GlobalVars.SelectedQuestID, str1, GlobalVars.EditMultiPlayRoomPassCode, isPrivate, limit, unitlv, clear, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          this.ExecRequest((WebAPI) new ReqMultiTwRoomMake(GlobalVars.SelectedMultiTowerID, str1, GlobalVars.EditMultiPlayRoomPassCode, selectedMultiTowerFloor1, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 2:
        case 5:
        case 23:
        case 50:
        case 60:
          string fuid = (string) null;
          if (pinID == 5)
            fuid = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID;
          else if (pinID == 50)
            fuid = GlobalVars.MultiInvitationRoomOwner;
          DebugUtility.Log("ListRoom FUID:" + fuid);
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.StartCoroutine(this.GetPhotonRoomList(fuid));
            break;
          }
          FlowNode_MultiPlayAPI.RoomList = (ReqMPRoom.Response) null;
          string iname = string.Empty;
          if (pinID == 2)
            iname = GlobalVars.SelectedQuestID;
          int selectedMultiTowerFloor2 = GlobalVars.SelectedMultiTowerFloor;
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.ROOM;
          if (pinID == 60 || this.IsMultiAreaQuest())
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            Vector2 location = GlobalVars.Location;
            List<string> stringList = new List<string>();
            if (!string.IsNullOrEmpty(iname))
              stringList.Add(iname);
            else
              stringList = instance.GetMultiAreaQuestList();
            this.ExecRequest((WebAPI) new ReqMultiAreaRoom(fuid, stringList.ToArray(), location, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          if (pinID != 23)
          {
            this.ExecRequest((WebAPI) new ReqMPRoom(fuid, iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          this.ExecRequest((WebAPI) new ReqMultiTwRoom(fuid, GlobalVars.SelectedMultiTowerID, selectedMultiTowerFloor2, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 3:
        case 6:
        case 24:
        case 51:
          if (FlowNode_MultiPlayAPI.RoomList == null || FlowNode_MultiPlayAPI.RoomList.rooms == null || FlowNode_MultiPlayAPI.RoomList.rooms.Length <= 0)
          {
            this.Failure();
            break;
          }
          this.ResetPlacementParam();
          bool LockRoom = false;
          switch (pinID)
          {
            case 6:
              if (FlowNode_MultiPlayAPI.RoomList.rooms.Length != 1 || FlowNode_MultiPlayAPI.RoomList.rooms[0] == null)
              {
                this.Failure();
                return;
              }
              GlobalVars.SelectedMultiPlayRoomID = FlowNode_MultiPlayAPI.RoomList.rooms[0].roomid;
              DebugUtility.Log("JoinRoom for LINE RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID);
              break;
            case 51:
              if (FlowNode_MultiPlayAPI.RoomList.rooms.Length != 1 || FlowNode_MultiPlayAPI.RoomList.rooms[0] == null)
              {
                this.Failure();
                return;
              }
              LockRoom = GlobalVars.MultiInvitationRoomLocked;
              GlobalVars.SelectedMultiPlayRoomID = FlowNode_MultiPlayAPI.RoomList.rooms[0].roomid;
              DebugUtility.Log("JoinRoom for Invitation RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID);
              break;
          }
          GlobalVars.ResumeMultiplayPlayerID = 0;
          GlobalVars.ResumeMultiplaySeatID = 0;
          GlobalVars.SelectedQuestID = (string) null;
          for (int index = 0; index < FlowNode_MultiPlayAPI.RoomList.rooms.Length; ++index)
          {
            if (FlowNode_MultiPlayAPI.RoomList.rooms[index].quest != null && !string.IsNullOrEmpty(FlowNode_MultiPlayAPI.RoomList.rooms[index].quest.iname) && FlowNode_MultiPlayAPI.RoomList.rooms[index].roomid == GlobalVars.SelectedMultiPlayRoomID)
            {
              GlobalVars.SelectedQuestID = FlowNode_MultiPlayAPI.RoomList.rooms[index].quest.iname;
              break;
            }
          }
          if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
          {
            this.Failure();
            break;
          }
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.StartCoroutine(this.GetPhotonRoomName());
            break;
          }
          int selectedMultiTowerFloor3 = GlobalVars.SelectedMultiTowerFloor;
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.JOIN;
          if (pinID != 24)
          {
            this.ExecRequest((WebAPI) new ReqMPRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), LockRoom));
            break;
          }
          this.ExecRequest((WebAPI) new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), LockRoom, selectedMultiTowerFloor3, false));
          break;
        case 7:
        case 26:
        case 55:
        case 56:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          if (!string.IsNullOrEmpty(GlobalVars.EditMultiPlayRoomComment) && !MyMsgInput.isLegal(GlobalVars.EditMultiPlayRoomComment))
          {
            DebugUtility.Log("comment is not legal");
            this.ActivateOutputLinks(4801);
            break;
          }
          if (pinID == 26 && string.IsNullOrEmpty(GlobalVars.EditMultiPlayRoomComment))
            GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
          switch (pinID)
          {
            case 55:
              GlobalVars.EditMultiPlayRoomPassCode = "0";
              GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
              break;
            case 56:
              GlobalVars.EditMultiPlayRoomPassCode = "1";
              GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
              break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.UPDATE;
          if (GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.TOWER)
          {
            this.ExecRequest((WebAPI) new ReqMPRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.EditMultiPlayRoomPassCode, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
            break;
          }
          string selectedMultiTowerId = GlobalVars.SelectedMultiTowerID;
          int selectedMultiTowerFloor4 = GlobalVars.SelectedMultiTowerFloor;
          this.ExecRequest((WebAPI) new ReqMultiTwRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.EditMultiPlayRoomPassCode, selectedMultiTowerId, selectedMultiTowerFloor4, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 8:
        case 25:
        case 28:
        case 30:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          if (GlobalVars.SelectedMultiPlayRoomID < FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MIN || GlobalVars.SelectedMultiPlayRoomID > FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MAX)
          {
            this.Failure();
            break;
          }
          this.ResetPlacementParam();
          GlobalVars.ResumeMultiplayPlayerID = 0;
          GlobalVars.ResumeMultiplaySeatID = 0;
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.JOIN;
          if (pinID != 25 && pinID != 28 && pinID != 30)
          {
            this.ExecRequest((WebAPI) new ReqMPRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
            break;
          }
          if (pinID == 30)
          {
            this.API = FlowNode_MultiPlayAPI.EAPI.MT_JOIN;
            this.ExecRequest((WebAPI) new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), false, 0, false));
            break;
          }
          this.API = FlowNode_MultiPlayAPI.EAPI.MT_JOIN;
          this.ExecRequest((WebAPI) new ReqMultiTwRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true, 0, pinID == 28));
          break;
        case 9:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSION;
          this.ExecRequest((WebAPI) new ReqMPVersion(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 10:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          GlobalVars.SelectedMultiPlayRoomName = string.Empty;
          GlobalVars.VersusRoomReuse = false;
          GlobalVars.ResumeMultiplayPlayerID = 0;
          GlobalVars.ResumeMultiplaySeatID = 0;
          GlobalVars.MultiPlayVersusKey = MonoSingleton<GameManager>.Instance.GetVersusKey(GlobalVars.SelectedMultiPlayVersusType);
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_START;
          this.ExecRequest((WebAPI) new ReqVersusStart(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 11:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          string empty2 = string.Empty;
          string str2 = !PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ROOM_COMMENT_KEY) ? LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT") : PlayerPrefsUtility.GetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, string.Empty);
          if (string.IsNullOrEmpty(str2))
            str2 = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
          if (!MyMsgInput.isLegal(str2))
          {
            DebugUtility.Log("comment is not legal");
            this.ActivateOutputLinks(4801);
            break;
          }
          if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Friend && MonoSingleton<GameManager>.Instance.VSDraftType != VersusDraftType.Normal && GlobalVars.IsVersusDraftMode)
            GlobalVars.SelectedQuestID = MonoSingleton<GameManager>.Instance.VSDraftQuestId;
          FlowNode_MultiPlayAPI.RoomMakeTime = Time.get_realtimeSinceStartup();
          GlobalVars.SelectedMultiPlayRoomComment = str2;
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_MAKE;
          this.ExecRequest((WebAPI) new ReqVersusMake(GlobalVars.SelectedMultiPlayVersusType, str2, GlobalVars.SelectedQuestID, false, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 12:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          if (GlobalVars.SelectedMultiPlayRoomID < FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MIN || GlobalVars.SelectedMultiPlayRoomID > FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MAX)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_JOIN;
          this.ExecRequest((WebAPI) new ReqVersusRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 13:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          if (!MyMsgInput.isLegal(GlobalVars.EditMultiPlayRoomComment))
          {
            DebugUtility.Log("comment is not legal");
            this.ActivateOutputLinks(4801);
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.UPDATE;
          this.ExecRequest((WebAPI) new ReqVersusRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.SelectedQuestID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 14:
          GlobalVars.SelectedMultiPlayRoomName = string.Empty;
          GlobalVars.VersusRoomReuse = false;
          GlobalVars.ResumeMultiplayPlayerID = 0;
          GlobalVars.ResumeMultiplaySeatID = 0;
          GlobalVars.MultiPlayVersusKey = MonoSingleton<GameManager>.Instance.GetVersusKey(GlobalVars.SelectedMultiPlayVersusType);
          this.Success();
          break;
        case 15:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_LINE_REQ;
          this.ExecRequest((WebAPI) new ReqVersusLine(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 16:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          GlobalVars.VersusRoomReuse = false;
          FlowNode_MultiPlayAPI.RoomMakeTime = Time.get_realtimeSinceStartup();
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_LINE_MAKE;
          this.ExecRequest((WebAPI) new ReqVersusLineMake(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 17:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          GlobalVars.SelectedMultiPlayRoomID = FlowNode_OnUrlSchemeLaunch.LINEParam_decided.roomid;
          if (GlobalVars.SelectedMultiPlayRoomID < FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MIN || GlobalVars.SelectedMultiPlayRoomID > FlowNode_MultiPlayAPI.ROOMID_VALIDATE_MAX)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_JOIN;
          this.ExecRequest((WebAPI) new ReqVersusRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 18:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          MonoSingleton<GameManager>.Instance.IsVSCpuBattle = false;
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_STATUS;
          this.ExecRequest((WebAPI) new ReqVersusStatus(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 19:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_SEASON;
          this.ExecRequest((WebAPI) new ReqVersusSeason(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 20:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_FRIEND;
          this.ExecRequest((WebAPI) new ReqVersusFriendScore(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 21:
          GameManager instance1 = MonoSingleton<GameManager>.Instance;
          MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance2, (UnityEngine.Object) null) && instance2.IsConnectedInRoom())
          {
            List<MyPhoton.MyPlayer> roomPlayerList = instance2.GetRoomPlayerList();
            if (roomPlayerList.Count > 1)
            {
              int index = !instance2.IsHost() ? 0 : 1;
              JSON_MyPhotonPlayerParam target_player = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
              if (target_player != null && GlobalVars.BlockList != null && (GlobalVars.BlockList.Count > 0 && GlobalVars.BlockList.FindIndex((Predicate<string>) (uid => uid == target_player.UID)) != -1))
              {
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
                  break;
                ((Behaviour) this).set_enabled(false);
                this.ActivateOutputLinks(4902);
                break;
              }
            }
            GlobalVars.CreateAutoMultiTower = true;
            if (instance2.IsCreatedRoom())
            {
              instance2.OpenRoom(true, false);
              MyPhoton.MyRoom currentRoom = instance2.GetCurrentRoom();
              if (currentRoom != null)
              {
                JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
                if (myPhotonRoomParam != null)
                {
                  List<MultiTowerFloorParam> mtAllFloorParam = instance1.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
                  if (mtAllFloorParam != null)
                    GlobalVars.SelectedMultiTowerFloor = Mathf.Min(mtAllFloorParam.Count, GlobalVars.SelectedMultiTowerFloor + 1);
                  myPhotonRoomParam.challegedMTFloor = GlobalVars.SelectedMultiTowerFloor;
                  myPhotonRoomParam.iname = GlobalVars.SelectedMultiTowerID + "_" + myPhotonRoomParam.challegedMTFloor.ToString();
                  instance2.SetRoomParam(myPhotonRoomParam.Serialize());
                  if (MultiPlayAPIRoom.IsLocked(myPhotonRoomParam.passCode))
                    GlobalVars.EditMultiPlayRoomPassCode = "1";
                }
              }
            }
            instance2.AddMyPlayerParam("BattleStart", (object) false);
            instance2.AddMyPlayerParam("resume", (object) false);
            MyPhoton.MyPlayer myPlayer = instance2.GetMyPlayer();
            if (myPlayer != null)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
              if (photonPlayerParam != null)
              {
                photonPlayerParam.mtChallengeFloor = instance1.GetMTChallengeFloor();
                photonPlayerParam.mtClearedFloor = instance1.GetMTClearedMaxFloor();
                instance2.SetMyPlayerParam(photonPlayerParam.Serialize());
              }
            }
            this.Success();
            break;
          }
          this.Failure();
          break;
        case 27:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.MT_STATUS;
          this.ExecRequest((WebAPI) new ReqMultiTwStatus(GlobalVars.SelectedMultiTowerID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 29:
          MyPhoton instance3 = PunMonoSingleton<MyPhoton>.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance3, (UnityEngine.Object) null) && instance3.IsConnectedInRoom())
          {
            this.Success();
            break;
          }
          this.Failure();
          break;
        case 31:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_DRAFT;
          this.ExecRequest((WebAPI) new ReqVersusDraft(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 32:
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_DRAFT_SELECTED;
          this.ExecRequest((WebAPI) new ReqVersusDraftSelect(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 33:
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_DRAFT_PARTY;
          this.ExecRequest((WebAPI) new ReqVersusDraftParty(GlobalVars.SelectedMultiPlayRoomName, VersusDraftList.DraftID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 70:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          MonoSingleton<GameManager>.Instance.IsVSCpuBattle = false;
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.RANKMATCH_STATUS;
          this.ExecRequest((WebAPI) new ReqRankMatchStatus(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 71:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          FlowNode_MultiPlayAPI.RoomMakeTime = Time.get_realtimeSinceStartup();
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_MAKE;
          this.ExecRequest((WebAPI) new ReqRankMatchMake(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 72:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          MonoSingleton<GameManager>.Instance.IsVSCpuBattle = false;
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.RANKMATCH_START;
          this.ExecRequest((WebAPI) new ReqRankMatchStart(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 80:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.VERSUS_LOBBY;
          this.ExecRequest((WebAPI) new ReqVersusLobby(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
      }
    }

    private void Success()
    {
      DebugUtility.Log("MultiPlayAPI success");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("MultiPlayAPI failure");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(101);
    }

    [DebuggerHidden]
    private IEnumerator GetPhotonRoomList(string fuid)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiPlayAPI.\u003CGetPhotonRoomList\u003Ec__IteratorBC()
      {
        fuid = fuid,
        \u003C\u0024\u003Efuid = fuid,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator GetPhotonRoomName()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiPlayAPI.\u003CGetPhotonRoomName\u003Ec__IteratorBD()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void OnSuccess(WWWResult www)
    {
      DebugUtility.Log(nameof (OnSuccess));
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.MultiMaintenance:
          case Network.EErrCode.VsMaintenance:
          case Network.EErrCode.MultiVersionMaintenance:
          case Network.EErrCode.MultiTowerMaintenance:
            Network.RemoveAPI();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(6000);
            break;
          case Network.EErrCode.OutOfDateQuest:
            Network.RemoveAPI();
            Network.ResetError();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4802);
            break;
          case Network.EErrCode.MultiVersionMismatch:
          case Network.EErrCode.VS_Version:
            Network.RemoveAPI();
            Network.ResetError();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(5000);
            break;
          case Network.EErrCode.RoomFailedMakeRoom:
            Network.RemoveAPI();
            Network.ResetError();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4800);
            break;
          case Network.EErrCode.RoomIllegalComment:
          case Network.EErrCode.VS_IllComment:
            if (this.API == FlowNode_MultiPlayAPI.EAPI.MAKE)
            {
              string str = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
              PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, str, false);
            }
            Network.RemoveAPI();
            Network.ResetError();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4801);
            break;
          case Network.EErrCode.RoomNoRoom:
          case Network.EErrCode.VS_NoRoom:
            Network.RemoveAPI();
            Network.ResetError();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4900);
            break;
          case Network.EErrCode.RepelledBlockList:
            Network.RemoveAPI();
            Network.ResetError();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4902);
            break;
          case Network.EErrCode.VS_NotLINERoomInfo:
            Network.RemoveAPI();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(7000);
            break;
          case Network.EErrCode.VS_FailRoomID:
            Network.RemoveAPI();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(8000);
            break;
          case Network.EErrCode.VS_NotPhotonAppID:
            Network.RemoveAPI();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(9000);
            break;
          case Network.EErrCode.VS_FaildSeasonGift:
            Network.RemoveAPI();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
              break;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(10000);
            break;
          default:
            this.OnFailed();
            break;
        }
      }
      else
      {
        if (this.API == FlowNode_MultiPlayAPI.EAPI.MAKE)
        {
          WebAPI.JSON_BodyResponse<ReqMPRoomMake.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoomMake.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnFailed();
            return;
          }
          GlobalVars.SelectedMultiPlayRoomID = jsonObject.body.roomid;
          GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
          GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
          DebugUtility.Log("MakeRoom RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID + " AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
        }
        else if (this.API == FlowNode_MultiPlayAPI.EAPI.ROOM)
        {
          WebAPI.JSON_BodyResponse<ReqMPRoom.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoom.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnFailed();
            return;
          }
          FlowNode_MultiPlayAPI.RoomList = jsonObject.body;
          if (FlowNode_MultiPlayAPI.RoomList == null)
            DebugUtility.Log("ListRoom null");
          else if (FlowNode_MultiPlayAPI.RoomList.rooms == null)
            DebugUtility.Log("ListRoom rooms null");
          else
            DebugUtility.Log("ListRoom num:" + (object) FlowNode_MultiPlayAPI.RoomList.rooms.Length);
        }
        else if (this.API == FlowNode_MultiPlayAPI.EAPI.JOIN)
        {
          WebAPI.JSON_BodyResponse<ReqMPRoomJoin.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPRoomJoin.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnFailed();
            return;
          }
          if (jsonObject.body.quest == null || string.IsNullOrEmpty(jsonObject.body.quest.iname))
          {
            this.OnFailed();
            return;
          }
          GlobalVars.SelectedQuestID = jsonObject.body.quest.iname;
          GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
          GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
          DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
        }
        else if (this.API != FlowNode_MultiPlayAPI.EAPI.UPDATE)
        {
          if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSION)
          {
            WebAPI.JSON_BodyResponse<ReqMPVersion.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMPVersion.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnFailed();
              return;
            }
            BootLoader.GetAccountManager().SetDeviceId((string) null, jsonObject.body.device_id);
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_START)
          {
            WebAPI.JSON_BodyResponse<ReqVersusStart.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusStart.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnFailed();
              return;
            }
            GameManager instance = MonoSingleton<GameManager>.Instance;
            GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
            switch (GlobalVars.SelectedMultiPlayVersusType)
            {
              case VERSUS_TYPE.Free:
                GlobalVars.SelectedQuestID = instance.VSDraftType != VersusDraftType.Draft ? jsonObject.body.maps.free : MonoSingleton<GameManager>.Instance.VSDraftQuestId;
                break;
              case VERSUS_TYPE.Friend:
                GlobalVars.SelectedQuestID = jsonObject.body.maps.friend;
                break;
            }
            DebugUtility.Log("MakeRoom RoomID: AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + "/ QuestID:" + GlobalVars.SelectedQuestID);
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_MAKE)
          {
            WebAPI.JSON_BodyResponse<ReqVersusMake.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusMake.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnFailed();
              return;
            }
            GlobalVars.SelectedMultiPlayRoomID = jsonObject.body.roomid;
            GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
            if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Friend)
              GlobalVars.EditMultiPlayRoomPassCode = "1";
          }
          else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_JOIN)
          {
            WebAPI.JSON_BodyResponse<ReqVersusRoomJoin.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusRoomJoin.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnFailed();
              return;
            }
            if (jsonObject.body.quest == null || string.IsNullOrEmpty(jsonObject.body.quest.iname))
            {
              this.OnFailed();
              return;
            }
            GlobalVars.SelectedQuestID = jsonObject.body.quest.iname;
            GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
            GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
            DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
          }
          else if (this.API != FlowNode_MultiPlayAPI.EAPI.VERSUS_LINE_REQ && this.API != FlowNode_MultiPlayAPI.EAPI.VERSUS_LINE_MAKE)
          {
            if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_STATUS)
            {
              WebAPI.JSON_BodyResponse<ReqVersusStatus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusStatus.Response>>(www.text);
              if (jsonObject == null)
              {
                this.OnFailed();
                return;
              }
              GameManager instance = MonoSingleton<GameManager>.Instance;
              instance.Player.SetTowerMatchInfo(jsonObject.body.floor, jsonObject.body.key, jsonObject.body.wincnt, jsonObject.body.is_give_season_gift != 0);
              instance.VersusTowerMatchBegin = !string.IsNullOrEmpty(jsonObject.body.vstower_id);
              instance.VersusTowerMatchReceipt = jsonObject.body.is_season_gift != 0;
              instance.VersusTowerMatchName = jsonObject.body.tower_iname;
              instance.VersusTowerMatchEndAt = jsonObject.body.end_at;
              instance.VersusCoinRemainCnt = jsonObject.body.daycnt;
              instance.VersusLastUid = jsonObject.body.last_enemyuid;
              instance.IsVSFirstWinRewardRecived = jsonObject.body.is_firstwin != 0;
              GlobalVars.SelectedQuestID = jsonObject.body.vstower_id;
              if (jsonObject.body.streakwins != null)
              {
                for (int index = 0; index < jsonObject.body.streakwins.Length; ++index)
                {
                  ReqVersusStatus.StreakStatus streakwin = jsonObject.body.streakwins[index];
                  switch (instance.SearchVersusJudgeType(streakwin.schedule_id, -1L))
                  {
                    case STREAK_JUDGE.AllPriod:
                      instance.VS_StreakWinCnt_NowAllPriod = streakwin.num;
                      instance.VS_StreakWinCnt_BestAllPriod = streakwin.best;
                      break;
                    case STREAK_JUDGE.Now:
                      instance.VS_StreakWinCnt_Now = streakwin.num;
                      instance.VS_StreakWinCnt_Best = streakwin.best;
                      break;
                  }
                }
              }
              if (jsonObject.body.enabletime != null)
              {
                instance.VSFreeExpiredTime = jsonObject.body.enabletime.expired;
                instance.VSFreeNextTime = jsonObject.body.enabletime.next;
                instance.VSDraftType = (VersusDraftType) jsonObject.body.enabletime.draft_type;
                instance.mVersusEnableId = jsonObject.body.enabletime.schedule_id;
                instance.VSDraftId = jsonObject.body.enabletime.draft_id;
                instance.VSDraftQuestId = jsonObject.body.enabletime.iname;
              }
              instance.Player.UpdateVersusTowerTrophyStates(jsonObject.body.tower_iname, jsonObject.body.floor);
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_SEASON)
            {
              WebAPI.JSON_BodyResponse<ReqVersusSeason.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusSeason.Response>>(www.text);
              if (jsonObject == null)
              {
                this.OnFailed();
                return;
              }
              PlayerData player = MonoSingleton<GameManager>.Instance.Player;
              player.VersusSeazonGiftReceipt = false;
              player.UnreadMailPeriod |= jsonObject.body.unreadmail == 1;
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_FRIEND)
            {
              WebAPI.JSON_BodyResponse<ReqVersusFriendScore.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusFriendScore.Response>>(www.text);
              if (jsonObject == null)
              {
                this.OnFailed();
                return;
              }
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.friends);
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.MT_STATUS)
            {
              WebAPI.JSON_BodyResponse<ReqMultiTwStatus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiTwStatus.Response>>(www.text);
              Debug.Log((object) www.text);
              if (jsonObject == null)
              {
                this.OnFailed();
                return;
              }
              GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.appid;
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.floors);
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.MT_JOIN)
            {
              WebAPI.JSON_BodyResponse<ReqMultiTwRoomJoin.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiTwRoomJoin.Response>>(www.text);
              DebugUtility.Assert(jsonObject != null, "res == null");
              if (jsonObject.body == null)
              {
                this.OnFailed();
                return;
              }
              if (jsonObject.body.quest == null || string.IsNullOrEmpty(jsonObject.body.quest.iname))
              {
                this.OnFailed();
                return;
              }
              GameManager instance = MonoSingleton<GameManager>.Instance;
              if (instance.GetMTChallengeFloor() < jsonObject.body.quest.floor)
              {
                Network.RemoveAPI();
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
                  return;
                ((Behaviour) this).set_enabled(false);
                this.ActivateOutputLinks(11000);
                return;
              }
              GlobalVars.SelectedMultiTowerID = jsonObject.body.quest.iname;
              GlobalVars.SelectedMultiTowerFloor = jsonObject.body.quest.floor;
              GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
              GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
              MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, GlobalVars.SelectedMultiTowerFloor);
              if (mtFloorParam != null)
              {
                QuestParam questParam = mtFloorParam.GetQuestParam();
                if (questParam != null)
                  GlobalVars.SelectedQuestID = questParam.iname;
              }
              DebugUtility.Log("JoinRoom  AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.RANKMATCH_START)
            {
              WebAPI.JSON_BodyResponse<ReqRankMatchStart.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchStart.Response>>(www.text);
              if (jsonObject == null)
              {
                this.OnFailed();
                return;
              }
              GameManager instance = MonoSingleton<GameManager>.Instance;
              PlayerData player = instance.Player;
              int _streak_win = 0;
              if (jsonObject.body.streakwin != null)
                _streak_win = jsonObject.body.streakwin.num;
              player.SetRankMatchInfo(jsonObject.body.rank, jsonObject.body.score, (RankMatchClass) jsonObject.body.type, jsonObject.body.bp, _streak_win, jsonObject.body.wincnt, jsonObject.body.losecnt);
              instance.RankMatchScheduleId = jsonObject.body.schedule_id;
              GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
              GameManager gameManager = instance;
              long num1 = 0;
              instance.RankMatchNextTime = num1;
              long num2 = num1;
              gameManager.RankMatchExpiredTime = num2;
              if (jsonObject.body.enabletime != null)
              {
                instance.RankMatchExpiredTime = jsonObject.body.enabletime.expired;
                instance.RankMatchNextTime = jsonObject.body.enabletime.next;
                GlobalVars.SelectedQuestID = jsonObject.body.enabletime.iname;
                long matchExpiredTime = instance.RankMatchExpiredTime;
                long num3 = TimeManager.FromDateTime(TimeManager.ServerTime);
                instance.RankMatchBegin = num3 < matchExpiredTime;
              }
              instance.RankMatchMatchedEnemies = jsonObject.body.enemies;
              GlobalVars.IsVersusDraftMode = false;
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.RANKMATCH_STATUS)
            {
              WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchStatus.Response>>(www.text);
              if (jsonObject == null)
              {
                this.OnFailed();
                return;
              }
              GameManager instance = MonoSingleton<GameManager>.Instance;
              instance.RankMatchScheduleId = jsonObject.body.schedule_id;
              instance.RankMatchRankingStatus = jsonObject.body.RankingStatus;
              GameManager gameManager = instance;
              long num1 = 0;
              instance.RankMatchNextTime = num1;
              long num2 = num1;
              gameManager.RankMatchExpiredTime = num2;
              if (jsonObject.body.enabletime != null)
              {
                instance.RankMatchExpiredTime = jsonObject.body.enabletime.expired;
                instance.RankMatchNextTime = jsonObject.body.enabletime.next;
                GlobalVars.SelectedQuestID = jsonObject.body.enabletime.iname;
                long matchExpiredTime = instance.RankMatchExpiredTime;
                long num3 = TimeManager.FromDateTime(TimeManager.ServerTime);
                instance.RankMatchBegin = num3 < matchExpiredTime;
              }
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_DRAFT)
            {
              WebAPI.JSON_BodyResponse<ReqVersusDraft.Response> res = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusDraft.Response>>(www.text);
              if (res == null)
              {
                this.OnFailed();
                return;
              }
              GameManager instance = MonoSingleton<GameManager>.Instance;
              List<VersusDraftUnitParam> versusDraftUnits = instance.GetVersusDraftUnits(instance.VSDraftId);
              if (versusDraftUnits == null || versusDraftUnits.Count < 16)
              {
                Debug.LogError((object) "VersusDraftUnits below than 16. It needs above than 16.");
                this.Failure();
                return;
              }
              if (res.body.draft_units == null || res.body.draft_units.Length != 16)
              {
                Debug.LogError((object) "The number of VersusDraftUnit is not 16.");
                this.Failure();
                return;
              }
              VersusDraftList.VersusDraftUnitList = new List<VersusDraftUnitParam>();
              for (int i = 0; i < res.body.draft_units.Length; ++i)
              {
                VersusDraftUnitParam versusDraftUnitParam = versusDraftUnits.Find((Predicate<VersusDraftUnitParam>) (vdup => vdup.DraftUnitId == res.body.draft_units[i].id));
                if (versusDraftUnitParam == null)
                {
                  Debug.LogError((object) ("Selecting ID invalid: " + (object) res.body.draft_units[i].id));
                  this.Failure();
                  return;
                }
                versusDraftUnitParam.IsHidden = res.body.draft_units[i].secret == 1;
                VersusDraftList.VersusDraftUnitList.Add(versusDraftUnitParam);
              }
              VersusDraftList.VersusDraftTurnOwn = res.body.turn_own == 1;
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_DRAFT_SELECTED)
            {
              WebAPI.JSON_BodyResponse<ReqVersusDraftSelect.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusDraftSelect.Response>>(www.text);
              if (jsonObject == null)
              {
                this.OnFailed();
                return;
              }
              VersusDraftList.DraftID = jsonObject.body.draft_id;
            }
            else if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_LOBBY)
            {
              WebAPI.JSON_BodyResponse<ReqVersusLobby.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusLobby.Response>>(www.text);
              if (jsonObject == null)
              {
                this.OnFailed();
                return;
              }
              GameManager instance = MonoSingleton<GameManager>.Instance;
              instance.RankMatchScheduleId = jsonObject.body.rankmatch_schedule_id;
              instance.RankMatchRankingStatus = jsonObject.body.RankMatchRankingStatus;
              GameManager gameManager = instance;
              long num1 = 0;
              instance.RankMatchNextTime = num1;
              long num2 = num1;
              gameManager.RankMatchExpiredTime = num2;
              if (jsonObject.body.rankmatch_enabletime != null)
              {
                instance.RankMatchExpiredTime = jsonObject.body.rankmatch_enabletime.expired;
                instance.RankMatchNextTime = jsonObject.body.rankmatch_enabletime.next;
                GlobalVars.SelectedQuestID = jsonObject.body.rankmatch_enabletime.iname;
                long matchExpiredTime = instance.RankMatchExpiredTime;
                long num3 = TimeManager.FromDateTime(TimeManager.ServerTime);
                instance.RankMatchBegin = num3 < matchExpiredTime;
              }
              instance.VSDraftType = (VersusDraftType) jsonObject.body.draft_type;
              instance.mVersusEnableId = (long) jsonObject.body.draft_schedule_id;
            }
          }
        }
        Network.RemoveAPI();
        this.Success();
      }
    }

    private enum EAPI
    {
      MAKE,
      ROOM,
      JOIN,
      UPDATE,
      VERSION,
      VERSUS_START,
      VERSUS_MAKE,
      VERSUS_JOIN,
      VERSUS_LINE_REQ,
      VERSUS_LINE_MAKE,
      VERSUS_STATUS,
      VERSUS_SEASON,
      VERSUS_FRIEND,
      MT_STATUS,
      MT_JOIN,
      RANKMATCH_START,
      RANKMATCH_STATUS,
      VERSUS_DRAFT,
      VERSUS_DRAFT_SELECTED,
      VERSUS_DRAFT_PARTY,
      VERSUS_LOBBY,
    }
  }
}
