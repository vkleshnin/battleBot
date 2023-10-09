using System;
using System.Collections.Generic;

namespace BattleBot.Migrations;

public partial class User
{
    public long Id { get; set; }

    public string Login { get; set; } = null!;

    public DateTime EnterDate { get; set; }

    public DateTime LastDate { get; set; }

    public int TypeProfile { get; set; }
}
