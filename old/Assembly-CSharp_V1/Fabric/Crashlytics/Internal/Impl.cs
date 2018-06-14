// Decompiled with JetBrains decompiler
// Type: Fabric.Crashlytics.Internal.Impl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Fabric.Internal.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Fabric.Crashlytics.Internal
{
  internal class Impl
  {
    private static readonly string FrameArgsRegex = "\\s?\\(.*\\)";
    private static readonly string FrameRegexWithoutFileInfo = "(?<class>[^\\s]+)\\.(?<method>[^\\s\\.]+)" + Impl.FrameArgsRegex;
    private static readonly string FrameRegexWithFileInfo = Impl.FrameRegexWithoutFileInfo + " .*[/|\\\\](?<file>.+):(?<line>\\d+)";
    private static readonly string MonoFilenameUnknownString = "<filename unknown>";
    private static readonly string[] StringDelimiters = new string[1]{ Environment.NewLine };
    protected const string KitName = "Crashlytics";

    public static Impl Make()
    {
      return (Impl) new AndroidImpl();
    }

    public virtual void SetDebugMode(bool mode)
    {
    }

    public virtual void Crash()
    {
      Utils.Log("Crashlytics", "Method Crash () is unimplemented on this platform");
    }

    public virtual void ThrowNonFatal()
    {
      Utils.Log("Crashlytics", ((string) null).ToLower());
    }

    public virtual void Log(string message)
    {
      Utils.Log("Crashlytics", "Would log custom message if running on a physical device: " + message);
    }

    public virtual void SetKeyValue(string key, string value)
    {
      Utils.Log("Crashlytics", "Would set key-value if running on a physical device: " + key + ":" + value);
    }

    public virtual void SetUserIdentifier(string identifier)
    {
      Utils.Log("Crashlytics", "Would set user identifier if running on a physical device: " + identifier);
    }

    public virtual void SetUserEmail(string email)
    {
      Utils.Log("Crashlytics", "Would set user email if running on a physical device: " + email);
    }

    public virtual void SetUserName(string name)
    {
      Utils.Log("Crashlytics", "Would set user name if running on a physical device: " + name);
    }

    public virtual void RecordCustomException(string name, string reason, StackTrace stackTrace)
    {
      Utils.Log("Crashlytics", "Would record custom exception if running on a physical device: " + name + ", " + reason);
    }

    public virtual void RecordCustomException(string name, string reason, string stackTraceString)
    {
      Utils.Log("Crashlytics", "Would record custom exception if running on a physical device: " + name + ", " + reason);
    }

    private static Dictionary<string, string> ParseFrameString(string regex, string frameString)
    {
      MatchCollection matchCollection = Regex.Matches(frameString, regex);
      if (matchCollection.Count < 1)
        return (Dictionary<string, string>) null;
      Match match = matchCollection[0];
      if (!match.Groups["class"].Success || !match.Groups["method"].Success)
        return (Dictionary<string, string>) null;
      string str1 = !match.Groups["file"].Success ? match.Groups["class"].Value : match.Groups["file"].Value;
      string str2 = !match.Groups["line"].Success ? "0" : match.Groups["line"].Value;
      if (str1 == Impl.MonoFilenameUnknownString)
      {
        str1 = match.Groups["class"].Value;
        str2 = "0";
      }
      return new Dictionary<string, string>() { { "class", match.Groups["class"].Value }, { "method", match.Groups["method"].Value }, { "file", str1 }, { "line", str2 } };
    }

    protected static Dictionary<string, string>[] ParseStackTraceString(string stackTraceString)
    {
      List<Dictionary<string, string>> dictionaryList = new List<Dictionary<string, string>>();
      string[] strArray = stackTraceString.Split(Impl.StringDelimiters, StringSplitOptions.None);
      if (strArray.Length < 1)
        return dictionaryList.ToArray();
      string regex;
      if (Regex.Matches(strArray[0], Impl.FrameRegexWithFileInfo).Count == 1)
      {
        regex = Impl.FrameRegexWithFileInfo;
      }
      else
      {
        if (Regex.Matches(strArray[0], Impl.FrameRegexWithoutFileInfo).Count != 1)
          return dictionaryList.ToArray();
        regex = Impl.FrameRegexWithoutFileInfo;
      }
      foreach (string frameString1 in strArray)
      {
        Dictionary<string, string> frameString2 = Impl.ParseFrameString(regex, frameString1);
        if (frameString2 != null)
          dictionaryList.Add(frameString2);
      }
      return dictionaryList.ToArray();
    }

    private delegate Dictionary<string, string> FrameParser(string frameString);
  }
}
