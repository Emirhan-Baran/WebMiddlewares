var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddHttpLogging();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();

app.Use(async(context, next) =>
{

    Console.WriteLine("Request Path: "+context.Request.Path);

    await next(context);

    Console.WriteLine("ResponseStatusCode: "+context.Response.StatusCode);

});

app.Use(async (context,next) =>
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
    Console.WriteLine("Start time : "+ DateTime.Now);
    await next(context);
    watch.Stop();
    Console.WriteLine("Response time : "+watch.ElapsedMilliseconds+ "ms");

});

app.Run();
