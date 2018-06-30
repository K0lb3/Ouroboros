namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(11, "ToggleChargeDisp", 0, 1)]
    public class OptionDetail : MonoBehaviour, IFlowInterface
    {
        public Slider SoundVolume;
        public Slider MusicVolume;
        public Slider VoiceVolume;
        public LText ChargeDispText;
        [CompilerGenerated]
        private static UnityAction<float> <>f__am$cache4;
        [CompilerGenerated]
        private static UnityAction<float> <>f__am$cache5;
        [CompilerGenerated]
        private static UnityAction<float> <>f__am$cache6;

        public OptionDetail()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__376(float value)
        {
            GameUtility.Config_SoundVolume = value;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__377(float value)
        {
            GameUtility.Config_MusicVolume = value;
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__378(float value)
        {
            GameUtility.Config_VoiceVolume = value;
            return;
        }

        public void Activated(int pinID)
        {
            bool flag;
            SceneBattle battle;
            if (pinID != 11)
            {
                goto Label_0040;
            }
            flag = GameUtility.Config_ChargeDisp.Value == 0;
            this.UpdateChargeDispText(flag);
            GameUtility.Config_ChargeDisp.Value = flag;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_0040;
            }
            battle.ReflectCastSkill(flag);
        Label_0040:
            return;
        }

        private void Start()
        {
            if ((this.SoundVolume != null) == null)
            {
                goto Label_004E;
            }
            this.SoundVolume.set_value(GameUtility.Config_SoundVolume);
            if (<>f__am$cache4 != null)
            {
                goto Label_0044;
            }
            <>f__am$cache4 = new UnityAction<float>(null, <Start>m__376);
        Label_0044:
            this.SoundVolume.get_onValueChanged().AddListener(<>f__am$cache4);
        Label_004E:
            if ((this.MusicVolume != null) == null)
            {
                goto Label_009C;
            }
            this.MusicVolume.set_value(GameUtility.Config_MusicVolume);
            if (<>f__am$cache5 != null)
            {
                goto Label_0092;
            }
            <>f__am$cache5 = new UnityAction<float>(null, <Start>m__377);
        Label_0092:
            this.MusicVolume.get_onValueChanged().AddListener(<>f__am$cache5);
        Label_009C:
            if ((this.VoiceVolume != null) == null)
            {
                goto Label_00EA;
            }
            this.VoiceVolume.set_value(GameUtility.Config_VoiceVolume);
            if (<>f__am$cache6 != null)
            {
                goto Label_00E0;
            }
            <>f__am$cache6 = new UnityAction<float>(null, <Start>m__378);
        Label_00E0:
            this.VoiceVolume.get_onValueChanged().AddListener(<>f__am$cache6);
        Label_00EA:
            this.UpdateChargeDispText(GameUtility.Config_ChargeDisp.Value);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void UpdateChargeDispText(bool is_disp)
        {
            if (this.ChargeDispText == null)
            {
                goto Label_0045;
            }
            if (is_disp == null)
            {
                goto Label_0030;
            }
            this.ChargeDispText.set_text(LocalizedText.Get("sys.BTN_CHARGE_DISP_ON"));
            goto Label_0045;
        Label_0030:
            this.ChargeDispText.set_text(LocalizedText.Get("sys.BTN_CHARGE_DISP_OFF"));
        Label_0045:
            return;
        }
    }
}

