using ChatService.Domain;
using UserService.Domain;

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
            chatGroup.MapGet(pattern: "/{id:long}", handler: GetChatByChatId);
            chatGroup.MapPost(pattern: "/", handler: CreateChat);
            chatGroup.MapPut(pattern: "/", handler: UpdateChat);
            chatGroup.MapDelete(pattern: "/{id:long}", handler: DeleteChat);

            return application;
        }

        private static IResult GetAllChats(IChatManager chatManager)
        {
            var chats = chatManager.GetAll();
            return Results.Ok(chats);
        }

        private static IResult GetChatByChatId(long id, IChatManager chatManager)
        {
            var chat = chatManager.GetById(id);
            return chat is null ? Results.NotFound() : Results.Ok(chat);
        }

        private static IResult CreateChat(Chat chat, IChatManager chatManager)
        {
            chat.Messages = null;
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

        

        
    }
}