// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckVersion
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Start Online", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(13, "バージョンが古い", FlowNode.PinTypes.Output, 13)]
  [FlowNode.NodeType("System/CheckVersion", 32741)]
  [FlowNode.Pin(12, "Reset to Title", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(101, "Start Offline", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Success Online", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Success Offline", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_CheckVersion : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100 && pinID != 101)
        return;
      if (GameUtility.Config_UseDevServer.Value)
        Network.SetHost(GameUtility.DevServerSetting);
      else if (GameUtility.Config_UseStgServer.Value)
        Network.SetHost("https://stg02-app.alcww.gumi.sg/");
      else if (GameUtility.Config_UseAwsServer.Value)
        Network.SetHost("http://app.alcww.gumi.sg/");
      else
        Network.ResetHost();
      if (pinID == 100)
      {
        Network.Mode = Network.EConnectMode.Online;
        string ver = "1512";
        string os = "android";
        TextAsset textAsset = (TextAsset) Resources.Load<TextAsset>("networkver");
        if (Object.op_Inequality((Object) textAsset, (Object) null))
          ver = textAsset.get_text();
        Network.Version = ver;
        this.ExecRequest((WebAPI) new ReqCheckVersion(ver, os, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
      {
        Network.Mode = Network.EConnectMode.Offline;
        this.ActivateOutputLinks(11);
        ((Behaviour) this).set_enabled(false);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<FlowNode_CheckVersion.Json_VersionInfo> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_CheckVersion.Json_VersionInfo>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.NoVersion)
        {
          this.ActivateOutputLinks(13);
          Network.RemoveAPI();
          ((Behaviour) this).set_enabled(false);
        }
        else
          this.OnRetry();
      }
      else if (jsonObject.body == null || jsonObject.body.host_ap == null)
      {
        this.OnRetry();
      }
      else
      {
        Network.SetHost(jsonObject.body.host_ap);
        Network.SetDLHost(jsonObject.body.host_dl);
        Network.SetSiteHost(jsonObject.body.host_site);
        Network.SetNewsHost(jsonObject.body.host_news);
        Network.RemoveAPI();
        if (jsonObject.body.tz != int.MaxValue)
          TimeManager.UTC2LOCAL = (long) jsonObject.body.tz;
        if (!string.IsNullOrEmpty(jsonObject.body.assets))
        {
          string assets = jsonObject.body.assets;
          Network.AssetVersion = assets;
          AssetDownloader.DownloadURL = jsonObject.body.host_dl + "/assets/" + assets + "/";
          AssetDownloader.StreamingURL = jsonObject.body.host_dl + "/";
        }
        this.checkNewsDisplay(jsonObject);
        MonoSingleton<GameManager>.Instance.InitAlterHash(jsonObject.body.digest);
        this.ActivateOutputLinks(10);
        ((Behaviour) this).set_enabled(false);
      }
    }

    private void checkNewsDisplay(WebAPI.JSON_BodyResponse<FlowNode_CheckVersion.Json_VersionInfo> res)
    {
      NewsUtility.setNewsState(res.body.pub, res.body.pub_u, MonoSingleton<GameManager>.Instance.Player.IsFirstLogin);
    }

    private class Json_VersionInfo
    {
      public int tz = int.MaxValue;
      public string result;
      public string type;
      public string host_ap;
      public string host_dl;
      public string host_site;
      public string host_news;
      public string assets;
      public string pub;
      public string pub_u;
      public string digest;
    }
  }
}
