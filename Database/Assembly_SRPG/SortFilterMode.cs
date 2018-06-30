namespace SRPG
{
    using System;
    using UnityEngine;

    public class SortFilterMode : MonoBehaviour, ISortableList
    {
        public GameObject Ascending;
        public GameObject Descending;
        public GameObject FilterOn;
        public GameObject FilterOff;

        public SortFilterMode()
        {
            base..ctor();
            return;
        }

        public void SetSortMethod(string method, bool ascending, string[] filters)
        {
            GameUtility.SetGameObjectActive(this.Ascending, ascending);
            GameUtility.SetGameObjectActive(this.Descending, ascending == 0);
            GameUtility.SetGameObjectActive(this.FilterOn, (filters == null) == 0);
            GameUtility.SetGameObjectActive(this.FilterOff, filters == null);
            return;
        }
    }
}

