namespace SRPG
{
    using System;

    public class ObjectiveParam
    {
        public string iname;
        public JSON_InnerObjective[] objective;

        public ObjectiveParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_ObjectiveParam json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            if (json.objective != null)
            {
                goto Label_0029;
            }
            throw new InvalidJSONException();
        Label_0029:
            this.objective = json.objective;
            return;
        }
    }
}

