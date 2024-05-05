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

        /// <summary>
        /// Метод для получения проблем по названию/описанию
        /// </summary>
        /// <param name="problems">Текущий список записей</param>
        /// <param name="text">Текстовое значение для сравнения</param>
        /// <returns>Список типа Problem, содержащий записи таблицы Problem, имеющие совпадения между названием/описанием и полученным текстовым значением</returns>
        public static List<Problem> SearchProblemsByTitleDescriptionList(List<Problem> problems, string text) => problems.Where(x => x.Title.ToLower().Contains(text.ToLower()) || x.Description.ToLower().Contains(text.ToLower())).ToList();

        /// <summary>
        /// Метод сортировки по возрастанию наименивания
        /// </summary>
        /// <param name="problems">Текущий список записей</param>
        /// <returns>Отсортированный по возрастанию наименования список типа Problem</returns>
        public static List<Problem> SortProblemsByAscendingTitle(List<Problem> problems) => problems.OrderBy(x => x.Title).ToList();

        /// <summary>
        /// Метод сортировки по убыванию наименивания
        /// </summary>
        /// <param name="problems">Текущий список записей</param>
        /// <returns>Отсортированный по убыванию наименования список типа Problem</returns>
        public static List<Problem> SortProblemsByDescendingTitle(List<Problem> problems) => problems.OrderBy(x => x.Title).Reverse().ToList();

        /// <summary>
        /// Метод сортировки по новизне (от позднему к новому)
        /// </summary>
        /// <param name="problems">Текущий список записей</param>
        /// <returns>Отсортированный по новизне список типа Problem</returns>
        public static List<Problem> SortProblemsByAscendingDate(List<Problem> problems) => problems;

        /// <summary>
        /// Метод сортировки по новизне (от нового к позднему)
        /// </summary>
        /// <param name="problems">Текущий список записей</param>
        /// <returns>Отсортированный по новизне список типа Problem</returns>
        public static List<Problem> SortProblemsByDescendingDate(List<Problem> problems)
        {
            problems.Reverse();
            return problems;
        }

        /// <summary>
        /// Метод фильтрации по тэгу
        /// </summary>
        /// <param name="problems">Текущий список записей</param>
        /// <param name="tag">Тэг для фильтрации</param>
        /// <returns>Список типа Problem с записями, отфлитрованными по тэгу</returns>
        public static List<Problem> FilterProblemsByTag(List<Problem> problems, Tag tag)
        {
            List<TagProblem> tp_list = Get.GetTagProblemsList();
            List<Problem> filtered_list = new List<Problem>();
            foreach (TagProblem tp in tp_list)
            {
                if ((tp.Tag == tag) && (problems.Contains(tp.Problem)))
                {
                    filtered_list.Add(tp.Problem);
                }
            }
            return filtered_list;
        }
    }
}
