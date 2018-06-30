namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(100, "念装詳細　開く", 1, 100), Pin(1, "念装詳細　閉じ", 0, 1)]
    public class GetConceptCardListWindow : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_CLOSED_DETAIL = 1;
        private const int OUTPUT_OPEN_DETAIL = 100;
        [SerializeField]
        private GameObject m_ListItemTemplate;
        [SerializeField]
        private RectTransform m_ContentRoot;
        private List<GameObject> m_ListItems;
        private static string s_SelectedConceptCardID;

        static GetConceptCardListWindow()
        {
            s_SelectedConceptCardID = string.Empty;
            return;
        }

        public GetConceptCardListWindow()
        {
            this.m_ListItems = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0007;
            }
        Label_0007:
            return;
        }

        private void Awake()
        {
            GameUtility.SetGameObjectActive(this.m_ListItemTemplate, 0);
            return;
        }

        private void ClearListItems()
        {
            int num;
            if (this.m_ListItems.Count <= 0)
            {
                goto Label_0056;
            }
            num = 0;
            goto Label_003A;
        Label_0018:
            Object.Destroy(this.m_ListItems[num]);
            this.m_ListItems[num] = null;
            num += 1;
        Label_003A:
            if (num < this.m_ListItems.Count)
            {
                goto Label_0018;
            }
            this.m_ListItems.Clear();
        Label_0056:
            return;
        }

        public static void ClearSelectedConceptCard()
        {
            s_SelectedConceptCardID = string.Empty;
            return;
        }

        private GameObject CreateListItem(ConceptCardData data)
        {
            GameObject obj2;
            ConceptCardIcon icon;
            ListItemEvents events;
            obj2 = Object.Instantiate<GameObject>(this.m_ListItemTemplate);
            obj2.get_transform().SetParent(this.m_ContentRoot, 0);
            obj2.SetActive(1);
            icon = obj2.GetComponentInChildren<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_003F;
            }
            icon.Setup(data);
        Label_003F:
            events = obj2.GetComponentInChildren<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0074;
            }
            events.OnSelect = (ListItemEvents.ListItemEvent) Delegate.Combine(events.OnSelect, new ListItemEvents.ListItemEvent(this.OnListItemSelect));
        Label_0074:
            return obj2;
        }

        public static string GetSelectedConceptCard()
        {
            return s_SelectedConceptCardID;
        }

        private void OnListItemSelect(GameObject go)
        {
            ConceptCardIcon icon;
            icon = go.GetComponentInChildren<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_0041;
            }
            if (icon.ConceptCard == null)
            {
                goto Label_0041;
            }
            GlobalVars.SelectedConceptCardData.Set(icon.ConceptCard);
            SetSelectedConceptCard(icon.ConceptCard);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0041:
            return;
        }

        public static void SetSelectedConceptCard(ConceptCardData data)
        {
            ClearSelectedConceptCard();
            if (data == null)
            {
                goto Label_0016;
            }
            if (data.Param != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            s_SelectedConceptCardID = data.Param.iname;
            return;
        }

        public void Setup(ConceptCardData[] data)
        {
            int num;
            GameObject obj2;
            if ((this.m_ListItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.ClearListItems();
            num = 0;
            goto Label_0039;
        Label_001F:
            obj2 = this.CreateListItem(data[num]);
            this.m_ListItems.Add(obj2);
            num += 1;
        Label_0039:
            if (num < ((int) data.Length))
            {
                goto Label_001F;
            }
            return;
        }
    }
}

