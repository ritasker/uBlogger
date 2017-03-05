namespace uBlogger.Api.Features.Users.Follow
{
    public class FollowUserCommand
    {
        public FollowUserCommand() { }
        public FollowUserCommand(string follower, string followee)
        {
            Follower = follower;
            Followee = followee;
        }
        
        public string Follower { get; set; }
        public string Followee { get; set; }
    }
}