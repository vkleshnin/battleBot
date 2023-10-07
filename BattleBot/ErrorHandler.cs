using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace BattleBot;

public class ErrorHandler
{
	public Task Error(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
	{
		var errorMessage = error switch
		{
			ApiRequestException apiRequestException
				=> $"Telegram API Error:\n [{apiRequestException.ErrorCode}]\n[{apiRequestException.Message}]",
			_ => error.ToString()
		};

		Console.WriteLine(errorMessage);
		return Task.CompletedTask;
	}
}