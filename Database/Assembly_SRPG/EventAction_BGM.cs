namespace SRPG
{
    using GR;
    using System;

    [EventActionInfo("BGM再生", "BGMを再生します", 0x707055, 0x888844)]
    public class EventAction_BGM : EventAction
    {
        public static readonly int DEMO_BGM_ST;
        public static readonly int DEMO_BGM_ED;
        public string BGM;

        static EventAction_BGM()
        {
            DEMO_BGM_ST = 0x22;
            DEMO_BGM_ED = 0x63;
            return;
        }

        public EventAction_BGM()
        {
            base..ctor();
            return;
        }

        public override string[] GetUnManagedAssetListData()
        {
            string[] textArray1;
            string[] strArray;
            if (string.IsNullOrEmpty(this.BGM) != null)
            {
                goto Label_0038;
            }
            if (string.IsNullOrEmpty(this.BGM) != null)
            {
                goto Label_0038;
            }
            textArray1 = new string[] { this.BGM };
            strArray = textArray1;
            return EventAction.GetUnManagedStreamAssets(strArray, 1);
        Label_0038:
            return null;
        }

        public override void OnActivate()
        {
            EventScript.ScriptSequence sequence;
            if (string.IsNullOrEmpty(this.BGM) == null)
            {
                goto Label_0039;
            }
            MonoSingleton<MySound>.Instance.StopBGM();
            if (SceneBattle.Instance == null)
            {
                goto Label_00A8;
            }
            SceneBattle.Instance.EventPlayBgmID = null;
            goto Label_00A8;
        Label_0039:
            MonoSingleton<MySound>.Instance.PlayBGM(this.BGM, null, EventAction.IsUnManagedAssets(this.BGM, 1));
            if (SceneBattle.Instance == null)
            {
                goto Label_00A8;
            }
            sequence = (base.Sequence == null) ? null : base.Sequence.ParentSequence;
            if (sequence == null)
            {
                goto Label_00A8;
            }
            if (sequence.IsSavePlayBgmID == null)
            {
                goto Label_00A8;
            }
            SceneBattle.Instance.EventPlayBgmID = this.BGM;
        Label_00A8:
            base.ActivateNext();
            return;
        }
    }
}

