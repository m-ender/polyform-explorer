using NUnit.Framework;

namespace PolyformExplorer.Data.Tests
{
    public class IntVector2Test
    {
        [Test]
        public void Default_constructor_returns_origin()
        {
            var vector = new IntVector2();

            Assert.That(vector.X, Is.Zero);
            Assert.That(vector.Y, Is.Zero);
        }
    }
}