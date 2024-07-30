///////////////////////////////////////////////////////////////////
//
// Project MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

namespace Youbiquitous.Minimo.Settings.Core;

public class CopyrightSettings
{
    public string Name { get; set; }
    public string Year { get; set; }

    public override string ToString()
    {
        return $"Copyright {Name} (c) {Year}";
    }
}