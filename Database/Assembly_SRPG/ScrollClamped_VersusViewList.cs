namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(ScrollListController))]
    public class ScrollClamped_VersusViewList : MonoBehaviour, ScrollListSetUp
    {
        private readonly string VS_LOBBY_NAME;
        private readonly string VS_FRIEND_SUFFIX;
        public float Space;
        private int m_Max;
        private List<MyPhoton.MyRoom> m_Rooms;

        public ScrollClamped_VersusViewList()
        {
            this.VS_LOBBY_NAME = "vs";
            this.VS_FRIEND_SUFFIX = "_friend";
            this.Space = 1f;
            this.m_Rooms = new List<MyPhoton.MyRoom>();
            base..ctor();
            return;
        }

        public unsafe void OnSetUpItems()
        {
            MyPhoton photon;
            List<MyPhoton.MyRoom> list;
            int num;
            ScrollListController controller;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (photon.CurrentState == 2)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (this.m_Rooms != null)
            {
                goto Label_0029;
            }
            this.m_Rooms = new List<MyPhoton.MyRoom>();
        Label_0029:
            this.m_Rooms.Clear();
            list = photon.GetRoomList();
            num = 0;
            goto Label_00A2;
        Label_0042:
            if ((list[num].lobby == this.VS_LOBBY_NAME) == null)
            {
                goto Label_009E;
            }
            if (list[num].name.IndexOf(this.VS_FRIEND_SUFFIX) != -1)
            {
                goto Label_009E;
            }
            if (list[num].start == null)
            {
                goto Label_009E;
            }
            this.m_Rooms.Add(list[num]);
        Label_009E:
            num += 1;
        Label_00A2:
            if (num < list.Count)
            {
                goto Label_0042;
            }
            this.m_Max = this.m_Rooms.Count;
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
            VersusViewRoomInfo info;
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
            goto Label_0051;
        Label_001F:
            obj.SetActive(1);
            DataSource.Bind<MyPhoton.MyRoom>(obj, this.m_Rooms[idx]);
            info = obj.GetComponent<VersusViewRoomInfo>();
            if ((info != null) == null)
            {
                goto Label_0051;
            }
            info.Refresh();
        Label_0051:
            return;
        }

        public void Start()
        {
        }
    }
}

