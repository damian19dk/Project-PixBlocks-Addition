using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using PixBlocks_Addition.Api;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace PixBlocks_Addition.Tests.EndToEnd.TestControllers
{
    [TestFixture]
    public abstract class UserControllerTest : BaseControllerTest
    {
        public UserDto UserDto { get; set; }

        public UserControllerTest()
        {
            Init().Wait();
        }

        private async Task Init()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("Login", "Login1");
            parameters.Add("E_mail", "email1@wp.pl");
            parameters.Add("Password", "password1");
            parameters.Add("RoleId", "2");
            parameters.Add("Is_premium", "true");
            parameters.Add("Status", "1");

            sendMultiPartAsync("api/identity/register", "POST", parameters).Wait();

            var response = await httpClient.GetAsync("api/user/login?login=Login1");
            var responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(responseString);
            UserDto = user.Single();
        }

        [Test]
        public async Task changing_password_should_work()
        {

            var parameters = new Dictionary<string, string>();
            parameters.Add("newPassword", "password2");
            parameters.Add("login", "Login1");
            parameters.Add("oldPassword", "password1");

            var Login = "Login1";
            var oldPassword = "password2";

            var parameters2 = new Dictionary<string, string>();
            parameters2.Add("Login", Login);
            parameters2.Add("Password", oldPassword);

            await sendMultiPartAsync("api/user/passwordChange", "PUT", parameters);
            Assert.Fail("api/identity/login", "POST", parameters2);
        }
    }
}
