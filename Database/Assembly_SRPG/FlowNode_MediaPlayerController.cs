namespace SRPG
{
    using System;
    using UnityEngine.Events;

    [Pin(0x41a, "OnLoadFailed", 1, 0x41a), Pin(0x3e8, "OnReady", 1, 0x3e8), Pin(0x3f2, "OnFirstFrameReady", 1, 0x3f2), Pin(0x3fc, "OnBufferingStart", 1, 0x3fc), Pin(0x3fd, "OnBufferingEnd", 1, 0x3fd), Pin(0x3fe, "OnBufferingTimeout", 1, 0x3fe), Pin(0x406, "OnEnd", 1, 0x406), Pin(0x410, "OnError", 1, 0x410), NodeType("AVProVideo/MediaPlayerController"), Pin(10, "Play", 0, 10), Pin(20, "Pause", 0, 20), Pin(30, "Stop", 0, 30), Pin(40, "Skip", 0, 40), Pin(50, "Unload", 0, 50), Pin(60, "Reload", 0, 60)]
    public class FlowNode_MediaPlayerController : FlowNodePersistent
    {
        public const int PIN_ID_PLAY = 10;
        public const int PIN_ID_PAUSE = 20;
        public const int PIN_ID_STOP = 30;
        public const int PIN_ID_SKIP = 40;
        public const int PIN_ID_UNLOAD = 50;
        public const int PIN_ID_RELOAD = 60;
        public const int PIN_ID_ON_READY = 0x3e8;
        public const int PIN_ID_ON_FIRST_FRAME_READY = 0x3f2;
        public const int PIN_ID_ON_BUFFERING_START = 0x3fc;
        public const int PIN_ID_ON_BUFFERING_END = 0x3fd;
        public const int PIN_ID_ON_BUFFERING_TIMEOUT = 0x3fe;
        public const int PIN_ID_ON_END = 0x406;
        public const int PIN_ID_ON_ERROR = 0x410;
        public const int PIN_ID_ON_LOAD_FAILED = 0x41a;
        public MediaPlayerWrapper m_MediaPlayerWrapper;
        public bool m_EnableBufferingTimeout;
        public float m_BufferingGraceTime;

        public FlowNode_MediaPlayerController()
        {
            base..ctor();
            return;
        }

        public void Load(string url)
        {
            this.m_MediaPlayerWrapper.LoadFromURL(url, 0);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_0018;
            }
            this.m_MediaPlayerWrapper.Play();
            goto Label_008C;
        Label_0018:
            if (pinID != 20)
            {
                goto Label_0030;
            }
            this.m_MediaPlayerWrapper.Pause();
            goto Label_008C;
        Label_0030:
            if (pinID != 30)
            {
                goto Label_0048;
            }
            this.m_MediaPlayerWrapper.Stop();
            goto Label_008C;
        Label_0048:
            if (pinID != 40)
            {
                goto Label_0060;
            }
            this.m_MediaPlayerWrapper.SkipToEnd();
            goto Label_008C;
        Label_0060:
            if (pinID != 50)
            {
                goto Label_0078;
            }
            this.m_MediaPlayerWrapper.Unload();
            goto Label_008C;
        Label_0078:
            if (pinID != 60)
            {
                goto Label_008C;
            }
            this.m_MediaPlayerWrapper.Reload(0);
        Label_008C:
            return;
        }

        private void OnBufferingEnd()
        {
            base.ActivateOutputLinks(0x3fd);
            return;
        }

        private void OnBufferingStart()
        {
            base.ActivateOutputLinks(0x3fc);
            return;
        }

        private void OnBufferingTimeout()
        {
            base.ActivateOutputLinks(0x3fe);
            return;
        }

        private void OnError()
        {
            base.ActivateOutputLinks(0x410);
            return;
        }

        private void OnFinished()
        {
            base.ActivateOutputLinks(0x406);
            return;
        }

        private void OnFirstFrameReady()
        {
            base.ActivateOutputLinks(0x3f2);
            return;
        }

        private void OnLoadFailed()
        {
            base.ActivateOutputLinks(0x41a);
            return;
        }

        private void OnMediaPlayerEvent(MediaPlayerWrapper.Event.Type eventType)
        {
            if (eventType != 1)
            {
                goto Label_0012;
            }
            this.OnReady();
            goto Label_008F;
        Label_0012:
            if (eventType != 3)
            {
                goto Label_0024;
            }
            this.OnFirstFrameReady();
            goto Label_008F;
        Label_0024:
            if (eventType != 4)
            {
                goto Label_0036;
            }
            this.OnFinished();
            goto Label_008F;
        Label_0036:
            if (eventType != 6)
            {
                goto Label_0048;
            }
            this.OnError();
            goto Label_008F;
        Label_0048:
            if (eventType != 11)
            {
                goto Label_005B;
            }
            this.OnBufferingStart();
            goto Label_008F;
        Label_005B:
            if (eventType != 12)
            {
                goto Label_006E;
            }
            this.OnBufferingEnd();
            goto Label_008F;
        Label_006E:
            if (eventType != 13)
            {
                goto Label_0081;
            }
            this.OnBufferingTimeout();
            goto Label_008F;
        Label_0081:
            if (eventType != 10)
            {
                goto Label_008F;
            }
            this.OnLoadFailed();
        Label_008F:
            return;
        }

        private void OnReady()
        {
            base.ActivateOutputLinks(0x3e8);
            return;
        }

        public void SetVolume(float value)
        {
            this.m_MediaPlayerWrapper.SetVolume(value);
            return;
        }

        private void Start()
        {
            this.m_MediaPlayerWrapper.Events.RemoveListener(new UnityAction<MediaPlayerWrapper.Event.Type>(this, this.OnMediaPlayerEvent));
            this.m_MediaPlayerWrapper.Events.AddListener(new UnityAction<MediaPlayerWrapper.Event.Type>(this, this.OnMediaPlayerEvent));
            this.m_MediaPlayerWrapper.EnableBufferingTimeout = this.m_EnableBufferingTimeout;
            this.m_MediaPlayerWrapper.BufferingGraceTime = this.m_BufferingGraceTime;
            return;
        }
    }
}

