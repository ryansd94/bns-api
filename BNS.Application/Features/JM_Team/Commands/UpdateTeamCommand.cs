using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Service.Implement.BaseImplement;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Service.Features
{
    public class UpdateTeamCommand : UpdateRequestHandler<UpdateTeamRequest, JM_Team>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTeamCommand(IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public override async Task OtherHandle(UpdateTeamRequest request, JM_Team entity)
        {
            var members = request.ChangeFields.Where(s => s.Key.Equals("members")).FirstOrDefault();
            if (members != null)
            {
                var value = JsonConvert.DeserializeObject<ChangeFieldTransferItem<Guid>>(members.Value.ToString());
                if (value.AddValues.Count > 0)
                {
                    var usersAddNewTeam = await _unitOfWork.Repository<JM_AccountCompany>().Where(s => value.AddValues.Contains(s.UserId)).ToListAsync();
                    if (usersAddNewTeam.Any())
                    {
                        foreach (var item in usersAddNewTeam)
                        {
                            item.TeamId = entity.Id;
                            item.UpdatedDate = DateTime.UtcNow;
                            item.UpdatedUser = request.UserId;
                        }
                    }
                }
                if (value.DeleteValues != null && value.DeleteValues.Count > 0)
                {
                    var usersDeleteTeam = await _unitOfWork.Repository<JM_AccountCompany>().Where(s => value.DeleteValues.Contains(s.Id)).ToListAsync();
                    if (usersDeleteTeam.Any())
                    {
                        foreach (var item in usersDeleteTeam)
                        {
                            item.TeamId = null;
                            item.UpdatedDate = DateTime.UtcNow;
                            item.UpdatedUser = request.UserId;
                        }
                    }
                }
            }
        }
    }
}
