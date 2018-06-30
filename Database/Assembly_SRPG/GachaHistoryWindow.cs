namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaHistoryWindow : MonoBehaviour
    {
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
        public static readonly int NODE_VIEW_MAX;
        private string mTitleName;
        private long mDropAt;
        private GameObject[] mLists;
        private bool IsDataSet;
        private bool IsView;
        private List<GachaHistoryData[]> mHistorySets;
        private List<GachaHistoryData> mCacheHistorys;
        private List<GameObject> mListObjects;

        static GachaHistoryWindow()
        {
            NODE_VIEW_MAX = 10;
            return;
        }

        public GachaHistoryWindow()
        {
            this.mHistorySets = new List<GachaHistoryData[]>();
            this.mCacheHistorys = new List<GachaHistoryData>();
            this.mListObjects = new List<GameObject>();
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.UnitTemp != null) == null)
            {
                goto Label_001D;
            }
            this.UnitTemp.SetActive(0);
        Label_001D:
            if ((this.ItemTemp != null) == null)
            {
                goto Label_003A;
            }
            this.ItemTemp.SetActive(0);
        Label_003A:
            if ((this.ArtifactTemp != null) == null)
            {
                goto Label_0057;
            }
            this.ArtifactTemp.SetActive(0);
        Label_0057:
            if ((this.Cauntion != null) == null)
            {
                goto Label_0074;
            }
            this.Cauntion.SetActive(0);
        Label_0074:
            if ((this.TitleText != null) == null)
            {
                goto Label_00A0;
            }
            this.TitleText.get_transform().get_parent().get_gameObject().SetActive(0);
        Label_00A0:
            if ((this.NodeTemplate != null) == null)
            {
                goto Label_00BD;
            }
            this.NodeTemplate.SetActive(0);
        Label_00BD:
            return;
        }

        public static ArtifactData CreateArtifactData(ArtifactParam param, int rarity)
        {
            ArtifactData data;
            Json_Artifact artifact;
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iid = 1L;
            artifact.exp = 0;
            artifact.iname = param.iname;
            artifact.fav = 0;
            artifact.rare = rarity;
            data.Deserialize(artifact);
            return data;
        }

        public static ItemData CreateItemData(ItemParam param, int num)
        {
            ItemData data;
            data = new ItemData();
            data.Setup(0L, param, num);
            return data;
        }

        public static UnitData CreateUnitData(UnitParam param)
        {
            UnitData data;
            Json_Unit unit;
            List<Json_Job> list;
            int num;
            int num2;
            JobSetParam param2;
            Json_Job job;
            data = new UnitData();
            unit = new Json_Unit();
            unit.iid = 1L;
            unit.iname = param.iname;
            unit.exp = 0;
            unit.lv = 1;
            unit.plus = 0;
            unit.rare = param.rare;
            unit.select = new Json_UnitSelectable();
            unit.select.job = 0L;
            unit.jobs = null;
            unit.abil = null;
            if (param.jobsets == null)
            {
                goto Label_011C;
            }
            if (((int) param.jobsets.Length) <= 0)
            {
                goto Label_011C;
            }
            list = new List<Json_Job>((int) param.jobsets.Length);
            num = 1;
            num2 = 0;
            goto Label_0101;
        Label_0098:
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(param.jobsets[num2]);
            if (param2 != null)
            {
                goto Label_00B9;
            }
            goto Label_00FB;
        Label_00B9:
            job = new Json_Job();
            job.iid = (long) num++;
            job.iname = param2.job;
            job.rank = 0;
            job.equips = null;
            job.abils = null;
            list.Add(job);
        Label_00FB:
            num2 += 1;
        Label_0101:
            if (num2 < ((int) param.jobsets.Length))
            {
                goto Label_0098;
            }
            unit.jobs = list.ToArray();
        Label_011C:
            data.Deserialize(unit);
            data.SetUniqueID(1L);
            data.JobRankUp(0);
            return data;
        }

        private bool InializeHistoryList()
        {
            int num;
            GameObject obj2;
            GachaHistoryItemData data;
            if (this.mHistorySets == null)
            {
                goto Label_001C;
            }
            if (this.mHistorySets.Count >= 0)
            {
                goto Label_0028;
            }
        Label_001C:
            DebugUtility.LogError("召喚履歴が存在しません");
            return 0;
        Label_0028:
            if ((this.NodeTemplate == null) == null)
            {
                goto Label_0045;
            }
            DebugUtility.LogError("召喚履歴ノードの指定されていません");
            return 0;
        Label_0045:
            this.mListObjects.Clear();
            num = 0;
            goto Label_00DC;
        Label_0057:
            obj2 = Object.Instantiate<GameObject>(this.NodeTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_00D8;
            }
            obj2.SetActive(1);
            data = new GachaHistoryItemData(this.mHistorySets[num], this.mTitleName, this.mDropAt);
            if (data != null)
            {
                goto Label_00A9;
            }
            DebugUtility.LogError("GachaHistoryItemDataの生成に失敗しました");
            goto Label_00D8;
        Label_00A9:
            DataSource.Bind<GachaHistoryItemData>(obj2, data);
            obj2.get_transform().SetParent(this.NodeTemplate.get_transform().get_parent(), 0);
            this.mListObjects.Add(obj2);
        Label_00D8:
            num += 1;
        Label_00DC:
            if (num < this.mHistorySets.Count)
            {
                goto Label_0057;
            }
            return 1;
        }

        private void Refresh()
        {
            this.InializeHistoryList();
            return;
        }

        public bool SetGachaHistoryData(Json_GachaHistoryLog historys)
        {
            List<GachaHistoryData> list;
            int num;
            GachaHistoryData data;
            int num2;
            int num3;
            if (historys != null)
            {
                goto Label_002F;
            }
            this.Cauntion.SetActive(1);
            this.TitleText.get_transform().get_parent().get_gameObject().SetActive(0);
            return 0;
        Label_002F:
            this.mTitleName = historys.title;
            this.mDropAt = historys.drop_at;
            list = new List<GachaHistoryData>();
            if (historys.drops == null)
            {
                goto Label_009F;
            }
            if (((int) historys.drops.Length) <= 0)
            {
                goto Label_009F;
            }
            num = 0;
            goto Label_0091;
        Label_006D:
            data = new GachaHistoryData();
            if (data.Deserialize(historys.drops[num]) == null)
            {
                goto Label_008D;
            }
            list.Add(data);
        Label_008D:
            num += 1;
        Label_0091:
            if (num < ((int) historys.drops.Length))
            {
                goto Label_006D;
            }
        Label_009F:
            if (historys.drops == null)
            {
                goto Label_0159;
            }
            if (((int) historys.drops.Length) <= 0)
            {
                goto Label_0159;
            }
            this.mHistorySets.Clear();
            this.mCacheHistorys.Clear();
            num2 = Mathf.Max(1, ((int) historys.drops.Length) / NODE_VIEW_MAX);
            num3 = 0;
            goto Label_0151;
        Label_00EB:
            this.mCacheHistorys.Clear();
            this.mCacheHistorys.AddRange(Enumerable.Take<GachaHistoryData>(Enumerable.Skip<GachaHistoryData>(list, num3 * NODE_VIEW_MAX), NODE_VIEW_MAX));
            if (this.mCacheHistorys == null)
            {
                goto Label_014B;
            }
            if (this.mCacheHistorys.Count <= 0)
            {
                goto Label_014B;
            }
            this.mHistorySets.Add(this.mCacheHistorys.ToArray());
        Label_014B:
            num3 += 1;
        Label_0151:
            if (num3 < num2)
            {
                goto Label_00EB;
            }
        Label_0159:
            this.IsDataSet = 1;
            return 1;
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (this.IsDataSet == null)
            {
                goto Label_0023;
            }
            if (this.IsView != null)
            {
                goto Label_0023;
            }
            this.IsView = 1;
            this.Refresh();
        Label_0023:
            return;
        }
    }
}

