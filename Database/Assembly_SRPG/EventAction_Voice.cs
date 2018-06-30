namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/ボイス再生", "ボイスを再生します。", 0x445555, 0x448888)]
    internal class EventAction_Voice : EventAction
    {
        public string m_VoiceName;
        [StringIsActorID]
        public bool m_Async;
        [HideInInspector]
        public float m_Delay;
        private MySound.Voice m_Voice;
        private bool m_Play;

        public EventAction_Voice()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if (this.m_Async == null)
            {
                goto Label_0033;
            }
            base.ActivateNext(1);
            if (this.m_Delay > 0f)
            {
                goto Label_003F;
            }
            this.m_Play = this.PlayVoice();
            goto Label_003F;
        Label_0033:
            this.m_Play = this.PlayVoice();
        Label_003F:
            return;
        }

        private bool PlayVoice()
        {
            char[] chArray1;
            bool flag;
            string[] strArray;
            string str;
            string str2;
            flag = 0;
            if (string.IsNullOrEmpty(this.m_VoiceName) != null)
            {
                goto Label_005D;
            }
            chArray1 = new char[] { 0x2e };
            strArray = this.m_VoiceName.Split(chArray1);
            if (((int) strArray.Length) != 2)
            {
                goto Label_005D;
            }
            str = strArray[0];
            str2 = strArray[1];
            this.m_Voice = new MySound.Voice(str, null, null, 0);
            this.m_Voice.Play(str2, 0f, 0);
            flag = 1;
        Label_005D:
            return flag;
        }

        public override void Update()
        {
            bool flag;
            if (this.m_Play == null)
            {
                goto Label_0048;
            }
            if (this.m_Voice == null)
            {
                goto Label_0048;
            }
            if (this.m_Voice.IsPlaying != null)
            {
                goto Label_0090;
            }
            if (this.m_Async != null)
            {
                goto Label_003C;
            }
            base.ActivateNext();
            goto Label_0043;
        Label_003C:
            base.enabled = 0;
        Label_0043:
            goto Label_0090;
        Label_0048:
            if (this.m_Async == null)
            {
                goto Label_0090;
            }
            this.m_Delay -= Time.get_deltaTime();
            if (this.m_Delay >= 0f)
            {
                goto Label_0090;
            }
            if ((this.m_Play = this.PlayVoice()) != null)
            {
                goto Label_0090;
            }
            base.enabled = 0;
        Label_0090:
            return;
        }
    }
}

