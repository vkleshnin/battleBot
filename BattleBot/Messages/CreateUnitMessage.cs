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
					InlineKeyboardButton.WithCallbackData("Создать персонажа.", BattleBot.Buttons.CREATE_UNIT),
				}
			});
	}

	public Task<Message> Send()
	{
		return Program.BotClient.SendTextMessageAsync(ChatId, Text, replyMarkup: Buttons);
	}
	
	public static async void CreateUnit(long chatId, long userId)
	{
		Unit unit = new();

		var messageName = new WaitMessage(chatId, "Введите имя: ", SetName);

		await messageName.Send()!;
		return;

		async void SetName(long cIdName, string sName)
		{
			UpdateHandler.MessageReceived -= SetName;
			if (UnitService.Get(sName) is not null)
			{
				var messageBadName = new WaitMessage(chatId, "Такое имя уже существует, введите другое: ", SetName);
				await messageBadName.Send()!;
				return;
			}
			
			unit.Name = sName;
			unit.MasterId = userId;

			var messageLvl = new WaitMessage(chatId, "Введите уровень: ", SetLevel);
			await messageLvl.Send()!;

			return;

			async void SetLevel(long cIdLevel, string sLevel)
			{
				UpdateHandler.MessageReceived -= SetLevel;
				
				unit.Level = uint.Parse(sLevel);

				var messageAc = new WaitMessage(chatId, "Введите КБ: ", SetArmorClass);
				await messageAc.Send()!;
				
				return;

				async void SetArmorClass(long cIdArmorClass, string sArmorClass)
				{
					UpdateHandler.MessageReceived -= SetArmorClass;
					
					unit.ArmorClass = uint.Parse(sArmorClass);

					var messageHp = new WaitMessage(chatId, "Введите ХП: ", SetHealthPoint);
					await messageHp.Send()!;
					
					return;

					async void SetHealthPoint(long cIdHealthPoint, string sHealthPoint)
					{
						UpdateHandler.MessageReceived -= SetHealthPoint;
						
						unit.HealthPoint = uint.Parse(sHealthPoint);
						await UnitService.Add(unit);

						await Program.BotClient.SendTextMessageAsync(chatId, "Персонаж создан.");
						
						var message = new MainMessage(chatId, UserService.GetType(userId));
						await message.Send();
					}
				}
			}
		}
	}
}