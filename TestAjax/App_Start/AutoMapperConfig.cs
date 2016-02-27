using CommentsDemo.Models;

namespace CommentsDemo
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<CommentsViewModal, CommentsModal>().ReverseMap();
        }
    }
}