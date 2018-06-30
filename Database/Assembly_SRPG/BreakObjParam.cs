namespace SRPG
{
    using System;

    public class BreakObjParam
    {
        private string mIname;
        private string mName;
        private string mExpr;
        private string mUnitId;
        private eMapBreakClashType mClashType;
        private eMapBreakAIType mAiType;
        private eMapBreakSideType mSideType;
        private eMapBreakRayType mRayType;
        private bool mIsUI;
        private int[] mRestHps;
        private int mAliveClock;
        private EUnitDirection mAppearDir;

        public BreakObjParam()
        {
            base..ctor();
            return;
        }

        public unsafe void Deserialize(JSON_BreakObjParam json)
        {
            char[] chArray1;
            string[] strArray;
            int num;
            int num2;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mIname = json.iname;
            this.mName = json.name;
            this.mExpr = json.expr;
            this.mUnitId = json.unit_id;
            this.mClashType = json.clash_type;
            this.mAiType = json.ai_type;
            this.mSideType = json.side_type;
            this.mRayType = json.ray_type;
            this.mIsUI = (json.is_ui == 0) == 0;
            this.mRestHps = null;
            if (string.IsNullOrEmpty(json.rest_hps) != null)
            {
                goto Label_00ED;
            }
            chArray1 = new char[] { 0x2c };
            strArray = json.rest_hps.Split(chArray1);
            if (strArray == null)
            {
                goto Label_00ED;
            }
            if (((int) strArray.Length) == null)
            {
                goto Label_00ED;
            }
            this.mRestHps = new int[(int) strArray.Length];
            num = 0;
            goto Label_00E4;
        Label_00CA:
            num2 = 0;
            int.TryParse(strArray[num], &num2);
            this.mRestHps[num] = num2;
            num += 1;
        Label_00E4:
            if (num < ((int) strArray.Length))
            {
                goto Label_00CA;
            }
        Label_00ED:
            this.mAliveClock = json.clock;
            this.mAppearDir = json.appear_dir;
            return;
        }

        public string Iname
        {
            get
            {
                return this.mIname;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }

        public string Expr
        {
            get
            {
                return this.mExpr;
            }
        }

        public string UnitId
        {
            get
            {
                return this.mUnitId;
            }
        }

        public eMapBreakClashType ClashType
        {
            get
            {
                return this.mClashType;
            }
        }

        public eMapBreakAIType AiType
        {
            get
            {
                return this.mAiType;
            }
        }

        public eMapBreakSideType SideType
        {
            get
            {
                return this.mSideType;
            }
        }

        public eMapBreakRayType RayType
        {
            get
            {
                return this.mRayType;
            }
        }

        public bool IsUI
        {
            get
            {
                return this.mIsUI;
            }
        }

        public int[] RestHps
        {
            get
            {
                return this.mRestHps;
            }
        }

        public int AliveClock
        {
            get
            {
                return this.mAliveClock;
            }
        }

        public EUnitDirection AppearDir
        {
            get
            {
                return this.mAppearDir;
            }
        }
    }
}

