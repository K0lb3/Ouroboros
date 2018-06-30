namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(4, "アビリティポイントが不足", 1, 4), Pin(2, "アビリティ成長限界に達している", 1, 2), NodeType("System/AbilityRankUp", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), Pin(3, "アビリティ成長可能回数が不足", 1, 3)]
    public class FlowNode_AbilityRankUp : FlowNode
    {
        public FlowNode_AbilityRankUp()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            PlayerData data;
            UnitData data2;
            AbilityData data3;
            Dictionary<long, int> dictionary;
            List<string> list;
            Dictionary<long, int> dictionary2;
            long num;
            int num2;
            if (pinID != null)
            {
                goto Label_011C;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = data.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            data3 = data2.GetAbilityData(GlobalVars.SelectedAbilityUniqueID);
            if (data3.Rank < data3.GetRankCap())
            {
                goto Label_0053;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
        Label_0053:
            if (data.AbilityRankUpCountNum != null)
            {
                goto Label_006D;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
        Label_006D:
            data.RankUpAbility(data3);
            dictionary = GlobalVars.AbilitiesRankUp;
            if (dictionary.ContainsKey(data3.UniqueID) != null)
            {
                goto Label_0099;
            }
            dictionary[data3.UniqueID] = 0;
        Label_0099:
            num2 = dictionary2[num];
            (dictionary2 = dictionary)[num = data3.UniqueID] = num2 + 1;
            MonoSingleton<GameManager>.Instance.Player.OnAbilityPowerUp(data2.UnitID, data3.AbilityID, data3.Rank, 0);
            list = data3.GetLearningSkillList(data3.Rank);
            if (list == null)
            {
                goto Label_010D;
            }
            if (list.Count <= 0)
            {
                goto Label_010D;
            }
            FlowNode_Variable.Set("LEARNING_SKILL", "1");
        Label_010D:
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
        Label_011C:
            return;
        }
    }
}

