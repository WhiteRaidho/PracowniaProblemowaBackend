﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Principal;
using FitKidCateringApp.Attributes;
using FitKidCateringApp.Helpers;
using FitKidCateringApp.Models.Children;
using FitKidCateringApp.Services.Children;
using FitKidCateringApp.Services.Core;
using FitKidCateringApp.Services.Institutions;
using FitKidCateringApp.ViewModels.Children;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitKidCateringApp.Extensions;
using System.Security.Claims;

namespace FitKidCateringApp.Controllers.Core
{
    [Authorize]
    [ApiController]
    [Route("api/children")]
    public class ChildrenController : ControllerBase
    {
        protected IMapper Mapper { get; }

        protected ChildrenService Children { get; }

        protected InstitutionsService Institutions { get; }

        protected CoreUserService Users { get; }

        protected ClaimsPrincipal User { get; }

        public ChildrenController(IMapper mapper, InstitutionsService institutions, CoreUserService users, ChildrenService children, IPrincipal user)
        {
            Mapper = mapper;
            Institutions = institutions;
            Users = users;
            Children = children;
            User = (ClaimsPrincipal)user;
        }

        #region GetById()
        [HttpGet("{publicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [RequireAny(StandardPermissions.AdminAccess, StandardPermissions.CateringEmployee)]
        public async Task<ActionResult<ChildListItemModel>> GetById(Guid publicId)
        {
            var child = Children.GetById(publicId);
            if (child == null) return NotFound();

            var result = Mapper.Map<ChildListItemModel>(child);
            return Ok(result);
        }
        #endregion

        #region GetChildren()
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [RequireAny(StandardPermissions.AdminAccess, StandardPermissions.CateringEmployee)]
        public async Task<ActionResult<List<ChildListItemModel>>> GetChildren()
        {
            var children = Children.GetList();
            var result = Mapper.Map<IEnumerable<ChildListItemModel>>(children);
            return Ok(result);
        }
        #endregion

        #region Create()
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]ChildFormModel model)
        {
            Guid formParentPublicId = model.ParentPublicId;
            Guid formInstitutionPublicId = model.InstitutionPublicId;

            var entity = Mapper.Map<Child>(model);

            var parent = Users.GetCoreUser(formParentPublicId);
            entity.ParentId = parent.Id;
            entity.Parent = parent;

            var institution = Institutions.GetById(formInstitutionPublicId);
            entity.InstitutionId = institution.Id;
            entity.Institution = institution;

            entity = Children.Create(entity);

            return CreatedAtAction(nameof(GetById), new { publicId = entity.PublicId }, Mapper.Map<ChildListItemModel>(entity));
        }
        #endregion

        #region edit()
        [HttpPut("{publicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Edit(Guid publicId, [FromBody]ChildFormModel model)
        {
            Guid formParentPublicId = model.ParentPublicId;
            Guid formInstitutionPublicId = model.InstitutionPublicId;

            var entity = Children.GetById(publicId);

            var parent = Users.GetCoreUser(formParentPublicId);
            entity.ParentId = parent.Id;
            entity.Parent = parent;

            var institution = Institutions.GetById(formInstitutionPublicId);
            entity.InstitutionId = institution.Id;
            entity.Institution = institution;

            entity = Mapper.Map(model, entity);
            entity = Children.Update(entity);

            return Accepted();
        }
        #endregion

        #region Remove()
        [HttpDelete("{publicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Remove(Guid publicId)
        {
            var entity = Children.GetById(publicId);

            if (entity == null) return NotFound();

            Children.Remove(entity);
            return Accepted();
        }
        #endregion

        #region GetMyChildren()
        [HttpGet("mychildren")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ChildListItemModel>>> GetMyChildren()
        {
            var user = Users.GetCoreUser(User.Id());
            if (user == null) return NotFound();

            var children = Children.GetMychild(user.Id);
            var result = Mapper.Map<IEnumerable<ChildListItemModel>>(children);

            return Ok(result);
        }
        #endregion



    }
}
