// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayJoinQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
