using BattleBot.Core;
using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace BattleBot;

public static class Program
{
	public static TelegramBotClient BotClient { get; private set; } = null!;

	private static string? _accessToken;

	private static async Task Main()
	{
		var fileWithToken = await File.ReadAllTextAsync("AccessTokens.json");
		_accessToken = fileWithToken.Split(':')[1].Split('"')[1];
		
		BotClient = new TelegramBotClient(_accessToken);
		using var cts = new CancellationTokenSource();
		
		
		
		var receiverOptions = new ReceiverOptions()
		{
			AllowedUpdates = new []
			{
				UpdateType.Message, 
				UpdateType.CallbackQuery
			},
			ThrowPendingUpdates = false
		};
		
		BotClient.StartReceiving(UpdateHandler.Update, ErrorHandler.Error, receiverOptions, cts.Token);
		
		var me = await BotClient.GetMeAsync(cancellationToken: cts.Token);
		Console.WriteLine($"{me.FirstName} is started.");

		while (!cts.Token.IsCancellationRequested)
		{
			var consoleInput = Console.ReadLine();
			if (consoleInput is not null && consoleInput.StartsWith($"/"))
			{
				var splitInput = consoleInput.Split(" ");
				switch (splitInput.First())
				{
					case "/change-type":
						var userTelegram = UserService.Get(splitInput[1]);

						if (userTelegram is not null)
							UserService.ChangeUserType(userTelegram);
						break;
					case "/exit":
						return ;
				}
			}
		}
		
		await Task.Delay(-1, cts.Token);
	}
}