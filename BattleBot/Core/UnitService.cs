using BattleBot.DataBase;
using BattleBot.Messages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BattleBot.Core;

public class UnitService
{
	public static async Task<Unit> Create(User user)
	{
		Unit unit = new Unit();

		var levelMessage = new WaitMessage(user.Id, "Введите уровень:");
		var nameMessage = new WaitMessage(user.Id, "Введите имя:");
		var acMessage = new WaitMessage(user.Id, "Введите класс брони:");
		var hpMessage = new WaitMessage(user.Id, "Введите здоровье:");

		var message = await levelMessage.Send();
		if (message.Text != null) unit.Level = uint.Parse(message.Text);
		
		message = await nameMessage.Send();
		if (message.Text != null) unit.Name = message.Text;

		message = await acMessage.Send();
		if (message.Text != null) unit.ArmorClass = uint.Parse(message.Text);

		message = await hpMessage.Send();
		if (message.Text != null) unit.HealthPoint = uint.Parse(message.Text);

		return unit;
	}
}