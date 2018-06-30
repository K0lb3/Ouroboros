namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class MultiPlayUnitRank : MonoBehaviour
    {
        public GameObject ItemTemplate;
        public GameObject Parent;
        public GameObject Root;
        public GameObject NotDataObj;
        public GameObject DataObj;

        public MultiPlayUnitRank()
        {
            base..ctor();
            return;
        }

        private void RefreshData()
        {
            GameManager manager;
            List<MultiRanking> list;
            QuestParam param;
            int num;
            GameObject obj2;
            UnitParam param2;
            UnitData data;
            manager = MonoSingleton<GameManager>.Instance;
            list = manager.MultiUnitRank;
            param = manager.FindQuest(GlobalVars.SelectedQuestID);
            DataSource.Bind<QuestParam>(base.get_gameObject(), param);
            if (list == null)
            {
                goto Label_0036;
            }
            if (list.Count != null)
            {
                goto Label_007F;
            }
        Label_0036:
            if ((this.NotDataObj != null) == null)
            {
                goto Label_0058;
            }
            this.NotDataObj.get_gameObject().SetActive(1);
        Label_0058:
            if ((this.DataObj != null) == null)
            {
                goto Label_018A;
            }
            this.DataObj.get_gameObject().SetActive(0);
            goto Label_018A;
        Label_007F:
            if ((this.NotDataObj != null) == null)
            {
                goto Label_00A1;
            }
            this.NotDataObj.get_gameObject().SetActive(0);
        Label_00A1:
            if ((this.DataObj != null) == null)
            {
                goto Label_00C3;
            }
            this.DataObj.get_gameObject().SetActive(1);
        Label_00C3:
            num = 0;
            goto Label_017E;
        Label_00CA:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_017A;
            }
            param2 = manager.GetUnitParam(list[num].unit);
            data = new UnitData();
            data.Setup(list[num].unit, 0, 1, 0, list[num].job, 1, param2.element, 0);
            DataSource.Bind<UnitData>(obj2, data);
            this.SetParam(obj2, num, list[num]);
            if ((this.Parent != null) == null)
            {
                goto Label_016D;
            }
            obj2.get_transform().SetParent(this.Parent.get_transform(), 0);
        Label_016D:
            obj2.get_gameObject().SetActive(1);
        Label_017A:
            num += 1;
        Label_017E:
            if (num < list.Count)
            {
                goto Label_00CA;
            }
        Label_018A:
            if ((this.Root != null) == null)
            {
                goto Label_01A6;
            }
            GameParameter.UpdateAll(this.Root);
        Label_01A6:
            return;
        }

        public void SetIcon(Transform body, JobParam job)
        {
            Transform transform;
            Transform transform2;
            Transform transform3;
            RawImage_Transparent transparent;
            transform = body.FindChild("ui_uniticon");
            if ((transform == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            transform2 = transform.FindChild("unit");
            if ((transform2 == null) == null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            transform3 = transform2.FindChild("job");
            if ((transform3 == null) == null)
            {
                goto Label_004B;
            }
            return;
        Label_004B:
            transparent = transform3.GetComponent<RawImage_Transparent>();
            if ((transparent == null) == null)
            {
                goto Label_005F;
            }
            return;
        Label_005F:
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(transparent, (job == null) ? null : AssetPath.JobIconSmall(job));
            return;
        }

        private unsafe void SetParam(GameObject item, int rank, MultiRanking param)
        {
            Transform transform;
            Transform transform2;
            Transform transform3;
            LText text;
            string str;
            UnitParam param2;
            JobParam param3;
            Transform transform4;
            LText text2;
            string str2;
            int num;
            transform = item.get_transform().FindChild("body");
            if ((transform != null) == null)
            {
                goto Label_010A;
            }
            transform2 = transform.FindChild("rank");
            if ((transform2 != null) == null)
            {
                goto Label_0085;
            }
            transform3 = transform2.FindChild("rank_txt");
            if ((transform3 != null) == null)
            {
                goto Label_0085;
            }
            text = transform3.GetComponent<LText>();
            if ((text != null) == null)
            {
                goto Label_0085;
            }
            num = rank + 1;
            str = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_RANK"), &num.ToString());
            text.set_text(str);
        Label_0085:
            param2 = MonoSingleton<GameManager>.Instance.GetUnitParam(param.unit);
            param3 = MonoSingleton<GameManager>.Instance.GetJobParam(param.job);
            transform4 = transform.FindChild("name");
            if ((transform4 != null) == null)
            {
                goto Label_0101;
            }
            text2 = transform4.GetComponent<LText>();
            if ((text2 != null) == null)
            {
                goto Label_0101;
            }
            str2 = string.Format(LocalizedText.Get("sys.MULTI_CLEAR_UNIT_NAME"), param2.name, param3.name);
            text2.set_text(str2);
        Label_0101:
            this.SetIcon(transform, param3);
        Label_010A:
            return;
        }

        private void Start()
        {
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.RefreshData();
            return;
        }
    }
}

