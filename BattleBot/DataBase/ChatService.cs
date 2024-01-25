using System.Diagnostics.CodeAnalysis;

namespace BattleBot.DataBase;

public class ChatService
{
	private static async Task<ChatTelegram?> Create(UserTelegram user)
	{
		await using var db = new AppContext();

		var userTelegram = UserService.Get(user);
		if (userTelegram is null)
		{
			Console.WriteLine($"Console: The user with the name {user.Login} was not found.");
			return null;
		}
		var chatTelegram = new ChatTelegram()
		{
			UserId = userTelegram.TelegramId,
			Messages = new List<long>()
		};

		db.ChatsTelegram.Add(chatTelegram);
		await db.SaveChangesAsync();

		return chatTelegram;
	}

	public static ChatTelegram? Get(UserTelegram userTelegram)
	{
		using var db = new AppContext();

		var chatTelegram = db.ChatsTelegram.FirstOrDefault(c => c.UserId == userTelegram.TelegramId);

		if (chatTelegram is not null) return chatTelegram;
		
		Console.WriteLine($"Console: The chat with the user {userTelegram.Login} was not found.");
		
		return null;
	}

	public static async void AddMessage(long userId, MessageTelegram? message)
	{
		await using var db = new AppContext();

		var chat = db.ChatsTelegram.FirstOrDefault(c => c.UserId == userId);
		
		if (chat is null)
		{
			chat = await Create(UserService.Get(userId)!);
			Console.WriteLine($"Console: The chat with the userId {userId} was not found. The chat is created now.");
		}
		
		chat!.Messages.Add(message!.Id);
		db.ChatsTelegram.Update(chat);

		await db.SaveChangesAsync();
	}
}