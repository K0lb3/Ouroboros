namespace SRPG
{
    using System;
    using UnityEngine;

    [ExecuteInEditMode, DisallowMultipleComponent]
    public class SceneRoot : MonoBehaviour
    {
        public SceneRoot()
        {
            base..ctor();
            return;
        }

        protected virtual void Awake()
        {
            SceneAwakeObserver.Invoke(base.get_gameObject());
            return;
        }
    }
}

