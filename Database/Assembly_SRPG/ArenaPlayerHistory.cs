namespace SRPG
{
    using System;

    public class ArenaPlayerHistory
    {
        public string type;
        public string result;
        public JSON_ArenaRankInfo ranking;
        public DateTime battle_at;
        public ArenaPlayer my;
        public ArenaPlayer enemy;

        public ArenaPlayerHistory()
        {
            this.battle_at = DateTime.MinValue;
            base..ctor();
            return;
        }

        public void Deserialize(Json_ArenaPlayerHistory json)
        {
            Exception exception;
            Exception exception2;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.ranking = new JSON_ArenaRankInfo();
            this.type = json.type;
            this.result = json.result;
            this.battle_at = GameUtility.UnixtimeToLocalTime(json.at);
            if (json.ranking == null)
            {
                goto Label_008D;
            }
            this.ranking.rank = json.ranking.rank;
            this.ranking.up = json.ranking.up;
            this.ranking.is_best = json.ranking.is_best;
        Label_008D:
            if (json.my == null)
            {
                goto Label_00C5;
            }
        Label_0098:
            try
            {
                this.my = new ArenaPlayer();
                this.my.Deserialize(json.my);
                goto Label_00C5;
            }
            catch (Exception exception1)
            {
            Label_00B9:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00C5;
            }
        Label_00C5:
            if (json.enemy == null)
            {
                goto Label_00FD;
            }
        Label_00D0:
            try
            {
                this.enemy = new ArenaPlayer();
                this.enemy.Deserialize(json.enemy);
                goto Label_00FD;
            }
            catch (Exception exception3)
            {
            Label_00F1:
                exception2 = exception3;
                DebugUtility.LogException(exception2);
                goto Label_00FD;
            }
        Label_00FD:
            return;
        }

        public bool IsAttack()
        {
            return (this.type == "challenge");
        }

        public unsafe bool IsNew()
        {
            TimeSpan span;
            span = DateTime.Now - this.battle_at;
            return ((&span.TotalHours > 2.0) == 0);
        }

        public bool IsWin()
        {
            return (this.result == "win");
        }
    }
}

