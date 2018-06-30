namespace SRPG
{
    using System;
    using UnityEngine;

    public class MultiPlayJoinQuest : MonoBehaviour
    {
        public MultiPlayJoinQuest()
        {
            base..ctor();
            return;
        }

        public void OnClickAll()
        {
            GlobalVars.SelectedQuestID = string.Empty;
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "SEARCH_CATEGORY_QUEST");
            return;
        }
    }
}

