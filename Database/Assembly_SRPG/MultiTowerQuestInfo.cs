namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class MultiTowerQuestInfo : MonoBehaviour
    {
        public GameObject EnemyTemplate;
        public GameObject EnemyRoot;
        public Text QuestTitle;
        public Text RecommendLv;
        public GameObject DetailObject;
        public GameObject RewardTemplate;
        public GameObject RewardRoot;
        private GameObject Detail;
        private List<GameObject> mEnemyObject;

        public MultiTowerQuestInfo()
        {
            this.mEnemyObject = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Refresh()
        {
            MultiTowerFloorParam param;
            param = DataSource.FindDataOfClass<MultiTowerFloorParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0094;
            }
            this.SetEnemy(param);
            if ((this.QuestTitle != null) == null)
            {
                goto Label_004C;
            }
            this.QuestTitle.set_text(param.title + " " + param.name);
        Label_004C:
            if ((this.RecommendLv != null) == null)
            {
                goto Label_008D;
            }
            this.RecommendLv.set_text(string.Format(LocalizedText.Get("sys.MULTI_TOWER_RECOMMEND"), (short) param.lv, (short) param.joblv));
        Label_008D:
            this.SetReward(param);
        Label_0094:
            return;
        }

        private void SetEnemy(MultiTowerFloorParam param)
        {
            int num;
            string str;
            string str2;
            JSON_MapUnit unit;
            int num2;
            JSON_MapEnemyUnit unit2;
            NPCSetting setting;
            Unit unit3;
            GameObject obj2;
            int num3;
            num = 0;
            if (param.map == null)
            {
                goto Label_018A;
            }
            str2 = AssetManager.LoadTextData(AssetPath.LocalMap(param.map[0].mapSetName));
            if (str2 != null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            unit = JSONParser.parseJSONObject<JSON_MapUnit>(str2);
            if (unit == null)
            {
                goto Label_018A;
            }
            if ((this.EnemyTemplate != null) == null)
            {
                goto Label_018A;
            }
            num2 = 0;
            goto Label_013C;
        Label_0058:
            unit2 = unit.enemy[num2];
            setting = new NPCSetting(unit2);
            unit3 = new Unit();
            if (unit3 == null)
            {
                goto Label_0136;
            }
            if (unit3.Setup(null, setting, null, null) == null)
            {
                goto Label_0136;
            }
            if (unit3.IsGimmick == null)
            {
                goto Label_009C;
            }
            goto Label_0136;
        Label_009C:
            obj2 = null;
            if ((num + 1) <= this.mEnemyObject.Count)
            {
                goto Label_00E3;
            }
            obj2 = Object.Instantiate<GameObject>(this.EnemyTemplate);
            if ((obj2 == null) == null)
            {
                goto Label_00D1;
            }
            goto Label_0136;
        Label_00D1:
            this.mEnemyObject.Add(obj2);
            goto Label_00F1;
        Label_00E3:
            obj2 = this.mEnemyObject[num];
        Label_00F1:
            DataSource.Bind<Unit>(obj2, unit3);
            GameParameter.UpdateAll(obj2);
            if ((this.EnemyRoot != null) == null)
            {
                goto Label_012A;
            }
            obj2.get_transform().SetParent(this.EnemyRoot.get_transform(), 0);
        Label_012A:
            obj2.SetActive(1);
            num += 1;
        Label_0136:
            num2 += 1;
        Label_013C:
            if (num2 < ((int) unit.enemy.Length))
            {
                goto Label_0058;
            }
            num3 = num;
            goto Label_016C;
        Label_0153:
            this.mEnemyObject[num3].SetActive(0);
            num3 += 1;
        Label_016C:
            if (num3 < this.mEnemyObject.Count)
            {
                goto Label_0153;
            }
            this.EnemyTemplate.SetActive(0);
        Label_018A:
            return;
        }

        private void SetReward(MultiTowerFloorParam param)
        {
            GameManager manager;
            List<MultiTowerRewardItem> list;
            MultiTowerRewardItem item;
            MultiTowerRewardInfo info;
            manager = MonoSingleton<GameManager>.Instance;
            list = manager.GetMTFloorReward(param.reward_id, manager.GetMTRound(param.floor));
            if ((list != null) && ((this.RewardTemplate == null) == null))
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            item = (list.Count <= 0) ? null : list[0];
            if ((this.RewardTemplate != null) == null)
            {
                goto Label_008C;
            }
            DataSource.Bind<MultiTowerRewardItem>(this.RewardTemplate, item);
            info = this.RewardTemplate.GetComponent<MultiTowerRewardInfo>();
            if ((info != null) == null)
            {
                goto Label_008C;
            }
            info.Refresh();
        Label_008C:
            return;
        }

        private void Start()
        {
        }
    }
}

