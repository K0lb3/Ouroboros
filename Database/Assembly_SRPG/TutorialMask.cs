namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class TutorialMask : MonoBehaviour
    {
        private const string DESTROY_MASK_EVENT_NAME = "CLOSE_TUTORIAL_MASK";
        [SerializeField]
        private GameObject mMask;
        [SerializeField]
        private Button mEnableArea;
        [SerializeField]
        private Button[] mDisableAreas;
        [SerializeField]
        private GameObject mArrow;
        [SerializeField]
        private GameObject mTextRoot;
        [SerializeField]
        private Text mText;
        private bool mIsFinishSetup;
        private RectTransform mMaskRectTransform;
        private Vector3 mTargetWorldPos;
        private Vector3 mTargetScreenPos;
        private eActionType mActionType;
        private bool mIsWorld2Screen;
        private float mNoResponseTime;
        private Vector2 mMaskSize;
        private Animator mAnimator;
        public OpendMethod mOpendMethod;

        public TutorialMask()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.mMask != null) == null)
            {
                goto Label_0033;
            }
            this.mMaskRectTransform = this.mMask.get_transform() as RectTransform;
            this.mMask.SetActive(0);
        Label_0033:
            this.mAnimator = base.GetComponent<Animator>();
            return;
        }

        private void DestroyMask()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "CLOSE_TUTORIAL_MASK");
            return;
        }

        private void ExecNormalAtk()
        {
            Button button;
            SceneBattle.Instance.BattleUI.CommandWindow.OKButton.GetComponentInChildren<Button>().get_onClick().Invoke();
            this.DestroyMask();
            return;
        }

        private void MoveUnit()
        {
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_003A;
            }
            if (SceneBattle.Instance.VirtualStickMoveInput == null)
            {
                goto Label_003A;
            }
            SceneBattle.Instance.VirtualStickMoveInput.MoveUnit(this.mTargetScreenPos);
            this.DestroyMask();
        Label_003A:
            return;
        }

        private void OnClick_DisableArea()
        {
            eActionType type;
            if (this.mNoResponseTime <= 0f)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            switch (this.mActionType)
            {
                case 0:
                    goto Label_003F;

                case 1:
                    goto Label_0044;

                case 2:
                    goto Label_004F;

                case 3:
                    goto Label_005A;

                case 4:
                    goto Label_0065;

                case 5:
                    goto Label_006A;

                case 6:
                    goto Label_006F;
            }
            goto Label_0074;
        Label_003F:
            goto Label_0074;
        Label_0044:
            this.DestroyMask();
            goto Label_0074;
        Label_004F:
            this.DestroyMask();
            goto Label_0074;
        Label_005A:
            this.DestroyMask();
            goto Label_0074;
        Label_0065:
            goto Label_0074;
        Label_006A:
            goto Label_0074;
        Label_006F:;
        Label_0074:
            return;
        }

        private void OnClick_EnableArea()
        {
            eActionType type;
            if (this.mNoResponseTime <= 0f)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            switch (this.mActionType)
            {
                case 0:
                    goto Label_003F;

                case 1:
                    goto Label_004A;

                case 2:
                    goto Label_0055;

                case 3:
                    goto Label_0060;

                case 4:
                    goto Label_006B;

                case 5:
                    goto Label_0076;

                case 6:
                    goto Label_0081;
            }
            goto Label_008C;
        Label_003F:
            this.MoveUnit();
            goto Label_008C;
        Label_004A:
            this.DestroyMask();
            goto Label_008C;
        Label_0055:
            this.DestroyMask();
            goto Label_008C;
        Label_0060:
            this.DestroyMask();
            goto Label_008C;
        Label_006B:
            this.TapNormalAtk();
            goto Label_008C;
        Label_0076:
            this.ExecNormalAtk();
            goto Label_008C;
        Label_0081:
            this.SelectDir();
        Label_008C:
            return;
        }

        private unsafe void Resize()
        {
            Vector2 vector;
            Vector2 vector2;
            RectTransform transform;
            Vector3 vector3;
            Vector3 vector4;
            float num;
            float num2;
            RectTransform transform2;
            Vector2 vector5;
            Vector2 vector6;
            Vector2 vector7;
            Vector2 vector8;
            if (&this.mMaskSize.x != 0f)
            {
                goto Label_002B;
            }
            if (&this.mMaskSize.y != 0f)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            &vector..ctor(&this.mMaskRectTransform.get_anchoredPosition().x - (&this.mMaskSize.x / 2f), &this.mMaskRectTransform.get_anchoredPosition().y - (&this.mMaskSize.y / 2f));
            &vector2..ctor(&this.mMaskRectTransform.get_anchoredPosition().x + (&this.mMaskSize.x / 2f), &this.mMaskRectTransform.get_anchoredPosition().y + (&this.mMaskSize.y / 2f));
            transform = base.get_transform() as RectTransform;
            vector3 = transform.InverseTransformPoint(vector);
            vector4 = transform.InverseTransformPoint(vector2);
            num = Mathf.Abs(&vector4.x - &vector3.x);
            num2 = Mathf.Abs(&vector4.y - &vector3.y);
            transform2 = this.mMask.get_transform() as RectTransform;
            transform2.set_sizeDelta(new Vector2(num, num2));
            return;
        }

        private void SelectDir()
        {
            this.DestroyMask();
            return;
        }

        public void Setup(eActionType act_type, Vector3 world_pos, bool is_world2screen, string text)
        {
            bool flag;
            int num;
            if ((this.mMask == null) != null)
            {
                goto Label_0022;
            }
            if ((this.mMaskRectTransform == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            this.mMask.SetActive(1);
            this.mIsFinishSetup = 1;
            this.mTargetWorldPos = world_pos;
            this.mActionType = act_type;
            this.mIsWorld2Screen = is_world2screen;
            flag = string.IsNullOrEmpty(text) == 0;
            this.mArrow.get_gameObject().SetActive(flag == 0);
            this.mTextRoot.get_gameObject().SetActive(flag);
            if (flag == null)
            {
                goto Label_008E;
            }
            this.mText.set_text(text);
        Label_008E:
            this.mEnableArea.get_onClick().AddListener(new UnityAction(this, this.OnClick_EnableArea));
            num = 0;
            goto Label_00D3;
        Label_00B1:
            this.mDisableAreas[num].get_onClick().AddListener(new UnityAction(this, this.OnClick_DisableArea));
            num += 1;
        Label_00D3:
            if (num < ((int) this.mDisableAreas.Length))
            {
                goto Label_00B1;
            }
            return;
        }

        public void SetupMaskSize(Vector2 size)
        {
            this.mMaskSize = size;
            return;
        }

        public void SetupNoResponseTime(float second)
        {
            this.mNoResponseTime = second;
            return;
        }

        private void TapNormalAtk()
        {
            Button button;
            SceneBattle.Instance.BattleUI.CommandWindow.AttackButton.GetComponentInChildren<Button>().get_onClick().Invoke();
            this.DestroyMask();
            return;
        }

        private unsafe void Update()
        {
            RectTransform transform;
            Vector3 vector;
            Vector3 vector2;
            AnimatorStateInfo info;
            if (this.mIsFinishSetup != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            transform = base.get_transform() as RectTransform;
            &vector..ctor(&this.mTargetWorldPos.x, &this.mTargetWorldPos.y, &this.mTargetWorldPos.z);
            if (this.mIsWorld2Screen == null)
            {
                goto Label_0063;
            }
            vector = RectTransformUtility.WorldToScreenPoint(Camera.get_main(), vector);
            this.mTargetScreenPos = vector;
        Label_0063:
            vector2 = transform.InverseTransformPoint(vector);
            this.mMaskRectTransform.set_anchoredPosition(new Vector3(&vector2.x, &vector2.y, &vector2.z));
            this.mNoResponseTime = Mathf.Max(0f, this.mNoResponseTime - Time.get_deltaTime());
            this.Resize();
            if (this.mOpendMethod == null)
            {
                goto Label_0103;
            }
            if ((this.mAnimator != null) == null)
            {
                goto Label_0103;
            }
            if (&this.mAnimator.GetCurrentAnimatorStateInfo(0).get_normalizedTime() < 1f)
            {
                goto Label_0103;
            }
            this.mOpendMethod();
            this.mOpendMethod = null;
        Label_0103:
            return;
        }

        public enum eActionType
        {
            MOVE_UNIT,
            ATTACK_TARGET_DESC,
            NORMAL_ATTACK_DESC,
            ABILITY_DESC,
            TAP_NORMAL_ATTACK,
            EXEC_NORMAL_ATTACK,
            SELECT_DIR
        }

        public delegate void OpendMethod();
    }
}

