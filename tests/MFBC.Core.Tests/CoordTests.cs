using Xunit;

namespace MFBC.Core.Tests;

public class CoordTests
{
    [Fact]
    public void ToString_FormatsCoordinates()
    {
        var coord = new Coord(-2, 5);

        Assert.Equal("(-2,5)", coord.ToString());
    }
}
