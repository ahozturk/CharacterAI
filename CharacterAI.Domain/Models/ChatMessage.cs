using System;

namespace CharacterAI.Domain.Models;

public class ChatMessage
{
    public string role { get; set; }
    public string content { get; set; }
}
