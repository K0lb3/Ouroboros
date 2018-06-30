namespace SRPG
{
    using System;

    public class TipsParam
    {
        public string iname;
        public ETipsType type;
        public int order;
        public string title;
        public string text;
        public string[] images;
        public bool hide;
        public string cond_text;

        public TipsParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TipsParam json)
        {
            this.iname = json.iname;
            this.type = (int) Enum.ToObject(typeof(ETipsType), json.type);
            this.order = json.order;
            this.title = json.title;
            this.text = json.text;
            this.images = json.images;
            this.hide = (json.hide == 0) == 0;
            this.cond_text = json.cond_text;
            return;
        }
    }
}

