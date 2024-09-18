using API.DTOs.Comment;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository  _commentRepository;
        private readonly IStockRepository  _stockRepository;

        public CommentsController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentsDto = comments.Select(x => x.ToCommmentDto()); 
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if(comment == null)
            {
                return NotFound ();
            }
            return Ok(comment.ToCommmentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId , [FromBody] CreateCommentDto commentDto )
        {
            if(!await _stockRepository.IsExisit(stockId))
            {
                return BadRequest("Stock dose not exisit");
            }
            var commentModel = commentDto.ToCommentFromCreate(stockId);

            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommmentDto());

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateCommentRequestDto commentRequestDto)
        {
            var commentModel = commentRequestDto.ToCommentFromUpdate();
            var comment = await _commentRepository.UpdateAsync(id, commentModel);
            if (comment == null)
                return NotFound("The comment dose not exisit");
            return Ok(comment.ToCommmentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteAsync(id);
            if (comment is null)
            { return NotFound (); }
            return Ok(comment.ToCommmentDto());
        }
    }
}
