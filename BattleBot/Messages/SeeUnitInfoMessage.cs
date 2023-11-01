using BattleBot.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Messages;

public class SeeUnitInfoMessage(long chatId, long unitId) : IMessage
{
    private long ChatId { get; set; } = chatId;
    
    private long UnitId { get; set; } = unitId;

    private InlineKeyboardMarkup Buttons { get; set;} = CreateButtons();
    
    private static InlineKeyboardMarkup CreateButtons()
    {
        return new InlineKeyboardMarkup(
            new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Назад", BattleBot.Buttons.MAIN_MESSAGE),
                }
            });
    }
    
    public Task<Message>? Send()
    {
        var unit = UnitService.Get(UnitId);
        if (unit is null)
        {
            Console.WriteLine($"Unit with id {UnitId} not found.");
            return null;
        }
        
        var message = Program.BotClient.SendTextMessageAsync(ChatId, 
            $"Имя: {unit.Name}\n" +
            $"Уровень: {unit.Level}\n" +
            $"КБ: {unit.ArmorClass}\n" +
            $"ХП: {unit.HealthPoint}\n" +
            $"Хозяин: {unit.MasterId}",
            replyMarkup: Buttons);

        return message;
    }
}