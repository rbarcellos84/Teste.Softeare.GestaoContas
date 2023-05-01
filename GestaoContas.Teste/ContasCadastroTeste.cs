using Bogus;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GestaoContas.Teste
{
    public class ContasCadastroTeste
    {
        [Fact]
        public void CadastrarContaComSucesso()
        {
            //abrir navegador
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            //acessando a pagina que sera testada
            driver.Navigate().GoToUrl("http://localhost:5185/Contas/Cadastro");

            //gerando dados fake com bogus
            var faker = new Faker("pt_BR");
            var produto = faker.Commerce.Product();

            //preenchendo campos do formulario
            var nome = driver.FindElement(By.XPath("//*[@id=\"Nome\"]"));
            nome.Clear();
            nome.SendKeys(produto);

            var data = driver.FindElement(By.XPath("//*[@id=\"Data\"]"));
            data.Clear();
            data.SendKeys(DateTime.Now.ToString("dd/MM/yyyy"));

            var valor = driver.FindElement(By.XPath("//*[@id=\"Valor\"]"));
            valor.Clear();
            valor.SendKeys(faker.Commerce.Price(2));

            var tipo = driver.FindElement(By.XPath("//*[@id=\"Tipo\"]"));
            tipo.Click();

            //precionar o botão
            var botao = driver.FindElement(By.XPath("/html/body/div/form/div[4]/div/div/input"));
            botao.Click();

            //capturar a msg do sistema
            var mensagem = driver.FindElement(By.XPath("/html/body/div[1]"));

            //comparadondo o resultado com o esperado, testado a msg exibida
            var resultadoEsperado = $"Conta {produto} cadastrada com sucesso!";
            var resultadoObtido = mensagem.Text;
            Assert.Equal(resultadoEsperado, resultadoObtido);

            //fechar navegador
            driver.Close();
            driver.Quit();
        }
    }
}