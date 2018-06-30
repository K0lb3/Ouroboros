namespace SRPG
{
    using System;

    [NodeType("System/DestroyAsset", 0x7fe5), Pin(0, "Start", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_DestroyAsset : FlowNode
    {
        public AssetBundleFlags flags;

        public FlowNode_DestroyAsset()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0011;
            }
            AssetDownloader.DestroyAssetStart(this.flags);
        Label_0011:
            base.ActivateOutputLinks(1);
            return;
        }

        private void Start()
        {
        }
    }
}

