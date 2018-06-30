namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class WorldMapController : MonoBehaviour
    {
        public AreaMapController[] AreaMaps;
        public RawImage[] Images;
        public RadialBlurEffect RadialBlurEffect;
        public bool AutoSelectArea;
        public QuestSectionList SectionList;
        private RectTransform mTransform;
        private Vector2 mDefaultPosition;
        private Vector2 mDefaultScale;
        private AreaMapController mCurrentArea;
        private AreaMapController mPrevArea;
        private AreaMapController mNextArea;
        public float TransitionTime;
        public AnimationCurve RadialBlurCurve;
        private StateMachine<WorldMapController> mStateMachine;

        public WorldMapController()
        {
            this.AreaMaps = new AreaMapController[0];
            this.Images = new RawImage[0];
            this.TransitionTime = 1f;
            base..ctor();
            return;
        }

        public static WorldMapController FindInstance(string gameobjectID)
        {
            GameObject obj2;
            obj2 = GameObjectID.FindGameObject(gameobjectID);
            if ((obj2 != null) == null)
            {
                goto Label_001A;
            }
            return obj2.GetComponent<WorldMapController>();
        Label_001A:
            return null;
        }

        public void GotoArea(string areaID)
        {
            int num;
            num = 0;
            goto Label_0032;
        Label_0007:
            if ((this.AreaMaps[num].MapID == areaID) == null)
            {
                goto Label_002E;
            }
            this.mCurrentArea = this.AreaMaps[num];
            return;
        Label_002E:
            num += 1;
        Label_0032:
            if (num < ((int) this.AreaMaps.Length))
            {
                goto Label_0007;
            }
            this.mCurrentArea = null;
            return;
        }

        private void SetRadialBlurStrength(float t)
        {
            if (this.RadialBlurCurve == null)
            {
                goto Label_003A;
            }
            if (((int) this.RadialBlurCurve.get_keys().Length) <= 0)
            {
                goto Label_003A;
            }
            this.RadialBlurEffect.Strength = this.RadialBlurCurve.Evaluate(t);
            goto Label_0051;
        Label_003A:
            this.RadialBlurEffect.Strength = Mathf.Sin(t * 3.141593f);
        Label_0051:
            return;
        }

        private void Start()
        {
            bool flag;
            GameManager manager;
            QuestParam param;
            int num;
            int num2;
            int num3;
            this.mTransform = base.get_transform() as RectTransform;
            this.mDefaultPosition = this.mTransform.get_anchoredPosition();
            this.mDefaultScale = this.mTransform.get_localScale();
            this.mStateMachine = new StateMachine<WorldMapController>(this);
            flag = MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart();
            if (this.AutoSelectArea == null)
            {
                goto Label_0229;
            }
            if (flag != null)
            {
                goto Label_0229;
            }
            manager = MonoSingleton<GameManager>.Instance;
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) == null)
            {
                goto Label_00A1;
            }
            param = manager.Player.FindLastStoryQuest();
            if (param == null)
            {
                goto Label_00A1;
            }
            GlobalVars.SelectedSection.Set(param.Chapter.section);
        Label_00A1:
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) == null)
            {
                goto Label_0136;
            }
            if (string.IsNullOrEmpty(GlobalVars.SelectedChapter) == null)
            {
                goto Label_0136;
            }
            GlobalVars.SelectedSection.Set(manager.Sections[0].iname);
            num = 0;
            goto Label_0128;
        Label_00E7:
            if ((manager.Chapters[num].section == GlobalVars.SelectedSection) == null)
            {
                goto Label_0124;
            }
            GlobalVars.SelectedChapter.Set(manager.Chapters[num].iname);
            goto Label_0136;
        Label_0124:
            num += 1;
        Label_0128:
            if (num < ((int) manager.Chapters.Length))
            {
                goto Label_00E7;
            }
        Label_0136:
            num2 = 0;
            goto Label_01F8;
        Label_013E:
            if ((manager.Chapters[num2].section == GlobalVars.SelectedSection) == null)
            {
                goto Label_01F2;
            }
            if ((manager.Chapters[num2].iname == GlobalVars.SelectedChapter) != null)
            {
                goto Label_0196;
            }
            if (string.IsNullOrEmpty(GlobalVars.SelectedChapter) == null)
            {
                goto Label_01F2;
            }
        Label_0196:
            num3 = 0;
            goto Label_01DE;
        Label_019E:
            if ((this.AreaMaps[num3].MapID == manager.Chapters[num2].world) == null)
            {
                goto Label_01D8;
            }
            this.mCurrentArea = this.AreaMaps[num3];
            goto Label_01ED;
        Label_01D8:
            num3 += 1;
        Label_01DE:
            if (num3 < ((int) this.AreaMaps.Length))
            {
                goto Label_019E;
            }
        Label_01ED:
            goto Label_0207;
        Label_01F2:
            num2 += 1;
        Label_01F8:
            if (num2 < ((int) manager.Chapters.Length))
            {
                goto Label_013E;
            }
        Label_0207:
            if ((this.mCurrentArea != null) == null)
            {
                goto Label_0236;
            }
            this.mStateMachine.GotoState<State_World2Area>();
            return;
            goto Label_0236;
        Label_0229:
            if (flag == null)
            {
                goto Label_0236;
            }
            this.AutoSelectArea = 0;
        Label_0236:
            this.mStateMachine.GotoState<State_WorldSelect>();
            return;
        }

        private void Update()
        {
            this.mStateMachine.Update();
            return;
        }

        private class State_Area2World : State<WorldMapController>
        {
            private float mTransition;
            private Vector2 mStartScale;
            private Vector2 mStartPosition;

            public State_Area2World()
            {
                base..ctor();
                return;
            }

            public override void Begin(WorldMapController self)
            {
                if ((self.mCurrentArea != null) == null)
                {
                    goto Label_002D;
                }
                self.mPrevArea.SetOpacity(0f);
                self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
                return;
            Label_002D:
                this.mStartScale = self.mTransform.get_localScale();
                this.mStartPosition = self.mTransform.get_localPosition();
                return;
            }

            public override void Update(WorldMapController self)
            {
                float num;
                if ((self.mCurrentArea != null) == null)
                {
                    goto Label_001D;
                }
                self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
                return;
            Label_001D:
                this.mTransition = Mathf.Clamp01(this.mTransition + ((1f / self.TransitionTime) * Time.get_deltaTime()));
                num = Mathf.Sin((this.mTransition * 3.141593f) * 0.5f);
                self.mPrevArea.SetOpacity(1f - num);
                self.mTransform.set_anchoredPosition(Vector2.Lerp(this.mStartPosition, self.mDefaultPosition, num));
                self.mTransform.set_localScale(Vector2.Lerp(this.mStartScale, self.mDefaultScale, num));
                self.SetRadialBlurStrength(this.mTransition);
                if (this.mTransition < 1f)
                {
                    goto Label_00E2;
                }
                self.mPrevArea.SetOpacity(0f);
                self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
                return;
            Label_00E2:
                return;
            }
        }

        private class State_AreaSelect : State<WorldMapController>
        {
            private AreaMapController mArea;

            public State_AreaSelect()
            {
                base..ctor();
                return;
            }

            public override void Begin(WorldMapController self)
            {
                this.mArea = self.mCurrentArea;
                return;
            }

            public override void Update(WorldMapController self)
            {
                if ((self.mCurrentArea != this.mArea) == null)
                {
                    goto Label_003A;
                }
                self.mPrevArea = this.mArea;
                self.mNextArea = self.mCurrentArea;
                self.mStateMachine.GotoState<WorldMapController.State_Area2World>();
                return;
            Label_003A:
                return;
            }
        }

        private class State_World2Area : State<WorldMapController>
        {
            private float mTransition;
            private AreaMapController mTarget;
            private Vector2 mDesiredScale;
            private Vector2 mDesiredPosition;
            private Vector2 mTargetPosition;

            public State_World2Area()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(WorldMapController self)
            {
                RectTransform transform;
                float num;
                float num2;
                Vector3 vector;
                Vector3 vector2;
                if ((self.mCurrentArea == null) == null)
                {
                    goto Label_001C;
                }
                self.mStateMachine.GotoState<WorldMapController.State_WorldSelect>();
            Label_001C:
                this.mTarget = self.mCurrentArea;
                transform = this.mTarget.get_transform() as RectTransform;
                num = (1f / &transform.get_localScale().x) * &self.mDefaultScale.x;
                num2 = (1f / &transform.get_localScale().y) * &self.mDefaultScale.y;
                this.mDesiredScale = new Vector3(num, num2);
                this.mTargetPosition = transform.get_anchoredPosition();
                this.mDesiredPosition = -transform.get_anchoredPosition();
                &this.mDesiredPosition.x *= num;
                &this.mDesiredPosition.y *= num2;
                if (self.AutoSelectArea == null)
                {
                    goto Label_0126;
                }
                self.AutoSelectArea = 0;
                this.mTarget.SetOpacity(1f);
                self.mTransform.set_anchoredPosition(this.mDesiredPosition);
                self.mTransform.set_localScale(this.mDesiredScale);
                self.mStateMachine.GotoState<WorldMapController.State_AreaSelect>();
                return;
            Label_0126:
                return;
            }

            public override unsafe void Update(WorldMapController self)
            {
                float num;
                float num2;
                float num3;
                Rect rect;
                Rect rect2;
                if ((self.mCurrentArea == null) == null)
                {
                    goto Label_0030;
                }
                self.mPrevArea = this.mTarget;
                self.mNextArea = null;
                self.mStateMachine.GotoState<WorldMapController.State_Area2World>();
                return;
            Label_0030:
                this.mTransition = Mathf.Clamp01(this.mTransition + ((1f / self.TransitionTime) * Time.get_deltaTime()));
                num = Mathf.Sin((this.mTransition * 3.141593f) * 0.5f);
                this.mTarget.SetOpacity(num);
                self.mTransform.set_anchoredPosition(Vector2.Lerp(self.mDefaultPosition, this.mDesiredPosition, num));
                self.mTransform.set_localScale(Vector2.Lerp(self.mDefaultScale, this.mDesiredScale, num));
                self.SetRadialBlurStrength(this.mTransition);
                if ((self.RadialBlurEffect != null) == null)
                {
                    goto Label_0133;
                }
                num2 = (&this.mTargetPosition.x / &self.mTransform.get_rect().get_width()) + 0.5f;
                num3 = (&this.mTargetPosition.y / &self.mTransform.get_rect().get_height()) + 0.5f;
                self.RadialBlurEffect.Focus = new Vector2(num2, num3);
            Label_0133:
                if (this.mTransition < 1f)
                {
                    goto Label_014F;
                }
                self.mStateMachine.GotoState<WorldMapController.State_AreaSelect>();
                return;
            Label_014F:
                return;
            }
        }

        private class State_WorldSelect : State<WorldMapController>
        {
            public State_WorldSelect()
            {
                base..ctor();
                return;
            }

            public override void Begin(WorldMapController self)
            {
                if ((self.mNextArea != null) == null)
                {
                    goto Label_0030;
                }
                self.mCurrentArea = self.mNextArea;
                self.mNextArea = null;
                self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
                return;
            Label_0030:
                return;
            }

            public override void Update(WorldMapController self)
            {
                if ((self.mCurrentArea != null) == null)
                {
                    goto Label_001D;
                }
                self.mStateMachine.GotoState<WorldMapController.State_World2Area>();
                return;
            Label_001D:
                return;
            }
        }
    }
}

