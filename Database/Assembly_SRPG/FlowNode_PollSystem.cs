namespace SRPG
{
    using System;
    using UnityEngine.EventSystems;

    [Pin(0x44c, "DisableInputModule", 0, 0), Pin(0x1f40, "CriticalSection", 0, 0), Pin(0x5dc, "BlockInterruptUrlSchemeLaunch", 0, 0), Pin(0x578, "BlockInterruptPhotonDisconnected", 0, 0), Pin(0x514, "BlockInterruptAll", 0, 0), Pin(0x500, "BeforeLogin", 0, 0), Pin(0x4e2, "NetworkConnecting", 0, 0), Pin(0x4b0, "FadeVisible", 0, 0), Pin(11, "すべてパスするまで待つ", 0, 0), Pin(10, "どれかに引っかかるまで待つ", 0, 0), Pin(1, "すべてパスした", 1, 0), Pin(0, "どれかに引っかかった", 1, 0), NodeType("System/PollSystem")]
    public class FlowNode_PollSystem : FlowNodePersistent
    {
        private bool[] mCheckFlag;
        private bool[] mCheckType;
        private bool mStart;

        public FlowNode_PollSystem()
        {
            this.mCheckFlag = new bool[8];
            this.mCheckType = new bool[3];
            base..ctor();
            return;
        }

        private void AllPass()
        {
            if (this.mCheckType[2] != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            this.Reset();
            base.ActivateOutputLinks(1);
            return;
        }

        private void AnyOn()
        {
            if (this.mCheckType[1] != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            this.Reset();
            base.ActivateOutputLinks(0);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_001D;
            }
            this.mStart = 1;
            this.mCheckType[1] = 1;
            goto Label_00FD;
        Label_001D:
            if (pinID != 11)
            {
                goto Label_003A;
            }
            this.mStart = 1;
            this.mCheckType[2] = 1;
            goto Label_00FD;
        Label_003A:
            if (pinID != 0x44c)
            {
                goto Label_0053;
            }
            this.mCheckFlag[0] = 1;
            goto Label_00FD;
        Label_0053:
            if (pinID != 0x4b0)
            {
                goto Label_006C;
            }
            this.mCheckFlag[1] = 1;
            goto Label_00FD;
        Label_006C:
            if (pinID != 0x4e2)
            {
                goto Label_0085;
            }
            this.mCheckFlag[2] = 1;
            goto Label_00FD;
        Label_0085:
            if (pinID != 0x500)
            {
                goto Label_009E;
            }
            this.mCheckFlag[3] = 1;
            goto Label_00FD;
        Label_009E:
            if (pinID != 0x514)
            {
                goto Label_00B7;
            }
            this.mCheckFlag[4] = 1;
            goto Label_00FD;
        Label_00B7:
            if (pinID != 0x578)
            {
                goto Label_00D0;
            }
            this.mCheckFlag[5] = 1;
            goto Label_00FD;
        Label_00D0:
            if (pinID != 0x5dc)
            {
                goto Label_00E9;
            }
            this.mCheckFlag[6] = 1;
            goto Label_00FD;
        Label_00E9:
            if (pinID != 0x1f40)
            {
                goto Label_00FD;
            }
            this.mCheckFlag[7] = 1;
        Label_00FD:
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
            if (this.mCheckFlag[0] == null)
            {
                goto Label_0034;
            }
            if (EventSystem.get_current().get_currentInputModule().get_enabled() != null)
            {
                goto Label_0034;
            }
            this.AnyOn();
            return;
        Label_0034:
            if (this.mCheckFlag[1] == null)
            {
                goto Label_0062;
            }
            if (FadeController.InstanceExists == null)
            {
                goto Label_0062;
            }
            if (FadeController.Instance.IsFading(0) == null)
            {
                goto Label_0062;
            }
            this.AnyOn();
            return;
        Label_0062:
            if (this.mCheckFlag[2] == null)
            {
                goto Label_0080;
            }
            if (Network.IsConnecting == null)
            {
                goto Label_0080;
            }
            this.AnyOn();
            return;
        Label_0080:
            if (this.mCheckFlag[3] == null)
            {
                goto Label_009E;
            }
            if (FlowNode_GetCurrentScene.IsAfterLogin() != null)
            {
                goto Label_009E;
            }
            this.AnyOn();
            return;
        Label_009E:
            if (this.mCheckFlag[4] == null)
            {
                goto Label_00BD;
            }
            if (BlockInterrupt.IsBlocked(1) == null)
            {
                goto Label_00BD;
            }
            this.AnyOn();
            return;
        Label_00BD:
            if (this.mCheckFlag[5] == null)
            {
                goto Label_00DC;
            }
            if (BlockInterrupt.IsBlocked(2) == null)
            {
                goto Label_00DC;
            }
            this.AnyOn();
            return;
        Label_00DC:
            if (this.mCheckFlag[6] == null)
            {
                goto Label_00FB;
            }
            if (BlockInterrupt.IsBlocked(3) == null)
            {
                goto Label_00FB;
            }
            this.AnyOn();
            return;
        Label_00FB:
            if (this.mCheckFlag[7] == null)
            {
                goto Label_0119;
            }
            if (CriticalSection.IsActive == null)
            {
                goto Label_0119;
            }
            this.AnyOn();
            return;
        Label_0119:
            this.AllPass();
            return;
        }

        private enum EState
        {
            NOP,
            CHECK,
            PASS,
            NUM
        }

        private enum EType
        {
            DISABLE_INPUT_MODULE,
            FADE_VISIBLE,
            NETWORK_CONNECTING,
            BEFORE_LOGIN,
            INTERRUPT_STOPPER_ALL,
            INTERRUPT_STOPPER_PHOTON_DISCONNECTED,
            INTERRUPT_STOPPER_URL_SCHEME_LAUNCH,
            CRITIAL_SECTION,
            NUM
        }
    }
}

