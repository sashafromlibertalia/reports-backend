using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Reports.DAL.Context;
using Reports.DAL.DTO.Body;
using Reports.DAL.DTO.Query;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
using TaskStatus = Reports.DAL.Types.TaskStatus;

namespace Reports.DAL.Repository.Tasks
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ReportsContext _context;
        public TasksRepository(ReportsContext context) {
            _context = context;
        }

        public async Task<TaskEntity> Create(AddTask addTask)
        {
            var task = new TaskEntity(addTask);
            task.Comments.Add( new CommentEntity(task.Id, task.EmployeeId, "Задача была создана"));
            task.Comments.Sort((x, y) => DateTime.Compare(DateTime.Parse(x.CreatedAt), DateTime.Parse(y.CreatedAt)));
            EmployeeEntity assignedEmployee = await _context.Employees.SingleOrDefaultAsync(item => item.Id == addTask.EmployeeId);
            if (assignedEmployee == null)
                throw new NullReferenceException("Employee is null.");

            assignedEmployee.Tasks.Add(task);
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity> GetById(Guid id)
        {
            TaskEntity task = await _context.Tasks.Include(item => item.Comments)
                .FirstOrDefaultAsync(item => item.Id == id);
            if (task == null)
                throw new NullReferenceException("Task is null.");

            return task;
        }

        public async Task<List<TaskEntity>> GetAll()
        {
           return await _context.Tasks.Include(item => item.Comments).ToListAsync();
        }

        public async Task<TaskEntity> Delete(Guid id)
        {
            TaskEntity task = await _context.Tasks.Include(item => item.Comments).SingleOrDefaultAsync(item => item.Id == id);
            List<CommentEntity> commentsToDelete = task.Comments;
            foreach(CommentEntity comment in commentsToDelete)
            {
                _context.Comments.Remove(comment);
            }

            if (task == null)
                throw new ReportsException("Task is null.");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<TaskEntity> GetByCreationDate(string creationDate)
        {
            TaskEntity task = await _context.Tasks.Include(item => item.Comments)
                .SingleOrDefaultAsync(item => item.CreatedAt == creationDate);
            if (task == null)
                throw new ReportsException("Task not found.");

            return task;
        }

        public async Task<TaskEntity> GetByEditingDate(string editedDate)
        {
            TaskEntity task = await _context.Tasks.Include(item => item.Comments)
                .SingleOrDefaultAsync(item => item.CreatedAt == editedDate);
            if (task == null)
                throw new ReportsException("Task not found.");

            return task;
        }

        public async Task<List<TaskEntity>> GetEditedBy(Guid employee)
        {
            List<TaskEntity> tasks = await _context.Tasks.Include(item => item.Comments)
                .Where(item => item.Comments.Any(comment => comment.Author == employee)).ToListAsync();

            return tasks;
        }

        public async Task<List<TaskEntity>> GetAssignedBy(Guid employee)
        {
            var tasks = new List<TaskEntity>();
            List<EmployeeEntity> staff = await _context.Employees.Include(item => item.Staff)
                .Include(item => item.Tasks)
                .Where(item => item.Boss == employee).ToListAsync();

            foreach (EmployeeEntity user in staff)
            {
                tasks.AddRange(user.Tasks);
            }

            return tasks;
        }

        public async Task<TaskEntity> Update(EditTask editTask, Guid id)
        {
            TaskEntity task = await _context.Tasks
                .Include(item => item.Comments)
                .SingleOrDefaultAsync(item => item.Id == id);

            if (task == null)
                throw new ReportsException("Task not found.");

            if (!string.IsNullOrWhiteSpace(editTask.Name))
            {
                task.Name = editTask.Name;
                task.Comments.Add( new CommentEntity(id, task.EmployeeId, $"Задача сменила исполнителя на '{editTask.Name}'"));
            }


            if (!string.IsNullOrWhiteSpace(editTask.Description))
            {
                task.Description = editTask.Description;
                task.Comments.Add( new CommentEntity(id, task.EmployeeId, $"Задача сменила описание на '{editTask.Description}'"));
            }


            if (await _context.Employees.AnyAsync(item => item.Id == editTask.NewEmployee))
            {
                task.EmployeeId = editTask.NewEmployee;
                task.Comments.Add( new CommentEntity(id, task.EmployeeId, $"Задача сменила исполнителя на '{editTask.NewEmployee}'"));
            }

            if (!string.IsNullOrWhiteSpace(editTask.Status))
            {
                switch (editTask.Status)
                {
                    case TaskStatus.Waiting:
                        task.Status = TaskStatus.Waiting;
                        break;
                    case TaskStatus.InProgress:
                        task.Status = TaskStatus.InProgress;
                        break;
                    case TaskStatus.Done:
                        task.Status = TaskStatus.Done;
                        break;
                }
                task.Comments.Add( new CommentEntity(id, task.EmployeeId, $"Задача сменила статус на {editTask.Status}"));
            }
            task.EditedAt = DateTime.UtcNow.ToString("o");
            task.Comments.Sort((x, y) => DateTime.Compare(DateTime.Parse(x.CreatedAt), DateTime.Parse(y.CreatedAt)));
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskEntity>> GetAllByEmployeeId(Guid employeeId)
        {
            return await _context.Tasks.Where(item => item.EmployeeId == employeeId)
                .Include(item => item.Comments)
                .ToListAsync();
        }

        public async Task<List<TaskEntity>> GetAllForSprint(Guid sprintId)
        {
            return await _context.Tasks.Where(item => item.Sprint == sprintId)
                .Include(item => item.Comments)
                .ToListAsync();
        }
    }
}