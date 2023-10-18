using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Messages;

public class UnitInfoMessage(long chatId, long unitId) : IMessage
{
	private long ChatId { get; set; } = chatId;
	
	private InlineKeyboardMarkup Buttons { get; set;} = new InlineKeyboardMarkup(
		new List<InlineKeyboardButton[]>
		{
			new []
			{
				InlineKeyboardButton.WithCallbackData("Назад", BattleBot.Buttons.MAIN_MESSAGE)
			}
		});
	
	public Task<Message>? Send()
	{
		var unit = UnitService.Get(unitId);
		if (unit is null)
		{
			Console.WriteLine($"UnitInfoMessage: The unit with ID: [{unitId}] was not found.");
			return null;
		}
		var message = Program.BotClient.SendTextMessageAsync(ChatId, 
			$"Имя: {unit?.Name}\n" +
			$"Уровень: {unit?.Level}\n" +
			$"Класс брони: {unit?.ArmorClass}\n" +
			$"Здоровье: {unit?.HealthPoint}\n" +
			$"Владелец: {unit?.MasterId}\n" +
			$"ID: {unit?.Id}", replyMarkup: Buttons);

		return message;
	}
}