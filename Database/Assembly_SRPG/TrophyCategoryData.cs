namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class TrophyCategoryData
    {
        private TrophyCategoryParam category_param;
        private List<TrophyState> trophies;
        private List<TrophyState> tmp1_trophies;
        private List<TrophyState> tmp2_trophies;
        private bool is_in_completed_data;

        public TrophyCategoryData(TrophyCategoryParam _tcp)
        {
            base..ctor();
            this.is_in_completed_data = 0;
            this.category_param = _tcp;
            this.trophies = new List<TrophyState>();
            this.tmp1_trophies = new List<TrophyState>();
            this.tmp2_trophies = new List<TrophyState>();
            return;
        }

        public void AddTrophy(TrophyState _trophy)
        {
            this.trophies.Add(_trophy);
            if (_trophy.IsCompleted == null)
            {
                goto Label_002F;
            }
            this.tmp1_trophies.Add(_trophy);
            this.is_in_completed_data = 1;
            goto Label_003B;
        Label_002F:
            this.tmp2_trophies.Add(_trophy);
        Label_003B:
            return;
        }

        public void Apply()
        {
            List<TrophyState> list;
            this.trophies.Clear();
            this.trophies.AddRange(this.tmp1_trophies);
            this.trophies.AddRange(this.tmp2_trophies);
            this.tmp1_trophies.Clear();
            this.tmp2_trophies.Clear();
            this.tmp1_trophies = this.tmp2_trophies = null;
            return;
        }

        public void RemoveTrophy(TrophyState _trophy)
        {
            if (this.trophies.Contains(_trophy) == null)
            {
                goto Label_001E;
            }
            this.trophies.Remove(_trophy);
        Label_001E:
            if (this.tmp1_trophies.Contains(_trophy) == null)
            {
                goto Label_003C;
            }
            this.tmp1_trophies.Remove(_trophy);
        Label_003C:
            if (this.tmp2_trophies.Contains(_trophy) == null)
            {
                goto Label_005A;
            }
            this.tmp2_trophies.Remove(_trophy);
        Label_005A:
            return;
        }

        public TrophyCategoryParam Param
        {
            get
            {
                return this.category_param;
            }
        }

        public List<TrophyState> Trophies
        {
            get
            {
                return this.trophies;
            }
        }

        public bool IsInCompletedData
        {
            get
            {
                return this.is_in_completed_data;
            }
        }
    }
}

