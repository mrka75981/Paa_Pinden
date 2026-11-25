using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaaPindenAPI.Data;
using PaaPindenAPI.Repositories;
using PaaPindenBlazor.Services;
using Shared.Models;

namespace Tests;

[TestClass]
public sealed class ProjectRepositoryUnitTest
{
    private PaaPindenContext? paaPindenContext;
    private Project? testProject;
    private IProjectRepository? testProjectRepo;

    [TestInitialize]
    public async Task Init()
    {
        var options = new DbContextOptionsBuilder<PaaPindenContext>()
            .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True")
            .Options;

        paaPindenContext = new PaaPindenContext(options);
        await paaPindenContext.Database.EnsureCreatedAsync();

        // Clear existing data
        paaPindenContext.Projects.RemoveRange(paaPindenContext.Projects);
        await paaPindenContext.SaveChangesAsync();
        // Add test data
        testProject = new Project
        {
            Title = "Test Project",
            Note = "This is a test project.",
            Needle = "Size 8",
            YarnType = "Wool",
            Recipe = "Test Recipe",
            Round = 10,
            Stitch = 20,
            UserId = "84cd148c-e055-4d01-8a32-d06e7489e2a3"
        };
        paaPindenContext.Projects.Add(testProject);
        await paaPindenContext.SaveChangesAsync();

        testProjectRepo = new ProjectRepository(paaPindenContext);
    }

    [TestMethod]
    public async Task CreateProjectAsync_Test()
    {
        // Arrange
        var testProject = new Project
        {
            Title = "Created Project",
            Note = "This is a new test project.",
            Needle = "Size 9",
            YarnType = "Good Wool",
            Recipe = "New Test Recipe",
            Round = 12,
            Stitch = 10,
            UserId = "84cd148c-e055-4d01-8a32-d06e7489e2a3"
        };

        // Act
        await testProjectRepo.CreateProjectAsync(testProject);

        // Assert
        var createdProject = paaPindenContext.Projects.FirstOrDefault(p => p.Id == testProject.Id);
        Assert.IsNotNull(createdProject);
        Assert.AreEqual(testProject.Title, createdProject.Title);
    }

    [TestMethod]
    public async Task UpdateProjectAsync_Test()
    {
        // Arrange
        testProject.Title = "Updated Project";
        
        // Act
        await testProjectRepo.UpdateProjectAsync(testProject);
        
        // Assert
        var updatedProject = paaPindenContext.Projects.FirstOrDefault(p => p.Id == testProject.Id);
        Assert.IsNotNull(updatedProject);
        Assert.AreEqual("Updated Project", updatedProject.Title);
    }

    [TestMethod]
    public async Task DeleteProjectAsync_Test()
    {
        // Act
        await testProjectRepo.DeleteProjectAsync(testProject.Id);
        // Assert
        var deletedProject = paaPindenContext.Projects.FirstOrDefault(p => p.Id == testProject.Id);
        Assert.IsNull(deletedProject);
    }

    [TestMethod]
    public async Task GetProjectAsync_Test()
    {
        // Act
        var project = await testProjectRepo.GetProjectAsync(testProject.Id);
        // Assert
        Assert.IsNotNull(project);
        Assert.AreEqual(testProject.Title, project.Title);
    }

    [TestMethod]
    public async Task GetUserProjectsAsync_Test()
    {
        // To-Do: Create user for the test and assign the project to that user

        // Act
        var projects = await testProjectRepo.GetUserProjectsAsync(testProject.UserId);
        // Assert
        Assert.IsNotNull(projects);
        Assert.IsTrue(projects.Any(p => p.Id == testProject.Id));
    }
}

    
