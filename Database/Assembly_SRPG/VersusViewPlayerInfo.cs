namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusViewPlayerInfo : MonoBehaviour
    {
        public GameObject EmptyObj;
        public GameObject ValidObj;
        public GameObject LeaderUnit;
        public GameObject ReadyObj;
        public GameObject Award;
        public Text Name;
        public Text Lv;
        public Text Total;

        public VersusViewPlayerInfo()
        {
            base..ctor();
            return;
        }

        public unsafe void Refresh()
        {
            GameManager manager;
            MyPhoton photon;
            JSON_MyPhotonRoomParam param;
            MyPhoton.MyRoom room;
            JSON_MyPhotonPlayerParam param2;
            Json_Unit unit;
            UnitData data;
            manager = MonoSingleton<GameManager>.Instance;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            param = JSON_MyPhotonRoomParam.Parse(manager.AudienceRoom.json);
            param = JSON_MyPhotonRoomParam.Parse(photon.SearchRoom(param.roomid).json);
            param2 = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(base.get_gameObject(), null);
            if (param2 == null)
            {
                goto Label_01FA;
            }
            if ((this.EmptyObj != null) == null)
            {
                goto Label_0068;
            }
            this.EmptyObj.SetActive(0);
        Label_0068:
            if ((this.ValidObj != null) == null)
            {
                goto Label_0085;
            }
            this.ValidObj.SetActive(1);
        Label_0085:
            if ((this.LeaderUnit != null) == null)
            {
                goto Label_011B;
            }
            if (param.draft_type != 1)
            {
                goto Label_00EF;
            }
            if (string.IsNullOrEmpty(param2.support_unit) != null)
            {
                goto Label_00EF;
            }
            unit = JSONParser.parseJSONObject<Json_Unit>(param2.support_unit);
            if (unit == null)
            {
                goto Label_011B;
            }
            data = new UnitData();
            data.Deserialize(unit);
            DataSource.Bind<UnitData>(this.LeaderUnit.get_gameObject(), data);
            goto Label_011B;
        Label_00EF:
            if (param2.units == null)
            {
                goto Label_011B;
            }
            param2.SetupUnits();
            DataSource.Bind<UnitData>(this.LeaderUnit, param2.units[0].unit);
        Label_011B:
            if ((this.Name != null) == null)
            {
                goto Label_013E;
            }
            this.Name.set_text(param2.playerName);
        Label_013E:
            if ((this.Lv != null) == null)
            {
                goto Label_0166;
            }
            this.Lv.set_text(&param2.playerLevel.ToString());
        Label_0166:
            if ((this.Total != null) == null)
            {
                goto Label_018E;
            }
            this.Total.set_text(&param2.totalAtk.ToString());
        Label_018E:
            if ((this.ReadyObj != null) == null)
            {
                goto Label_01B7;
            }
            this.ReadyObj.SetActive((param2.state == 4) == 0);
        Label_01B7:
            if ((this.Award != null) == null)
            {
                goto Label_01EA;
            }
            this.Award.get_gameObject().SetActive(0);
            this.Award.get_gameObject().SetActive(1);
        Label_01EA:
            GameParameter.UpdateAll(base.get_gameObject());
            goto Label_0251;
        Label_01FA:
            if ((this.EmptyObj != null) == null)
            {
                goto Label_0217;
            }
            this.EmptyObj.SetActive(1);
        Label_0217:
            if ((this.ValidObj != null) == null)
            {
                goto Label_0234;
            }
            this.ValidObj.SetActive(0);
        Label_0234:
            if ((this.ReadyObj != null) == null)
            {
                goto Label_0251;
            }
            this.ReadyObj.SetActive(0);
        Label_0251:
            return;
        }

        private void Start()
        {
        }
    }
}

