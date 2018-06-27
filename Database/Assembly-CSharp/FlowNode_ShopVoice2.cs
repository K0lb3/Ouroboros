// Decompiled with JetBrains decompiler
// Type: FlowNode_ShopVoice2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[FlowNode.Pin(100, "店タイプ設定", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(2004, "停止", FlowNode.PinTypes.Input, 2004)]
[FlowNode.Pin(2003, "一時再生禁止解除", FlowNode.PinTypes.Input, 2003)]
[FlowNode.Pin(2002, "一時再生禁止", FlowNode.PinTypes.Input, 2002)]
[FlowNode.Pin(2001, "再生終了してから指定時間たつまで待つ", FlowNode.PinTypes.Input, 2001)]
[FlowNode.Pin(2000, "キュー再生", FlowNode.PinTypes.Input, 2000)]
[FlowNode.Pin(1003, "イベントボイス再生", FlowNode.PinTypes.Input, 1003)]
[FlowNode.Pin(1002, "時報ボイス再生", FlowNode.PinTypes.Input, 1002)]
[FlowNode.Pin(1001, "退出ボイス再生", FlowNode.PinTypes.Input, 1001)]
[FlowNode.Pin(1000, "入店ボイス再生", FlowNode.PinTypes.Input, 1000)]
[FlowNode.Pin(500, "再生中？", FlowNode.PinTypes.Input, 500)]
[FlowNode.Pin(200, "現在の店と一致？", FlowNode.PinTypes.Input, 200)]
[FlowNode.Pin(5, "No", FlowNode.PinTypes.Output, 5)]
[FlowNode.Pin(4, "Yes", FlowNode.PinTypes.Output, 4)]
[FlowNode.Pin(3, "NotPlayed", FlowNode.PinTypes.Output, 3)]
[FlowNode.Pin(2, "Played", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("Sound/ShopVoice2", 32741)]
public class FlowNode_ShopVoice2 : FlowNodePersistent
{
  public float StopSec = 0.3f;
  public string DateEventFileName = "Data/ShopVoiceEvent";
  private static FlowNode_ShopVoice2.EType sType;
  public FlowNode_ShopVoice2.EType Type;
  private MySound.Voice mVoice;
  private FlowNode_ShopVoice2.EType mType;
  public string CueID;
  public bool DirectCueID;
  public bool ExitCueID;
  public float WaitSecAfterPlayEnd;
  private float mWaitSecAfterPlayEnd;
  private bool mWaitingForPlayEnd;
  private bool mDisableIdleVoice;
  private static int sDisableIdleVoice;
  private static string sDelayCueID;
  private static bool sDelayCueIDDirect;
  private static List<FlowNode_ShopVoice2.DateEvent>[] sDateEvent;

  protected override void OnDestroy()
  {
    this.DisableIdleVoice(false);
    base.OnDestroy();
  }

  private void DisableIdleVoice(bool flag)
  {
    if (flag)
    {
      if (this.mDisableIdleVoice)
        return;
      ++FlowNode_ShopVoice2.sDisableIdleVoice;
      this.mDisableIdleVoice = true;
    }
    else
    {
      if (!this.mDisableIdleVoice)
        return;
      this.mDisableIdleVoice = false;
      --FlowNode_ShopVoice2.sDisableIdleVoice;
    }
  }

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        FlowNode_ShopVoice2.sType = this.Type;
        FlowNode_ShopVoice2.sDelayCueID = (string) null;
        this.ActivateOutputLinks(1);
        break;
      case 200:
        this.ActivateOutputLinks(this.Type != FlowNode_ShopVoice2.sType ? 5 : 4);
        break;
      case 500:
        this.ActivateOutputLinks(!MySound.Voice.IsCueSheetPlaying(FlowNode_ShopVoice2.sType.ToString()) ? 5 : 4);
        break;
      case 2001:
        this.mWaitingForPlayEnd = true;
        this.mWaitSecAfterPlayEnd = this.WaitSecAfterPlayEnd;
        break;
      case 2002:
      case 2003:
        this.DisableIdleVoice(pinID == 2002);
        this.ActivateOutputLinks(1);
        break;
      case 2004:
        MySound.Voice.StopAll(FlowNode_ShopVoice2.sType.ToString(), this.StopSec, false);
        break;
      default:
        if (this.mVoice == null)
        {
          this.mType = FlowNode_ShopVoice2.sType;
          this.mVoice = new MySound.Voice(this.mType.ToString());
        }
        string cueID = (string) null;
        bool direct = false;
        float stopSec = this.StopSec;
        bool exitPlay = this.ExitCueID;
        switch (pinID)
        {
          case 1000:
            cueID = this.mType != FlowNode_ShopVoice2.EType.shop ? "action_0007" : "action_0013";
            break;
          case 1001:
            cueID = this.mType != FlowNode_ShopVoice2.EType.shop ? "action_0008" : "action_0014";
            exitPlay = true;
            break;
          case 1002:
            int hour = TimeManager.ServerTime.Hour;
            cueID = hour > 3 ? (hour > 7 ? (hour > 11 ? (hour > 15 ? (hour > 19 ? "time_0023" : "time_0022") : "time_0021") : "time_0020") : "time_0019") : "time_0024";
            break;
          case 1003:
            cueID = FlowNode_ShopVoice2.GetEventCueID(this.mType, TimeManager.ServerTime, this.DateEventFileName);
            break;
          case 2000:
            cueID = this.CueID;
            direct = this.DirectCueID;
            break;
        }
        this.ActivateOutputLinks(!this.Play(cueID, direct, stopSec, exitPlay) ? 3 : 2);
        this.ActivateOutputLinks(1);
        break;
    }
  }

  public bool Play(string cueID, bool direct, float stopSec, bool exitPlay)
  {
    if (this.mVoice == null || this.mType != FlowNode_ShopVoice2.sType || (string.IsNullOrEmpty(cueID) || FlowNode_ShopVoice2.sDisableIdleVoice > 0))
      return false;
    bool flag = MySound.Voice.IsCueSheetPlaying(FlowNode_ShopVoice2.sType.ToString());
    if (flag)
      MySound.Voice.StopAll(FlowNode_ShopVoice2.sType.ToString(), stopSec, true);
    if (!exitPlay && flag)
    {
      FlowNode_ShopVoice2.sDelayCueID = cueID;
      FlowNode_ShopVoice2.sDelayCueIDDirect = direct;
      return true;
    }
    float delaySec = !exitPlay || !flag ? 0.0f : stopSec;
    if (direct)
      this.mVoice.PlayDirect(cueID, delaySec);
    else
      this.mVoice.Play(cueID, delaySec, false);
    FlowNode_ShopVoice2.sDelayCueID = (string) null;
    return true;
  }

  private void Update()
  {
    if (this.mWaitingForPlayEnd)
    {
      if (MySound.Voice.IsCueSheetPlaying(FlowNode_ShopVoice2.sType.ToString()))
      {
        this.mWaitSecAfterPlayEnd = this.WaitSecAfterPlayEnd;
      }
      else
      {
        this.mWaitSecAfterPlayEnd -= Time.get_unscaledDeltaTime();
        if ((double) this.mWaitSecAfterPlayEnd <= 0.0)
        {
          this.mWaitingForPlayEnd = false;
          this.ActivateOutputLinks(1);
        }
      }
    }
    if (this.mVoice == null || string.IsNullOrEmpty(FlowNode_ShopVoice2.sDelayCueID) || MySound.Voice.IsCueSheetPlaying(FlowNode_ShopVoice2.sType.ToString()))
      return;
    if (FlowNode_ShopVoice2.sDelayCueIDDirect)
      this.mVoice.PlayDirect(FlowNode_ShopVoice2.sDelayCueID, 0.0f);
    else
      this.mVoice.Play(FlowNode_ShopVoice2.sDelayCueID, 0.0f, false);
    FlowNode_ShopVoice2.sDelayCueID = (string) null;
  }

  public static string GetEventCueID(FlowNode_ShopVoice2.EType type, DateTime dt, string fileName)
  {
    int index = (int) type;
    int length = 3;
    if (index < 0 || index >= length)
      return (string) null;
    if (FlowNode_ShopVoice2.sDateEvent == null)
      FlowNode_ShopVoice2.sDateEvent = new List<FlowNode_ShopVoice2.DateEvent>[length];
    if (FlowNode_ShopVoice2.sDateEvent == null)
      return (string) null;
    if (FlowNode_ShopVoice2.sDateEvent[index] == null)
    {
      FlowNode_ShopVoice2.sDateEvent[index] = new List<FlowNode_ShopVoice2.DateEvent>();
      string path = fileName;
      string s;
      if (GameUtility.Config_UseAssetBundles.Value)
      {
        s = AssetManager.LoadTextData(path);
      }
      else
      {
        TextAsset textAsset = (TextAsset) Resources.Load<TextAsset>(path);
        s = !UnityEngine.Object.op_Inequality((UnityEngine.Object) textAsset, (UnityEngine.Object) null) ? (string) null : textAsset.get_text();
      }
      if (string.IsNullOrEmpty(s))
        return (string) null;
      using (StringReader stringReader = new StringReader(s))
      {
        string str;
        while ((str = stringReader.ReadLine()) != null)
        {
          if (!string.IsNullOrEmpty(str) && !str.StartsWith(";"))
          {
            string[] strArray = str.Split('\t');
            if (strArray != null && strArray.Length >= 3)
            {
              FlowNode_ShopVoice2.DateEvent dateEvent = new FlowNode_ShopVoice2.DateEvent();
              if (DateTime.TryParse(strArray[0], out dateEvent.startDate) && DateTime.TryParse(strArray[1], out dateEvent.endDate))
              {
                dateEvent.cueID = strArray[2];
                FlowNode_ShopVoice2.sDateEvent[index].Add(dateEvent);
              }
            }
          }
        }
      }
    }
    using (List<FlowNode_ShopVoice2.DateEvent>.Enumerator enumerator = FlowNode_ShopVoice2.sDateEvent[index].GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        FlowNode_ShopVoice2.DateEvent current = enumerator.Current;
        if (current.startDate <= dt && dt <= current.endDate)
          return current.cueID;
      }
    }
    return (string) null;
  }

  public enum EType
  {
    none = -1,
    shop = 0,
    sakaba = 1,
    yadoya = 2,
    Num = 3,
  }

  private class DateEvent
  {
    public DateTime startDate;
    public DateTime endDate;
    public string cueID;
  }
}
