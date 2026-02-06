using Xunit;

namespace MFBC.Core.Tests;

public class PieceTests
{
    [Fact]
    public void Constructor_Throws_WhenIdEmpty()
    {
        Assert.Throws<ArgumentException>(() => new Piece(Guid.Empty, PieceType.Rook, Side.White, new Coord(0, 0)));
    }
}
