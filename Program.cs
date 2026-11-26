using AddisEats.Api.Services.MenuItemServices;
using AddisEats.Api.Service.RestaurantServices;
using AddisEats.Api.Services.OrderService;
using AddisEats.Api.Middleware;

using System.Reflection;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    Options => 
    {Options.SwaggerDoc(
        "v1", 
        new OpenApiInfo
    {
        Title = "Addis Eats API",
        Version = "V1",
         Description = "Ordering and delivery API for restaurant in "
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
});



builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IRestaurantServices, RestaurantService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var freeAboveEtb = builder.Configuration.GetValue<string>("Delivery:FreeAboveEtb");
Console.WriteLine("free above etb");


var app = builder.Build();


app.Use(async (context, next) => {
    Console.WriteLine("The Reauest is coming in");
    
    await next(context);
    
    Console.WriteLine("The Request is going out 1");
    });
// Configure request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseCors("frontend");
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();



