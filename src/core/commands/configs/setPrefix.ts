import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { Context } from '@bot/models/context'
import { Message, MessageEmbed, MessageReaction, User } from 'discord.js'
import { __ } from 'i18n'
import embedConfig from '@configs/embedConfig.json'
import { setPrefix } from '@server/functions/setPrefix'

@CommandName('setprefix')
@CommandInfo({
    description: 'setprefix',
    module: 'configs'
})
export default class SetPrefix extends Command {
    async validPermission (ctx: Context): Promise<boolean> {
        if (!ctx.memberClient?.permissionsIn(ctx.channel).has(['ADD_REACTIONS', 'USE_EXTERNAL_EMOJIS', 'MANAGE_MESSAGES'])) {
            throw new BotPermissionError(['ADD_REACTIONS', 'USE_EXTERNAL_EMOJIS', 'MANAGE_MESSAGES'], ctx.channel)
        }

        if (!ctx.memberAuthor?.permissionsIn(ctx.channel).has(['MANAGE_GUILD', 'ADD_REACTIONS'])) {
            throw new ClientPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS'], ctx.channel)
        }

        return true
    }

    async execCommand (ctx: Context): Promise<void> {
        let embed = new MessageEmbed({
            color: embedConfig.colors.green,
            thumbnail: {
                url: ctx.client?.displayAvatarURL({ dynamic: true })
            }
        })
        embed.title = __({
            phrase: 'command.setprefix.embeds.enter.title',
            locale: ctx.guildConfig.lang
        })
        embed.description = __({
            phrase: 'command.setprefix.embeds.enter.description',
            locale: ctx.guildConfig.lang
        })
        embed.footer = {
            text: __({
                phrase: 'command.setprefix.embeds.enter.footer',
                locale: ctx.guildConfig.lang
            })
        }

        const embedMessage = await ctx.channel.send(embed)

        try {
            const message = (await ctx.channel.awaitMessages((message: Message) => {
                return message.author.id === ctx.author.id &&
                    message.content.length > 0 &&
                    message.content.length < 7
            }, {
                max: 1,
                time: 20000,
                errors: [
                    'time'
                ]
            })).first()

            embed = new MessageEmbed({
                color: embedConfig.colors.green,
                thumbnail: {
                    url: ctx.client?.displayAvatarURL({ dynamic: true })
                }
            })
            embed.title = __({
                phrase: 'command.setprefix.embeds.confirm.title',
                locale: ctx.guildConfig.lang
            })
            embed.addField(
                __({
                    phrase: 'command.setprefix.embeds.confirm.update',
                    locale: ctx.guildConfig.lang
                }),
                '`' + ctx.guildConfig.prefix + '` ' +
                ctx.clientBot.emojis.cache.get(embedConfig.emojis.join_arrow)?.toString() +
                ' `' + message?.content + '`'
            )

            await embedMessage.edit(embed)
            await embedMessage.react(embedConfig.emojis.check)
            await embedMessage.react(embedConfig.emojis.uncheck)

            const reaction = (await embedMessage.awaitReactions((reaction: MessageReaction, user: User) => {
                return (reaction.emoji.id === embedConfig.emojis.check ||
                    reaction.emoji.id === embedConfig.emojis.uncheck) &&
                    user.id === ctx.author.id
            }, {
                max: 1,
                time: 20000,
                errors: [
                    'time'
                ]
            })).first()

            await embedMessage.reactions.removeAll()

            if (reaction?.emoji.id === embedConfig.emojis.check) {
                const newPrefix = await setPrefix(ctx.message.id, {
                    guildId: ctx.guild?.id as string,
                    newPrefix: message?.content as string
                })

                embed = new MessageEmbed({
                    color: embedConfig.colors.green,
                    thumbnail: {
                        url: ctx.client?.displayAvatarURL()
                    }
                })
                embed.title = __({
                    phrase: 'command.setprefix.embeds.check.title',
                    locale: ctx.guildConfig.lang
                })
                embed.addField(
                    __({
                        phrase: 'command.setprefix.embeds.check.new',
                        locale: ctx.guildConfig.lang
                    }),
                    ctx.clientBot.emojis.cache.get(embedConfig.emojis.join_arrow)?.toString() +
                    ' `' + newPrefix + '`'
                )

                await embedMessage.edit(embed)
            } else {
                embed = new MessageEmbed({
                    color: embedConfig.colors.yellow,
                    thumbnail: {
                        url: ctx.client?.displayAvatarURL()
                    }
                })
                embed.title = __({
                    phrase: 'command.setprefix.embeds.uncheck.title',
                    locale: ctx.guildConfig.lang
                })
                embed.addField(
                    __({
                        phrase: 'command.setprefix.embeds.uncheck.current',
                        locale: ctx.guildConfig.lang
                    }),
                    ctx.clientBot.emojis.cache.get(embedConfig.emojis.join_arrow)?.toString() +
                    ' `' + ctx.guildConfig.prefix + '`'
                )

                await embedMessage.edit(embed)
            }
        } catch (error) {
            await embedMessage.delete()
        }
    }
}
