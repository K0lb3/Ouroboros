namespace SRPG
{
    using GR;
    using Gsc.Auth;
    using LogKit;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [NodeType("SendLogKit/SendMessage", 0x7fe5), Pin(0, "Request", 0, 0), Pin(10, "OutPut", 1, 10)]
    public class FlowNode_SendLogMessage : FlowNode
    {
        private const string BaseLoggerTitle = "application";
        private const LogLevel BaseLogLevel = 1;
        [SerializeField]
        private string mLoggerTitle;
        [SerializeField]
        private string mTag;
        [SerializeField]
        private LogLevel mLogLevel;
        [SerializeField]
        private string mMessage;
        [SerializeField]
        private bool bDevieceID;
        [SerializeField]
        private bool bOsname;
        [SerializeField]
        private bool bOkyakusamaCode;
        [SerializeField]
        private bool bUserAgent;
        [SerializeField]
        private Image mImage;
        [SerializeField]
        private Text mText;

        public FlowNode_SendLogMessage()
        {
            this.mLoggerTitle = "application";
            this.mTag = "login";
            this.mLogLevel = 1;
            this.mMessage = string.Empty;
            this.bDevieceID = 1;
            this.bUserAgent = 1;
            base..ctor();
            return;
        }

        public static bool IsSetup()
        {
            if (string.IsNullOrEmpty(MonoSingleton<GameManager>.Instance.DeviceId) == null)
            {
                goto Label_0016;
            }
            return 0;
        Label_0016:
            return 1;
        }

        public override void OnActivate(int pinID)
        {
            SendLogGenerator generator;
            Logger logger;
            if (pinID != null)
            {
                goto Label_0078;
            }
            base.set_enabled(0);
            generator = new SendLogGenerator();
            generator.AddCommon(this.bDevieceID, this.bOsname, this.bOkyakusamaCode, this.bUserAgent);
            generator.Add("msg", this.mMessage);
            generator.AddOriginal(this.mImage, this.mText);
            Logger.CreateLogger(this.mLoggerTitle).Post(this.mTag, this.mLogLevel, generator.GetSendMessage());
        Label_0078:
            base.ActivateOutputLinks(10);
            return;
        }

        public static void SceneChangeEvent(string category, string before, string after)
        {
            SendLogGenerator generator;
            Logger logger;
            if (IsSetup() != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            generator = new SendLogGenerator();
            generator.AddCommon(1, 0, 0, 1);
            generator.Add("before", before);
            generator.Add("after", after);
            Logger.CreateLogger("application").Post(category, 1, generator.GetSendMessage());
            return;
        }

        public static void TrackEvent(string category, string name)
        {
            SendLogGenerator generator;
            Logger logger;
            if (IsSetup() != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            generator = new SendLogGenerator();
            generator.Add("category", category);
            generator.Add("name", name);
            generator.AddCommon(1, 0, 0, 1);
            Logger.CreateLogger("application").Post(category, 1, generator.GetSendMessage());
            return;
        }

        public static unsafe void TrackEvent(string category, string name, int value)
        {
            SendLogGenerator generator;
            Logger logger;
            if (IsSetup() != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            generator = new SendLogGenerator();
            generator.Add("category", category);
            generator.Add("name", name);
            generator.Add("value", &value.ToString());
            generator.AddCommon(1, 0, 0, 1);
            Logger.CreateLogger("application").Post(category, 1, generator.GetSendMessage());
            return;
        }

        public static unsafe void TrackPurchase(string productId, string currency, double price)
        {
            SendLogGenerator generator;
            Logger logger;
            if (IsSetup() != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            generator = new SendLogGenerator();
            generator.Add("productId", productId);
            generator.Add("currency", currency);
            generator.Add("price", &price.ToString());
            generator.AddCommon(1, 0, 0, 1);
            Logger.CreateLogger("application").Post("purchase", 1, generator.GetSendMessage());
            return;
        }

        public static unsafe void TrackSpend(string category, string name, int value)
        {
            SendLogGenerator generator;
            Logger logger;
            if (IsSetup() != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            generator = new SendLogGenerator();
            generator.Add("category", category);
            generator.Add("name", name);
            generator.Add("value", &value.ToString());
            generator.AddCommon(1, 0, 0, 1);
            Logger.CreateLogger("application").Post(category, 1, generator.GetSendMessage());
            return;
        }

        public class SendLogGenerator
        {
            private Dictionary<string, string> dict;

            public SendLogGenerator()
            {
                this.dict = new Dictionary<string, string>();
                base..ctor();
                return;
            }

            public void Add(string tag, string msg)
            {
                if (string.IsNullOrEmpty(msg) != null)
                {
                    goto Label_0036;
                }
                this.dict.Add("\"" + tag + "\"", "\"" + msg + "\"");
            Label_0036:
                return;
            }

            public virtual void AddCommon(bool b_deviece_id, bool b_osname, bool b_okyakusama_code, bool b_user_agent)
            {
                if (b_deviece_id == null)
                {
                    goto Label_0043;
                }
                if (string.IsNullOrEmpty(MonoSingleton<GameManager>.Instance.DeviceId) != null)
                {
                    goto Label_0043;
                }
                this.dict.Add("\"DeviceID\"", "\"" + MonoSingleton<GameManager>.Instance.DeviceId + "\"");
            Label_0043:
                if (b_osname == null)
                {
                    goto Label_006D;
                }
                if (string.IsNullOrEmpty("windows") != null)
                {
                    goto Label_006D;
                }
                this.dict.Add("\"OSNAME\"", "\"windows\"");
            Label_006D:
                if (b_okyakusama_code == null)
                {
                    goto Label_00A6;
                }
                if (string.IsNullOrEmpty(GameUtility.Config_OkyakusamaCode) != null)
                {
                    goto Label_00A6;
                }
                this.dict.Add("\"OkyakusamaCode\"", "\"" + GameUtility.Config_OkyakusamaCode + "\"");
            Label_00A6:
                if (b_user_agent == null)
                {
                    goto Label_00E5;
                }
                if (Session.DefaultSession == null)
                {
                    goto Label_00E5;
                }
                if (string.IsNullOrEmpty(Session.DefaultSession.UserAgent) != null)
                {
                    goto Label_00E5;
                }
                this.dict.Add("\"UserAgent\"", Session.DefaultSession.UserAgent);
            Label_00E5:
                return;
            }

            public void AddOriginal(Image image, Text txt)
            {
                if ((image != null) == null)
                {
                    goto Label_0047;
                }
                if ((image.get_sprite() != null) == null)
                {
                    goto Label_0047;
                }
                this.dict.Add("\"image\"", "\"" + image.get_sprite().get_name() + "\"");
            Label_0047:
                if ((txt != null) == null)
                {
                    goto Label_0078;
                }
                this.dict.Add("\"text\"", "\"" + txt.get_text() + "\"");
            Label_0078:
                return;
            }

            public unsafe string GetSendMessage()
            {
                string str;
                KeyValuePair<string, string> pair;
                Dictionary<string, string>.Enumerator enumerator;
                str = string.Empty + "{";
                enumerator = this.dict.GetEnumerator();
            Label_001E:
                try
                {
                    goto Label_0051;
                Label_0023:
                    pair = &enumerator.Current;
                    str = str + &pair.Key + ":";
                    str = str + &pair.Value + ",";
                Label_0051:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0023;
                    }
                    goto Label_006E;
                }
                finally
                {
                Label_0062:
                    ((Dictionary<string, string>.Enumerator) enumerator).Dispose();
                }
            Label_006E:
                if (str.EndsWith(",") == null)
                {
                    goto Label_008E;
                }
                str = str.Substring(0, str.Length - 1);
            Label_008E:
                return (str + "}");
            }
        }
    }
}

