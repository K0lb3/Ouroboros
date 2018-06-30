namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class TrophyObjectiveList : MonoBehaviour
    {
        public GameObject Item_Complete;
        public GameObject Item_Incomplete;

        public TrophyObjectiveList()
        {
            base..ctor();
            return;
        }

        public virtual void Start()
        {
            TrophyParam param;
            TrophyState state;
            Transform transform;
            int num;
            TrophyObjective objective;
            bool flag;
            TrophyObjectiveData data;
            GameObject obj2;
            GameObject obj3;
            if ((this.Item_Complete != null) == null)
            {
                goto Label_001D;
            }
            this.Item_Complete.SetActive(0);
        Label_001D:
            if ((this.Item_Incomplete != null) == null)
            {
                goto Label_003A;
            }
            this.Item_Incomplete.SetActive(0);
        Label_003A:
            param = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_004E;
            }
            return;
        Label_004E:
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(param, 0);
            transform = base.get_transform();
            num = 0;
            goto Label_0137;
        Label_006E:
            objective = param.Objectives[num];
            data = new TrophyObjectiveData();
            data.CountMax = objective.RequiredCount;
            data.Description = SRPG_Extensions.GetDescription(objective);
            data.Objective = objective;
            if (num >= ((int) state.Count.Length))
            {
                goto Label_00DC;
            }
            data.Count = state.Count[num];
            flag = (objective.RequiredCount > state.Count[num]) == 0;
            goto Label_00DF;
        Label_00DC:
            flag = 0;
        Label_00DF:
            obj2 = (flag == null) ? this.Item_Incomplete : this.Item_Complete;
            if ((obj2 == null) == null)
            {
                goto Label_010B;
            }
            goto Label_0133;
        Label_010B:
            obj3 = Object.Instantiate<GameObject>(obj2);
            DataSource.Bind<TrophyObjectiveData>(obj3, data);
            obj3.get_transform().SetParent(transform, 0);
            obj3.SetActive(1);
        Label_0133:
            num += 1;
        Label_0137:
            if (num < ((int) param.Objectives.Length))
            {
                goto Label_006E;
            }
            return;
        }
    }
}

