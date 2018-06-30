namespace SRPG
{
    using System;
    using UnityEngine;

    public class ResultOrdeal : MonoBehaviour
    {
        public ResultOrdeal()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            SceneBattle battle;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_001C;
            }
            if (battle.ResultData != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            DataSource.Bind<BattleCore.Record>(base.get_gameObject(), battle.ResultData.Record);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }
    }
}

