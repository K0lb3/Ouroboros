namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ArtifactJobs : MonoBehaviour, IGameParameter
    {
        public GameObject ListItem;
        public GameObject AnyJob;
        private List<JobParam> mCurrentJobs;
        private List<GameObject> mJobListItems;
        private bool mUpdated;

        public ArtifactJobs()
        {
            this.mCurrentJobs = new List<JobParam>(8);
            this.mJobListItems = new List<GameObject>(8);
            base..ctor();
            return;
        }

        private void Start()
        {
            if ((this.ListItem != null) == null)
            {
                goto Label_002D;
            }
            if (this.ListItem.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.ListItem.SetActive(0);
        Label_002D:
            if (this.mUpdated != null)
            {
                goto Label_003E;
            }
            this.UpdateValue();
        Label_003E:
            return;
        }

        public void UpdateValue()
        {
            ArtifactParam param;
            ArtifactData data;
            int num;
            Transform transform;
            MasterParam param2;
            string[] strArray;
            int num2;
            int num3;
            JobParam param3;
            GameObject obj2;
            int num4;
            if ((this.ListItem == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0031;
            }
            param = data.ArtifactParam;
            goto Label_003E;
        Label_0031:
            param = DataSource.FindDataOfClass<ArtifactParam>(base.get_gameObject(), null);
        Label_003E:
            if (param != null)
            {
                goto Label_0073;
            }
            num = 0;
            goto Label_0061;
        Label_004B:
            this.mJobListItems[num].SetActive(0);
            num += 1;
        Label_0061:
            if (num < this.mJobListItems.Count)
            {
                goto Label_004B;
            }
            return;
        Label_0073:
            transform = base.get_transform();
            param2 = MonoSingleton<GameManager>.Instance.MasterParam;
            strArray = param.condition_jobs;
            num2 = 0;
            if (strArray == null)
            {
                goto Label_01A5;
            }
            num3 = 0;
            goto Label_019A;
        Label_00A0:
            param3 = null;
            if (string.IsNullOrEmpty(strArray[num3]) == null)
            {
                goto Label_00B7;
            }
            goto Label_0194;
        Label_00B7:
            try
            {
                param3 = param2.GetJobParam(strArray[num3]);
                goto Label_00D5;
            }
            catch (Exception)
            {
            Label_00CA:
                goto Label_0194;
            }
        Label_00D5:
            if (param3 != null)
            {
                goto Label_00E1;
            }
            goto Label_0194;
        Label_00E1:
            if (this.mJobListItems.Count > num2)
            {
                goto Label_0127;
            }
            obj2 = Object.Instantiate<GameObject>(this.ListItem);
            obj2.get_transform().SetParent(transform, 0);
            this.mJobListItems.Add(obj2);
            this.mCurrentJobs.Add(null);
        Label_0127:
            this.mJobListItems[num2].SetActive(1);
            if (this.mCurrentJobs[num2] != param3)
            {
                goto Label_0159;
            }
            num2 += 1;
            goto Label_0194;
        Label_0159:
            this.mCurrentJobs[num2] = param3;
            DataSource.Bind<JobParam>(this.mJobListItems[num2], param3);
            GameParameter.UpdateAll(this.mJobListItems[num2]);
            num2 += 1;
        Label_0194:
            num3 += 1;
        Label_019A:
            if (num3 < ((int) strArray.Length))
            {
                goto Label_00A0;
            }
        Label_01A5:
            num4 = num2;
            goto Label_01C7;
        Label_01AE:
            this.mJobListItems[num4].SetActive(0);
            num4 += 1;
        Label_01C7:
            if (num4 < this.mJobListItems.Count)
            {
                goto Label_01AE;
            }
            if ((this.AnyJob != null) == null)
            {
                goto Label_01FA;
            }
            this.AnyJob.SetActive(num2 == 0);
        Label_01FA:
            this.mUpdated = 1;
            return;
        }
    }
}

