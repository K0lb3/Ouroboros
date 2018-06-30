namespace SRPG.AnimEvents
{
    using System;
    using UnityEngine;

    public class SoundSE : AnimEvent
    {
        public CustomSound.EType SoundType;
        public string CueID;

        public SoundSE()
        {
            base..ctor();
            return;
        }

        public override void OnEnd(GameObject go)
        {
            CustomSound sound;
            base.OnEnd(go);
            sound = go.GetComponent<CustomSound>();
            if ((sound != null) == null)
            {
                goto Label_0020;
            }
            Object.Destroy(sound);
        Label_0020:
            return;
        }

        public override void OnStart(GameObject go)
        {
            CustomSound sound;
            if ((go == null) != null)
            {
                goto Label_001C;
            }
            if (string.IsNullOrEmpty(this.CueID) == null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            sound = go.GetComponent<CustomSound>();
            if ((sound == null) == null)
            {
                goto Label_0037;
            }
            sound = go.AddComponent<CustomSound>();
        Label_0037:
            sound.type = this.SoundType;
            sound.cueID = this.CueID;
            sound.LoopFlag = 0;
            sound.StopSec = 0f;
            sound.PlayOnAwake = 0;
            sound.Play();
            return;
        }
    }
}

