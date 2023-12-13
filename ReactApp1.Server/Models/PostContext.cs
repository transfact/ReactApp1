using Microsoft.EntityFrameworkCore;

namespace ReactApp1.Server.Models;

public class PostContext : DbContext
{
	public PostContext(DbContextOptions<PostContext> options)
		: base(options)
	{
	}

	public DbSet<Member> Members { get; set; } = null!;
}