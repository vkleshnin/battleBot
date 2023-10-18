using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Messages;

public class MainMessage(long chatId, ETypeProfile eTypeProfile) : IMessage
{
	private long ChatId { get; set; } = chatId;

	private string Text { get; set;} = eTypeProfile == ETypeProfile.Default ? "Вы можете только посмотреть своего персонажа, пока ГМ не начал бой и не добавил вас." : ".";

	private InlineKeyboardMarkup Buttons { get; set;} = eTypeProfile == ETypeProfile.Default ? DefaultButtons() : GameMasterButtons();

	private static InlineKeyboardMarkup GameMasterButtons()
	{
		 return new InlineKeyboardMarkup(
			new List<InlineKeyboardButton[]>
			{
				new []
				{
					InlineKeyboardButton.WithCallbackData("Добавить NPC", BattleBot.Buttons.CREATE_UNIT),
					InlineKeyboardButton.WithCallbackData("Создать бой", BattleBot.Buttons.BATTLE), 
				}
			});
	}
	
	private static InlineKeyboardMarkup DefaultButtons()
	{
		return new InlineKeyboardMarkup(
			new List<InlineKeyboardButton[]>
			{
				new []
				{
					InlineKeyboardButton.WithCallbackData("Персонаж", BattleBot.Buttons.SEE_USER_UNIT_INFO), 
				}
			});
	}


	public Task<Message> Send()
	{
		return Program.BotClient.SendTextMessageAsync(ChatId, Text, replyMarkup: Buttons);
	}
}