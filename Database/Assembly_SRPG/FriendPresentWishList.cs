namespace SRPG
{
    using GR;
    using System;
    using System.Reflection;

    public class FriendPresentWishList
    {
        private FriendPresentItemParam[] m_Array;

        public FriendPresentWishList()
        {
            this.m_Array = new FriendPresentItemParam[3];
            base..ctor();
            return;
        }

        public void Clear()
        {
            int num;
            num = 0;
            goto Label_0014;
        Label_0007:
            this.m_Array[num] = null;
            num += 1;
        Label_0014:
            if (num < ((int) this.m_Array.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void Deserialize(Json[] jsons)
        {
            int num;
            Json json;
            FriendPresentItemParam param;
            if (jsons != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.Clear();
            num = 0;
            goto Label_009E;
        Label_0019:
            json = jsons[num];
            if (json == null)
            {
                goto Label_009A;
            }
            if (json.priority <= 0)
            {
                goto Label_0073;
            }
            if (json.priority > ((int) this.m_Array.Length))
            {
                goto Label_0073;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(json.iname);
            if (param == null)
            {
                goto Label_009A;
            }
            this.m_Array[json.priority - 1] = param;
            goto Label_009A;
        Label_0073:
            DebugUtility.LogError(string.Format("ウィッシュリスト優先の範囲は 1 ~ {0} まで > {1}", (int) ((int) this.m_Array.Length), (int) json.priority));
        Label_009A:
            num += 1;
        Label_009E:
            if (num < ((int) jsons.Length))
            {
                goto Label_0019;
            }
            return;
        }

        public void Set(string iname, int priority)
        {
            this.m_Array[priority] = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(iname);
            return;
        }

        public FriendPresentItemParam this[int index]
        {
            get
            {
                return (((this.m_Array == null) || (index >= ((int) this.m_Array.Length))) ? null : this.m_Array[index]);
            }
        }

        public int count
        {
            get
            {
                return ((this.m_Array == null) ? 0 : ((int) this.m_Array.Length));
            }
        }

        public FriendPresentItemParam[] array
        {
            get
            {
                return this.m_Array;
            }
        }

        [Serializable]
        public class Json
        {
            public string iname;
            public int priority;

            public Json()
            {
                base..ctor();
                return;
            }
        }
    }
}

