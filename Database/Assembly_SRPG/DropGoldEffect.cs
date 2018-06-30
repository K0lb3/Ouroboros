namespace SRPG
{
    using System;
    using UnityEngine;

    public class DropGoldEffect : MonoBehaviour
    {
        public const string GOLD_GAMEOBJECT_NAME = "UI_GOLD";
        [NonSerialized]
        public int Gold;
        private RectTransform m_TargetRect;
        private Unit m_DropOwner;

        public DropGoldEffect()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            GameObject obj2;
            obj2 = GameObjectID.FindGameObject("UI_GOLD");
            if ((obj2 == null) == null)
            {
                goto Label_001C;
            }
            goto Label_002D;
        Label_001C:
            this.m_TargetRect = obj2.get_transform() as RectTransform;
        Label_002D:
            SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropGoldEffect.End", this);
            GameUtility.RequireComponent<OneShotParticle>(base.get_gameObject());
            return;
        }

        public RectTransform TargetRect
        {
            get
            {
                return this.m_TargetRect;
            }
        }

        public Unit DropOwner
        {
            get
            {
                return this.m_DropOwner;
            }
            set
            {
                this.m_DropOwner = value;
                return;
            }
        }
    }
}

