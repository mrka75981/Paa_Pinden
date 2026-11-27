using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaaPindenAPI.Data;
using PaaPindenBlazor.Services;
using Shared.DTOs;
using Shared.Models;

namespace Tests;

[TestClass]
public class PaaPindenAPIServiceProjectUnitTests
{
    private PaaPindenContext? _paaPindenContextTest;
    private User? _testInitializeUser;
    private Project? _testInitializeProject;
    private Project? _testInitializeProject2;
    private Project? _testInitializeProject3;
    private ServiceProvider? _provider;
    private PaaPindenAPIService? _paaPindenAPIService;

    [TestInitialize]
    public async Task Init()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddHttpClient("PaaPindenClient", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5044/");
        });
        _provider = services.BuildServiceProvider();
        IHttpClientFactory factory = _provider.GetRequiredService<IHttpClientFactory>();
        _paaPindenAPIService = new PaaPindenAPIService(factory);

        await DatabaseInit();
    }

    private async Task DatabaseInit()
    {
        _paaPindenContextTest = new PaaPindenContext(new DbContextOptionsBuilder<PaaPindenContext>()
            .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=PaaPindenDb;Integrated Security=true;Encrypt=true;Trust Server Certificate=True").Options);
        _testInitializeUser = new User()
        {
            Id = "84cd148c-e055-4d01-8a32-d06e7489e2a3",
            UserName = "test",
            NormalizedUserName = "TEST",
            Email = "test@test.test",
            NormalizedEmail = "TEST@TEST.TEST",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEDU5ss3/xPgY9SmYXgjbzqrnwgKE4cQpHkYP+zu0cfUKVes0+fsTo/Y1micgg+lFrQ==",
            SecurityStamp = "E6PKSVRQKLTX6JM23QOOG7NSEMZAT47O",
            ConcurrencyStamp = "451190ea-b605-4320-980a-6b863f169b73",
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0
        };

        _testInitializeProject = new Project()
        {
            Title = "First Project",
            Note = "This is a test project.",
            Needle = "Size 8",
            YarnType = "Wool",
            Recipe = "Test Recipe",
            Round = 10,
            Stitch = 20,
            UserId = _testInitializeUser.Id
        };
        
        _testInitializeProject2 = new Project()
        {
            Title = "Secound Project",
            Note = "This is also a test project.",
            Needle = "Size 10",
            YarnType = "Wool 2",
            Recipe = "Also Test Recipe",
            Round = 12,
            Stitch = 22,
            UserId = _testInitializeUser.Id
        };

        _testInitializeProject3 = new Project()
        {
            Title = "Not updated Project",
            Note = "This is also a test project.",
            Needle = "Size 10",
            YarnType = "Wool 2",
            Recipe = "Also Test Recipe",
            Round = 12,
            Stitch = 22,
            UserId = _testInitializeUser.Id
        };

            _paaPindenContextTest.Projects.Add(_testInitializeProject);
        _paaPindenContextTest.Projects.Add(_testInitializeProject2);
        _paaPindenContextTest.Projects.Add(_testInitializeProject3);
        _paaPindenContextTest.Database.EnsureCreated();
        _paaPindenContextTest.SaveChanges();
    }

    [TestMethod]
    public async Task GetProjectAsync()
    {
        // Arrange
        int projectId = _testInitializeProject.Id;

        // Act
        Project? project = await _paaPindenAPIService.GetProjectAsync(projectId);

        // Assert
        Assert.IsNotNull(project);
        Assert.AreEqual(_testInitializeProject.Title, project.Title);
        Assert.AreEqual(_testInitializeProject.Note, project.Note);
        Assert.AreEqual(_testInitializeProject.Needle, project.Needle);
        Assert.AreEqual(_testInitializeProject.YarnType, project.YarnType);
        Assert.AreEqual(_testInitializeProject.Recipe, project.Recipe);
        Assert.AreEqual(_testInitializeProject.Round, project.Round);
        Assert.AreEqual(_testInitializeProject.Stitch, project.Stitch);
        Assert.AreEqual(_testInitializeProject.UserId, project.UserId);

    }

    [TestMethod]
    public async Task GetUserProjectsAsync()
    {
        // Arrange
        string userId = _testInitializeUser.Id;

        // Act
        List<Project> userProjects = await _paaPindenAPIService.GetUserProjectsAsync(userId);

        // Assert
        Assert.IsNotNull(userProjects);
        Assert.IsTrue(userProjects.Count > 0);
        Assert.AreEqual(_testInitializeProject.Title, userProjects[0].Title);
        Assert.AreEqual(_testInitializeProject.Note, userProjects[0].Note);
        Assert.AreEqual(_testInitializeProject.Needle, userProjects[0].Needle);
        Assert.AreEqual(_testInitializeProject.YarnType, userProjects[0].YarnType);
        Assert.AreEqual(_testInitializeProject.Recipe, userProjects[0].Recipe);
        Assert.AreEqual(_testInitializeProject.Round, userProjects[0].Round);
        Assert.AreEqual(_testInitializeProject.Stitch, userProjects[0].Stitch);
        Assert.AreEqual(_testInitializeProject.UserId, userProjects[0].UserId);
        Assert.AreEqual(_testInitializeProject2.Title, userProjects[1].Title);
        Assert.AreEqual(_testInitializeProject2.Note, userProjects[1].Note);
        Assert.AreEqual(_testInitializeProject2.Needle, userProjects[1].Needle);
        Assert.AreEqual(_testInitializeProject2.YarnType, userProjects[1].YarnType);
        Assert.AreEqual(_testInitializeProject2.Recipe, userProjects[1].Recipe);
        Assert.AreEqual(_testInitializeProject2.Round, userProjects[1].Round);
        Assert.AreEqual(_testInitializeProject2.Stitch, userProjects[1].Stitch);
        Assert.AreEqual(_testInitializeProject2.UserId, userProjects[1].UserId);
    }

    [TestMethod]
    public async Task CreateProjectAsync()
    {
        // Arrange
        Project project = new Project
        {
            Title = "New Project",
            Note = "This is a new test project.",
            Needle = "Size 9",
            YarnType = "Good Wool",
            Recipe = "New Test Recipe",
            Round = 12,
            Stitch = 10,
            UserId = _testInitializeUser.Id
        };

        // Act
        await _paaPindenAPIService.CreateProjectAsync(project);

        // Assert
        Project? createdProject = _paaPindenContextTest.Projects.FirstOrDefault(p => p.Title == project.Title);
        Assert.IsNotNull(createdProject);
        Assert.AreEqual(project.Title, createdProject.Title);
        Assert.AreEqual(project.Note, createdProject.Note);
        Assert.AreEqual(project.Needle, createdProject.Needle);
        Assert.AreEqual(project.YarnType, createdProject.YarnType);
        Assert.AreEqual(project.Recipe, createdProject.Recipe);
        Assert.AreEqual(project.Round, createdProject.Round);
        Assert.AreEqual(project.Stitch, createdProject.Stitch);
        Assert.AreEqual(project.UserId, createdProject.UserId);
    }

    [TestMethod]
    public async Task UpdateProjectAsync()
    {
        // Arrange
        Project project = new Project
        {
            Id = _testInitializeProject3.Id,
            Title = "Updated Project",
            Note = "This is an updated test project.",
            Needle = "Size 10",
            YarnType = "Better Wool",
            Recipe = "Updated Test Recipe",
            Round = 15,
            Stitch = 12,
            UserId = _testInitializeUser.Id
        };

        // Act
        await _paaPindenAPIService.UpdateProjectAsync(project);

        // Assert
        Project? updatedProject = await _paaPindenContextTest.Projects
    .AsNoTracking()
    .FirstOrDefaultAsync(p => p.Id == project.Id);
        Assert.IsNotNull(updatedProject);
        Assert.AreEqual(project.Title, updatedProject.Title);
        Assert.AreEqual(project.Note, updatedProject.Note);
        Assert.AreEqual(project.Needle, updatedProject.Needle);
        Assert.AreEqual(project.YarnType, updatedProject.YarnType);
        Assert.AreEqual(project.Recipe, updatedProject.Recipe);
        Assert.AreEqual(project.Round, updatedProject.Round);
        Assert.AreEqual(project.Stitch, updatedProject.Stitch);
        Assert.AreEqual(project.UserId, updatedProject.UserId);
    }

    [TestMethod]
    public async Task DeleteProjectAsync()
    {
        // Arrange
        int projectId = _testInitializeProject.Id;

        // Act
        await _paaPindenAPIService.DeleteProjectAsync(projectId);

        // Assert
        Project? deletedProject = _paaPindenContextTest.Projects.FirstOrDefault(p => p.Id == projectId);
        Assert.IsNull(deletedProject);
    }
}
