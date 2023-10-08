using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Messages;

public class MainMessage(long chatId, ETypeProfile eTypeProfile) : IMessage
{
	private long ChatId { get; set; } = chatId;

	private string Text { get; set;} = eTypeProfile == ETypeProfile.Default ? "Вы можете только посмотреть своего персонажа, пока ГМ не начал бой и не добавил вас." : String.Empty;

	private InlineKeyboardMarkup Buttons { get; set;} = eTypeProfile == ETypeProfile.Default ? DefaultButtons() : GameMasterButtons();

	private static InlineKeyboardMarkup DefaultButtons()
	{
		 return new InlineKeyboardMarkup(
			new List<InlineKeyboardButton[]>
			{
				new []
				{
					InlineKeyboardButton.WithCallbackData("Добавить NPC", "AddNpcButton"),
					InlineKeyboardButton.WithCallbackData("Создать бой", "CreateBattleButton"), 
				}
			});
	}
	
	private static InlineKeyboardMarkup GameMasterButtons()
	{
		return new InlineKeyboardMarkup(
			new List<InlineKeyboardButton[]>
			{
				new []
				{
					InlineKeyboardButton.WithCallbackData("Персонаж", "SeeCharacterButton"), 
				}
			});
	}


	public Task<Message>? Send()
	{
		return Program.BotClient != null ? Program.BotClient.SendTextMessageAsync(ChatId, Text, replyMarkup: Buttons) : null;
	}
}