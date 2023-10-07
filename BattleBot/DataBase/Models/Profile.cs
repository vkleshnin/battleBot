namespace BattleBot.DataBase.Models;

public enum ETypeProfile
{
	Player,
	GameMaster
}

public class Profile
{
	public long Id;
	public string Name;
	public Unit Character;
	public DateTime EnterDate;
	public DateTime LastDate;
	public ETypeProfile TypeProfile;
}