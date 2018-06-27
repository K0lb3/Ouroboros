// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(200, "LoadVersus", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(301, "AudienceFailedMax", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(400, "NotGpsQuest", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(600, "NotRoomMT", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(300, "AudienceFailed", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(100, "LoadMultiPlay", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Quest/Start", 32741)]
  [FlowNode.Pin(10, "Resume", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(20, "AudienceConnect", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(30, "AudienceStart", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(1, "Started", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(500, "LoadMultiTower", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(3, "NoMatchVersion", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(4, "MultiMaintenance", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(5, "NetworkSuccess", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(6, "ColoRankModify", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(7, "MatchSuccess", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(8, "NoRoom", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(9, "NoAudienceData", FlowNode.PinTypes.Output, 18)]
  public class FlowNode_StartQuest : FlowNode_Network
  {
    public int mReqID = -1;
    [HideInInspector]
    public string QuestID;
    public bool ReplaceScene;
    [HideInInspector]
    public bool PlayOffline;
    protected bool mResume;
    [HideInInspector]
    public RestorePoints RestorePoint;
    public bool SetRestorePoint;
    private BattleCore.Json_Battle mQuestData;
    protected QuestParam mStartingQuest;
    private float mConnectTime;

    public override void OnActivate(int pinID)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      instance.AudienceMode = false;
      this.mReqID = pinID;
      if (pinID == 0 || pinID == 100 || (pinID == 200 || pinID == 500))
      {
        PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay = pinID == 100 || pinID == 200 || pinID == 500;
        PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus = pinID == 200;
        pinID = 0;
      }
      if (pinID == 10)
      {
        this.mResume = true;
        pinID = 0;
      }
      if (pinID == 0)
      {
        if (((Behaviour) this).get_enabled())
          return;
        ((Behaviour) this).set_enabled(true);
        CriticalSection.Enter(CriticalSections.SceneChange);
        if (this.mResume)
        {
          long btlId = (long) GlobalVars.BtlID;
          GlobalVars.BtlID.Set(0L);
          this.ExecRequest((WebAPI) new ReqBtlComResume(btlId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
        else
        {
          this.mStartingQuest = instance.FindQuest(GlobalVars.SelectedQuestID);
          PlayerPartyTypes partyIndex1 = this.QuestToPartyIndex(this.mStartingQuest.type);
          if (!string.IsNullOrEmpty(this.QuestID))
          {
            GlobalVars.SelectedQuestID = this.QuestID;
            GlobalVars.SelectedFriendID = string.Empty;
          }
          if (!this.PlayOffline && Network.Mode == Network.EConnectMode.Online)
          {
            PartyData partyOfType = instance.Player.FindPartyOfType(partyIndex1);
            int partyIndex2 = instance.Player.Partys.IndexOf(partyOfType);
            if (this.mStartingQuest.type == QuestTypes.Arena)
            {
              this.ActivateOutputLinks(5);
              this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
            }
            else
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              FlowNode_StartQuest.\u003COnActivate\u003Ec__AnonStorey2CC activateCAnonStorey2Cc = new FlowNode_StartQuest.\u003COnActivate\u003Ec__AnonStorey2CC();
              // ISSUE: reference to a compiler-generated field
              activateCAnonStorey2Cc.pt = PunMonoSingleton<MyPhoton>.Instance;
              bool multi = false;
              bool isHost = false;
              int seat = -1;
              int plid = -1;
              string uid = string.Empty;
              List<string> stringList = new List<string>();
              // ISSUE: reference to a compiler-generated field
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) activateCAnonStorey2Cc.pt, (UnityEngine.Object) null))
              {
                // ISSUE: reference to a compiler-generated field
                multi = activateCAnonStorey2Cc.pt.IsMultiPlay;
                // ISSUE: reference to a compiler-generated field
                isHost = activateCAnonStorey2Cc.pt.IsOldestPlayer();
                // ISSUE: reference to a compiler-generated field
                seat = activateCAnonStorey2Cc.pt.MyPlayerIndex;
                // ISSUE: reference to a compiler-generated field
                MyPhoton.MyPlayer myPlayer = activateCAnonStorey2Cc.pt.GetMyPlayer();
                if (myPlayer != null)
                  plid = myPlayer.playerID;
                // ISSUE: reference to a compiler-generated field
                if (activateCAnonStorey2Cc.pt.IsMultiVersus)
                {
                  // ISSUE: reference to a compiler-generated field
                  List<JSON_MyPhotonPlayerParam> myPlayersStarted = activateCAnonStorey2Cc.pt.GetMyPlayersStarted();
                  // ISSUE: reference to a compiler-generated field
                  MyPhoton.MyRoom currentRoom = activateCAnonStorey2Cc.pt.GetCurrentRoom();
                  int num = currentRoom == null ? 1 : currentRoom.playerCount;
                  // ISSUE: reference to a compiler-generated method
                  JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted.Find(new Predicate<JSON_MyPhotonPlayerParam>(activateCAnonStorey2Cc.\u003C\u003Em__2AB));
                  if (photonPlayerParam != null)
                    uid = photonPlayerParam.UID;
                  if (string.IsNullOrEmpty(uid) || num == 1)
                  {
                    this.OnVersusNoPlayer();
                    return;
                  }
                }
                else
                {
                  // ISSUE: reference to a compiler-generated field
                  List<JSON_MyPhotonPlayerParam> myPlayersStarted = activateCAnonStorey2Cc.pt.GetMyPlayersStarted();
                  for (int index = 0; index < myPlayersStarted.Count; ++index)
                  {
                    // ISSUE: reference to a compiler-generated field
                    if (myPlayersStarted[index].playerIndex != activateCAnonStorey2Cc.pt.MyPlayerIndex)
                      stringList.Add(myPlayersStarted[index].UID);
                  }
                }
              }
              if (this.mReqID == 200)
                this.ExecRequest((WebAPI) new ReqVersus(this.mStartingQuest.iname, plid, seat, uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.SelectedMultiPlayVersusType));
              else if (this.mReqID == 500)
                this.ExecRequest((WebAPI) new ReqBtlMultiTwReq(this.mStartingQuest.iname, partyIndex2, plid, seat, stringList.ToArray(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
              else
                this.ExecRequest((WebAPI) new ReqBtlComReq(this.mStartingQuest.iname, GlobalVars.SelectedFriendID, GlobalVars.SelectedSupport.Get(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), multi, partyIndex2, isHost, plid, seat, GlobalVars.Location, GlobalVars.SelectedRankingQuestParam));
            }
          }
          else
            this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
        }
      }
      else if (pinID == 20)
      {
        if (instance.AudienceRoom == null)
          return;
        this.StartCoroutine(this.StartAudience());
      }
      else
      {
        if (pinID != 30)
          return;
        if (Network.IsError)
        {
          this.ActivateOutputLinks(300);
          Network.ResetError();
        }
        else if (!Network.IsStreamConnecting)
        {
          Network.ResetError();
          this.ActivateOutputLinks(300);
        }
        else
        {
          VersusAudienceManager audienceManager = instance.AudienceManager;
          audienceManager.AddStartQuest();
          if (audienceManager.GetStartedParam() != null)
          {
            if (audienceManager.GetStartedParam().btlinfo != null)
            {
              BattleCore.Json_Battle json = new BattleCore.Json_Battle();
              json.btlinfo = audienceManager.GetStartedParam().btlinfo;
              CriticalSection.Enter(CriticalSections.SceneChange);
              instance.AudienceMode = true;
              this.StartCoroutine(this.StartScene(json));
            }
            else
            {
              DebugUtility.LogError("Not Exist btlInfo");
              if (audienceManager.IsRetryError)
              {
                Network.Abort();
                this.ActivateOutputLinks(300);
              }
              else
                this.ActivateOutputLinks(9);
            }
          }
          else
          {
            DebugUtility.LogError("Not Exist StartParam");
            if (audienceManager.IsRetryError)
            {
              Network.Abort();
              this.ActivateOutputLinks(300);
            }
            else
              this.ActivateOutputLinks(9);
          }
        }
      }
    }

    public PlayerPartyTypes QuestToPartyIndex(QuestTypes type)
    {
      switch (type)
      {
        case QuestTypes.Multi:
          return PlayerPartyTypes.Multiplay;
        case QuestTypes.Arena:
          return PlayerPartyTypes.Arena;
        case QuestTypes.Free:
        case QuestTypes.Extra:
          return PlayerPartyTypes.Event;
        case QuestTypes.Character:
          return PlayerPartyTypes.Character;
        case QuestTypes.Tower:
          return PlayerPartyTypes.Tower;
        case QuestTypes.VersusFree:
        case QuestTypes.VersusRank:
          return PlayerPartyTypes.Versus;
        default:
          return PlayerPartyTypes.Normal;
      }
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
    }

    private void OnSceneLoad(GameObject sceneRoot)
    {
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
      CriticalSection.Leave(CriticalSections.SceneChange);
    }

    public override void OnBack()
    {
      CriticalSection.Leave(CriticalSections.SceneChange);
      base.OnBack();
    }

    public void OnMismatchVersion()
    {
      ((Behaviour) this).set_enabled(false);
      CriticalSection.Leave(CriticalSections.SceneChange);
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(3);
    }

    public void OnMultiMaintenance()
    {
      ((Behaviour) this).set_enabled(false);
      CriticalSection.Leave(CriticalSections.SceneChange);
      Network.RemoveAPI();
      this.ActivateOutputLinks(4);
    }

    public void OnVersusNoPlayer()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsOldestPlayer())
        instance.OpenRoom(true, false);
      ((Behaviour) this).set_enabled(false);
      CriticalSection.Leave(CriticalSections.SceneChange);
      Network.RemoveAPI();
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.VS_NotSelfBattle:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_NotPlayer:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_NotQuestInfo:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_NotQuestData:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_BattleNotEnd:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_ComBattleEnd:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_TowerNotPlay:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_NotContinuousEnemy:
            this.OnFailed();
            break;
          case Network.EErrCode.VS_RowerNotMatching:
            this.OnFailed();
            break;
          default:
            switch (errCode - 3800)
            {
              case Network.EErrCode.Success:
                this.OnBack();
                return;
              case Network.EErrCode.Unknown:
                this.OnBack();
                return;
              case Network.EErrCode.Version:
                this.OnFailed();
                return;
              case Network.EErrCode.AssetVersion:
                this.OnBack();
                return;
              case Network.EErrCode.NoVersionDbg:
                this.OnFailed();
                return;
              case Network.EErrCode.Unknown | Network.EErrCode.NoVersionDbg:
                this.OnBack();
                return;
              default:
                switch (errCode - 202)
                {
                  case Network.EErrCode.Success:
                  case Network.EErrCode.Unknown:
                  case Network.EErrCode.AssetVersion:
                  case Network.EErrCode.NoVersionDbg:
                    this.OnMultiMaintenance();
                    return;
                  default:
                    switch (errCode - 3300)
                    {
                      case Network.EErrCode.Success:
                        this.OnBack();
                        return;
                      case Network.EErrCode.Unknown:
                        this.OnBack();
                        return;
                      case Network.EErrCode.AssetVersion:
                        this.OnBack();
                        return;
                      default:
                        switch (errCode - 12001)
                        {
                          case Network.EErrCode.Success:
                            this.OnFailed();
                            return;
                          case Network.EErrCode.Version:
                            CriticalSection.Leave(CriticalSections.SceneChange);
                            Network.RemoveAPI();
                            ((Behaviour) this).set_enabled(false);
                            this.ActivateOutputLinks(600);
                            return;
                          default:
                            if (errCode != Network.EErrCode.NotLocation)
                            {
                              if (errCode != Network.EErrCode.NotGpsQuest)
                              {
                                if (errCode != Network.EErrCode.QuestEnd)
                                {
                                  if (errCode != Network.EErrCode.NoBtlInfo)
                                  {
                                    if (errCode == Network.EErrCode.MultiVersionMismatch)
                                    {
                                      this.OnMismatchVersion();
                                      return;
                                    }
                                    this.OnRetry();
                                    return;
                                  }
                                  this.OnFailed();
                                  return;
                                }
                                this.OnFailed();
                                return;
                              }
                              CriticalSection.Leave(CriticalSections.SceneChange);
                              Network.RemoveAPI();
                              Network.ResetError();
                              this.ActivateOutputLinks(400);
                              ((Behaviour) this).set_enabled(false);
                              return;
                            }
                            this.OnBack();
                            return;
                        }
                    }
                }
            }
        }
      }
      else if (this.mReqID == 30)
      {
        Network.RemoveAPI();
        this.ActivateOutputLinks(5);
      }
      else
      {
        string text = www.text;
        DebugMenu.Log("API", "StartQuest:" + www.text);
        WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(www.text);
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          this.ActivateOutputLinks(5);
          this.SetVersusAudienceParam(text);
          if (this.mResume && (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L && jsonObject.body.btlinfo.qid == "QE_OP_0002")
            MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
          this.StartCoroutine(this.StartScene(jsonObject.body));
        }
      }
    }

    private void SetVersusAudienceParam(string text)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (!instance.IsMultiVersus)
        return;
      if (instance.IsOldestPlayer())
      {
        int startIndex = text.IndexOf("\"btlinfo\"");
        if (startIndex != -1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          string str = text.Substring(startIndex);
          string roomParam = instance.GetRoomParam("started");
          if (!string.IsNullOrEmpty(roomParam))
          {
            stringBuilder.Append(roomParam);
            --stringBuilder.Length;
            stringBuilder.Append(",");
            stringBuilder.Append(str);
            --stringBuilder.Length;
            instance.SetRoomParam("started", stringBuilder.ToString());
          }
        }
      }
      instance.BattleStartRoom();
    }

    [DebuggerHidden]
    protected IEnumerator StartScene(BattleCore.Json_Battle json)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartScene\u003Ec__IteratorC9() { json = json, \u003C\u0024\u003Ejson = json, \u003C\u003Ef__this = this };
    }

    private void OnSceneAwake(GameObject scene)
    {
      SceneBattle component = (SceneBattle) scene.GetComponent<SceneBattle>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      CriticalSection.Leave(CriticalSections.SceneChange);
      CriticalSection.Leave(CriticalSections.SceneChange);
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
      component.StartQuest(this.mStartingQuest.iname, this.mQuestData);
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    [DebuggerHidden]
    private IEnumerator StartAudience()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartAudience\u003Ec__IteratorCA() { \u003C\u003Ef__this = this };
    }

    private class QuestLauncher
    {
      public QuestParam Quest;
      public BattleCore.Json_Battle InitData;
      public bool Resume;

      public void OnSceneAwake(GameObject scene)
      {
        SceneBattle component = (SceneBattle) scene.GetComponent<SceneBattle>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        CriticalSection.Leave(CriticalSections.SceneChange);
        CriticalSection.Leave(CriticalSections.SceneChange);
        SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
        component.StartQuest(this.Quest.iname, this.InitData);
      }
    }
  }
}
