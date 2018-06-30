namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FriendPresentReceiveList
    {
        private List<Param> m_List;

        public FriendPresentReceiveList()
        {
            this.m_List = new List<Param>();
            base..ctor();
            return;
        }

        public void Clear()
        {
            this.m_List.Clear();
            return;
        }

        public void Deserialize(Json[] jsons)
        {
            string[] textArray1;
            int num;
            Param param;
            <Deserialize>c__AnonStorey2BE storeybe;
            if (jsons != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            num = 0;
            goto Label_00F8;
        Label_0013:
            storeybe = new <Deserialize>c__AnonStorey2BE();
            storeybe.json = jsons[num];
            if (storeybe.json == null)
            {
                goto Label_00F4;
            }
            param = this.GetParam(storeybe.json.pname);
            if (param == null)
            {
                goto Label_008B;
            }
            param.num += 1;
            if (param.uids.FindIndex(new Predicate<string>(storeybe.<>m__234)) != -1)
            {
                goto Label_00F4;
            }
            param.uids.Add(storeybe.json.fuid);
            goto Label_00F4;
        Label_008B:
            param = new Param();
            param.present = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(storeybe.json.pname);
            param.iname = storeybe.json.pname;
            param.num = 1;
            textArray1 = new string[] { storeybe.json.fuid };
            param.uids = new List<string>(textArray1);
            this.m_List.Add(param);
        Label_00F4:
            num += 1;
        Label_00F8:
            if (num < ((int) jsons.Length))
            {
                goto Label_0013;
            }
            return;
        }

        public Param GetParam(string iname)
        {
            <GetParam>c__AnonStorey2BD storeybd;
            storeybd = new <GetParam>c__AnonStorey2BD();
            storeybd.iname = iname;
            return this.m_List.Find(new Predicate<Param>(storeybd.<>m__233));
        }

        public Param this[int index]
        {
            get
            {
                return ((index >= this.m_List.Count) ? null : this.m_List[index]);
            }
        }

        public int count
        {
            get
            {
                return this.m_List.Count;
            }
        }

        public List<Param> list
        {
            get
            {
                return this.m_List;
            }
        }

        public Param[] array
        {
            get
            {
                return this.m_List.ToArray();
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey2BE
        {
            internal FriendPresentReceiveList.Json json;

            public <Deserialize>c__AnonStorey2BE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__234(string prop)
            {
                return (prop == this.json.fuid);
            }
        }

        [CompilerGenerated]
        private sealed class <GetParam>c__AnonStorey2BD
        {
            internal string iname;

            public <GetParam>c__AnonStorey2BD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__233(FriendPresentReceiveList.Param prop)
            {
                return (prop.iname == this.iname);
            }
        }

        [Serializable]
        public class Json
        {
            public string uid;
            public string fuid;
            public string pname;

            public Json()
            {
                base..ctor();
                return;
            }
        }

        public class Param
        {
            public FriendPresentItemParam present;
            public string iname;
            public int num;
            public List<string> uids;

            public Param()
            {
                base..ctor();
                return;
            }
        }
    }
}

