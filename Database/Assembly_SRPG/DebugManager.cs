namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [ExecuteInEditMode, AddComponentMenu("Scripts/SRPG/Manager/Debug")]
    public class DebugManager : MonoSingleton<DebugManager>
    {
        private float mLastCollectNum;
        private int mAllocMem;
        private int mAllocPeak;
        [CompilerGenerated]
        private bool <IsShowed>k__BackingField;
        [CompilerGenerated]
        private bool <IsShowedInEditor>k__BackingField;

        public DebugManager()
        {
            base..ctor();
            return;
        }

        protected override void Initialize()
        {
            this.IsShowed = 1;
            this.IsShowedInEditor = 0;
            Object.DontDestroyOnLoad(this);
            return;
        }

        public bool IsWebViewEnable()
        {
            return 1;
        }

        private void Update()
        {
            int num;
            if ((this.IsShowed != null) && ((Application.get_isPlaying() != null) || (this.IsShowedInEditor != null)))
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            num = GC.CollectionCount(0);
            if (this.mLastCollectNum == ((float) num))
            {
                goto Label_0035;
            }
        Label_0035:
            this.mAllocMem = Profiler.get_usedHeapSize();
            this.mAllocPeak = (this.mAllocMem <= this.mAllocPeak) ? this.mAllocPeak : this.mAllocMem;
            return;
        }

        public bool IsShowed
        {
            [CompilerGenerated]
            get
            {
                return this.<IsShowed>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsShowed>k__BackingField = value;
                return;
            }
        }

        public bool IsShowedInEditor
        {
            [CompilerGenerated]
            get
            {
                return this.<IsShowedInEditor>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsShowedInEditor>k__BackingField = value;
                return;
            }
        }
    }
}

