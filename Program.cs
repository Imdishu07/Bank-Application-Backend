using AutoMapper;
using BankApplicationProject;
using BankApplicationProject.Helper;
using BankApplicationProject.Models;
using BankApplicationProject.Repository;
using BankManagementProject.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankManagementContext>();
builder.Services.AddAutoMapper(typeof(ApplicationModelMapping));
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<IAccount, SavingAccountInterest>();
builder.Services.AddTransient<IAccount,CurrentAccountInterest>();


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Allow requests from this origin
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseRouting();

app.UseCors("AllowOrigin"); // Apply CORS policy

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
