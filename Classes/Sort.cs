using KnowledgeBaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBaseLibrary.Classes
{
    /// <summary>
    /// Класс содержит методы для фильтрации и сортировки списков
    /// </summary>
    public class Sort
    {
        private static _43pKnowledgeBaseContext BaseConnecton = new _43pKnowledgeBaseContext();

        /// <summary>
        /// Метод для получения проблем по названию/описанию
        /// </summary>
        /// <param name="text">Текущий список записей</param>
        /// <param name="text">Текстовое значение для сравнения</param>
        /// <returns>Список типа Problem, содержащий записи таблицы Problem, имеющие совпадения между названием/описанием и полученным текстовым значением</returns>
        public static List<Problem> GetProblemsByTitleDescriptionList(List<Problem> problems, string text) => problems.Where(x => x.Title.ToLower().Contains(text.ToLower()) || x.Description.ToLower().Contains(text.ToLower())).ToList();
    }
}
