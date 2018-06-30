namespace SRPG
{
    using System;
    using UnityEngine;

    public class TowerEnemyListItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject DisableIcon;
        [SerializeField]
        private CanvasGroup mCanvasGroup;

        public TowerEnemyListItem()
        {
            base..ctor();
            return;
        }

        private void Refresh()
        {
            Unit unit;
            unit = DataSource.FindDataOfClass<Unit>(base.get_gameObject(), null);
            if (unit != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            if (unit.IsDead == null)
            {
                goto Label_0034;
            }
            this.mCanvasGroup.set_alpha(0.5f);
            goto Label_0044;
        Label_0034:
            this.mCanvasGroup.set_alpha(1f);
        Label_0044:
            return;
        }

        private void Start()
        {
            this.UpdateValue();
            this.Refresh();
            if ((this.DisableIcon != null) == null)
            {
                goto Label_0029;
            }
            this.DisableIcon.SetActive(0);
        Label_0029:
            return;
        }

        private void Update()
        {
        }

        public void UpdateValue()
        {
            this.Refresh();
            return;
        }
    }
}

