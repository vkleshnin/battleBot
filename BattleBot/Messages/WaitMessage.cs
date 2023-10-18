using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BattleBot.Messages;

public class WaitMessage(long chatId, string text, Action<long, string> func) : IMessage
{
	private long ChatId { get; set; } = chatId;

	private string Text { get; set; } = text;
	
	public async Task<Message>? Send()
	{
		if (UpdateHandler.ChatsStates.ContainsKey(chatId))
			UpdateHandler.ChatsStates[chatId] = ChatState.WaitInput;

		UpdateHandler.MessageReceived += func;
		
		return await Program.BotClient.SendTextMessageAsync(ChatId, Text);
	}
}