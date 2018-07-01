// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GsccInit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using Gsc.App;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("GscSystem/GsccInit", 32741)]
  [FlowNode.Pin(10, "Success Online", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Success Offline", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "Start Online", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(12, "Reset to Title", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "バージョンが古い", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "直接起動している", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(101, "Start Offline", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_GsccInit : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (BootLoader.BootStates == BootLoader.BootState.SUCCESS)
      {
        ((Behaviour) this).set_enabled(false);
        this.ActivateOutputLinks(10);
      }
      else
      {
        if (GameUtility.Config_UseDevServer.Value)
          Network.SetDefaultHostConfigured(GameUtility.DevServerSetting);
        else if (GameUtility.Config_UseAwsServer.Value)
          Network.SetDefaultHostConfigured("https://alchemist.gu3.jp/");
        Network.Mode = Network.EConnectMode.Online;
        string str = "grdev";
        TextAsset textAsset = (TextAsset) Resources.Load<TextAsset>("networkver");
        if (Object.op_Inequality((Object) textAsset, (Object) null))
          str = textAsset.get_text();
        Network.Version = str;
        ((Behaviour) this).set_enabled(true);
        BootLoader.GscInit();
      }
    }

    private void Update()
    {
      switch (BootLoader.BootStates)
      {
        case BootLoader.BootState.SUCCESS:
          if (Network.IsNoVersion)
          {
            Network.ErrCode = Network.EErrCode.NoVersion;
            ((Behaviour) this).set_enabled(false);
            this.ActivateOutputLinks(13);
            break;
          }
          this.Success();
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(10);
          break;
        case BootLoader.BootState.FAILED:
          ((Behaviour) this).set_enabled(false);
          this.ActivateOutputLinks(14);
          break;
      }
    }

    private void Success()
    {
      FlowNode_GsccInit.SettingAssets(Network.GetEnvironment.Assets, Network.GetEnvironment.AssetsEx);
      NewsUtility.setNewsState(Network.Pub, Network.PubU, MonoSingleton<GameManager>.Instance.Player.IsFirstLogin);
      MonoSingleton<GameManager>.Instance.InitAlterHash(Network.Digest);
    }

    public static void SettingAssets(string version, string version_ex)
    {
      if (string.IsNullOrEmpty(version))
        return;
      string dlHost = Network.DLHost;
      Network.AssetVersion = version;
      Network.AssetVersionEx = version_ex;
      AssetDownloader.DownloadURL = dlHost + "assets/" + version + "/";
      AssetDownloader.StreamingURL = dlHost + string.Empty;
      AssetDownloader.SetBaseDownloadURL(dlHost + "assets/");
      if (string.IsNullOrEmpty(version_ex))
        AssetDownloader.ExDownloadURL = dlHost + "assets/demo_ex/";
      else
        AssetDownloader.ExDownloadURL = dlHost + "assets_ex/" + version_ex + "/";
    }
  }
}
