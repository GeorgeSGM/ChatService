using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain;

namespace ChatService.Domain
{
    public class Message
    {
        [Key]
        public long Id { get; set; }

        public string Content { get; set; } = "";

        public long ChatId { get; set; }
        
        public Message(string content, long chatId)
        {
            Content = content;
            ChatId = chatId;
        }
    }
}