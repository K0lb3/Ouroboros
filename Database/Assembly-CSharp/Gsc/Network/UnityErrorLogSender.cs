// Decompiled with JetBrains decompiler
// Type: Gsc.Network.UnityErrorLogSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using DeviceKit;
using Gsc.Auth;
using Gsc.Core;
using Gsc.Device;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Gsc.Network
{
  public class UnityErrorLogSender : IEnumerator
  {
    private static readonly Regex regex = new Regex("(?<func>.*)\\s+\\(at\\s+(?<path>.*):(?<lineno>\\d+)\\)", RegexOptions.IgnoreCase);
    private LinkedList<UnityErrorLogSender.ErrorData> buffer = new LinkedList<UnityErrorLogSender.ErrorData>();
    private const int BUFFER_SIZE = 5;
    private const float TRANS_INTERVAL = 5f;
    private static UnityErrorLogSender _instance;
    private object current;
    private bool isRunning;

    public static UnityErrorLogSender Instance
    {
      get
      {
        if (UnityErrorLogSender._instance == null)
          UnityErrorLogSender._instance = new UnityErrorLogSender();
        return UnityErrorLogSender._instance;
      }
    }

    public static void Initialize()
    {
      UnityErrorLogSender._instance = (UnityErrorLogSender) null;
    }

    public Action<CustomHeaders, Dictionary<string, object>, Dictionary<string, object>, Dictionary<string, object>> Callback { get; set; }

    public void Send(string message, string stacktrace, LogType logType)
    {
      if (SDK.Configuration.Env.ClientErrorApi == null || this.buffer.Count >= 5)
        return;
      this.buffer.AddLast(new UnityErrorLogSender.ErrorData()
      {
        message = message,
        stacktrace = stacktrace,
        logType = logType
      });
      if (this.isRunning)
        return;
      RootObject.Instance.StartCoroutine((IEnumerator) this);
    }

    public object Current
    {
      get
      {
        return this.current;
      }
    }

    public bool MoveNext()
    {
      this.current = (object) null;
      if (this.buffer.Count > 0)
      {
        this.current = (object) new WaitForSeconds(5f);
        this.InternalSend(this.buffer.First.Value);
        this.buffer.RemoveFirst();
      }
      this.isRunning = this.current != null || this.buffer.Count > 0;
      return this.isRunning;
    }

    public void Reset()
    {
    }

    private static void TryGetException(string message, IEnumerator stacktrace, List<object> exceptions)
    {
      string[] strArray = message.Split(new char[1]{ ':' }, 2);
      exceptions.Add((object) new Dictionary<string, object>()
      {
        {
          "type",
          (object) strArray[0].Trim()
        },
        {
          "value",
          (object) strArray[1].Trim()
        },
        {
          nameof (stacktrace),
          (object) new Dictionary<string, object>()
          {
            {
              "frames",
              (object) UnityErrorLogSender.GetStackframes(stacktrace, exceptions)
            }
          }
        }
      });
    }

    private static List<object> GetStackframes(IEnumerator stacktrace, List<object> exceptions)
    {
      List<object> objectList = new List<object>();
      while (stacktrace.MoveNext())
      {
        string input = ((string) stacktrace.Current).Trim();
        if (!string.IsNullOrEmpty(input))
        {
          if (input.StartsWith("Rethrow as"))
          {
            UnityErrorLogSender.TryGetException(input.Substring(11), stacktrace, exceptions);
            break;
          }
          string str1 = (string) null;
          string str2 = (string) null;
          Match match = UnityErrorLogSender.regex.Match(input);
          string str3;
          if (match.Success)
          {
            str3 = match.Groups["func"].Captures[0].Value;
            str1 = match.Groups["lineno"].Captures[0].Value;
            str2 = match.Groups["path"].Captures[0].Value;
          }
          else
            str3 = input;
          objectList.Add((object) new Dictionary<string, object>()
          {
            {
              "function",
              (object) str3
            },
            {
              "lineno",
              (object) str1
            },
            {
              "abs_path",
              (object) str2
            }
          });
        }
      }
      return objectList;
    }

    private static Dictionary<string, object> TryParseError(UnityErrorLogSender.ErrorData data, Dictionary<string, object> user, Dictionary<string, object> tags, Dictionary<string, object> extra)
    {
      IEnumerator enumerator = data.stacktrace.Split('\n').GetEnumerator();
      string message = data.message;
      Dictionary<string, object> dictionary1 = (Dictionary<string, object>) null;
      Dictionary<string, object> dictionary2 = (Dictionary<string, object>) null;
      if (data.logType == 4)
      {
        List<object> exceptions = new List<object>();
        UnityErrorLogSender.TryGetException(message, enumerator, exceptions);
        dictionary1 = new Dictionary<string, object>()
        {
          {
            "values",
            (object) exceptions
          }
        };
        Dictionary<string, object> dictionary3 = (Dictionary<string, object>) exceptions[0];
        message = (string) dictionary3["type"] + ": " + (string) dictionary3["value"];
      }
      else
      {
        List<object> exceptions = new List<object>();
        dictionary2 = new Dictionary<string, object>()
        {
          {
            "frames",
            (object) UnityErrorLogSender.GetStackframes(enumerator, exceptions)
          }
        };
        if (exceptions.Count > 0)
          dictionary1 = new Dictionary<string, object>()
          {
            {
              "values",
              (object) exceptions
            }
          };
      }
      return new Dictionary<string, object>() { { "message", (object) message }, { "stacktrace", (object) dictionary2 }, { "exception", (object) dictionary1 }, { nameof (user), (object) user }, { nameof (tags), (object) tags }, { nameof (extra), (object) extra } };
    }

    private static Dictionary<string, object> GetSendData(UnityErrorLogSender.ErrorData data, Dictionary<string, object> user, Dictionary<string, object> tags, Dictionary<string, object> extra)
    {
      try
      {
        return UnityErrorLogSender.TryParseError(data, user, tags, extra);
      }
      catch (Exception ex)
      {
        return new Dictionary<string, object>() { { "message", (object) (data.message + "\n\n" + data.stacktrace) }, { "stacktrace", (object) null }, { "exception", (object) null }, { nameof (user), (object) user }, { nameof (tags), (object) tags }, { nameof (extra), (object) extra } };
      }
    }

    private static Dictionary<string, object> CreateUserData()
    {
      try
      {
        return new Dictionary<string, object>() { { "device_id", (object) Session.DefaultSession.DeviceID }, { "osname", (object) "android" }, { "platform", (object) Gsc.Auth.Device.Platform } };
      }
      catch (Exception ex)
      {
        return new Dictionary<string, object>() { { "osname", (object) "android" } };
      }
    }

    private static Dictionary<string, object> CreateExtraData()
    {
      try
      {
        return new Dictionary<string, object>() { { "available_memory", (object) Sys.GetAvailableMemoryBytes() }, { "available_storage", (object) Sys.GetAvailableStorageBytes() }, { "language", (object) Sys.GetLanguageLocale() }, { "os_info", (object) DeviceInfo.OperatingSystem }, { "cpu_info", (object) DeviceInfo.ProcessorType }, { "device_model", (object) DeviceInfo.DeviceModel }, { "device_vendor", (object) DeviceInfo.DeviceVendor } };
      }
      catch (Exception ex)
      {
        return new Dictionary<string, object>();
      }
    }

    private void InternalSend(UnityErrorLogSender.ErrorData data)
    {
      Dictionary<string, object> userData1 = UnityErrorLogSender.CreateUserData();
      Dictionary<string, object> userData2 = UnityErrorLogSender.CreateUserData();
      Dictionary<string, object> extraData = UnityErrorLogSender.CreateExtraData();
      UnityErrorLogSender.Request request = new UnityErrorLogSender.Request(UnityErrorLogSender.GetSendData(data, userData1, userData2, extraData));
      if (this.Callback != null)
      {
        try
        {
          this.Callback(request.CustomHeaders, userData1, userData2, extraData);
        }
        catch (Exception ex)
        {
        }
      }
      request.Cast();
    }

    private struct ErrorData
    {
      public string message;
      public string stacktrace;
      public LogType logType;
    }

    private class Request : Gsc.Network.Request<UnityErrorLogSender.Request, UnityErrorLogSender.Response>
    {
      private readonly Dictionary<string, object> data;

      public Request(Dictionary<string, object> data)
      {
        this.data = data;
      }

      public override string GetPath()
      {
        return SDK.Configuration.Env.ClientErrorApi;
      }

      public override string GetMethod()
      {
        return "POST";
      }

      protected override Dictionary<string, object> GetParameters()
      {
        return this.data;
      }
    }

    private class Response : Gsc.Network.Response<UnityErrorLogSender.Response>
    {
      public Response(WebInternalResponse response)
      {
      }
    }
  }
}
