namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(ScrollListController))]
    public class ScrollClamped : MonoBehaviour, ScrollListSetUp
    {
        private int m_Max;
        private int[] m_CategoryNum;

        public ScrollClamped()
        {
            base..ctor();
            return;
        }

        public unsafe void OnSetUpItems()
        {
            HelpWindow window;
            ScrollListController controller;
            RectTransform transform;
            Vector2 vector;
            string str;
            window = base.get_transform().GetComponentInParent<HelpWindow>();
            if ((window == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            controller = base.GetComponent<ScrollListController>();
            controller.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            base.GetComponentInParent<ScrollRect>().set_movementType(2);
            transform = base.GetComponent<RectTransform>();
            vector = transform.get_sizeDelta();
            str = "0";
            if (window.MiddleHelp == null)
            {
                goto Label_007C;
            }
            this.m_Max = this.m_CategoryNum[window.SelectMiddleID];
            goto Label_00A2;
        Label_007C:
            str = LocalizedText.Get("help.MENU_L_NUM");
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0095;
            }
            return;
        Label_0095:
            this.m_Max = int.Parse(str);
        Label_00A2:
            &vector.y = (controller.ItemScale * 1.2f) * ((float) this.m_Max);
            transform.set_sizeDelta(vector);
            return;
        }

        public void OnUpdateItems(int idx, GameObject obj)
        {
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
            goto Label_0037;
        Label_001F:
            obj.SetActive(1);
            obj.SendMessage("UpdateParam", (int) idx);
        Label_0037:
            return;
        }

        public void Start()
        {
            string str;
            int num;
            string str2;
            int num2;
            string str3;
            string str4;
            int num3;
            int num4;
            int num5;
            str = LocalizedText.Get("help.MENU_L_NUM");
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            num = int.Parse(str);
            str2 = LocalizedText.Get("help.MENU_NUM");
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_0035;
            }
            return;
        Label_0035:
            num2 = int.Parse(str2);
            this.m_CategoryNum = new int[num];
            num3 = 0;
            num4 = 0;
            num5 = 0;
            goto Label_00CA;
        Label_0056:
            str3 = LocalizedText.Get("help.MENU_CATE_NAME_" + ((int) (num5 + 1)));
            num4 = 0;
            goto Label_00B1;
        Label_0078:
            if (string.Equals(LocalizedText.Get("help.MENU_CATE_" + ((int) (num3 + 1))), str3) != null)
            {
                goto Label_00A5;
            }
            goto Label_00B9;
        Label_00A5:
            num4 += 1;
            num3 += 1;
        Label_00B1:
            if (num3 < num2)
            {
                goto Label_0078;
            }
        Label_00B9:
            this.m_CategoryNum[num5] = num4;
            num5 += 1;
        Label_00CA:
            if (num5 < num)
            {
                goto Label_0056;
            }
            return;
        }
    }
}

