// Decompiled with JetBrains decompiler
// Type: FlowNode_NaviVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;

[FlowNode.Pin(109, "設定画面", FlowNode.PinTypes.Input, 109)]
[FlowNode.Pin(111, "レベルアップ", FlowNode.PinTypes.Input, 111)]
[FlowNode.Pin(110, "招待ページ", FlowNode.PinTypes.Input, 110)]
[FlowNode.NodeType("NaviVoice", 32741)]
[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(2, "再生終了", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(101, "放置ON", FlowNode.PinTypes.Input, 101)]
[FlowNode.Pin(102, "放置OFF", FlowNode.PinTypes.Input, 102)]
[FlowNode.Pin(103, "編成画面", FlowNode.PinTypes.Input, 103)]
[FlowNode.Pin(104, "ユニット強化画面", FlowNode.PinTypes.Input, 104)]
[FlowNode.Pin(105, "ガチャ画面", FlowNode.PinTypes.Input, 105)]
[FlowNode.Pin(106, "プレゼント画面", FlowNode.PinTypes.Input, 106)]
[FlowNode.Pin(107, "アイテム画面", FlowNode.PinTypes.Input, 107)]
[FlowNode.Pin(108, "ヘルプ画面", FlowNode.PinTypes.Input, 108)]
public class FlowNode_NaviVoice : FlowNodePersistent
{
  private float IdleVoiceWaitTime = 30f;
  private MySound.Voice mVoice;
  private bool mWaitingForPlayEnd;
  private bool mPlayIdleVoice;
  private float mPlayIdleVoiceWait;
  private List<string> mNotifyCueList;
  private Random mRandom;

  protected override void Awake()
  {
    base.Awake();
    this.mVoice = new MySound.Voice("uroboros");
  }

  public override void OnActivate(int pinID)
  {
    if (pinID == 101 || pinID == 102)
    {
      this.mPlayIdleVoice = pinID == 101;
      this.mPlayIdleVoiceWait = this.IdleVoiceWaitTime;
      if (pinID == 101)
      {
        if (this.mNotifyCueList == null)
          this.mNotifyCueList = new List<string>();
        if (this.mRandom == null)
          this.mRandom = new Random();
        this.mNotifyCueList.Clear();
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if ((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.UnitUnlock) != ~GameManager.BadgeTypes.All)
          this.mNotifyCueList.Add("navi_0002");
        if ((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.GoldGacha) != ~GameManager.BadgeTypes.All || (MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.RareGacha) != ~GameManager.BadgeTypes.All)
          this.mNotifyCueList.Add("navi_0003");
        if (player.Stamina >= player.StaminaMax)
          this.mNotifyCueList.Add("navi_0004");
        if (player.Stamina < player.StaminaMax / 4)
          this.mNotifyCueList.Add("navi_0005");
      }
      this.ActivateOutputLinks(1);
    }
    else
    {
      string cueID = (string) null;
      switch (pinID)
      {
        case 103:
          cueID = "navi_0008";
          break;
        case 104:
          cueID = "navi_0009";
          break;
        case 105:
          cueID = "navi_0010";
          break;
        case 106:
          if ((MonoSingleton<GameManager>.Instance.BadgeFlags & GameManager.BadgeTypes.GiftBox) == ~GameManager.BadgeTypes.All)
          {
            this.ActivateOutputLinks(1);
            return;
          }
          cueID = "navi_0011";
          break;
        case 107:
          cueID = "navi_0012";
          break;
        case 108:
          cueID = "navi_0013";
          break;
        case 109:
          cueID = "navi_0014";
          break;
        case 110:
          cueID = "navi_0015";
          break;
        case 111:
          cueID = "navi_0016";
          break;
      }
      this.Play(cueID);
      this.mWaitingForPlayEnd = true;
      this.ActivateOutputLinks(1);
    }
  }

  public void Play(string cueID)
  {
    if (this.mVoice == null || MySound.Voice.IsCueSheetPlaying(this.mVoice.CharName) || string.IsNullOrEmpty(cueID))
      return;
    this.mVoice.Play(cueID, 0.0f, false);
  }

  private void Update()
  {
    if (this.mVoice != null && MySound.Voice.IsCueSheetPlaying(this.mVoice.CharName))
      this.mPlayIdleVoiceWait = this.IdleVoiceWaitTime;
    else if (this.mWaitingForPlayEnd)
    {
      this.mWaitingForPlayEnd = false;
      this.ActivateOutputLinks(2);
    }
    else
    {
      if (!this.mPlayIdleVoice)
        return;
      if ((double) this.mPlayIdleVoiceWait >= 0.0)
        this.mPlayIdleVoiceWait -= Time.get_unscaledDeltaTime();
      if ((double) this.mPlayIdleVoiceWait >= 0.0)
        return;
      string cueID = "favorite_random";
      if (this.mNotifyCueList != null && this.mNotifyCueList.Count > 0 && this.mRandom != null)
      {
        int num = this.mRandom.Next();
        if (num % 2 != 0)
        {
          int index = num % this.mNotifyCueList.Count;
          cueID = this.mNotifyCueList[index];
          this.mNotifyCueList.RemoveAt(index);
        }
      }
      this.Play(cueID);
      this.mPlayIdleVoiceWait = this.IdleVoiceWaitTime;
      this.mWaitingForPlayEnd = true;
    }
  }
}
