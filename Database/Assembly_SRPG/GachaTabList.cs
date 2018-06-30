namespace SRPG
{
    using System;
    using UnityEngine;

    public class GachaTabList : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private GameObject TabTemplate;
        [SerializeField]
        private GameObject RareTab;
        [SerializeField]
        private GameObject NormalTab;
        [SerializeField]
        private GameObject ArtifactTab;
        [SerializeField]
        private GameObject TicketTab;

        public GachaTabList()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Refresh()
        {
        }

        private void Start()
        {
            if ((this.TabTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.TabTemplate.SetActive(0);
        Label_001D:
            if ((this.RareTab != null) == null)
            {
                goto Label_003A;
            }
            this.RareTab.SetActive(0);
        Label_003A:
            if ((this.NormalTab != null) == null)
            {
                goto Label_0057;
            }
            this.NormalTab.SetActive(0);
        Label_0057:
            if ((this.ArtifactTab != null) == null)
            {
                goto Label_0074;
            }
            this.ArtifactTab.SetActive(0);
        Label_0074:
            if ((this.TicketTab != null) == null)
            {
                goto Label_0091;
            }
            this.TicketTab.SetActive(0);
        Label_0091:
            return;
        }

        private void Update()
        {
        }
    }
}

