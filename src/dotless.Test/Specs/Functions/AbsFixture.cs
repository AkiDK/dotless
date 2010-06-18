namespace dotless.Test.Specs.Functions
{
    using NUnit.Framework;

    public class AbsFixture : SpecFixtureBase
    {
        [Test]
        public void Abs()
        {
            AssertExpression("5", "abs(-5)");
            AssertExpression("5", "abs(5)");
            AssertExpression("5px", "abs(-5px)");
            AssertExpression("5px", "abs(5px)");
        }

        [Test]
        public void ThrowsIfIncorrectType()
        {
            AssertExpressionError("Expected number in function 'abs', found #aaaaaa", 4, "abs(#aaa)");
        }
    }
}