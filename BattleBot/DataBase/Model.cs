using Microsoft.EntityFrameworkCore;

namespace BattleBot.DataBase
{
	public partial class AppContext : DbContext
	{
		public DbSet<UserTelegram> Users { get; set; } = null!;
		public DbSet<Unit> Units { get; set; } = null!;
		public DbSet<BattleSession> BattleSessions { get; set; } = null!;
		public DbSet<ChatTelegram> ChatsTelegram { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db_battle_log_bot;" +
			                            "Username=admin;Password=root");
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
	
	public class UserTelegram
	{
		public long Id { get; set; }
		public string Login { get; init; } = null!;
		public DateTime EnterDate { get; set; }
		public DateTime LastDate { get; set; }
		public ETypeProfile TypeProfile { get; set; }
	}
	
	public class Unit
	{
		public long Id { get; set; }
		public uint Level { get; set; }
		public string Name { get; set; } = null!;
		public uint ArmorClass { get; set; }
		public uint HealthPoint { get; set; }
		public long MasterId { get; set; }
	}
	public class BattleSession
	{
		public long Id { get; set; }
		public DateTime StartTime { get; set; }
		public IEnumerable<long>? Enemies { get; set; }
		public IEnumerable<long>? Allies { get; set; }
		public IEnumerable<string>? BattleLog { get; set; }
	}

	public class ChatTelegram
	{
		public UserTelegram User { get; set; } = null!;
		public IEnumerable<MessageTelegram>? Messages { get; set; }
	}

	public class MessageTelegram
	{
		public DateTime Date { get; set; }
		public string Message { get; set; } = null!;
	}
	
	public enum ETypeProfile
	{
		Default,
		GameMaster
	}
}