using ChatService.Domain;
using UserService.Domain;
using UserService.Infrastructure.Contexts;

namespace UserService.Infrastructure.Managers
{
    /// <summary>
    ///     Реализация интерфейса <see cref="IChatManager"/>
    /// </summary>
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
            existingChat.Messages = chat.Messages;

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

        public Message AddMessage(long chatId, string message)
        {
            var chat = _context.Chats.FirstOrDefault(x => x.Id == chatId);
            if (chat is null)
            {
                throw new ArgumentException($"Chat with id {chatId} not found");
            }

            var newMessage = new Message(message);
            newMessage.ChatId = chat.Id;
            chat.Messages.Add(newMessage);

            _context.SaveChanges();

            return newMessage;
        }

        public List<Message> GetChatHistory(long chatId)
        {
            var chat = _context.Chats.FirstOrDefault(x => x.Id == chatId);
            if (chat is null)
            {
                throw new ArgumentException($"Chat with id {chatId} not found");
            }

            return chat.Messages;
        }

        public Chat GetChatInfo(long chatId)
        {
            var chat = _context.Chats.FirstOrDefault(x => x.Id == chatId);
            if (chat is null)
            {
                throw new ArgumentException($"Chat with id {chatId} not found");
            }

            return chat;
        }
    }
}