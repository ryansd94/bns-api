﻿
using AutoMapper;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Features
{
    public class GetTaskByIdQuery : IRequestHandler<GetTaskByIdRequest, ApiResult<TaskByIdResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private IMediator _mediator;

        public GetTaskByIdQuery(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<TaskByIdResponse>> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<TaskByIdResponse>();
            var task = await _unitOfWork.Repository<JM_Task>()
                .Where(s => s.Id == request.Id && !s.IsDelete)
                .Include(s => s.TaskType)
                .ThenInclude(s => s.Template)
                .ThenInclude(s => s.TemplateStatus)
                .ThenInclude(s => s.Status)
                .Include(s => s.User)
                .Include(s => s.JM_TaskParent)
                .Include(s => s.CommentTasks).ThenInclude(s => s.Comment).ThenInclude(s => s.Chidrens)
                .ThenInclude(s => s.User)
                .Include(s => s.CommentTasks).ThenInclude(s => s.Comment).ThenInclude(s => s.User)
                .Include(s => s.TaskTags).ThenInclude(s => s.Tag).FirstOrDefaultAsync();
            if (task == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            var files = await _unitOfWork.Repository<JM_AttachedFiles>()
                .Include(s => s.File)
                .Where(s => s.EntityId == task.Id)
                .Select(s => new FileUpload
                {
                    Id = s.Id,
                    File = new FileItem
                    {
                        Name = s.File.Name
                    },
                    Url = s.File.Url,
                    IsDelete = s.IsDelete,
                })
                .ToListAsync();
            var taskItem = _mapper.Map<TaskItem>(task);
            var dynamicData = await _unitOfWork.Repository<JM_TaskCustomColumnValue>().Where(s => s.TaskId == task.Id
                 && !s.IsDelete).Select(s => new
                 {
                     id = s.TemplateDetailId.ToString().ToLower(),
                     value = s.Value
                 }).ToDictionaryAsync(r => r.id, r => r.value);
            taskItem.DynamicData = dynamicData;
            taskItem.Files = files;
            var taskChilds = await _unitOfWork.Repository<JM_Task>()
                .Where(s => s.ParentId != null && s.ParentId == task.Id && !s.IsDelete)
                .Select(s => _mapper.Map<TaskItem>(s))
                .ToListAsync();
            var comments = task.CommentTasks.Select(s => s.Comment).ToList();

            response.data.Task = taskItem;
            response.data.TaskType = _mapper.Map<TaskTypeItem>(task.TaskType);
            response.data.Task.TaskParent = _mapper.Map<TaskChildItem>(task.JM_TaskParent);
            response.data.Task.Childs = taskChilds;
            response.data.Comments = GetComments(comments);
            return response;
        }

        private List<CommentResponseItem> GetComments(List<JM_Comment> lstComments)
        {
            var commentParents = lstComments.Where(s => s.ParentId == null).OrderByDescending(s => s.CreatedDate).ToList();
            var commentChilds = lstComments.Where(s => s.ParentId != null).OrderByDescending(s => s.CreatedDate).ToList();
            var result = new List<CommentResponseItem>();

            foreach (var commentParent in commentParents)
            {
                var comment = _mapper.Map<CommentResponseItem>(commentParent);
                result.Add(comment);
            }

            return result;
        }

        //private void GetCommentChild(CommentResponseItem commentParent, List<JM_Comment> lstCommentChilds)
        //{
        //    var commentChilds = lstCommentChilds.Where(s => s.ParentId == commentParent.Id).ToList();
        //    if (commentChilds != null && commentChilds.Count > 0)
        //    {
        //        commentParent.Childrens = commentChilds.Select(s => new CommentResponseItem
        //        {
        //            Id = s.Id,
        //            Value = s.Value,
        //            UpdatedTime = s.UpdatedDate.ToString(),
        //            CountReply = s.CountReply,
        //            User = new User
        //            {
        //                Id = s.User.Id,
        //                FullName = s.User.FullName,
        //                Image = s.User.Image,
        //            }
        //        }).ToList();
        //        foreach (var comment in commentParent.Childrens)
        //        {
        //            GetCommentChild(comment, lstCommentChilds);
        //        }
        //    }
        //}

    }
}
