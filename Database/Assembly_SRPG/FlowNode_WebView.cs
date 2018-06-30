namespace SRPG
{
    using GR;
    using Gsc.App.NetworkHelper;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [NodeType("System/WebView", 0x7fe5), AddComponentMenu(""), Pin(0x66, "Preload", 0, 2), Pin(0x65, "Destroy", 0, 1), Pin(1, "Created", 1, 10), Pin(2, "Destroyed", 1, 11), Pin(100, "Create", 0, 0)]
    public class FlowNode_WebView : FlowNode
    {
        private const int PIN_ID_CREATE = 100;
        private const int PIN_ID_DESTROY = 0x65;
        private const int PIN_ID_PRELOAD = 0x66;
        private const int PIN_ID_CREATED = 1;
        private const int PIN_ID_DESTROYED = 2;
        private const string PREFAB_PATH = "UI/WebView";
        public string Title;
        public string URL;
        public bool usegAuth;
        public bool useVariable;
        private WebView webView;
        public URL_Mode URLMode;

        public FlowNode_WebView()
        {
            this.usegAuth = 1;
            base..ctor();
            return;
        }

        private unsafe void Create()
        {
            GameObject obj2;
            string str;
            string str2;
            Dictionary<string, string> dictionary;
            KeyValuePair<string, string> pair;
            Dictionary<string, string>.Enumerator enumerator;
            obj2 = AssetManager.Load<GameObject>("UI/WebView");
            if ((obj2 != null) == null)
            {
                goto Label_002D;
            }
            this.webView = Object.Instantiate<GameObject>(obj2).GetComponent<WebView>();
            goto Label_0038;
        Label_002D:
            Debug.Log("Prefab not Found");
            return;
        Label_0038:
            if (this.useVariable == null)
            {
                goto Label_007F;
            }
            str = FlowNode_Variable.Get(this.URL);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0061;
            }
            this.URL = str;
        Label_0061:
            str2 = FlowNode_Variable.Get(this.Title);
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_007F;
            }
            this.Title = str2;
        Label_007F:
            this.webView.OnClose = new UIUtility.DialogResultEvent(this.OnClose);
            this.webView.Text_Title.set_text(LocalizedText.Get(this.Title));
            if (this.usegAuth == null)
            {
                goto Label_0121;
            }
            dictionary = new Dictionary<string, string>();
            GsccBridge.SetWebViewHeaders(new Action<string, string>(dictionary.Add));
            enumerator = dictionary.GetEnumerator();
        Label_00DC:
            try
            {
                goto Label_0103;
            Label_00E1:
                pair = &enumerator.Current;
                this.webView.SetHeaderField(&pair.Key, &pair.Value);
            Label_0103:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00E1;
                }
                goto Label_0121;
            }
            finally
            {
            Label_0114:
                ((Dictionary<string, string>.Enumerator) enumerator).Dispose();
            }
        Label_0121:
            if (this.URL.StartsWith("http://") != null)
            {
                goto Label_014B;
            }
            if (this.URL.StartsWith("https://") == null)
            {
                goto Label_0176;
            }
        Label_014B:
            MonoSingleton<GameManager>.Instance.Player.UpdateViewNewsTrophy(this.URL);
            this.webView.OpenURL(this.URL);
            goto Label_0213;
        Label_0176:
            if (this.URLMode != null)
            {
                goto Label_01A1;
            }
            this.webView.OpenURL(Network.Host + this.URL);
            goto Label_0213;
        Label_01A1:
            if (this.URLMode != 1)
            {
                goto Label_01CD;
            }
            this.webView.OpenURL(Network.SiteHost + this.URL);
            goto Label_0213;
        Label_01CD:
            if (this.URLMode != 2)
            {
                goto Label_0213;
            }
            MonoSingleton<GameManager>.Instance.Player.UpdateViewNewsTrophy(Network.NewsHost + this.URL);
            this.webView.OpenURL(Network.NewsHost + this.URL);
        Label_0213:
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 100))
            {
                case 0:
                    goto Label_001C;

                case 1:
                    goto Label_002F;

                case 2:
                    goto Label_0058;
            }
            goto Label_005D;
        Label_001C:
            this.Create();
            base.ActivateOutputLinks(1);
            goto Label_005D;
        Label_002F:
            if ((this.webView != null) == null)
            {
                goto Label_004B;
            }
            this.webView.BeginClose();
        Label_004B:
            base.ActivateOutputLinks(2);
            goto Label_005D;
        Label_0058:;
        Label_005D:
            return;
        }

        public void OnClose(GameObject go)
        {
            base.ActivateOutputLinks(2);
            return;
        }

        public enum URL_Mode
        {
            APIHost,
            SiteHost,
            NewsHost
        }
    }
}

