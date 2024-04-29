using KnowledgeBaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// Метод для получения записей таблицы SolutionSteps (связь решений и шагов) для определенного решения
        /// </summary>
        /// <param name="solution">Решение, для которого необходимо получить записи</param>
        /// <returns>Список типа SolutionStep, содержащий записи таблицы SolutionSteps для определенного решения</returns>
        public static List<SolutionStep> GetSolutionStepsList(Solution solution) => BaseConnecton.SolutionSteps.Where(x=>x.SolutionId == solution.Id).ToList();

        /// <summary>
        /// Метод для получения записей таблицы Steps (шаги решения) для определенного решения
        /// </summary>
        /// <param name="solution">Решение, для которого необходимо получить записи</param>
        /// <returns>Список типа Step, содержащий записи таблицы Steps для определенного решения</returns>
        public static List<Step> GetStepsList(Solution solution)
        {
            List<Step> steps = new List<Step>();
            List<SolutionStep> sp = GetSolutionStepsList(solution);
            foreach (SolutionStep step in sp)
            {
                Step a = (Step)BaseConnecton.Steps.FirstOrDefault(x => x.Id == step.StepId);
                if (a != null) steps.Add(a);
            }
            steps = steps.OrderBy(x=>x.Number).ToList();
            return steps;
        }

        /// <summary>
        /// Метод для получения записей таблицы Steps (шаги решения), представленных в виде строки для определенного решения
        /// </summary>
        /// <param name="solution">Решение, для которого необходимо получить записи</param>
        /// <returns>Список типа string, содержащий записи таблицы Step в виде строки для определенного решения</returns>
        public static List<string> GetStepsStringList(Solution solution)
        {
            List<Step> default_list = GetStepsList(solution);
            List<string> string_list = new List<string>();
            foreach (Step step in default_list)
            {
                if (step.SoftId.ToString().Length > 0)
                {
                    string soft = BaseConnecton.Softs.FirstOrDefault(x => x.Id == step.SoftId).Title;
                    string_list.Add(step.Action + " " + soft);
                }
                else
                {
                    string_list.Add(step.Action);
                }
            }
            return string_list;
        }

        /// <summary>
        /// Метод для получения всех записей таблицы Tags (тэги типа проблемы)
        /// </summary>
        /// <returns>Список типа Tag, содержащий все записи таблицы Tags</returns>
        public static List<Tag> GetTagsList() => BaseConnecton.Tags.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы TagProblems (взаимосвязь тэгов и проблем)
        /// </summary>
        /// <returns>Список типа TagProblem, содержащий все записи таблицы TagProblems</returns>
        public static List<TagProblem> GetTagProblemsList() => BaseConnecton.TagProblems.ToList();

        /// <summary>
        /// Метод для получения проблемы по ее идентификатору
        /// </summary>
        /// <param name="ProblemId">Идентификатор (Id) записи</param>
        /// <returns>Объект Problem - запись таблицы Problems, найденная по полученному Id</returns>
        public static Problem GetProblemById(Guid ProblemId) => BaseConnecton.Problems.FirstOrDefault(x => x.Id == ProblemId);

        /// <summary>
        /// Метод для получения списка тэгов для определенной проблемы
        /// </summary>
        /// <param name="problem">Решение, чей список тэгов необходимо получить</param>
        /// <returns>Список типа Tag, содержащий тэги проблемы problem</returns>
        public static List<Tag> GetTagsByProblem(Problem problem)
        {
            List<TagProblem> tg = BaseConnecton.TagProblems.Where(x=>x.ProblemId == problem.Id).ToList();
            List<Tag> tags = new List<Tag>();
            foreach (TagProblem tp in tg)
            {
                Tag tag = BaseConnecton.Tags.FirstOrDefault(x=>x.Id == tp.TagId);
                tags.Add(tag);
            }
            return tags;
        }

        /// <summary>
        /// Метод для получения шаблона ответа для решения
        /// </summary>
        /// <param name="solution">Решение, для которого необходимо получить шаблон ответа</param>
        /// <returns>Шаблон ответа для решения solution</returns>
        public static Answer GetAnswerBySolution(Solution solution) => BaseConnecton.Answers.FirstOrDefault(x=>x.Id == solution.AnswerId);
    }
}
