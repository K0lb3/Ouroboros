namespace SRPG
{
    using System;
    using UnityEngine;

    public class FilterDispatcher : MonoBehaviour, ISortableList
    {
        public GameObject[] Targets;

        public FilterDispatcher()
        {
            base..ctor();
            return;
        }

        public void SetSortMethod(string method, bool ascending, string[] filters)
        {
            GameObject obj2;
            GameObject[] objArray;
            int num;
            ISortableList list;
            if (this.Targets != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            objArray = this.Targets;
            num = 0;
            goto Label_0049;
        Label_001A:
            obj2 = objArray[num];
            if ((obj2 == null) == null)
            {
                goto Label_002F;
            }
            goto Label_0045;
        Label_002F:
            list = obj2.GetComponent<ISortableList>();
            if (list == null)
            {
                goto Label_0045;
            }
            list.SetSortMethod(method, ascending, filters);
        Label_0045:
            num += 1;
        Label_0049:
            if (num < ((int) objArray.Length))
            {
                goto Label_001A;
            }
            return;
        }
    }
}

