using BattleBot.Core;
using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using AppContext = BattleBot.DataBase.AppContext;

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

    private async void MessageHandler(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        var user = message?.From;
        var chat = message?.Chat;
        Console.WriteLine($"User [{user?.Username}]: \"{message?.Text}\"");
                    
        if (message is not null && chat is not null && user is not null 
            && message.Text!.StartsWith($"/"))
        {
            switch (message.Text)
            {
                case "/start":
                    await Commands.Start(user, chat);
                    break;
            }
        }
    }

    private async void CallbackQueryHandler(Update update, CancellationToken cancellationToken)
    {
        var callbackQuery = update.CallbackQuery;
        var user = callbackQuery?.From;
        var chat = callbackQuery?.Message?.Chat;

        switch (callbackQuery?.Data)
        {
            case "battleButton":
                break;
            case "CreateUnitButton":
                if (user is not null)
                {
                    Unit unit;
                    unit = await UnitService.Create(user);
                    var userTelegram = Program.Context.Users
                        .FirstOrDefault(u => u != null && u.Login == user.Username);
                    if (userTelegram != null) unit.MasterId = userTelegram.Id;
                    
                    Program.Context.Units.Add(unit);
                    await Program.Context.SaveChangesAsync(cancellationToken);
                }

                break;
        }
    }
}