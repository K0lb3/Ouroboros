namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(ScrollListController))]
    public class ScrollClamped_MultiTower : MonoBehaviour, ScrollListSetUp
    {
        private readonly float OFFSET;
        private readonly int MARGIN;
        private int mMax;
        public float Space;
        public ScrollAutoFit AutoFit;
        public MultiTowerInfo TowerInfo;

        public ScrollClamped_MultiTower()
        {
            this.OFFSET = 2f;
            this.MARGIN = 5;
            this.Space = 1f;
            base..ctor();
            return;
        }

        public unsafe void OnSetUpItems()
        {
            List<MultiTowerFloorParam> list;
            ScrollListController controller;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            float num;
            float num2;
            list = MonoSingleton<GameManager>.Instance.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
            if (list == null)
            {
                goto Label_0022;
            }
            this.mMax = list.Count;
        Label_0022:
            this.mMax += this.MARGIN;
            controller = base.GetComponent<ScrollListController>();
            controller.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            base.GetComponentInParent<ScrollRect>().set_movementType(2);
            transform = base.GetComponent<RectTransform>();
            vector = transform.get_sizeDelta();
            vector2 = transform.get_anchoredPosition();
            num = controller.ItemScale * this.Space;
            num2 = num - controller.ItemScale;
            &vector2.y = controller.ItemScale * this.OFFSET;
            &vector.y = (num * ((float) (this.mMax - this.MARGIN))) - num2;
            transform.set_sizeDelta(vector);
            transform.set_anchoredPosition(vector2);
            if ((this.AutoFit != null) == null)
            {
                goto Label_00F7;
            }
            this.AutoFit.ItemScale = controller.ItemScale * this.Space;
        Label_00F7:
            this.TowerInfo.Init();
            return;
        }

        public void OnUpdateItems(int idx, GameObject obj)
        {
            GameManager manager;
            MultiTowerFloorParam param;
            DataSource source;
            MultiTowerFloorInfo info;
            manager = MonoSingleton<GameManager>.Instance;
            if (idx < 0)
            {
                goto Label_0019;
            }
            if (idx < this.mMax)
            {
                goto Label_0025;
            }
        Label_0019:
            obj.SetActive(0);
            goto Label_007F;
        Label_0025:
            obj.SetActive(1);
            param = manager.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, idx + 1);
            if (param == null)
            {
                goto Label_004D;
            }
            DataSource.Bind<MultiTowerFloorParam>(obj, param);
            goto Label_0066;
        Label_004D:
            source = obj.GetComponent<DataSource>();
            if ((source != null) == null)
            {
                goto Label_0066;
            }
            source.Clear();
        Label_0066:
            info = obj.GetComponent<MultiTowerFloorInfo>();
            if ((info != null) == null)
            {
                goto Label_007F;
            }
            info.Refresh();
        Label_007F:
            return;
        }

        public void Start()
        {
        }
    }
}

