namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class UnitPicker : UIBehaviour
    {
        private Animator mAnimator;
        public string OpenTrigger;
        public string CloseTrigger;
        public float CloseDelay1;
        public float CloseDelay2;
        public ListItemEvents Item_Remove;
        public ListItemEvents Item_Inactive;
        public ListItemEvents Item_Active;

        public UnitPicker()
        {
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            this.mAnimator = base.GetComponent<Animator>();
            return;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            return;
        }

        public void Refresh(List<UnitData> inactive, List<UnitData> active)
        {
        }

        protected override void Start()
        {
            base.Start();
            this.mAnimator.SetTrigger(this.OpenTrigger);
            return;
        }
    }
}

