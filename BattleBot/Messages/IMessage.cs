using Telegram.Bot.Types;

namespace BattleBot.Messages;

public interface IMessage
{
	public Task<Message>? Send();
}