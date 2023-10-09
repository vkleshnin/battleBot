using System;
using System.Collections.Generic;

namespace BattleBot.Migrations;

public partial class Unit
{
    public long Id { get; set; }

    public long Level { get; set; }

    public string Name { get; set; } = null!;

    public long ArmorClass { get; set; }

    public long HealthPoint { get; set; }

    public long MasterId { get; set; }
}
