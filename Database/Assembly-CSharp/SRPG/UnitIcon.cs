// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitIcon : BaseIcon
  {
    private bool mIsLvActive = true;
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
    public SortBadge SortBadge;
    public bool AllowJobChange;

    public override bool HasTooltip
    {
      get
      {
        return this.Tooltip;
      }
    }

    private void Awake()
    {
    }

    private void OnEnable()
    {
      this.UpdateValue();
    }

    private void OnDisable()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Inequality((Object) instanceDirect, (Object) null) || !Object.op_Inequality((Object) this.Icon, (Object) null))
        return;
      instanceDirect.CancelTextureLoadRequest(this.Icon);
    }

    protected virtual UnitData GetInstanceData()
    {
      return this.InstanceType.GetInstanceData(((Component) this).get_gameObject());
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      this.UpdatePartyWindow();
      UnitData instanceData = this.GetInstanceData();
      if (instanceData == null)
        return;
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(((Component) this).get_gameObject(), PlayerPartyTypes.Max);
      GameObject root = (GameObject) Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>("UI/UnitTooltip_1"));
      UnitData data = new UnitData();
      data.Setup(instanceData);
      data.TempFlags = instanceData.TempFlags;
      DataSource.Bind<UnitData>(root, data);
      DataSource.Bind<PlayerPartyTypes>(root, dataOfClass);
      UnitJobDropdown componentInChildren1 = (UnitJobDropdown) root.GetComponentInChildren<UnitJobDropdown>();
      if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
      {
        bool flag = (instanceData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        ((Component) componentInChildren1).get_gameObject().SetActive(true);
        componentInChildren1.UpdateValue = new UnitJobDropdown.ParentObjectEvent(this.UpdateValue);
        Selectable component1 = (Selectable) ((Component) componentInChildren1).get_gameObject().GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.set_interactable(flag);
        Image component2 = (Image) ((Component) componentInChildren1).get_gameObject().GetComponent<Image>();
        if (Object.op_Inequality((Object) component2, (Object) null))
          ((Graphic) component2).set_color(!flag ? new Color(0.5f, 0.5f, 0.5f) : Color.get_white());
      }
      ArtifactSlots componentInChildren2 = (ArtifactSlots) root.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = (AbilitySlots) root.GetComponentInChildren<AbilitySlots>();
      if (Object.op_Inequality((Object) componentInChildren2, (Object) null) && Object.op_Inequality((Object) componentInChildren3, (Object) null))
      {
        bool enable = (instanceData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren2.Refresh(enable);
        componentInChildren3.Refresh(enable);
      }
      GameParameter.UpdateAll(root);
    }

    public override void UpdateValue()
    {
      GameSettings instance = GameSettings.Instance;
      UnitData instanceData = this.GetInstanceData();
      if (Object.op_Inequality((Object) this.Icon, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, instanceData == null ? (string) null : AssetPath.UnitSkinIconSmall(instanceData.UnitParam, instanceData.GetSelectedSkin(-1), instanceData.CurrentJobId));
      if (Object.op_Inequality((Object) this.LvParent, (Object) null))
        this.LvParent.SetActive(this.mIsLvActive);
      if (Object.op_Inequality((Object) this.Level, (Object) null))
      {
        if (instanceData != null)
        {
          this.Level.set_text(instanceData.Lv.ToString());
          ((Component) this.Level).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.Level).get_gameObject().SetActive(false);
      }
      if (Object.op_Inequality((Object) this.Rarity, (Object) null) && Object.op_Inequality((Object) instance, (Object) null) && instance.UnitIcon_Rarity.Length > 0)
      {
        if (instanceData != null)
        {
          int index = 0;
          if (instanceData.CurrentJob != null)
            index = Mathf.Clamp(instanceData.Rarity, 0, instance.UnitIcon_Rarity.Length - 1);
          this.Rarity.set_sprite(instance.UnitIcon_Rarity[index]);
        }
        else
          this.Rarity.set_sprite((Sprite) null);
      }
      if (Object.op_Inequality((Object) this.Frame, (Object) null) && Object.op_Inequality((Object) instance, (Object) null) && instance.UnitIcon_Frames.Length > 0)
      {
        if (instanceData != null)
        {
          int index = 0;
          if (instanceData.CurrentJob != null)
            index = Mathf.Clamp(instanceData.CurrentJob.Rank, 0, instance.UnitIcon_Frames.Length - 1);
          this.Frame.set_sprite(instance.UnitIcon_Frames[index]);
        }
        else
          this.Frame.set_sprite((Sprite) null);
      }
      if (Object.op_Inequality((Object) this.Element, (Object) null) && Object.op_Inequality((Object) instance, (Object) null))
      {
        if (instanceData != null && EElement.None <= instanceData.Element && instanceData.Element < (EElement) instance.Elements_IconSmall.Length)
          this.Element.set_sprite(instance.Elements_IconSmall[(int) instanceData.Element]);
        else
          this.Element.set_sprite((Sprite) null);
      }
      if (Object.op_Inequality((Object) this.Job, (Object) null))
      {
        JobParam job = (JobParam) null;
        if (instanceData != null && instanceData.CurrentJob != null)
          job = instanceData.CurrentJob.Param;
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Job, job == null ? (string) null : AssetPath.JobIconSmall(job));
      }
      if (!MonoSingleton<GameManager>.Instance.IsTutorial() || instanceData == null || (!(MonoSingleton<GameManager>.Instance.GetNextTutorialStep() == "ShowUnitList") || !(instanceData.UnitID == "UN_V2_LOGI")))
        return;
      SGHighlightObject.Instance().highlightedObject = ((Component) this).get_gameObject();
      SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.017", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomLeft, true, false, false);
    }

    public void SetSortValue(GameUtility.UnitSortModes mode, int value, bool isLevelActive = true)
    {
      if (!Object.op_Inequality((Object) this.SortBadge, (Object) null))
        return;
      if (mode != GameUtility.UnitSortModes.Level && mode != GameUtility.UnitSortModes.Rarity && mode != GameUtility.UnitSortModes.Time)
      {
        if (Object.op_Inequality((Object) this.SortBadge.Value, (Object) null))
          this.SortBadge.Value.set_text(value.ToString());
        if (Object.op_Inequality((Object) this.SortBadge.Icon, (Object) null))
          this.SortBadge.Icon.set_sprite(GameSettings.Instance.GetUnitSortModeIcon(mode));
        ((Component) this.SortBadge).get_gameObject().SetActive(true);
        this.mIsLvActive = isLevelActive;
      }
      else
      {
        ((Component) this.SortBadge).get_gameObject().SetActive(false);
        this.mIsLvActive = true;
      }
    }

    public void ClearSortValue()
    {
      if (!Object.op_Inequality((Object) this.SortBadge, (Object) null))
        return;
      ((Component) this.SortBadge).get_gameObject().SetActive(false);
      this.mIsLvActive = true;
    }

    public void UpdatePartyWindow()
    {
      PartyWindow2 componentInParent = (PartyWindow2) ((Component) this).GetComponentInParent<PartyWindow2>();
      if (!Object.op_Inequality((Object) componentInParent, (Object) null))
        return;
      componentInParent.Refresh(true);
    }
  }
}
