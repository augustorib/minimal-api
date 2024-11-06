using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

namespace Teste.Domain.Servicos
{
    [TestClass]
    public class AdministradorServicoTest
    {

        private DbContexto CriarContextoDeTeste()
        {

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

            var builder = new ConfigurationBuilder()
                .SetBasePath(path ?? Directory.GetCurrentDirectory())    
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            return new DbContexto(configuration);
        }

        [TestMethod]
        public void TestandoSalvarAdministrador()
        {
            
            //Arrange
            var contexto = CriarContextoDeTeste();
            contexto.Database.ExecuteSqlRaw("TRUNCATE TABLE administradores");

            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
            var administradorServico = new AdministradorService(contexto);

            //Act
            administradorServico.Create(adm);
        
            //Assert
            Assert.AreEqual(1, administradorServico.ListAll(1).Count());
        }

        [TestMethod]
        public void TestandoBuscaPorId()
        {
            
            //Arrange
            var contexto = CriarContextoDeTeste();
            contexto.Database.ExecuteSqlRaw("TRUNCATE TABLE administradores");

            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
            var administradorServico = new AdministradorService(contexto);

            //Act
            administradorServico.Create(adm);
            var admBD = administradorServico.FindById(adm.Id);
            
            //Assert
            Assert.AreEqual(1, admBD?.Id);
        }

        [TestMethod]
        public void TestandoListarTodosAdministradores()
        {
            
            //Arrange
            var contexto = CriarContextoDeTeste();
            contexto.Database.ExecuteSqlRaw("TRUNCATE TABLE administradores");

            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
            var administradorServico = new AdministradorService(contexto);

            var editor = new Administrador();
            editor.Email = "editor@teste.com";
            editor.Senha = "editor";
            editor.Perfil = "Editor";

            //Act
            administradorServico.Create(adm);
            administradorServico.Create(editor);
            
            var admnistradoresBD = administradorServico.ListAll(1);
            
            //Assert
            Assert.AreEqual(2, admnistradoresBD.Count);
        }

        [TestMethod]
        public void TestandoLogineSenhaAdmnistrador()
        {
            
            //Arrange
            var contexto = CriarContextoDeTeste();
            contexto.Database.ExecuteSqlRaw("TRUNCATE TABLE administradores");

            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
            var administradorServico = new AdministradorService(contexto);

            var login = new LoginDTO();
            login.Email = adm.Email;
            login.Senha = adm.Senha;

            //Act
            administradorServico.Create(adm);
            var admnistradorLogin = administradorServico.Login(login);
            
            //Assert
            Assert.IsNotNull(admnistradorLogin);
        }
    }
}