namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [RequireComponent(typeof(Selectable))]
    public class LevelLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
    {
        private static List<LevelLock> mInstances;
        public UnlockTargets Condition;
        public Text ConditionText;
        public GameObject ShowLocked;
        public GameObject ShowUnlocked;
        public bool ToggleInteractable;
        public GameObject ReleaseStoryPart;
        [SerializeField]
        private Animator UnlockAnimator;
        [SerializeField]
        private bool UnlockAnimationOnStart;
        private int mUnlockLevel;
        private int mUnlockVipRank;

        static LevelLock()
        {
            mInstances = new List<LevelLock>();
            return;
        }

        public LevelLock()
        {
            this.ToggleInteractable = 1;
            base..ctor();
            return;
        }

        public bool GetIsUnlockAnimationPlayable()
        {
            GameManager manager;
            UnlockParam param;
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.MasterParam.FindUnlockParam(this.Condition);
            if (param == null)
            {
                goto Label_0043;
            }
            if (manager.Player.Lv < param.PlayerLevel)
            {
                goto Label_0043;
            }
            return (PlayerPrefsUtility.GetIsUnlockLevelAnimationPlayed(this.Condition) == 0);
        Label_0043:
            return 0;
        }

        private void OnDisable()
        {
            mInstances.Remove(this);
            return;
        }

        private void OnEnable()
        {
            mInstances.Add(this);
            return;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerData data;
            data = MonoSingleton<GameManager>.Instance.Player;
            ShowLockMessage(data.Lv, data.VipRank, this.Condition);
            eventData.Use();
            return;
        }

        public void PlayUnlockAnimation(OnUnlockAnimationEnd callback)
        {
            base.StartCoroutine(this.PlayUnlockAnimationAuto(callback));
            return;
        }

        [DebuggerHidden]
        private IEnumerator PlayUnlockAnimationAuto(OnUnlockAnimationEnd callback)
        {
            <PlayUnlockAnimationAuto>c__Iterator11E iteratore;
            iteratore = new <PlayUnlockAnimationAuto>c__Iterator11E();
            iteratore.callback = callback;
            iteratore.<$>callback = callback;
            iteratore.<>f__this = this;
            return iteratore;
        }

        public static bool ShowLockMessage(int playerLv, int playerVipRank, UnlockTargets target)
        {
            GameManager manager;
            UnlockParam[] paramArray;
            int num;
            UnlockParam param;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.Unlocks;
            num = 0;
            goto Label_0047;
        Label_0019:
            param = paramArray[num];
            if (param == null)
            {
                goto Label_0043;
            }
            if (param.UnlockTarget != target)
            {
                goto Label_0043;
            }
            return ShowLockMessage(playerLv, param.PlayerLevel, playerVipRank, param.VipRank);
        Label_0043:
            num += 1;
        Label_0047:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0019;
            }
            return 0;
        }

        public static bool ShowLockMessage(int playerLv, int reqLv, int vipRank, int reqVipRank)
        {
            string str;
            string str2;
            string str3;
            string str4;
            if (reqLv <= playerLv)
            {
                goto Label_002D;
            }
            str2 = string.Format(LocalizedText.Get("sys.UNLOCK_REQLV"), (int) reqLv);
            UIUtility.SystemMessage(null, str2, null, null, 0, -1);
            return 1;
        Label_002D:
            if (reqVipRank <= vipRank)
            {
                goto Label_005A;
            }
            str4 = string.Format(LocalizedText.Get("sys.UNLOCK_REQVIP"), (int) reqVipRank);
            UIUtility.SystemMessage(null, str4, null, null, 0, -1);
            return 1;
        Label_005A:
            return 0;
        }

        private void Start()
        {
            GameManager manager;
            UnlockParam[] paramArray;
            int num;
            UnlockParam param;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.Unlocks;
            num = 0;
            goto Label_0055;
        Label_0019:
            param = paramArray[num];
            if (param == null)
            {
                goto Label_0051;
            }
            if (param.UnlockTarget != this.Condition)
            {
                goto Label_0051;
            }
            this.mUnlockLevel = param.PlayerLevel;
            this.mUnlockVipRank = param.VipRank;
            goto Label_005E;
        Label_0051:
            num += 1;
        Label_0055:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0019;
            }
        Label_005E:
            this.UpdateLockState();
            if (this.UnlockAnimationOnStart == null)
            {
                goto Label_007D;
            }
            base.StartCoroutine(this.PlayUnlockAnimationAuto(null));
        Label_007D:
            return;
        }

        public void UpdateLockState()
        {
            GameManager manager;
            PlayerData data;
            bool flag;
            Selectable selectable;
            data = MonoSingleton<GameManager>.Instance.Player;
            flag = MonoSingleton<GameManager>.Instance.Player.CheckUnlock(this.Condition);
            if (this.ToggleInteractable == null)
            {
                goto Label_0048;
            }
            selectable = base.GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_0048;
            }
            selectable.set_interactable(flag);
        Label_0048:
            if ((this.ShowUnlocked != null) == null)
            {
                goto Label_0065;
            }
            this.ShowUnlocked.SetActive(flag);
        Label_0065:
            if ((this.ShowLocked != null) == null)
            {
                goto Label_0085;
            }
            this.ShowLocked.SetActive(flag == 0);
        Label_0085:
            if ((this.ConditionText != null) == null)
            {
                goto Label_011F;
            }
            if (this.mUnlockLevel <= 0)
            {
                goto Label_00DD;
            }
            if (data.Lv >= this.mUnlockLevel)
            {
                goto Label_00DD;
            }
            this.ConditionText.set_text(string.Format(LocalizedText.Get("sys.UNLOCK_LV"), (int) this.mUnlockLevel));
            goto Label_011F;
        Label_00DD:
            if (this.mUnlockVipRank <= 0)
            {
                goto Label_011F;
            }
            if (data.VipRank >= this.mUnlockVipRank)
            {
                goto Label_011F;
            }
            this.ConditionText.set_text(string.Format(LocalizedText.Get("sys.UNLOCK_VIP"), (int) this.mUnlockVipRank));
        Label_011F:
            return;
        }

        public static void UpdateLockStates()
        {
            int num;
            num = 0;
            goto Label_001B;
        Label_0007:
            mInstances[num].UpdateLockState();
            num += 1;
        Label_001B:
            if (num < mInstances.Count)
            {
                goto Label_0007;
            }
            return;
        }

        [DebuggerHidden]
        private IEnumerator WaitUnlockAnimation()
        {
            <WaitUnlockAnimation>c__Iterator11D iteratord;
            iteratord = new <WaitUnlockAnimation>c__Iterator11D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        [CompilerGenerated]
        private sealed class <PlayUnlockAnimationAuto>c__Iterator11E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LevelLock.OnUnlockAnimationEnd callback;
            internal int $PC;
            internal object $current;
            internal LevelLock.OnUnlockAnimationEnd <$>callback;
            internal LevelLock <>f__this;

            public <PlayUnlockAnimationAuto>c__Iterator11E()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_006E;
                }
                goto Label_009C;
            Label_0021:
                if (this.<>f__this.GetIsUnlockAnimationPlayable() == null)
                {
                    goto Label_0074;
                }
                PlayerPrefsUtility.SetUnlockLevelAnimationPlayed(this.<>f__this.Condition);
                SRPG_TouchInputModule.LockInput();
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.WaitUnlockAnimation());
                this.$PC = 1;
                goto Label_009E;
            Label_006E:
                SRPG_TouchInputModule.UnlockInput(0);
            Label_0074:
                if (this.callback == null)
                {
                    goto Label_0095;
                }
                this.callback(this.<>f__this.Condition);
            Label_0095:
                this.$PC = -1;
            Label_009C:
                return 0;
            Label_009E:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <WaitUnlockAnimation>c__Iterator11D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal AnimatorStateInfo <animState>__0;
            internal int $PC;
            internal object $current;
            internal LevelLock <>f__this;

            public <WaitUnlockAnimation>c__Iterator11D()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_00AC;
                }
                goto Label_00B8;
            Label_0021:
                if ((this.<>f__this.UnlockAnimator == null) == null)
                {
                    goto Label_003C;
                }
                goto Label_00B8;
            Label_003C:
                this.<>f__this.UnlockAnimator.get_gameObject().SetActive(1);
                this.<>f__this.UnlockAnimator.SetBool("open", 1);
            Label_0068:
                this.<animState>__0 = this.<>f__this.UnlockAnimator.GetCurrentAnimatorStateInfo(0);
                if (&this.<animState>__0.IsName("opened") == null)
                {
                    goto Label_0099;
                }
                goto Label_00B1;
            Label_0099:
                this.$current = null;
                this.$PC = 1;
                goto Label_00BA;
            Label_00AC:
                goto Label_0068;
            Label_00B1:
                this.$PC = -1;
            Label_00B8:
                return 0;
            Label_00BA:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public delegate void OnUnlockAnimationEnd(UnlockTargets condition);
    }
}

