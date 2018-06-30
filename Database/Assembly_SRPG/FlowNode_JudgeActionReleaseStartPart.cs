namespace SRPG
{
    using GR;
    using System;

    [Pin(0x65, "解放演出を見ない", 1, 0x65), Pin(0, "イン", 0, 0), Pin(100, "解放演出を見せる", 1, 100), NodeType("SRPG/ストーリーパート解放演出を見せるか判断", 0x7fe5)]
    public class FlowNode_JudgeActionReleaseStartPart : FlowNode
    {
        public FlowNode_JudgeActionReleaseStartPart()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_002C;
            }
            if (MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart() == null)
            {
                goto Label_0023;
            }
            base.ActivateOutputLinks(100);
            goto Label_002C;
        Label_0023:
            base.ActivateOutputLinks(0x65);
        Label_002C:
            return;
        }
    }
}

