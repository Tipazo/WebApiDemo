namespace TestWebAPi
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            int resultado = 10;
            Assert.Equal(7+3, resultado);

        }

        [Fact]
        public void TestHola()
        {
            string resultado = "Hola Mundo Comparing changes";

            Assert.Equal("Hola Mundo Comparing changes", resultado);

        }
    }
}