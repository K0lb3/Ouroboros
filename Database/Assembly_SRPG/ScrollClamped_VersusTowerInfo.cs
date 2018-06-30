namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(ScrollListController))]
    public class ScrollClamped_VersusTowerInfo : MonoBehaviour, ScrollListSetUp
    {
        private readonly int MARGIN;
        public float Space;
        private int m_Max;

        public ScrollClamped_VersusTowerInfo()
        {
            this.MARGIN = 5;
            this.Space = 1f;
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
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.GetVersusTowerParam();
            if (paramArray == null)
            {
                goto Label_006A;
            }
            num = 0;
            goto Label_004E;
        Label_001A:
            if (string.Equals(paramArray[num].VersusTowerID, manager.VersusTowerMatchName) != null)
            {
                goto Label_003C;
            }
            goto Label_004A;
        Label_003C:
            this.m_Max += 1;
        Label_004A:
            num += 1;
        Label_004E:
            if (num < ((int) paramArray.Length))
            {
                goto Label_001A;
            }
            this.m_Max += this.MARGIN;
        Label_006A:
            controller = base.GetComponent<ScrollListController>();
            controller.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            base.GetComponentInParent<ScrollRect>().set_movementType(2);
            transform = base.GetComponent<RectTransform>();
            vector = transform.get_sizeDelta();
            &vector.y = (controller.ItemScale * this.Space) * ((float) this.m_Max);
            transform.set_sizeDelta(vector);
            return;
        }

        public void OnUpdateItems(int idx, GameObject obj)
        {
            VersusTowerFloor floor;
            if (idx < 0)
            {
                goto Label_0013;
            }
            if (idx < this.m_Max)
            {
                goto Label_001F;
            }
        Label_0013:
            obj.SetActive(0);
            goto Label_004D;
        Label_001F:
            obj.SetActive(1);
            floor = obj.GetComponent<VersusTowerFloor>();
            if ((floor != null) == null)
            {
                goto Label_004D;
            }
            floor.Refresh(idx, this.m_Max - this.MARGIN);
        Label_004D:
            return;
        }

        public void Start()
        {
        }
    }
}

