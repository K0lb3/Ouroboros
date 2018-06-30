namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class MultiTowerFloorInfo : MonoBehaviour
    {
        public GameObject Lock;
        public GameObject Clear;
        public Text Floor;
        public SRPG_Button Root;
        [SerializeField]
        private GameObject mBody;
        private RectTransform mBodyTransform;
        public RectTransform rectTransform;
        [SerializeField]
        private ImageArray[] mBanner;
        public CanvasRenderer Source;
        private Color UnknownColor;
        public MultiTowerInfo MultiTower;
        public GameObject[] NowCharengeFloor;

        public MultiTowerFloorInfo()
        {
            this.UnknownColor = new Color(0.4f, 0.4f, 0.4f, 1f);
            base..ctor();
            return;
        }

        public void OnFocus(bool value)
        {
            if ((this.mBodyTransform != null) == null)
            {
                goto Label_005A;
            }
            if (value == null)
            {
                goto Label_003B;
            }
            this.mBodyTransform.set_localScale(new Vector3(1f, 1f, 1f));
            goto Label_005A;
        Label_003B:
            this.mBodyTransform.set_localScale(new Vector3(0.9f, 0.9f, 1f));
        Label_005A:
            return;
        }

        public void Refresh()
        {
            GameManager manager;
            MultiTowerFloorParam param;
            int num;
            int num2;
            int num3;
            GameObject obj2;
            int num4;
            MyPhoton photon;
            List<MyPhoton.MyPlayer> list;
            int num5;
            MyPhoton.MyPlayer player;
            JSON_MyPhotonPlayerParam param2;
            manager = MonoSingleton<GameManager>.Instance;
            param = DataSource.FindDataOfClass<MultiTowerFloorParam>(base.get_gameObject(), null);
            num = manager.GetMTChallengeFloor();
            num2 = manager.GetMTClearedMaxFloor();
            num3 = 0;
            goto Label_0042;
        Label_0029:
            obj2 = this.NowCharengeFloor[num3];
            obj2.SetActive(0);
            num3 += 1;
        Label_0042:
            if (num3 < ((int) this.NowCharengeFloor.Length))
            {
                goto Label_0029;
            }
            if (this.MultiTower.MultiTowerTop == null)
            {
                goto Label_0074;
            }
            this.SetFloorInfo(param, num, num2, 0x7fffffff);
            goto Label_012F;
        Label_0074:
            num4 = 0x7fffffff;
            if (param == null)
            {
                goto Label_0124;
            }
            list = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
            num5 = 0;
            goto Label_0116;
        Label_0099:
            player = list[num5];
            param2 = JSON_MyPhotonPlayerParam.Parse(player.json);
            if (num4 <= param2.mtChallengeFloor)
            {
                goto Label_00C9;
            }
            num4 = param2.mtChallengeFloor;
        Label_00C9:
            if (((int) this.NowCharengeFloor.Length) <= (param2.playerIndex - 1))
            {
                goto Label_0110;
            }
            if (param2.playerIndex <= 0)
            {
                goto Label_0110;
            }
            this.NowCharengeFloor[param2.playerIndex - 1].SetActive(param.floor == param2.mtChallengeFloor);
        Label_0110:
            num5 += 1;
        Label_0116:
            if (num5 < list.Count)
            {
                goto Label_0099;
            }
        Label_0124:
            this.SetFloorInfo(param, num, num2, num4);
        Label_012F:
            return;
        }

        public void SetFloor()
        {
            MultiTowerFloorParam param;
            param = DataSource.FindDataOfClass<MultiTowerFloorParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            this.MultiTower.OnTapFloor(param.floor);
            return;
        }

        public void SetFloorInfo(MultiTowerFloorParam param, int challenge, int cleared, int min_floor)
        {
            if (param == null)
            {
                goto Label_009D;
            }
            if ((this.Floor != null) == null)
            {
                goto Label_0048;
            }
            this.Floor.get_gameObject().SetActive(1);
            this.Floor.set_text(((short) param.floor) + "!");
        Label_0048:
            if (param.floor <= challenge)
            {
                goto Label_0060;
            }
            this.SetVisible(0);
            goto Label_0098;
        Label_0060:
            if (param.floor <= min_floor)
            {
                goto Label_0079;
            }
            this.SetVisible(4);
            goto Label_0098;
        Label_0079:
            if (param.floor > cleared)
            {
                goto Label_0091;
            }
            this.SetVisible(1);
            goto Label_0098;
        Label_0091:
            this.SetVisible(2);
        Label_0098:
            goto Label_00A4;
        Label_009D:
            this.SetVisible(3);
        Label_00A4:
            return;
        }

        private void SetVisible(Type type)
        {
            Type type2;
            GameUtility.SetGameObjectActive(this.Clear, 0);
            GameUtility.SetGameObjectActive(this.Lock, 0);
            GameUtility.SetGameObjectActive(this.Floor, 0);
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0049;

                case 1:
                    goto Label_0092;

                case 2:
                    goto Label_00DB;

                case 3:
                    goto Label_0118;

                case 4:
                    goto Label_014A;

                case 5:
                    goto Label_0187;
            }
            goto Label_018C;
        Label_0049:
            this.Source.SetColor(Color.get_gray());
            this.mBanner[0].ImageIndex = 1;
            this.mBanner[1].ImageIndex = 1;
            GameUtility.SetGameObjectActive(this.Lock, 1);
            GameUtility.SetGameObjectActive(this.Floor, 1);
            goto Label_018C;
        Label_0092:
            this.Source.SetColor(Color.get_white());
            this.mBanner[0].ImageIndex = 0;
            this.mBanner[1].ImageIndex = 0;
            GameUtility.SetGameObjectActive(this.Clear, 1);
            GameUtility.SetGameObjectActive(this.Floor, 1);
            goto Label_018C;
        Label_00DB:
            this.Source.SetColor(Color.get_white());
            this.mBanner[0].ImageIndex = 0;
            this.mBanner[1].ImageIndex = 0;
            GameUtility.SetGameObjectActive(this.Floor, 1);
            goto Label_018C;
        Label_0118:
            this.Source.SetColor(this.UnknownColor);
            this.mBanner[0].ImageIndex = 1;
            this.mBanner[1].ImageIndex = 1;
            goto Label_018C;
        Label_014A:
            this.Source.SetColor(Color.get_gray());
            this.mBanner[0].ImageIndex = 1;
            this.mBanner[1].ImageIndex = 1;
            GameUtility.SetGameObjectActive(this.Floor, 1);
            goto Label_018C;
        Label_0187:;
        Label_018C:
            return;
        }

        public void Start()
        {
            if ((this.mBody != null) == null)
            {
                goto Label_0022;
            }
            this.mBodyTransform = this.mBody.GetComponent<RectTransform>();
        Label_0022:
            this.rectTransform = base.GetComponent<RectTransform>();
            this.Root.get_onClick().AddListener(new UnityAction(this, this.SetFloor));
            return;
        }

        private enum Type
        {
            Locked,
            Cleared,
            Current,
            Unknown,
            PartnerLocked,
            TypeEnd
        }
    }
}

