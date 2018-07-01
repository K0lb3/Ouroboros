// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayJoinQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class MultiPlayJoinQuest : MonoBehaviour
  {
    public MultiPlayJoinQuest()
    {
      base.\u002Ector();
    }

    public void OnClickAll()
    {
      GlobalVars.SelectedQuestID = string.Empty;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "SEARCH_CATEGORY_QUEST");
    }
  }
}
