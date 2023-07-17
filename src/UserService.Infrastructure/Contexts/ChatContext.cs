using ChatService.Domain;
using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Infrastructure.Contexts
{
    /// <summary>
    ///     Контекст для работы с чатами
    /// </summary>
    public sealed class ChatContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Message>()
                .HasOne<Chat>()
                .WithMany(c => c.Messages)  // Указываем навигационное свойство в классе Chat
                .HasForeignKey(m => m.ChatId);

            base.OnModelCreating(modelBuilder);
        }
    }
}