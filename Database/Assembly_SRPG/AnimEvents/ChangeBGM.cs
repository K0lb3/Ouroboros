namespace SRPG.AnimEvents
{
    using GR;
    using SRPG;
    using System;
    using UnityEngine;

    public class ChangeBGM : AnimEvent
    {
        public string BgmId;

        public ChangeBGM()
        {
            this.BgmId = string.Empty;
            base..ctor();
            return;
        }

        public override void OnStart(GameObject go)
        {
            if (string.IsNullOrEmpty(this.BgmId) == null)
            {
                goto Label_001F;
            }
            SceneBattle.Instance.PlayBGM();
            goto Label_0031;
        Label_001F:
            MonoSingleton<MySound>.Instance.PlayBGM(this.BgmId, null, 0);
        Label_0031:
            return;
        }
    }
}

