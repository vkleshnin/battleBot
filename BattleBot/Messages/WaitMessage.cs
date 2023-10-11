using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BattleBot.Messages;

public class WaitMessage(long chatId, string text, UserTelegram user) : IMessage
{
	private long ChatId { get; set; } = chatId;

	private string Text { get; set; } = text;
	
	public async Task<Message>? Send()
	{
		throw new NotImplementedException();
	}
}