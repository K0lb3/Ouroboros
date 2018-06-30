namespace SRPG
{
    using System;
    using UnityEngine;

    public class NewBadge : MonoBehaviour
    {
        [SerializeField]
        private GameObject BadgeObject;
        [SerializeField]
        public NewBadgeType SelectBadgeType;

        public NewBadge()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            NewBadgeParam param;
            bool flag;
            NewBadgeType type;
            if ((this.BadgeObject == null) == null)
            {
                goto Label_001D;
            }
            this.BadgeObject = base.get_gameObject();
        Label_001D:
            param = DataSource.FindDataOfClass<NewBadgeParam>(this.BadgeObject, null);
            if (param != null)
            {
                goto Label_0031;
            }
            return;
        Label_0031:
            if (param.use_newflag == null)
            {
                goto Label_004E;
            }
            base.get_gameObject().SetActive(param.is_new);
            return;
        Label_004E:
            flag = GameObjectExtensions.GetActive(base.get_gameObject());
            switch (param.type)
            {
                case 0:
                    goto Label_007C;

                case 1:
                    goto Label_007C;

                case 2:
                    goto Label_007C;

                case 3:
                    goto Label_007C;
            }
        Label_007C:;
        Label_0081:
            base.get_gameObject().SetActive(flag);
            return;
        }

        private void Update()
        {
        }
    }
}

