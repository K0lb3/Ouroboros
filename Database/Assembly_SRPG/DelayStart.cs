namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class DelayStart : MonoBehaviour
    {
        public float ActivateInterval;
        private List<GameObject> mChildren;
        private int mActivateIndex;
        private float mInterval;

        public DelayStart()
        {
            this.ActivateInterval = 0.5f;
            this.mChildren = new List<GameObject>();
            base..ctor();
            return;
        }

        private void Start()
        {
            Transform transform;
            int num;
            int num2;
            transform = base.get_transform();
            num = transform.get_childCount();
            num2 = 0;
            goto Label_0042;
        Label_0015:
            this.mChildren.Add(transform.GetChild(num2).get_gameObject());
            this.mChildren[num2].SetActive(0);
            num2 += 1;
        Label_0042:
            if (num2 < num)
            {
                goto Label_0015;
            }
            this.mInterval = 0f;
            return;
        }

        private void Update()
        {
            int num;
            int num2;
            if (this.mActivateIndex >= this.mChildren.Count)
            {
                goto Label_006C;
            }
            this.mInterval -= Time.get_deltaTime();
            if (this.mInterval > 0f)
            {
                goto Label_00AB;
            }
            this.mChildren[this.mActivateIndex++].SetActive(1);
            this.mInterval = this.ActivateInterval;
            return;
            goto Label_00AB;
        Label_006C:
            num = 0;
            goto Label_008F;
        Label_0073:
            if ((this.mChildren[num] != null) == null)
            {
                goto Label_008B;
            }
            return;
        Label_008B:
            num += 1;
        Label_008F:
            if (num < this.mChildren.Count)
            {
                goto Label_0073;
            }
            Object.Destroy(base.get_gameObject());
        Label_00AB:
            return;
        }
    }
}

