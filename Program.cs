using AddisEats.Api.Services.MenuItemServices;
using AddisEats.Api.Service.RestaurantServices;
using AddisEats.Api.Services.OrderService;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMenuInterfaceService, MenuItemService>();
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

app.UseMiddleware<RequestTimingMiddleware>();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();



