// Decompiled with JetBrains decompiler
// Type: SRPG.BackgroundDownloader
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class BackgroundDownloader : MonoBehaviour
  {
    private static BackgroundDownloader instance;
    private int currentDownloadStep;
    private int requiredDownloadStep;
    private bool isQueued;
    private bool isEnabled;
    public bool simulateError;

    private BackgroundDownloader()
    {
      base.\u002Ector();
      this.currentDownloadStep = 0;
      this.requiredDownloadStep = 0;
      this.isQueued = false;
      int num = PlayerPrefs.GetInt("ENABLE_BGDL", -1);
      if (num == -1)
        this.isEnabled = SystemInfo.get_systemMemorySize() > 1024;
      else
        this.isEnabled = num == 1;
    }

    public static BackgroundDownloader Instance
    {
      get
      {
        if (Object.op_Equality((Object) BackgroundDownloader.instance, (Object) null))
        {
          GameObject gameObject = new GameObject(typeof (BackgroundDownloader).Name, new System.Type[1]{ typeof (BackgroundDownloader) });
          BackgroundDownloader.instance = (BackgroundDownloader) gameObject.GetComponent<BackgroundDownloader>();
          Object.DontDestroyOnLoad((Object) gameObject);
        }
        return BackgroundDownloader.instance;
      }
    }

    public bool IsEnabled
    {
      get
      {
        return this.isEnabled;
      }
      set
      {
        this.isEnabled = value;
        PlayerPrefs.SetInt("ENABLE_BGDL", !this.isEnabled ? 0 : 1);
      }
    }

    public void StartBackgroundDownload(string tutorialStep)
    {
      if (!this.isEnabled)
        return;
      this.UpdateDownloadStep(tutorialStep);
      if (this.currentDownloadStep >= this.requiredDownloadStep || this.isQueued)
        return;
      DebugUtility.LogWarning("################ Background Downloader: Checking step " + (object) this.currentDownloadStep + "/" + (object) this.requiredDownloadStep + " ###################");
      this.StartCoroutine(this.BackgroundDLAsync());
    }

    public bool IsCurrentStepComplete()
    {
      if (this.isEnabled)
        return this.currentDownloadStep >= this.requiredDownloadStep;
      return true;
    }

    public void GetRequiredAssets(string tutorialStep)
    {
      if (tutorialStep == "tutorial_start")
      {
        AssetManager.PrepareAssets("PortraitsM/urob");
        if ((long) GlobalVars.BtlID == 0L)
        {
          AssetManager.PrepareAssets("tutorial_start");
        }
        else
        {
          AssetManager.PrepareAssets("UI/QuestAssets");
          AssetManager.PrepareAssets("SkillSplash/splash_base");
          AssetManager.PrepareAssets("SGDevelopment/Tutorial/Tutorial_Guidance");
          AssetManager.PrepareAssets("CHM/F_cmn_jumploop0");
          AssetManager.PrepareAssets("CHM/M_cmn_jumploop0");
          AssetManager.PrepareAssets("CHM/MM_cmn_jumploop0");
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (instance.HasTutorialDLAssets)
            instance.DownloadTutorialAssetsByFolder("Effects/");
          if ((long) GlobalVars.BtlID != 200L)
            return;
          AssetManager.PrepareAssets("StreamingAssets/BGM_0028.acb");
          AssetManager.PrepareAssets("StreamingAssets/BGM_0028.awb");
        }
      }
      else if (tutorialStep == "op0002exit")
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        AssetManager.PrepareAssets("op0002exit");
        AssetManager.PrepareAssets("StreamingAssets/tut001b.acb");
        AssetManager.PrepareAssets("StreamingAssets/tut001b.awb");
        AssetManager.PrepareAssets("StreamingAssets/BGM_0002.acb");
        AssetManager.PrepareAssets("StreamingAssets/BGM_0002.awb");
        AssetManager.PrepareAssets("CHM/F_cmn_jumploop0");
        AssetManager.PrepareAssets("CHM/M_cmn_jumploop0");
      }
      else if (tutorialStep == "op0005exit")
      {
        AssetManager.PrepareAssets("op0005exit");
        AssetManager.PrepareAssets("StreamingAssets/0_6b_2d.acb");
        AssetManager.PrepareAssets("StreamingAssets/0_6b_2d.awb");
        AssetManager.PrepareAssets("StreamingAssets/BGM_0002.acb");
        AssetManager.PrepareAssets("StreamingAssets/BGM_0002.awb");
        AssetManager.PrepareAssets("StreamingAssets/BGM_0000.acb");
        AssetManager.PrepareAssets("StreamingAssets/BGM_0000.awb");
        AssetManager.PrepareAssets("StreamingAssets/VO_uroboros.acb");
        AssetManager.PrepareAssets("StreamingAssets/VO_uroboros.awb");
        AssetManager.PrepareAssets("UI/QuestAssets");
        AssetManager.PrepareAssets("SkillSplash/splash_base");
        AssetManager.PrepareAssets("SGDevelopment/Tutorial/Tutorial_Guidance");
        AssetManager.PrepareAssets("CHM/F_cmn_jumploop0");
        AssetManager.PrepareAssets("CHM/M_cmn_jumploop0");
        AssetManager.PrepareAssets("CHM/MM_cmn_jumploop0");
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
        {
          for (int index = 0; index < player.Units.Count; ++index)
            DownloadUtility.DownloadUnit(player.Units[index].UnitParam, player.Units[index].Jobs);
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (!instance.HasTutorialDLAssets)
          return;
        instance.DownloadTutorialAssetsByFolder("UI/");
        instance.DownloadTutorialAssetsByFolder("ItemIcon/");
        instance.DownloadTutorialAssetsByFolder("Effects/");
        instance.DownloadTutorialAssetsByFolder("JIN_");
      }
      else
      {
        if (!(tutorialStep == "op0006exit"))
          return;
        AssetManager.PrepareAssets("op0006exit");
        AssetManager.PrepareAssets("StreamingAssets/0_8_2d.acb");
        AssetManager.PrepareAssets("StreamingAssets/0_8_2d.awb");
      }
    }

    private void UpdateDownloadStep(string tutorialStep)
    {
      if (tutorialStep == "tutorial_start")
        this.requiredDownloadStep = 1;
      if (tutorialStep == "QE_OP_0002")
        this.requiredDownloadStep = 2;
      if (tutorialStep == "tut001b")
        this.requiredDownloadStep = 3;
      if (tutorialStep == "QE_OP_0003")
        this.requiredDownloadStep = 4;
      if (tutorialStep == "QE_OP_0004")
        this.requiredDownloadStep = 5;
      if (tutorialStep == "QE_OP_0006")
        this.requiredDownloadStep = 6;
      if (!(tutorialStep == "op0006exit"))
        return;
      this.requiredDownloadStep = 7;
    }

    [DebuggerHidden]
    private IEnumerator BackgroundDLAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new BackgroundDownloader.\u003CBackgroundDLAsync\u003Ec__IteratorC() { \u003C\u003Ef__this = this };
    }
  }
}
