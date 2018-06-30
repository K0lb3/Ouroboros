namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class TowerPartyHP : MonoBehaviour, IGameParameter
    {
        public Slider mSlider;

        public TowerPartyHP()
        {
            base..ctor();
            return;
        }

        public void Refresh()
        {
            TowerResuponse resuponse;
            int num;
            TowerResuponse.PlayerUnit unit;
            <Refresh>c__AnonStorey3AC storeyac;
            storeyac = new <Refresh>c__AnonStorey3AC();
            if ((this.mSlider == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            storeyac.UnitData = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (storeyac.UnitData != null)
            {
                goto Label_0047;
            }
            this.mSlider.get_gameObject().SetActive(0);
            return;
        Label_0047:
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            if (resuponse != null)
            {
                goto Label_006A;
            }
            this.mSlider.get_gameObject().SetActive(0);
            return;
        Label_006A:
            num = 0;
            if (resuponse.pdeck != null)
            {
                goto Label_00AC;
            }
            this.mSlider.get_gameObject().SetActive(1);
            num = storeyac.UnitData.Status.param.hp;
            this.SetSliderValue(num, num);
            return;
        Label_00AC:
            unit = resuponse.pdeck.Find(new Predicate<TowerResuponse.PlayerUnit>(storeyac.<>m__423));
            if (unit != null)
            {
                goto Label_00FF;
            }
            this.mSlider.get_gameObject().SetActive(1);
            num = storeyac.UnitData.Status.param.hp;
            this.SetSliderValue(num, num);
            return;
        Label_00FF:
            if (unit.isDied == null)
            {
                goto Label_011C;
            }
            this.mSlider.get_gameObject().SetActive(0);
            return;
        Label_011C:
            this.mSlider.get_gameObject().SetActive(1);
            num = Mathf.Max(storeyac.UnitData.Status.param.hp - unit.dmg, 1);
            this.SetSliderValue(num, storeyac.UnitData.Status.param.hp);
            return;
        }

        private void SetSliderValue(int value, int maxValue)
        {
            if ((this.mSlider != null) == null)
            {
                goto Label_003B;
            }
            this.mSlider.set_maxValue((float) maxValue);
            this.mSlider.set_minValue(0f);
            this.mSlider.set_value((float) value);
        Label_003B:
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }

        public void UpdateValue()
        {
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3AC
        {
            internal SRPG.UnitData UnitData;

            public <Refresh>c__AnonStorey3AC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__423(TowerResuponse.PlayerUnit x)
            {
                return (x.unitname == this.UnitData.UnitParam.iname);
            }
        }
    }
}

