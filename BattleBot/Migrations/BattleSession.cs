using System;
using System.Collections.Generic;

namespace BattleBot.Migrations;

public partial class BattleSession
{
    public long Id { get; set; }

    public DateTime StartTime { get; set; }

    public long[] Enemies { get; set; } = null!;

    public long[] Allies { get; set; } = null!;

    public string[] BattleLog { get; set; } = null!;
}
