using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.ModelViews;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

#region Builder
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddScoped<IAdministradorService, AdministradorService>();
    builder.Services.AddScoped<IVeiculoService, VeiculoService>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<DbContexto>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
    );

    var app = builder.Build();
#endregion

#region Home
    app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Admnistradores
    app.MapPost("administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorService administradorService) => {
        if(administradorService.Login(loginDTO) != null)
            return Results.Ok("Login com sucesso!");
        else
            return Results.Unauthorized();        
    }).WithTags("Administradores");
#endregion

#region Veiculo
    app.MapPost("/veiculo", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoService veiculoService) => {
        
        var veiculo = new Veiculo{
            Nome = veiculoDTO.Nome,
            Marca = veiculoDTO.Marca,
            Ano = veiculoDTO.Ano
        };
        
        veiculoService.Create(veiculo);

        return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
    }).WithTags("Veiculos");

    app.MapGet("/veiculo", ([FromQuery] int? pagina, IVeiculoService veiculoService) => {
        
        var veiculos = veiculoService.ListAll(pagina);

        return Results.Ok(veiculos);
    }).WithTags("Veiculos");

#endregion

#region App
    app.UseSwagger();
    app.UseSwaggerUI();

    app.Run();
#endregion

