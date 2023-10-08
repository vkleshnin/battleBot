using BattleBot.DataBase;
using Telegram.Bot.Types;

namespace BattleBot.Core;

public class UserService
{
	public static UserTelegram Create(User user)
	{
		return new UserTelegram()
		{
			Login = user.Username,
			EnterDate = DateTime.UtcNow,
			LastDate = DateTime.UtcNow,
			TypeProfile = ETypeProfile.Default
		};
	}
}