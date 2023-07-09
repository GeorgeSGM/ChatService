using UserService.Domain;
using UserService.Infrastructure.Managers;

namespace UserService.Host.Routes
{
    /// <summary>
    /// Роутер для работы с чатами
    /// </summary>
    public static class ChatRouter
    {
        /// <summary>
        /// Добавляем роутер для работы с чатами
        /// </summary>
        /// <param name="application">Объект приложения</param>
        /// <returns>Модифицированный объект приложения</returns>
        public static WebApplication AddChatRouter(this WebApplication application)
        {
            // Производим группировку логики.
            var chatGroup = application.MapGroup("/api/chats");

            chatGroup.MapGet(pattern: "/", handler: GetAllChats);
            chatGroup.MapGet(pattern: "/{id:long}", handler: GetChatById);
            chatGroup.MapPost(pattern: "/", handler: CreateChat);
            chatGroup.MapPut(pattern: "/", handler: UpdateChat);
            chatGroup.MapDelete(pattern: "/{id:long}", handler: DeleteChat);
            chatGroup.MapPost(pattern: "/{chatId:long}/messages", handler: AddMessage);
            chatGroup.MapGet(pattern: "/{chatId:long}/messages", handler: GetChatHistory);
            chatGroup.MapGet(pattern: "/{chatId:long}", handler: GetChatInfo);

            return application;
        }

        private static IResult GetAllChats(IChatManager chatManager)
        {
            var chats = chatManager.GetAll();
            return Results.Ok(chats);
        }

        private static IResult GetChatById(long id, IChatManager chatManager)
        {
            var chat = chatManager.GetById(id);
            return chat is null ? Results.NotFound() : Results.Ok(chat);
        }

        private static IResult CreateChat(Chat chat, IChatManager chatManager)
        {
            var createdChat = chatManager.Create(chat);
            return Results.Ok(createdChat);
        }

        private static IResult UpdateChat(Chat chat, IChatManager chatManager)
        {
            var updatedChat = chatManager.Update(chat);
            return updatedChat != null ? Results.Ok(updatedChat) : Results.NotFound();
        }

        private static IResult DeleteChat(long id, IChatManager chatManager)
        {
            var deletedChat = chatManager.Delete(id);
            return deletedChat is null ? Results.NotFound() : Results.Ok(deletedChat);
        }

        private static IResult AddMessage(long chatId, string message, IChatManager chatManager)
        {
            var newMessage = chatManager.AddMessage(chatId, message);
            return Results.Ok(newMessage);
        }

        private static IResult GetChatHistory(long chatId, IChatManager chatManager)
        {
            var chatHistory = chatManager.GetChatHistory(chatId);
            return Results.Ok(chatHistory);
        }

        private static IResult GetChatInfo(long chatId, IChatManager chatManager)
        {
            var chat = chatManager.GetChatInfo(chatId);
            return chat is null ? Results.NotFound() : Results.Ok(chat);
        }
    }
}