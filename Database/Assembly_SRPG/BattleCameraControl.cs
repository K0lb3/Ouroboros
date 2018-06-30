namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class BattleCameraControl : MonoBehaviour
    {
        public Button RotateLeft;
        public Button RotateRight;
        public Slider RotationSlider;
        public Scrollbar RotationScroll;
        public float RotateAmount;
        public float RotateTime;
        private Animator m_Animator;
        private Canvas m_Canvas;
        private GraphicRaycaster m_GraphicRaycatser;
        private bool m_Disp;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$map5;

        public BattleCameraControl()
        {
            this.RotateAmount = 0.25f;
            this.RotateTime = 0.2f;
            base..ctor();
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
                goto Label_00B3;
            }
            if (<>f__switch$map5 != null)
            {
                goto Label_0037;
            }
            dictionary = new Dictionary<string, int>(2);
            dictionary.Add("DISP", 0);
            dictionary.Add("FULLROTATION", 1);
            <>f__switch$map5 = dictionary;
        Label_0037:
            if (<>f__switch$map5.TryGetValue(str, &num) == null)
            {
                goto Label_00B3;
            }
            if (num == null)
            {
                goto Label_005B;
            }
            if (num == 1)
            {
                goto Label_0083;
            }
            goto Label_00B3;
        Label_005B:
            if ((value == "on") == null)
            {
                goto Label_0077;
            }
            this.SetDisp(1);
            goto Label_007E;
        Label_0077:
            this.SetDisp(0);
        Label_007E:
            goto Label_00B3;
        Label_0083:
            if ((value == "on") == null)
            {
                goto Label_00A3;
            }
            SceneBattle.Instance.SetFullRotationCamera(1);
            goto Label_00AE;
        Label_00A3:
            SceneBattle.Instance.SetFullRotationCamera(0);
        Label_00AE:;
        Label_00B3:
            return;
        }

        private void OnRotateLeft()
        {
            SceneBattle.Instance.RotateCamera(-this.RotateAmount, this.RotateTime);
            return;
        }

        private void OnRotateRight()
        {
            SceneBattle.Instance.RotateCamera(this.RotateAmount, this.RotateTime);
            return;
        }

        private void OnRotationValueChange(float value)
        {
        }

        public void SetDisp(bool value)
        {
            Animator animator;
            if (value == null)
            {
                goto Label_0018;
            }
            if (SceneBattle.Instance.isUpView == null)
            {
                goto Label_0018;
            }
            value = 0;
        Label_0018:
            animator = base.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0037;
            }
            animator.SetBool("open", value);
        Label_0037:
            return;
        }

        private void Start()
        {
            if ((this.RotateLeft != null) == null)
            {
                goto Label_002D;
            }
            this.RotateLeft.get_onClick().AddListener(new UnityAction(this, this.OnRotateLeft));
        Label_002D:
            if ((this.RotateRight != null) == null)
            {
                goto Label_005A;
            }
            this.RotateRight.get_onClick().AddListener(new UnityAction(this, this.OnRotateRight));
        Label_005A:
            if ((this.RotationSlider != null) == null)
            {
                goto Label_0087;
            }
            this.RotationSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnRotationValueChange));
        Label_0087:
            this.m_Animator = base.GetComponent<Animator>();
            this.m_Canvas = base.GetComponent<Canvas>();
            this.m_GraphicRaycatser = base.GetComponent<GraphicRaycaster>();
            this.SetDisp(0);
            return;
        }

        private unsafe void Update()
        {
            SceneBattle battle;
            bool flag;
            AnimatorStateInfo info;
            battle = SceneBattle.Instance;
            if ((battle == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if ((this.RotateLeft != null) == null)
            {
                goto Label_0035;
            }
            this.RotateLeft.set_interactable(battle.isCameraLeftMove);
        Label_0035:
            if ((this.RotateRight != null) == null)
            {
                goto Label_0057;
            }
            this.RotateRight.set_interactable(battle.isCameraRightMove);
        Label_0057:
            if ((this.m_Animator != null) == null)
            {
                goto Label_0127;
            }
            flag = this.m_Animator.GetBool("open");
            info = this.m_Animator.GetCurrentAnimatorStateInfo(0);
            if (flag == null)
            {
                goto Label_00DC;
            }
            if ((this.m_Canvas != null) == null)
            {
                goto Label_00A9;
            }
            this.m_Canvas.set_enabled(1);
        Label_00A9:
            if (&info.get_normalizedTime() < 1f)
            {
                goto Label_0127;
            }
            if ((this.m_GraphicRaycatser != null) == null)
            {
                goto Label_0127;
            }
            this.m_GraphicRaycatser.set_enabled(1);
            goto Label_0127;
        Label_00DC:
            if (&info.get_normalizedTime() < 1f)
            {
                goto Label_010A;
            }
            if ((this.m_Canvas != null) == null)
            {
                goto Label_010A;
            }
            this.m_Canvas.set_enabled(0);
        Label_010A:
            if ((this.m_GraphicRaycatser != null) == null)
            {
                goto Label_0127;
            }
            this.m_GraphicRaycatser.set_enabled(0);
        Label_0127:
            return;
        }
    }
}

