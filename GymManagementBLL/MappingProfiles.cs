using AutoMapper;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Models;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region For session
            CreateMap<Session, SessionViewModel>().ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.CategoryName))
                                                      .ForMember(dest => dest.TrainerName, options => options.MapFrom(x => x.Trainer.Name));

            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<UpdateSessionViewModel, Session>();
            #endregion

            #region For Trainer
            CreateMap<Trainer, TrainerToSelectViewModel>();
            #endregion

            #region For category
            CreateMap<Category, CategoryToSelectViewModel>().ForMember(dest => dest.Name,
                                                                       opt=> opt.MapFrom(src => src.CategoryName));
            #endregion
        }
    }
}
