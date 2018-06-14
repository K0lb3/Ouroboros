// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VideoAd
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace SRPG
{
  [FlowNode.Pin(1, "AdRewarded", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "AdFailed", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "NotAvailable", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "NotVideoAd", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "AdSkipped", FlowNode.PinTypes.Output, 5)]
  [FlowNode.NodeType("System/VideoAd", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_VideoAd : FlowNode
  {
    private bool isTryingToShowAd;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (GlobalVars.SelectedTrophy == null || !GlobalVars.SelectedTrophy.ToString().Contains("DAILY_GLAPVIDEO"))
      {
        this.ActivateOutputLinks(4);
      }
      else
      {
        if (this.isTryingToShowAd)
          return;
        ShowOptions videoOptions = new ShowOptions();
        videoOptions.set_resultCallback(new Action<ShowResult>(this.HandleShowAdResult));
        if (!this.TryToShowVideoAd(videoOptions, string.Empty))
        {
          this.isTryingToShowAd = false;
          this.ActivateOutputLinks(3);
        }
        else
          this.isTryingToShowAd = true;
      }
    }

    private bool TryToShowVideoAd(ShowOptions videoOptions, string placementName = "")
    {
      if (Advertisement.get_isSupported() && Advertisement.get_isInitialized() && Advertisement.IsReady())
      {
        Advertisement.Show(placementName, videoOptions);
        return true;
      }
      Debug.Log((object) ("Advert is Supported: " + (object) Advertisement.get_isSupported() + "\nAdvert was Initialized: " + (object) Advertisement.get_isInitialized() + "\nAdvert was Ready: " + (object) Advertisement.IsReady()));
      return false;
    }

    private void HandleShowAdResult(ShowResult result)
    {
      this.isTryingToShowAd = false;
      switch ((int) result)
      {
        case 0:
          DebugUtility.LogError("Video Ad failed.");
          this.ActivateOutputLinks(2);
          break;
        case 1:
          DebugUtility.Log("Video Ad skipped.");
          this.ActivateOutputLinks(5);
          break;
        case 2:
          DebugUtility.Log("Video Ad rewarded.");
          this.ActivateOutputLinks(1);
          break;
      }
    }
  }
}
