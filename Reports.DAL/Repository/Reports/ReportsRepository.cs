using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Reports.DAL.Context;
using Reports.DAL.DTO.Body;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
using Reports.DAL.Types;

namespace Reports.DAL.Repository.Reports
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly ReportsContext _context;
        public ReportsRepository(ReportsContext context) {
            _context = context;
        }

        public async Task<ReportEntity> Create(AddReport addReport)
        {
            var report = new ReportEntity(addReport);

            EmployeeEntity author = await _context.Employees.SingleOrDefaultAsync(item => item.Id == addReport.Author);
            EmployeeEntity boss = await _context.Employees.SingleOrDefaultAsync(item => item.Staff.Any(staffMember => staffMember.Id == author.Id));

            if (author == null)
                throw new ReportsException("Invalid author's credentials.");

            if (boss == null)
            {
                report.RefersToAuthor = Guid.Empty;
                report.StaffReports.AddRange(await _context.Reports.Where(item => item.RefersToAuthor == author.Id).ToListAsync());
            }
            else
            {
                report.RefersToAuthor = boss.Id;
                report.StaffReports.AddRange(await _context.Reports.Where(item => item.RefersToAuthor == author.Id).ToListAsync());
            }
            author.HasReport = true;

            foreach (TaskEntity task in addReport.Tasks.ToList())
            {
                task.Report = report.Id;
                _context.Entry(task).State = EntityState.Modified;
                report.Tasks.Add(task);
            }

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public async Task<ReportEntity> GetById(Guid reportId)
        {
            ReportEntity report = await _context.Reports
                .Include(item => item.Tasks)
                .Include(item => item.StaffReports)
                .SingleOrDefaultAsync(item => item.Id == reportId);

            if (report == null)
                throw new ReportsException("Report not found.");

            return report;
        }

        public async Task<List<ReportEntity>> GetAll()
        {
            return await _context.Reports
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments)
                .Include(item => item.StaffReports)
                .ToListAsync();
        }

        public async Task<List<ReportEntity>> ApproveAll(Guid leadId)
        {
            SprintEntity sprint = await _context.Sprints.SingleOrDefaultAsync(item => item.IsActive);
            if (sprint == null)
                throw new ArgumentNullException(string.Empty, "Sprint not found.");

            EmployeeEntity lead = await _context.Employees.SingleOrDefaultAsync(item => item.Id == leadId);
            if (lead == null || lead.Role != Roles.Lead)
                throw new ReportsException("Invalid lead's credentials.");

            List<ReportEntity> reports = await _context.Reports
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments)
                .Include(item => item.StaffReports)
                .Where(item => item.Sprint == sprint.Id)
                .ToListAsync();

            foreach (ReportEntity report in reports)
                report.Status = ReportStatus.Approved;

            sprint.ApprovedReports = reports;
            await _context.SaveChangesAsync();

            return reports;
        }

        public async Task<List<ReportEntity>> GetSprintReports()
        {
            SprintEntity sprint = await _context.Sprints.SingleOrDefaultAsync(item => item.IsActive);
            if (sprint == null)
                throw new ArgumentNullException(string.Empty, "Sprint not found.");

            return await _context.Reports
                .Include(item => item.Tasks)
                .Include(item => item.StaffReports)
                .Where(item => item.Sprint == sprint.Id)
                .ToListAsync();
        }

        public async Task<ReportEntity> Delete(Guid id)
        {
            ReportEntity report = await _context.Reports
                .Include(item => item.Tasks)
                .SingleOrDefaultAsync(item => item.Id == id);

            if (report == null)
                throw new ReportsException("Report not found.");

            EmployeeEntity employee = await _context.Employees.SingleOrDefaultAsync(item => item.Id == report.Author);
            employee.HasReport = false;

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public async Task<ReportEntity> Update(AddReport addReport, Guid id)
        {
            ReportEntity report = await _context.Reports.AsNoTracking()
                .Include(item => item.Tasks)
                .ThenInclude(item => item.Comments)
                .SingleOrDefaultAsync(item => item.Id == id);

            if (report == null)
                throw new ReportsException("Report not found.");

            if (report.Status == ReportStatus.Approved)
                throw new ReportsException("Can't edit approved report.");

            if (addReport.Author == Guid.Empty)
                throw new ArgumentNullException(string.Empty, "Author is null.");

            if (addReport.Status == ReportStatus.Approved && addReport.Author != _context.Employees.SingleOrDefault(item => item.Role == Roles.Lead)!.Id)
                throw new ReportsException("Invalid credentials.");

            if (addReport.Status != ReportStatus.Approved && addReport.Status != ReportStatus.Draft &&
                addReport.Status != ReportStatus.Submitted)
                throw new ReportsException("Invalid report status.");

            EmployeeEntity author = await _context.Employees.SingleOrDefaultAsync(item => item.Id == addReport.Author);
            if (author == null)
                throw new ReportsException("Invalid author's credentials.");

            author.HasReport = true;
            report.Author = addReport.Author;
            report.Description = addReport.Description;
            report.Status = addReport.Status;
            report.Tasks = addReport.Tasks;
            report.EditedAt = DateTime.UtcNow.ToString("o");
            _context.Entry(report).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return report;
        }
    }
}