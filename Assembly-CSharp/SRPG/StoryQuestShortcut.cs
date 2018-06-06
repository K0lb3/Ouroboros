// Decompiled with JetBrains decompiler
// Type: SRPG.StoryQuestShortcut
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "イベントページへ", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "キークエスト", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "ノーマルクエスト", FlowNode.PinTypes.Input, 0)]
  public class StoryQuestShortcut : MonoBehaviour, IFlowInterface
  {
    public Button EventQuestButton;
    public Button KeyQuestButton;
    public GameObject KeyQuestOpenEffect;

    public StoryQuestShortcut()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      bool flag1 = false;
      bool flag2 = false;
      if (chapters != null)
      {
        long serverTime = Network.GetServerTime();
        for (int index = 0; index < chapters.Length; ++index)
        {
          if (chapters[index].IsKeyQuest())
          {
            if (chapters[index].IsDateUnlock(serverTime))
              flag2 = true;
            if (chapters[index].IsKeyUnlock(serverTime))
              flag1 = true;
          }
        }
      }
      if (Object.op_Inequality((Object) this.KeyQuestOpenEffect, (Object) null))
        this.KeyQuestOpenEffect.SetActive(flag1);
      if (!Object.op_Inequality((Object) this.KeyQuestButton, (Object) null))
        return;
      ((Component) this.KeyQuestButton).get_gameObject().SetActive(true);
      ((Selectable) this.KeyQuestButton).set_interactable(flag2);
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
