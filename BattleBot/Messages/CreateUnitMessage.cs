using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Messages;

public class CreateUnitMessage(long chatId) : IMessage
{
	private long ChatId { get; set; } = chatId;

	private string Text { get; set; } = "У вас нет персонажа, чтобы продолжить, создайте персонажа.";

	private InlineKeyboardMarkup Buttons { get; set;} = DefaultButtons();

	private static InlineKeyboardMarkup DefaultButtons()
	{
		return new InlineKeyboardMarkup(
			new List<InlineKeyboardButton[]>
			{
				new []
				{
					InlineKeyboardButton.WithCallbackData("Создать персонажа.", "CreateUnitButton"),
				}
			});
	}

	public Task<Message>? Send()
	{
		return Program.BotClient != null ? Program.BotClient.SendTextMessageAsync(ChatId, Text, replyMarkup: Buttons) : null;
	}
}