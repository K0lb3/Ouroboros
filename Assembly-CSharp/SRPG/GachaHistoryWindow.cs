// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GachaHistoryWindow : MonoBehaviour
  {
    public static readonly int MAX_VIEW = 1;
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
    private GachaHistoryData[] mHistorys;
    private string mTitleName;
    private long mDropAt;
    private Json_GachaHistoryLocalisedTitle mLocalisedTitle;
    private GameObject[] mLists;
    private bool IsDataSet;
    private bool IsView;

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
      if (!Object.op_Inequality((Object) this.TitleText, (Object) null))
        return;
      ((Component) ((Component) this.TitleText).get_transform().get_parent()).get_gameObject().SetActive(false);
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
      this.mLocalisedTitle = historys.multi_title;
      if (historys.drops != null && historys.drops.Length > 0)
      {
        this.mHistorys = new GachaHistoryData[historys.drops.Length];
        for (int index = 0; index < historys.drops.Length; ++index)
        {
          GachaHistoryData gachaHistoryData = new GachaHistoryData();
          if (gachaHistoryData.Deserialize(historys.drops[index]))
            this.mHistorys[index] = gachaHistoryData;
        }
      }
      this.IsDataSet = true;
      return true;
    }

    private bool Refresh()
    {
      if (this.mHistorys == null || this.mHistorys.Length <= 0)
      {
        DebugUtility.LogError(((object) this).GetType().FullName + " Error: mHistory is Null or Length < 0!");
        if (Object.op_Inequality((Object) this.Cauntion, (Object) null))
          this.Cauntion.SetActive(true);
        return false;
      }
      if (Object.op_Inequality((Object) this.TitleText, (Object) null))
      {
        ((Component) ((Component) this.TitleText).get_transform().get_parent()).get_gameObject().SetActive(true);
        this.TitleText.set_text(this.mTitleName + (object) ' ' + LocalizedText.Get("sys.TEXT_GACHA_HISTORY_FOOTER"));
        string configLanguage = GameUtility.Config_Language;
        if (configLanguage != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (GachaHistoryWindow.\u003C\u003Ef__switch\u0024map10 == null)
          {
            // ISSUE: reference to a compiler-generated field
            GachaHistoryWindow.\u003C\u003Ef__switch\u0024map10 = new Dictionary<string, int>(3)
            {
              {
                "french",
                0
              },
              {
                "spanish",
                1
              },
              {
                "german",
                2
              }
            };
          }
          int num;
          // ISSUE: reference to a compiler-generated field
          if (GachaHistoryWindow.\u003C\u003Ef__switch\u0024map10.TryGetValue(configLanguage, out num))
          {
            switch (num)
            {
              case 0:
                this.TitleText.set_text(this.mLocalisedTitle.fr);
                goto label_14;
              case 1:
                this.TitleText.set_text(this.mLocalisedTitle.es);
                goto label_14;
              case 2:
                this.TitleText.set_text(this.mLocalisedTitle.de);
                goto label_14;
            }
          }
        }
        this.TitleText.set_text(this.mLocalisedTitle.en);
      }
label_14:
      if (Object.op_Inequality((Object) this.DropAtText, (Object) null))
        this.DropAtText.set_text(GameUtility.UnixtimeToLocalTime(this.mDropAt).ToString(GameUtility.Localized_TimePattern_Short, (IFormatProvider) GameUtility.CultureSetting));
      this.RefreshHistoryList();
      return true;
    }

    private bool RefreshHistoryList()
    {
      this.mLists = new GameObject[this.mHistorys.Length];
      for (int index = 0; index < this.mHistorys.Length; ++index)
      {
        GachaHistoryData mHistory = this.mHistorys[index];
        if (mHistory != null)
        {
          GameObject root = (GameObject) null;
          if (mHistory.type == GachaDropData.Type.Unit)
          {
            root = (GameObject) Object.Instantiate<GameObject>((M0) this.UnitTemp);
            root.get_transform().SetParent(this.UnitTemp.get_transform().get_parent(), false);
            DataSource.Bind<UnitData>(root, this.CreateUnitData(mHistory.unit));
          }
          else if (mHistory.type == GachaDropData.Type.Item)
          {
            root = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemp);
            root.get_transform().SetParent(this.ItemTemp.get_transform().get_parent(), false);
            DataSource.Bind<ItemData>(root, this.CreateItemData(mHistory.item, mHistory.num));
          }
          else if (mHistory.type == GachaDropData.Type.Artifact)
          {
            root = (GameObject) Object.Instantiate<GameObject>((M0) this.ArtifactTemp);
            root.get_transform().SetParent(this.ArtifactTemp.get_transform().get_parent(), false);
            DataSource.Bind<ArtifactData>(root, this.CreateArtifactData(mHistory.artifact));
          }
          if (!Object.op_Equality((Object) root, (Object) null))
          {
            Transform transform = root.get_transform().Find("body/status/new");
            if (Object.op_Inequality((Object) transform, (Object) null))
              ((Component) transform).get_gameObject().SetActive(mHistory.isNew);
            this.mLists[index] = root;
            root.SetActive(true);
            GameParameter.UpdateAll(root);
          }
        }
      }
      return true;
    }

    private ItemData CreateItemData(ItemParam param, int num = 0)
    {
      ItemData itemData = new ItemData();
      itemData.Setup(0L, param, num);
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

    private UnitData CreateUnitData(UnitParam param)
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
          JobSetParam jobSetParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam((string) param.jobsets[index]);
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
