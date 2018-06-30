namespace SRPG.AnimEvents
{
    using System;
    using UnityEngine;

    public class SoundVoice1 : AnimEvent
    {
        public string SheetName;
        public string CueID;

        public SoundVoice1()
        {
            base..ctor();
            return;
        }

        public override void OnEnd(GameObject go)
        {
            CustomSound3 sound;
            base.OnEnd(go);
            sound = go.GetComponent<CustomSound3>();
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
            CustomSound3 sound;
            if ((go == null) != null)
            {
                goto Label_002C;
            }
            if (string.IsNullOrEmpty(this.SheetName) != null)
            {
                goto Label_002C;
            }
            if (string.IsNullOrEmpty(this.CueID) == null)
            {
                goto Label_002D;
            }
        Label_002C:
            return;
        Label_002D:
            sound = go.GetComponent<CustomSound3>();
            if ((sound == null) == null)
            {
                goto Label_0047;
            }
            sound = go.AddComponent<CustomSound3>();
        Label_0047:
            sound.sheetName = this.SheetName;
            sound.cueID = this.CueID;
            sound.PlayFunction = 3;
            sound.CueSheetHandlePlayCategory = 3;
            sound.CueSheetHandlePlayLoopType = 0;
            sound.StopOnPlay = 0;
            sound.StopOnDisable = 0;
            sound.StopSec = 0f;
            sound.DelayPlaySec = 0f;
            sound.PlayOnEnable = 1;
            sound.Play();
            return;
        }
    }
}

