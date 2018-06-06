// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartQuest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(3, "NoMatchVersion", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(5, "NetworkSuccess", FlowNode.PinTypes.Output, 14)]
  [FlowNode.NodeType("System/Quest/Start", 32741)]
  [FlowNode.Pin(0, "Load", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "LoadMultiPlay", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(200, "LoadVersus", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(6, "ColoRankModify", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(10, "Resume", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Started", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(4, "MultiMaintenance", FlowNode.PinTypes.Output, 13)]
  public class FlowNode_StartQuest : FlowNode_Network
  {
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

    public override void OnActivate(int pinID)
    {
      int num1 = pinID;
      if (pinID == 0 || pinID == 100 || pinID == 200)
      {
        PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay = pinID == 100 || pinID == 200;
        PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus = pinID == 200;
        pinID = 0;
      }
      if (pinID == 10)
      {
        this.mResume = true;
        pinID = 0;
      }
      if (pinID != 0 || ((Behaviour) this).get_enabled())
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
        GameManager instance = MonoSingleton<GameManager>.Instance;
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
            ArenaPlayer selectedArenaPlayer = (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
            this.ExecRequest((WebAPI) new ReqBtlColoReq(this.mStartingQuest.iname, selectedArenaPlayer.FUID, selectedArenaPlayer, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), partyIndex2));
          }
          else
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            FlowNode_StartQuest.\u003COnActivate\u003Ec__AnonStorey20E activateCAnonStorey20E = new FlowNode_StartQuest.\u003COnActivate\u003Ec__AnonStorey20E();
            // ISSUE: reference to a compiler-generated field
            activateCAnonStorey20E.pt = PunMonoSingleton<MyPhoton>.Instance;
            bool multi = false;
            bool isHost = false;
            int seat = -1;
            int plid = -1;
            string uid = string.Empty;
            // ISSUE: reference to a compiler-generated field
            if (Object.op_Inequality((Object) activateCAnonStorey20E.pt, (Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              multi = activateCAnonStorey20E.pt.IsMultiPlay;
              // ISSUE: reference to a compiler-generated field
              isHost = activateCAnonStorey20E.pt.IsOldestPlayer();
              // ISSUE: reference to a compiler-generated field
              seat = activateCAnonStorey20E.pt.MyPlayerIndex;
              // ISSUE: reference to a compiler-generated field
              MyPhoton.MyPlayer myPlayer = activateCAnonStorey20E.pt.GetMyPlayer();
              if (myPlayer != null)
                plid = myPlayer.playerID;
              // ISSUE: reference to a compiler-generated field
              if (activateCAnonStorey20E.pt.IsMultiVersus)
              {
                // ISSUE: reference to a compiler-generated field
                List<JSON_MyPhotonPlayerParam> myPlayersStarted = activateCAnonStorey20E.pt.GetMyPlayersStarted();
                // ISSUE: reference to a compiler-generated field
                MyPhoton.MyRoom currentRoom = activateCAnonStorey20E.pt.GetCurrentRoom();
                int num2 = currentRoom == null ? 1 : currentRoom.playerCount;
                // ISSUE: reference to a compiler-generated method
                JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted.Find(new Predicate<JSON_MyPhotonPlayerParam>(activateCAnonStorey20E.\u003C\u003Em__1F5));
                if (photonPlayerParam != null)
                  uid = photonPlayerParam.UID;
                if (string.IsNullOrEmpty(uid) || num2 == 1)
                {
                  this.OnVersusNoPlayer();
                  return;
                }
              }
            }
            if (num1 == 200)
              this.ExecRequest((WebAPI) new ReqVersus(this.mStartingQuest.iname, plid, seat, uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), GlobalVars.SelectedMultiPlayVersusType));
            else
              this.ExecRequest((WebAPI) new ReqBtlComReq(this.mStartingQuest.iname, GlobalVars.SelectedFriendID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), multi, partyIndex2, isHost, plid, seat));
          }
        }
        else
          this.StartCoroutine(this.StartScene((BattleCore.Json_Battle) null));
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

    public void OnColoRankModify()
    {
      CriticalSection.Leave(CriticalSections.SceneChange);
      string errMsg = Network.ErrMsg;
      Network.RemoveAPI();
      Network.ResetError();
      SRPG_TouchInputModule.LockInput();
      UIUtility.SystemMessage((string) null, errMsg, (UIUtility.DialogResultEvent) (go =>
      {
        SRPG_TouchInputModule.UnlockInput(false);
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(6);
      }), (GameObject) null, true, -1);
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
      if (Object.op_Inequality((Object) instance, (Object) null) && instance.IsOldestPlayer())
        instance.OpenRoom();
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
          case Network.EErrCode.ColoCantSelect:
            this.OnBack();
            break;
          case Network.EErrCode.ColoIsBusy:
            this.OnBack();
            break;
          case Network.EErrCode.ColoCostShort:
            this.OnFailed();
            break;
          case Network.EErrCode.ColoIntervalShort:
            this.OnBack();
            break;
          case Network.EErrCode.ColoBattleNotEnd:
            this.OnFailed();
            break;
          case Network.EErrCode.ColoPlayerLvShort:
            this.OnBack();
            break;
          case Network.EErrCode.ColoRankLower:
label_21:
            this.OnColoRankModify();
            break;
          default:
            switch (errCode - 10000)
            {
              case Network.EErrCode.Success:
                this.OnFailed();
                return;
              case Network.EErrCode.Unknown:
                this.OnFailed();
                return;
              case Network.EErrCode.Version:
                this.OnFailed();
                return;
              case (Network.EErrCode) 6:
                this.OnFailed();
                return;
              default:
                switch (errCode - 10011)
                {
                  case Network.EErrCode.Success:
                    this.OnFailed();
                    return;
                  case Network.EErrCode.Version:
                    this.OnFailed();
                    return;
                  case (Network.EErrCode) 4:
                    this.OnFailed();
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
                        if (errCode != Network.EErrCode.MultiMaintenance)
                        {
                          if (errCode != Network.EErrCode.QuestEnd)
                          {
                            if (errCode != Network.EErrCode.NoBtlInfo)
                            {
                              if (errCode != Network.EErrCode.MultiVersionMismatch)
                              {
                                if (errCode != Network.EErrCode.ColoRankModify)
                                {
                                  this.OnRetry();
                                  return;
                                }
                                goto label_21;
                              }
                              else
                              {
                                this.OnMismatchVersion();
                                return;
                              }
                            }
                            else
                            {
                              this.OnFailed();
                              return;
                            }
                          }
                          else
                          {
                            this.OnFailed();
                            return;
                          }
                        }
                        else
                        {
                          this.OnMultiMaintenance();
                          return;
                        }
                    }
                }
            }
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(www.text);
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          this.ActivateOutputLinks(5);
          if (this.mResume && (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L && jsonObject.body.btlinfo.qid == "QE_OP_0002")
            MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
          this.StartCoroutine(this.StartScene(jsonObject.body));
        }
      }
    }

    [DebuggerHidden]
    protected IEnumerator StartScene(BattleCore.Json_Battle json)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_StartQuest.\u003CStartScene\u003Ec__Iterator8A() { json = json, \u003C\u0024\u003Ejson = json, \u003C\u003Ef__this = this };
    }

    private void OnSceneAwake(GameObject scene)
    {
      SceneBattle component = (SceneBattle) scene.GetComponent<SceneBattle>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      CriticalSection.Leave(CriticalSections.SceneChange);
      CriticalSection.Leave(CriticalSections.SceneChange);
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
      component.StartQuest(this.mStartingQuest.iname, this.mQuestData);
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    private class QuestLauncher
    {
      public QuestParam Quest;
      public BattleCore.Json_Battle InitData;
      public bool Resume;

      public void OnSceneAwake(GameObject scene)
      {
        SceneBattle component = (SceneBattle) scene.GetComponent<SceneBattle>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        CriticalSection.Leave(CriticalSections.SceneChange);
        CriticalSection.Leave(CriticalSections.SceneChange);
        SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
        component.StartQuest(this.Quest.iname, this.InitData);
      }
    }
  }
}
