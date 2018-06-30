namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class MultiPlayDebug : MonoBehaviour
    {
        public GameObject debuginfo;
        public Button m_DebugBtn;

        public MultiPlayDebug()
        {
            base..ctor();
            return;
        }

        private void OnClick()
        {
        }

        private void OnDestroy()
        {
        }

        private void Start()
        {
            this.m_DebugBtn.get_gameObject().SetActive(0);
            return;
        }
    }
}

