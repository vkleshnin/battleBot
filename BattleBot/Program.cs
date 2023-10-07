using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace BattleBot;

public static class Program
{
	private static TelegramBotClient? _botClient;
	
	private const string ACCESS_TOKEN = "6576435744:AAHpfjXc5iebQERrtiBneuuQglfn7MnUl-E";

	private static async Task Main()
	{
		_botClient = new TelegramBotClient(ACCESS_TOKEN);
		using var cts = new CancellationTokenSource();

		var updateHandler = new UpdateHandler(); //создание объекта обработчика обновлений
		var errorHandler = new ErrorHandler(); //создание объекта обработчика ошибок
		
		var receiverOptions = new ReceiverOptions()
		{
			AllowedUpdates = new []
			{
				UpdateType.Message, 
				UpdateType.CallbackQuery
			},
			ThrowPendingUpdates = true //параметр, отвечающий за обработку сообщений, если бот был offline
		};
		
		_botClient.StartReceiving(updateHandler.Update, errorHandler.Error, receiverOptions, cts.Token);
		
		var me = await _botClient.GetMeAsync(cancellationToken: cts.Token);
		Console.WriteLine($"{me.FirstName} is started.");
		
		await Task.Delay(-1, cts.Token);
	}
}