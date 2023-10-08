using System.Diagnostics;
using BattleBot.Core;
using BattleBot.DataBase;
using BattleBot.Messages;
using Telegram.Bot.Types;

namespace BattleBot;

public abstract class Commands
{
	public static async Task Start(User user, Chat chat)
	{
		var userTelegram = Program.Context.Users
			.FirstOrDefault(u => u != null && u.Login == user.Username);

		if (userTelegram is null)
		{
			Program.Context.Users.Add(UserService.Create(user));
			await Program.Context.SaveChangesAsync();
		}
		else
		{
			if (userTelegram.TypeProfile == ETypeProfile.GameMaster)
			{
				var message = new MainMessage(chat.Id, ETypeProfile.GameMaster);
				await message.Send();
			}
			else
			{
				var unit = Program.Context.Units.FirstOrDefault(u => u.MasterId == userTelegram.Id);
				if (unit is not null)
				{
					var message = new MainMessage(chat.Id, ETypeProfile.Default);
					await message.Send();
				}
				else
				{
					var message = new CreateUnitMessage(chat.Id);
					await message.Send();
				}
			}
		}
	}
}