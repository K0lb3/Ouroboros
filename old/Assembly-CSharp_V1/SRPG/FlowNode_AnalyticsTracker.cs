// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AnalyticsTracker
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Analytics/AnalyticsTracker", 32741)]
  public class FlowNode_AnalyticsTracker : FlowNode
  {
    public FlowNode_AnalyticsTracker.TrackingType trackingType;

    public override void OnActivate(int pinID)
    {
      switch (this.trackingType)
      {
        case FlowNode_AnalyticsTracker.TrackingType.StaminaReward_Video:
          AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.AP, AnalyticsManager.CurrencySubType.FREE, (long) GlobalVars.LastReward.Get().Stamina, "Video Ads", (Dictionary<string, object>) null);
          break;
        case FlowNode_AnalyticsTracker.TrackingType.StaminaReward_Milestone:
          AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.AP, AnalyticsManager.CurrencySubType.FREE, (long) GlobalVars.LastReward.Get().Stamina, "Milestone Rewards", (Dictionary<string, object>) null);
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_SKIP_Yes:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.confirm_skip1_yes", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "16.1"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_SKIP_No:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.confirm_skip1_no", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "16.2"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_Download:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.download_main", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "2"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_Download_Dialog:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.download_bg", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "3"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_Download_Start:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.download_bg_start", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "4"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_Movie_Intro:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.movie_intro", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "5"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_Movie_AnimeIntro:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.movie_anime", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "8"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_Skip2_Yes:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.confirm_skip2_yes", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "22.1"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_Skip2_No:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.confirm_skip2_no", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "22.2"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Player_New:
          AnalyticsManager.RecordNewPlayerLogin();
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Player_Guest:
          if (PlayerPrefs.GetInt("AccountLinked", 0) == 0)
          {
            AnalyticsManager.RecordGuestLogin();
            break;
          }
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Player_FB:
          if (PlayerPrefs.GetInt("AccountLinked", 0) == 1)
          {
            AnalyticsManager.RecordFacebookLogin();
            break;
          }
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_Movie_World:
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.movie_world", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "14"
            }
          });
          break;
        case FlowNode_AnalyticsTracker.TrackingType.Tutorial_BGDLC:
          if (BackgroundDownloader.Instance.IsEnabled)
          {
            AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.download_bg_yes", new Dictionary<string, object>()
            {
              {
                "step_number",
                (object) "3.1"
              }
            });
            break;
          }
          AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.download_bg_no", new Dictionary<string, object>()
          {
            {
              "step_number",
              (object) "3.2"
            }
          });
          break;
      }
      this.ActivateOutputLinks(1);
    }

    public enum TrackingType
    {
      StaminaReward_Video,
      StaminaReward_Milestone,
      Tutorial_SKIP_Yes,
      Tutorial_SKIP_No,
      Tutorial_Download,
      Tutorial_Download_Dialog,
      Tutorial_Download_Start,
      Tutorial_Movie_Intro,
      Tutorial_Movie_AnimeIntro,
      Tutorial_Skip2_Yes,
      Tutorial_Skip2_No,
      Player_New,
      Player_Guest,
      Player_FB,
      Tutorial_Movie_World,
      Tutorial_BGDLC,
    }
  }
}
