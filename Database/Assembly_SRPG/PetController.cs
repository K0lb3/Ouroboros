namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class PetController : AnimationPlayer
    {
        public const string PetPath = "Pets/";
        public const string PetAnimationPath = "CHM/";
        private const string ID_IDLE = "IDLE";
        private const string ID_RUN = "RUN";
        public string PetID;
        public GameObject Owner;
        private GameObject mTarget;
        private bool mLoading;
        private StateMachine<PetController> mStateMachine;
        private Vector3 mVelocity;
        private Vector3 mAcceleration;

        public PetController()
        {
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncSetup()
        {
            <AsyncSetup>c__Iterator7D iteratord;
            iteratord = new <AsyncSetup>c__Iterator7D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        protected override void Start()
        {
            base.Start();
            base.StartCoroutine(this.AsyncSetup());
            this.mStateMachine = new StateMachine<PetController>(this);
            return;
        }

        protected override void Update()
        {
            base.Update();
            this.mStateMachine.Update();
            return;
        }

        public override bool IsLoading
        {
            get
            {
                return ((base.IsLoading != null) ? 1 : this.mLoading);
            }
        }

        [CompilerGenerated]
        private sealed class <AsyncSetup>c__Iterator7D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <prefab>__1;
            internal Transform <prefabTransform>__2;
            internal int $PC;
            internal object $current;
            internal PetController <>f__this;

            public <AsyncSetup>c__Iterator7D()
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
                        goto Label_0029;

                    case 1:
                        goto Label_0082;

                    case 2:
                        goto Label_0186;

                    case 3:
                        goto Label_01C5;
                }
                goto Label_01CC;
            Label_0029:
                this.<>f__this.mLoading = 1;
                this.<req>__0 = GameUtility.LoadResourceAsyncChecked<GameObject>("Pets/" + this.<>f__this.PetID);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0082;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_01CE;
            Label_0082:
                this.<prefab>__1 = this.<req>__0.asset as GameObject;
                this.<prefabTransform>__2 = this.<prefab>__1.get_transform();
                this.<>f__this.mTarget = Object.Instantiate(this.<prefab>__1, this.<prefabTransform>__2.get_position(), this.<prefabTransform>__2.get_rotation()) as GameObject;
                this.<>f__this.mTarget.get_transform().SetParent(this.<>f__this.get_transform(), 0);
                this.<>f__this.SetAnimationComponent(this.<>f__this.mTarget.GetComponent<Animation>());
                this.<>f__this.LoadAnimationAsync("IDLE", "CHM/" + this.<>f__this.PetID + "_idle0");
                this.<>f__this.LoadAnimationAsync("RUN", "CHM/" + this.<>f__this.PetID + "_runfield0");
                goto Label_0186;
            Label_016F:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_01CE;
            Label_0186:
                if (this.<>f__this.IsLoading != null)
                {
                    goto Label_016F;
                }
                this.<>f__this.mLoading = 0;
                this.<>f__this.mStateMachine.GotoState<PetController.State_Idle>();
                this.$current = null;
                this.$PC = 3;
                goto Label_01CE;
            Label_01C5:
                this.$PC = -1;
            Label_01CC:
                return 0;
            Label_01CE:
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

        private class State : State<PetController>
        {
            public State()
            {
                base..ctor();
                return;
            }
        }

        private class State_Idle : PetController.State
        {
            public State_Idle()
            {
                base..ctor();
                return;
            }

            public override void Begin(PetController self)
            {
                self.PlayAnimation("IDLE", 1);
                return;
            }
        }

        private class State_Move : PetController.State
        {
            public State_Move()
            {
                base..ctor();
                return;
            }
        }
    }
}

