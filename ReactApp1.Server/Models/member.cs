namespace ReactApp1.Server.Models
{
	public class Member
	{
		public int MemberId { get; set; }
		public string? MemberName { get; set; }
		public string? MemberType { get; set; }
		public string? MemberDescription { get; set; }
		
		public string? Secret { get; set; }

	}
}
