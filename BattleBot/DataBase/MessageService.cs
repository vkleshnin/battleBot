namespace BattleBot.DataBase;

public class MessageService
{
	public static async Task<MessageTelegram> Add(long userId, string text)
	{
		await using var db = new AppContext();
		
		var message = new MessageTelegram()
		{
			Date = DateTime.UtcNow,
			Message = text,
			UserId = userId
		};

		db.MessagesTelegram.Add(message);
		await db.SaveChangesAsync();

		return message;
	}

	public static MessageTelegram Get(long userId, string text)
	{
		using var db = new AppContext();

		var message = db.MessagesTelegram.FirstOrDefault(m => m.UserId == userId && m.Message == text);
		
		if (message is not null) return message;
		
		Console.WriteLine($"Console: UserId[{userId}] message was not found.");
		
		return null!;
	}

	public static void Delete(MessageTelegram message)
	{
		using var db = new AppContext();
		
		var desiredMessage = db.MessagesTelegram.
			FirstOrDefault(m => m.UserId == message.UserId 
			                    && m.Message == message.Message && m.Date == message.Date);

		if (desiredMessage is null)
		{
			Console.WriteLine($"Console: UserId[{message.UserId}] message was not found.");
			return ;
		}
		
		db.MessagesTelegram.Remove(desiredMessage);
		db.SaveChanges();
	}
}