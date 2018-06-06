// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayAPI
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(12, "VersusRoomJoinID", FlowNode.PinTypes.Input, 12)]
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
  [FlowNode.Pin(4801, "IllegalComment", FlowNode.PinTypes.Output, 4801)]
  [FlowNode.Pin(4800, "FailedMakeRoom", FlowNode.PinTypes.Output, 4800)]
  [FlowNode.Pin(101, "Failure", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(19, "VersusRecvSeasonGift", FlowNode.PinTypes.Input, 19)]
  [FlowNode.Pin(18, "VersusStatus", FlowNode.PinTypes.Input, 18)]
  [FlowNode.Pin(17, "VersusLineJoin", FlowNode.PinTypes.Input, 17)]
  [FlowNode.Pin(15, "VersusLineReq", FlowNode.PinTypes.Input, 15)]
  [FlowNode.Pin(16, "VersusLineMake", FlowNode.PinTypes.Input, 16)]
  [FlowNode.Pin(9000, "VersusNotPhotonID", FlowNode.PinTypes.Output, 9000)]
  [FlowNode.Pin(10000, "VersusFaildSeasonGift", FlowNode.PinTypes.Output, 10000)]
  [FlowNode.Pin(8000, "VersusFailRoomID", FlowNode.PinTypes.Output, 8000)]
  [FlowNode.Pin(7000, "VersusNotLineRoom", FlowNode.PinTypes.Output, 7000)]
  [FlowNode.Pin(6000, "MultiMaintenance", FlowNode.PinTypes.Output, 6000)]
  [FlowNode.Pin(5000, "NoMatchVersion", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(4900, "NoRoom", FlowNode.PinTypes.Output, 4900)]
  [FlowNode.Pin(14, "VersusReset", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(13, "VersusRoomUpdate", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(11, "VersusCreateRoom", FlowNode.PinTypes.Input, 11)]
  public class FlowNode_MultiPlayAPI : FlowNode_Network
  {
    public static readonly int ROOMID_VALIDATE_MAX = 99999;
    public static float RoomMakeTime;
    public static ReqMPRoom.Response RoomList;
    public static readonly int ROOMID_VALIDATE_MIN;

    private FlowNode_MultiPlayAPI.EAPI API { get; set; }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
        case 4:
          string empty1 = string.Empty;
          string str1 = !PlayerPrefs.HasKey(PlayerData.ROOM_COMMENT_KEY) ? LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT") : PlayerPrefs.GetString(PlayerData.ROOM_COMMENT_KEY);
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
          bool isPrivate = flag;
          FlowNode_MultiPlayAPI.RoomMakeTime = Time.get_realtimeSinceStartup();
          if (Network.Mode != Network.EConnectMode.Online)
          {
            GlobalVars.SelectedMultiPlayRoomID = (int) (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            GlobalVars.SelectedMultiPlayPhotonAppID = string.Empty;
            GlobalVars.SelectedMultiPlayRoomName = Guid.NewGuid().ToString();
            DebugUtility.Log("MakeRoom RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID + " AppID:" + GlobalVars.SelectedMultiPlayPhotonAppID + " Name:" + GlobalVars.SelectedMultiPlayRoomName);
            this.Success();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.MAKE;
          this.ExecRequest((WebAPI) new ReqMPRoomMake(GlobalVars.SelectedQuestID, str1, GlobalVars.EditMultiPlayRoomPassCode, isPrivate, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 2:
        case 5:
          string fuid = pinID != 2 ? FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID : (string) null;
          DebugUtility.Log("ListRoom FUID:" + fuid);
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.StartCoroutine(this.GetPhotonRoomList(fuid));
            break;
          }
          if (pinID == 2)
            GlobalVars.SelectedMultiPlayArea = (string) null;
          FlowNode_MultiPlayAPI.RoomList = (ReqMPRoom.Response) null;
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.ROOM;
          this.ExecRequest((WebAPI) new ReqMPRoom(fuid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 3:
        case 6:
          if (FlowNode_MultiPlayAPI.RoomList == null || FlowNode_MultiPlayAPI.RoomList.rooms == null || FlowNode_MultiPlayAPI.RoomList.rooms.Length <= 0)
          {
            this.Failure();
            break;
          }
          bool LockRoom = false;
          if (pinID == 6)
          {
            if (FlowNode_MultiPlayAPI.RoomList.rooms.Length != 1 || FlowNode_MultiPlayAPI.RoomList.rooms[0] == null)
            {
              this.Failure();
              break;
            }
            GlobalVars.SelectedMultiPlayRoomID = FlowNode_MultiPlayAPI.RoomList.rooms[0].roomid;
            DebugUtility.Log("JoinRoom for LINE RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID);
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
          ((Behaviour) this).set_enabled(true);
          this.API = FlowNode_MultiPlayAPI.EAPI.JOIN;
          this.ExecRequest((WebAPI) new ReqMPRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), LockRoom));
          break;
        case 7:
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
          this.ExecRequest((WebAPI) new ReqMPRoomUpdate(GlobalVars.SelectedMultiPlayRoomID, GlobalVars.EditMultiPlayRoomComment, GlobalVars.EditMultiPlayRoomPassCode, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 8:
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
          this.API = FlowNode_MultiPlayAPI.EAPI.JOIN;
          this.ExecRequest((WebAPI) new ReqMPRoomJoin(GlobalVars.SelectedMultiPlayRoomID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), true));
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
          this.ExecRequest((WebAPI) new ReqVersusStart(GlobalVars.SelectedMultiPlayVersusType, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 11:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Success();
            break;
          }
          string empty2 = string.Empty;
          string str2 = !PlayerPrefs.HasKey(PlayerData.ROOM_COMMENT_KEY) ? LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT") : PlayerPrefs.GetString(PlayerData.ROOM_COMMENT_KEY);
          if (string.IsNullOrEmpty(str2))
            str2 = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
          if (!MyMsgInput.isLegal(str2))
          {
            DebugUtility.Log("comment is not legal");
            this.ActivateOutputLinks(4801);
            break;
          }
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
      }
    }

    private void Success()
    {
      DebugUtility.Log("MultiPlayAPI success");
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("MultiPlayAPI failure");
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(101);
    }

    [DebuggerHidden]
    private IEnumerator GetPhotonRoomList(string fuid)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiPlayAPI.\u003CGetPhotonRoomList\u003Ec__Iterator88() { fuid = fuid, \u003C\u0024\u003Efuid = fuid, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator GetPhotonRoomName()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiPlayAPI.\u003CGetPhotonRoomName\u003Ec__Iterator89() { \u003C\u003Ef__this = this };
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
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(6000);
            break;
          case Network.EErrCode.MultiVersionMismatch:
          case Network.EErrCode.VS_Version:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(5000);
            break;
          case Network.EErrCode.RoomFailedMakeRoom:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4800);
            break;
          case Network.EErrCode.RoomIllegalComment:
          case Network.EErrCode.VS_IllComment:
            if (this.API == FlowNode_MultiPlayAPI.EAPI.MAKE)
            {
              string str = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
              PlayerPrefs.SetString(PlayerData.ROOM_COMMENT_KEY, str);
            }
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4801);
            break;
          case Network.EErrCode.RoomNoRoom:
          case Network.EErrCode.VS_NoRoom:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(4900);
            break;
          case Network.EErrCode.VS_NotLINERoomInfo:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(7000);
            break;
          case Network.EErrCode.VS_FailRoomID:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(8000);
            break;
          case Network.EErrCode.VS_NotPhotonAppID:
            Network.RemoveAPI();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(9000);
            break;
          case Network.EErrCode.VS_FaildSeasonGift:
            Network.RemoveAPI();
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
        else if (this.API != FlowNode_MultiPlayAPI.EAPI.UPDATE && this.API != FlowNode_MultiPlayAPI.EAPI.VERSION)
        {
          if (this.API == FlowNode_MultiPlayAPI.EAPI.VERSUS_START)
          {
            WebAPI.JSON_BodyResponse<ReqVersusStart.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusStart.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnFailed();
              return;
            }
            GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
            switch (GlobalVars.SelectedMultiPlayVersusType)
            {
              case VERSUS_TYPE.Free:
                GlobalVars.SelectedQuestID = jsonObject.body.maps.free;
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
              GlobalVars.SelectedQuestID = jsonObject.body.vstower_id;
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
    }
  }
}
