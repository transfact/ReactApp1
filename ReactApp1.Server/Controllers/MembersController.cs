using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{

	[Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly PostContext _context;

        public MembersController(PostContext context)
        {
            _context = context;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        {
            return await _context.Members.Select(x=>ItemToDTO(x)).ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return ItemToDTO(member);
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, MemberDto member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }
			var findMember = await _context.Members.FindAsync(id);
            if (findMember == null)
            {
                return NotFound();
            }
            findMember.MemberName = member.MemberName;
            findMember.MemberDescription = member.MemberDescription;


			//_context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MemberDto>> PostMember(MemberDto member)
        {

			var newMember = new Member
			{
	            MemberId = member.MemberId,
                MemberName = member.MemberName,
                MemberDescription = member.MemberDescription,
                MemberType = member.MemberType,
			};
			_context.Members.Add(newMember);
            await _context.SaveChangesAsync();
			

			return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, ItemToDTO(newMember));
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }

		private static MemberDto ItemToDTO(Member m) => new MemberDto
		{
			MemberId = m.MemberId,
			MemberName = m.MemberName,
			MemberType = m.MemberType,
			MemberDescription = m.MemberDescription
		};
	}



}
