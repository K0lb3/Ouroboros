namespace SRPG
{
    using System;

    public interface ISortableList
    {
        void SetSortMethod(string method, bool ascending, string[] filters);
    }
}

