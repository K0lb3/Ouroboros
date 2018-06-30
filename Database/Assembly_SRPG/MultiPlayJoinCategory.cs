namespace SRPG
{
    using System;
    using UnityEngine;

    public class MultiPlayJoinCategory : MonoBehaviour
    {
        public MultiPlayJoinCategory()
        {
            base..ctor();
            return;
        }

        public void OnClickAll()
        {
            GlobalVars.SelectedMultiPlayArea = string.Empty;
            GlobalVars.SelectedQuestID = string.Empty;
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "SELECT_ALL_ROOM");
            return;
        }
    }
}

