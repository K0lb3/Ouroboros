namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(2, "Search", 0, 2), Pin(1, "Refresh", 0, 1)]
    public class VersusViewListManager : MonoBehaviour, IFlowInterface
    {
        public ScrollListController Scroll;
        public ScrollClamped_VersusViewList ViewList;

        public VersusViewListManager()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0044;
            }
            if ((this.Scroll != null) == null)
            {
                goto Label_0051;
            }
            if ((this.ViewList != null) == null)
            {
                goto Label_0051;
            }
            this.ViewList.OnSetUpItems();
            this.Scroll.Refresh();
            goto Label_0051;
        Label_0044:
            if (pinID != 2)
            {
                goto Label_0051;
            }
            this.Search();
        Label_0051:
            return;
        }

        private void Search()
        {
            GameManager manager;
            int num;
            MyPhoton photon;
            manager = MonoSingleton<GameManager>.Instance;
            num = GlobalVars.SelectedMultiPlayRoomID;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            manager.AudienceRoom = photon.SearchRoom(num);
            if (manager.AudienceRoom == null)
            {
                goto Label_005A;
            }
            if (manager.AudienceRoom.start == null)
            {
                goto Label_004A;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "AlreadyStartFriendMode");
            goto Label_0055;
        Label_004A:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "FindRoom");
        Label_0055:
            goto Label_0065;
        Label_005A:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "NotFindRoom");
        Label_0065:
            return;
        }
    }
}

