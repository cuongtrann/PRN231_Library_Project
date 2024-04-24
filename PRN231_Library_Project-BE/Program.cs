using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Okta.AspNetCore;
using PRN231_Library_Project.BusinessObject.Mapping;
using PRN231_Library_Project.BusinessObject.Repository;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.DAO;
using PRN231_Library_Project.DataAccess.Models;
using PRN231_Library_Project.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Add authenticate by Okta:

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = "https://dev-59753842.okta.com/oauth2/default";
    options.Audience = "api://default";
});




// Add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add singleton and transient
builder.Services.AddTransient<BookDAO>();
builder.Services.AddTransient<ReviewDAO>();
builder.Services.AddTransient<CheckoutDAO>();
builder.Services.AddTransient<HistoryDAO>();
builder.Services.AddTransient<MessageDAO>();
builder.Services.AddTransient<PaymentDAO>();




builder.Services.AddTransient<BookService>();
builder.Services.AddTransient<ReviewService>();
builder.Services.AddTransient<MessageService>();
builder.Services.AddTransient<AdminService>();
builder.Services.AddTransient<PaymentService>();



builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<ICheckoutRepository, CheckoutRepository>();
builder.Services.AddTransient<IHistoryRepository, HistoryRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();





//builder.Services.AddTransient<PRN231_Library_ProjectContext>();

builder.Services.AddDbContext<PRN231_Library_ProjectContext>();

// Add automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
