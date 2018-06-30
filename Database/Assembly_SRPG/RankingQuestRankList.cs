namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class RankingQuestRankList : MonoBehaviour, ScrollListSetUp
    {
        private float Space;
        private int m_Max;
        private RankingQuestUserData[] m_UserDatas;
        private RankingQuestRankWindow m_RankingWindow;

        public RankingQuestRankList()
        {
            this.Space = 10f;
            base..ctor();
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            this.m_RankingWindow.OnItemSelect(go);
            return;
        }

        public unsafe void OnSetUpItems()
        {
            ScrollListController controller;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            if (this.m_UserDatas != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            controller = base.GetComponent<ScrollListController>();
            controller.OnItemUpdate.RemoveListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            controller.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            base.GetComponentInParent<ScrollRect>().set_movementType(2);
            transform = base.GetComponent<RectTransform>();
            vector = transform.get_sizeDelta();
            vector2 = transform.get_anchoredPosition();
            this.m_Max = (int) this.m_UserDatas.Length;
            controller.Space = (controller.ItemScale + this.Space) / controller.ItemScale;
            &vector2.y = 0f;
            &vector.y = (controller.ItemScale * controller.Space) * ((float) this.m_Max);
            transform.set_sizeDelta(vector);
            transform.set_anchoredPosition(vector2);
            return;
        }

        public void OnUpdateItems(int idx, GameObject obj)
        {
            ListItemEvents events;
            RankingQuestInfo info;
            if (this.m_UserDatas == null)
            {
                goto Label_0020;
            }
            if (idx < 0)
            {
                goto Label_0020;
            }
            if (idx < ((int) this.m_UserDatas.Length))
            {
                goto Label_002C;
            }
        Label_0020:
            obj.SetActive(0);
            goto Label_0092;
        Label_002C:
            obj.SetActive(1);
            events = obj.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0058;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        Label_0058:
            DataSource.Bind<RankingQuestUserData>(obj, this.m_UserDatas[idx]);
            DataSource.Bind<UnitData>(obj, this.m_UserDatas[idx].m_UnitData);
            info = obj.GetComponent<RankingQuestInfo>();
            if ((info != null) == null)
            {
                goto Label_0092;
            }
            info.UpdateValue();
        Label_0092:
            return;
        }

        public void SetData(RankingQuestUserData[] data)
        {
            this.m_UserDatas = data;
            return;
        }

        private void Start()
        {
            this.m_RankingWindow = base.GetComponentInParent<RankingQuestRankWindow>();
            return;
        }
    }
}

