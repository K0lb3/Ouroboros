namespace SRPG
{
    using GR;
    using Gsc.App;
    using System;
    using UnityEngine;

    [Pin(12, "Reset to Title", 1, 12), Pin(14, "直接起動している", 1, 14), NodeType("GscSystem/GsccInit", 0x7fe5), Pin(100, "Start Online", 0, 0), Pin(0x65, "Start Offline", 0, 1), Pin(10, "Success Online", 1, 10), Pin(11, "Success Offline", 1, 11), Pin(13, "バージョンが古い", 1, 13), Pin(0x3e9, "Different Assets", 1, 0x3e9)]
    public class FlowNode_GsccInit : FlowNode
    {
        public FlowNode_GsccInit()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            TextAsset asset;
            if (pinID != 100)
            {
                goto Label_009B;
            }
            if (BootLoader.BootStates != 1)
            {
                goto Label_0028;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            goto Label_009B;
        Label_0028:
            if (GameUtility.Config_UseDevServer.Value == null)
            {
                goto Label_0046;
            }
            Network.SetDefaultHostConfigured("http://localhost:5000/");
            goto Label_005F;
        Label_0046:
            if (GameUtility.Config_UseAwsServer.Value == null)
            {
                goto Label_005F;
            }
            Network.SetDefaultHostConfigured("https://alchemist.gu3.jp/");
        Label_005F:
            Network.Mode = 0;
            str = "grdev";
            asset = Resources.Load<TextAsset>("networkver");
            if ((asset != null) == null)
            {
                goto Label_0089;
            }
            str = asset.get_text();
        Label_0089:
            Network.Version = str;
            base.set_enabled(1);
            BootLoader.GscInit();
        Label_009B:
            return;
        }

        public static bool SettingAssets(string version, string version_ex)
        {
            bool flag;
            string str;
            flag = 0;
            if (string.IsNullOrEmpty(version) != null)
            {
                goto Label_009D;
            }
            if ((Network.AssetVersion != version) == null)
            {
                goto Label_001F;
            }
            flag = 1;
        Label_001F:
            str = Network.DLHost;
            Network.AssetVersion = version;
            Network.AssetVersionEx = version_ex;
            AssetDownloader.DownloadURL = str + "/assets/" + version + "/";
            AssetDownloader.StreamingURL = str + "/";
            AssetDownloader.SetBaseDownloadURL(str + "/assets/");
            if (string.IsNullOrEmpty(version_ex) == null)
            {
                goto Label_0087;
            }
            AssetDownloader.ExDownloadURL = str + "/assets/demo_ex/";
            goto Label_009D;
        Label_0087:
            AssetDownloader.ExDownloadURL = str + "/assets_ex/" + version_ex + "/";
        Label_009D:
            return flag;
        }

        private void Success()
        {
            NewsUtility.setNewsState(Network.Pub, Network.PubU, MonoSingleton<GameManager>.Instance.Player.IsFirstLogin);
            MonoSingleton<GameManager>.Instance.InitAlterHash(Network.Digest);
            return;
        }

        private unsafe void Update()
        {
            string str;
            string str2;
            bool flag;
            BootLoader.BootState state;
            Environment environment;
            Environment environment2;
            state = BootLoader.BootStates;
            if (state == 1)
            {
                goto Label_0019;
            }
            if (state == 2)
            {
                goto Label_00B4;
            }
            goto Label_00C9;
        Label_0019:
            if (Network.IsNoVersion == null)
            {
                goto Label_0042;
            }
            Network.ErrCode = 0x44c;
            base.set_enabled(0);
            base.ActivateOutputLinks(13);
            goto Label_00AF;
        Label_0042:
            str = &Network.GetEnvironment.Assets;
            str2 = &Network.GetEnvironment.AssetsEx;
            if (SettingAssets(str, str2) == null)
            {
                goto Label_0099;
            }
            if (MonoSingleton<GameManager>.Instance.IsRelogin == null)
            {
                goto Label_0099;
            }
            MonoSingleton<GameManager>.Instance.IsRelogin = 0;
            base.ActivateOutputLinks(0x3e9);
            goto Label_00A8;
        Label_0099:
            this.Success();
            base.ActivateOutputLinks(10);
        Label_00A8:
            base.set_enabled(0);
        Label_00AF:
            goto Label_00C9;
        Label_00B4:
            base.set_enabled(0);
            base.ActivateOutputLinks(14);
        Label_00C9:
            return;
        }
    }
}

