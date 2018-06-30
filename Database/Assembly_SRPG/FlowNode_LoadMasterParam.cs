namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using UnityEngine;

    [Pin(100, "Finished", 1, 100), Pin(0, "Start", 0, 0), NodeType("System/LoadMasterParam", 0xffffff)]
    public class FlowNode_LoadMasterParam : FlowNode
    {
        private Mutex mMutex;
        private int mResult;

        public FlowNode_LoadMasterParam()
        {
            base..ctor();
            return;
        }

        private static unsafe void LoadMasterDataThread(object param)
        {
            ThreadStartParam param2;
            int num;
            Exception exception;
            Debug.Log("LoadMasterDataThread");
            param2 = (ThreadStartParam) param;
            Debug.Log("LoadMasterDataThread START");
            num = -1;
        Label_001D:
            try
            {
                num = (&param2.GameManager.ReloadMasterData(null, null) == null) ? -1 : 1;
                goto Label_0049;
            }
            catch (Exception exception1)
            {
            Label_003D:
                exception = exception1;
                Debug.LogException(exception);
                goto Label_0049;
            }
        Label_0049:
            Debug.Log("LoadMasterDataThread END");
            if (&param2.self.mMutex == null)
            {
                goto Label_0094;
            }
            &param2.self.mMutex.WaitOne();
            &param2.self.mResult = num;
            &param2.self.mMutex.ReleaseMutex();
        Label_0094:
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            ThreadStartParam param;
            Thread thread;
            if (pinID != null)
            {
                goto Label_0098;
            }
            if (base.get_enabled() != null)
            {
                goto Label_0098;
            }
            this.mResult = 0;
            if (GameUtility.Config_UseAssetBundles.Value == null)
            {
                goto Label_0088;
            }
            base.set_enabled(1);
            CriticalSection.Enter(1);
            param = new ThreadStartParam();
            &param.self = this;
            &param.GameManager = MonoSingleton<GameManager>.Instance;
            Debug.Log("Starting Thread");
            this.mMutex = new Mutex();
            thread = new Thread(new ParameterizedThreadStart(FlowNode_LoadMasterParam.LoadMasterDataThread));
            thread.Start((ThreadStartParam) param);
            goto Label_0098;
        Label_0088:
            base.set_enabled(0);
            base.ActivateOutputLinks(100);
        Label_0098:
            return;
        }

        protected override void OnDestroy()
        {
            if (this.mMutex == null)
            {
                goto Label_0034;
            }
            this.mMutex.WaitOne();
            this.mMutex.ReleaseMutex();
            this.mMutex.Close();
            this.mMutex = null;
        Label_0034:
            base.OnDestroy();
            return;
        }

        private void Update()
        {
            bool flag;
            if (this.mMutex == null)
            {
                goto Label_0082;
            }
            this.mMutex.WaitOne();
            flag = (this.mResult == 0) == 0;
            this.mMutex.ReleaseMutex();
            if (flag == null)
            {
                goto Label_0082;
            }
            this.mMutex.Close();
            this.mMutex = null;
            if (this.mResult >= 0)
            {
                goto Label_005D;
            }
            DebugUtility.LogError("Failed to load MasterParam");
        Label_005D:
            base.set_enabled(0);
            CriticalSection.Leave(1);
            MonoSingleton<GameManager>.Instance.MasterParam.DumpLoadedLog();
            base.ActivateOutputLinks(100);
        Label_0082:
            return;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ThreadStartParam
        {
            public FlowNode_LoadMasterParam self;
            public SRPG.GameManager GameManager;
        }
    }
}

