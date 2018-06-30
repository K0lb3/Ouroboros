namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("UI/UnitJobClassChange")]
    public class UnitJobClassChange : MonoBehaviour
    {
        public string PrevJobID;
        public string NextJobID;
        public GameObject PrevJob;
        public GameObject NextJob;
        [NonSerialized]
        public int CurrentRank;
        private int mCurrentJobRank;
        private int mNextJobRank;
        public Text NewRank;
        public Text OldRank;

        public UnitJobClassChange()
        {
            base..ctor();
            return;
        }

        private unsafe void Start()
        {
            JobParam param;
            JobParam param2;
            UnitData data;
            JobData data2;
            int num;
            JobData data3;
            int num2;
            int num3;
            param = null;
            param2 = null;
            if (string.IsNullOrEmpty(this.PrevJobID) == null)
            {
                goto Label_00C9;
            }
            if (string.IsNullOrEmpty(this.NextJobID) == null)
            {
                goto Label_00C9;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            if (data != null)
            {
                goto Label_0045;
            }
            return;
        Label_0045:
            data2 = null;
            num = 0;
            goto Label_008F;
        Label_004F:
            if (data.Jobs[num] == null)
            {
                goto Label_0089;
            }
            if (data.Jobs[num].UniqueID == GlobalVars.SelectedJobUniqueID)
            {
                goto Label_007F;
            }
            goto Label_0089;
        Label_007F:
            data2 = data.Jobs[num];
        Label_0089:
            num += 1;
        Label_008F:
            if (num < ((int) data.Jobs.Length))
            {
                goto Label_004F;
            }
            if (data2 != null)
            {
                goto Label_00A5;
            }
            return;
        Label_00A5:
            data3 = data.GetBaseJob(data2.JobID);
            if (data3 == null)
            {
                goto Label_00C2;
            }
            param = data3.Param;
        Label_00C2:
            param2 = data2.Param;
        Label_00C9:
            if (string.IsNullOrEmpty(this.PrevJobID) != null)
            {
                goto Label_00EF;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.PrevJobID);
        Label_00EF:
            if (string.IsNullOrEmpty(this.NextJobID) != null)
            {
                goto Label_0115;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.NextJobID);
        Label_0115:
            if ((this.NextJob != null) == null)
            {
                goto Label_013D;
            }
            DataSource.Bind<JobParam>(this.NextJob, param2);
            GameParameter.UpdateAll(this.NextJob);
        Label_013D:
            if ((this.PrevJob != null) == null)
            {
                goto Label_0165;
            }
            DataSource.Bind<JobParam>(this.PrevJob, param);
            GameParameter.UpdateAll(this.PrevJob);
        Label_0165:
            if ((this.NewRank != null) == null)
            {
                goto Label_0190;
            }
            this.NewRank.set_text(&this.NextJobRank.ToString());
        Label_0190:
            if ((this.OldRank != null) == null)
            {
                goto Label_01BB;
            }
            this.OldRank.set_text(&this.CurrentJobRank.ToString());
        Label_01BB:
            return;
        }

        public int CurrentJobRank
        {
            get
            {
                return this.mCurrentJobRank;
            }
            set
            {
                this.mCurrentJobRank = value;
                return;
            }
        }

        public int NextJobRank
        {
            get
            {
                return this.mNextJobRank;
            }
            set
            {
                this.mNextJobRank = value;
                return;
            }
        }
    }
}

