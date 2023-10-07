using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace BattleBot;

public static class Program
{
	private static ITelegramBotClient _botClient;
	private static ErrorHandler _errorHandler;
	private static UpdateHandler _updateHandler;
	private static ReceiverOptions _receiverOptions;

	private const string TOKEN = "6576435744:AAHpfjXc5iebQERrtiBneuuQglfn7MnUl-E";

	private static async Task Main()
	{
		_botClient = new TelegramBotClient(TOKEN);
		_receiverOptions = new ReceiverOptions()
		{
			AllowedUpdates = new []
			{
				UpdateType.Message, 
				UpdateType.CallbackQuery
			},
			ThrowPendingUpdates = true //параметр, отвечающий за обработку сообщений, если бот был offline
		};

		_errorHandler = new ErrorHandler(); //обработчик ошибок
		_updateHandler = new UpdateHandler(); //обработчик изменений

		using var cts = new CancellationTokenSource();
        
		_botClient.StartReceiving(_updateHandler.Update, _errorHandler.Error, _receiverOptions, cts.Token);

		var me = await _botClient.GetMeAsync(cancellationToken: cts.Token);
		Console.WriteLine($"{me.FirstName} is started.");
        
		await Task.Delay(-1, cts.Token);
	}
}