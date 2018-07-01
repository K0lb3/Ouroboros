// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSkinSelectWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "選択確認：決定", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(105, "閉じる：完了", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(104, "全ジョブ設定：完了", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(2, "選択確認：取消", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "外す", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "全ジョブ設定", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(102, "取消：完了", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "外す：完了", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(5, "閉じる", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(101, "決定：完了", FlowNode.PinTypes.Output, 101)]
  public class UnitSkinSelectWindow : SRPG_ListBase, IFlowInterface
  {
    private List<GameObject> mSkins = new List<GameObject>();
    public GameObject ListItemTemplate;
    public GameObject SelectConfirmTemplate;
    public GameObject SettingOverlay;
    public GameObject PointingOverlay;
    public UnitSkinSelectWindow.SkinSelectEvent OnSkinSelect;
    public UnitSkinSelectWindow.SkinDecideEvent OnSkinDecide;
    public UnitSkinSelectWindow.SkinDecideEvent OnSkinDecideAll;
    public GameObject RemoveButton;
    public Text RemoveName;
    public UnitSkinSelectWindow.SkinRemoveEvent OnSkinRemove;
    public UnitSkinSelectWindow.SkinRemoveEvent OnSkinRemoveAll;
    public SRPG_Button DecideButton;
    public UnitSkinSelectWindow.SkinSelectEvent OnDecide;
    public UnitSkinSelectWindow.SkinCloseEvent OnSkinClose;
    public bool IsViewOnly;
    private UnitData mCurrentUnit;
    private GameObject mPointingItem;
    private GameObject mDecidedItem;
    private ArtifactParam mConfirmSkin;
    private GameObject mDecidedOverlay;
    private GameObject mPointingOverlay;
    public Text TitleSkinName;
    public Text TitleSkinDesc;

    protected override void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItemTemplate, (UnityEngine.Object) null))
        this.ListItemTemplate.SetActive(false);
      this.mCurrentUnit = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SettingOverlay, (UnityEngine.Object) null))
      {
        this.mDecidedOverlay = this.SettingOverlay;
        this.mDecidedOverlay.get_transform().SetParent(this.SettingOverlay.get_transform().get_parent(), false);
        this.mDecidedOverlay.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PointingOverlay, (UnityEngine.Object) null))
      {
        this.mPointingOverlay = this.PointingOverlay;
        this.mPointingOverlay.get_transform().SetParent(this.SettingOverlay.get_transform().get_parent(), false);
        this.mPointingOverlay.SetActive(false);
      }
      this.DecideButtonInteractive(false);
      ArtifactParam[] allSkins = this.mCurrentUnit.GetAllSkins(-1);
      ArtifactParam[] selectableSkins = this.mCurrentUnit.GetSelectableSkins(-1);
      ArtifactParam selectedSkinData = this.mCurrentUnit.GetSelectedSkinData(-1);
      UnitData data1 = new UnitData();
      data1.Setup(this.mCurrentUnit);
      data1.ResetJobSkinAll();
      DataSource.Bind<UnitData>(this.RemoveButton, data1);
      UnitSkinListItem component1 = (UnitSkinListItem) this.RemoveButton.GetComponent<UnitSkinListItem>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
      {
        component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        component1.OnSelectAll = new ListItemEvents.ListItemEvent(this.OnSelectAll);
      }
      if (selectedSkinData == null)
      {
        this.SetPointingOverLay(this.RemoveButton);
        this.SetDecidedOverlay(this.RemoveButton);
      }
      this.RemoveName.set_text(LocalizedText.Get("sys.UNITLIST_SKIN_DEFAULT_NAME", new object[1]
      {
        (object) this.mCurrentUnit.UnitParam.name
      }));
      Array.Reverse((Array) allSkins);
      for (int index = 0; index < allSkins.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitSkinSelectWindow.\u003CStart\u003Ec__AnonStorey3A0 startCAnonStorey3A0 = new UnitSkinSelectWindow.\u003CStart\u003Ec__AnonStorey3A0();
        // ISSUE: reference to a compiler-generated field
        startCAnonStorey3A0.skin = allSkins[index];
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ListItemTemplate);
        gameObject.SetActive(true);
        gameObject.get_transform().SetParent(((Component) this).get_gameObject().get_transform(), false);
        // ISSUE: reference to a compiler-generated field
        DataSource.Bind<ArtifactParam>(gameObject, startCAnonStorey3A0.skin);
        bool active = true;
        // ISSUE: reference to a compiler-generated method
        if (selectableSkins == null || Array.Find<ArtifactParam>(selectableSkins, new Predicate<ArtifactParam>(startCAnonStorey3A0.\u003C\u003Em__46C)) == null)
          active = false;
        this.SetActiveListItem(gameObject, active);
        UnitData data2 = new UnitData();
        data2.Setup(this.mCurrentUnit);
        // ISSUE: reference to a compiler-generated field
        data2.SetJobSkin(startCAnonStorey3A0.skin.iname, this.mCurrentUnit.JobIndex);
        DataSource.Bind<UnitData>(gameObject, data2);
        UnitSkinListItem component2 = (UnitSkinListItem) gameObject.GetComponent<UnitSkinListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        {
          component2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
          component2.OnSelectAll = new ListItemEvents.ListItemEvent(this.OnSelectAll);
          // ISSUE: reference to a compiler-generated field
          if (selectedSkinData != null && startCAnonStorey3A0.skin.iname == selectedSkinData.iname)
          {
            this.SetPointingOverLay(gameObject);
            this.SetDecidedOverlay(gameObject);
          }
        }
        this.mSkins.Add(gameObject);
      }
    }

    protected override void OnDestroy()
    {
      if (this.OnSkinSelect == null || this.OnSkinSelect.Target == null || (!(this.OnSkinSelect.Target is UnityEngine.Object) || this.OnSkinSelect.Target.Equals((object) null)))
        return;
      this.OnSelectAll(this.mDecidedItem);
    }

    private void SetActiveListItem(GameObject listItem, bool active)
    {
      UnitSkinListItem component = (UnitSkinListItem) listItem.GetComponent<UnitSkinListItem>();
      ((Selectable) component.Button).set_interactable(active);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Lock, (UnityEngine.Object) null))
        return;
      component.Lock.SetActive(!active);
    }

    private void SetDecidedOverlay(GameObject parent)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDecidedOverlay, (UnityEngine.Object) null))
      {
        this.mDecidedOverlay.SetActive(true);
        this.mDecidedOverlay.get_transform().SetParent(parent.get_transform(), false);
        this.mDecidedOverlay.get_transform().SetAsLastSibling();
      }
      this.mDecidedItem = parent;
    }

    private void SetPointingOverLay(GameObject parent)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPointingOverlay, (UnityEngine.Object) null))
      {
        this.mPointingOverlay.SetActive(true);
        this.mPointingOverlay.get_transform().SetParent(parent.get_transform(), false);
        this.mPointingOverlay.get_transform().SetAsLastSibling();
      }
      this.mPointingItem = parent;
    }

    private void DecideButtonInteractive(bool interactive)
    {
      SRPG_Button component = (SRPG_Button) ((Component) this.DecideButton).get_gameObject().GetComponent<SRPG_Button>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      ((Selectable) component).set_interactable(interactive);
    }

    private void Refresh()
    {
      if (this.mCurrentUnit == null || this.mCurrentUnit.UnitParam.skins == null || this.mCurrentUnit.UnitParam.skins.Length >= 1)
        ;
    }

    private void OnSelect(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPointingOverlay, (UnityEngine.Object) null))
        return;
      this.mPointingOverlay.get_gameObject().SetActive(true);
      this.mPointingOverlay.get_transform().SetParent(go.get_transform(), false);
      this.mPointingOverlay.get_transform().SetAsLastSibling();
    }

    private void OnSelectAll(GameObject go)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDecidedItem, (UnityEngine.Object) go))
        this.DecideButtonInteractive(false);
      else
        this.DecideButtonInteractive(!this.IsViewOnly);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPointingItem, (UnityEngine.Object) go))
        return;
      ArtifactParam dataOfClass = DataSource.FindDataOfClass<ArtifactParam>(go, (ArtifactParam) null);
      if (dataOfClass != null)
      {
        this.TitleSkinName.set_text(dataOfClass.name);
        this.TitleSkinDesc.set_text(dataOfClass.expr);
      }
      if (this.OnSkinSelect != null)
        this.OnSkinSelect(dataOfClass);
      this.SetPointingOverLay(go);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDecidedItem, (UnityEngine.Object) go))
        this.SetDecidedOverlay(this.mDecidedItem);
      this.mPointingItem = go;
    }

    private void OnRemoveSelect(GameObject go)
    {
      this.Activated(3);
    }

    private void OnRemoveAll(GameObject go)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPointingOverlay, (UnityEngine.Object) null))
      {
        this.mPointingOverlay.get_transform().SetParent(go.get_transform(), false);
        this.mPointingOverlay.get_transform().SetAsLastSibling();
        this.mPointingOverlay.get_gameObject().SetActive(true);
      }
      this.mPointingItem = go;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.mCurrentUnit != null && this.mConfirmSkin != null && !string.IsNullOrEmpty(this.mConfirmSkin.iname))
          {
            this.mCurrentUnit.SetJobSkin(this.mConfirmSkin.iname, -1);
            this.Refresh();
            if (this.OnSkinDecide != null)
              this.OnSkinDecide(this.mConfirmSkin);
          }
          this.mConfirmSkin = (ArtifactParam) null;
          break;
        case 2:
          this.mConfirmSkin = (ArtifactParam) null;
          break;
        case 3:
          if (this.mCurrentUnit != null)
          {
            this.mCurrentUnit.ResetJobSkin(-1);
            this.Refresh();
            if (this.OnSkinRemove != null)
              this.OnSkinRemove();
          }
          this.mConfirmSkin = (ArtifactParam) null;
          break;
        case 4:
          ArtifactParam dataOfClass = DataSource.FindDataOfClass<ArtifactParam>(this.mPointingItem, (ArtifactParam) null);
          if (this.mCurrentUnit != null)
          {
            if (dataOfClass != null && !string.IsNullOrEmpty(dataOfClass.iname))
              this.mCurrentUnit.SetJobSkinAll(dataOfClass.iname);
            else
              this.mCurrentUnit.SetJobSkinAll((string) null);
            this.SetPointingOverLay(this.mPointingItem);
            this.SetDecidedOverlay(this.mPointingItem);
            this.DecideButtonInteractive(false);
            this.Refresh();
            if (this.OnSkinDecideAll != null)
              this.OnSkinDecideAll(dataOfClass);
            if (this.OnSkinClose != null)
              this.OnSkinClose();
          }
          this.mConfirmSkin = (ArtifactParam) null;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
          break;
        case 5:
          if (this.OnSkinClose != null)
            this.OnSkinClose();
          this.mConfirmSkin = (ArtifactParam) null;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
          break;
      }
    }

    public void OnClose()
    {
      this.mConfirmSkin = (ArtifactParam) null;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
    }

    public delegate void SkinSelectEvent(ArtifactParam artifact);

    public delegate void SkinDecideEvent(ArtifactParam artifact);

    public delegate void SkinRemoveEvent();

    public delegate void SkinCloseEvent();
  }
}
