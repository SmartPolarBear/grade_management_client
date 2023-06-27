using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using Student = GradeManagement.Data.Model.Student;

namespace GradeManagement.Service.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;
using GradeComposition = Data.Model.GradeComposition;

public class CourseGradingService(Teacher teacher, Course course)
{
    private readonly Teacher _teacher = teacher;
    private readonly Course _course = course;

    private readonly GradeManagementContext _dbc = new(UserType.Teacher);

    public async Task GradeStudent(Student student, Course c, decimal gradeResult)
    {
        var sc = await _dbc.Scs.FindAsync(student.Id, c.Id);
        if (sc == null)
        {
            sc = new Sc
            {
                StudentId = student.Id,
                CourseId = c.Id,
                Score = gradeResult
            };
            _dbc.Scs.Add(sc);
        }
        else
        {
            sc.Score = gradeResult;
        }

        await _dbc.SaveChangesAsync();
    }

    public void ChangeGradeCompositionWeight(Tcgc tcgc, decimal weight)
    {
        tcgc.Weight = weight;
        _dbc.Tcgcs.Update(tcgc);
        _dbc.SaveChanges();
    }

    public void AddGradeComposition(GradeComposition gc, decimal weight)
    {
        if (_dbc.Tcgcs.Any(i => i.CourseId == _course.Id && i.GradeCompositionId == gc.Id))
        {
            throw new ArgumentException($"{gc.Name} is already exists.");
        }

        _dbc.Tcgcs.Add(new Tcgc
        {
            CourseId = _course.Id,
            GradeCompositionId = gc.Id,
            TeacherId = _teacher.Id,
            Weight = weight
        });

        _dbc.SaveChanges();
    }

    public void RemoveGradeComposition(Tcgc itemToRemove)
    {
        _dbc.Tcgcs.Remove(itemToRemove);
        _dbc.SaveChanges();
    }

    public IEnumerable<Stc> Stcs =>
        from stc in _teacher.Stcs
        where stc.CourseId == _course.Id
        select stc;

    public IEnumerable<Student> Students =>
        from stc in Stcs
        select stc.Student;

    public IEnumerable<(Student Student, decimal? Grade)> StudentsWithGrades =>
        from s in Stcs
        join sc in _dbc.Scs on
            new { s.StudentId, s.CourseId } equals
            new { sc.StudentId, sc.CourseId }
            into scs
        from sc in scs.DefaultIfEmpty()
        select (s.Student, sc?.Score);

    public IEnumerable<(Student Student, Sc? Sc)> GradedStudents =>
        from s in Stcs
        join sc in _dbc.Scs on
            new { s.StudentId, s.CourseId } equals
            new { sc.StudentId, sc.CourseId }
            into scs
        from sc in scs.DefaultIfEmpty()
        where sc != null
        select (s.Student, sc);

    public IEnumerable<(Student Student, Sc? Sc)> UngradedStudents =>
        from s in Stcs
        join sc in _dbc.Scs on
            new { s.StudentId, s.CourseId } equals
            new { sc.StudentId, sc.CourseId }
            into scs
        from sc in scs.DefaultIfEmpty()
        where sc == null
        select (s.Student, sc);

    public IEnumerable<Tcgc> GradeCompositions =>
        from tcgc in _dbc.Tcgcs
        where tcgc.CourseId == _course.Id && tcgc.TeacherId == _teacher.Id
        select tcgc;

    public IEnumerable<GradeComposition> UnusedGradeCompositions
        => from gc in _dbc.GradeCompositions.ToList()
            where !(from tcgc in _dbc.Tcgcs.ToList()
                where tcgc.CourseId == _course.Id && tcgc.TeacherId == _teacher.Id
                select tcgc.GradeCompositionId).Contains(gc.Id)
            select gc;
}