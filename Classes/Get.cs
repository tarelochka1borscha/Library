using KnowledgeBaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBaseLibrary.Classes
{
    /// <summary>
    /// Класс содержит методы для получение записей из базы данных
    /// </summary>
    public class Get
    {
        private static _43pKnowledgeBaseContext BaseConnecton = new _43pKnowledgeBaseContext();


        /// <summary>
        /// Метод для получения всех записей таблицы Answers (шаблоны ответа на проблему)
        /// </summary>
        /// <returns>Список типа Answer, содержащий все записи таблицы Answers</returns>
        public static List<Answer> GetAnswersList() => BaseConnecton.Answers.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Problems (проблемы)
        /// </summary>
        /// <returns>Список типа Problem, содержащий все записи таблицы Problems</returns>
        public static List<Problem> GetProblemsList() => BaseConnecton.Problems.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Reasons (причин возникановения проблем)
        /// </summary>
        /// <returns>Список типа Reason, содержащий все записи таблицы Reasons</returns>
        public static List<Reason> GetReasonsList() => BaseConnecton.Reasons.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Softs (программное обеспечение и его элементы)
        /// </summary>
        /// <returns>Список типа Soft, содержащий все записи таблицы Softs</returns>
        public static List<Soft> GetSoftsList() => BaseConnecton.Softs.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Solutions (решения проблем)
        /// </summary>
        /// <returns>Список типа Solution, содержащий все записи таблицы Solutions</returns>
        public static List<Solution> GetSolutionsList() => BaseConnecton.Solutions.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы SolutionSteps (связь решений и шагов)
        /// </summary>
        /// <returns>Список типа SolutionStep, содержащий все записи таблицы SolutionSteps</returns>
        public static List<SolutionStep> GetSolutionStepsList() => BaseConnecton.SolutionSteps.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Steps (шаги решения)
        /// </summary>
        /// <returns>Список типа Step, содержащий все записи таблицы Steps</returns>
        public static List<Step> GetStepsList() => BaseConnecton.Steps.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Tags (тэги типа проблемы)
        /// </summary>
        /// <returns>Список типа Tag, содержащий все записи таблицы Tags</returns>
        public static List<Tag> GetTagsList() => BaseConnecton.Tags.ToList();

        /// <summary>
        /// Метод для получения проблем по названию/описанию
        /// </summary>
        /// <param name="text">Текстовое значение для сравнения</param>
        /// <returns>Список типа Problem, содержащий записи таблицы Problem, имеющие совпадения между названием/описанием и полученным текстовым значением</returns>
        public static List<Problem> GetProblemsByTitleDescriptionList(string text) => BaseConnecton.Problems.Where(x => x.Title.ToLower().Contains(text.ToLower()) || x.Description.ToLower().Contains(text.ToLower())).ToList();

        /// <summary>
        /// Метод для получения проблем по тэгу/названию
        /// </summary>
        /// <param name="text">Текстовое значение для сравнения</param>
        /// <returns></returns>
        public static Problem GetProblemById(Guid ProblemId) => BaseConnecton.Problems.FirstOrDefault(x => x.Id == ProblemId);
         
    }
}
