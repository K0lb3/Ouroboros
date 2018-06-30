namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class TowerHPColor : MonoBehaviour, IGameParameter
    {
        private const float BorderGreen = 1f;
        private const float BorderRed = 0.2f;
        [SerializeField]
        private Image image;
        [SerializeField]
        private Sprite ColorBlue;
        [SerializeField]
        private Sprite ColorGreen;
        [SerializeField]
        private Sprite ColorRed;

        public TowerHPColor()
        {
            base..ctor();
            return;
        }

        public void ChangeValue(int hp, int max_hp)
        {
            float num;
            num = ((float) hp) / ((float) max_hp);
            if (num < 1f)
            {
                goto Label_0027;
            }
            this.image.set_sprite(this.ColorBlue);
            goto Label_0059;
        Label_0027:
            if (num <= 0.2f)
            {
                goto Label_0048;
            }
            this.image.set_sprite(this.ColorGreen);
            goto Label_0059;
        Label_0048:
            this.image.set_sprite(this.ColorRed);
        Label_0059:
            return;
        }

        public void UpdateValue()
        {
            UnitData data;
            int num;
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            num = MonoSingleton<GameManager>.Instance.TowerResuponse.GetPlayerUnitHP(data);
            this.ChangeValue(num, data.Status.param.hp);
            return;
        }
    }
}

