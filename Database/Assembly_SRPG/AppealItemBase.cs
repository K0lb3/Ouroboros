namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class AppealItemBase : MonoBehaviour
    {
        private Sprite mAppealSprite;
        public Image AppealObject;

        public AppealItemBase()
        {
            base..ctor();
            return;
        }

        protected virtual void Awake()
        {
            if ((this.AppealObject != null) == null)
            {
                goto Label_0022;
            }
            this.AppealObject.get_gameObject().SetActive(0);
        Label_0022:
            return;
        }

        protected virtual void Destroy()
        {
        }

        protected virtual void OnDestroy()
        {
        }

        protected virtual void Refresh()
        {
            if ((this.mAppealSprite == null) == null)
            {
                goto Label_0034;
            }
            if ((this.AppealObject != null) == null)
            {
                goto Label_0033;
            }
            this.AppealObject.get_gameObject().SetActive(0);
        Label_0033:
            return;
        Label_0034:
            this.AppealObject.get_gameObject().SetActive(1);
            this.AppealObject.set_sprite(this.mAppealSprite);
            return;
        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
        }

        public Sprite AppealSprite
        {
            get
            {
                return this.mAppealSprite;
            }
            set
            {
                this.mAppealSprite = value;
                return;
            }
        }
    }
}

