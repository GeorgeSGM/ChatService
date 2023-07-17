using ChatService.Domain;
using System.ComponentModel.DataAnnotations;

namespace UserService.Domain;

public class Chat
{

    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = "";

    public int NumberOfUsers { get; set; }

    public bool FlagZakrep { get; set; } = false;

    public List<Message> Messages { get; set; } = new List<Message>();

}