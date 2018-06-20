# TAC-BOT

An *unofficial* [Discord](https://discordapp.com/) bot for [The Alchemist Code](https://alchemistcode.com/)

## What does it do?

`TAC-BOT` is a Discord bot for listing relevant information about The Alchemist Code. `TAC_bot_db` supports following commands:
* `o?unit *name*`: Displays game-related information about the unit
* `o?lore *name*`: Displays the lore of the unit
* `o?art *name*`: Display artworks of all available skins of the unit
* `o?farm *name*`: Displays quests in which the item can be acquired
* `o?collabs *name*`: Elaborates the shortcuts of each collaboration events that are used in the unit related commands
* `o?info`: Displays the usage information about this bot

## How do I run it?

Check out the code and run it as a simple Python process! A hosting solutions like [Heroku](https://www.heroku.com/) is great for this. To run, you need to pass the Discord bot token as the environment variable.

### In Unix-like environments

```bash
$ export DISCORD_BOT_TOKEN='MyDiscordBot'
$ python discord_bot/Bot.py
```

### In Windows environments

```bash
C:\TAC_bot_db> set DISCORD_BOT_TOKEN='MyDiscordBot'
C:\TAC_bot_db> python discord_bot\Bot.py
```

## How do I contribute?

We welcome your contribution! Check out [`CONTRIBUTING.md`](.github/CONTRIBUTING.md)
