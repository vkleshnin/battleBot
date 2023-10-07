using Microsoft.EntityFrameworkCore;

namespace BattleBot.DataBase
{
	public class AppContext : DbContext
	{
		private DbSet<User> Users { get; set; }
		private DbSet<Unit> Units { get; set; }
		private DbSet<BattleSession> BattleSessions { get; set; }
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseNpgsql("Host=localhost;Database=db_battle_log_bot;Username=admin;Password=root");
	}
	
	public class User
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public DateTime EnterDate { get; set; }
		public DateTime LastDate { get; set; }
		public ETypeProfile TypeProfile { get; set; }
	}
	
	public class Unit
	{
		public long Id { get; set; }
		public uint Level { get; set; }
		public string Name { get; set; }
		public uint ArmorClass { get; set; }
		public uint HealthPoint { get; set; }
		public long MasterId { get; set; }
	}
	public class BattleSession
	{
		public long Id { get; set; }
		public DateTime StartTime { get; set; }
		public IEnumerable<long> Enemies { get; set; }
		public IEnumerable<long> Allies { get; set; }
		public IEnumerable<string> BattleLog { get; set; }
	}
	
	public enum ETypeProfile
	{
		Player,
		GameMaster
	}
}