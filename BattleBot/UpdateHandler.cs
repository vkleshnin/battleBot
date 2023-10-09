using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BattleBot;

public abstract class UpdateHandler
{
    public static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    MessageHandler(update, cancellationToken);
                    return Task.CompletedTask;
                case UpdateType.CallbackQuery:
                    CallbackQueryHandler(update, cancellationToken);
                    return Task.CompletedTask;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }

        return Task.CompletedTask;
    }

    private static async void MessageHandler(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        var user = message?.From;
        var chat = message?.Chat;
        Console.WriteLine($"User [{user?.Username}]: \"{message?.Text}\"");

        if (message is null || chat is null || user is null
            || !message.Text!.StartsWith($"/")) return;

        switch (message.Text)
        {
            case "/start":
                await Commands.Start(user, chat);
                break;
        }
    }

    private static async void CallbackQueryHandler(Update update, CancellationToken cancellationToken)
    {
        var callbackQuery = update.CallbackQuery;
        var user = callbackQuery?.From;
        var chat = callbackQuery?.Message?.Chat;

        switch (callbackQuery?.Data)
        {
            case Buttons.BATTLE:
                break;
            case Buttons.CREATE_UNIT:
                break;
            case Buttons.SEE_CHARACTER_INFO:
                break;
        }
    }
}