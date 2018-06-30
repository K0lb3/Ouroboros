namespace SRPG.AnimEvents
{
    using System;
    using UnityEngine;

    public class SoundVoice2 : AnimEvent
    {
        public UnitVoice.ECharType CharType;
        public UnitVoice.eCollaboType CollaboType;
        public string DirectCharName;
        public string CueName;

        public SoundVoice2()
        {
            base..ctor();
            return;
        }

        public override void OnEnd(GameObject go)
        {
            UnitVoice voice;
            base.OnEnd(go);
            voice = go.GetComponent<UnitVoice>();
            if ((voice != null) == null)
            {
                goto Label_0020;
            }
            Object.Destroy(voice);
        Label_0020:
            return;
        }

        public override unsafe void OnStart(GameObject go)
        {
            UnitVoice voice;
            string str;
            string str2;
            string str3;
            if ((go == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            voice = go.GetComponent<UnitVoice>();
            if ((voice == null) == null)
            {
                goto Label_0027;
            }
            voice = go.AddComponent<UnitVoice>();
        Label_0027:
            voice.CharType = this.CharType;
            voice.CollaboType = this.CollaboType;
            voice.DirectCharName = this.DirectCharName;
            voice.CueName = this.CueName;
            voice.PlayOnAwake = 0;
            str = null;
            str2 = null;
            str3 = null;
            voice.GetDefaultCharName(&str, &str2, &str3);
            voice.SetCharName(str, str2, str3);
            voice.SetupCueName();
            voice.Play();
            return;
        }
    }
}

