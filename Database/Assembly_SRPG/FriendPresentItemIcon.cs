namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class FriendPresentItemIcon : MonoBehaviour
    {
        [CustomGroup("期間"), CustomField("ルート", 1)]
        public GameObject m_TimeLimitObject;
        [CustomGroup("期間"), CustomField("時間", 2)]
        public GameObject m_TimeLimitValueObject;
        [CustomField("名前", 2), CustomGroup("アイテム")]
        public GameObject m_NameObject;
        [CustomField("フレーム", 4), CustomGroup("アイテム")]
        public GameObject m_FrameObject;
        [CustomGroup("アイテム"), CustomField("アイテムアイコン", 3)]
        public GameObject m_IconObject;
        [CustomGroup("アイテム"), CustomField("個数", 2)]
        public GameObject m_AmountObject;
        [CustomGroup("アイテム"), CustomField("ゼニーアイコン", 4)]
        public GameObject m_CoinIconObject;
        [CustomGroup("アイテム"), CustomField("ゼニー", 2)]
        public GameObject m_ZenyObject;
        [CustomGroup("アイテム"), CustomField("設定テキスト", 1)]
        public GameObject m_SettingTextObject;
        [CustomField("ルート", 1), CustomGroup("所持数")]
        public GameObject m_NumObject;
        [CustomGroup("所持数"), CustomField("個数", 2)]
        public GameObject m_NumValueObject;
        private FriendPresentItemParam m_Present;
        private ItemData m_ItemData;

        public FriendPresentItemIcon()
        {
            base..ctor();
            return;
        }

        public void Bind(FriendPresentItemParam present, bool checkTimeLimit)
        {
            <Bind>c__AnonStorey33D storeyd;
            storeyd = new <Bind>c__AnonStorey33D();
            storeyd.present = present;
            if (storeyd.present != null)
            {
                goto Label_0039;
            }
            storeyd.present = FriendPresentItemParam.DefaultParam;
            if (storeyd.present != null)
            {
                goto Label_0039;
            }
            Debug.LogError("フレンドプレゼントのデフォルトパラメータが存在しません!");
            return;
        Label_0039:
            if (checkTimeLimit == null)
            {
                goto Label_005F;
            }
            if (storeyd.present.IsValid(Network.GetServerTime()) != null)
            {
                goto Label_005F;
            }
            storeyd.present = FriendPresentItemParam.DefaultParam;
        Label_005F:
            this.m_Present = storeyd.present;
            if (this.m_Present.item == null)
            {
                goto Label_00D1;
            }
            this.m_ItemData = MonoSingleton<GameManager>.Instance.Player.Items.Find(new Predicate<ItemData>(storeyd.<>m__31D));
            if (this.m_ItemData != null)
            {
                goto Label_00D1;
            }
            this.m_ItemData = new ItemData();
            this.m_ItemData.Setup(0L, this.m_Present.item, 0);
        Label_00D1:
            return;
        }

        public void Clear()
        {
            this.m_Present = null;
            this.m_ItemData = null;
            return;
        }

        public void Refresh()
        {
            IconLoader loader;
            if (this.m_Present != null)
            {
                goto Label_00E9;
            }
            if ((this.m_TimeLimitObject != null) == null)
            {
                goto Label_0028;
            }
            this.m_TimeLimitObject.SetActive(0);
        Label_0028:
            if ((this.m_NameObject != null) == null)
            {
                goto Label_0045;
            }
            this.m_NameObject.SetActive(0);
        Label_0045:
            if ((this.m_IconObject != null) == null)
            {
                goto Label_0062;
            }
            this.m_IconObject.SetActive(0);
        Label_0062:
            if ((this.m_CoinIconObject != null) == null)
            {
                goto Label_007F;
            }
            this.m_CoinIconObject.SetActive(0);
        Label_007F:
            if ((this.m_NumObject != null) == null)
            {
                goto Label_009C;
            }
            this.m_NumObject.SetActive(0);
        Label_009C:
            if ((this.m_SettingTextObject != null) == null)
            {
                goto Label_00B9;
            }
            this.m_SettingTextObject.SetActive(1);
        Label_00B9:
            if ((this.m_FrameObject != null) == null)
            {
                goto Label_00E8;
            }
            this.SetSprite(this.m_FrameObject, GameSettings.Instance.GetItemFrame(12, 0), Color.get_gray());
        Label_00E8:
            return;
        Label_00E9:
            if ((this.m_SettingTextObject != null) == null)
            {
                goto Label_0106;
            }
            this.m_SettingTextObject.SetActive(0);
        Label_0106:
            if ((this.m_TimeLimitObject != null) == null)
            {
                goto Label_012D;
            }
            this.m_TimeLimitObject.SetActive(this.m_Present.HasTimeLimit());
        Label_012D:
            if ((this.m_TimeLimitValueObject != null) == null)
            {
                goto Label_0155;
            }
            this.SetRestTime(this.m_TimeLimitValueObject, this.m_Present.end_at);
        Label_0155:
            if ((this.m_NameObject != null) == null)
            {
                goto Label_017D;
            }
            this.SetText(this.m_NameObject, this.m_Present.name);
        Label_017D:
            if (this.m_ItemData == null)
            {
                goto Label_0299;
            }
            if ((this.m_FrameObject != null) == null)
            {
                goto Label_01BF;
            }
            this.SetSprite(this.m_FrameObject, GameSettings.Instance.GetItemFrame(this.m_Present.item), Color.get_white());
        Label_01BF:
            if ((this.m_IconObject != null) == null)
            {
                goto Label_01FE;
            }
            this.m_IconObject.SetActive(1);
            GameUtility.RequireComponent<IconLoader>(this.m_IconObject).ResourcePath = AssetPath.ItemIcon(this.m_ItemData.Param);
        Label_01FE:
            if ((this.m_CoinIconObject != null) == null)
            {
                goto Label_021B;
            }
            this.m_CoinIconObject.SetActive(0);
        Label_021B:
            if ((this.m_AmountObject != null) == null)
            {
                goto Label_0243;
            }
            this.SetText(this.m_AmountObject, this.m_Present.num);
        Label_0243:
            if ((this.m_NumObject != null) == null)
            {
                goto Label_0260;
            }
            this.m_NumObject.SetActive(1);
        Label_0260:
            if ((this.m_NumValueObject != null) == null)
            {
                goto Label_0347;
            }
            this.m_NumValueObject.SetActive(1);
            this.SetText(this.m_NumValueObject, this.m_ItemData.Num);
            goto Label_0347;
        Label_0299:
            if ((this.m_FrameObject != null) == null)
            {
                goto Label_02C8;
            }
            this.SetSprite(this.m_FrameObject, GameSettings.Instance.GetItemFrame(12, 0), Color.get_white());
        Label_02C8:
            if ((this.m_IconObject != null) == null)
            {
                goto Label_02E5;
            }
            this.m_IconObject.SetActive(0);
        Label_02E5:
            if ((this.m_CoinIconObject != null) == null)
            {
                goto Label_0302;
            }
            this.m_CoinIconObject.SetActive(1);
        Label_0302:
            if ((this.m_ZenyObject != null) == null)
            {
                goto Label_032A;
            }
            this.SetText(this.m_ZenyObject, this.m_Present.zeny);
        Label_032A:
            if ((this.m_NumObject != null) == null)
            {
                goto Label_0347;
            }
            this.m_NumObject.SetActive(0);
        Label_0347:
            return;
        }

        public void SetRestTime(GameObject gobj, long endTime)
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            Text text;
            long num;
            string str;
            int num2;
            int num3;
            int num4;
            text = gobj.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_00BC;
            }
            num = endTime - Network.GetServerTime();
            if (num > 0L)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            str = null;
            if (num < 0x15180L)
            {
                goto Label_005B;
            }
            num2 = (int) (num / 0x15180L);
            objArray1 = new object[] { (int) num2 };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", objArray1);
            goto Label_00B5;
        Label_005B:
            if (num < 0xe10L)
            {
                goto Label_0092;
            }
            num3 = (int) (num / 0xe10L);
            objArray2 = new object[] { (int) num3 };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", objArray2);
            goto Label_00B5;
        Label_0092:
            num4 = (int) (num / 60L);
            objArray3 = new object[] { (int) num4 };
            str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", objArray3);
        Label_00B5:
            text.set_text(str);
        Label_00BC:
            return;
        }

        public void SetSprite(GameObject gobj, Sprite sprite)
        {
            Image image;
            image = gobj.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_001A;
            }
            image.set_sprite(sprite);
        Label_001A:
            return;
        }

        public void SetSprite(GameObject gobj, Sprite sprite, Color color)
        {
            Image image;
            image = gobj.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_0021;
            }
            image.set_sprite(sprite);
            image.set_color(color);
        Label_0021:
            return;
        }

        public unsafe void SetText(GameObject gobj, int num)
        {
            Text text;
            text = gobj.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_0020;
            }
            text.set_text(&num.ToString());
        Label_0020:
            return;
        }

        public void SetText(GameObject gobj, string text)
        {
            Text text2;
            text2 = gobj.GetComponent<Text>();
            if ((text2 != null) == null)
            {
                goto Label_001A;
            }
            text2.set_text(text);
        Label_001A:
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        [CompilerGenerated]
        private sealed class <Bind>c__AnonStorey33D
        {
            internal FriendPresentItemParam present;

            public <Bind>c__AnonStorey33D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__31D(ItemData prop)
            {
                return (prop.ItemID == this.present.item.iname);
            }
        }
    }
}

