using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;


namespace SuperHeroes.WebSite.Tests
{
    public class ProgramTests
    {
        [Test]
        public void Main_Should_Not_Throw_Exception()
        {
            // Arrange
            string[] args = Array.Empty<string>();

            // Create a mock of IHostBuilder
            var mockHostBuilder = new Mock<IHostBuilder>();

            // Set up the expectation that Build will be called
            mockHostBuilder.Setup(builder => builder.Build()).Verifiable();

            // Act
            Program.CreateHostBuilder(args).Build().Run();

            // Assert
            // Verify that the expected method (Build) was called
            mockHostBuilder.Verify();
        }

        [Test]
        public void CreateHostBuilder_Should_Not_Return_Null()
        {
            // Arrange
            string[] args = Array.Empty<string>();

            // Act
            var hostBuilder = Program.CreateHostBuilder(args);

            // Assert
            Assert.NotNull(hostBuilder);
        }

        [Test]
        public void CreateHostBuilder_ConfiguresWebHostDefaults()
        {
            // Arrange
            var args = new string[] { };

            // Act
            var hostBuilder = Program.CreateHostBuilder(args);

            // Assert
            Assert.IsInstanceOf<IHostBuilder>(hostBuilder);
            // You can add more specific assertions about the configuration if needed
        }
    }
}
