namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitQueue : MonoBehaviour
    {
        public static UnitQueue Instance;
        public GameObject[] Items;
        public Layer[] Units;
        public Button[] UnitButtons;
        public GameObject LastUnit;
        private Unit[] mCurrentUnits;

        public UnitQueue()
        {
            this.Items = new GameObject[0];
            this.Units = new Layer[0];
            this.UnitButtons = new Button[0];
            base..ctor();
            return;
        }

        private void OnDisable()
        {
            if ((Instance == this) == null)
            {
                goto Label_0016;
            }
            Instance = null;
        Label_0016:
            return;
        }

        private void OnEnable()
        {
            if ((Instance == null) == null)
            {
                goto Label_0016;
            }
            Instance = this;
        Label_0016:
            return;
        }

        public unsafe void Refresh(Unit unit)
        {
            int num;
            int num2;
            num = 0;
            goto Label_00C5;
        Label_0007:
            if (this.mCurrentUnits[num] != unit)
            {
                goto Label_00C1;
            }
            if (num >= ((int) this.Items.Length))
            {
                goto Label_0043;
            }
            if ((this.Items[num] != null) == null)
            {
                goto Label_0043;
            }
            GameParameter.UpdateAll(this.Items[num]);
        Label_0043:
            if (num >= ((int) this.Units.Length))
            {
                goto Label_00C1;
            }
            if (&(this.Units[num]).Layers == null)
            {
                goto Label_00C1;
            }
            num2 = 0;
            goto Label_00A8;
        Label_006E:
            if ((&(this.Units[num]).Layers[num2] != null) == null)
            {
                goto Label_00A4;
            }
            GameParameter.UpdateAll(&(this.Units[num]).Layers[num2]);
        Label_00A4:
            num2 += 1;
        Label_00A8:
            if (num2 < ((int) &(this.Units[num]).Layers.Length))
            {
                goto Label_006E;
            }
        Label_00C1:
            num += 1;
        Label_00C5:
            if (num < ((int) this.mCurrentUnits.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void Refresh(int offset)
        {
            SceneBattle battle;
            BattleCore core;
            int num;
            int num2;
            BattleCore.OrderData data;
            Unit unit;
            int num3;
            battle = SceneBattle.Instance;
            if ((battle == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            core = battle.Battle;
            if (core != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if (core.Order.Count != null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            num = Mathf.Max((int) this.Items.Length, (int) this.Units.Length);
            num2 = 0;
            goto Label_0168;
        Label_004F:
            data = core.Order[num2 % core.Order.Count];
            unit = data.Unit;
            if (num2 >= ((int) this.mCurrentUnits.Length))
            {
                goto Label_008A;
            }
            this.mCurrentUnits[num2] = unit;
        Label_008A:
            if (num2 >= ((int) this.Items.Length))
            {
                goto Label_00D6;
            }
            if ((this.Items[num2] != null) == null)
            {
                goto Label_00D6;
            }
            DataSource.Bind<Unit>(this.Items[num2], unit);
            DataSource.Bind<BattleCore.OrderData>(this.Items[num2], data);
            GameParameter.UpdateAll(this.Items[num2]);
        Label_00D6:
            if (num2 >= ((int) this.Units.Length))
            {
                goto Label_0164;
            }
            num3 = 0;
            goto Label_014A;
        Label_00EC:
            if ((&(this.Units[num2]).Layers[num3] == null) == null)
            {
                goto Label_0110;
            }
            goto Label_0144;
        Label_0110:
            DataSource.Bind<Unit>(&(this.Units[num2]).Layers[num3], unit);
            GameParameter.UpdateAll(&(this.Units[num2]).Layers[num3]);
        Label_0144:
            num3 += 1;
        Label_014A:
            if (num3 < ((int) &(this.Units[num2]).Layers.Length))
            {
                goto Label_00EC;
            }
        Label_0164:
            num2 += 1;
        Label_0168:
            if (num2 < num)
            {
                goto Label_004F;
            }
            if ((this.LastUnit != null) == null)
            {
                goto Label_01BE;
            }
            DataSource.Bind<Unit>(this.LastUnit, core.Order[0].Unit);
            DataSource.Bind<BattleCore.OrderData>(this.LastUnit, core.Order[0]);
            GameParameter.UpdateAll(this.LastUnit);
        Label_01BE:
            return;
        }

        private void Start()
        {
            this.mCurrentUnits = new Unit[Mathf.Max((int) this.Items.Length, (int) this.Units.Length)];
            return;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct Layer
        {
            public GameObject[] Layers;
        }
    }
}

