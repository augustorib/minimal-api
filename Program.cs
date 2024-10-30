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
    ErrosDeValidacao ValidaDTO(VeiculoDTO veiculoDTO)
    {
        var validacao = new ErrosDeValidacao{
            Mensagens = new List<string>()
        };

        if(string.IsNullOrEmpty(veiculoDTO.Nome))
            validacao.Mensagens.Add("O nome não pode ser vazio");

        if(string.IsNullOrEmpty(veiculoDTO.Marca))
            validacao.Mensagens.Add("A marca não pode ser vazia");

        if(veiculoDTO.Ano < 1950)
            validacao.Mensagens.Add("Veículo muito antigo, aceito somente anos superiores de 1950");

        return validacao;
    }

    app.MapPost("/veiculo", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoService veiculoService) => {

        var validacao = ValidaDTO(veiculoDTO);

        if(validacao.Mensagens.Count() > 0)
            return Results.BadRequest(validacao);

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

    app.MapGet("/veiculo/{id}", ([FromRoute] int id, IVeiculoService veiculoService) => {
 
        var veiculo = veiculoService.FindById(id);

        if(veiculo == null)
            return Results.NotFound();
        
        return Results.Ok(veiculo);
        
    }).WithTags("Veiculos");

    app.MapPut("/veiculo/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, IVeiculoService veiculoService) => {
 
        var veiculo = veiculoService.FindById(id);

        if(veiculo == null)
            return Results.NotFound();
        
        
        var validacao = ValidaDTO(veiculoDTO);

        if(validacao.Mensagens.Count() > 0)
            return Results.BadRequest(validacao);
            
        veiculo.Nome = veiculoDTO.Nome;
        veiculo.Marca = veiculoDTO.Marca;
        veiculo.Ano = veiculoDTO.Ano;

        veiculoService.Update(veiculo);

        return Results.Ok(veiculo);

    }).WithTags("Veiculos");

    app.MapDelete("/veiculo/{id}", ([FromRoute] int id, IVeiculoService veiculoService) => {
 
        var veiculo = veiculoService.FindById(id);

        if(veiculo == null)
            return Results.NotFound();
        
        veiculoService.Delete(veiculo);

        return Results.NoContent();
        
    }).WithTags("Veiculos");
#endregion

#region App
    app.UseSwagger();
    app.UseSwaggerUI();

    app.Run();
#endregion

