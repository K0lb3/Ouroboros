namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GlobalEvent
    {
        private static Dictionary<string, Delegate> mListeners;

        static GlobalEvent()
        {
            mListeners = new Dictionary<string, Delegate>();
            return;
        }

        public GlobalEvent()
        {
            base..ctor();
            return;
        }

        public static void AddListener(string eventName, Delegate callback)
        {
            Dictionary<string, Delegate> dictionary;
            string str;
            Delegate delegate2;
            if (mListeners.ContainsKey(eventName) == null)
            {
                goto Label_0038;
            }
            delegate2 = dictionary[str];
            (dictionary = mListeners)[str = eventName] = (Delegate) Delegate.Combine(delegate2, callback);
            goto Label_0044;
        Label_0038:
            mListeners[eventName] = callback;
        Label_0044:
            return;
        }

        public static void Invoke(string eventName, object param)
        {
            if (mListeners.ContainsKey(eventName) == null)
            {
                goto Label_0021;
            }
            mListeners[eventName](param);
        Label_0021:
            return;
        }

        public static void RemoveListener(string eventName, Delegate callback)
        {
            Dictionary<string, Delegate> dictionary;
            string str;
            Delegate delegate2;
            if (mListeners.ContainsKey(eventName) == null)
            {
                goto Label_004F;
            }
            delegate2 = dictionary[str];
            (dictionary = mListeners)[str = eventName] = (Delegate) Delegate.Remove(delegate2, callback);
            if (mListeners[eventName] != null)
            {
                goto Label_004F;
            }
            mListeners.Remove(eventName);
        Label_004F:
            return;
        }

        public delegate void Delegate(object caller);
    }
}

