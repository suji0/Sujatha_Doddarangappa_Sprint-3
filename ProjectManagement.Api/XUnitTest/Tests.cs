using Newtonsoft.Json;
using ProjectManagement.Api;
using ProjectManagement.Entities;
using ProjectManagement.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    [Collection("Sequential")]
    public class ControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CustomWebApplicationFactory<Startup> _factory { get; }

        public ControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("api/Project")]
        [InlineData("api/User")]
        [InlineData("api/Task")]
        public async void GetHttpRequestUrl(string url)
        {
            ///Arrange
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("api/Project")]
        [InlineData("api/User")]
        [InlineData("api/Task")]
        public async void GetHttpRequest(string url)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<List<object>>();
            Assert.Single(res);
        }

        [Theory]
        [InlineData("api/Project/1")]
        [InlineData("api/User/1")]
        [InlineData("api/Task/1")]
        public async void GetHttpRequestWithParameter(string url)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<BaseEntity>();

            Assert.Equal(1, res.ID);
        }

        [Theory]
        [InlineData("api/Project")]
        public async void PutProjectHttpRequestWithParameter(string url)
        {
            //Arrange
            var client = _factory.CreateClient();
            Project project = new Project
            {
                ID = 001,
                Name = "Test",
                CreatedOn = DateTime.Now,
                Detail = "Updated Test Project"
            };
            var json = JsonConvert.SerializeObject(project);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(url, stringContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<int>();

            Assert.Equal(1, res);
        }

        [Theory]
        [InlineData("api/Task")]
        public async void PutTaskHttpRequestWithParameter(string url)
        {
            //Arrange
            var client = _factory.CreateClient();
            Task task = new Task
            {
                ID = 001,
                AssignedToUserID = 001,
                CreatedOn = DateTime.Now,
                ProjectID = 001,
                Status = TaskStatus.New,
                Detail = "Upadted Test task"
            };
            var json = JsonConvert.SerializeObject(task);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(url, stringContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<int>();

            Assert.Equal(1, res);
        }

        [Theory]
        [InlineData("api/User")]
        public async void PutUserHttpRequestWithParameter(string url)
        {
            //Arrange
            var client = _factory.CreateClient();
            User user = new User
            {
                ID = 001,
                FirstName = "tetFirstUpdate",
                LastName = "testLastUpdate",
                Email = "test@gmail.com",
                Password = "Suji8050"
            };
            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(url, stringContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<int>();
            Assert.Equal(1, res);
        }

        [Theory]
        [InlineData("api/Project")]
        public async void PostProjectHttpRequestWithParameter(string url)
        {
            //Arrange
            var client = _factory.CreateClient();
            Project project = new Project
            {
                ID = 002,
                Name = "New Test",
                CreatedOn = DateTime.Now,
                Detail = "New Test Project"
            };
            var json = JsonConvert.SerializeObject(project);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(url, stringContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<int>();

            Assert.Equal(1, res);
        }

        [Theory]
        [InlineData("api/Task")]
        public async void PostTaskHttpRequestWithParameter(string url)
        {
            //Arrange
            var client = _factory.CreateClient();
            Task task = new Task
            {
                ID = 002,
                AssignedToUserID = 001,
                CreatedOn = DateTime.Now,
                ProjectID = 001,
                Status = TaskStatus.New,
                Detail = "Upadted Test task"
            };
            var json = JsonConvert.SerializeObject(task);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(url, stringContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<int>();

            Assert.Equal(1, res);
        }

        [Theory]
        [InlineData("api/User")]
        public async void PostUserHttpRequestWithParameter(string url)
        {
            //Arrange
            var client = _factory.CreateClient();
            User user = new User
            {
                ID = 002,
                FirstName = "NewFirstUpdate",
                LastName = "NewLastUpdates",
                Email = "test@gmail.com",
                Password = "Suji8050"
            };
            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(url, stringContent);

            // Assert  
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<int>();

            Assert.Equal(1, res);
        }

        [Theory]
        [InlineData("api/Login?userId=1&password=Suji8050")]
        public async void LoginHttpRequest(string url)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(url);

            // Assert  
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<bool>();

            Assert.True(res);
        }


        [Theory]
        [InlineData("api/Project?Id=1")]
        [InlineData("api/User?Id=1")]
        [InlineData("api/Task?Id=1")]
        public async void DeleteHttpRequestWithParameter(string url)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.DeleteAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var res = await response.Content.ReadAsAsync<int>();

            Assert.Equal(1, res);
        }
    }
}
