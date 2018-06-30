namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class TreasureBox : MonoBehaviour
    {
        public AnimationClip OpenAnimation;
        public float DropDelay;
        public float GoldDelay;
        public Vector3 DropOffset;
        private DropItemEffect mDropItem;
        private bool mDropSpawned;
        private bool mGoldSpawned;
        private bool mOpened;
        private DropGoldEffect mDropGold;
        [CompilerGenerated]
        private Unit <owner>k__BackingField;

        public TreasureBox()
        {
            this.DropDelay = 1f;
            this.GoldDelay = 1f;
            this.DropOffset = new Vector3(0f, 0f, 0f);
            base..ctor();
            return;
        }

        public bool IsPlaying()
        {
            return (((base.get_gameObject() != null) == null) ? 0 : base.GetComponent<Animation>().get_isPlaying());
        }

        private void OnDestroy()
        {
            GameUtility.DestroyGameObject(this.mDropItem);
            GameUtility.DestroyGameObject(this.mDropGold);
            return;
        }

        public void Open(Unit.DropItem DropItem, DropItemEffect dropItemTemplate, int numGolds, DropGoldEffect dropGoldTemplate)
        {
            Transform transform;
            base.GetComponent<Animation>().AddClip(this.OpenAnimation, this.OpenAnimation.get_name());
            base.GetComponent<Animation>().Play(this.OpenAnimation.get_name());
            this.mOpened = 1;
            transform = base.get_transform();
            if (numGolds <= 0)
            {
                goto Label_0099;
            }
            this.mDropGold = Object.Instantiate<DropGoldEffect>(dropGoldTemplate);
            this.mDropGold.get_transform().set_position(transform.get_position());
            this.mDropGold.DropOwner = this.owner;
            this.mDropGold.Gold = numGolds;
            this.mDropGold.get_gameObject().SetActive(0);
        Label_0099:
            if ((dropItemTemplate != null) == null)
            {
                goto Label_013C;
            }
            if (DropItem == null)
            {
                goto Label_013C;
            }
            this.mDropItem = Object.Instantiate<DropItemEffect>(dropItemTemplate);
            this.mDropItem.get_transform().set_position(transform.get_position() + this.DropOffset);
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_010E;
            }
            SceneBattle.Popup2D(this.mDropItem.get_gameObject(), this.mDropItem.get_transform().get_position(), 0, 0f);
        Label_010E:
            this.mDropItem.DropOwner = this.owner;
            this.mDropItem.DropItem = DropItem;
            this.mDropItem.get_gameObject().SetActive(0);
        Label_013C:
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (this.mOpened != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mDropSpawned != null)
            {
                goto Label_0062;
            }
            if ((this.mDropItem != null) == null)
            {
                goto Label_0062;
            }
            this.DropDelay -= Time.get_deltaTime();
            if (this.DropDelay > 0f)
            {
                goto Label_0062;
            }
            this.mDropSpawned = 1;
            this.mDropItem.get_gameObject().SetActive(1);
        Label_0062:
            if (this.mGoldSpawned != null)
            {
                goto Label_00BF;
            }
            if ((this.mDropGold != null) == null)
            {
                goto Label_00BF;
            }
            this.GoldDelay -= Time.get_deltaTime();
            if (this.GoldDelay > 0f)
            {
                goto Label_00BF;
            }
            this.mGoldSpawned = 1;
            this.mDropGold.get_gameObject().SetActive(1);
            this.mDropGold = null;
        Label_00BF:
            if (this.IsPlaying() != null)
            {
                goto Label_00F6;
            }
            if (this.mDropGold != null)
            {
                goto Label_00F6;
            }
            if (this.mDropItem != null)
            {
                goto Label_00F6;
            }
            Object.Destroy(base.get_gameObject());
            return;
        Label_00F6:
            return;
        }

        public Unit owner
        {
            [CompilerGenerated]
            get
            {
                return this.<owner>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<owner>k__BackingField = value;
                return;
            }
        }
    }
}

