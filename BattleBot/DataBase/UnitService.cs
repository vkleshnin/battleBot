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

	public static async Task Add(Unit unit)
	{
		await using var db = new AppContext();

		db.Units.Add(unit);
		await db.SaveChangesAsync();
		
		var user = UserService.Get(unit.MasterId);
		if (user is null)
		{
			Console.WriteLine($"UnitService: The user with the ID: [{unit.MasterId}] was not found. " +
			                  $"The unit {unit.Name} was not added in user.");
			return ;
		}
		
		var unitId = db.Units.FirstOrDefault(u => u.Name == unit.Name)?.Id ?? 0;
		
		await UserService.AddUnit(user, unitId);
	}

	public static Unit? Get(string unitName)
	{
		using var db = new AppContext();

		var searchResult = db.Units.FirstOrDefault(u => u.Name == unitName);
		
		if (searchResult is not null) return searchResult;
		
		Console.WriteLine($"UnitService: The unit with the name {unitName} was not found.");
		
		return null;
	}
	
	public static Unit? Get(long dbId)
	{
		using var db = new AppContext();

		var searchResult = db.Units.FirstOrDefault(u => u.Id == dbId);
		
		if (searchResult is not null) return searchResult;
		
		Console.WriteLine($"UnitService: The unit with ID: [{dbId}] was not found.");
		
		return null;
	}

	public static void Delete(Unit unit)
	{
		using var db = new AppContext();
		
		var searchResult = db.Units.Find(unit);
		
		if (searchResult is null)
		{
			Console.WriteLine($"UnitService: The unit {unit.Name} was not found.");
			return ;
		}
		db.Units.Remove(unit);
		db.SaveChanges();
	}

	public static void EditName(long unitId, string newName)
	{
		using var db = new AppContext();
		
		var unit = Get(unitId);
		
		if (unit is null)
		{
			Console.WriteLine($"UnitService: The unit ID: [{unitId}] was not found.");
			return ;
		}
		
		unit.Name = newName;
		db.Units.Update(unit);
		db.SaveChanges();
	}
	
	public static void EditLevel(long unitId, uint newLevel)
	{
		using var db = new AppContext();
		
		var unit = Get(unitId);
		
		if (unit is null)
		{
			Console.WriteLine($"UnitService: The unit ID: [{unitId}] was not found.");
			return ;
		}
		
		unit.Level = newLevel;
		db.Units.Update(unit);
		db.SaveChanges();
	}
	
	public static void EditArmorClass(long unitId, uint newArmorClass)
	{
		using var db = new AppContext();
		
		var unit = Get(unitId);
		
		if (unit is null)
		{
			Console.WriteLine($"UnitService: The unit ID: [{unitId}] was not found.");
			return ;
		}
		
		unit.ArmorClass = newArmorClass;
		db.Units.Update(unit);
		db.SaveChanges();
	}
	
	public static void EditHealthPoint(long unitId, uint newHealthPoint)
	{
		using var db = new AppContext();
		
		var unit = Get(unitId);
		
		if (unit is null)
		{
			Console.WriteLine($"UnitService: The unit ID: [{unitId}] was not found.");
			return ;
		}
		
		unit.HealthPoint = newHealthPoint;
		db.Units.Update(unit);
		db.SaveChanges();
	}
}