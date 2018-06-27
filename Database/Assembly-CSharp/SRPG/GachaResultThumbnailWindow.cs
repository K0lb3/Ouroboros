// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultThumbnailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(101, "ItemDetail", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "BackGachaTop", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "BackToUnitDetail", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Back Top", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Back Top", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(102, "PieceDetail", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(20, "OneMore Exec", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(21, "OneMore Exec", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(100, "UnitDetail", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "Disable UnitDetail", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(111, "UnitDetailSingle", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(103, "ArtifactDetail", FlowNode.PinTypes.Output, 103)]
  public class GachaResultThumbnailWindow : MonoBehaviour, IFlowInterface
  {
    public static readonly int CONTENT_VIEW_MAX = 10;
    public static readonly int VIEW_COUNT = 10;
    private readonly int IN_REFRESH;
    private readonly int IN_BACKTO_UNITDETAIL;
    private readonly int IN_BACK_TOP;
    private readonly int OUT_BACK_TOP;
    private readonly int IN_ONEMORE_GACHA;
    private readonly int OUT_ONEMORE_GACHA;
    private readonly int OUT_UNITDETAIL;
    private readonly int OUT_DISABLE_UNITDETAIL;
    private readonly int OUT_UNITDETAIL_SINGLE;
    private readonly int OUT_ITEM_DETAIL;
    private readonly int OUT_PIECE_DETAIL;
    private readonly int OUT_ARTIFACT_DETAIL;
    private readonly int OUT_BACK_GACHATOP;
    public GameObject ResultListTemplate;
    public GameObject UnitIconTemplate;
    public GameObject ItemIconTemplate;
    public GameObject ArtifactIconTemplate;
    public GameObject PagerObject;
    public Button PrevPageButton;
    public Button NextPageButton;
    public Text CurrentPageText;
    public Text MaxPageText;
    public GameObject OneMoreButton;
    public GameObject CostTicket;
    public GameObject CostDefault;
    public GameObject CostDefaultBG;
    private GameObject[] mResultList;
    private GameObject[] mThumbnailList;
    private int mCurrentPage;
    private int mMaxPage;
    private bool m_inialize;
    [SerializeField]
    private GameObject PagerPanel;
    [SerializeField]
    private Toggle PagerItemTemplate;
    private List<GachaDropData[]> mGachaDropSets;
    private List<GachaDropData> mCachsGachaDropDatas;
    private List<Toggle> mPagerItems;

    public GachaResultThumbnailWindow()
    {
      base.\u002Ector();
    }

    public bool IsInialize
    {
      get
      {
        return this.m_inialize;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID == this.IN_REFRESH)
        return;
      if (pinID == this.IN_BACKTO_UNITDETAIL)
      {
        if (this.CheckSingleDropForUnit())
          FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_BACK_GACHATOP);
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_DISABLE_UNITDETAIL);
      }
      else if (pinID == this.IN_BACK_TOP)
      {
        this.m_inialize = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_BACK_TOP);
      }
      else
      {
        if (pinID != this.IN_ONEMORE_GACHA)
          return;
        this.m_inialize = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_ONEMORE_GACHA);
      }
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultListTemplate, (UnityEngine.Object) null))
        this.ResultListTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitIconTemplate, (UnityEngine.Object) null))
        this.UnitIconTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemIconTemplate, (UnityEngine.Object) null))
        this.ItemIconTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactIconTemplate, (UnityEngine.Object) null))
        this.ArtifactIconTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PagerObject, (UnityEngine.Object) null))
        this.PagerObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextPageButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.NextPageButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnNextPage)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevPageButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.PrevPageButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnPrevPage)));
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
      if (GachaResultData.drops == null || GachaResultData.drops.Length <= 0)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) HomeWindow.Current, (UnityEngine.Object) null))
        HomeWindow.Current.SetVisible(true);
      this.Initalize();
    }

    private void Reset()
    {
      GameUtility.DestroyGameObjects(this.mThumbnailList);
      GameUtility.DestroyGameObjects(this.mResultList);
    }

    private void OnDestroy()
    {
      this.Reset();
    }

    private void Refresh()
    {
      this.Reset();
      if (this.CheckSingleDropForUnit())
      {
        this.OnSelectIcon(0, GachaResultThumbnailWindow.GachaResultType.Unit);
      }
      else
      {
        this.RefreshThumbnail();
        this.RefreshPagerButton();
      }
    }

    private GameObject CreateHorizontalList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ResultListTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogError(string.Empty);
        return (GameObject) null;
      }
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ResultListTemplate);
      gameObject.get_transform().SetParent(this.ResultListTemplate.get_transform().get_parent(), false);
      gameObject.SetActive(true);
      return gameObject;
    }

    private void Initalize()
    {
      if (this.IsInialize || GachaResultData.drops == null)
        return;
      this.SetGachaDropSet(GachaResultData.drops);
      for (int index = 0; index < GachaResultData.drops.Length; ++index)
      {
        if (GachaResultData.drops[index].type == GachaDropData.Type.Unit)
        {
          MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(false);
          break;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PagerObject, (UnityEngine.Object) null))
        this.PagerObject.SetActive(this.CheckIsNeedPager());
      this.InitalizePager();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OneMoreButton, (UnityEngine.Object) null))
      {
        this.RefreshGachaCostObject();
        this.OneMoreButton.SetActive(GachaResultData.UseOneMore);
      }
      this.Refresh();
      this.m_inialize = true;
    }

    private void RefreshPagerButton()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevPageButton, (UnityEngine.Object) null))
        ((Selectable) this.PrevPageButton).set_interactable(this.mCurrentPage - 1 >= 0);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextPageButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.NextPageButton).set_interactable(this.mCurrentPage + 1 < this.mMaxPage);
    }

    private void RefreshThumbnail2(int count)
    {
      if (this.mThumbnailList != null && this.mThumbnailList.Length > 0)
      {
        for (int index = 0; index < this.mThumbnailList.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mThumbnailList[index], (UnityEngine.Object) null))
            this.mThumbnailList[index].SetActive(false);
        }
      }
      Transform transform1 = this.mResultList[0].get_transform().Find("content");
      Transform transform2 = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mResultList[1], (UnityEngine.Object) null) ? this.mResultList[1].get_transform().Find("content") : (Transform) null;
      int num = Math.Min(count, GachaResultThumbnailWindow.CONTENT_VIEW_MAX);
      for (int index = 0; index < num; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GachaResultThumbnailWindow.\u003CRefreshThumbnail2\u003Ec__AnonStorey332 thumbnail2CAnonStorey332 = new GachaResultThumbnailWindow.\u003CRefreshThumbnail2\u003Ec__AnonStorey332();
        // ISSUE: reference to a compiler-generated field
        thumbnail2CAnonStorey332.\u003C\u003Ef__this = this;
        GachaDropData drop = GachaResultData.drops[index];
        // ISSUE: reference to a compiler-generated field
        thumbnail2CAnonStorey332.type = GachaResultThumbnailWindow.GachaResultType.None;
        // ISSUE: reference to a compiler-generated field
        thumbnail2CAnonStorey332.index = index;
        GameObject gameObject1 = (GameObject) null;
        if (drop.type == GachaDropData.Type.Unit)
        {
          gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.UnitIconTemplate);
          DataSource.Bind<UnitData>(gameObject1, this.CreateUnitData(drop.unit));
          // ISSUE: reference to a compiler-generated field
          thumbnail2CAnonStorey332.type = GachaResultThumbnailWindow.GachaResultType.Unit;
        }
        else if (drop.type == GachaDropData.Type.Item)
        {
          gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemIconTemplate);
          DataSource.Bind<ItemData>(gameObject1, this.CreateItemData(drop.item, drop.num));
          // ISSUE: reference to a compiler-generated field
          thumbnail2CAnonStorey332.type = !string.IsNullOrEmpty(drop.item.flavor) ? GachaResultThumbnailWindow.GachaResultType.Item : GachaResultThumbnailWindow.GachaResultType.Piece;
        }
        else if (drop.type == GachaDropData.Type.Artifact)
        {
          gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ArtifactIconTemplate);
          DataSource.Bind<ArtifactData>(gameObject1, this.CreateArtifactData(drop.artifact, drop.Rare));
          // ISSUE: reference to a compiler-generated field
          thumbnail2CAnonStorey332.type = GachaResultThumbnailWindow.GachaResultType.Artifact;
        }
        // ISSUE: reference to a compiler-generated field
        if (thumbnail2CAnonStorey332.type != GachaResultThumbnailWindow.GachaResultType.None)
        {
          gameObject1.SetActive(true);
          Button component = (Button) gameObject1.GetComponent<Button>();
          ((UnityEventBase) component.get_onClick()).RemoveAllListeners();
          // ISSUE: method pointer
          ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) thumbnail2CAnonStorey332, __methodptr(\u003C\u003Em__37C)));
          if (drop.isNew)
          {
            GameObject gameObject2 = ((Component) gameObject1.get_transform().FindChild("body/new")).get_gameObject();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
              gameObject2.SetActive(true);
          }
          if (index < 5)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform1, (UnityEngine.Object) null))
              gameObject1.get_transform().SetParent(transform1, false);
          }
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
            gameObject1.get_transform().SetParent(transform2, false);
          this.mThumbnailList[index] = gameObject1;
        }
      }
    }

    private void RefreshThumbnail()
    {
      if (GachaResultData.drops == null)
        return;
      int length = GachaResultData.drops.Length;
      this.mResultList[0] = this.CreateHorizontalList();
      if (length > 5)
        this.mResultList[1] = this.CreateHorizontalList();
      Transform transform1 = this.mResultList[0].get_transform().Find("content");
      Transform transform2 = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mResultList[1], (UnityEngine.Object) null) ? this.mResultList[1].get_transform().Find("content") : (Transform) null;
      int num = Math.Min(length, GachaResultThumbnailWindow.CONTENT_VIEW_MAX);
      for (int index = 0; index < num; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GachaResultThumbnailWindow.\u003CRefreshThumbnail\u003Ec__AnonStorey333 thumbnailCAnonStorey333 = new GachaResultThumbnailWindow.\u003CRefreshThumbnail\u003Ec__AnonStorey333();
        // ISSUE: reference to a compiler-generated field
        thumbnailCAnonStorey333.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        thumbnailCAnonStorey333.index = index + this.mCurrentPage * GachaResultThumbnailWindow.CONTENT_VIEW_MAX;
        // ISSUE: reference to a compiler-generated field
        if (length >= thumbnailCAnonStorey333.index)
        {
          // ISSUE: reference to a compiler-generated field
          GachaDropData drop = GachaResultData.drops[thumbnailCAnonStorey333.index];
          // ISSUE: reference to a compiler-generated field
          thumbnailCAnonStorey333.type = GachaResultThumbnailWindow.GachaResultType.None;
          GameObject gameObject1 = (GameObject) null;
          if (drop.type == GachaDropData.Type.Unit)
          {
            gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.UnitIconTemplate);
            DataSource.Bind<UnitData>(gameObject1, this.CreateUnitData(drop.unit));
            // ISSUE: reference to a compiler-generated field
            thumbnailCAnonStorey333.type = GachaResultThumbnailWindow.GachaResultType.Unit;
          }
          else if (drop.type == GachaDropData.Type.Item)
          {
            gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemIconTemplate);
            DataSource.Bind<ItemData>(gameObject1, this.CreateItemData(drop.item, drop.num));
            // ISSUE: reference to a compiler-generated field
            thumbnailCAnonStorey333.type = !string.IsNullOrEmpty(drop.item.flavor) ? GachaResultThumbnailWindow.GachaResultType.Item : GachaResultThumbnailWindow.GachaResultType.Piece;
          }
          else if (drop.type == GachaDropData.Type.Artifact)
          {
            gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ArtifactIconTemplate);
            DataSource.Bind<ArtifactData>(gameObject1, this.CreateArtifactData(drop.artifact, drop.Rare));
            // ISSUE: reference to a compiler-generated field
            thumbnailCAnonStorey333.type = GachaResultThumbnailWindow.GachaResultType.Artifact;
          }
          // ISSUE: reference to a compiler-generated field
          if (thumbnailCAnonStorey333.type != GachaResultThumbnailWindow.GachaResultType.None)
          {
            gameObject1.SetActive(true);
            Button component = (Button) gameObject1.GetComponent<Button>();
            ((UnityEventBase) component.get_onClick()).RemoveAllListeners();
            // ISSUE: method pointer
            ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) thumbnailCAnonStorey333, __methodptr(\u003C\u003Em__37D)));
            if (drop.isNew)
            {
              GameObject gameObject2 = ((Component) gameObject1.get_transform().FindChild("body/new")).get_gameObject();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
                gameObject2.SetActive(true);
            }
            if (index < 5)
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform1, (UnityEngine.Object) null))
                gameObject1.get_transform().SetParent(transform1, false);
            }
            else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
              gameObject1.get_transform().SetParent(transform2, false);
            this.mThumbnailList[index] = gameObject1;
          }
        }
        else
          break;
      }
      this.RefreshPagerToggle(this.mCurrentPage);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnSelectIcon(int index, GachaResultThumbnailWindow.GachaResultType type)
    {
      FlowNode_Variable.Set("GachaResultDataIndex", index.ToString());
      FlowNode_Variable.Set("GachaResultCurrentDetail", type.ToString());
      int pinID = 0;
      switch (type)
      {
        case GachaResultThumbnailWindow.GachaResultType.Unit:
          pinID = !this.CheckSingleDropForUnit() ? this.OUT_UNITDETAIL : this.OUT_UNITDETAIL_SINGLE;
          break;
        case GachaResultThumbnailWindow.GachaResultType.Item:
          pinID = this.OUT_ITEM_DETAIL;
          break;
        case GachaResultThumbnailWindow.GachaResultType.Piece:
          pinID = this.OUT_PIECE_DETAIL;
          break;
        case GachaResultThumbnailWindow.GachaResultType.Artifact:
          pinID = this.OUT_ARTIFACT_DETAIL;
          break;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private ItemData CreateItemData(ItemParam iparam, int num)
    {
      ItemData itemData = new ItemData();
      itemData.Setup(0L, iparam, num);
      return itemData;
    }

    private ArtifactData CreateArtifactData(ArtifactParam param, int rarity)
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = param.iname,
        fav = 0,
        rare = Math.Min(Math.Max(param.rareini, rarity), param.raremax)
      });
      return artifactData;
    }

    private UnitData CreateUnitData(UnitParam uparam)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit();
      json.iid = 1L;
      json.iname = uparam.iname;
      json.exp = 0;
      json.lv = 1;
      json.plus = 0;
      json.rare = 0;
      json.select = new Json_UnitSelectable();
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      if (uparam.jobsets != null && uparam.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(uparam.jobsets.Length);
        int num = 1;
        for (int index = 0; index < uparam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam((string) uparam.jobsets[index]);
          if (jobSetParam != null)
            jsonJobList.Add(new Json_Job()
            {
              iid = (long) num++,
              iname = jobSetParam.job,
              rank = 0,
              equips = (Json_Equip[]) null,
              abils = (Json_Ability[]) null
            });
        }
        json.jobs = jsonJobList.ToArray();
      }
      unitData.Deserialize(json);
      unitData.SetUniqueID(1L);
      unitData.JobRankUp(0);
      return unitData;
    }

    private bool CheckSingleDropForUnit()
    {
      bool flag = false;
      if (this.mGachaDropSets != null && this.mGachaDropSets.Count >= 0)
        return this.mGachaDropSets.Count <= 1 && (this.mGachaDropSets[0].Length == 1 && this.mGachaDropSets[0][0].type == GachaDropData.Type.Unit);
      DebugUtility.LogError("召喚結果が存在しません");
      return flag;
    }

    private void OnNextPage()
    {
      this.mCurrentPage = Mathf.Min(this.mCurrentPage + 1, this.mMaxPage);
      this.Refresh();
    }

    private void OnPrevPage()
    {
      this.mCurrentPage = Mathf.Max(0, this.mCurrentPage - 1);
      this.Refresh();
    }

    private void SetGachaDropSet(GachaDropData[] drops)
    {
      if (drops == null || drops.Length < 0)
      {
        DebugUtility.LogError("召喚結果が存在しません");
      }
      else
      {
        this.mGachaDropSets.Clear();
        this.mCachsGachaDropDatas.Clear();
        int num = Mathf.Max(1, drops.Length / GachaResultThumbnailWindow.VIEW_COUNT);
        for (int index = 0; index < num; ++index)
        {
          this.mCachsGachaDropDatas.Clear();
          this.mCachsGachaDropDatas.AddRange(((IEnumerable<GachaDropData>) drops).Skip<GachaDropData>(index * GachaResultThumbnailWindow.VIEW_COUNT).Take<GachaDropData>(GachaResultThumbnailWindow.VIEW_COUNT));
          if (this.mCachsGachaDropDatas != null && this.mCachsGachaDropDatas.Count > 0)
            this.mGachaDropSets.Add(this.mCachsGachaDropDatas.ToArray());
        }
        this.mCurrentPage = 0;
        this.mMaxPage = num;
      }
    }

    private void InitalizePager()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PagerPanel, (UnityEngine.Object) null))
        DebugUtility.LogError("PagerPanelが設定されていません");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PagerItemTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("PagerItemTemplateが設定されていません");
      }
      else
      {
        ((Component) this.PagerItemTemplate).get_gameObject().SetActive(false);
        if (this.mGachaDropSets != null && this.mGachaDropSets.Count > 0)
        {
          int num = this.mGachaDropSets.Count;
          if (this.mPagerItems != null)
          {
            for (int index = 0; index < this.mPagerItems.Count; ++index)
            {
              ((Component) this.mPagerItems[index]).get_gameObject().SetActive(false);
              this.mPagerItems[index].set_isOn(false);
            }
            int count = this.mPagerItems.Count;
            num = count <= num ? num - count : 0;
          }
          for (int index = 0; index < num; ++index)
          {
            Toggle toggle = (Toggle) UnityEngine.Object.Instantiate<Toggle>((M0) this.PagerItemTemplate);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) toggle, (UnityEngine.Object) null))
            {
              ((Component) toggle).get_transform().SetParent(this.PagerPanel.get_transform(), false);
              toggle.set_isOn(false);
              this.mPagerItems.Add(toggle);
            }
          }
          if (this.mPagerItems != null)
          {
            if (this.mPagerItems.Count < this.mGachaDropSets.Count)
            {
              DebugUtility.LogError("ページャーアイテム数が少ないです");
              return;
            }
            for (int index = 0; index < this.mGachaDropSets.Count; ++index)
              ((Component) this.mPagerItems[index]).get_gameObject().SetActive(true);
          }
        }
        this.PagerPanel.SetActive(true);
        this.RefreshPagerButton();
      }
    }

    private void RefreshPagerToggle(int index)
    {
      if (index >= this.mPagerItems.Count)
        return;
      this.mPagerItems[index].set_isOn(true);
    }

    private bool CheckIsNeedPager()
    {
      if (this.mGachaDropSets != null)
        return this.mGachaDropSets.Count > 1;
      return false;
    }

    private void RefreshGachaCostObject()
    {
      GachaCostObject gachaCostObject1 = (GachaCostObject) this.OneMoreButton.GetComponent<GachaCostObject>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gachaCostObject1, (UnityEngine.Object) null))
      {
        GachaCostObject gachaCostObject2 = (GachaCostObject) this.OneMoreButton.AddComponent<GachaCostObject>();
        gachaCostObject2.RootObject = this.OneMoreButton;
        gachaCostObject2.TicketObject = this.CostTicket;
        gachaCostObject2.DefaultObject = this.CostDefault;
        gachaCostObject2.DefaultBGObject = this.CostDefaultBG;
        gachaCostObject1 = gachaCostObject2;
      }
      gachaCostObject1.Refresh();
    }

    public enum GachaResultType
    {
      None,
      Unit,
      Item,
      Piece,
      Artifact,
      End,
    }
  }
}
