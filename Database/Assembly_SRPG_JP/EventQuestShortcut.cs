// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestShortcut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class EventQuestShortcut : MonoBehaviour
  {
    public GameObject KeyQuestOpenEffect;

    public EventQuestShortcut()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.RefreshSwitchButton();
    }

    private void RefreshSwitchButton()
    {
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      bool flag = false;
      if (chapters != null)
      {
        long serverTime = Network.GetServerTime();
        for (int index = 0; index < chapters.Length; ++index)
        {
          if (chapters[index].IsKeyQuest() && chapters[index].IsKeyUnlock(serverTime))
            flag = true;
        }
      }
      if (!Object.op_Inequality((Object) this.KeyQuestOpenEffect, (Object) null))
        return;
      this.KeyQuestOpenEffect.SetActive(flag);
    }
  }
}
