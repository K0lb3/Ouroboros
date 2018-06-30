namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/配置(アニメーション再生付）2", "キャラクターを配置します", 0x664444, 0xaa4444)]
    internal class EventAction_SpawnActorWithAnime2 : EventAction_SpawnActor2
    {
        private const string MOVIE_PATH = "Movies/";
        private const string DEMO_PATH = "Demo/";
        public PREFIX_PATH Path;
        [HideInInspector]
        public string m_AnimationName;
        [HideInInspector]
        public bool m_Loop;
        public AnimeType m_AnimeType;
        private string m_AnimationID;
        [Tooltip("走りアニメーションを指定出来ます。"), HideInInspector]
        public string m_RunAnimation;

        public EventAction_SpawnActorWithAnime2()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            YuremonoInstance[] instanceArray;
            int num;
            if ((base.mController != null) == null)
            {
                goto Label_0133;
            }
            base.mController.get_transform().set_position(base.Position);
            base.mController.CollideGround = base.GroundSnap;
            base.mController.get_transform().set_rotation(Quaternion.Euler(base.RotationX, base.RotationY, base.RotationZ));
            base.mController.SetVisible(base.Display);
            if (base.Yuremono != null)
            {
                goto Label_00A9;
            }
            instanceArray = base.mController.get_gameObject().GetComponentsInChildren<YuremonoInstance>();
            num = 0;
            goto Label_00A0;
        Label_0093:
            instanceArray[num].set_enabled(0);
            num += 1;
        Label_00A0:
            if (num < ((int) instanceArray.Length))
            {
                goto Label_0093;
            }
        Label_00A9:
            if (this.m_AnimeType != null)
            {
                goto Label_00F6;
            }
            if (string.IsNullOrEmpty(this.m_AnimationName) != null)
            {
                goto Label_00F6;
            }
            base.mController.RootMotionMode = 1;
            base.mController.PlayAnimation(this.m_AnimationID, this.m_Loop, 0.1f, 0f);
            goto Label_0112;
        Label_00F6:
            if (this.m_AnimeType != 1)
            {
                goto Label_0112;
            }
            base.mController.PlayIdle(0f);
        Label_0112:
            if (string.IsNullOrEmpty(this.m_RunAnimation) != null)
            {
                goto Label_0133;
            }
            base.mController.SetRunAnimation(this.m_RunAnimation);
        Label_0133:
            base.ActivateNext();
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if ((base.mController != null) == null)
            {
                goto Label_0038;
            }
            if (string.IsNullOrEmpty(this.m_AnimationID) != null)
            {
                goto Label_0038;
            }
            base.mController.UnloadAnimation(this.m_AnimationID);
        Label_0038:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator97 iterator;
            iterator = new <PreloadAssets>c__Iterator97();
            iterator.<>f__this = this;
            return iterator;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__Iterator97 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal GameObject <obj>__1;
            internal string <path>__2;
            internal string <path>__3;
            internal int $PC;
            internal object $current;
            internal EventAction_SpawnActorWithAnime2 <>f__this;

            public <PreloadAssets>c__Iterator97()
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
                Type[] typeArray1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_025F;

                    case 2:
                        goto Label_0344;

                    case 3:
                        goto Label_0390;
                }
                goto Label_0397;
            Label_0029:
                typeArray1 = new Type[] { typeof(TacticsUnitController) };
                this.<go>__0 = new GameObject("DemoCharacter", typeArray1);
                this.<>f__this.mController = this.<go>__0.GetComponent<TacticsUnitController>();
                this.<>f__this.mController.LoadEquipments = this.<>f__this.ShowEquipments;
                this.<>f__this.mController.Posture = this.<>f__this.Posture;
                this.<>f__this.mController.SetupUnit(this.<>f__this.UnitID, this.<>f__this.JobID);
                this.<>f__this.mController.UniqueName = this.<>f__this.ActorID;
                this.<>f__this.mController.KeepUnitHidden = 1;
                this.<>f__this.Sequence.SpawnedObjects.Add(this.<go>__0);
                this.<obj>__1 = EventAction.FindActor(this.<>f__this.ActorID);
                if ((this.<obj>__1 != null) == null)
                {
                    goto Label_035E;
                }
                this.<>f__this.mController = this.<obj>__1.GetComponent<TacticsUnitController>();
                if ((this.<>f__this.mController != null) == null)
                {
                    goto Label_0274;
                }
                if (this.<>f__this.m_AnimeType != null)
                {
                    goto Label_0274;
                }
                if (string.IsNullOrEmpty(this.<>f__this.m_AnimationName) != null)
                {
                    goto Label_0274;
                }
                this.<>f__this.m_AnimationID = "tmp_" + Convert.ToString(this.<>f__this.GetInstanceID(), 0x10);
                this.<path>__2 = string.Empty;
                if (this.<>f__this.Path != 1)
                {
                    goto Label_01DB;
                }
                this.<path>__2 = this.<path>__2 + "Movies/";
                goto Label_0201;
            Label_01DB:
                if (this.<>f__this.Path != null)
                {
                    goto Label_0201;
                }
                this.<path>__2 = this.<path>__2 + "Demo/";
            Label_0201:
                this.<path>__2 = this.<path>__2 + "CHM/" + this.<>f__this.m_AnimationName;
                this.<>f__this.mController.LoadAnimationAsync(this.<>f__this.m_AnimationID, this.<path>__2);
                goto Label_025F;
            Label_0248:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0399;
            Label_025F:
                if (this.<>f__this.mController.IsLoading != null)
                {
                    goto Label_0248;
                }
            Label_0274:
                if (string.IsNullOrEmpty(this.<>f__this.m_RunAnimation) != null)
                {
                    goto Label_037D;
                }
                this.<path>__3 = string.Empty;
                if (this.<>f__this.Path != 1)
                {
                    goto Label_02C0;
                }
                this.<path>__3 = this.<path>__3 + "Movies/";
                goto Label_02E6;
            Label_02C0:
                if (this.<>f__this.Path != null)
                {
                    goto Label_02E6;
                }
                this.<path>__3 = this.<path>__3 + "Demo/";
            Label_02E6:
                this.<path>__3 = this.<path>__3 + "CHM/" + this.<>f__this.m_RunAnimation;
                this.<>f__this.mController.LoadRunAnimation(this.<>f__this.m_RunAnimation, this.<path>__3);
                goto Label_0344;
            Label_032D:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_0399;
            Label_0344:
                if (this.<>f__this.mController.IsLoading != null)
                {
                    goto Label_032D;
                }
                goto Label_037D;
            Label_035E:
                Debug.LogError("アクター'" + this.<>f__this.ActorID + "'は存在しません");
            Label_037D:
                this.$current = null;
                this.$PC = 3;
                goto Label_0399;
            Label_0390:
                this.$PC = -1;
            Label_0397:
                return 0;
            Label_0399:
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

        public enum AnimeType
        {
            Custom,
            Idel
        }

        public enum PREFIX_PATH
        {
            Demo,
            Movie,
            Default
        }
    }
}

