namespace SRPG
{
    using GR;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitSlot : MonoBehaviour, IGameParameter
    {
        public Image Frame;
        public Sprite Frame_Leader;
        public Sprite Frame_Main;
        public Sprite Frame_Hero;
        public Sprite Frame_Support;
        public Sprite Frame_Sub;
        [Space(10f)]
        public Image Label;
        public Sprite Label_Leader;
        public Sprite Label_Hero;
        public Sprite Label_Support;
        public Sprite Label_Sub;
        [Space(10f)]
        public GameObject Lock;
        public GameObject Disabled;
        public GameObject Support_Empty;
        [Space(10f)]
        public GameObject OverlayImage;

        public UnitSlot()
        {
            base..ctor();
            return;
        }

        private void OnEnable()
        {
            this.UpdateValue();
            return;
        }

        private void SetSlotDisabled()
        {
            if ((this.Disabled != null) == null)
            {
                goto Label_001D;
            }
            this.Disabled.SetActive(1);
        Label_001D:
            if ((this.Label != null) == null)
            {
                goto Label_003F;
            }
            this.Label.get_gameObject().SetActive(0);
        Label_003F:
            if ((this.Lock != null) == null)
            {
                goto Label_005C;
            }
            this.Lock.SetActive(0);
        Label_005C:
            if ((this.Support_Empty != null) == null)
            {
                goto Label_0079;
            }
            this.Support_Empty.SetActive(0);
        Label_0079:
            return;
        }

        public void UpdateValue()
        {
            UnitData data;
            UnitParam param;
            bool flag;
            <UpdateValue>c__AnonStorey3DA storeyda;
            PartySlotIndex index;
            storeyda = new <UpdateValue>c__AnonStorey3DA();
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            param = DataSource.FindDataOfClass<UnitParam>(base.get_gameObject(), null);
            storeyda.slotData = DataSource.FindDataOfClass<PartySlotData>(base.get_gameObject(), null);
            if (storeyda.slotData != null)
            {
                goto Label_003E;
            }
            return;
        Label_003E:
            if ((this.Frame != null) == null)
            {
                goto Label_013A;
            }
            this.Frame.get_gameObject().SetActive(1);
            switch (storeyda.slotData.Index)
            {
                case 0:
                    goto Label_0099;

                case 1:
                    goto Label_00AF;

                case 2:
                    goto Label_00AF;

                case 3:
                    goto Label_00AF;

                case 4:
                    goto Label_00AF;

                case 5:
                    goto Label_0124;

                case 6:
                    goto Label_0124;

                case 7:
                    goto Label_010E;
            }
            goto Label_013A;
        Label_0099:
            this.Frame.set_sprite(this.Frame_Leader);
            goto Label_013A;
        Label_00AF:
            if (((storeyda.slotData.Type != 3) && (storeyda.slotData.Type != 4)) && (storeyda.slotData.Type != 5))
            {
                goto Label_00F8;
            }
            this.Frame.set_sprite(this.Frame_Hero);
            goto Label_0109;
        Label_00F8:
            this.Frame.set_sprite(this.Frame_Main);
        Label_0109:
            goto Label_013A;
        Label_010E:
            this.Frame.set_sprite(this.Frame_Support);
            goto Label_013A;
        Label_0124:
            this.Frame.set_sprite(this.Frame_Sub);
        Label_013A:
            if ((this.Label != null) == null)
            {
                goto Label_0264;
            }
            switch (storeyda.slotData.Index)
            {
                case 0:
                    goto Label_0184;

                case 1:
                    goto Label_01AB;

                case 2:
                    goto Label_01AB;

                case 3:
                    goto Label_01AB;

                case 4:
                    goto Label_01AB;

                case 5:
                    goto Label_023D;

                case 6:
                    goto Label_023D;

                case 7:
                    goto Label_0216;
            }
            goto Label_0264;
        Label_0184:
            this.Label.set_sprite(this.Label_Leader);
            this.Label.get_gameObject().SetActive(1);
            goto Label_0264;
        Label_01AB:
            if ((storeyda.slotData.Type != 3) && (storeyda.slotData.Type != 5))
            {
                goto Label_01F4;
            }
            this.Label.set_sprite(this.Label_Hero);
            this.Label.get_gameObject().SetActive(1);
            goto Label_0211;
        Label_01F4:
            this.Label.set_sprite(null);
            this.Label.get_gameObject().SetActive(0);
        Label_0211:
            goto Label_0264;
        Label_0216:
            this.Label.set_sprite(this.Label_Support);
            this.Label.get_gameObject().SetActive(1);
            goto Label_0264;
        Label_023D:
            this.Label.set_sprite(this.Label_Sub);
            this.Label.get_gameObject().SetActive(1);
        Label_0264:
            if ((this.Lock != null) == null)
            {
                goto Label_02F7;
            }
            if ((storeyda.slotData.Type != null) && (storeyda.slotData.Type != 1))
            {
                goto Label_02A7;
            }
            this.Lock.SetActive(0);
            goto Label_02F7;
        Label_02A7:
            if (((storeyda.slotData.Type != 2) && (storeyda.slotData.Type != 3)) && ((storeyda.slotData.Type != 4) && (storeyda.slotData.Type != 5)))
            {
                goto Label_02F7;
            }
            this.Lock.SetActive(1);
        Label_02F7:
            if ((this.Support_Empty != null) == null)
            {
                goto Label_0344;
            }
            if (storeyda.slotData.Index != 7)
            {
                goto Label_0338;
            }
            flag = (data != null) ? 0 : (param == null);
            this.Support_Empty.SetActive(flag);
            goto Label_0344;
        Label_0338:
            this.Support_Empty.SetActive(0);
        Label_0344:
            if (storeyda.slotData.Type != 1)
            {
                goto Label_03AF;
            }
            if (storeyda.slotData.IsSettable == null)
            {
                goto Label_03A4;
            }
            if ((this.OverlayImage != null) == null)
            {
                goto Label_0382;
            }
            this.OverlayImage.SetActive(1);
        Label_0382:
            if ((this.Disabled != null) == null)
            {
                goto Label_041E;
            }
            this.Disabled.SetActive(0);
            goto Label_03AA;
        Label_03A4:
            this.SetSlotDisabled();
        Label_03AA:
            goto Label_041E;
        Label_03AF:
            if (storeyda.slotData.Type == 2)
            {
                goto Label_03D1;
            }
            if (storeyda.slotData.Type != 3)
            {
                goto Label_0401;
            }
        Label_03D1:
            if (Enumerable.Any<UnitData>(MonoSingleton<GameManager>.Instance.Player.Units, new Func<UnitData, bool>(storeyda.<>m__47C)) != null)
            {
                goto Label_0401;
            }
            this.SetSlotDisabled();
            goto Label_041E;
        Label_0401:
            if ((this.Disabled != null) == null)
            {
                goto Label_041E;
            }
            this.Disabled.SetActive(0);
        Label_041E:
            return;
        }

        [CompilerGenerated]
        private sealed class <UpdateValue>c__AnonStorey3DA
        {
            internal PartySlotData slotData;

            public <UpdateValue>c__AnonStorey3DA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__47C(UnitData unit)
            {
                return (unit.UnitID == this.slotData.UnitName);
            }
        }
    }
}

