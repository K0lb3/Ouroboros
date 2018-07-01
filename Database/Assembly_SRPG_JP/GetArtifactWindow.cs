// Decompiled with JetBrains decompiler
// Type: SRPG.GetArtifactWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "武具選択", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "武具更新", FlowNode.PinTypes.Output, 101)]
  public class GetArtifactWindow : SRPG_FixedList, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    private ArtifactSelectListItemData[] mArtifactListItem;

    protected virtual void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.get_activeInHierarchy())
        return;
      this.ItemTemplate.SetActive(false);
    }

    protected override void Start()
    {
    }

    public void Activated(int pinID)
    {
    }

    public void Refresh(ArtifactSelectListItemData[] data)
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      List<ArtifactData> artifactDataList = new List<ArtifactData>();
      for (int index = 0; index < data.Length; ++index)
      {
        ArtifactData artifactData = new ArtifactData();
        artifactData.Deserialize(new Json_Artifact()
        {
          iname = data[index].iname,
          rare = data[index].param.rareini
        });
        artifactDataList.Add(artifactData);
      }
      this.SetData((object[]) artifactDataList.ToArray(), typeof (ArtifactData));
      this.mArtifactListItem = data;
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    protected override GameObject CreateItem()
    {
      return (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
    }

    public override void BindData()
    {
      for (int index1 = 0; index1 < this.mItems.Count; ++index1)
      {
        int index2 = this.mPage * this.mPageSize + index1 - this.ExtraItems.Length;
        if (0 <= index2 && index2 < this.Data.Length)
        {
          DataSource.Bind(this.mItems[index1], this.mDataType, this.Data[index2]);
          DataSource.Bind(this.mItems[index1], typeof (ArtifactSelectListItemData), (object) this.mArtifactListItem[index2]);
          this.OnUpdateItem(this.mItems[index1], index2);
          this.mItems[index1].SetActive(true);
          GameParameter.UpdateAll(this.mItems[index1]);
        }
        else
          this.mItems[index1].SetActive(false);
      }
    }

    protected override void OnItemSelect(GameObject go)
    {
      GlobalVars.ArtifactListItem = DataSource.FindDataOfClass<ArtifactSelectListItemData>(go, (ArtifactSelectListItemData) null);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
