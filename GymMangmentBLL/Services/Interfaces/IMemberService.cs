using GymMangmentBLL.ViewModels.MemeberViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMember);

        MemberViewModel? GetMemberDetails(int MemberId);
        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);

        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel memberToUpdate);

        bool RemoveMember(int MemberId);
    }
}
