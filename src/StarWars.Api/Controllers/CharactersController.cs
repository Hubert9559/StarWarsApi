using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarWars.Api.Resources;
using StarWars.Characters.Domain.Commands;
using StarWars.Characters.Domain.ReadModels;
using StarWars.Characters.Domain.Responses;

namespace StarWars.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CharactersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get single page of Star Wars characters 
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="size">Page size</param>
        /// <returns>List of characters</returns>
        /// <response code="400">Validation error</response>
        /// <response code="200">Returns list of characters</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ActionName("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CharacterReadModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery][Range(1, long.MaxValue)] int page, [FromQuery][Range(1, 100)] int size)
        {
            var result = await _mediator.Send(new GetPagedCharactersCommand(page, size));
            return result.Status switch
            {
                CharacterModuleResponseStatus.SUCCESS => Ok(result.Data),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        /// <summary>
        /// Create Star Wars character
        /// </summary>
        /// <param name="resource">New character resource</param>
        /// <returns></returns>
        /// <response code="201">Character succesfully created</response>
        /// <response code="400">Validation error</response>
        /// <response code="406">Name is already taken</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NewCharacterResource resource)
        {
            var result = await _mediator.Send(new CreateCharacterCommand(resource.Name, resource.Episodes, resource.FriendsIds));

            return result.Status switch
            {
                CharacterModuleResponseStatus.SUCCESS => Ok(),
                CharacterModuleResponseStatus.DUPLICATE => StatusCode(StatusCodes.Status406NotAcceptable),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        /// <summary>
        /// Update Star Wars character
        /// </summary>
        /// <param name="resource">Update character resource</param>
        /// <returns></returns>
        /// <response code="200">Character succesfully updated</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">Character not found</response>
        /// <response code="406">Name is already taken</response>
        /// <response code="409">Character can't befriend itself</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateCharacterResource resource)
        {
            var result = await _mediator.Send(new UpdateCharacterCommand(resource.Id, resource.Name, resource.FriendsIds, resource.Episodes));

            return result.Status switch
            {
                CharacterModuleResponseStatus.SUCCESS => Ok(),
                CharacterModuleResponseStatus.NOTFOUND => NotFound(),
                CharacterModuleResponseStatus.DUPLICATE => StatusCode(StatusCodes.Status406NotAcceptable),
                CharacterModuleResponseStatus.SELFFRIEND => StatusCode(StatusCodes.Status409Conflict),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        /// <summary>
        /// Delete Star Wars character
        /// </summary>
        /// <param name="id">Character id</param>
        /// <returns></returns>
        /// <response code="200">Character succesfully deleted</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">Character not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute][Range(1, long.MaxValue)] long id)
        {
            var result = await _mediator.Send(new DeleteCharacterCommand(id));

            return result.Status switch
            {
                CharacterModuleResponseStatus.SUCCESS => Ok(),
                CharacterModuleResponseStatus.NOTFOUND => NotFound(),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }
    }
}