namespace TestWebAPi
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            int resultado = 300;
            Assert.Equal(200+100, resultado);

        }

        [Fact]
        public void TestHola()
        {
            string resultado = "Hola Mundo Computing";

            Assert.Equal("Hola Mundo Computing", resultado);

        }
    }
}