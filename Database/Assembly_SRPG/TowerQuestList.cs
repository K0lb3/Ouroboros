namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "更新", 0, 0), Pin(100, "フロア選択", 1, 100)]
    public class TowerQuestList : MonoBehaviour, IFlowInterface, ScrollListSetUp
    {
        [SerializeField]
        private TowerQuestInfo info;
        [SerializeField]
        private TowerScrollListController mScrollListController;
        [SerializeField]
        private ListItemEvents mListItemTemplate;
        [SerializeField]
        private Button mChallenge;
        private List<TowerFloorParam> mFloorParams;
        private bool isInitialized;

        public TowerQuestList()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0022;
            }
            if (this.isInitialized == null)
            {
                goto Label_001C;
            }
            this.Refresh();
            goto Label_0022;
        Label_001C:
            this.Initialize();
        Label_0022:
            return;
        }

        private void Initialize()
        {
            TowerFloorParam param;
            TowerFloorParam param2;
            this.isInitialized = 1;
            this.mFloorParams = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
            this.mListItemTemplate.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            this.mScrollListController.OnListItemFocus.AddListener(new UnityAction<GameObject>(this, this.OnScrollStop));
            this.mScrollListController.UpdateList();
            if (MonoSingleton<GameManager>.Instance.TowerResuponse == null)
            {
                goto Label_009E;
            }
            param = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
            if (param == null)
            {
                goto Label_00CE;
            }
            this.ScrollToCurrentFloor(param);
            GlobalVars.SelectedQuestID = param.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_00CE;
        Label_009E:
            param2 = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID);
            if (param2 == null)
            {
                goto Label_00CE;
            }
            this.ScrollToCurrentFloor(param2);
            GlobalVars.SelectedQuestID = param2.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_00CE:
            return;
        }

        private void OnScrollStop(GameObject go)
        {
            TowerFloorParam param;
            param = DataSource.FindDataOfClass<TowerFloorParam>(go, null);
            if (param != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            GlobalVars.SelectedQuestID = param.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnSelectItem(GameObject go)
        {
            TowerFloorParam param;
            int num;
            param = DataSource.FindDataOfClass<TowerFloorParam>(go, null);
            if (param != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            num = this.mFloorParams.IndexOf(param);
            this.mScrollListController.SetScrollTo((this.mScrollListController.ItemScaleMargin * ((float) num)) - (this.mScrollListController.ItemScaleMargin * 2f));
            GlobalVars.SelectedQuestID = param.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public unsafe void OnSetUpItems()
        {
            RectTransform transform;
            Vector2 vector;
            if (this.mFloorParams != null)
            {
                goto Label_0020;
            }
            this.mFloorParams = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
        Label_0020:
            this.mScrollListController.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            base.GetComponentInParent<ScrollRect>().set_movementType(2);
            transform = base.GetComponent<RectTransform>();
            vector = transform.get_sizeDelta();
            &vector.y = this.mScrollListController.ItemScaleMargin * ((float) this.mFloorParams.Count);
            transform.set_sizeDelta(vector);
            return;
        }

        public void OnUpdateItems(int idx, GameObject obj)
        {
            TowerQuestListItem item;
            TowerQuestListItem item2;
            ListItemEvents events;
            if (this.mFloorParams != null)
            {
                goto Label_0020;
            }
            this.mFloorParams = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
        Label_0020:
            if (idx >= 0)
            {
                goto Label_0033;
            }
            obj.SetActive(0);
            goto Label_00E8;
        Label_0033:
            if (idx < this.mFloorParams.Count)
            {
                goto Label_0072;
            }
            DataSource.Bind<TowerFloorParam>(obj, null);
            obj.SetActive(1);
            item = obj.GetComponent<TowerQuestListItem>();
            if ((item != null) == null)
            {
                goto Label_00E8;
            }
            item.UpdateParam(null, 0);
            goto Label_00E8;
        Label_0072:
            obj.SetActive(1);
            DataSource.Bind<TowerFloorParam>(obj, this.mFloorParams[idx]);
            item2 = obj.GetComponent<TowerQuestListItem>();
            if ((item2 != null) == null)
            {
                goto Label_00C3;
            }
            item2.UpdateParam(this.mFloorParams[idx], this.mFloorParams[idx].FloorIndex + 1);
        Label_00C3:
            events = obj.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_00E8;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        Label_00E8:
            return;
        }

        private void Refresh()
        {
            this.mFloorParams = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
            this.mScrollListController.UpdateList();
            this.ScrollToCurrentFloor();
            return;
        }

        public void ScrollToCurrentFloor()
        {
            TowerFloorParam param;
            int num;
            param = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
            num = this.mFloorParams.IndexOf(param);
            this.mScrollListController.SetScrollTo((this.mScrollListController.ItemScaleMargin * ((float) num)) - (this.mScrollListController.ItemScaleMargin * 2f));
            return;
        }

        public void ScrollToCurrentFloor(TowerFloorParam floorParam)
        {
            int num;
            num = this.mFloorParams.IndexOf(floorParam);
            this.mScrollListController.SetAnchoredPosition((this.mScrollListController.ItemScaleMargin * ((float) num)) - (this.mScrollListController.ItemScaleMargin * 2f));
            return;
        }
    }
}

