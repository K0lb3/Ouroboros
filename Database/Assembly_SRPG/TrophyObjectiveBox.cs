namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class TrophyObjectiveBox : TrophyObjectiveList
    {
        public TrophyObjectiveBox()
        {
            base..ctor();
            return;
        }

        public override void Start()
        {
            TrophyParam param;
            TrophyState state;
            Transform transform;
            int num;
            TrophyObjective objective;
            bool flag;
            TrophyObjectiveData data;
            GameObject obj2;
            param = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(param, 0);
            transform = base.get_transform();
            num = 0;
            goto Label_00F4;
        Label_0034:
            objective = param.Objectives[num];
            data = new TrophyObjectiveData();
            data.CountMax = objective.RequiredCount;
            data.Description = SRPG_Extensions.GetDescription(objective);
            data.Objective = objective;
            if (num >= ((int) state.Count.Length))
            {
                goto Label_00A2;
            }
            data.Count = state.Count[num];
            flag = (objective.RequiredCount > state.Count[num]) == 0;
            goto Label_00A5;
        Label_00A2:
            flag = 0;
        Label_00A5:
            obj2 = (flag == null) ? base.Item_Incomplete : base.Item_Complete;
            if ((obj2 == null) == null)
            {
                goto Label_00D1;
            }
            goto Label_00F0;
        Label_00D1:
            DataSource.Bind<TrophyObjectiveData>(obj2, data);
            obj2.get_transform().SetParent(transform, 0);
            obj2.SetActive(1);
        Label_00F0:
            num += 1;
        Label_00F4:
            if (num < ((int) param.Objectives.Length))
            {
                goto Label_0034;
            }
            return;
        }
    }
}

