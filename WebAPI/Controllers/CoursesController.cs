﻿using Application.Courses.Commands;
using Application.Courses.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Dapper.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : MyBaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<CourseDTO>>> Get()
        {
            return await Mediator.Send(new Query.CoursesList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetById(Guid id)
        {
            return await Mediator.Send(new QueryById.CourseById { Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.New data)
        {
            return await Mediator.Send(data);
        }

        [HttpPost("report")]
        public async Task<ActionResult<Pagination>> Report(CoursePagination.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditCourse.Edit data)
        {
            data.CourseId = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.DeleteCourse { CourseId = id});
        }
    }
}
