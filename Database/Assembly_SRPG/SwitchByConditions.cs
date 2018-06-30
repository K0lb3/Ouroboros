namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class SwitchByConditions : MonoBehaviour
    {
        [SerializeField]
        public int lv;

        public SwitchByConditions()
        {
            this.lv = 10;
            base..ctor();
            return;
        }

        private void Start()
        {
            GameManager manager;
            if (MonoSingleton<GameManager>.Instance.Player.Lv >= this.lv)
            {
                goto Label_0028;
            }
            base.get_gameObject().SetActive(0);
        Label_0028:
            return;
        }
    }
}

