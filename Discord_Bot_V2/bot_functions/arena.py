from bot_functions.TAC_API import req_arena_ranking
from bot_functions._MainFunctions import TimeDif_hms
import discord
def arena():
    arena = req_arena_ranking()
    #start embed - title
    embed = discord.Embed(
        title="Top 50",
        description="ranking in:  " + TimeDif_hms("06:00:00") + "\n" + "reset in:    " + TimeDif_hms("10:00:00"),
        url="",
        color=16711680
        )
    #icon
    embed.set_thumbnail(url="https://drive.google.com/uc?export=download&id=1W369kdS_4ISgJgOcHAgNxoidJRqWNNsl")
    #fields
    field=[
        str(i+1)+": " + arena[i]['name']
        for i in range(0,len(arena))
        ]

    embed.add_field(name="Rank  1-25",    value='\n'.join(field[0:25]),    inline=True)
    embed.add_field(name="Rank 26-50",    value='\n'.join(field[25:]),    inline=True)
    return embed