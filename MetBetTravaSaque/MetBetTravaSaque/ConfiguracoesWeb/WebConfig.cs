using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;

namespace MetBetTravaSaque.ConfiguracoesWeb
{
    public class WebConfig : IPipe
    {
        public object Run(dynamic input)
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless");
            //options.AddArgument("--no-sandbox");
            options.AddArgument("start-maximized");

            // Inicializando o driver com as opções configuradas
            IWebDriver driver = new ChromeDriver(options);

            string urlPainelMetBet = "https://app.metbet.io/";

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(urlPainelMetBet);

            string userDocumentationPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var builder = new ConfigurationBuilder()
            .AddJsonFile(@$"{userDocumentationPath}\appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            string login = config["CredentialsMetBet:Login"];
            string password = config["CredentialsMetBet:Password"];
            string emailMetbet = config["CredentialsMetBet:Email"];
            string senhaEmail = config["CredentialsMetBet:SenhaEmail"];
            string emailDestinatario1 = config["Credentials:EmailDestinatario1"];

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            input.login = login;
            input.password = password;
            input.wait = wait;
            input.driver = driver;
            input.emailMetbet = emailMetbet;
            input.senhaEmail = senhaEmail;
            input.emailDestinatario1 = emailDestinatario1;

            return input;
        }
    }
}
