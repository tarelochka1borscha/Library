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
        private static _43pKnowledgeBaseContext BaseConnecton = new _43pKnowledgeBaseContext();

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Answers (ответы)
        /// </summary>
        /// <param name="answer">Запись для добавления/изменения</param>
        public static void InputAnwer(Answer answer)
        {
            if (answer == null) return; //проверка на пустое значение объекта
            if ((BaseConnecton.Answers.FirstOrDefault(x => x.Answer1 == answer.Answer1) != null) && (BaseConnecton.Answers.FirstOrDefault(x => x.Id == answer.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (BaseConnecton.Answers.FirstOrDefault(x => x.Id == answer.Id) == null) BaseConnecton.Answers.Add(answer);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Problems (проблемы)
        /// </summary>
        /// <param name="problem">Запись для добавления/изменения</param>
        public static void InputProblem(Problem problem)
        {
            if (problem == null) return; //проверка на пустое значение объекта
            if ((BaseConnecton.Problems.FirstOrDefault(x => x.Title == problem.Title) != null) && (BaseConnecton.Problems.FirstOrDefault(x=>x.Id == problem.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (BaseConnecton.Problems.FirstOrDefault(x => x.Id == problem.Id) == null) BaseConnecton.Problems.Add(problem);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице проблем, если новая/измененная проблема относится к какому-либо тэгу
        /// </summary>
        /// <param name="problem">Запись для добавления/изменения</param>
        /// <param name="problem">Запись для добавления/изменения</param>
        public static void InputProblem(Problem problem, List<Tag> tags)
        {
            if ((problem == null) || (tags.Count < 1)) return; //проверка на пустое значение объекта
            if ((BaseConnecton.Problems.FirstOrDefault(x => x.Title == problem.Title) != null) && (BaseConnecton.Problems.FirstOrDefault(x => x.Id == problem.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)

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

            if (BaseConnecton.Problems.FirstOrDefault(x => x.Id == problem.Id) != null)
            {
                //удаление всех старых связей проблемы и тэгов в таблице TagProblems
                List<TagProblem> tp_delete = BaseConnecton.TagProblems.Where(x=> x.ProblemId == problem.Id).ToList();
                Remove.DeleteTagProblem(tp_delete);
            }
            else
            {
                BaseConnecton.Problems.Add(problem);
            }

            foreach (TagProblem tp in problem_tags)
            {
                BaseConnecton.Add(tp);
            }
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Reasons (причины проблем)
        /// </summary>
        /// <param name="reason">Запись для добавления/изменения</param>
        public static void InputReason(Reason reason)
        {
            if (reason == null) return; //проверка на пустое значение объекта
            if ((BaseConnecton.Reasons.FirstOrDefault(x => x.Description == reason.Description) != null) && (BaseConnecton.Reasons.FirstOrDefault(x => x.Id == reason.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (BaseConnecton.Reasons.FirstOrDefault(x => x.Id == reason.Id) == null) BaseConnecton.Reasons.Add(reason);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Softs (программное обеспечение и его элементы)
        /// </summary>
        /// <param name="soft">Запись для добавления/изменения</param>
        public static void InputSoft(Soft soft)
        {
            if (soft == null) return; //проверка на пустое значение объекта
            if ((BaseConnecton.Softs.FirstOrDefault(x => x.Title == soft.Title) != null) && (BaseConnecton.Softs.FirstOrDefault(x => x.Id == soft.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (BaseConnecton.Softs.FirstOrDefault(x => x.Id == soft.Id) == null) BaseConnecton.Softs.Add(soft);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения таблицы решений, включая основные связанные с ней таблицы (Solutions, SolutionSteps, Steps, TagProblems)
        /// </summary>
        /// <param name="solution">Объект решения</param>
        /// <param name="steps">Список шагов</param>
        public static void InputSolution(Solution solution, List<Step> steps)
        {
            //проверка на пустые значения входных данных
            if ((solution == null) || (steps.Count < 1)) return;
            
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

            if (BaseConnecton.Solutions.FirstOrDefault(x=>x.Id == solution.Id) != null)
            {
                //удаление всех связей шагов и решения solution во избежания нарушения порядка последовательности, а так же "мусора" в таблице Steps
                List<SolutionStep> sp_delete = BaseConnecton.SolutionSteps.Where(x => x.SolutionId == solution.Id).ToList();
                List<Step> steps_delete = [.. sp_delete.Select(x => x.Step)];
                Remove.DeleteSolutionStep(sp_delete);
                foreach (Step s in steps_delete)
                {
                    Remove.DeleteStep(s);
                }
            }
            else
            {
                BaseConnecton.Solutions.Add(solution);
            }

            foreach (Step step in steps) BaseConnecton.Steps.Add(step);
            foreach (SolutionStep sp in solution_steps) BaseConnecton.SolutionSteps.Add(sp);
            BaseConnecton.SaveChanges();
        }

        /// <summary>
        /// Метод для добавления/изменения записи в таблице Tags (тэги)
        /// </summary>
        /// <param name="tag">Запись для добавления/изменения</param>
        public static void InputTag(Tag tag)
        {
            if (tag == null) return; //проверка на пустое значение объекта
            if ((BaseConnecton.Tags.FirstOrDefault(x => x.Title == tag.Title) != null) && (BaseConnecton.Tags.FirstOrDefault(x => x.Id == tag.Id) == null)) return; //проверка на совпадение содержимого главного поля нового объекта с существующим объектом (защита от дубликата)
            if (BaseConnecton.Tags.FirstOrDefault(x=> x.Id == tag.Id) == null) BaseConnecton.Tags.Add(tag);
            BaseConnecton.SaveChanges();
        }
    }
}
