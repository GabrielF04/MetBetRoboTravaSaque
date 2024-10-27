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
    public class PainelLogin : IPipe
    {
        public object Run(dynamic input)
        {
            var wait = input.wait;
            string login = input.login;
            string password = input.password;

            IWebElement campoLogin = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@id=\'email\']")));
            campoLogin.SendKeys(login);

            IWebElement campoSenha = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//input[@id=\'password\']")));
            campoSenha.SendKeys(password);
            
            IWebElement btnLogin = wait.Until(CustomExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\'app\']/div/div/div/div/div[2]/form/div[3]/button")));
            btnLogin.Click();

            return input;
        }
    }
}
