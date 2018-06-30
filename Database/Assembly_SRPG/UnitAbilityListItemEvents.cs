namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UnitAbilityListItemEvents : ListItemEvents
    {
        private ListItemTouchController mTouchController;
        public ListItemEvents.ListItemEvent OnRankUp;
        public ListItemEvents.ListItemEvent OnRankUpBtnPress;
        public ListItemEvents.ListItemEvent OnRankUpBtnUp;
        [HelpBox("アビリティをランクアップ可能であればこのゲームオブジェクトを選択可能にします。")]
        public Selectable RankupButton;
        public RectTransform AbilityPoint;
        public GameObject LabelLevel;
        public GameObject LabelLevelMax;
        private float mLastUpdateTime;

        public UnitAbilityListItemEvents()
        {
            base..ctor();
            return;
        }

        private void OnDisable()
        {
            GameManager local1;
            if ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null)
            {
                goto Label_0036;
            }
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnAbilityRankUpCountChange = (GameManager.RankUpCountChangeEvent) Delegate.Remove(local1.OnAbilityRankUpCountChange, new GameManager.RankUpCountChangeEvent(this.OnRankUpCountChange));
        Label_0036:
            return;
        }

        private void OnEnable()
        {
            GameManager local1;
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnAbilityRankUpCountChange = (GameManager.RankUpCountChangeEvent) Delegate.Combine(local1.OnAbilityRankUpCountChange, new GameManager.RankUpCountChangeEvent(this.OnRankUpCountChange));
            return;
        }

        private void OnRankUpCountChange(int count)
        {
            this.UpdateItemStates();
            return;
        }

        public void RankUp(ListItemTouchController controller)
        {
            if (this.OnRankUp == null)
            {
                goto Label_001C;
            }
            this.OnRankUp(base.get_gameObject());
        Label_001C:
            return;
        }

        private void RankUpPress(ListItemTouchController controller)
        {
            if (this.OnRankUpBtnPress == null)
            {
                goto Label_001C;
            }
            this.OnRankUpBtnPress(base.get_gameObject());
        Label_001C:
            return;
        }

        private void RankUpUp(ListItemTouchController controller)
        {
            if (this.OnRankUpBtnUp == null)
            {
                goto Label_001C;
            }
            this.OnRankUpBtnUp(base.get_gameObject());
        Label_001C:
            return;
        }

        private void Start()
        {
            this.UpdateItemStates();
            if ((this.RankupButton != null) == null)
            {
                goto Label_0072;
            }
            this.mTouchController = this.RankupButton.get_gameObject().AddComponent<ListItemTouchController>();
            this.mTouchController.OnPointerDownFunc = new ListItemTouchController.DelegateOnPointerDown(this.RankUpPress);
            this.mTouchController.OnPointerUpFunc = new ListItemTouchController.DelegateOnPointerUp(this.RankUpUp);
            this.mTouchController.RankUpFunc = new ListItemTouchController.DelegateRankUp(this.RankUp);
        Label_0072:
            return;
        }

        private void UpdateItemStates()
        {
            AbilityData data;
            bool flag;
            bool flag2;
            data = DataSource.FindDataOfClass<AbilityData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_00E1;
            }
            flag = (data.Rank < data.GetRankMaxCap()) == 0;
            if ((this.LabelLevel != null) == null)
            {
                goto Label_0045;
            }
            this.LabelLevel.SetActive(flag == 0);
        Label_0045:
            if ((this.LabelLevelMax != null) == null)
            {
                goto Label_0062;
            }
            this.LabelLevelMax.SetActive(flag);
        Label_0062:
            if ((this.RankupButton != null) == null)
            {
                goto Label_00B2;
            }
            this.RankupButton.get_gameObject().SetActive(data.Rank < data.GetRankCap());
            flag2 = 1;
            flag2 &= MonoSingleton<GameManager>.Instance.Player.CheckRankUpAbility(data);
            this.RankupButton.set_interactable(flag2);
        Label_00B2:
            if ((this.AbilityPoint != null) == null)
            {
                goto Label_00E1;
            }
            this.AbilityPoint.get_gameObject().SetActive(data.Rank < data.GetRankCap());
        Label_00E1:
            return;
        }

        public ListItemTouchController ItemTouchController
        {
            get
            {
                return this.mTouchController;
            }
        }

        public class ListItemTouchController : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
        {
            public DelegateOnPointerDown OnPointerDownFunc;
            public DelegateOnPointerUp OnPointerUpFunc;
            public DelegateRankUp RankUpFunc;
            public float HoldDuration;
            public float HoldSpan;
            public bool Holding;
            public bool IsFirstDownFunc;
            private static readonly float FirstSpan;
            private static readonly float SecondSpanMax;
            private static readonly float SecondSpanMin;
            private static readonly float SecondSpanOffset;
            private Vector2 mDragStartPos;

            static ListItemTouchController()
            {
                FirstSpan = 0.3f;
                SecondSpanMax = 0.3f;
                SecondSpanMin = 0.1f;
                SecondSpanOffset = 0.1f;
                return;
            }

            public ListItemTouchController()
            {
                this.HoldSpan = 0.25f;
                base..ctor();
                return;
            }

            public void OnDestroy()
            {
                this.StatusReset();
                if (this.OnPointerDownFunc == null)
                {
                    goto Label_0018;
                }
                this.OnPointerDownFunc = null;
            Label_0018:
                if (this.OnPointerUpFunc == null)
                {
                    goto Label_002A;
                }
                this.OnPointerUpFunc = null;
            Label_002A:
                return;
            }

            public void OnPointerDown(PointerEventData eventData)
            {
                if (this.OnPointerDownFunc == null)
                {
                    goto Label_0030;
                }
                this.StatusReset();
                this.OnPointerDownFunc(this);
                this.Holding = 1;
                this.mDragStartPos = eventData.get_position();
            Label_0030:
                return;
            }

            public void OnPointerUp()
            {
                if (this.OnPointerUpFunc == null)
                {
                    goto Label_0017;
                }
                this.OnPointerUpFunc(this);
            Label_0017:
                this.StatusReset();
                return;
            }

            public unsafe void StatusReset()
            {
                this.HoldDuration = 0f;
                this.Holding = 0;
                this.HoldSpan = FirstSpan;
                this.IsFirstDownFunc = 0;
                &this.mDragStartPos.Set(0f, 0f);
                return;
            }

            public unsafe void UpdatePress(float deltaTime)
            {
                bool flag;
                GameSettings settings;
                float num;
                bool flag2;
                Vector2 vector;
                flag = Input.GetMouseButton(0);
                if (this.Holding == null)
                {
                    goto Label_003C;
                }
                if (flag != null)
                {
                    goto Label_003C;
                }
                if (this.HoldDuration >= this.HoldSpan)
                {
                    goto Label_0035;
                }
                this.RankUpFunc(this);
            Label_0035:
                this.OnPointerUp();
                return;
            Label_003C:
                settings = GameSettings.Instance;
                num = (float) (settings.HoldMargin * settings.HoldMargin);
                vector = this.mDragStartPos - Input.get_mousePosition();
                flag2 = &vector.get_sqrMagnitude() > num;
                if (this.HoldDuration >= this.HoldSpan)
                {
                    goto Label_0091;
                }
                if (flag2 == null)
                {
                    goto Label_0091;
                }
                this.OnPointerUp();
                return;
            Label_0091:
                if (this.Holding == null)
                {
                    goto Label_0124;
                }
                this.HoldDuration += deltaTime;
                if (this.HoldDuration < this.HoldSpan)
                {
                    goto Label_0124;
                }
                this.HoldDuration -= this.HoldSpan;
                if (this.IsFirstDownFunc != null)
                {
                    goto Label_00F0;
                }
                this.IsFirstDownFunc = 1;
                this.HoldSpan = SecondSpanMax;
                goto Label_0118;
            Label_00F0:
                this.HoldSpan -= SecondSpanOffset;
                this.HoldSpan = Mathf.Max(SecondSpanMin, this.HoldSpan);
            Label_0118:
                this.RankUpFunc(this);
            Label_0124:
                return;
            }

            public delegate void DelegateOnPointerDown(UnitAbilityListItemEvents.ListItemTouchController controller);

            public delegate void DelegateOnPointerUp(UnitAbilityListItemEvents.ListItemTouchController controller);

            public delegate void DelegateRankUp(UnitAbilityListItemEvents.ListItemTouchController controller);
        }
    }
}

