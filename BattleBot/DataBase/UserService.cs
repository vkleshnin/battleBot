using Telegram.Bot.Types;

namespace BattleBot.DataBase;

public abstract class UserService
{
	public static async Task<UserTelegram> Add(User user)
	{
		await using var db = new AppContext();
		
		var userTelegram = new UserTelegram()
		{
			Login = user.Username!,
			EnterDate = DateTime.UtcNow,
			LastDate = DateTime.UtcNow,
			TypeProfile = ETypeProfile.Default,
			TelegramId = user.Id,
			Units = new List<long>()
		};

		db.Users.Add(userTelegram);
		await db.SaveChangesAsync();
			
		return userTelegram;
	}
	
	public static Task AddUnit(UserTelegram user, long unitId)
	{
		using var db = new AppContext();

		user.Units.Add(unitId);
		
		db.Users.Update(user);
		db.SaveChangesAsync();
		
		return Task.CompletedTask;
	} 

	public static UserTelegram? Get(User user)
	{
		using var db = new AppContext();

		var telegramUser = db.Users.FirstOrDefault(u => u.Login == user.Username);
		
		if (telegramUser is not null) return telegramUser;
		
		Console.WriteLine($"UserService: The user with the login {user.Username} was not found.");
		
		return null;
	}
	
	public static UserTelegram? Get(UserTelegram userTelegram)
	{
		using var db = new AppContext();

		var telegramUser = db.Users.FirstOrDefault(u => u.Login == userTelegram.Login);
		
		if (telegramUser is not null) return telegramUser;
		
		Console.WriteLine($"UserService: The user with the login {userTelegram.Login} was not found.");
		
		return null;
	}

	public static ETypeProfile GetType(long telegramId)
	{
		using var db = new AppContext();

		var telegramUser = db.Users.FirstOrDefault(u => u.TelegramId == telegramId);
		
		if (telegramUser is not null) return telegramUser.TypeProfile;
		
		Console.WriteLine($"UserService: The user with the telegramId {telegramId} was not found.");
		
		return ETypeProfile.Default;
	}
	
	public static UserTelegram? Get(string userName)
	{
		using var db = new AppContext();

		var telegramUser = db.Users.FirstOrDefault(u => u.Login == userName);
		
		if (telegramUser is not null) return telegramUser;
		
		Console.WriteLine($"UserService: The user with the login {userName} was not found.");
		
		return null;
	}

	public static UserTelegram? Get(long telegramId)
	{
		using var db = new AppContext();
		
		var telegramUser = db.Users.FirstOrDefault(u => u.TelegramId == telegramId);
		
		if(telegramUser is not null) return telegramUser;
		
		Console.WriteLine($"UserService: The user with the telegramId {telegramId} was not found.");

		return null;
	}

	public static List<long>? GetUnits(long userId)
	{
		using var db = new AppContext();

		var user = Get(userId);
		if (user is not null) return user.Units;
		
		Console.WriteLine($"UserService: The user with the telegramId {userId} was not found.");
		
		return null;
	}

	public static void Delete(UserTelegram userTelegram)
	{
		using var db = new AppContext();

		var user = db.Users.Find(userTelegram);
		if (user is null)
		{
			Console.WriteLine($"Console: The user with the login {userTelegram.Login} was not found.");
			return ;
		}

		db.Users.Remove(userTelegram);
		
		Console.WriteLine($"Console: The user with the login {userTelegram.Login} was deleted.");
	}

	public static UserTelegram ChangeUserType(UserTelegram user)
	{
		using var db = new AppContext();
		
		user.TypeProfile = user.TypeProfile == ETypeProfile.GameMaster ? ETypeProfile.Default : ETypeProfile.GameMaster;
		
		db.Users.Update(user);
		db.SaveChanges();
		
		return user;
	}
}