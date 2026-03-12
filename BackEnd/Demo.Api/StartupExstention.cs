namespace Demo.Api
{
    public static class StartupExstention
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                await Demo.Identity.Seed.AddRoleAdmin.SeedAsync(scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Demo.Identity.Entities.ApplicationRole>>());
                await Demo.Identity.Seed.AddFirstUser.SeedAsync(scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Demo.Identity.Entities.ApplicationUser>>());
            }
            return app;
        }
    }
}
