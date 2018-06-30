namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "CheckStart", 0, 0), Pin(100, "CheckFinished", 1, 100), NodeType("System/CheckSceneChangeUrlScheme", 0x7fe5), Pin(10, "MovieEndEvent", 1, 10)]
    public class FlowNode_CheckSceneChangeUrlScheme : FlowNodePersistent
    {
        private CheckFlag m_checkFlag;
        private bool startCheck;

        public FlowNode_CheckSceneChangeUrlScheme()
        {
            base..ctor();
            return;
        }

        private void Finished()
        {
            this.startCheck = 0;
            this.m_checkFlag = 0;
            base.ActivateOutputLinks(100);
            DebugUtility.Log("CheckSceneChangeUrlScheme => Finished");
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0014;
            }
            this.startCheck = 1;
            this.m_checkFlag = 0;
        Label_0014:
            return;
        }

        private void PushMovieEndEvent()
        {
            MonoSingleton<StreamingMovie>.Instance.IsNotReplay = 1;
            base.ActivateOutputLinks(10);
            DebugUtility.Log("CheckSceneChangeUrlScheme => Flag = StartEndMovie");
            this.m_checkFlag |= 1;
            DebugUtility.Log("CheckSceneChangeUrlScheme => PushMovieEndEvent");
            return;
        }

        private void Start()
        {
            base.set_enabled(1);
            return;
        }

        private void Update()
        {
            if (this.startCheck != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.m_checkFlag & 0x400) == null)
            {
                goto Label_0028;
            }
            this.Finished();
            goto Label_008D;
        Label_0028:
            if ((this.m_checkFlag & 2) == null)
            {
                goto Label_0052;
            }
            this.m_checkFlag |= 0x400;
            DebugUtility.Log("CheckSceneChangeUrlScheme => Flag = FinishedCheck");
            return;
        Label_0052:
            if (StreamingMovie.IsPlaying == null)
            {
                goto Label_0074;
            }
            if ((this.m_checkFlag & 1) != null)
            {
                goto Label_008D;
            }
            this.PushMovieEndEvent();
            goto Label_008D;
        Label_0074:
            this.m_checkFlag |= 2;
            DebugUtility.Log("CheckSceneChangeUrlScheme => Flag = FinishEndMovie");
            return;
        Label_008D:
            return;
        }

        private enum CheckFlag
        {
            StartEndMovie = 1,
            FinishEndMovie = 2,
            FinishedCheck = 0x400
        }
    }
}

