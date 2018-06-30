namespace SRPG
{
    using System;

    [NodeType("GetCurrentScene", 0x7fe5), Pin(100, "Other", 1, 0), Pin(0x65, "Single", 1, 0), Pin(0x66, "Multi", 1, 0), Pin(0x67, "HomeMulti", 1, 0), Pin(0x68, "Home", 1, 0), Pin(0x69, "Title", 1, 0), Pin(0, "Test", 0, 0)]
    public class FlowNode_GetCurrentScene : FlowNode
    {
        public FlowNode_GetCurrentScene()
        {
            base..ctor();
            return;
        }

        public static bool IsAfterLogin()
        {
            GameUtility.EScene scene;
            bool flag;
            GameUtility.EScene scene2;
            scene = GameUtility.GetCurrentScene();
            flag = 0;
            scene2 = scene;
            switch ((scene2 - 1))
            {
                case 0:
                    goto Label_0027;

                case 1:
                    goto Label_0027;

                case 2:
                    goto Label_0027;

                case 3:
                    goto Label_0027;
            }
            goto Label_002E;
        Label_0027:
            flag = 1;
            goto Label_0035;
        Label_002E:
            flag = 0;
        Label_0035:
            return flag;
        }

        public override void OnActivate(int pinID)
        {
            GameUtility.EScene scene;
            if (pinID != null)
            {
                goto Label_007E;
            }
            scene = GameUtility.GetCurrentScene();
            if (scene != 1)
            {
                goto Label_0021;
            }
            base.ActivateOutputLinks(0x65);
            goto Label_007E;
        Label_0021:
            if (scene != 2)
            {
                goto Label_0036;
            }
            base.ActivateOutputLinks(0x66);
            goto Label_007E;
        Label_0036:
            if (scene != 4)
            {
                goto Label_004B;
            }
            base.ActivateOutputLinks(0x67);
            goto Label_007E;
        Label_004B:
            if (scene != 3)
            {
                goto Label_0060;
            }
            base.ActivateOutputLinks(0x68);
            goto Label_007E;
        Label_0060:
            if (scene != 5)
            {
                goto Label_0075;
            }
            base.ActivateOutputLinks(0x69);
            goto Label_007E;
        Label_0075:
            base.ActivateOutputLinks(100);
        Label_007E:
            return;
        }
    }
}

