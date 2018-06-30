namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Selectable))]
    public class LimitedShopLock : MonoBehaviour
    {
        [SerializeField]
        private GameObject LockObject;
        private Button mButton;

        public LimitedShopLock()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            Button button;
            button = base.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_001A;
            }
            this.mButton = button;
        Label_001A:
            return;
        }

        private void Start()
        {
            this.UpdateLockState();
            return;
        }

        private void UpdateLockState()
        {
            if ((this.mButton == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.LockObject.SetActive(this.mButton.get_interactable() == 0);
            return;
        }
    }
}

