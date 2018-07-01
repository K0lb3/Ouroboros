// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftPopupList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Generate List", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Generated", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "Page Prev", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(3, "Page Next", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(21, "Is First Page", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(22, "Is Last Page", FlowNode.PinTypes.Output, 6)]
  public class VersusDraftPopupList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_GENERATE_LIST = 1;
    private const int PIN_INPUT_PAGE_PREV = 2;
    private const int PIN_INPUT_PAGE_NEXT = 3;
    private const int PIN_OUTPUT_GENERATED = 11;
    private const int PIN_OUTPUT_FIRST_PAGE = 21;
    private const int PIN_OUTPUT_LAST_PAGE = 22;
    private const int UNIT_COUNT_PER_PAGE = 28;
    [SerializeField]
    private Transform mUnitParentTransform;
    [SerializeField]
    private GameObject mGOUnitItem;
    [SerializeField]
    private Text mPageMaxTxt;
    [SerializeField]
    private Text mPageNowTxt;
    private List<VersusDraftUnitParam> mDraftUnitListCache;
    private List<GameObject> mUnitList;
    private int mPage;
    private int mPageMax;

    public VersusDraftPopupList()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mDraftUnitListCache = instance.GetVersusDraftUnits(instance.VSDraftId);
      this.mGOUnitItem.SetActive(false);
      this.mUnitList = new List<GameObject>();
      this.mPage = 0;
      this.mPageMax = Mathf.CeilToInt((float) this.mDraftUnitListCache.Count / 28f);
      this.mPageMaxTxt.set_text(this.mPageMax.ToString());
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.GenerateList(0);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
        case 2:
          if (this.mPage - 1 < 0)
            break;
          this.GenerateList(--this.mPage);
          break;
        case 3:
          if (this.mPage + 1 >= this.mPageMax)
            break;
          this.GenerateList(++this.mPage);
          break;
      }
    }

    private void GenerateList(int page = 0)
    {
      this.mUnitList.ForEach((Action<GameObject>) (go => UnityEngine.Object.Destroy((UnityEngine.Object) go)));
      this.mUnitList.Clear();
      for (int index1 = 0; index1 < 28; ++index1)
      {
        UnitData data = (UnitData) null;
        int index2 = index1 + page * 28;
        if (index2 < this.mDraftUnitListCache.Count)
        {
          Json_Unit jsonUnit = this.mDraftUnitListCache[index2].GetJson_Unit();
          if (jsonUnit != null)
          {
            data = new UnitData();
            data.Deserialize(jsonUnit);
          }
        }
        GameObject root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mGOUnitItem);
        DataSource.Bind<UnitData>(root, data);
        GameParameter.UpdateAll(root);
        root.get_transform().SetParent(this.mUnitParentTransform, false);
        root.SetActive(true);
        this.mUnitList.Add(root);
      }
      this.mPageNowTxt.set_text((this.mPage + 1).ToString());
      if (this.mPage - 1 < 0)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
      if (this.mPage + 1 < this.mPageMax)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 22);
    }
  }
}
