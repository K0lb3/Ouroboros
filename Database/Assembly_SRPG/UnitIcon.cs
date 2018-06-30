namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitIcon : BaseIcon
    {
        private const string TooltipPath = "UI/UnitTooltip_1";
        [Space(10f)]
        public GameParameter.UnitInstanceTypes InstanceType;
        public int InstanceIndex;
        public bool Tooltip;
        [Space(10f)]
        public RawImage Icon;
        public Image Frame;
        public Image Rarity;
        public Text Level;
        public Image Element;
        public RawImage Job;
        public GameObject LvParent;
        public SRPG.SortBadge SortBadge;
        public bool AllowJobChange;
        private bool mIsLvActive;

        public UnitIcon()
        {
            this.mIsLvActive = 1;
            base..ctor();
            return;
        }

        private void Awake()
        {
        }

        public void ClearSortValue()
        {
            if ((this.SortBadge != null) == null)
            {
                goto Label_0029;
            }
            this.SortBadge.get_gameObject().SetActive(0);
            this.mIsLvActive = 1;
        Label_0029:
            return;
        }

        protected virtual UnitData GetInstanceData()
        {
            UnitData data;
            return SRPG_Extensions.GetInstanceData(this.InstanceType, base.get_gameObject());
        }

        private void OnDisable()
        {
            GameManager manager;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_002F;
            }
            if ((this.Icon != null) == null)
            {
                goto Label_002F;
            }
            manager.CancelTextureLoadRequest(this.Icon);
        Label_002F:
            return;
        }

        private void OnEnable()
        {
            this.UpdateValue();
            return;
        }

        public unsafe void SetSortValue(GameUtility.UnitSortModes mode, int value, bool isLevelActive)
        {
            if ((this.SortBadge != null) == null)
            {
                goto Label_00B9;
            }
            if (mode == 1)
            {
                goto Label_00A1;
            }
            if (mode == 13)
            {
                goto Label_00A1;
            }
            if (mode == null)
            {
                goto Label_00A1;
            }
            if ((this.SortBadge.Value != null) == null)
            {
                goto Label_0053;
            }
            this.SortBadge.Value.set_text(&value.ToString());
        Label_0053:
            if ((this.SortBadge.Icon != null) == null)
            {
                goto Label_0084;
            }
            this.SortBadge.Icon.set_sprite(GameSettings.Instance.GetUnitSortModeIcon(mode));
        Label_0084:
            this.SortBadge.get_gameObject().SetActive(1);
            this.mIsLvActive = isLevelActive;
            goto Label_00B9;
        Label_00A1:
            this.SortBadge.get_gameObject().SetActive(0);
            this.mIsLvActive = 1;
        Label_00B9:
            return;
        }

        protected override void ShowTooltip(Vector2 screen)
        {
            UnitData data;
            if (this.Tooltip == null)
            {
                goto Label_003D;
            }
            this.UpdatePartyWindow();
            data = this.GetInstanceData();
            if (data == null)
            {
                goto Label_003D;
            }
            data.ShowTooltip(base.get_gameObject(), this.AllowJobChange, new UnitJobDropdown.ParentObjectEvent(this.UpdateValue));
        Label_003D:
            return;
        }

        public void UpdatePartyWindow()
        {
            PartyWindow2 window;
            window = base.GetComponentInParent<PartyWindow2>();
            if ((window != null) == null)
            {
                goto Label_001A;
            }
            window.Refresh(1);
        Label_001A:
            return;
        }

        public override unsafe void UpdateValue()
        {
            GameSettings settings;
            UnitData data;
            int num;
            int num2;
            JobParam param;
            int num3;
            settings = GameSettings.Instance;
            data = this.GetInstanceData();
            if ((this.Icon != null) == null)
            {
                goto Label_0052;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, (data == null) ? null : AssetPath.UnitSkinIconSmall(data.UnitParam, data.GetSelectedSkin(-1), data.CurrentJobId));
        Label_0052:
            if ((this.LvParent != null) == null)
            {
                goto Label_0074;
            }
            this.LvParent.SetActive(this.mIsLvActive);
        Label_0074:
            if ((this.Level != null) == null)
            {
                goto Label_00CC;
            }
            if (data == null)
            {
                goto Label_00BB;
            }
            this.Level.set_text(&data.Lv.ToString());
            this.Level.get_gameObject().SetActive(1);
            goto Label_00CC;
        Label_00BB:
            this.Level.get_gameObject().SetActive(0);
        Label_00CC:
            if ((((this.Rarity != null) == null) || ((settings != null) == null)) || (((int) settings.UnitIcon_Rarity.Length) <= 0))
            {
                goto Label_0145;
            }
            if (data == null)
            {
                goto Label_0139;
            }
            num = 0;
            if (data.CurrentJob == null)
            {
                goto Label_0121;
            }
            num = Mathf.Clamp(data.Rarity, 0, ((int) settings.UnitIcon_Rarity.Length) - 1);
        Label_0121:
            this.Rarity.set_sprite(settings.UnitIcon_Rarity[num]);
            goto Label_0145;
        Label_0139:
            this.Rarity.set_sprite(null);
        Label_0145:
            if ((((this.Frame != null) == null) || ((settings != null) == null)) || (((int) settings.UnitIcon_Frames.Length) <= 0))
            {
                goto Label_01C3;
            }
            if (data == null)
            {
                goto Label_01B7;
            }
            num2 = 0;
            if (data.CurrentJob == null)
            {
                goto Label_019F;
            }
            num2 = Mathf.Clamp(data.CurrentJob.Rank, 0, ((int) settings.UnitIcon_Frames.Length) - 1);
        Label_019F:
            this.Frame.set_sprite(settings.UnitIcon_Frames[num2]);
            goto Label_01C3;
        Label_01B7:
            this.Frame.set_sprite(null);
        Label_01C3:
            if (((this.Element != null) == null) || ((settings != null) == null))
            {
                goto Label_022E;
            }
            if (((data == null) || (0 > data.Element)) || (data.Element >= ((int) settings.Elements_IconSmall.Length)))
            {
                goto Label_0222;
            }
            this.Element.set_sprite(settings.Elements_IconSmall[data.Element]);
            goto Label_022E;
        Label_0222:
            this.Element.set_sprite(null);
        Label_022E:
            if ((this.Job != null) == null)
            {
                goto Label_0284;
            }
            param = null;
            if ((data == null) || (data.CurrentJob == null))
            {
                goto Label_0260;
            }
            param = data.CurrentJob.Param;
        Label_0260:
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Job, (param == null) ? null : AssetPath.JobIconSmall(param));
        Label_0284:
            return;
        }

        public override bool HasTooltip
        {
            get
            {
                return this.Tooltip;
            }
        }
    }
}

