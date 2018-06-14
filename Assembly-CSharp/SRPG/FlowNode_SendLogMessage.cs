// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendLogMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using Gsc.Auth;
using LogKit;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/SendLogMessage", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "OutPut", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SendLogMessage : FlowNode
  {
    [SerializeField]
    public string mLoggerTitle = "application";
    [SerializeField]
    public string mTag = "login";
    [SerializeField]
    public LogLevel mLogLevel = LogLevel.Info;
    [SerializeField]
    public string mMessage = string.Empty;
    [SerializeField]
    public bool bDevieceID = true;
    [SerializeField]
    public bool bOsname = true;
    [SerializeField]
    public bool bOkyakusamaCode = true;
    [SerializeField]
    public bool bUserAgent = true;
    private const string BaseLoggerTitle = "application";
    private const LogLevel BaseLogLevel = LogLevel.Info;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        ((Behaviour) this).set_enabled(false);
        FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
        sendLogGenerator.AddCommon(this.bDevieceID, this.bOsname, this.bOkyakusamaCode, this.bUserAgent);
        sendLogGenerator.Add("msg", this.mMessage);
        LogKit.Logger.CreateLogger(this.mLoggerTitle).Post(this.mTag, this.mLogLevel, sendLogGenerator.GetSendMessage());
      }
      this.ActivateOutputLinks(10);
    }

    public static bool IsSetup()
    {
      return !string.IsNullOrEmpty(MonoSingleton<GameManager>.Instance.DeviceId);
    }

    public static void TrackEvent(string category, string name)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.Add(nameof (category), category);
      sendLogGenerator.Add(nameof (name), name);
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post(category, LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public static void TrackEvent(string category, string name, int value)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.Add(nameof (category), category);
      sendLogGenerator.Add(nameof (name), name);
      sendLogGenerator.Add(nameof (value), value.ToString());
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post(category, LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public static void TrackPurchase(string productId, string currency, double price = 0)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.Add(nameof (productId), productId);
      sendLogGenerator.Add(nameof (currency), currency);
      sendLogGenerator.Add(nameof (price), price.ToString());
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post("purchase", LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public static void TrackSpend(string category, string name, int value)
    {
      if (!FlowNode_SendLogMessage.IsSetup())
        return;
      FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
      sendLogGenerator.Add(nameof (category), category);
      sendLogGenerator.Add(nameof (name), name);
      sendLogGenerator.Add(nameof (value), value.ToString());
      sendLogGenerator.AddCommon(true, false, false, true);
      LogKit.Logger.CreateLogger("application").Post(category, LogLevel.Info, sendLogGenerator.GetSendMessage());
    }

    public class SendLogGenerator
    {
      private Dictionary<string, string> dict = new Dictionary<string, string>();

      public void Add(string tag, string msg)
      {
        if (string.IsNullOrEmpty(msg))
          return;
        this.dict.Add("\"" + tag + "\"", "\"" + msg + "\"");
      }

      public void AddCommon(bool b_deviece_id, bool b_osname, bool b_okyakusama_code, bool b_user_agent)
      {
        if (b_deviece_id && !string.IsNullOrEmpty(MonoSingleton<GameManager>.Instance.DeviceId))
          this.dict.Add("\"DeviceID\"", "\"" + MonoSingleton<GameManager>.Instance.DeviceId + "\"");
        if (b_osname && !string.IsNullOrEmpty("android"))
          this.dict.Add("\"OSNAME\"", "\"android\"");
        if (b_okyakusama_code && !string.IsNullOrEmpty(GameUtility.Config_OkyakusamaCode))
          this.dict.Add("\"OkyakusamaCode\"", "\"" + GameUtility.Config_OkyakusamaCode + "\"");
        if (!b_user_agent || Session.DefaultSession == null || string.IsNullOrEmpty(Session.DefaultSession.UserAgent))
          return;
        this.dict.Add("\"UserAgent\"", Session.DefaultSession.UserAgent);
      }

      public string GetSendMessage()
      {
        string str = string.Empty + "{";
        using (Dictionary<string, string>.Enumerator enumerator = this.dict.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, string> current = enumerator.Current;
            str = str + current.Key + ":";
            str = str + current.Value + ",";
          }
        }
        if (str.EndsWith(","))
          str = str.Substring(0, str.Length - 1);
        return str + "}";
      }
    }
  }
}
