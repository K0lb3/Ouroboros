// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GachaHistoryWindow : MonoBehaviour
  {
    public static readonly int NODE_VIEW_MAX = 10;
    [SerializeField]
    private RectTransform Root;
    [SerializeField]
    private GameObject Cauntion;
    [SerializeField]
    private GameObject UnitTemp;
    [SerializeField]
    private GameObject ItemTemp;
    [SerializeField]
    private GameObject ArtifactTemp;
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private Text DropAtText;
    [SerializeField]
    private GameObject NodeTemplate;
    private string mTitleName;
    private long mDropAt;
    private GameObject[] mLists;
    private bool IsDataSet;
    private bool IsView;
    private List<GachaHistoryData[]> mHistorySets;
    private List<GachaHistoryData> mCacheHistorys;
    private List<GameObject> mListObjects;

    public GachaHistoryWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.UnitTemp, (Object) null))
        this.UnitTemp.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemTemp, (Object) null))
        this.ItemTemp.SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactTemp, (Object) null))
        this.ArtifactTemp.SetActive(false);
      if (Object.op_Inequality((Object) this.Cauntion, (Object) null))
        this.Cauntion.SetActive(false);
      if (Object.op_Inequality((Object) this.TitleText, (Object) null))
        ((Component) ((Component) this.TitleText).get_transform().get_parent()).get_gameObject().SetActive(false);
      if (!Object.op_Inequality((Object) this.NodeTemplate, (Object) null))
        return;
      this.NodeTemplate.SetActive(false);
    }

    private void Start()
    {
    }

    private void Update()
    {
      if (!this.IsDataSet || this.IsView)
        return;
      this.IsView = true;
      this.Refresh();
    }

    public bool SetGachaHistoryData(Json_GachaHistoryLog historys)
    {
      if (historys == null)
      {
        this.Cauntion.SetActive(true);
        ((Component) ((Component) this.TitleText).get_transform().get_parent()).get_gameObject().SetActive(false);
        return false;
      }
      this.mTitleName = historys.title;
      this.mDropAt = historys.drop_at;
      List<GachaHistoryData> source = new List<GachaHistoryData>();
      if (historys.drops != null && historys.drops.Length > 0)
      {
        for (int index = 0; index < historys.drops.Length; ++index)
        {
          GachaHistoryData gachaHistoryData = new GachaHistoryData();
          if (gachaHistoryData.Deserialize(historys.drops[index]))
            source.Add(gachaHistoryData);
        }
      }
      if (historys.drops != null && historys.drops.Length > 0)
      {
        this.mHistorySets.Clear();
        this.mCacheHistorys.Clear();
        int num = Mathf.Max(1, historys.drops.Length / GachaHistoryWindow.NODE_VIEW_MAX);
        for (int index = 0; index < num; ++index)
        {
          this.mCacheHistorys.Clear();
          this.mCacheHistorys.AddRange(source.Skip<GachaHistoryData>(index * GachaHistoryWindow.NODE_VIEW_MAX).Take<GachaHistoryData>(GachaHistoryWindow.NODE_VIEW_MAX));
          if (this.mCacheHistorys != null && this.mCacheHistorys.Count > 0)
            this.mHistorySets.Add(this.mCacheHistorys.ToArray());
        }
      }
      this.IsDataSet = true;
      return true;
    }

    private void Refresh()
    {
      this.InializeHistoryList();
    }

    private bool InializeHistoryList()
    {
      if (this.mHistorySets == null || this.mHistorySets.Count < 0)
      {
        DebugUtility.LogError("召喚履歴が存在しません");
        return false;
      }
      if (Object.op_Equality((Object) this.NodeTemplate, (Object) null))
      {
        DebugUtility.LogError("召喚履歴ノードの指定されていません");
        return false;
      }
      this.mListObjects.Clear();
      for (int index = 0; index < this.mHistorySets.Count; ++index)
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.NodeTemplate);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.SetActive(true);
          GachaHistoryItemData data = new GachaHistoryItemData(this.mHistorySets[index], this.mTitleName, this.mDropAt);
          if (data == null)
          {
            DebugUtility.LogError("GachaHistoryItemDataの生成に失敗しました");
          }
          else
          {
            DataSource.Bind<GachaHistoryItemData>(gameObject, data);
            gameObject.get_transform().SetParent(this.NodeTemplate.get_transform().get_parent(), false);
            this.mListObjects.Add(gameObject);
          }
        }
      }
      return true;
    }

    public static ItemData CreateItemData(ItemParam param, int num = 0)
    {
      ItemData itemData = new ItemData();
      itemData.Setup(0L, param, num);
      return itemData;
    }

    public static ArtifactData CreateArtifactData(ArtifactParam param, int rarity)
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = param.iname,
        fav = 0,
        rare = rarity
      });
      return artifactData;
    }

    public static UnitData CreateUnitData(UnitParam param)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit();
      json.iid = 1L;
      json.iname = param.iname;
      json.exp = 0;
      json.lv = 1;
      json.plus = 0;
      json.rare = (int) param.rare;
      json.select = new Json_UnitSelectable();
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      if (param.jobsets != null && param.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(param.jobsets.Length);
        int num = 1;
        for (int index = 0; index < param.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(param.jobsets[index]);
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
  }
}
