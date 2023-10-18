using BattleBot.Core;
using BattleBot.DataBase;
using BattleBot.Messages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BattleBot;

public abstract class UpdateHandler
{
    public static readonly Dictionary<long, ChatState> ChatsStates = new ();

    public static event Action<long, string> MessageReceived; 
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

        if (message is null || chat is null || user is null) 
            return;
        
        var messageTelegram = await MessageService.Add(user.Id, message.Text!);
        
        if (ChatsStates.TryGetValue(chat.Id, out var value) && value == ChatState.WaitInput)
        {
            ChatsStates[chat.Id] = ChatState.Default;
            MessageReceived.Invoke(chat.Id, message.Text!);
        }
        
        switch (message.Text)
        {
            case "/start":
                await Commands.Start(user, chat);
                break;
        }
        
        ChatService.AddMessage(user.Id, messageTelegram);
        ChatsStates.TryAdd(chat.Id, ChatState.Default);
    }

    private static async void CallbackQueryHandler(Update update, CancellationToken cancellationToken)
    {
        var callbackQuery = update.CallbackQuery;
        var user = callbackQuery?.From;
        var userTelegram = UserService.Get(user!);
        var chat = callbackQuery?.Message?.Chat;

        switch (callbackQuery?.Data)
        {
            case Buttons.BATTLE:
                break;
            case Buttons.CREATE_UNIT:
                CreateUnitMessage.CreateUnit(chat!.Id, user!.Id);
                break;
            case Buttons.SEE_USER_UNIT_INFO:
                if (userTelegram is null) 
                    return;
                
                var unitInfoMessage = new UnitInfoMessage(chat!.Id, userTelegram.Units[0]);
                await unitInfoMessage.Send()!;
                break;
            case Buttons.MAIN_MESSAGE:
                if (userTelegram is null) 
                    return;
                
                var mainMessage = new MainMessage(chat!.Id, userTelegram.TypeProfile);
                await mainMessage.Send();
                break;
        }
    }
}