namespace SRPG
{
    using GR;
    using System;

    public class ConditionsResult_JobLv : ConditionsResult_Unit
    {
        public string mCondsJobIname;
        public int mCondsJobLv;
        public JobData mJobData;
        public JobParam mJobParam;

        public ConditionsResult_JobLv(UnitData unitData, UnitParam unitParam, string condsJobIname, int condsJobLv)
        {
            JobParam param;
            JobData data;
            JobData[] dataArray;
            int num;
            base..ctor(unitData, unitParam);
            this.mCondsJobLv = condsJobLv;
            this.mCondsJobIname = condsJobIname;
            base.mTargetValue = condsJobLv;
            param = MonoSingleton<GameManager>.Instance.GetJobParam(condsJobIname);
            if (param == null)
            {
                goto Label_0038;
            }
            this.mJobParam = param;
        Label_0038:
            if (unitData == null)
            {
                goto Label_00AC;
            }
            dataArray = unitData.Jobs;
            num = 0;
            goto Label_009E;
        Label_004C:
            data = dataArray[num];
            if ((data.Param.iname == this.mCondsJobIname) == null)
            {
                goto Label_009A;
            }
            this.mJobData = data;
            base.mIsClear = (data.Rank < this.mCondsJobLv) == 0;
            base.mCurrentValue = data.Rank;
            goto Label_00A7;
        Label_009A:
            num += 1;
        Label_009E:
            if (num < ((int) dataArray.Length))
            {
                goto Label_004C;
            }
        Label_00A7:
            goto Label_00B3;
        Label_00AC:
            base.mIsClear = 0;
        Label_00B3:
            return;
        }

        public override string text
        {
            get
            {
                object[] objArray1;
                objArray1 = new object[] { base.unitName, this.mJobParam.name, (int) this.mCondsJobLv };
                return LocalizedText.Get("sys.TOBIRA_CONDITIONS_JOB_LEVEL", objArray1);
            }
        }

        public override string errorText
        {
            get
            {
                if (this.mJobData == null)
                {
                    goto Label_001C;
                }
                return string.Format("ユニット「{0}」を所持していません", base.unitName);
            Label_001C:
                return string.Format("ユニット「{0}」のジョブ「{1}」が解放されていません", base.unitName, this.mJobParam.name);
            }
        }
    }
}

