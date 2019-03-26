# Ouroboros

An *unofficial* [Discord](https://discordapp.com/) bot for [The Alchemist Code](https://alchemistcode.com/)
Data from [The Alchemist Codes/Database/export](https://gitlab.com/the-alchemist-codes/Database).

## What does it do?

`Ouroboros` is a Discord bot for listing relevant information about The Alchemist Code. 
It supports following commands:
* `ai` : displays boundary conditions and settings of the ai
* `arena [alias: gr ,ranking]` : shows the current top 50 arena ranking
* `arena_map [alias: arenamap ,amap ,am]` : displays the current arena map
* `art [alias: artwork]` : posts all images of the unit
* `emoji` : converts reaction to unicode (for copypaste)
* `farm [alias: grind ,drop]` : shows missions which drop the item
* `gear [alias: arments ,equip]` : displays stats of the gear
* `help` : shows description of all commands
* `info` : shows informations about the bot and holds the invite link
* `item` : displays data of the item
* `job` : displays details of the job
* `kaigan [alias: enlightment]` : displays the kaigan stats and skills of the unit
* `lore` : shows the lore of the unit
* `map` : displays map and enemies of the mission
* `nensou [alias: conceptcard ,concept card ,card]` : displays stats of the concept card
* `quest` : displays quest informations
* `rank [alias: enemy]` : shows units and their gear of the selected rank (top 50)
* `story` : shows the conversations of the mission
* `unit` : displays key data of the unit

The usage statistics are tracked via a MongoDB.

## How do I run it?

Check out the code and run it as a simple Python process! A hosting solutions like [Heroku](https://www.heroku.com/) is great for this. To run, you need to pass the Discord bot token as the environment variable.

### In Unix-like environments

```bash
$ export DISCORD_BOT_TOKEN=
$ export DISCORD_BOT_TOKEN_ROLES=
$ export MONGO_USER=
$ export MONGO_PW=
$ export python discord_bot/Bot.py
```

### In Windows environments

```bash
.\RunBot.bat
```

## How do I contribute?

We welcome your contribution! Check out [`CONTRIBUTING.md`](.github/CONTRIBUTING.md)
