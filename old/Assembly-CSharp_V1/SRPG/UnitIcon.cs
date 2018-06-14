// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIcon
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitIcon : BaseIcon
  {
    private const string TooltipPath = "UI/UnitTooltip";
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
      UnitData instanceData = this.GetInstanceData();
      if (instanceData == null)
        return;
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(((Component) this).get_gameObject(), PlayerPartyTypes.Max);
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>("UI/UnitTooltip"));
      DataSource.Bind<UnitData>(gameObject, instanceData);
      DataSource.Bind<PlayerPartyTypes>(gameObject, dataOfClass);
      UnitJobDropdown componentInChildren = (UnitJobDropdown) gameObject.GetComponentInChildren<UnitJobDropdown>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      ((Component) componentInChildren).get_gameObject().SetActive((instanceData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max);
    }

    public override void UpdateValue()
    {
      GameSettings instance = GameSettings.Instance;
      UnitData instanceData = this.GetInstanceData();
      if (Object.op_Inequality((Object) this.Icon, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, instanceData == null ? (string) null : AssetPath.UnitSkinIconSmall(instanceData.UnitParam, instanceData.GetSelectedSkin(-1), instanceData.CurrentJobId));
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
      if (!Object.op_Inequality((Object) this.Job, (Object) null))
        return;
      JobParam job = (JobParam) null;
      if (instanceData != null && instanceData.CurrentJob != null)
        job = instanceData.CurrentJob.Param;
      MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Job, job == null ? (string) null : AssetPath.JobIconSmall(job));
    }

    public void SetSortValue(GameUtility.UnitSortModes mode, int value)
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
      }
      else
        ((Component) this.SortBadge).get_gameObject().SetActive(false);
    }

    public void ClearSortValue()
    {
      if (!Object.op_Inequality((Object) this.SortBadge, (Object) null))
        return;
      ((Component) this.SortBadge).get_gameObject().SetActive(false);
    }
  }
}
