namespace BattleBot.DataBase.Models;

public class BattleSession
{
	public long Id;
	public DateTime StartTime;
	public List<Unit> Enemies;
	public List<Unit> Allies;
	public Dictionary<int, List<string>> BattleLog;
}