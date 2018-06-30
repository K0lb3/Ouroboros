namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public abstract class Scene : MonoBehaviour
    {
        [CompilerGenerated]
        private bool <IsLoaded>k__BackingField;

        protected Scene()
        {
            base..ctor();
            return;
        }

        protected void Awake()
        {
            MonoSingleton<SystemInstance>.Instance.Ensure();
            GameUtility.RemoveDuplicatedMainCamera();
            return;
        }

        protected bool IsLoaded
        {
            [CompilerGenerated]
            get
            {
                return this.<IsLoaded>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsLoaded>k__BackingField = value;
                return;
            }
        }
    }
}

