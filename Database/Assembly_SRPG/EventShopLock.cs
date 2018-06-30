namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventShopLock : MonoBehaviour
    {
        [SerializeField]
        private GameObject LockObject;

        public EventShopLock()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            Button button;
            if ((this.LockObject != null) == null)
            {
                goto Label_0029;
            }
            this.LockObject.SetActive(GlobalVars.IsEventShopOpen == 0);
        Label_0029:
            button = base.get_gameObject().GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0051;
            }
            button.set_interactable(GlobalVars.IsEventShopOpen);
        Label_0051:
            return;
        }
    }
}

