// Decompiled with JetBrains decompiler
// Type: SRPG.BeginnerQuestIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class BeginnerQuestIcon : MonoBehaviour, IFlowInterface
  {
    public BeginnerQuestIcon()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      bool flag = true;
      foreach (QuestParam quest in MonoSingleton<GameManager>.Instance.Quests)
      {
        if (quest.type == QuestTypes.Beginner && quest.state != QuestStates.Cleared)
        {
          flag = false;
          break;
        }
      }
      ((Component) this).get_gameObject().SetActive(!flag);
    }
  }
}
