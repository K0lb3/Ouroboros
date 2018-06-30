namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Cancel", 1, 0), NodeType("System/PrepareSceneChange"), Pin(0, "Done", 1, 0), Pin(100, "Start", 0, 0)]
    public class FlowNode_PrepareSceneChange : FlowNodePersistent
    {
        private bool mStart;

        public FlowNode_PrepareSceneChange()
        {
            base..ctor();
            return;
        }

        private void Cancel()
        {
            this.Reset();
            base.ActivateOutputLinks(1);
            return;
        }

        private void Done()
        {
            this.Reset();
            base.ActivateOutputLinks(0);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0025;
            }
            if (MonoSingleton<GameManager>.Instance.PrepareSceneChange() != null)
            {
                goto Label_001E;
            }
            this.Cancel();
            return;
        Label_001E:
            this.mStart = 1;
        Label_0025:
            return;
        }

        private void Reset()
        {
            this.mStart = 0;
            return;
        }

        private void Update()
        {
            if (this.mStart != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (MonoSingleton<GameManager>.Instance.IsImportantJobRunning == null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            this.Done();
            return;
        }
    }
}

