using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot;

public class UpdateHandler
{
    public Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    MessageHandler(botClient, update, cancellationToken);
                    return Task.CompletedTask;
                case UpdateType.CallbackQuery:
                    CallbackQueryHandler(botClient, update, cancellationToken);
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

    private void MessageHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        var user = message?.From;
        var chat = message?.Chat;

        Console.WriteLine($"User [{user?.Username}]: \"{message?.Text}\"");
                    
        if (message is not null && chat is not null && message.Text!.StartsWith("/"))
        {
            switch (message.Text)
            {
                case "/start":
                    HandleStartCommand(botClient, chat);
                    break;
            }
        }
    }

    private void CallbackQueryHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var callbackQuery = new CallbackQuery();
        var user = callbackQuery?.From;
        var chat = callbackQuery?.Message?.Chat;

        switch (callbackQuery?.Data)
        {
            case "battleButton":
                break;
        }
    }
    
    private void HandleStartCommand(ITelegramBotClient botClient, Chat chat)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(
            new List<InlineKeyboardButton[]>()
            {
                // Каждый новый массив - это дополнительные строки,
                // а каждая дополнительная строка (кнопка) в массиве - это добавление ряда
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Создать профиль.", "battleButton")
                }
            });

        botClient.SendTextMessageAsync(chat.Id, "Добро пожаловать!", replyMarkup: inlineKeyboard);
    }
}