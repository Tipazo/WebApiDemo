namespace TestWebAPi
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            int resultado = 105;
            Assert.Equal(7+3, resultado);

        }

        [Fact]
        public void TestHola()
        {
            string resultado = "Hola Mundo Comp";

            Assert.Equal("Hola Mundo Comparing changes", resultado);

        }
    }
}