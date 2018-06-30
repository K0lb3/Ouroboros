namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayBackUnitVoiceItem : MonoBehaviour
    {
        [SerializeField]
        private Text VoiceName;
        [SerializeField]
        private Toggle PlayingBadge;
        [SerializeField]
        private GameObject LockIcon;
        private UnitData.UnitVoiceCueInfo mUnitVoiceCueInfo;
        private string m_CueName;
        private string m_ButtonName;
        private bool m_IsLocked;
        public TapEvent OnTabEvent;

        public PlayBackUnitVoiceItem()
        {
            this.m_CueName = string.Empty;
            this.m_ButtonName = string.Empty;
            base..ctor();
            return;
        }

        public string GetUnlockConditionsText()
        {
            return this.mUnitVoiceCueInfo.unlock_conditions_text;
        }

        public void Lock()
        {
            SRPG_Button button;
            this.m_IsLocked = 1;
            button = base.GetComponentInChildren<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0021;
            }
            button.set_interactable(0);
        Label_0021:
            if ((this.LockIcon != null) == null)
            {
                goto Label_003E;
            }
            this.LockIcon.SetActive(1);
        Label_003E:
            return;
        }

        public void Refresh()
        {
            if (this.m_ButtonName == null)
            {
                goto Label_001C;
            }
            this.VoiceName.set_text(this.m_ButtonName);
        Label_001C:
            return;
        }

        public void SetPlayingBadge(bool value)
        {
            if ((this.PlayingBadge == null) == null)
            {
                goto Label_001C;
            }
            DebugUtility.LogError("PlayingBadgeが指定されていません");
            return;
        Label_001C:
            this.PlayingBadge.set_isOn(value);
            return;
        }

        public unsafe void SetUp(UnitData.UnitVoiceCueInfo UnitVoiceCueInfo)
        {
            this.mUnitVoiceCueInfo = UnitVoiceCueInfo;
            this.m_CueName = &UnitVoiceCueInfo.cueInfo.name;
            this.m_ButtonName = UnitVoiceCueInfo.voice_name;
            return;
        }

        private void Start()
        {
        }

        public void Unlock()
        {
            SRPG_Button button;
            this.m_IsLocked = 0;
            button = base.GetComponentInChildren<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0021;
            }
            button.set_interactable(1);
        Label_0021:
            if ((this.LockIcon != null) == null)
            {
                goto Label_003E;
            }
            this.LockIcon.SetActive(0);
        Label_003E:
            return;
        }

        public string CueName
        {
            get
            {
                return this.m_CueName;
            }
        }

        public bool IsLocked
        {
            get
            {
                return this.m_IsLocked;
            }
        }

        public delegate void TapEvent();
    }
}

