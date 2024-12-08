using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MVC.Models;
using CharacterAI.Infrastructure;
using CharacterAI.Domain.Models;


namespace MVC.Controllers;

public class HomeController : Controller
{
    List<string> messages = new();
    List<ChatMessage> chatMessages = new();

    public IActionResult Index()
    {
        return View();
    }

    void ReadMessages()
    {
        messages = System.IO.File.ReadAllLines("/Users/hakan/Documents/GitHub/CharacterAI/CharacterAI.Persistence/chat.txt").ToList();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        ReadMessages();
        
        return View(messages);
    }
    [HttpPost]
    public async Task<IActionResult> Privacy(string message)
    {
        System.IO.File.AppendAllText("/Users/hakan/Documents/GitHub/CharacterAI/CharacterAI.Persistence/chat.txt", message + "\n");

        ReadMessages();



        

        for (int i = 0; i < messages.Count; i++)
        {
            chatMessages.Add(new ChatMessage
            {
                role = i % 2 == 1 ? "user" : "system",
                content = messages[i]
            });
        }

        chatMessages.Insert(0, new ChatMessage
        {
            role = "system",
            content = "You are Spider-Man, the friendly neighborhood superhero known for your quick wit, bravery, and sense of responsibility. You are always ready to help people in need, whether they face dangerous villains like the Green Goblin or common street crimes like theft. If the user shares a problem, respond as Spider-Man would — with confidence, compassion, and a touch of humor. Never mention the police; you are the first and best line of defense. Stay true to Spider-Man's character: heroic, resourceful, and always ready to save the day. Max 2 sentences."


            // content = "Spiderman lives his life by the creed \"With great power, there must also come great responsibility.\" As such, he always tries using his powers for the greater good, sacrificing his mental and physical health, his personal life, and relationships for the citizens of New York and the world at large. Although an adult, Peter still retains fragments of his awkward, geeky persona from his adolescent years as a social outcast, shown by how he repeatedly stumbles over his own words."
        });

        // Call the API with the formatted messages

        string response = await GPTService.AskToChatGPT(chatMessages.ToArray());

        System.IO.File.AppendAllText("/Users/hakan/Documents/GitHub/CharacterAI/CharacterAI.Persistence/chat.txt", response + "\n");


        return Privacy();
    }
}