using GymMangmentBLL.ViewModels.AnalyticsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    public interface IAnalyticsService
    {

        AnalyticsViewModel GetAnalyticsData();
    }
}
