namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [NodeType("System/Friend/RequestFavorite", 0x7fe5), Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0)]
    public class FlowNode_Friend_RequestFavorite : FlowNode_Network
    {
        private string mTargetFuid;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache1;

        public FlowNode_Friend_RequestFavorite()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <OnActivate>m__1D7(GameObject go)
        {
        }

        [CompilerGenerated]
        private bool <OnSuccess>m__1D8(FriendData f)
        {
            return (f.FUID == this.mTargetFuid);
        }

        public override void OnActivate(int pinID)
        {
            FriendData data;
            string str;
            <OnActivate>c__AnonStorey282 storey;
            if (pinID != null)
            {
                goto Label_0120;
            }
            storey = new <OnActivate>c__AnonStorey282();
            if (Network.Mode != 1)
            {
                goto Label_001E;
            }
            this.Success();
            return;
        Label_001E:
            storey.fuid = null;
            if (string.IsNullOrEmpty(GlobalVars.SelectedFriendID) != null)
            {
                goto Label_0044;
            }
            storey.fuid = GlobalVars.SelectedFriendID;
            goto Label_0072;
        Label_0044:
            if (GlobalVars.FoundFriend == null)
            {
                goto Label_0072;
            }
            if (string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID) != null)
            {
                goto Label_0072;
            }
            storey.fuid = GlobalVars.FoundFriend.FUID;
        Label_0072:
            if (storey.fuid != null)
            {
                goto Label_0084;
            }
            this.Success();
            return;
        Label_0084:
            data = MonoSingleton<GameManager>.Instance.Player.Friends.Find(new Predicate<FriendData>(storey.<>m__1D6));
            if (data == null)
            {
                goto Label_00F0;
            }
            str = string.Empty;
            if (data.IsFavorite == null)
            {
                goto Label_00F0;
            }
            str = LocalizedText.Get("sys.FRIEND_ALREADY_FAVORITE");
            if (<>f__am$cache1 != null)
            {
                goto Label_00E1;
            }
            <>f__am$cache1 = new UIUtility.DialogResultEvent(FlowNode_Friend_RequestFavorite.<OnActivate>m__1D7);
        Label_00E1:
            UIUtility.SystemMessage(null, str, <>f__am$cache1, null, 0, -1);
            return;
        Label_00F0:
            this.mTargetFuid = storey.fuid;
            base.ExecRequest(new ReqFriendFavoriteAdd(storey.fuid, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0120:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_FriendArray> response;
            FriendData data;
            Exception exception;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_FriendArray>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (Network.IsError == null)
            {
                goto Label_002F;
            }
            this.OnRetry();
            return;
        Label_002F:
            if (response.body != null)
            {
                goto Label_0041;
            }
            this.OnRetry();
            return;
        Label_0041:
            if (string.IsNullOrEmpty(this.mTargetFuid) != null)
            {
                goto Label_00AB;
            }
        Label_0051:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.friends, 1);
                data = MonoSingleton<GameManager>.Instance.Player.Friends.Find(new Predicate<FriendData>(this.<OnSuccess>m__1D8));
                if (data == null)
                {
                    goto Label_0094;
                }
                GlobalVars.SelectedFriend = data;
            Label_0094:
                goto Label_00AB;
            }
            catch (Exception exception1)
            {
            Label_0099:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_00B6;
            }
        Label_00AB:
            Network.RemoveAPI();
            this.Success();
        Label_00B6:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey282
        {
            internal string fuid;

            public <OnActivate>c__AnonStorey282()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1D6(FriendData f)
            {
                return (f.FUID == this.fuid);
            }
        }
    }
}

