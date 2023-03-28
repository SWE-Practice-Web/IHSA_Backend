using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public class SchoolRequestHandler : RequestHandler<ISchoolCollection, SchoolRequestModel, SchoolModel>, ISchoolRequestHandler
    {
        private readonly ISchoolCollection _collection;
        private readonly IMapper _mapper;
        public SchoolRequestHandler(
            ISchoolCollection collection,
            IMapper mapper) : base(collection, mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public override SchoolRequestModel PostHandle(SchoolModel entity)
        {
            throw new NotImplementedException();
        }

        public override SchoolModel PreHandle(SchoolRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
