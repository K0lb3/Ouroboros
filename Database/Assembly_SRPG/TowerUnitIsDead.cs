namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class TowerUnitIsDead : MonoBehaviour, IGameParameter
    {
        public GameObject dead;
        public GameObject target;
        public CanvasGroup canvas_group;

        public TowerUnitIsDead()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            this.UpdateValue();
            return;
        }

        public void UpdateValue()
        {
            TowerResuponse resuponse;
            TowerResuponse.PlayerUnit unit;
            <UpdateValue>c__AnonStorey3B1 storeyb;
            storeyb = new <UpdateValue>c__AnonStorey3B1();
            this.dead.SetActive(0);
            storeyb.data = DataSource.FindDataOfClass<UnitData>(this.target, null);
            if (storeyb.data == null)
            {
                goto Label_0077;
            }
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            if (resuponse.pdeck == null)
            {
                goto Label_0077;
            }
            unit = resuponse.pdeck.Find(new Predicate<TowerResuponse.PlayerUnit>(storeyb.<>m__430));
            this.dead.SetActive((unit == null) ? 0 : unit.isDied);
        Label_0077:
            return;
        }

        [CompilerGenerated]
        private sealed class <UpdateValue>c__AnonStorey3B1
        {
            internal UnitData data;

            public <UpdateValue>c__AnonStorey3B1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__430(TowerResuponse.PlayerUnit x)
            {
                return (this.data.UnitParam.iname == x.unitname);
            }
        }
    }
}

