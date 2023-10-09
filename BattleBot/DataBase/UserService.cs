using Telegram.Bot.Types;

namespace BattleBot.DataBase;

public abstract class UserService
{
	public static UserTelegram Add(User user)
	{
		using var db = new AppContext();
		
		var userTelegram = new UserTelegram()
		{
			Login = user.Username!,
			EnterDate = DateTime.UtcNow,
			LastDate = DateTime.UtcNow,
			TypeProfile = ETypeProfile.Default
		};

		db.Users.Add(userTelegram);
		db.SaveChanges();
			
		return userTelegram;
	}

	public static UserTelegram? Get(User user)
	{
		using var db = new AppContext();

		var telegramUser = db.Users.FirstOrDefault(u => u != null && u.Login == user.Username);
		
		if (telegramUser is not null) return telegramUser;
		
		Console.WriteLine($"Console: The user with the login {user.Username} was not found.");
		
		return null;
	}
	
	public static UserTelegram? Get(UserTelegram userTelegram)
	{
		using var db = new AppContext();

		var telegramUser = db.Users.FirstOrDefault(u => u != null && u.Login == userTelegram.Login);
		
		if (telegramUser is not null) return telegramUser;
		
		Console.WriteLine($"Console: The user with the login {userTelegram.Login} was not found.");
		
		return null;
	}
	
	public static UserTelegram? Get(string userName)
	{
		using var db = new AppContext();

		var telegramUser = db.Users.FirstOrDefault(u => u != null && u.Login == userName);
		
		if (telegramUser is not null) return telegramUser;
		
		Console.WriteLine($"Console: The user with the login {userName} was not found.");
		
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