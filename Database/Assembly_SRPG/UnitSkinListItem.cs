namespace SRPG
{
    using System;
    using UnityEngine;

    public class UnitSkinListItem : ListItemEvents
    {
        public ListItemEvents.ListItemEvent OnSelectAll;
        public ListItemEvents.ListItemEvent OnRemoveAll;
        public SRPG_Button Button;
        public GameObject Lock;

        public UnitSkinListItem()
        {
            base..ctor();
            return;
        }

        public void RemoveAll()
        {
            if (this.OnRemoveAll == null)
            {
                goto Label_001C;
            }
            this.OnRemoveAll(base.get_gameObject());
        Label_001C:
            return;
        }

        public void SelectAll()
        {
            if (this.OnSelectAll == null)
            {
                goto Label_001C;
            }
            this.OnSelectAll(base.get_gameObject());
        Label_001C:
            return;
        }
    }
}

