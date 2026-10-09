using System.Text;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<ComputerComponent> computerComponents = new List<ComputerComponent>() 
{
    new ComputerComponent(){Name = "GeForce RTX 5070", ComponentType = ComputerComponent.Type.GPU, Price = 5000.0},
    new ComputerComponent(){Name = "AMD Ryzen 7 5700X", ComponentType = ComputerComponent.Type.CPU, Price = 5000.0},
    new ComputerComponent(){Name = "Kingston FURY 32 GB (2x16GB) DDR5 6000 MHz Beast EXPO", ComponentType = ComputerComponent.Type.RAM, Price = 5000.0}
};
app.Run(async (context) =>
{
    using StreamReader reader = new StreamReader(context.Request.Body);
    string message = await reader.ReadToEndAsync();

    var selectedComputerComponents = computerComponents.Where(e => e.Name.Contains(message));

    StringBuilder fullList = new StringBuilder();

    foreach(var item in selectedComputerComponents)
    {
        fullList.Append($"{item.Name}\nType: {item.ComponentType}\nPrice: {item.Price}\n--\n");
    }

    await context.Response.WriteAsync(fullList.ToString());

});

app.Run();

struct ComputerComponent
{
    public string Name { get; set; }

    public Type ComponentType { get; set; }

    public double Price { get; set; }
    public enum Type
    {
        CPU,
        GPU,
        RAM
    }
}
