namespace SRPG
{
    using GR;
    using System;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitProfileWindow : MonoBehaviour
    {
        public string DebugUnitID;
        private MySound.Voice mUnitVoice;
        [FlexibleArray]
        public Text[] ProfileTexts;

        public UnitProfileWindow()
        {
            this.ProfileTexts = new Text[0];
            base..ctor();
            return;
        }

        private void OnDestroy()
        {
            if (this.mUnitVoice == null)
            {
                goto Label_0026;
            }
            this.mUnitVoice.StopAll(0f);
            this.mUnitVoice.Cleanup();
        Label_0026:
            this.mUnitVoice = null;
            return;
        }

        private void PlayProfileVoice()
        {
            if (this.mUnitVoice == null)
            {
                goto Label_0021;
            }
            this.mUnitVoice.Play("chara_0001", 0f, 0);
        Label_0021:
            return;
        }

        private void Start()
        {
            UnitData data;
            string str;
            string str2;
            string str3;
            int num;
            StringBuilder builder;
            data = null;
            if (string.IsNullOrEmpty(this.DebugUnitID) == null)
            {
                goto Label_0031;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            goto Label_0047;
        Label_0031:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.DebugUnitID);
        Label_0047:
            str = data.GetUnitSkinVoiceSheetName(-1);
            str2 = "VO_" + str;
            str3 = data.GetUnitSkinVoiceCueName(-1) + "_";
            this.mUnitVoice = new MySound.Voice(str2, str, str3, 0);
            this.PlayProfileVoice();
            DataSource.Bind<UnitData>(base.get_gameObject(), data);
            GameParameter.UpdateAll(base.get_gameObject());
            if (data == null)
            {
                goto Label_0151;
            }
            num = 0;
            goto Label_0142;
        Label_00A7:
            if ((this.ProfileTexts[num] == null) != null)
            {
                goto Label_013C;
            }
            if (string.IsNullOrEmpty(this.ProfileTexts[num].get_text()) == null)
            {
                goto Label_00D8;
            }
            goto Label_013C;
        Label_00D8:
            builder = GameUtility.GetStringBuilder();
            builder.Append("unit.");
            builder.Append(data.UnitParam.iname);
            builder.Append("_");
            builder.Append(this.ProfileTexts[num].get_text());
            this.ProfileTexts[num].set_text(LocalizedText.Get(builder.ToString()));
        Label_013C:
            num += 1;
        Label_0142:
            if (num < ((int) this.ProfileTexts.Length))
            {
                goto Label_00A7;
            }
        Label_0151:
            return;
        }
    }
}

