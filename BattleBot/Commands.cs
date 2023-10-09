using BattleBot.Core;
using BattleBot.DataBase;
using BattleBot.Messages;
using Telegram.Bot.Types;

namespace BattleBot;

public abstract class Commands
{
	public static async Task Start(User user, Chat chat)
	{
		var userTelegram = UserService.Get(user);

		if (userTelegram is null)
		{
			UserService.Add(user);
		}
		else
		{
			if (userTelegram.TypeProfile == ETypeProfile.GameMaster)
			{
				var message = new MainMessage(chat.Id, ETypeProfile.GameMaster);
				await message.Send()!;
			}
			else
			{
				var unit = UnitService.Get(userTelegram.Id);
				if (unit is not null)
				{
					var message = new MainMessage(chat.Id, ETypeProfile.Default);
					await message.Send()!;
				}
				else
				{
					var message = new CreateUnitMessage(chat.Id);
					await message.Send()!;
				}
			}
		}
	}
}