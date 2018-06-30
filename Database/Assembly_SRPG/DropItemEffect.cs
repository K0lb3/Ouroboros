namespace SRPG
{
    using System;
    using System.Collections;
    using UnityEngine;

    public class DropItemEffect : MonoBehaviour
    {
        private const string TREASURE_GAMEOBJECT_NAME = "UI_TREASURE";
        private State m_State;
        private RectTransform m_TargetRect;
        private ItemIcon m_ItemIcon;
        private Unit m_DropOwner;
        private SRPG.Unit.DropItem m_DropItem;
        public float Acceleration;
        public float Delay;
        private float m_Speed;
        private Animator m_EndAnimator;
        public float OpenWait;
        public float PopSpeed;
        private float m_PopSpeed;
        private float m_ScaleSpeed;
        private float m_DeleteDelay;

        public DropItemEffect()
        {
            this.m_State = 1;
            this.Acceleration = 1f;
            this.Delay = 1f;
            this.OpenWait = 0.2f;
            this.PopSpeed = 1f;
            this.m_DeleteDelay = 1f;
            base..ctor();
            return;
        }

        private void Hide()
        {
            Transform transform;
            IEnumerator enumerator;
            IDisposable disposable;
            enumerator = base.get_gameObject().get_transform().GetEnumerator();
        Label_0011:
            try
            {
                goto Label_002E;
            Label_0016:
                transform = (Transform) enumerator.Current;
                transform.get_gameObject().SetActive(0);
            Label_002E:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0016;
                }
                goto Label_0050;
            }
            finally
            {
            Label_003E:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_0049;
                }
            Label_0049:
                disposable.Dispose();
            }
        Label_0050:
            return;
        }

        public void SetItem(SRPG.Unit.DropItem item)
        {
            this.m_DropItem = item;
            return;
        }

        private void Show()
        {
            Transform transform;
            IEnumerator enumerator;
            ItemIcon icon;
            IDisposable disposable;
            enumerator = base.get_gameObject().get_transform().GetEnumerator();
        Label_0011:
            try
            {
                goto Label_002E;
            Label_0016:
                transform = (Transform) enumerator.Current;
                transform.get_gameObject().SetActive(1);
            Label_002E:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0016;
                }
                goto Label_0050;
            }
            finally
            {
            Label_003E:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_0049;
                }
            Label_0049:
                disposable.Dispose();
            }
        Label_0050:
            icon = base.get_gameObject().GetComponent<ItemIcon>();
            if ((icon != null) == null)
            {
                goto Label_008F;
            }
            if (icon.IsSecret == null)
            {
                goto Label_008F;
            }
            if (icon.SecretAmount == null)
            {
                goto Label_008F;
            }
            icon.SecretAmount.SetActive(0);
        Label_008F:
            return;
        }

        private void Start()
        {
            this.Hide();
            return;
        }

        private void State_Delete()
        {
            this.m_DeleteDelay -= Time.get_deltaTime();
            if (this.m_DeleteDelay >= 0f)
            {
                goto Label_0034;
            }
            Object.Destroy(base.get_gameObject());
            this.m_State = 0;
        Label_0034:
            return;
        }

        private void State_End()
        {
            SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropItemEffect.End", this);
            if ((this.m_EndAnimator != null) == null)
            {
                goto Label_0062;
            }
            if (this.m_EndAnimator.GetBool("open") != null)
            {
                goto Label_004C;
            }
            this.m_EndAnimator.SetBool("open", 1);
            goto Label_0062;
        Label_004C:
            this.m_EndAnimator.Play("open", 0, 0f);
        Label_0062:
            this.m_DeleteDelay = 0.1f;
            this.m_State = 6;
            return;
        }

        private unsafe void State_Move()
        {
            Transform transform1;
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            Vector2 vector4;
            Vector2 vector5;
            Vector3 vector6;
            Vector3 vector7;
            Vector3 vector8;
            this.m_Speed += this.Acceleration * Time.get_deltaTime();
            vector = this.m_TargetRect.get_position();
            &vector.x -= &this.m_TargetRect.get_sizeDelta().x * 0.8f;
            &vector.y += &this.m_TargetRect.get_sizeDelta().y * 0.5f;
            vector2 = vector - base.get_transform().get_position();
            vector3 = &vector2.get_normalized() * this.m_Speed;
            if (&vector3.get_sqrMagnitude() >= &vector2.get_sqrMagnitude())
            {
                goto Label_0150;
            }
            transform1 = base.get_transform();
            transform1.set_position(transform1.get_position() + vector3);
            this.m_ScaleSpeed += 1f * Time.get_deltaTime();
            if ((&base.get_transform().get_localScale().x - this.m_ScaleSpeed) <= 0.5f)
            {
                goto Label_0169;
            }
            base.get_transform().set_localScale(new Vector3(&base.get_transform().get_localScale().x - this.m_ScaleSpeed, &base.get_transform().get_localScale().y - this.m_ScaleSpeed, 1f));
            goto Label_0169;
        Label_0150:
            base.get_transform().set_position(vector);
            this.Hide();
            this.m_State = 5;
        Label_0169:
            return;
        }

        private void State_Open()
        {
            if ((this.m_TargetRect == null) == null)
            {
                goto Label_002D;
            }
            SceneBattle.SimpleEvent.Send(SceneBattle.TreasureEvent.GROUP, "DropItemEffect.End", this);
            Object.Destroy(base.get_gameObject());
            return;
        Label_002D:
            this.OpenWait -= Time.get_deltaTime();
            if (this.OpenWait > 0f)
            {
                goto Label_005C;
            }
            this.Show();
            this.m_State = 3;
        Label_005C:
            return;
        }

        private unsafe void State_Popup()
        {
            Vector3 vector;
            float num;
            Vector3 vector2;
            Vector3 vector3;
            this.Delay -= Time.get_deltaTime();
            this.m_PopSpeed += this.PopSpeed * Time.get_deltaTime();
            if (1f <= (&base.get_transform().get_localScale().x + this.m_PopSpeed))
            {
                goto Label_00E4;
            }
            vector = base.get_transform().get_localScale();
            base.get_transform().set_localScale(new Vector3(&vector.x + this.m_PopSpeed, &vector.y + this.m_PopSpeed, &vector.z));
            num = this.m_PopSpeed * 100f;
            if (num <= 25f)
            {
                goto Label_00AC;
            }
            num = 25f;
        Label_00AC:
            vector2 = base.get_transform().get_localPosition();
            base.get_transform().set_localPosition(new Vector3(&vector2.x, &vector2.y + num, &vector2.z));
            goto Label_011A;
        Label_00E4:
            base.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
            if (this.Delay >= 0f)
            {
                goto Label_011A;
            }
            this.m_State = 4;
        Label_011A:
            return;
        }

        private unsafe void State_Setup()
        {
            GameObject obj2;
            ItemIcon icon;
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            obj2 = GameObjectID.FindGameObject("UI_TREASURE");
            if ((obj2 == null) == null)
            {
                goto Label_0026;
            }
            Debug.LogError("UI_TREASUREが見つかりませんでした。");
            goto Label_0037;
        Label_0026:
            this.m_TargetRect = obj2.get_transform() as RectTransform;
        Label_0037:
            if (this.m_DropItem.isItem == null)
            {
                goto Label_0062;
            }
            DataSource.Bind<ItemParam>(base.get_gameObject(), this.m_DropItem.itemParam);
            goto Label_0088;
        Label_0062:
            if (this.m_DropItem.isConceptCard == null)
            {
                goto Label_0088;
            }
            DataSource.Bind<ConceptCardParam>(base.get_gameObject(), this.m_DropItem.conceptCardParam);
        Label_0088:
            if (this.m_DropItem.is_secret == null)
            {
                goto Label_00BC;
            }
            icon = base.get_gameObject().GetComponent<DropItemIcon>();
            if ((icon != null) == null)
            {
                goto Label_00BC;
            }
            icon.IsSecret = 1;
        Label_00BC:
            GameParameter.UpdateAll(base.get_gameObject());
            this.m_ItemIcon = base.get_gameObject().GetComponent<DropItemIcon>();
            if ((this.m_ItemIcon != null) == null)
            {
                goto Label_0109;
            }
            this.m_ItemIcon.Num.set_text(&this.m_DropItem.num.ToString());
        Label_0109:
            base.get_transform().set_localScale(new Vector3(0.3f, 0.3f, 1f));
            base.get_transform().set_position(new Vector3(&base.get_transform().get_position().x, &base.get_transform().get_position().y + 25f, &base.get_transform().get_position().z));
            this.m_EndAnimator = obj2.GetComponent<Animator>();
            this.m_State = 2;
            return;
        }

        private void Update()
        {
            State state;
            switch ((this.m_State - 1))
            {
                case 0:
                    goto Label_002C;

                case 1:
                    goto Label_0037;

                case 2:
                    goto Label_0042;

                case 3:
                    goto Label_004D;

                case 4:
                    goto Label_0058;

                case 5:
                    goto Label_0063;
            }
            goto Label_006E;
        Label_002C:
            this.State_Setup();
            goto Label_006E;
        Label_0037:
            this.State_Open();
            goto Label_006E;
        Label_0042:
            this.State_Popup();
            goto Label_006E;
        Label_004D:
            this.State_Move();
            goto Label_006E;
        Label_0058:
            this.State_End();
            goto Label_006E;
        Label_0063:
            this.State_Delete();
        Label_006E:
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

        public SRPG.Unit.DropItem DropItem
        {
            set
            {
                this.m_DropItem = value;
                return;
            }
        }

        private enum State
        {
            NONE,
            SETUP,
            OPEN,
            POPUP,
            MOVE,
            END,
            DELETE
        }
    }
}

