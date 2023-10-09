using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Messages;

public class WaitMessage(long chatId, string text) : IMessage
{
	private long ChatId { get; set; } = chatId;

	private string Text { get; set; } = text;
	
	public Task<Message>? Send()
	{
		return Program.BotClient.SendTextMessageAsync(ChatId, Text,
			replyMarkup: new ForceReplyMarkup
			{
				Selective = true
			});
	}
}