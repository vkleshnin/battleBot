using Telegram.Bot.Types;

namespace BattleBot.DataBase;

public abstract class UnitService
{
	public static Unit Add(uint level, string name, uint armorClass, uint healthPoint, User user)
	{
		using var db = new AppContext();
		
		var unit = new Unit
		{
			Level = level,
			Name = name,
			ArmorClass = armorClass,
			HealthPoint = healthPoint,
			MasterId = user.Id
		};

		db.Units.Add(unit);
		db.SaveChanges();
		

		return unit;
	}

	public static Unit? Get(string unitName)
	{
		using var db = new AppContext();

		var searchResult = db.Units.FirstOrDefault(u => u.Name == unitName);
		
		if (searchResult is not null) return searchResult;
		
		Console.WriteLine($"Console: The unit with the name {unitName} was not found.");
		
		return null;
	}
	
	public static Unit? Get(long masterId)
	{
		using var db = new AppContext();

		var searchResult = db.Units.FirstOrDefault(u => u.MasterId == masterId);
		
		if (searchResult is not null) return searchResult;
		
		Console.WriteLine($"Console: MasterID {masterId} has no characters.");
		
		return null;
	}

	public static void Delete(Unit unit)
	{
		using var db = new AppContext();
		
		var searchResult = db.Units.Find(unit);
		
		if (searchResult is null)
		{
			Console.WriteLine($"Console: The unit {unit.Name} was not found.");
			return ;
		}
		db.Units.Remove(unit);
		db.SaveChanges();
	}

	public static void Edit(Unit unit, string? newName, uint? newArmorClass, uint? newHealthPoint, uint? newLevel)
	{
		using var db = new AppContext();
		
		var searchResult = db.Units.Find(unit);
		
		if (searchResult is null)
		{
			Console.WriteLine($"Console: The unit {unit.Name} was not found.");
			return ;
		}

		if (newName != null) searchResult.Name = newName;
		if (newArmorClass != null) searchResult.ArmorClass = (uint)newArmorClass;
		if (newHealthPoint != null) searchResult.ArmorClass = (uint)newHealthPoint;
		if (newLevel != null) searchResult.ArmorClass = (uint)newLevel;

		db.Units.Update(searchResult);
		db.SaveChanges();
	}
}