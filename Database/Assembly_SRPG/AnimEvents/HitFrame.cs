namespace SRPG.AnimEvents
{
    using SRPG;
    using System;

    public class HitFrame : AnimEvent
    {
        public HitReactionTypes ReactionType;

        public HitFrame()
        {
            base..ctor();
            return;
        }
    }
}

