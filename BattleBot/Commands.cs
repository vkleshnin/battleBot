using BattleBot.DataBase;
using BattleBot.Messages;
using Telegram.Bot.Types;

namespace BattleBot;

public abstract class Commands
{
	public static async Task Start(User user, Chat chat)
	{
		var userTelegram = UserService.Get(user) ?? await UserService.Add(user);
		
		if (userTelegram.Units.Count > 0 && userTelegram.TypeProfile != ETypeProfile.GameMaster)
		{
			var createUnitMessage = new CreateUnitMessage(chat.Id);
			await createUnitMessage.Send();
			return ;
		}
		var welcomeMessage = new MainMessage(chat.Id, userTelegram.TypeProfile);
		await welcomeMessage.Send();
	}
}