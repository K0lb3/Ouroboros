// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResumeTutorial
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
  [FlowNode.Pin(3, "Resume Quest", FlowNode.PinTypes.Output, 3)]
  [FlowNode.NodeType("System/Tutorial")]
  [FlowNode.Pin(0, "Try", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Next Step", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "DebugEndMovieLoad", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(2, "Start Quest", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(4, "Tutorial Skipped", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "Resume Tower", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(6, "Resume Multi", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(7, "ClearResumeMulti", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(8, "ResumeTowerError", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(11, "DebugMovieLoad", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "ClearTutorial", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "Resume Multi Cancel", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "Resume Versus", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(15, "Resume Versus Cancel", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(16, "ClearResumeVersus", FlowNode.PinTypes.Input, 16)]
  [FlowNode.Pin(17, "GotoHome", FlowNode.PinTypes.Input, 17)]
  [FlowNode.Pin(18, "FgGChainWish", FlowNode.PinTypes.Output, 18)]
  public class FlowNode_ResumeTutorial : FlowNode
  {
    private bool mSkipTutorial;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          GameManager instance = MonoSingleton<GameManager>.Instance;
          ((Behaviour) this).set_enabled(true);
          if ((long) GlobalVars.BtlID != 0L)
          {
            if ((instance.Player.TutorialFlags & 1L) == 0L)
            {
              this.ActivateOutputLinks(3);
              break;
            }
            switch (GlobalVars.QuestType)
            {
              case QuestTypes.Multi:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnMultiResumeAccept), new UIUtility.DialogResultEvent(this.OnMultiResumeCancel), (GameObject) null, false, -1);
                return;
              case QuestTypes.Tower:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnTowerResumeAccept), new UIUtility.DialogResultEvent(this.OnTowerResumeCancel), (GameObject) null, false, -1);
                return;
              case QuestTypes.VersusFree:
              case QuestTypes.VersusRank:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnVersusAccept), new UIUtility.DialogResultEvent(this.OnVersusResumeCancel), (GameObject) null, false, -1);
                return;
              default:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnResumeCancel), (GameObject) null, false, -1);
                return;
            }
          }
          else
          {
            BattleCore.RemoveSuspendData();
            if ((instance.Player.TutorialFlags & 1L) != 0L)
            {
              GlobalVars.IsTutorialEnd = true;
              if (MonoSingleton<GameManager>.Instance.AuthStatus == ReqFgGAuth.eAuthStatus.Synchronized)
              {
                this.LoadStartScene();
                break;
              }
              this.ActivateOutputLinks(18);
              break;
            }
            instance.UpdateTutorialStep();
            if (instance.TutorialStep == 0 && GameUtility.IsDebugBuild)
            {
              if (GlobalVars.DebugIsPlayTutorial)
              {
                this.PlayTutorial();
                break;
              }
              this.mSkipTutorial = true;
              this.CompleteTutorial();
              break;
            }
            this.PlayTutorial();
            break;
          }
        case 1:
          MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
          this.PlayTutorial();
          break;
        case 7:
          this.ClearMultiResumeData();
          break;
        case 8:
          this.ClearTowerResumeData();
          break;
        case 10:
          this.PlayTutorial();
          break;
        case 16:
          this.ClearVersusResumeData();
          break;
        case 17:
          this.LoadStartScene();
          break;
      }
    }

    private void OnResumeAccept(GameObject go)
    {
      this.ActivateOutputLinks(3);
    }

    private void OnTowerResumeAccept(GameObject go)
    {
      this.ActivateOutputLinks(5);
    }

    private void OnMultiResumeAccept(GameObject go)
    {
      this.ActivateOutputLinks(6);
    }

    private void OnVersusAccept(GameObject go)
    {
      this.ActivateOutputLinks(14);
    }

    private void OnResumeCancel(GameObject go)
    {
      this.ClearResumeData();
    }

    private void OnTowerResumeCancel(GameObject go)
    {
      this.ClearTowerResumeData();
    }

    private void OnMultiResumeCancel(GameObject go)
    {
      this.ActivateOutputLinks(13);
    }

    private void OnVersusResumeCancel(GameObject go)
    {
      this.ActivateOutputLinks(15);
    }

    private void OnPlayTutorial(GameObject go)
    {
      this.ActivateOutputLinks(11);
    }

    private void OnSkipTutorial(GameObject go)
    {
      this.mSkipTutorial = true;
      this.CompleteTutorial();
    }

    private void PlayTutorial()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      string nextTutorialStep = instance.GetNextTutorialStep();
      if (string.IsNullOrEmpty(nextTutorialStep))
      {
        this.CompleteTutorial();
      }
      else
      {
        if (BackgroundDownloader.Instance.IsEnabled)
          ProgressWindow.Close();
        if (instance.FindQuest(nextTutorialStep) == null)
        {
          if (nextTutorialStep.Contains("_SG"))
          {
            string sceneName = nextTutorialStep.Split(new string[1]{ "_SG" }, StringSplitOptions.None)[0];
            Debug.LogWarning((object) ("SG Tutorial: " + nextTutorialStep + ", going to scene: " + sceneName));
            instance.CompleteTutorialStep();
            this.StartCoroutine(this.LoadSceneAsync(sceneName));
          }
          else
            this.StartCoroutine(this.LoadSceneAsync(nextTutorialStep));
        }
        else
        {
          GlobalVars.SelectedQuestID = nextTutorialStep;
          GlobalVars.SelectedFriendID = string.Empty;
          this.ActivateOutputLinks(2);
        }
      }
    }

    private void CompleteTutorial()
    {
      MonoSingleton<GameManager>.Instance.UpdateTutorialFlags(1L);
      this.StartCoroutine(this.WaitCompleteTutorialAsync());
    }

    [DebuggerHidden]
    private IEnumerator WaitCompleteTutorialAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResumeTutorial.\u003CWaitCompleteTutorialAsync\u003Ec__Iterator8E() { \u003C\u003Ef__this = this };
    }

    private void ClearResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqBtlComEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Cancel, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd), BtlEndTypes.com, (string) null, (string) null, (string) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearTowerResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqTowerBtlComEnd((long) GlobalVars.BtlID, (Unit[]) null, (Unit[]) null, 0, 0, (byte) 0, BtlResultTypes.Cancel, (RandDeckResult[]) null, new Network.ResponseCallback(this.OnBtlComEnd), (string) null, (string) null, (string) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearMultiResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqBtlComEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Cancel, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd), BtlEndTypes.multi, (string) null, (string) null, (string) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearVersusResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqVersusEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Retire, (int[]) null, (string) null, (string) null, new Network.ResponseCallback(this.OnBtlComEnd), VERSUS_TYPE.Free, (string) null, (string) null, (string) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void OnBtlComEnd(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www) || GlobalVars.QuestType == QuestTypes.Tower && TowerErrorHandle.Error((FlowNode_Network) null))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestEnd:
          case Network.EErrCode.ColoNoBattle:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            if (jsonObject.body.mails != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.mails);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          Network.RemoveAPI();
          this.LoadStartScene();
        }
      }
    }

    private void LoadStartScene()
    {
      this.StartCoroutine(this.LoadSceneAsync("Home"));
    }

    [DebuggerHidden]
    private IEnumerator LoadSceneAsync(string sceneName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResumeTutorial.\u003CLoadSceneAsync\u003Ec__Iterator8F() { sceneName = sceneName, \u003C\u0024\u003EsceneName = sceneName, \u003C\u003Ef__this = this };
    }
  }
}
