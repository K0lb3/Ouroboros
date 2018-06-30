namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ArtifactJobList : MonoBehaviour, IGameParameter
    {
        public GameObject ListItem;
        public GameObject AnyJob;
        private List<JobParam> mCurrentJobs;
        private List<GameObject> mJobListItems;

        public ArtifactJobList()
        {
            this.mCurrentJobs = new List<JobParam>(8);
            this.mJobListItems = new List<GameObject>(8);
            base..ctor();
            return;
        }

        private void OnDestroy()
        {
            GlobalVars.ConditionJobs = null;
            return;
        }

        private void Start()
        {
            if ((this.ListItem != null) == null)
            {
                goto Label_001D;
            }
            this.ListItem.SetActive(0);
        Label_001D:
            if ((this.AnyJob != null) == null)
            {
                goto Label_003A;
            }
            this.AnyJob.SetActive(0);
        Label_003A:
            return;
        }

        public void UpdateValue()
        {
            string[] strArray;
            MasterParam param;
            int num;
            int num2;
            JobParam param2;
            GameObject obj2;
            ArtifactJobItem item;
            int num3;
            if ((this.ListItem == null) != null)
            {
                goto Label_0022;
            }
            if ((this.AnyJob == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            if ((this.ListItem != null) == null)
            {
                goto Label_0040;
            }
            this.ListItem.SetActive(0);
        Label_0040:
            if ((this.AnyJob != null) == null)
            {
                goto Label_005D;
            }
            this.AnyJob.SetActive(0);
        Label_005D:
            strArray = GlobalVars.ConditionJobs;
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num = 0;
            if (strArray == null)
            {
                goto Label_0184;
            }
            num2 = 0;
            goto Label_017B;
        Label_007D:
            param2 = null;
            if (string.IsNullOrEmpty(strArray[num2]) == null)
            {
                goto Label_0092;
            }
            goto Label_0177;
        Label_0092:
            try
            {
                param2 = param.GetJobParam(strArray[num2]);
                goto Label_00AD;
            }
            catch (Exception)
            {
            Label_00A2:
                goto Label_0177;
            }
        Label_00AD:
            if (param2 != null)
            {
                goto Label_00B9;
            }
            goto Label_0177;
        Label_00B9:
            if (this.mJobListItems.Count > num)
            {
                goto Label_0103;
            }
            obj2 = Object.Instantiate<GameObject>(this.ListItem);
            obj2.get_transform().SetParent(base.get_transform(), 0);
            this.mJobListItems.Add(obj2);
            this.mCurrentJobs.Add(null);
        Label_0103:
            this.mJobListItems[num].SetActive(1);
            if (this.mCurrentJobs[num] != param2)
            {
                goto Label_0131;
            }
            num += 1;
            goto Label_0177;
        Label_0131:
            this.mCurrentJobs[num] = param2;
            item = this.mJobListItems[num].GetComponent<ArtifactJobItem>();
            DataSource.Bind<JobParam>(item.jobIcon, param2);
            item.jobName.set_text(param2.name);
            num += 1;
        Label_0177:
            num2 += 1;
        Label_017B:
            if (num2 < ((int) strArray.Length))
            {
                goto Label_007D;
            }
        Label_0184:
            num3 = num;
            goto Label_01A5;
        Label_018C:
            this.mJobListItems[num3].SetActive(0);
            num3 += 1;
        Label_01A5:
            if (num3 < this.mJobListItems.Count)
            {
                goto Label_018C;
            }
            if ((this.AnyJob != null) == null)
            {
                goto Label_01D7;
            }
            this.AnyJob.SetActive(num == 0);
        Label_01D7:
            return;
        }
    }
}

