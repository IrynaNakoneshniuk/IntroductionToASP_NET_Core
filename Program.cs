using Newtonsoft.Json;
using System.Runtime.CompilerServices;

string[] RandomPhrase = { "Lorem Ipsum és un text de farciment usat per la indústria de la tipografia i la impremta.",
"Lorem Ipsum ha estat el text estàndard de la indústria des de l'any 1500","No només ha sobreviscut cinc segles, sinó que ha fet el salt cap a la creació de"};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;

    if (request.Path == "/task1")
    {
        await response.WriteAsync("Hello!");
    }
    else if (request.Path == "/task2")
    {
        DateTime currentTime = DateTime.Now;
        DateTime currentTimeData = new DateTime
        (currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, currentTime.Minute, currentTime.Second);
        await response.WriteAsync($"Date:{currentTimeData}");
    }
    else if (request.Path == "/task3")
    {
        string filePath = "./public/user.json";
        try
        {
            User person = new User("23", "Ivan");
            string jsonString = JsonConvert.SerializeObject(person);
            if(File.Exists(filePath))
            {
                await File.WriteAllTextAsync(filePath, jsonString);
                await response.SendFileAsync(filePath);
            }
        }
        catch {
        }
    }else if(request.Path == "/task4")
    {
        string filePath = "./public/user.json";
        var jsnString =await File.ReadAllTextAsync(filePath);
        var person = JsonConvert.DeserializeObject<User>(jsnString);
        if (person != null)
        {
            await response.WriteAsync($"Name : {person.name}, Age:{person.age}");
        }
    }
    else if(request.Path == "/task5")
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("public/index.html");
        Random random = new Random();
        var index = random.Next(3);
        await response.WriteAsync(RandomPhrase[index]);
    }
});
app.Run();
record User(string age,string name);

