﻿using KnowledgeBaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// <summary>
        /// Метод для получения всех записей таблицы Answers (шаблоны ответа на проблему)
        /// </summary>
        /// <returns>Список типа Answer, содержащий все записи таблицы Answers</returns>
        public static List<Answer> GetAnswersList() => DBContext.BaseConnecton.Answers.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Problems (проблемы)
        /// </summary>
        /// <returns>Список типа Problem, содержащий все записи таблицы Problems</returns>
        public static List<Problem> GetProblemsList() => DBContext.BaseConnecton.Problems.ToList();

        /// <summary>
        /// Метод для получения актуальных записей таблицы Problems (проблемы)
        /// </summary>
        /// <returns>Список типа Problem, содержащий актуальные записи таблицы Problems</returns>
        public static List<Problem> GetActualProblemsList()
        {
            Guid id = GetActualStatus().Id;
            return DBContext.BaseConnecton.Problems.Where(x=>x.ProblemStatus == id).ToList();
        }

        /// <summary>
        /// Метод для получения записи статуса "Актуален"
        /// </summary>
        /// <returns>Объект типа Status - запись статуса "Актуален"</returns>
        public static Status GetActualStatus() => DBContext.BaseConnecton.Statuses.FirstOrDefault(x => x.Title == "Активен");

        /// <summary>
        /// Метод для получения записи статуса "На удалении"
        /// </summary>
        /// <returns>Объект типа Status - запись статуса "На удалении"</returns>
        public static Status GetForDeletionStatus() => DBContext.BaseConnecton.Statuses.FirstOrDefault(x => x.Title == "На удалении");

        /// <summary>
        /// Метод для получения записей таблицы Problems (проблемы), которые находятся на удалении
        /// </summary>
        /// <returns>Список типа Problem, содержащий записи таблицы Problems "на удалении"</returns>
        public static List<Problem> GetDeletedProblemsList()
        {
            Guid id = GetForDeletionStatus().Id;
            return DBContext.BaseConnecton.Problems.Where(x => x.ProblemStatus == id).ToList();
        }

        /// <summary>
        /// Метод для получения всех записей таблицы Reasons (причин возникановения проблем)
        /// </summary>
        /// <returns>Список типа Reason, содержащий все записи таблицы Reasons</returns>
        public static List<Reason> GetReasonsList() => DBContext.BaseConnecton.Reasons.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Softs (программное обеспечение и его элементы)
        /// </summary>
        /// <returns>Список типа Soft, содержащий все записи таблицы Softs</returns>
        public static List<Soft> GetSoftsList() => DBContext.BaseConnecton.Softs.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы Solutions (решения проблем)
        /// </summary>
        /// <returns>Список типа Solution, содержащий все записи таблицы Solutions</returns>
        public static List<Solution> GetSolutionsList() => DBContext.BaseConnecton.Solutions.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы SolutionSteps (связь решений и шагов)
        /// </summary>
        /// <returns>Список типа SolutionStep, содержащий все записи таблицы SolutionSteps</returns>
        public static List<SolutionStep> GetSolutionStepsList() => DBContext.BaseConnecton.SolutionSteps.ToList();

        /// <summary>
        /// Метод для получения записей таблицы SolutionSteps (связь решений и шагов) для определенного решения
        /// </summary>
        /// <param name="solution">Решение, для которого необходимо получить записи</param>
        /// <returns>Список типа SolutionStep, содержащий записи таблицы SolutionSteps для определенного решения</returns>
        public static List<SolutionStep> GetSolutionStepsList(Solution solution) => DBContext.BaseConnecton.SolutionSteps.Where(x=>x.SolutionId == solution.Id).ToList();

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
                Step a = (Step)DBContext.BaseConnecton.Steps.FirstOrDefault(x => x.Id == step.StepId);
                if (a != null) steps.Add(a);
            }
            steps = steps.OrderBy(x=>x.Number).ToList();
            return steps;
        }

        /// <summary>
        /// Метод для получения даты удаления проблемы
        /// </summary>
        /// <param name="problem">Проблема, дату удаления которой необходимо получить</param>
        /// <returns>Дата удаления проблемы типа DateOnly</returns>
        public static DateOnly GetDateOfDeletionByProblem(Problem problem)
        {
            if (problem == null) return DateOnly.MinValue;
            Deleted deleted_problem = DBContext.BaseConnecton.Deleteds.FirstOrDefault(x=>x.ProblemId==problem.Id);
            if (deleted_problem == null) return DateOnly.MinValue;
            return deleted_problem.DateOfDeletion;
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
                    string soft = DBContext.BaseConnecton.Softs.FirstOrDefault(x => x.Id == step.SoftId).Title;
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
        public static List<Tag> GetTagsList() => DBContext.BaseConnecton.Tags.ToList();

        /// <summary>
        /// Метод для получения всех записей таблицы TagProblems (взаимосвязь тэгов и проблем)
        /// </summary>
        /// <returns>Список типа TagProblem, содержащий все записи таблицы TagProblems</returns>
        public static List<TagProblem> GetTagProblemsList() => DBContext.BaseConnecton.TagProblems.ToList();

        /// <summary>
        /// Метод для получения проблемы по ее идентификатору
        /// </summary>
        /// <param name="ProblemId">Идентификатор (Id) записи</param>
        /// <returns>Объект Problem - запись таблицы Problems, найденная по полученному Id</returns>
        public static Problem GetProblemById(Guid ProblemId) => DBContext.BaseConnecton.Problems.FirstOrDefault(x => x.Id == ProblemId);

        /// <summary>
        /// Метод для получения списка тэгов для определенной проблемы
        /// </summary>
        /// <param name="problem">Решение, чей список тэгов необходимо получить</param>
        /// <returns>Список типа Tag, содержащий тэги проблемы problem</returns>
        public static List<Tag> GetTagsByProblem(Problem problem)
        {
            List<TagProblem> tg = DBContext.BaseConnecton.TagProblems.Where(x=>x.ProblemId == problem.Id).ToList();
            List<Tag> tags = new List<Tag>();
            foreach (TagProblem tp in tg)
            {
                Tag tag = DBContext.BaseConnecton.Tags.FirstOrDefault(x=>x.Id == tp.TagId);
                tags.Add(tag);
            }
            return tags;
        }

        /// <summary>
        /// Метод для получения шаблона ответа для решения
        /// </summary>
        /// <param name="solution">Решение, для которого необходимо получить шаблон ответа</param>
        /// <returns>Шаблон ответа для решения solution</returns>
        public static Answer GetAnswerBySolution(Solution solution) => DBContext.BaseConnecton.Answers.FirstOrDefault(x=>x.Id == solution.AnswerId);

        /// <summary>
        /// Метод для получения решений для проблемы
        /// </summary>
        /// <param name="problem">Проблема, для которой необходимо получить решения</param>
        /// <returns>Список типа Solution, содержащий решения для проблемы problem</returns>
        public static List<Solution> GetSolutionsByProblem(Problem problem) => DBContext.BaseConnecton.Solutions.Where(x=>x.ProblemId == problem.Id).ToList();
    }
}
