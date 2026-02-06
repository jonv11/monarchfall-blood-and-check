using MFBC.Core;

namespace MFBC.Cli;

internal static class MoveParser
{
    public static bool TryParseCoord(string input, out Coord coord)
    {
        coord = default;
        if (string.IsNullOrWhiteSpace(input) || input.Length != 2)
        {
            return false;
        }

        var file = char.ToLowerInvariant(input[0]);
        var rank = input[1];
        if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
        {
            return false;
        }

        var x = file - 'a';
        var y = rank - '1';
        coord = new Coord(x, y);
        return true;
    }

    public static bool TryParseMove(string input, out Coord from, out Coord to)
    {
        from = default;
        to = default;
        if (string.IsNullOrWhiteSpace(input) || input.Length != 4)
        {
            return false;
        }

        return TryParseCoord(input[..2], out from) && TryParseCoord(input[2..], out to);
    }
}
