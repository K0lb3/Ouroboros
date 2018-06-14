// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultThumbnailWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "BackToUnitDetail", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "UnitDetail", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ItemDetail", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "PieceDetail", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "ArtifactDetail", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(200, "BackGachaTop", FlowNode.PinTypes.Output, 200)]
  public class GachaResultThumbnailWindow : MonoBehaviour, IFlowInterface
  {
    private readonly int IN_REFRESH;
    private readonly int IN_BACKTO_UNITDETAIL;
    private readonly int OUT_UNIT_DETAIL;
    private readonly int OUT_ITEM_DETAIL;
    private readonly int OUT_PIECE_DETAIL;
    private readonly int OUT_ARTIFACT_DETAIL;
    private readonly int OUT_BACK_GACHATOP;
    public GameObject ResultListTemplate;
    public GameObject UnitIconTemplate;
    public GameObject ItemIconTemplate;
    public GameObject ArtifactIconTemplate;
    private GameObject[] mResultList;
    private GameObject[] mThumbnailList;

    public GachaResultThumbnailWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID == this.IN_REFRESH || pinID != this.IN_BACKTO_UNITDETAIL || (GachaResultData.drops.Length != 1 || GachaResultData.drops[0].type != GachaDropData.Type.Unit))
        return;
      FlowNode_Variable.Set("GachaResultCurrentDetail", string.Empty);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_BACK_GACHATOP);
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ResultListTemplate, (Object) null))
        this.ResultListTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.UnitIconTemplate, (Object) null))
        this.UnitIconTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemIconTemplate, (Object) null))
        this.ItemIconTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.ArtifactIconTemplate, (Object) null))
        return;
      this.ArtifactIconTemplate.SetActive(false);
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
      if (GachaResultData.drops == null || GachaResultData.drops.Length <= 0)
        return;
      if (Object.op_Inequality((Object) HomeWindow.Current, (Object) null))
        HomeWindow.Current.SetVisible(true);
      this.Refresh();
    }

    private void OnDisable()
    {
      this.Reset();
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
      for (int index = 0; index < GachaResultData.drops.Length; ++index)
      {
        if (GachaResultData.drops[index].type == GachaDropData.Type.Unit)
        {
          MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(false);
          break;
        }
      }
      FlowNode_Variable.Set("GachaResultSingle", "0");
      if (GachaResultData.drops.Length == 1 && GachaResultData.drops[0].type == GachaDropData.Type.Unit)
      {
        if (!string.IsNullOrEmpty(FlowNode_Variable.Get("GachaResultCurrentDetail")))
        {
          FlowNode_Variable.Set("GachaResultCurrentDetail", string.Empty);
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CLOSED_RESULT_SINGLE");
        }
        else
        {
          FlowNode_Variable.Set("GachaResultSingle", "1");
          this.OnSelectIcon(0, GachaResultThumbnailWindow.GachaResultType.Unit);
        }
      }
      else
        this.RefreshThumbnail();
    }

    private void RefreshThumbnail()
    {
      GameObject gameObject1 = (GameObject) Object.Instantiate<GameObject>((M0) this.ResultListTemplate);
      gameObject1.get_transform().SetParent(this.ResultListTemplate.get_transform().get_parent(), false);
      gameObject1.SetActive(true);
      this.mResultList[0] = gameObject1;
      if (GachaResultData.drops.Length > 5)
      {
        GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.ResultListTemplate);
        gameObject2.get_transform().SetParent(this.ResultListTemplate.get_transform().get_parent(), false);
        gameObject2.SetActive(true);
        this.mResultList[1] = gameObject2;
      }
      Transform transform1 = this.mResultList[0].get_transform().Find("content");
      Transform transform2 = !Object.op_Inequality((Object) this.mResultList[1], (Object) null) ? (Transform) null : this.mResultList[1].get_transform().Find("content");
      int length = GachaResultData.drops.Length;
      for (int index = 0; index < length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GachaResultThumbnailWindow.\u003CRefreshThumbnail\u003Ec__AnonStorey248 thumbnailCAnonStorey248 = new GachaResultThumbnailWindow.\u003CRefreshThumbnail\u003Ec__AnonStorey248();
        // ISSUE: reference to a compiler-generated field
        thumbnailCAnonStorey248.\u003C\u003Ef__this = this;
        GachaDropData drop = GachaResultData.drops[index];
        GameObject gameObject2 = (GameObject) null;
        // ISSUE: reference to a compiler-generated field
        thumbnailCAnonStorey248.type = GachaResultThumbnailWindow.GachaResultType.None;
        // ISSUE: reference to a compiler-generated field
        thumbnailCAnonStorey248.index = index;
        if (drop.type == GachaDropData.Type.Unit)
        {
          gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.UnitIconTemplate);
          DataSource.Bind<UnitData>(gameObject2, this.CreateUnitData(drop.unit));
          ((UnitIcon) gameObject2.GetComponent<UnitIcon>()).UpdateValue();
          // ISSUE: reference to a compiler-generated field
          thumbnailCAnonStorey248.type = GachaResultThumbnailWindow.GachaResultType.Unit;
        }
        else if (drop.type == GachaDropData.Type.Item)
        {
          gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemIconTemplate);
          DataSource.Bind<ItemData>(gameObject2, this.CreateItemData(drop.item, drop.num));
          ((ItemIcon) gameObject2.GetComponent<ItemIcon>()).UpdateValue();
          // ISSUE: reference to a compiler-generated field
          thumbnailCAnonStorey248.type = !string.IsNullOrEmpty(drop.item.flavor) ? GachaResultThumbnailWindow.GachaResultType.Item : GachaResultThumbnailWindow.GachaResultType.Piece;
        }
        else if (drop.type == GachaDropData.Type.Artifact)
        {
          gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.ArtifactIconTemplate);
          DataSource.Bind<ArtifactData>(gameObject2, this.CreateArtifactData(drop.artifact));
          ((ArtifactIcon) gameObject2.GetComponent<ArtifactIcon>()).UpdateValue();
          // ISSUE: reference to a compiler-generated field
          thumbnailCAnonStorey248.type = GachaResultThumbnailWindow.GachaResultType.Artifact;
        }
        gameObject2.SetActive(true);
        Button component = (Button) gameObject2.GetComponent<Button>();
        ((UnityEventBase) component.get_onClick()).RemoveAllListeners();
        // ISSUE: method pointer
        ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) thumbnailCAnonStorey248, __methodptr(\u003C\u003Em__284)));
        if (drop.isNew)
        {
          GameObject gameObject3 = ((Component) gameObject2.get_transform().FindChild("body/new")).get_gameObject();
          if (Object.op_Inequality((Object) gameObject3, (Object) null))
            gameObject3.SetActive(true);
        }
        if (index < 5)
        {
          if (Object.op_Inequality((Object) transform1, (Object) null))
            gameObject2.get_transform().SetParent(transform1, false);
        }
        else if (Object.op_Inequality((Object) transform2, (Object) null))
          gameObject2.get_transform().SetParent(transform2, false);
        this.mThumbnailList[index] = gameObject2;
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void Init()
    {
      int length = GachaResultData.drops.Length;
      bool flag = false;
      for (int index = 0; index < GachaResultData.drops.Length; ++index)
      {
        if (GachaResultData.drops[index].type == GachaDropData.Type.Unit)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(false);
      FlowNode_Variable.Set("GachaResultSingle", "0");
      if (length == 1 && GachaResultData.drops[0].type == GachaDropData.Type.Unit)
      {
        if (!string.IsNullOrEmpty(FlowNode_Variable.Get("GachaResultCurrentDetail")))
        {
          FlowNode_Variable.Set("GachaResultCurrentDetail", string.Empty);
          FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CLOSED_RESULT_SINGLE");
        }
        else
        {
          FlowNode_Variable.Set("GachaResultSingle", "1");
          this.OnSelectIcon(0, GachaResultThumbnailWindow.GachaResultType.Unit);
        }
      }
      else
      {
        GameObject gameObject1 = (GameObject) Object.Instantiate<GameObject>((M0) this.ResultListTemplate);
        ((Object) gameObject1).set_name("resultlist1");
        gameObject1.get_transform().SetParent(this.ResultListTemplate.get_transform().get_parent(), false);
        this.mResultList[0] = gameObject1;
        if (length > 5)
        {
          GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.ResultListTemplate);
          ((Object) gameObject2).set_name("resultlist2");
          gameObject2.get_transform().SetParent(this.ResultListTemplate.get_transform().get_parent(), false);
          this.mResultList[1] = gameObject2;
        }
        Transform transform1 = this.mResultList[0].get_transform().Find("content");
        Transform transform2 = (Transform) null;
        if (Object.op_Inequality((Object) this.mResultList[1], (Object) null))
          transform2 = this.mResultList[1].get_transform().Find("content");
        for (int index = 0; index < length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          GachaResultThumbnailWindow.\u003CInit\u003Ec__AnonStorey249 initCAnonStorey249 = new GachaResultThumbnailWindow.\u003CInit\u003Ec__AnonStorey249();
          // ISSUE: reference to a compiler-generated field
          initCAnonStorey249.\u003C\u003Ef__this = this;
          GameObject gameObject2 = (GameObject) null;
          GachaDropData drop = GachaResultData.drops[index];
          // ISSUE: reference to a compiler-generated field
          initCAnonStorey249.index = index;
          if (drop.type == GachaDropData.Type.Unit)
          {
            gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.UnitIconTemplate);
            ((Object) gameObject2).set_name("icon_" + index.ToString());
            DataSource.Bind<UnitData>(gameObject2, this.CreateUnitData(drop.unit));
            ((UnitIcon) gameObject2.GetComponent<UnitIcon>()).UpdateValue();
            Button component = (Button) gameObject2.GetComponent<Button>();
            ((UnityEventBase) component.get_onClick()).RemoveAllListeners();
            // ISSUE: method pointer
            ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) initCAnonStorey249, __methodptr(\u003C\u003Em__285)));
          }
          else if (drop.type == GachaDropData.Type.Item)
          {
            gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemIconTemplate);
            DataSource.Bind<ItemData>(gameObject2, this.CreateItemData(drop.item, drop.num));
            ((ItemIcon) gameObject2.GetComponent<ItemIcon>()).UpdateValue();
            Button component = (Button) gameObject2.GetComponent<Button>();
            ((UnityEventBase) component.get_onClick()).RemoveAllListeners();
            if (!string.IsNullOrEmpty(drop.item.flavor))
            {
              // ISSUE: method pointer
              ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) initCAnonStorey249, __methodptr(\u003C\u003Em__286)));
            }
            else
            {
              // ISSUE: method pointer
              ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) initCAnonStorey249, __methodptr(\u003C\u003Em__287)));
            }
          }
          else if (drop.type == GachaDropData.Type.Artifact)
          {
            gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.ArtifactIconTemplate);
            DataSource.Bind<ArtifactData>(gameObject2, this.CreateArtifactData(drop.artifact));
            ((ArtifactIcon) gameObject2.GetComponent<ArtifactIcon>()).UpdateValue();
            Button component = (Button) gameObject2.GetComponent<Button>();
            ((UnityEventBase) component.get_onClick()).RemoveAllListeners();
            // ISSUE: method pointer
            ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) initCAnonStorey249, __methodptr(\u003C\u003Em__288)));
          }
          if (drop.isNew)
          {
            GameObject gameObject3 = ((Component) gameObject2.get_transform().FindChild("body/new")).get_gameObject();
            if (Object.op_Inequality((Object) gameObject3, (Object) null))
              gameObject3.SetActive(true);
          }
          if (index < 5)
            gameObject2.get_transform().SetParent(transform1, false);
          else if (Object.op_Inequality((Object) transform2, (Object) null))
            gameObject2.get_transform().SetParent(transform2, false);
          this.mThumbnailList[index] = gameObject2;
        }
        GameParameter.UpdateAll(((Component) this).get_gameObject());
        this.RefreshThumbnail();
      }
    }

    private void OnSelectIcon(int index, GachaResultThumbnailWindow.GachaResultType type)
    {
      FlowNode_Variable.Set("GachaResultDataIndex", index.ToString());
      FlowNode_Variable.Set("GachaResultCurrentDetail", type.ToString());
      switch (type)
      {
        case GachaResultThumbnailWindow.GachaResultType.Unit:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_UNIT_DETAIL);
          break;
        case GachaResultThumbnailWindow.GachaResultType.Item:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_ITEM_DETAIL);
          break;
        case GachaResultThumbnailWindow.GachaResultType.Piece:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_PIECE_DETAIL);
          break;
        case GachaResultThumbnailWindow.GachaResultType.Artifact:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_ARTIFACT_DETAIL);
          break;
      }
    }

    private ItemData CreateItemData(ItemParam iparam, int num)
    {
      ItemData itemData = new ItemData();
      itemData.Setup(0L, iparam, num);
      return itemData;
    }

    private ArtifactData CreateArtifactData(ArtifactParam param)
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = param.iname,
        fav = 0,
        rare = param.rareini
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
