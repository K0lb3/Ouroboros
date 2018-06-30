namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class SceneAwakeObserver
    {
        private static SceneEvent mListeners;
        [CompilerGenerated]
        private static SceneEvent <>f__am$cache1;
        [CompilerGenerated]
        private static SceneEvent <>f__am$cache2;

        static SceneAwakeObserver()
        {
            if (<>f__am$cache1 != null)
            {
                goto Label_0018;
            }
            <>f__am$cache1 = new SceneEvent(SceneAwakeObserver.<mListeners>m__15D);
        Label_0018:
            mListeners = <>f__am$cache1;
            return;
        }

        [CompilerGenerated]
        private static void <ClearListeners>m__15E(GameObject go)
        {
        }

        [CompilerGenerated]
        private static void <mListeners>m__15D(GameObject go)
        {
        }

        public static void AddListener(SceneEvent listener)
        {
            mListeners = (SceneEvent) Delegate.Combine(mListeners, listener);
            return;
        }

        public static void ClearListeners()
        {
            if (<>f__am$cache2 != null)
            {
                goto Label_0018;
            }
            <>f__am$cache2 = new SceneEvent(SceneAwakeObserver.<ClearListeners>m__15E);
        Label_0018:
            mListeners = <>f__am$cache2;
            return;
        }

        public static void Invoke(GameObject scene)
        {
            Delegate[] delegateArray;
            int num;
            if (mListeners == null)
            {
                goto Label_0061;
            }
            delegateArray = mListeners.GetInvocationList();
            num = 0;
            goto Label_0058;
        Label_001C:
            if ((delegateArray[num].Target as Object) == null)
            {
                goto Label_0046;
            }
            if ((((Object) delegateArray[num].Target) != null) == null)
            {
                goto Label_0054;
            }
        Label_0046:
            ((SceneEvent) delegateArray[num])(scene);
        Label_0054:
            num += 1;
        Label_0058:
            if (num < ((int) delegateArray.Length))
            {
                goto Label_001C;
            }
        Label_0061:
            return;
        }

        public static void RemoveListener(SceneEvent listener)
        {
            mListeners = (SceneEvent) Delegate.Remove(mListeners, listener);
            return;
        }

        public delegate void SceneEvent(GameObject sceneRoot);
    }
}

