namespace SRPG
{
    using System;

    public class GachaButtonParam
    {
        private int m_Cost;
        private int m_StepIndex;
        private int m_StepMax;
        private int m_TicketNum;
        private int m_ExecNum;
        private int m_AppealType;
        private string m_ButtonText;
        private string m_AppealText;
        private bool m_IsShowStepup;
        private bool m_IsNoUseFree;
        private GachaCostType m_CostType;
        private GachaCategory m_Category;

        public GachaButtonParam()
        {
            this.m_ButtonText = string.Empty;
            this.m_AppealText = string.Empty;
            this.m_IsShowStepup = 1;
            base..ctor();
            this.m_Cost = 0;
            this.m_StepIndex = 0;
            this.m_StepMax = 0;
            this.m_TicketNum = 0;
            this.m_ExecNum = 0;
            this.m_ButtonText = string.Empty;
            this.m_AppealText = string.Empty;
            this.m_IsShowStepup = 1;
            this.m_IsNoUseFree = 0;
            this.m_CostType = 0;
            this.m_Category = 0;
            return;
        }

        public GachaButtonParam(GachaButtonParam _param)
        {
            this.m_ButtonText = string.Empty;
            this.m_AppealText = string.Empty;
            this.m_IsShowStepup = 1;
            base..ctor();
            this.m_Cost = _param.Cost;
            this.m_StepIndex = _param.StepIndex;
            this.m_StepMax = _param.StepMax;
            this.m_TicketNum = _param.TicketNum;
            this.m_ExecNum = _param.ExecNum;
            this.m_AppealType = _param.AppealType;
            this.m_ButtonText = _param.ButtonText;
            this.m_AppealText = _param.AppealText;
            this.m_IsShowStepup = _param.IsShowStepup;
            this.m_IsNoUseFree = _param.IsNoUseFree;
            this.m_CostType = _param.CostType;
            this.m_Category = _param.Category;
            return;
        }

        public GachaButtonParam(int _cost, int _step_index, int _step_max, int _ticket_num, int _exec_num, int _appeal_type, string _button_text, string _appeal_text, bool _is_show_stepup, bool _is_nouse_free, GachaCostType _cost_type, GachaCategory _category)
        {
            this.m_ButtonText = string.Empty;
            this.m_AppealText = string.Empty;
            this.m_IsShowStepup = 1;
            base..ctor();
            this.m_Cost = _cost;
            this.m_StepIndex = _step_index;
            this.m_StepMax = _step_max;
            this.m_TicketNum = _ticket_num;
            this.m_ExecNum = _exec_num;
            this.m_AppealType = _appeal_type;
            this.m_ButtonText = _button_text;
            this.m_AppealText = _appeal_text;
            this.m_IsShowStepup = _is_show_stepup;
            this.m_IsNoUseFree = _is_nouse_free;
            this.m_CostType = _cost_type;
            this.m_Category = _category;
            return;
        }

        public bool IsStepUp()
        {
            return (this.m_StepMax > 0);
        }

        public int Cost
        {
            get
            {
                return this.m_Cost;
            }
        }

        public int StepIndex
        {
            get
            {
                return this.m_StepIndex;
            }
        }

        public int StepMax
        {
            get
            {
                return this.m_StepMax;
            }
        }

        public int TicketNum
        {
            get
            {
                return this.m_TicketNum;
            }
        }

        public int ExecNum
        {
            get
            {
                return this.m_ExecNum;
            }
        }

        public int AppealType
        {
            get
            {
                return this.m_AppealType;
            }
        }

        public string ButtonText
        {
            get
            {
                return this.m_ButtonText;
            }
        }

        public string AppealText
        {
            get
            {
                return this.m_AppealText;
            }
        }

        public bool IsShowStepup
        {
            get
            {
                return this.m_IsShowStepup;
            }
        }

        public GachaCostType CostType
        {
            get
            {
                return this.m_CostType;
            }
        }

        public GachaCategory Category
        {
            get
            {
                return this.m_Category;
            }
        }

        public bool IsNoUseFree
        {
            get
            {
                return ((this.m_Category == null) ? 0 : this.m_IsNoUseFree);
            }
        }
    }
}

