namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuestCampaignIcon : MonoBehaviour
    {
        public UnityEngine.UI.Text Text;
        public Image UnitIcon;

        public QuestCampaignIcon()
        {
            base..ctor();
            return;
        }

        private bool SetUnitIcon(QuestCampaignData data)
        {
            GameManager manager;
            UnitParam param;
            SpriteSheet sheet;
            ItemParam param2;
            if (string.IsNullOrEmpty(data.unit) == null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            param = manager.GetUnitParam(data.unit);
            if (param == null)
            {
                goto Label_006D;
            }
            sheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
            param2 = manager.GetItemParam(param.piece);
            if ((this.UnitIcon != null) == null)
            {
                goto Label_006B;
            }
            this.UnitIcon.set_sprite(sheet.GetSprite(param2.icon));
        Label_006B:
            return 1;
        Label_006D:
            return 0;
        }

        private void Start()
        {
            QuestCampaignData data;
            data = DataSource.FindDataOfClass<QuestCampaignData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0020;
            }
            base.get_gameObject().SetActive(0);
            return;
        Label_0020:
            if ((this.UnitIcon != null) == null)
            {
                goto Label_004E;
            }
            if (this.SetUnitIcon(data) != null)
            {
                goto Label_004E;
            }
            this.UnitIcon.get_gameObject().SetActive(0);
        Label_004E:
            if ((this.Text != null) == null)
            {
                goto Label_0076;
            }
            this.Text.set_text(this.ValueToString(data.value));
        Label_0076:
            return;
        }

        private unsafe void ValueToFraction(int value, out int num, out int denom)
        {
            float num2;
            int num3;
            float num4;
            int num5;
            int num6;
            int num7;
            float num8;
            float num9;
            num2 = ((float) value) / 100f;
            num3 = 10;
            num4 = 1E-06f;
            num5 = 1;
            goto Label_0068;
        Label_0019:
            num6 = (int) (num2 * ((float) num5));
            num7 = num6 + 1;
            num8 = ((float) num6) / ((float) num5);
            num9 = ((float) num7) / ((float) num5);
            if (Mathf.Abs(num8 - num2) >= num4)
            {
                goto Label_004D;
            }
            *((int*) num) = num6;
            *((int*) denom) = num5;
            return;
        Label_004D:
            if (Mathf.Abs(num9 - num2) >= num4)
            {
                goto Label_0064;
            }
            *((int*) num) = num7;
            *((int*) denom) = num5;
            return;
        Label_0064:
            num5 += 1;
        Label_0068:
            if (num5 <= num3)
            {
                goto Label_0019;
            }
            *((int*) num) = 1;
            *((int*) denom) = 1;
            return;
        }

        private unsafe string ValueToString(int value)
        {
            int num;
            int num2;
            float num3;
            float num4;
            if (value >= 100)
            {
                goto Label_002A;
            }
            this.ValueToFraction(value, &num, &num2);
            return string.Format("{0}/{1}", (int) num, (int) num2);
        Label_002A:
            if ((value % 100) != null)
            {
                goto Label_0050;
            }
            return string.Format("{0}", (int) Mathf.FloorToInt(((float) value) / 100f));
        Label_0050:
            num3 = (float) Mathf.FloorToInt(((float) value) / 100f);
            num4 = (float) Mathf.FloorToInt(((float) (value % 100)) / 10f);
            return string.Format("{0}.{1}", (float) num3, (float) num4);
        }
    }
}

