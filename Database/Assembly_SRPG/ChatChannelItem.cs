namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ChatChannelItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject Begginer;
        [SerializeField]
        private GameObject ChannelName;
        [SerializeField]
        private GameObject Fever;
        private int mChannelID;

        public ChatChannelItem()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.Begginer != null) == null)
            {
                goto Label_001D;
            }
            this.Begginer.SetActive(0);
        Label_001D:
            if ((this.ChannelName != null) == null)
            {
                goto Label_003A;
            }
            this.ChannelName.SetActive(0);
        Label_003A:
            if ((this.Fever != null) == null)
            {
                goto Label_0057;
            }
            this.Fever.SetActive(0);
        Label_0057:
            return;
        }

        public unsafe void Refresh(ChatChannelParam param)
        {
            string str;
            Text text;
            ImageArray array;
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (param.category_id != 2)
            {
                goto Label_001F;
            }
            this.Begginer.SetActive(1);
        Label_001F:
            this.mChannelID = param.id;
            str = "CH " + &param.id.ToString();
            if (string.IsNullOrEmpty(param.name) != null)
            {
                goto Label_0058;
            }
            str = param.name;
        Label_0058:
            this.ChannelName.get_transform().FindChild("text").GetComponent<Text>().set_text(str);
            this.ChannelName.SetActive(1);
            array = this.Fever.GetComponent<ImageArray>();
            if (param.fever_level < 15)
            {
                goto Label_00AB;
            }
            array.ImageIndex = 2;
            goto Label_00CB;
        Label_00AB:
            if (param.fever_level < 10)
            {
                goto Label_00C4;
            }
            array.ImageIndex = 1;
            goto Label_00CB;
        Label_00C4:
            array.ImageIndex = 0;
        Label_00CB:
            this.Fever.SetActive(1);
            return;
        }

        public int ChannelID
        {
            get
            {
                return this.mChannelID;
            }
        }
    }
}

