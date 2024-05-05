using KnowledgeBaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBaseLibrary.Classes
{
    /// <summary>
    /// Класс содержит методы для проверки записей и их добавления/изменения в базу данных
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Метод для добавления/изменения записи в таблице Answers (ответы)
        /// </summary>
        /// <param name="answer">Запись для добавления/изменения</param>
        public static void InputAnwer(Answer answer)
        {
            if (answer == null) return; //проверка на пустое значение объекта
            if ((DBContext.BaseConnecton.Answers.FirstOrDefault(x => x.Answer1 == answer.Answer1) != null) && (DBContext.BaseConnecton.Answers.FirstOrDefault(x => x.Id == answer.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (DBContext.BaseConnecton.Answers.FirstOrDefault(x => x.Id == answer.Id) == null) DBContext.BaseConnecton.Answers.Add(answer);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Problems (проблемы)
        /// </summary>
        /// <param name="problem">Запись для добавления/изменения</param>
        public static void InputProblem(Problem problem)
        {
            if (problem == null) return; //проверка на пустое значение объекта
            if ((DBContext.BaseConnecton.Problems.FirstOrDefault(x => x.Title == problem.Title) != null) && (DBContext.BaseConnecton.Problems.FirstOrDefault(x=>x.Id == problem.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (DBContext.BaseConnecton.Problems.FirstOrDefault(x => x.Id == problem.Id) == null) DBContext.BaseConnecton.Problems.Add(problem);
            else
            {
                Problem problem1 = DBContext.BaseConnecton.Problems.FirstOrDefault(x => x.Id == problem.Id);
                problem1.Title = problem.Title;
                problem1.Description = problem.Description;
            }
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице проблем, если новая/измененная проблема относится к какому-либо тэгу
        /// </summary>
        /// <param name="problem">Запись для добавления/изменения</param>
        /// <param name="problem">Запись для добавления/изменения</param>
        public static void InputProblem(Problem problem, List<Tag> tags)
        {
            if ((problem == null) || (tags.Count < 1)) return; //проверка на пустое значение объекта

            if (DBContext.BaseConnecton.Problems.FirstOrDefault(x => x.Id == problem.Id) != null)
            {
                Problem problem1 = DBContext.BaseConnecton.Problems.FirstOrDefault(x => x.Id == problem.Id);
                problem1.Title = problem.Title;
                problem1.Description = problem.Description;
                DBContext.BaseConnecton.SaveChanges();
                //удаление всех старых связей проблемы и тэгов в таблице TagProblems
                List<TagProblem> tp_delete = DBContext.BaseConnecton.TagProblems.Where(x => x.ProblemId == problem.Id).ToList();
                Remove.DeleteTagProblem(tp_delete);
            }
            else if (DBContext.BaseConnecton.Problems.FirstOrDefault(x => x.Title == problem.Title) != null) return; //избежание дубликата
            else
            {
                DBContext.BaseConnecton.Problems.Add(problem);
                DBContext.BaseConnecton.SaveChanges();
            }

            //создание списка связей проблемы и тэгов
            List<TagProblem> problem_tags = new List<TagProblem>();
            foreach (Tag tag in tags)
            {
                TagProblem tp = new TagProblem
                {
                    TagId = tag.Id,
                    ProblemId = problem.Id,
                };
                problem_tags.Add(tp);
            }

            foreach (TagProblem tp in problem_tags)
            {
                TagProblem tagProblem = tp as TagProblem;
                DBContext.BaseConnecton.TagProblems.Add(tagProblem);
                DBContext.BaseConnecton.SaveChanges();
            }
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Reasons (причины проблем)
        /// </summary>
        /// <param name="reason">Запись для добавления/изменения</param>
        public static void InputReason(Reason reason)
        {
            if (reason == null) return; //проверка на пустое значение объекта
            if ((DBContext.BaseConnecton.Reasons.FirstOrDefault(x => x.Description == reason.Description) != null) && (DBContext.BaseConnecton.Reasons.FirstOrDefault(x => x.Id == reason.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (DBContext.BaseConnecton.Reasons.FirstOrDefault(x => x.Id == reason.Id) == null) DBContext.BaseConnecton.Reasons.Add(reason);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Softs (программное обеспечение и его элементы)
        /// </summary>
        /// <param name="soft">Запись для добавления/изменения</param>
        public static void InputSoft(Soft soft)
        {
            if (soft == null) return; //проверка на пустое значение объекта
            if ((DBContext.BaseConnecton.Softs.FirstOrDefault(x => x.Title == soft.Title) != null) && (DBContext.BaseConnecton.Softs.FirstOrDefault(x => x.Id == soft.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (DBContext.BaseConnecton.Softs.FirstOrDefault(x => x.Id == soft.Id) == null) DBContext.BaseConnecton.Softs.Add(soft);
            DBContext.BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления решений, включая основные связанные с ней таблицы (Solutions, SolutionSteps, Steps, TagProblems)
        /// </summary>
        /// <param name="solution">Объект решения</param>
        /// <param name="steps">Список шагов</param>
        public static void InputSolution(Solution solution, List<Step> steps)
        {
            //проверка на пустые значения входных данных
            if ((solution == null) || (steps.Count < 1)) return;

            DBContext.BaseConnecton.Solutions.Add(solution);
            DBContext.BaseConnecton.SaveChanges();

            foreach (Step step in steps) DBContext.BaseConnecton.Steps.Add(step);
            DBContext.BaseConnecton.SaveChanges();

            //создание списка связи решения и шагов
            List<SolutionStep> solution_steps = new List<SolutionStep>();
            foreach (Step step in steps)
            {
                SolutionStep sp = new SolutionStep
                {
                    SolutionId = solution.Id,
                    StepId = step.Id,
                };
                solution_steps.Add(sp);
            }

            foreach (SolutionStep sp in solution_steps)
            {
                DBContext.BaseConnecton.SolutionSteps.Add(sp);
                DBContext.BaseConnecton.SaveChanges();
            }
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Tags (тэги)
        /// </summary>
        /// <param name="tag">Запись для добавления/изменения</param>
        public static void InputTag(Tag tag)
        {
            if (tag == null) return; //проверка на пустое значение объекта
            if ((DBContext.BaseConnecton.Tags.FirstOrDefault(x => x.Title == tag.Title) != null) && (DBContext.BaseConnecton.Tags.FirstOrDefault(x => x.Id == tag.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (DBContext.BaseConnecton.Tags.FirstOrDefault(x=> x.Id == tag.Id) == null) DBContext.BaseConnecton.Tags.Add(tag);
            DBContext.BaseConnecton.SaveChanges();
        }
    }
}
