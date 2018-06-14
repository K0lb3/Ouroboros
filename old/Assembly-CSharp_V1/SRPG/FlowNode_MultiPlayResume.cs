// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayResume
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(200, "ResumeMulti", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(202, "ResumeVersus", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(300, "Success", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(301, "Failure", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5000, "NoMatchVersion", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(6000, "MultiResumeMaintenance", FlowNode.PinTypes.Output, 6000)]
  [FlowNode.Pin(7000, "NoRoom", FlowNode.PinTypes.Output, 7000)]
  [FlowNode.NodeType("Multi/MultiPlayResume", 32741)]
  [FlowNode.Pin(201, "ResumeMultiQuest", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_MultiPlayResume : FlowNode_StartQuest
  {
    public static Json_BtlInfo_Multi BtlInfo;
    public FlowNode_MultiPlayResume.RESUME_TYPE mType;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 200:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.ExecRequest((WebAPI) new ReqMultiPlayResume((long) GlobalVars.BtlID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 201:
          PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay = true;
          CriticalSection.Enter(CriticalSections.SceneChange);
          this.mStartingQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          if (!string.IsNullOrEmpty(this.QuestID))
          {
            GlobalVars.SelectedQuestID = this.QuestID;
            GlobalVars.SelectedFriendID = string.Empty;
          }
          if (FlowNode_MultiPlayResume.BtlInfo == null)
            break;
          MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
          if (Object.op_Inequality((Object) instance, (Object) null))
          {
            instance.IsMultiPlay = true;
            instance.IsMultiVersus = this.mType == FlowNode_MultiPlayResume.RESUME_TYPE.VERSUS;
          }
          this.StartCoroutine(this.StartScene(new BattleCore.Json_Battle()
          {
            btlinfo = (BattleCore.Json_BtlInfo) FlowNode_MultiPlayResume.BtlInfo,
            btlid = (long) GlobalVars.BtlID
          }));
          break;
        case 202:
          if (Network.Mode != Network.EConnectMode.Online)
          {
            this.Failure();
            break;
          }
          ((Behaviour) this).set_enabled(true);
          this.ExecRequest((WebAPI) new ReqVersusResume((long) GlobalVars.BtlID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        default:
          base.OnActivate(pinID);
          break;
      }
    }

    private void Failure()
    {
      DebugUtility.Log("MultiPlay Resume Failure");
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(301);
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
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(6000);
            break;
          case Network.EErrCode.MultiVersionMismatch:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(5000);
            break;
          case Network.EErrCode.RoomNoRoom:
          case Network.EErrCode.VS_NoRoom:
            Network.RemoveAPI();
            Network.ResetError();
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(7000);
            break;
          default:
            this.OnFailed();
            break;
        }
      }
      else
      {
        if (this.mType == FlowNode_MultiPlayResume.RESUME_TYPE.MULTI)
        {
          WebAPI.JSON_BodyResponse<ReqMultiPlayResume.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqMultiPlayResume.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnFailed();
            return;
          }
          GlobalVars.SelectedQuestID = jsonObject.body.quest.iname;
          GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
          GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
          GlobalVars.ResumeMultiplayPlayerID = int.Parse(jsonObject.body.btlinfo.plid);
          GlobalVars.ResumeMultiplaySeatID = int.Parse(jsonObject.body.btlinfo.seat);
          FlowNode_MultiPlayResume.BtlInfo = jsonObject.body.btlinfo;
        }
        else if (this.mType == FlowNode_MultiPlayResume.RESUME_TYPE.VERSUS)
        {
          WebAPI.JSON_BodyResponse<ReqVersusResume.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusResume.Response>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            this.OnFailed();
            return;
          }
          GlobalVars.SelectedQuestID = jsonObject.body.quest.iname;
          GlobalVars.SelectedMultiPlayPhotonAppID = jsonObject.body.app_id;
          GlobalVars.SelectedMultiPlayRoomName = jsonObject.body.token;
          GlobalVars.ResumeMultiplayPlayerID = int.Parse(jsonObject.body.btlinfo.plid);
          GlobalVars.ResumeMultiplaySeatID = int.Parse(jsonObject.body.btlinfo.seat);
          if (string.Compare(jsonObject.body.type, VERSUS_TYPE.Free.ToString().ToLower()) == 0)
            GlobalVars.SelectedMultiPlayVersusType = VERSUS_TYPE.Free;
          else if (string.Compare(jsonObject.body.type, VERSUS_TYPE.Tower.ToString().ToLower()) == 0)
          {
            MonoSingleton<GameManager>.Instance.VersusTowerMatchBegin = true;
            GlobalVars.SelectedMultiPlayVersusType = VERSUS_TYPE.Tower;
          }
          else if (string.Compare(jsonObject.body.type, VERSUS_TYPE.Friend.ToString().ToLower()) == 0)
            GlobalVars.SelectedMultiPlayVersusType = VERSUS_TYPE.Friend;
          FlowNode_MultiPlayResume.BtlInfo = jsonObject.body.btlinfo;
        }
        Network.RemoveAPI();
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(300);
      }
    }

    public enum RESUME_TYPE
    {
      MULTI,
      VERSUS,
    }
  }
}
