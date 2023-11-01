using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Messages;

public class ChoiceUnitMessage(long chatId, long userId) : IMessage
{
	private long ChatId { get; set; } = chatId;
	
	public Task<Message>? Send()
	{
		var userTelegram = UserService.Get(userId);
		if (userTelegram is null)
		{
			Console.WriteLine($"User with id {userId} not found.");
			return null;
		}
		var unitButtons = new List<InlineKeyboardButton[]>();
		var units = new List<Unit>();
		foreach (var elem in userTelegram.Units)
		{
			var unit = UnitService.Get(elem);
			if (unit is null)
			{
				Console.WriteLine($"Unit with id {elem} not found.");
				return null;
			}
			units.Add(unit);
		}

		foreach (var unit in units)
		{
			unitButtons.Add(new []
			{
				InlineKeyboardButton.WithCallbackData(unit.Name, BattleBot.Buttons.SEE_UNIT_INFO + unit.Id)
			});
		}
		
		var message = Program.BotClient.SendTextMessageAsync(ChatId, "Выберите персонажа.", 
			replyMarkup: new InlineKeyboardMarkup(unitButtons));

		return message;
	}
}