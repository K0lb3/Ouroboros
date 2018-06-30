namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Out", 1, 1), NodeType("System/Init", 0xffff), Pin(0, "In", 0, 0)]
    public class FlowNode_InitEnvironment : FlowNode
    {
        public FlowNode_InitEnvironment()
        {
            base..ctor();
            return;
        }

        private void Init()
        {
            GameManager manager;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_0030;
            }
            if (manager.IsRelogin == null)
            {
                goto Label_0028;
            }
            manager.ResetData();
            goto Label_0030;
        Label_0028:
            Object.DestroyImmediate(manager);
            manager = null;
        Label_0030:
            CriticalSection.ForceReset();
            ButtonEvent.Reset();
            SRPG_TouchInputModule.UnlockInput(1);
            PunMonoSingleton<MyPhoton>.Instance.Disconnect();
            UIUtility.PopCanvasAll();
            AssetManager.UnloadAll();
            AssetDownloader.Reset();
            Network.Reset();
            manager = MonoSingleton<GameManager>.Instance;
            GameUtility.ForceSetDefaultSleepSetting();
            if (GameUtility.IsStripBuild == null)
            {
                goto Label_007E;
            }
            GameUtility.Config_UseAssetBundles.Value = 1;
        Label_007E:
            LocalizedText.UnloadAllTables();
            SRPG_InputField.ResetInput();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0014;
            }
            this.Init();
            base.ActivateOutputLinks(1);
        Label_0014:
            return;
        }
    }
}

