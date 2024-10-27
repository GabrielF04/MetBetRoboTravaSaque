using OpenQA.Selenium;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MetBetTravaSaque.Program;

namespace MetBetTravaSaque.AcoesRobo
{
    public class TravaSaque : IPipe
    {
        public object Run(dynamic input)
        {
            IWebDriver driver = input.driver;
            string userDocumentationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var wait = input.wait;
            TimeSpan sixAM = input.sixAM;
            TimeSpan tenPM = input.tenPM;
            TimeSpan currentTime = input.currentTime;

            string diretorioPrint = $@"{userDocumentationPath}\TravaSaquePrints";
            string nomeArquivoPrint = "RetiradaMaxima.png";

            string caminhoCompleto = Path.Combine(diretorioPrint, nomeArquivoPrint);

            if (Directory.Exists(diretorioPrint))
            {
                // Deleta todos os arquivos do diretório
                foreach (var file in Directory.GetFiles(diretorioPrint))
                {
                    File.Delete(file);
                }
            }
            else
            {
                // Caso o diretório não exista, crie-o
                Directory.CreateDirectory(diretorioPrint);
            }

            //INTERAGINDO COM ADMINISTRACAO
            IWebElement administracao = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Administração')]")));
            administracao.Click();

            IWebElement saques = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://app.metbet.io/withdraw-settings\']")));
            saques.Click();

            IWebElement saqueMaximo = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("(//*[@id=\'label-undefined\'])[2]")));

            IWebElement saqueMaximoAutomatico = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("(//*[@id=\'label-undefined\'])[4]")));

            //O ROBO VERIFICA A HORA ATUAL PARA INTERAGIR COM O SAQUE MAXIMO

            if (currentTime >= sixAM && currentTime < tenPM)
            {
                saqueMaximo.Clear();
                saqueMaximo.SendKeys("1000000");
                Thread.Sleep(1000);
                saqueMaximoAutomatico.Clear();
                saqueMaximoAutomatico.SendKeys("1000000");
            }
            else if (currentTime >= tenPM)
            {
                saqueMaximo.Clear();
                saqueMaximo.SendKeys("100000");
                Thread.Sleep(1000);
                saqueMaximoAutomatico.Clear();
                saqueMaximoAutomatico.SendKeys("100000");
            }

            Thread.Sleep(1000);
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile($"{caminhoCompleto}.Png");

            IWebElement btnSalvar = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Salvar alterações')]")));
            btnSalvar.Click();
            Thread.Sleep(2000);
            return input;
        }
    }
}
