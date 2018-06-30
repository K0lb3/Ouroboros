namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class MapEffectQuest : MonoBehaviour
    {
        public GameObject GoMapEffectParent;
        public GameObject GoMapEffectBaseItem;

        public MapEffectQuest()
        {
            base..ctor();
            return;
        }

        public static GameObject CreateInstance(GameObject go_target, Transform parent)
        {
            GameObject obj2;
            RectTransform transform;
            RectTransform transform2;
            if (go_target != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            obj2 = Object.Instantiate<GameObject>(go_target);
            if (obj2 != null)
            {
                goto Label_0021;
            }
            return null;
        Label_0021:
            if (parent == null)
            {
                goto Label_0038;
            }
            obj2.get_transform().SetParent(parent);
        Label_0038:
            transform = go_target.GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_009E;
            }
            if ((go_target.GetComponent<Canvas>() != null) == null)
            {
                goto Label_009E;
            }
            transform2 = obj2.GetComponent<RectTransform>();
            if (transform2 == null)
            {
                goto Label_009E;
            }
            transform2.set_anchorMax(transform.get_anchorMax());
            transform2.set_anchorMin(transform.get_anchorMin());
            transform2.set_anchoredPosition(transform.get_anchoredPosition());
            transform2.set_sizeDelta(transform.get_sizeDelta());
        Label_009E:
            obj2.get_transform().set_localScale(Vector3.get_one());
            return obj2;
        }

        public unsafe void Setup()
        {
            GameObject[] objArray1;
            QuestParam param;
            GameManager manager;
            MapEffectParam param2;
            DataSource source;
            int num;
            SkillParam param3;
            List<JobParam> list;
            JobParam param4;
            List<JobParam>.Enumerator enumerator;
            GameObject obj2;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if (manager != null)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            if (this.GoMapEffectParent == null)
            {
                goto Label_0071;
            }
            if (this.GoMapEffectBaseItem == null)
            {
                goto Label_0071;
            }
            this.GoMapEffectBaseItem.SetActive(0);
            objArray1 = new GameObject[] { this.GoMapEffectBaseItem };
            BattleUnitDetail.DestroyChildGameObjects(this.GoMapEffectParent, new List<GameObject>(objArray1));
        Label_0071:
            param2 = manager.GetMapEffectParam(param.MapEffectId);
            if (param2 != null)
            {
                goto Label_0085;
            }
            return;
        Label_0085:
            source = base.get_gameObject().GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_00A2;
            }
            source.Clear();
        Label_00A2:
            DataSource.Bind<MapEffectParam>(base.get_gameObject(), param2);
            if (this.GoMapEffectParent == null)
            {
                goto Label_01B2;
            }
            if (this.GoMapEffectBaseItem == null)
            {
                goto Label_01B2;
            }
            num = 0;
            goto Label_01A0;
        Label_00D6:
            param3 = manager.GetSkillParam(param2.ValidSkillLists[num]);
            if (param3 != null)
            {
                goto Label_00F7;
            }
            goto Label_019A;
        Label_00F7:
            enumerator = MapEffectParam.GetHaveJobLists(param3.iname).GetEnumerator();
        Label_010E:
            try
            {
                goto Label_017C;
            Label_0113:
                param4 = &enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(this.GoMapEffectBaseItem);
                if (obj2 != null)
                {
                    goto Label_013A;
                }
                goto Label_017C;
            Label_013A:
                obj2.get_transform().SetParent(this.GoMapEffectParent.get_transform());
                obj2.get_transform().set_localScale(Vector3.get_one());
                DataSource.Bind<JobParam>(obj2, param4);
                DataSource.Bind<SkillParam>(obj2, param3);
                obj2.SetActive(1);
            Label_017C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0113;
                }
                goto Label_019A;
            }
            finally
            {
            Label_018D:
                ((List<JobParam>.Enumerator) enumerator).Dispose();
            }
        Label_019A:
            num += 1;
        Label_01A0:
            if (num < param2.ValidSkillLists.Count)
            {
                goto Label_00D6;
            }
        Label_01B2:
            return;
        }
    }
}

