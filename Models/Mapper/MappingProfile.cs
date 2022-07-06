using AutoMapper;
using System.Threading.Tasks;

namespace ExpenseTracker.Models.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserCategoryMap, UserCategoryMapModel>().ReverseMap();
            CreateMap<Expenses, ExpensesModel>().ReverseMap();
        }

    }
}
