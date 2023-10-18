using Microsoft.EntityFrameworkCore;

namespace BattleBot.DataBase
{
	public partial class AppContext : DbContext
	{
		public DbSet<UserTelegram> Users { get; private set; } = null!;
		public DbSet<Unit> Units { get; private set; } = null!;
		public DbSet<BattleSession> BattleSessions { get; private set; } = null!;
		public DbSet<ChatTelegram> ChatsTelegram { get; private set; } = null!;
		public DbSet<MessageTelegram> MessagesTelegram { get; private set; } = null!;

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
		public long TelegramId { get; set; }
		public List<long> Units { get; set; } = null!;
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
		public List<long> Enemies { get; set; } = null!;
		public List<long> Allies { get; set; } = null!;
		public List<string> BattleLog { get; set; } = null!;
	}

	public class ChatTelegram
	{
		public long Id { get; set; }
		public long UserId { get; init; }
		public List<long> Messages { get; init; } = null!;
	}

	public class MessageTelegram
	{
		public long Id { get; set; }
		public long UserId { get; set; }
		public string Message { get; set; } = null!;
		public DateTime Date { get; set; }
	}
	
	public enum ETypeProfile
	{
		Default,
		GameMaster
	}
}