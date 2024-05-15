using KnowledgeBaseLibrary.Models;

namespace KnowledgeBaseLibrary.Classes
{
    /// <summary>
    /// Класс содержит методы для удаления записей из базы данных
    /// </summary>
    public class Remove
    {
        //private static _43pKnowledgeBaseContext BaseConnecton = new _43pKnowledgeBaseContext();

        /// <summary>
        /// Метод для удаления записи в таблицы Answers (ответы)
        /// </summary>
        /// <param name="answer">Запись для удаления</param>
        public static void DeleteAnwer(Answer answer)
        {
            if (answer == null) return;
            DBContext.BaseConnecton.Answers.Remove(answer);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Problems (проблемы)
        /// </summary>
        /// <param name="problem">Запись для удаления</param>
        public static void DeleteProblem(Problem problem)
        {
            if (problem == null) return;
            Problem problem_to_delete = DBContext.BaseConnecton.Problems.FirstOrDefault(x=>x.Id == problem.Id);
            problem_to_delete.ProblemStatus = Get.GetForDeletionStatus().Id;
            DBContext.BaseConnecton.Deleteds.Add(new Deleted { ProblemId = problem_to_delete.Id, DateOfDeletion = DateOnly.FromDateTime(DateTime.Now) });
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Problems (проблемы)
        /// </summary>
        /// <param name="problem">Запись для удаления</param>
        public static void DeleteProblemRightAway(Problem problem)
        {
            if (problem == null) return;
            Deleted deleted = DBContext.BaseConnecton.Deleteds.FirstOrDefault(x => x.ProblemId == problem.Id);
            if (deleted != null)
            {
                DBContext.BaseConnecton.Deleteds.Remove(deleted);
                DBContext.BaseConnecton.SaveChanges();
            }
            DBContext.BaseConnecton.Problems.Remove(problem);
            DeleteSolutionsForProblem(problem);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Reasons (причины проблем)
        /// </summary>
        /// <param name="reason">Запись для удаления</param>
        public static void DeleteReason(Reason reason)
        {
            if (reason == null) return;
            DBContext.BaseConnecton.Reasons.Remove(reason);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Softs (программное обеспечение и его элементы)
        /// </summary>
        /// <param name="soft">Запись для удаления</param>
        public static void DeleteSoft(Soft soft)
        {
            if (soft == null) return;
            DBContext.BaseConnecton.Softs.Remove(soft);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Solutions (решение)
        /// </summary>
        /// <param name="solution">Запись для удаления</param>
        public static void DeleteSolution(Solution solution)
        {
            if (solution == null) return;
            DBContext.BaseConnecton.Solutions.Remove(solution);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления всех решений из таблицы Solutions для проблемы problem
        /// </summary>
        /// <param name="problem">Проблема, решения которой необходимо удалить</param>
        public static void DeleteSolutionsForProblem(Problem problem)
        {
            if (problem == null) return;
            List<Solution> solutions = DBContext.BaseConnecton.Solutions.Where(x => x.ProblemId == problem.Id).ToList();
            if (solutions.Count < 1) return;
            foreach (Solution s in solutions)
            {
                Solution solution = s;
                List<SolutionStep> solutionSteps = DBContext.BaseConnecton.SolutionSteps.Where(x => x.SolutionId == solution.Id).ToList();
                List<Step> steps = new();
                foreach (SolutionStep ss in solutionSteps)
                {
                    Step step = DBContext.BaseConnecton.Steps.FirstOrDefault(x=>x.Id == ss.StepId);
                    steps.Add(step);
                }
                DeleteSteps(steps);
                DBContext.BaseConnecton.Solutions.Remove(solution);
            }
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы SolutionSteps (связь шагов и решений)
        /// </summary>
        /// <param name="solution_step">Запись для удаления</param>
        public static void DeleteSolutionStep(SolutionStep solution_step)
        {
            if (solution_step == null) return;
            DBContext.BaseConnecton.SolutionSteps.Remove(solution_step);
            DBContext.BaseConnecton.SaveChanges();
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
                SolutionStep solutionStep = sp as SolutionStep;
                DBContext.BaseConnecton.SolutionSteps.Remove(solutionStep);
            }
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Steps (шаги)
        /// </summary>
        /// <param name="step">Запись для удаления</param>
        public static void DeleteStep(Step step)
        {
            if (step == null) return;
            DBContext.BaseConnecton.Steps.Remove(step);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записей в таблице Steps (шаги)
        /// </summary>
        /// <param name="steps">Список шагов для удаления</param>
        public static void DeleteSteps(List<Step> steps)
        {
            if (steps.Count < 1) return;
            foreach (Step step in steps) DBContext.BaseConnecton.Steps.Remove(step);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы Tags (тэги)
        /// </summary>
        /// <param name="tag">Запись для удаления</param>
        public static void DeleteTag(Tag tag)
        {
            if (tag == null) return;
            DBContext.BaseConnecton.Tags.Remove(tag);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления записи в таблицы TagProblems (связь тэгов и проблем)
        /// </summary>
        /// <param name="tag_problem">Запись для удаления</param>
        public static void DeleteTagProblem(TagProblem tag_problem)
        {
            if (tag_problem == null) return;
            DBContext.BaseConnecton.TagProblems.Remove(tag_problem);
            DBContext.BaseConnecton.SaveChanges();
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
                TagProblem tag = tp as TagProblem;
                DBContext.BaseConnecton.TagProblems.Remove(tag);
            }
            DBContext.BaseConnecton.SaveChanges();
        }
    }
}
