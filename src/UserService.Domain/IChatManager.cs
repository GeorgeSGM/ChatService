using ChatService.Domain;
using System.Collections.Generic;

namespace UserService.Domain
{
    /// <summary>
    ///     Интерфейс взаимодействия с чатами
    /// </summary>
    public interface IChatManager
    {
        /// <summary>
        ///     Вернуть список всех чатов
        /// </summary>
        /// <returns>Список всех чатов</returns>
        List<Chat> GetAll();

        /// <summary>
        ///     Получить чат по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор чата</param>
        /// <returns>Данные искомого чата</returns>
        Chat? GetById(long id);

        /// <summary>
        ///     Создать новый чат
        /// </summary>
        /// <param name="chat">Данные нового чата</param>
        /// <returns>Данные созданного чата</returns>
        Chat Create(Chat chat);

        /// <summary>
        ///     Обновить данные чата
        /// </summary>
        /// <param name="chat">Данные обновляемого чата</param>
        /// <returns>Данные обновленного чата</returns>
        Chat? Update(Chat chat);

        /// <summary>
        ///     Удалить чат по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор чата</param>
        /// <returns>Данные удаленного чата</returns>
        Chat? Delete(long id);

    }
}