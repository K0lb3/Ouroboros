namespace SRPG
{
    using System;
    using UnityEngine;

    public class ArenaPlayerInfo : MonoBehaviour
    {
        [Space(10f)]
        public GameObject unit1;
        public GameObject unit2;
        public GameObject unit3;

        public ArenaPlayerInfo()
        {
            base..ctor();
            return;
        }

        private void OnEnable()
        {
            this.UpdateValue();
            return;
        }

        public void UpdateValue()
        {
            ArenaPlayer player;
            player = DataSource.FindDataOfClass<ArenaPlayer>(base.get_gameObject(), null);
            if (player != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            DataSource.Bind<ArenaPlayer>(this.unit1, player);
            DataSource.Bind<ArenaPlayer>(this.unit2, player);
            DataSource.Bind<ArenaPlayer>(this.unit3, player);
            this.unit1.GetComponent<UnitIcon>().UpdateValue();
            this.unit2.GetComponent<UnitIcon>().UpdateValue();
            this.unit3.GetComponent<UnitIcon>().UpdateValue();
            return;
        }
    }
}

