namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MultiConceptCard
    {
        private List<OLong> mMultiSelectedUniqueID;

        public MultiConceptCard()
        {
            this.mMultiSelectedUniqueID = new List<OLong>();
            base..ctor();
            return;
        }

        public void Add(ConceptCardData ccd)
        {
            if (ccd != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.mMultiSelectedUniqueID.Contains(ccd.UniqueID) == null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            this.mMultiSelectedUniqueID.Add(ccd.UniqueID);
            return;
        }

        public void Clear()
        {
            this.mMultiSelectedUniqueID.Clear();
            return;
        }

        public void Clone(MultiConceptCard mbase)
        {
            this.mMultiSelectedUniqueID = new List<OLong>(mbase.mMultiSelectedUniqueID);
            return;
        }

        public bool Contains(long uniqueID)
        {
            return this.mMultiSelectedUniqueID.Contains(uniqueID);
        }

        public void Flip(ConceptCardData ccd)
        {
            if (this.IsSelected(ccd) != null)
            {
                goto Label_0018;
            }
            this.Add(ccd);
            goto Label_001F;
        Label_0018:
            this.Remove(ccd);
        Label_001F:
            return;
        }

        public List<OLong> GetIDList()
        {
            return this.mMultiSelectedUniqueID;
        }

        public ConceptCardData GetItem(int index)
        {
            <GetItem>c__AnonStorey329 storey;
            storey = new <GetItem>c__AnonStorey329();
            if (this.mMultiSelectedUniqueID.Count > index)
            {
                goto Label_0019;
            }
            return null;
        Label_0019:
            storey.uid = this.mMultiSelectedUniqueID[index];
            return MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find(new Predicate<ConceptCardData>(storey.<>m__2DE));
        }

        public unsafe List<ConceptCardData> GetList()
        {
            List<ConceptCardData> list;
            List<OLong>.Enumerator enumerator;
            ConceptCardData data;
            <GetList>c__AnonStorey328 storey;
            list = new List<ConceptCardData>();
            storey = new <GetList>c__AnonStorey328();
            enumerator = this.mMultiSelectedUniqueID.GetEnumerator();
        Label_0018:
            try
            {
                goto Label_0052;
            Label_001D:
                storey.uid = &enumerator.Current;
                data = MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find(new Predicate<ConceptCardData>(storey.<>m__2DD));
                list.Add(data);
            Label_0052:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001D;
                }
                goto Label_006F;
            }
            finally
            {
            Label_0063:
                ((List<OLong>.Enumerator) enumerator).Dispose();
            }
        Label_006F:
            return list;
        }

        public unsafe List<long> GetUniqueIDs()
        {
            List<long> list;
            OLong @long;
            List<OLong>.Enumerator enumerator;
            long num;
            list = new List<long>();
            enumerator = this.mMultiSelectedUniqueID.GetEnumerator();
        Label_0012:
            try
            {
                goto Label_002D;
            Label_0017:
                @long = &enumerator.Current;
                num = @long;
                list.Add(num);
            Label_002D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0017;
                }
                goto Label_004A;
            }
            finally
            {
            Label_003E:
                ((List<OLong>.Enumerator) enumerator).Dispose();
            }
        Label_004A:
            return list;
        }

        public bool IsSelected(ConceptCardData ccd)
        {
            if (ccd != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (this.mMultiSelectedUniqueID.Contains(ccd.UniqueID) == null)
            {
                goto Label_0020;
            }
            return 1;
        Label_0020:
            return 0;
        }

        public void Remove(ConceptCardData ccd)
        {
            if (ccd != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.mMultiSelectedUniqueID.Contains(ccd.UniqueID) != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            this.mMultiSelectedUniqueID.Remove(ccd.UniqueID);
            return;
        }

        public void Remove(long uniqueID)
        {
            this.mMultiSelectedUniqueID.Remove(uniqueID);
            return;
        }

        public void SetArray(ConceptCardData[] array)
        {
            ConceptCardData data;
            ConceptCardData[] dataArray;
            int num;
            this.mMultiSelectedUniqueID.Clear();
            if (array != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            dataArray = array;
            num = 0;
            goto Label_0034;
        Label_001B:
            data = dataArray[num];
            this.mMultiSelectedUniqueID.Add(data.UniqueID);
            num += 1;
        Label_0034:
            if (num < ((int) dataArray.Length))
            {
                goto Label_001B;
            }
            return;
        }

        public int Count
        {
            get
            {
                return this.mMultiSelectedUniqueID.Count;
            }
        }

        [CompilerGenerated]
        private sealed class <GetItem>c__AnonStorey329
        {
            internal OLong uid;

            public <GetItem>c__AnonStorey329()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2DE(ConceptCardData obj)
            {
                return (obj.UniqueID == this.uid);
            }
        }

        [CompilerGenerated]
        private sealed class <GetList>c__AnonStorey328
        {
            internal OLong uid;

            public <GetList>c__AnonStorey328()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2DD(ConceptCardData obj)
            {
                return (obj.UniqueID == this.uid);
            }
        }
    }
}

