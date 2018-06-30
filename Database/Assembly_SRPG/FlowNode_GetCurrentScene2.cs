namespace SRPG
{
    using System;

    [Pin(100, "Other", 1, 100), Pin(110, "Cancel", 1, 110), Pin(0x6a, "Unknown", 1, 0x6a), Pin(0x69, "Title", 1, 0x69), Pin(0x68, "Home", 1, 0x68), Pin(0x67, "HomeMulti", 1, 0x67), Pin(0x66, "Multi", 1, 0x66), Pin(0x65, "Single", 1, 0x65), Pin(0, "Input", 0, 0), NodeType("Scene/GetCurrentScene2", 0x7fe5)]
    public class FlowNode_GetCurrentScene2 : FlowNode
    {
        public string HomeString;
        public string TownString;

        public FlowNode_GetCurrentScene2()
        {
            this.HomeString = "Home";
            this.TownString = "town";
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameUtility.EScene scene;
            int num;
            if (pinID != null)
            {
                goto Label_010E;
            }
            if ((CanvasStack.Top != null) == null)
            {
                goto Label_0034;
            }
            if (CanvasStack.Top.GetComponent<CanvasStack>().SystemModal == null)
            {
                goto Label_0034;
            }
            base.ActivateOutputLinks(110);
            return;
        Label_0034:
            if (StreamingMovie.IsPlaying == null)
            {
                goto Label_0048;
            }
            base.ActivateOutputLinks(110);
            return;
        Label_0048:
            scene = GameUtility.GetCurrentScene();
            num = 0x6a;
            if (scene != 1)
            {
                goto Label_006A;
            }
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.BATTLE");
            num = 0x65;
            goto Label_0106;
        Label_006A:
            if (scene != 2)
            {
                goto Label_0083;
            }
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.BATTLE_MULTI");
            num = 0x66;
            goto Label_0106;
        Label_0083:
            if (scene != 4)
            {
                goto Label_009C;
            }
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.HOME_MULTI");
            num = 0x67;
            goto Label_0106;
        Label_009C:
            if (scene != 5)
            {
                goto Label_00B5;
            }
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.TITLE");
            num = 0x69;
            goto Label_0106;
        Label_00B5:
            if (scene != 3)
            {
                goto Label_00F9;
            }
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.NOT_HOME");
            num = 100;
            if (HomeWindow.Current == null)
            {
                goto Label_0106;
            }
            if (HomeWindow.Current.DesiredSceneIsHome == null)
            {
                goto Label_0106;
            }
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.NONE");
            num = 0x68;
            goto Label_0106;
        Label_00F9:
            DebugUtility.Log("FlowNode_GetCurrentScene2:EScene.UNKNOWN");
            num = 0x6a;
        Label_0106:
            base.ActivateOutputLinks(num);
        Label_010E:
            return;
        }
    }
}

