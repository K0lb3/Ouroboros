namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [EventActionInfo("New/SE再生(2D)", "SEを再生します", 0x555555, 0x444488)]
    public class Event2dAction_SELoop : EventAction
    {
        public static List<Event2dAction_SELoop> instances;
        public string SE;
        public bool Loop;
        [HideInInspector]
        public float Interval;
        [HideInInspector]
        public int Count;
        [HideInInspector]
        public bool async;
        [HideInInspector]
        public string SE_ID;
        private static readonly float MinInterval;
        private static readonly int MinCount;
        private float mTimer;
        private int mCounter;
        private MySound.CueSheetHandle mHandleSE;
        private static readonly string SECueSheetName;

        static Event2dAction_SELoop()
        {
            instances = new List<Event2dAction_SELoop>();
            MinInterval = 0.1f;
            MinCount = 0;
            SECueSheetName = "SE";
            return;
        }

        public Event2dAction_SELoop()
        {
            this.Interval = 1f;
            this.Count = 1;
            base..ctor();
            return;
        }

        public static Event2dAction_SELoop Find(string id)
        {
            int num;
            num = 0;
            goto Label_0032;
        Label_0007:
            if ((instances[num].SE_ID == id) == null)
            {
                goto Label_002E;
            }
            return instances[num];
        Label_002E:
            num += 1;
        Label_0032:
            if (num < instances.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public override void OnActivate()
        {
            if (this.Loop == null)
            {
                goto Label_0039;
            }
            this.mTimer = 0f;
            this.mCounter = this.Count;
            if (this.async == null)
            {
                goto Label_0062;
            }
            base.ActivateNext(1);
            goto Label_0062;
        Label_0039:
            if (this.mHandleSE == null)
            {
                goto Label_005C;
            }
            this.mHandleSE.PlayDefaultOneShot(this.SE, 0, 0f, 0);
        Label_005C:
            base.ActivateNext();
        Label_0062:
            return;
        }

        public override void PreStart()
        {
            this.mHandleSE = MySound.CueSheetHandle.Create(SECueSheetName, 2, 1, 1, 0, 0);
            if (this.mHandleSE == null)
            {
                goto Label_0036;
            }
            this.mHandleSE.CreateDefaultOneShotSource();
            instances.Add(this);
        Label_0036:
            return;
        }

        public override void Update()
        {
            int num;
            this.mTimer -= Time.get_deltaTime();
            if (this.mTimer > 0f)
            {
                goto Label_0091;
            }
            if (this.mHandleSE == null)
            {
                goto Label_0045;
            }
            this.mHandleSE.PlayDefaultOneShot(this.SE, 0, 0f, 0);
        Label_0045:
            this.mTimer = this.Interval;
            if (this.mCounter != null)
            {
                goto Label_005D;
            }
            return;
        Label_005D:
            if ((this.mCounter -= 1) > 0)
            {
                goto Label_0091;
            }
            if (this.async == null)
            {
                goto Label_008B;
            }
            base.enabled = 0;
            goto Label_0091;
        Label_008B:
            base.ActivateNext();
        Label_0091:
            return;
        }

        public MySound.CueSheetHandle HandleSE
        {
            get
            {
                return this.mHandleSE;
            }
        }
    }
}

