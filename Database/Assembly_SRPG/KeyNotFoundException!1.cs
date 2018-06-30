namespace SRPG
{
    using System;

    public class KeyNotFoundException<T> : Exception
    {
        public KeyNotFoundException(string key)
        {
            base..ctor(typeof(T).ToString() + " '" + key + "' doesn't exist.");
            return;
        }
    }
}

