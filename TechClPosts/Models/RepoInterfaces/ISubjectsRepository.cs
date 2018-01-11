using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechClPosts.Models.AppModels;

namespace TechClPosts.Models.RepoInterfaces
{
    interface ISubjectsRepository
    {
        void AddSubject(Subject subject);

        Subject GetSubject(Guid subjectKey);

        IEnumerable<Subject> AllSubjects();

        void EditSubject(Subject updatedSubject);

        void DeleteSubject(Subject subjectToDelete);
    }
}
