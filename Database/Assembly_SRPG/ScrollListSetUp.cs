namespace SRPG
{
    using System;
    using UnityEngine;

    public interface ScrollListSetUp
    {
        void OnSetUpItems();
        void OnUpdateItems(int idx, GameObject obj);
    }
}

