namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class SoundTrigger : MonoBehaviour
    {
        [FlexibleArray]
        public string[] VoiceNames;
        private MySound.Voice[] mVoices;

        public SoundTrigger()
        {
            this.VoiceNames = new string[0];
            base..ctor();
            return;
        }

        private void OnDestroy()
        {
            int num;
            num = 0;
            goto Label_0040;
        Label_0007:
            if (this.mVoices[num] == null)
            {
                goto Label_003C;
            }
            this.mVoices[num].StopAll(1f);
            this.mVoices[num].Cleanup();
            this.mVoices[num] = null;
        Label_003C:
            num += 1;
        Label_0040:
            if (num < ((int) this.mVoices.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void PlayJingle(string cueID)
        {
            MonoSingleton<MySound>.Instance.PlayJingle(cueID, 0f, null);
            return;
        }

        public void PlaySE(string cueID)
        {
            MonoSingleton<MySound>.Instance.PlaySEOneShot(cueID, 0f);
            return;
        }

        public void PlayVoice(string cueID)
        {
            int num;
            string str;
            string str2;
            int num2;
            num = cueID.IndexOf(0x2e);
            if (num > 0)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            str = cueID.Substring(0, num);
            str2 = cueID.Substring(num + 1);
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_0030;
            }
            return;
        Label_0030:
            num2 = 0;
            goto Label_0074;
        Label_0037:
            if ((this.VoiceNames[num2] == str) == null)
            {
                goto Label_0070;
            }
            if (this.mVoices[num2] == null)
            {
                goto Label_0070;
            }
            this.mVoices[num2].Play(str2, 0f, 0);
            goto Label_0082;
        Label_0070:
            num2 += 1;
        Label_0074:
            if (num2 < ((int) this.VoiceNames.Length))
            {
                goto Label_0037;
            }
        Label_0082:
            return;
        }

        private void Start()
        {
            int num;
            this.mVoices = new MySound.Voice[(int) this.VoiceNames.Length];
            num = 0;
            goto Label_0048;
        Label_001A:
            if (string.IsNullOrEmpty(this.VoiceNames[num]) != null)
            {
                goto Label_0044;
            }
            this.mVoices[num] = new MySound.Voice(this.VoiceNames[num], null, null, 0);
        Label_0044:
            num += 1;
        Label_0048:
            if (num < ((int) this.VoiceNames.Length))
            {
                goto Label_001A;
            }
            return;
        }
    }
}

