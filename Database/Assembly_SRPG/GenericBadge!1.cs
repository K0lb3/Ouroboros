namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    internal class GenericBadge<T> where T: class
    {
        public bool mValue;
        public T mRawData;

        public GenericBadge(T data, bool value)
        {
            base..ctor();
            this.mRawData = data;
            this.mValue = value;
            return;
        }
    }
}

