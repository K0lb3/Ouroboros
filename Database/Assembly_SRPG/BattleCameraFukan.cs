namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class BattleCameraFukan : MonoBehaviour
    {
        public SRPG.BattleCameraControl BattleCameraControl;
        public GameObject DefaultButton;
        public GameObject UpViewButton;
        private bool m_Disp;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$map6;

        public BattleCameraFukan()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__58()
        {
            this.OnClick(0);
            return;
        }

        [CompilerGenerated]
        private void <Start>m__59()
        {
            this.OnClick(1);
            return;
        }

        private void OnClick(ButtonType buttonType)
        {
            SceneBattle battle;
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0032;
            }
            if (buttonType != null)
            {
                goto Label_0024;
            }
            this.SetCameraMode(0);
            goto Label_0032;
        Label_0024:
            if (buttonType != 1)
            {
                goto Label_0032;
            }
            this.SetCameraMode(1);
        Label_0032:
            return;
        }

        public unsafe void OnEventCall(string key, string value)
        {
            string str;
            Dictionary<string, int> dictionary;
            int num;
            str = key;
            if (str == null)
            {
                goto Label_0070;
            }
            if (<>f__switch$map6 != null)
            {
                goto Label_002B;
            }
            dictionary = new Dictionary<string, int>(1);
            dictionary.Add("DISP", 0);
            <>f__switch$map6 = dictionary;
        Label_002B:
            if (<>f__switch$map6.TryGetValue(str, &num) == null)
            {
                goto Label_0070;
            }
            if (num == null)
            {
                goto Label_0048;
            }
            goto Label_0070;
        Label_0048:
            if ((value == "on") == null)
            {
                goto Label_0064;
            }
            this.SetDisp(1);
            goto Label_006B;
        Label_0064:
            this.SetDisp(0);
        Label_006B:;
        Label_0070:
            return;
        }

        private Button SetButtonEvent(GameObject go, ClickEvent callback)
        {
            Button button;
            button = go.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_002A;
            }
            button.get_onClick().AddListener(new UnityAction(callback, this.Invoke));
        Label_002A:
            return button;
        }

        public void SetCameraMode(SceneBattle.CameraMode mode)
        {
            SceneBattle battle;
            battle = SceneBattle.Instance;
            if ((battle != null) == null)
            {
                goto Label_00E6;
            }
            if (battle.IsControlBattleUI(4) != null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            battle.OnCameraModeChange(mode);
            if (mode != null)
            {
                goto Label_0088;
            }
            if ((this.DefaultButton != null) == null)
            {
                goto Label_0049;
            }
            this.DefaultButton.SetActive(0);
        Label_0049:
            if ((this.UpViewButton != null) == null)
            {
                goto Label_0066;
            }
            this.UpViewButton.SetActive(1);
        Label_0066:
            if ((this.BattleCameraControl != null) == null)
            {
                goto Label_00E6;
            }
            this.BattleCameraControl.SetDisp(1);
            goto Label_00E6;
        Label_0088:
            if (mode != 1)
            {
                goto Label_00E6;
            }
            if ((this.DefaultButton != null) == null)
            {
                goto Label_00AC;
            }
            this.DefaultButton.SetActive(1);
        Label_00AC:
            if ((this.UpViewButton != null) == null)
            {
                goto Label_00C9;
            }
            this.UpViewButton.SetActive(0);
        Label_00C9:
            if ((this.BattleCameraControl != null) == null)
            {
                goto Label_00E6;
            }
            this.BattleCameraControl.SetDisp(0);
        Label_00E6:
            return;
        }

        public void SetDisp(bool value)
        {
            Animator animator;
            SceneBattle battle;
            animator = base.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_001F;
            }
            animator.SetBool("open", value);
        Label_001F:
            if (value == null)
            {
                goto Label_00FF;
            }
            battle = SceneBattle.Instance;
            if ((battle != null) == null)
            {
                goto Label_00C0;
            }
            if (battle.isUpView == null)
            {
                goto Label_0081;
            }
            if ((this.DefaultButton != null) == null)
            {
                goto Label_005F;
            }
            this.DefaultButton.SetActive(1);
        Label_005F:
            if ((this.UpViewButton != null) == null)
            {
                goto Label_0139;
            }
            this.UpViewButton.SetActive(0);
            goto Label_00BB;
        Label_0081:
            if ((this.DefaultButton != null) == null)
            {
                goto Label_009E;
            }
            this.DefaultButton.SetActive(0);
        Label_009E:
            if ((this.UpViewButton != null) == null)
            {
                goto Label_0139;
            }
            this.UpViewButton.SetActive(1);
        Label_00BB:
            goto Label_00FA;
        Label_00C0:
            if ((this.DefaultButton != null) == null)
            {
                goto Label_00DD;
            }
            this.DefaultButton.SetActive(1);
        Label_00DD:
            if ((this.UpViewButton != null) == null)
            {
                goto Label_0139;
            }
            this.UpViewButton.SetActive(1);
        Label_00FA:
            goto Label_0139;
        Label_00FF:
            if ((this.DefaultButton != null) == null)
            {
                goto Label_011C;
            }
            this.DefaultButton.SetActive(0);
        Label_011C:
            if ((this.UpViewButton != null) == null)
            {
                goto Label_0139;
            }
            this.UpViewButton.SetActive(0);
        Label_0139:
            this.m_Disp = value;
            return;
        }

        private void Start()
        {
            if ((this.DefaultButton != null) == null)
            {
                goto Label_002A;
            }
            this.SetButtonEvent(this.DefaultButton, new ClickEvent(this.<Start>m__58));
        Label_002A:
            if ((this.UpViewButton != null) == null)
            {
                goto Label_0054;
            }
            this.SetButtonEvent(this.UpViewButton, new ClickEvent(this.<Start>m__59));
        Label_0054:
            this.SetDisp(0);
            return;
        }

        private void Update()
        {
        }

        public bool isDisp
        {
            get
            {
                return this.m_Disp;
            }
        }

        private enum ButtonType
        {
            DEFAULT,
            UPVIEW
        }

        private delegate void ClickEvent();
    }
}

