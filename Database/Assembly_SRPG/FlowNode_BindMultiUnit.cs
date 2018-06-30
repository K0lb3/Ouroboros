namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [AddComponentMenu(""), NodeType("Multi/BindMultiUnit", 0x7fe5), Pin(2, "Out", 1, 1), Pin(1, "Set", 0, 0)]
    public class FlowNode_BindMultiUnit : FlowNode
    {
        [DropTarget(typeof(GameObject[]), false)]
        public GameObject[] Targets;
        [ShowInInfo]
        public TargetType Type;
        [DropTarget(typeof(GameObject[]), false)]
        public GameObject Image;

        public FlowNode_BindMultiUnit()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            JSON_MyPhotonPlayerParam param;
            string str;
            FlowNode_StartMultiPlay.PlayerList list;
            JSON_MyPhotonPlayerParam[] paramArray;
            PartyData data;
            int num;
            <OnActivate>c__AnonStorey265 storey;
            storey = new <OnActivate>c__AnonStorey265();
            storey.pt = PunMonoSingleton<MyPhoton>.Instance;
            param = null;
            if ((storey.pt != null) == null)
            {
                goto Label_0197;
            }
            str = storey.pt.GetRoomParam("started");
            if (str == null)
            {
                goto Label_0197;
            }
            paramArray = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(str).players;
            if (((int) paramArray.Length) <= 0)
            {
                goto Label_0197;
            }
            if (this.Type != null)
            {
                goto Label_007A;
            }
            param = Array.Find<JSON_MyPhotonPlayerParam>(paramArray, new Predicate<JSON_MyPhotonPlayerParam>(storey.<>m__182));
            goto Label_008E;
        Label_007A:
            param = Array.Find<JSON_MyPhotonPlayerParam>(paramArray, new Predicate<JSON_MyPhotonPlayerParam>(storey.<>m__183));
        Label_008E:
            if (param == null)
            {
                goto Label_0197;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(10);
            num = 0;
            goto Label_0188;
        Label_00AF:
            if (num < ((int) param.units.Length))
            {
                goto Label_00C3;
            }
            goto Label_0197;
        Label_00C3:
            if (GlobalVars.SelectedMultiPlayRoomType != 3)
            {
                goto Label_00E8;
            }
            if (data == null)
            {
                goto Label_00E8;
            }
            if (num < data.VSWAITMEMBER_START)
            {
                goto Label_00E8;
            }
            goto Label_0197;
        Label_00E8:
            param.units[num].unit = new UnitData();
            param.units[num].unit.Deserialize(param.units[num].unitJson);
            DataSource.Bind<UnitData>(this.Targets[num], param.units[num].unit);
            GameParameter.UpdateAll(this.Targets[num]);
            if ((this.Image != null) == null)
            {
                goto Label_0182;
            }
            if (num != null)
            {
                goto Label_0182;
            }
            DataSource.Bind<UnitData>(this.Image, param.units[num].unit);
            GameParameter.UpdateAll(this.Image);
        Label_0182:
            num += 1;
        Label_0188:
            if (num < ((int) this.Targets.Length))
            {
                goto Label_00AF;
            }
        Label_0197:
            base.ActivateOutputLinks(2);
            return;
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey265
        {
            internal MyPhoton pt;

            public <OnActivate>c__AnonStorey265()
            {
                base..ctor();
                return;
            }

            internal bool <>m__182(JSON_MyPhotonPlayerParam p)
            {
                return (p.playerID == this.pt.GetMyPlayer().playerID);
            }

            internal bool <>m__183(JSON_MyPhotonPlayerParam p)
            {
                return ((p.playerID == this.pt.GetMyPlayer().playerID) == 0);
            }
        }

        public enum TargetType
        {
            Player,
            Enemy
        }
    }
}

