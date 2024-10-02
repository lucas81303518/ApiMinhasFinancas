using BibliotecaMinhasFinancas.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiMinhasFinancas.Data
{
    public class DatabaseInicializer
    {
        public static async Task SeedRolesAndUsers(UserManager<Usuarios> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            var roleExists = await roleManager.RoleExistsAsync("Administrador");
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Administrador"));
            }

            var adminUser = new Usuarios
            {
                UserName = "admin",
                NomeCompleto = "Lucas Lira Ferreira",
                Email = "lucas_81303518@live.com",
                Situacao = true,
                DataNascimento = new DateTime(1999, 7, 28) 
            };

            var result = await userManager.CreateAsync(adminUser, "Samuel172839*");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Administrador");
            }
        }
    }
}
