using KnowledgeBaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBaseLibrary.Classes
{
    /// <summary>
    /// Класс содержит методы для удаления записей из базы данных
    /// </summary>
    public class Remove
    {
        private static _43pKnowledgeBaseContext BaseConnecton = new _43pKnowledgeBaseContext();

        /// <summary>
        /// Метод для удаления записи в таблицы Answers (ответы)
        /// </summary>
        /// <param name="answer">Запись для удаления</param>
        public static void DeleteAnwer(Answer answer)
        {
            if (answer == null) return;
            BaseConnecton.Answers.Remove(answer);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Problems (проблемы)
        /// </summary>
        /// <param name="problem">Запись для удаления</param>
        public static void DeleteProblem(Problem problem)
        {
            if (problem == null) return;
            BaseConnecton.Problems.Remove(problem);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Reasons (причины проблем)
        /// </summary>
        /// <param name="reason">Запись для удаления</param>
        public static void DeleteReason(Reason reason)
        {
            if (reason == null) return;
            BaseConnecton.Reasons.Remove(reason);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Softs (программное обеспечение и его элементы)
        /// </summary>
        /// <param name="soft">Запись для удаления</param>
        public static void DeleteSoft(Soft soft)
        {
            if (soft == null) return;
            BaseConnecton.Softs.Remove(soft);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Solutions (решение)
        /// </summary>
        /// <param name="soft">Запись для удаления</param>
        public static void DeleteSolution(Solution solution)
        {
            if (solution == null) return;
            BaseConnecton.Solutions.Remove(solution);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы SolutionSteps (связь шагов и решений)
        /// </summary>
        /// <param name="solution_step">Запись для удаления</param>
        public static void DeleteSolutionStep(SolutionStep solution_step)
        {
            if (solution_step == null) return;
            BaseConnecton.SolutionSteps.Remove(solution_step);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записей в таблицы SolutionSteps (связь шагов и решений)
        /// </summary>
        /// <param name="solutions_steps">Список записей для удаления</param>
        public static void DeleteSolutionStep(List<SolutionStep> solutions_steps)
        {
            if (solutions_steps.Count < 1) return;
            foreach (SolutionStep sp in solutions_steps)
            {
                BaseConnecton.SolutionSteps.Remove(sp);
            }
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Steps (шаги)
        /// </summary>
        /// <param name="step">Запись для удаления</param>
        public static void DeleteStep(Step step)
        {
            if (step == null) return;
            BaseConnecton.Steps.Remove(step);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Tags (тэги)
        /// </summary>
        /// <param name="tag">Запись для удаления</param>
        public static void DeleteTag(Tag tag)
        {
            if (tag == null) return;
            BaseConnecton.Tags.Remove(tag);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы TagProblems (связь тэгов и проблем)
        /// </summary>
        /// <param name="tag_problem">Запись для удаления</param>
        public static void DeleteTagProblem(TagProblem tag_problem)
        {
            if (tag_problem == null) return;
            BaseConnecton.TagProblems.Remove(tag_problem);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записей в таблицы TagProblems (связь тегов и проблем)
        /// </summary>
        /// <param name="tags_problems">Список записей для удаления</param>
        public static void DeleteTagProblem(List<TagProblem> tags_problems)
        {
            if (tags_problems.Count < 1) return;
            foreach (TagProblem tp in tags_problems)
            {
                BaseConnecton.TagProblems.Remove(tp);
            }
            BaseConnecton.SaveChanges();
        }
    }
}
