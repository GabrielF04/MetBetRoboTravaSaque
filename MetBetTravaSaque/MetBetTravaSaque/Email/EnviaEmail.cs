﻿using OpenQA.Selenium;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MetBetTravaSaque.Program;

namespace MetBetTravaSaque.Email
{
    public class EnviaEmail : IPipe
    {
        public object Run(dynamic input)
        {
            string userDocumentationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string emailMetbet = input.emailMetbet;
            string senhaEmail = input.senhaEmail;
            string emailDestinatario1 = input.emailDestinatario1;
            //string emailDestinatario2 = input.emailDestinatario2;
            //string emailDestinatario3 = input.emailDestinatario3;

            var wait = input.wait;
            IWebDriver driver = input.driver;
            int attempts = 0;
            bool clicked = false;
            int maxRetries = 5;
            string urlOutlook = "https://login.live.com/login.srf?wa=wsignin1.0&rpsnv=159&ct=1724896707&rver=7.0.6738.0&wp=MBI_SSL&wreply=https%3a%2f%2foutlook.live.com%2fowa%2f%3fnlp%3d1%26cobrandid%3dab0455a0-8d03-46b9-b18b-df2f57b9e44c%26RpsCsrfState%3da98fda45-28ce-a3a8-4063-164197ab1381&id=292841&aadredir=1&CBCXT=out&lw=1&fl=dob%2cflname%2cwld&cobrandid=ab0455a0-8d03-46b9-b18b-df2f57b9e44c";

            string diretorioPrint = $@"{userDocumentationPath}\TravaSaquePrints";
            string nomeArquivoPrint = "RetiradaMaxima.png";

            string caminhoCompleto = Path.Combine(diretorioPrint, nomeArquivoPrint);

            driver.Navigate().GoToUrl(urlOutlook);


            //IWebElement btnEntrar = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//a[@href=\'https://go.microsoft.com/fwlink/p/?linkid=2125442\']")));
            //btnEntrar.Click();

            IWebElement campoEmail = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@id=\'i0116\']")));
            campoEmail.SendKeys(emailMetbet);

            IWebElement btnAvancar = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@id=\'idSIButton9\']")));
            btnAvancar.Click();

            IWebElement campoSenha = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@id=\'i0118\']")));
            campoSenha.SendKeys(senhaEmail);

            IWebElement btnEntrarEmail = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@id=\'idSIButton9\']")));
            btnEntrarEmail.Click();


            IWebElement btnSim = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//button[@id=\'acceptButton\']")));
            btnSim.Click();
            Thread.Sleep(1000);

            while (attempts < maxRetries && !clicked)
            {
                try
                {
                    Thread.Sleep(1000);
                    // Tenta encontrar o elemento e clicar
                    IWebElement btnNovoEmail = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Novo email')]")));
                    btnNovoEmail.Click();

                    IWebElement campoDestinatario1 = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\'docking_InitVisiblePart_0\']/div/div[3]/div[1]/div/div[3]/div/div/div[1]")));
                    campoDestinatario1.SendKeys(emailDestinatario1);

                    IWebElement virgula1 = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\'docking_InitVisiblePart_0\']/div/div[3]/div[1]/div/div[3]/div/div/div[1]")));
                    virgula1.SendKeys(",");

                    //IWebElement campoDestinatario2 = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\'docking_InitVisiblePart_0\']/div/div[3]/div[1]/div/div[3]/div/div/div[1]")));
                    //campoDestinatario2.SendKeys(emailDestinatario2);

                    //IWebElement virgula2 = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\'docking_InitVisiblePart_0\']/div/div[3]/div[1]/div/div[3]/div/div/div[1]")));
                    //virgula2.SendKeys(",");

                    //IWebElement campoDestinatario3 = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\'docking_InitVisiblePart_0\']/div/div[3]/div[1]/div/div[3]/div/div/div[1]")));
                    //campoDestinatario3.SendKeys(emailDestinatario3);

                    IWebElement campoAssunto = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder=\'Adicionar um assunto\']")));
                    campoAssunto.SendKeys("O Robô Trava Saque rodou corretamente. Se possível, verifique as configurações de saque.");

                    Thread.Sleep(1000);

                    IWebElement btnEnviar = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Enviar')]")));
                    btnEnviar.Click();
                    clicked = true; // Se conseguir clicar, termina o loop
                }
                catch (NoSuchElementException)
                {
                    // O elemento não foi encontrado, continua tentando
                }
                catch (ElementClickInterceptedException)
                {
                    // Outro elemento está bloqueando o clique, continua tentando
                }
                catch (WebDriverTimeoutException)
                {
                    // O elemento não foi encontrado dentro do tempo de espera, continua tentando
                }

                attempts++;
                if (!clicked)
                {
                    // Espera um breve intervalo antes de tentar novamente
                    System.Threading.Thread.Sleep(1000); // 1 segundo
                }
            }


            return input;
        }
    }
}
