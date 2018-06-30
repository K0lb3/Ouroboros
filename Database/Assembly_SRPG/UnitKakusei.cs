namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitKakusei : MonoBehaviour
    {
        public GameObject JobUnlock;
        public GameObject[] JobUnlocks;
        public JobParam UnlockJobParam;
        public JobParam[] UnlockJobParams;
        public Text LevelValue;
        public Text CombValue;
        [CompilerGenerated]
        private int <UpdateLevelValue>k__BackingField;
        [CompilerGenerated]
        private int <UpdateCombValue>k__BackingField;

        public UnitKakusei()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            int num;
            if ((this.JobUnlock != null) == null)
            {
                goto Label_001D;
            }
            this.JobUnlock.SetActive(0);
        Label_001D:
            if (this.JobUnlocks == null)
            {
                goto Label_005D;
            }
            if (((int) this.JobUnlocks.Length) <= 0)
            {
                goto Label_005D;
            }
            num = 0;
            goto Label_004F;
        Label_003D:
            this.JobUnlocks[num].SetActive(0);
            num += 1;
        Label_004F:
            if (num < ((int) this.JobUnlocks.Length))
            {
                goto Label_003D;
            }
        Label_005D:
            return;
        }

        private unsafe void Start()
        {
            int num;
            int num2;
            int num3;
            if (this.UnlockJobParams == null)
            {
                goto Label_0070;
            }
            if (((int) this.UnlockJobParams.Length) <= 0)
            {
                goto Label_0070;
            }
            num = 0;
            goto Label_0062;
        Label_0020:
            if (num >= ((int) this.JobUnlocks.Length))
            {
                goto Label_005E;
            }
            if (this.UnlockJobParams[num] == null)
            {
                goto Label_005E;
            }
            DataSource.Bind<JobParam>(this.JobUnlocks[num], this.UnlockJobParams[num]);
            this.JobUnlocks[num].SetActive(1);
        Label_005E:
            num += 1;
        Label_0062:
            if (num < ((int) this.UnlockJobParams.Length))
            {
                goto Label_0020;
            }
        Label_0070:
            if ((this.LevelValue != null) == null)
            {
                goto Label_00B0;
            }
            if (this.UpdateLevelValue <= 0)
            {
                goto Label_00B0;
            }
            this.LevelValue.set_text("+" + &this.UpdateLevelValue.ToString());
        Label_00B0:
            if ((this.CombValue != null) == null)
            {
                goto Label_00F0;
            }
            if (this.UpdateCombValue <= 0)
            {
                goto Label_00F0;
            }
            this.CombValue.set_text("+" + &this.UpdateCombValue.ToString());
        Label_00F0:
            return;
        }

        public int UpdateLevelValue
        {
            [CompilerGenerated]
            get
            {
                return this.<UpdateLevelValue>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<UpdateLevelValue>k__BackingField = value;
                return;
            }
        }

        public int UpdateCombValue
        {
            [CompilerGenerated]
            get
            {
                return this.<UpdateCombValue>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<UpdateCombValue>k__BackingField = value;
                return;
            }
        }
    }
}

