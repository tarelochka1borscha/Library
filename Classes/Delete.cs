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
    public class Delete
    {
        private static _43pKnowledgeBaseContext BaseConnecton = new _43pKnowledgeBaseContext();

        /// <summary>
        /// Метод для удаления записи в таблицы Answers (ответы)
        /// </summary>
        /// <param name="answer">Запись для удаления/param>
        public void DeleteAnwer(Answer answer)
        {
            BaseConnecton.Answers.Remove(answer);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Problems (проблемы)
        /// </summary>
        /// <param name="problem">Запись для удаления</param>
        public void DeleteProblem(Problem problem)
        {
            BaseConnecton.Problems.Remove(problem);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Reasons (причины проблем)
        /// </summary>
        /// <param name="reason">Запись для удаления</param>
        public void DeleteReason(Reason reason)
        {
            BaseConnecton.Reasons.Remove(reason);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Softs (программное обеспечение и его элементы)
        /// </summary>
        /// <param name="soft">Запись для удаления</param>
        public void DeleteSoft(Soft soft)
        {
            BaseConnecton.Softs.Remove(soft);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Solutions (решение)
        /// </summary>
        /// <param name="soft">Запись для удаления</param>
        public void DeleteSolution(Solution solution)
        {
            BaseConnecton.Solutions.Remove(solution);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы SolutionSteps (связь шагов и решений)
        /// </summary>
        /// <param name="solution_step">Запись для удаления</param>
        public void DeleteSolutionStep(SolutionStep solution_step)
        {
            BaseConnecton.SolutionSteps.Remove(solution_step);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записей в таблицы SolutionSteps (связь шагов и решений)
        /// </summary>
        /// <param name="solutions_steps">Список записей для удаления</param>
        public void DeleteSolutionStep(List<SolutionStep> solutions_steps)
        {
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
        public void DeleteStep(Step step)
        {
            BaseConnecton.Steps.Remove(step);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Tags (тэги)
        /// </summary>
        /// <param name="tag">Запись для удаления</param>
        public void DeleteTag(Tag tag)
        {
            BaseConnecton.Tags.Remove(tag);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы TagProblems (связь тэгов и проблем)
        /// </summary>
        /// <param name="tag_problem">Запись для удаления</param>
        public void DeleteTagProblem(TagProblem tag_problem)
        {
            BaseConnecton.TagProblems.Remove(tag_problem);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записей в таблицы TagProblems (связь тегов и проблем)
        /// </summary>
        /// <param name="tags_problems">Список записей для удаления</param>
        public void DeleteTagProblem(List<TagProblem> tags_problems)
        {
            foreach (TagProblem tp in tags_problems)
            {
                BaseConnecton.TagProblems.Remove(tp);
            }
            BaseConnecton.SaveChanges();
        }
    }
}
