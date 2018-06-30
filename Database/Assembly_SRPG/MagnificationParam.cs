namespace SRPG
{
    using System;

    public class MagnificationParam
    {
        public string iname;
        public int[] atkMagnifications;

        public MagnificationParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_MagnificationParam json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            this.atkMagnifications = json.atk;
            return;
        }
    }
}

