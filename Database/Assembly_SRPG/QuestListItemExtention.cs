namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuestListItemExtention : MonoBehaviour, IGameParameter
    {
        [SerializeField]
        private LayoutElement m_LayoutElement;
        private Vector2 m_InitialLayoutElementMinSize;
        private Vector2 m_InitialLayoutElementPreferredSize;

        public QuestListItemExtention()
        {
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            Vector2 vector;
            Vector2 vector2;
            vector = new Vector2();
            vector2 = new Vector2();
            &vector.x = this.m_LayoutElement.get_minWidth();
            &vector.y = this.m_LayoutElement.get_minHeight();
            &vector2.x = this.m_LayoutElement.get_preferredWidth();
            &vector2.y = this.m_LayoutElement.get_minHeight();
            this.m_InitialLayoutElementMinSize = vector;
            this.m_InitialLayoutElementPreferredSize = vector2;
            return;
        }

        private unsafe void Update()
        {
            object[] objArray1;
            bool flag;
            bool flag2;
            int num;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            this.m_LayoutElement.set_minHeight(160f);
            flag = 0;
            flag2 = base.get_gameObject().get_activeInHierarchy();
            num = 0;
            goto Label_0042;
        Label_0025:
            flag |= base.get_transform().GetChild(num).get_gameObject().get_activeInHierarchy();
            num += 1;
        Label_0042:
            if (num < base.get_transform().get_childCount())
            {
                goto Label_0025;
            }
            base.get_gameObject().SetActive(flag);
            base.set_enabled(0);
            if (flag == null)
            {
                goto Label_0114;
            }
            transform = base.GetComponent<RectTransform>();
            if (&this.m_InitialLayoutElementMinSize.x == 0f)
            {
                goto Label_00A7;
            }
            this.m_LayoutElement.set_minWidth(&transform.get_sizeDelta().x);
            goto Label_00C1;
        Label_00A7:
            this.m_LayoutElement.set_preferredWidth(&transform.get_sizeDelta().x);
        Label_00C1:
            if (&this.m_InitialLayoutElementMinSize.y == 0f)
            {
                goto Label_00F5;
            }
            this.m_LayoutElement.set_minHeight(&transform.get_sizeDelta().y);
            goto Label_010F;
        Label_00F5:
            this.m_LayoutElement.set_preferredHeight(&transform.get_sizeDelta().y);
        Label_010F:
            goto Label_016C;
        Label_0114:
            this.m_LayoutElement.set_minWidth(&this.m_InitialLayoutElementMinSize.x);
            this.m_LayoutElement.set_minHeight(&this.m_InitialLayoutElementMinSize.y);
            this.m_LayoutElement.set_preferredWidth(&this.m_InitialLayoutElementPreferredSize.x);
            this.m_LayoutElement.set_preferredHeight(&this.m_InitialLayoutElementPreferredSize.y);
        Label_016C:
            objArray1 = new object[] { (bool) flag2, " => ", (bool) flag, " child (", (int) base.get_transform().get_childCount(), ")" };
            Debug.Log(string.Concat(objArray1));
            return;
        }

        public void UpdateValue()
        {
            base.set_enabled(1);
            return;
        }
    }
}

