namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(ScrollListController))]
    public class ScrollClamped_VersusReward : MonoBehaviour, ScrollListSetUp
    {
        public float Space;
        public bool Arrival;
        private int m_Max;
        private List<VersusTowerParam> m_Param;

        public ScrollClamped_VersusReward()
        {
            this.Space = 1f;
            this.Arrival = 1;
            this.m_Param = new List<VersusTowerParam>();
            base..ctor();
            return;
        }

        public unsafe void OnSetUpItems()
        {
            GameManager manager;
            VersusTowerParam[] paramArray;
            int num;
            ScrollListController controller;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.GetVersusTowerParam();
            if (paramArray == null)
            {
                goto Label_00B7;
            }
            num = ((int) paramArray.Length) - 1;
            goto Label_009F;
        Label_001E:
            if (string.Equals(paramArray[num].VersusTowerID, manager.VersusTowerMatchName) != null)
            {
                goto Label_0040;
            }
            goto Label_009B;
        Label_0040:
            if (this.Arrival == null)
            {
                goto Label_006C;
            }
            if (string.IsNullOrEmpty(paramArray[num].ArrivalIteminame) == null)
            {
                goto Label_008D;
            }
            goto Label_009B;
            goto Label_008D;
        Label_006C:
            if (paramArray[num].SeasonIteminame == null)
            {
                goto Label_009B;
            }
            if (((int) paramArray[num].SeasonIteminame.Length) != null)
            {
                goto Label_008D;
            }
            goto Label_009B;
        Label_008D:
            this.m_Param.Add(paramArray[num]);
        Label_009B:
            num -= 1;
        Label_009F:
            if (num >= 0)
            {
                goto Label_001E;
            }
            this.m_Max = this.m_Param.Count;
        Label_00B7:
            controller = base.GetComponent<ScrollListController>();
            controller.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            base.GetComponentInParent<ScrollRect>().set_movementType(2);
            transform = base.GetComponent<RectTransform>();
            vector = transform.get_sizeDelta();
            vector2 = transform.get_anchoredPosition();
            &vector2.y = 0f;
            &vector.y = (controller.ItemScale * this.Space) * ((float) this.m_Max);
            transform.set_sizeDelta(vector);
            transform.set_anchoredPosition(vector2);
            return;
        }

        public void OnUpdateItems(int idx, GameObject obj)
        {
            VersusTowerRewardItem item;
            VersusSeasonRewardInfo info;
            if (idx < 0)
            {
                goto Label_001E;
            }
            if (idx >= this.m_Max)
            {
                goto Label_001E;
            }
            if (this.m_Param != null)
            {
                goto Label_002A;
            }
        Label_001E:
            obj.SetActive(0);
            goto Label_0087;
        Label_002A:
            obj.SetActive(1);
            DataSource.Bind<VersusTowerParam>(obj, this.m_Param[idx]);
            if (this.Arrival == null)
            {
                goto Label_006E;
            }
            item = obj.GetComponent<VersusTowerRewardItem>();
            if ((item != null) == null)
            {
                goto Label_0087;
            }
            item.Refresh(0, 0);
            goto Label_0087;
        Label_006E:
            info = obj.GetComponent<VersusSeasonRewardInfo>();
            if ((info != null) == null)
            {
                goto Label_0087;
            }
            info.Refresh();
        Label_0087:
            return;
        }

        public void Start()
        {
        }
    }
}

