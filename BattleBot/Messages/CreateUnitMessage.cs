using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Messages;

public class CreateUnitMessage(long chatId) : IMessage
{
	private long ChatId { get; set; } = chatId;
	private string Text { get; set; } = "У вас нет персонажа, чтобы продолжить, создайте персонажа.";
	private InlineKeyboardMarkup Buttons { get; set;} = CreateButtons();
	private static string? _returnedText;
	

	private static InlineKeyboardMarkup CreateButtons()
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
	
	public static async Task<Unit> CreateUnit(long chatId, long userId)
	{
		Unit unit = new() { MasterId = userId };
    
		var name = await PromptForString(chatId, "Введите имя: ", 
			sName => UnitService.Get(sName) is null, "Такое имя уже существует, введите другое: ");    
		unit.Name = name;

		var levelStr = await PromptForString(chatId, "Введите уровень: ", 
			sLevel => uint.TryParse(sLevel, out _), "Введите корректный уровень: ");
		unit.Level = uint.Parse(levelStr);

		var acStr = await PromptForString(chatId, "Введите КБ: ", 
			sAc => uint.TryParse(sAc, out _), "Введите корректный КБ: ");
		unit.ArmorClass = uint.Parse(acStr);

		var hpStr = await PromptForString(chatId, "Введите ХП: ", 
			sHp => uint.TryParse(sHp, out _), "Введите корректное состояние здоровья: ");
		unit.HealthPoint = uint.Parse(hpStr);
    
		await UnitService.Add(unit);

		await Program.BotClient.SendTextMessageAsync(chatId, "Персонаж создан.");

		var message = new MainMessage(chatId, UserService.GetType(userId));
		await message.Send();
    
		return unit;
	}

	private static Task<string> PromptForString(long chatId, string prompt, Func<string, bool> validator, string retryPrompt)
	{
		var tcs = new TaskCompletionSource<string>();

		_ = SendAndWait();

		return tcs.Task;

		async Task SendAndWait()
		{
			var message = new WaitMessage(chatId, prompt, HandleResponse);
			await message.Send()!;
		}

		async void HandleResponse(long cId, string s)
		{
			UpdateHandler.MessageReceived -= HandleResponse;

			if (validator(s))
			{
				tcs.SetResult(s);
				return;
			}

			prompt = retryPrompt;
			await SendAndWait();
		}
	}
}