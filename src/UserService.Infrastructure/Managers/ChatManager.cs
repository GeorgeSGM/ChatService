using ChatService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Domain;
using UserService.Infrastructure.Contexts;

namespace UserService.Infrastructure.Managers
{
    public class ChatManager : IChatManager
    {
        private readonly ChatContext _context;

        public ChatManager(ChatContext context)
        {
            _context = context;
        }

        public List<Chat> GetAll()
        {
            return _context.Chats.ToList();
        }
        public Chat? GetById(long id)
        {
            return _context.Chats.FirstOrDefault(x => x.Id == id);
        }

        public Chat Create(Chat chat)
        {
            var entry = _context.Add(chat);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Chat? Update(Chat chat)
        {
            var existingChat = _context.Chats.FirstOrDefault(x => x.Id == chat.Id);
            if (existingChat is null)
            {
                return null;
            }

            existingChat.Name = chat.Name;
            existingChat.NumberOfUsers = chat.NumberOfUsers;
            existingChat.FlagZakrep = chat.FlagZakrep;

            _context.Update(existingChat);
            _context.SaveChanges();
            return existingChat;
        }

        public Chat? Delete(long id)
        {
            var existingChat = _context.Chats.FirstOrDefault(x => x.Id == id);
            if (existingChat is null)
            {
                return null;
            }

            var entry = _context.Remove(existingChat);
            _context.SaveChanges();
            return entry.Entity;
        }

        
    }
}